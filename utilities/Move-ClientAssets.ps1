# Move-ClientAssets.ps1

Function Main
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, Position = 1)] [string] $source
    )

    $usage = "@
    Usage: move-client-assets <source-folder>
    #    <source-folder> - Angular ClientApp folder
    # Move assets from ClientApp to Web_App.
    # Step 1: Move files from dist folder in ClientApp to wwwroot folder in WebApp.
    # Step 2: Edit _layout.cshtml file to match index.html in ClientApp (with slight modifications).
    # Step 3: Edit index.cshtml in Web_App Home controller to just contain Angular app element.
@"
    $me = "Move-ClientAssets: "

    $GOVMEETING = $true

    # Uncomment the notice you want to get.
    #[void][Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms")
    #[void][System.Windows.Forms.MessageBox]::Show("It works.")
    #[Console]::Beep(600, 800)

    Write-Host "$me Running pre-build script Move-ClientAssets.ps1 " -NoNewline
    Write-Host @args

    $location = Get-Location
    Write-Host "$me My location is $location"

    $destination = $location.Path
    $_sourceAssets = "dist\ClientApp"
    $_destAssets = "wwwroot"
    $_indexpage = "wwwroot\index.html" 


    if ($GOVMEETING)
    {
        $webapp = "BackEnd\Web_App"
        $_source = "..\..\FrontEnd\ClientApp"
        $source = join-path $destination $_source
        $_layout = "Features\Shared\_layout.cshtml"
        $_homeview = "Features\Home\index.cshtml" 

    } else {
        $webapp = "Web_App"
        $source = "C:\GOVMEETING\_SOURCECODE\FrontEnd\ClientApp"
        $_layout = "Views\shared\_layout.cshtml"
        $_homeview = "Views\Home\index.cshtml" 
    }

    ##################   Check Web App location   ########################

    # When this command is run, we should already be in Backend\Web_App
    # But we want to make absolutely sure. We will be deleting some contents of this folder and modifying others.
    if (!($destination.EndsWith($webapp)))
    {
        echo "$me ERROR Current location should end with $webapp"
        exit
    }


    ##################   set assets destination   ########################


    $destAssets = join-path $destination $_destAssets
    echo "$me destAssets is $destAssets"
    if (!(Test-Path $destAssets -pathType container))
    {
        echo "$me ERROR $destAssets does not exist"
        exit
    } 

    ##################   set assets source   ########################
    
    $sourceAssets = [IO.Path]::GetFullPath( (join-path $source $_sourceAssets) )
    echo "$me sourceAssets is $sourceAssets"
    if (!(Test-Path $sourceAssets -pathType container))
    {
        echo "$me ERROR $sourceAssets does not exist"
        exit
    }


    ##################   set layout page   ########################

    $layout = join-path $destination $_layout
    echo "$me layout is $layout"
    if (!(Test-Path $layout -pathType leaf))
    {
        echo "$me ERROR $layout does not exist"
        exit
    }


    ##################   set Home controller page   ########################

    $homeview = join-path $destination $_homeview 
    echo "$me homeview is $homeview"
    if (!(Test-Path $homeview -pathType leaf))
    {
        echo "$me ERROR $homeview does not exist"
        exit
    }

    ##################   set index page   ########################

    $indexpage = join-path $destination $_indexpage
    echo "$me indexpage is $indexpage"
    # NOTE: we can't test for existence until we copy the client assets.


    ##################  Copy Assets   ########################

    MoveClientAssets $sourceAssets $destAssets $layout $homeview $indexpage

}


Function MoveClientAssets
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, Position = 1)] [string] $sourceAssets,
        [Parameter(Mandatory = $true, Position = 2)] [string] $destAssets,
        [Parameter(Mandatory = $true, Position = 3)] [string] $layout,
        [Parameter(Mandatory = $true, Position = 4)] [string] $homeview,
        [Parameter(Mandatory = $true, Position = 5)] [string] $indexpage
    )


    DeleteFolderContentsMax100 $destAssets

    CopyFolderContents $sourceAssets $destAssets

    if (!(Test-Path $indexpage -pathType leaf))
    {
        echo "$me ERROR $indexpage does not exist"
        exit
    }

    $content = EditLayoutPage $indexpage

    (EditLayoutPage $indexpage) | set-content $layout

    "<app-root></app-root>"  | set-content $homeview

}


# Delete folder contents, but not if it contains > 100 items.
Function DeleteFolderContentsMax100($folder)
{
    # deleting everything in a user-supplied folder is dangerous.
    # Count the items that we will delete and abort if over 100.
    $count = ( Get-ChildItem $folder -Recurse | Measure-Object ).Count
    if ($count -gt 100)
    {
        echo "ERROR  $me There are $count items in $folder. Are you sure you want to delete it?"
        exit
    }

    Get-Childitem $folder -Recurse | ForEach-Object { 
        Remove-Item $_.FullName -Recurse -Force
    }
}

Function CopyFolderContents($source, $destination)
{
    $sourceContents = $source + "\*"
    Copy-item -Recurse $sourceContents -Destination $destination
}

Function EditLayoutPage($page)
{
    # No, this is not the fastest nor the most compact way to do this.
    # But it is a lot more readable than doing it in one command line.
    # And there is no reason to care about speed.
    $content = get-content $page
    $content = $content -replace '<app-root></app-root>','@RenderBody()'
    $content = $content -replace 'script src="(?!http)', 'script src="~/'
    $content = $content -replace 'rel="stylesheet" href="(?!http)', 'rel="stylesheet" href="~/'
    $content = $content -replace 'link rel="icon" type="image/x-icon" href="', 'link rel="icon" type="image/x-icon" href="~/'
    return $content
}


# Execute Main function. This is excecuted first.
# Main @args

$source = "..\..\FrontEnd\ClientApp"

Main $source
