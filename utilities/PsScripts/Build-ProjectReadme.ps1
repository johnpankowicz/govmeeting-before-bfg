# BuildProjectReadme.ps1
# Build the project's Readme.md from overview.md and setup.md in ClientApp/src/assets/docs.

Function BuildReadme
{

  [CmdletBinding()]
  param (
      [Parameter(Position = 1)] [string] $clientapp,
      [Parameter(Position = 2)] [string] $readme
  )
  Write-Host "############################ Build-ProjectReadme.ps1 ############################"

    $me = "BuildProjectReadme: "
    $useAllofSetup = $true

$preSetup =
@"
`n# Developer Setup `n
"@

$postSetup =
@"
`n<a href="http://www.govmeeting.org/about?id=setup#continue">Setup Continued</a>`n
"@

$postOverview =
@"
`n<a href="http://www.govmeeting.org/overview#continue">Overview Continued</a>`n
"@

    $overviewDoc = $clientapp + "\src\assets\docs\overview.md"
    $setupDoc = $clientapp + "\src\assets\docs\setup.md"

    $overview = get-content -Raw $overviewDoc
    $index = $overview.IndexOf('<!-- END OF README SECTION -->')
    if ($index -gt 0) {
      $overview = $overview.Substring(0, $index) + $postOverview
    }

    $setup = get-content -Raw $setupDoc
    if ($useAllOfSetup){
      $setup = $preSetup + $setup
    } else {
      $index = $setup.IndexOf('<!-- END OF README SECTION -->')
      if ($index -gt 0) {
        $setup = $preSetup + $setup.Substring(0, $index) + $postSetup
      }
    }

    $overview + $setup | set-content $readme
}

BuildReadme @args
