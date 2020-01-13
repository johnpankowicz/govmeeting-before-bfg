# Test-VsPreBuild.ps1

$_sourceAssets = "..\..\FrontEnd\ClientApp\dist"
$_destAssets = "wwwroot"
$_layout = "Views\shared\_layout.cshtml"
$_homeview = "Views\Home\index.cshtml"  


$location = Get-Location
Write-Host "My location is $location"

$sourceAssets = [IO.Path]::GetFullPath( $_sourceAssets )
echo "sourceAssets is $sourceAssets"

$destAssets = [IO.Path]::GetFullPath( $_destAssets )
echo "destAssets is $destAssets"

$layout = $destAssets = [IO.Path]::GetFullPath( $_layout ) 
echo "layout is $layout"


$homeview = [IO.Path]::GetFullPath( $_homeview )  
echo "homeview is $homeview"


