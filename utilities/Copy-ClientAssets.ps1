# Copy-ClientAssets.ps1

Function Main
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, Position = 1)] [string] $source
    )

    $usage = "@
    Usage: Copy-ClientAssets <source-folder>
    #    <source-folder> - Angular ClientApp folder
    # Copy assets from ClientApp dist folder to Web_App wwwroot.
@"
    $me = "Copy-ClientAssets: "

    $GOVMEETING = $true

    # Uncomment the notice you want to get.
    #[void][Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms")
    #[void][System.Windows.Forms.MessageBox]::Show("It works.")
    #[Console]::Beep(600, 800)

    Write-Host "$me Running pre-build script Copy-ClientAssets.ps1 " -NoNewline
    Write-Host @args

    $location = Get-Location
    Write-Host "$me My location is $location"

    $destination = $location.Path
    $_sourceAssets = "dist\ClientApp"
    $_destAssets = "wwwroot"


    if ($GOVMEETING)
    {
        $webapp = "BackEnd\Web_App"
        $_source = "..\..\FrontEnd\ClientApp"
        $source = join-path $destination $_source

    } else {
        $webapp = "Web_App"
        $source = "C:\GOVMEETING\_SOURCECODE\FrontEnd\ClientApp"
    }

    ##################   Check Web App location   ########################

    # When this command is run, we should already be in Backend\Web_App
    # But we want to make absolutely sure. We will be deleting the contents of this folder.
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


    ##################  Copy Assets   ########################

    CopyClientAssets $sourceAssets $destAssets

}


Function CopyClientAssets
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, Position = 1)] [string] $sourceAssets,
        [Parameter(Mandatory = $true, Position = 2)] [string] $destAssets
    )


    DeleteFolderContentsMax100 $destAssets

    CopyFolderContents $sourceAssets $destAssets

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


# Execute Main function. This is excecuted first.
# Main @args

$source = "..\..\FrontEnd\ClientApp"

Main $source
