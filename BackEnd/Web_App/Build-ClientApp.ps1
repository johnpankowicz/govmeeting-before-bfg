﻿# Build-ClientApp.ps1
# This script is meant to be run from the WebApp folder as a pre-build step.
# The location of the ClientApp relative to the WebApp is passed as "$source".

Function Main
{
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, Position = 1)] [string] $source
    )

    $usage = "@
    Usage: Build-ClientApp <source-folder>
    #    <source-folder> - Angular ClientApp folder relative to the WebApp
@"
    $me = "Build-ClientApp: "

    $GOVMEETING = $true

    # Uncomment the notice you want to get.
    #[void][Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms")
    #[void][System.Windows.Forms.MessageBox]::Show("It works.")
    #[Console]::Beep(600, 800)

    Write-Host "$me Running pre-build script Build-ClientApp.ps1 " -NoNewline
    Write-Host @args

    $location = Get-Location
    Write-Host "$me My location is $location"

    $destination = $location.Path


    if ($GOVMEETING)
    {
        $webapp = "BackEnd\Web_App".ToLower()
        $source = join-path $destination $source

    } else {
        $webapp = "Web_App".ToLower()
        $source = "C:\GOVMEETING\_SOURCECODE\FrontEnd\ClientApp"
    }

    ##################   Check Web App location   ########################

    # When this command is run, we should already be in Backend\Web_App
    if (!($destination.ToLower().EndsWith($webapp)))
    {
        echo "$me ERROR Current location should end with $webapp"
        exit
    }


    ##################   Check ClientApp location   ########################


    echo "$me ClientApp is $source"
    if (!(Test-Path $source -pathType container))
    {
        echo "$me ERROR $source does not exist"
        exit
    } 


    ##################  Build ClientApp   ########################

    cd $source
    npm run build

}


# Execute Main function. This is excecuted first.
# Main @args

# Pass location of ClientApp relative to the WebApp
$source = "..\..\FrontEnd\ClientApp"

Main $source

#DeleteClientAssets c:\tmp\wwwroot