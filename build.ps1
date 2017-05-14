Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Function Get-GitRevision {
    git rev-parse --verify --short HEAD
}

$sonarProjectName = "SuperMassive"
$sonarOrganization = "pulsarblow-github"
$sonarLogin = Get-Content .\sonarqube.secret.txt
if(!$sonarLogin) { Exit 99 }
$sonarHostUrl = "https://sonarqube.com"
$sonarVersion = Get-GitRevision
$msbuildexe = "D:\Programs\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MsBuild.exe"
$nunitexe = "E:\Temp\.nuget\nunit.consolerunner\3.6.1\tools\nunit3-console.exe"
$codecoverageexe = "D:\Programs\Microsoft Visual Studio\2017\Enterprise\Team Tools\Dynamic Code Coverage Tools\CodeCoverage.exe"
$outputPath = ".\build"
$coverageReportXml = Join-Path $outputPath ("coverage-report.xml")
$testAssemblies = @(
    ".\Source\UnitTests\SuperMassive.Tests\bin\Debug\net462\SuperMassive.Tests.dll",
    ".\Source\UnitTests\SuperMassive.Fakers.Tests\bin\Debug\net462\SuperMassive.Fakers.Tests.dll",
    ".\Source\UnitTests\SuperMassive.Logging.Tests\bin\Debug\net462\SuperMassive.Logging.Tests.dll",
    ".\Source\UnitTests\SuperMassive.Logging.AzureTable.Tests\bin\Debug\net462\SuperMassive.Logging.AzureTable.Tests.dll"
)

if (Test-Path $outputPath) { Remove-Item -Recurse -Force $outputPath }
New-Item -ItemType Directory -Force -Path $outputPath
if (Test-Path $coverageReportXml) { Remove-Item $CodeCoverageReportFile }

SonarQube.Scanner.MSBuild.exe begin /k:$sonarProjectName /d:"sonar.host.url=${sonarHostUrl}" /d:"sonar.organization=${sonarOrganization}" /d:"sonar.login=${sonarLogin}" /d:"sonar.cs.vscoveragexml.reportsPaths=${coverageReportXml}" /v:$sonarVersion

& $msbuildexe /t:Rebuild /p:Configuration="Debug"
if ($testAssemblies -ne $null) {
    $coverageFiles = @()
    ForEach ($assembly in $testAssemblies) {
        $assemblyName = [io.Path]::GetFilenamewithoutExtension($assembly)
        $coverageOutputFile = Join-Path $outputPath ($assemblyName + ".coverage")
        if (Test-Path $coverageOutputFile) { Remove-Item $coverageOutputFile }
        & $codecoverageexe collect /output:$coverageOutputFile /verbose $nunitexe $assembly --work=$outputPath
        $coverageFiles += $coverageOutputFile
    }
    & $codecoverageexe analyze /output:$coverageReportXml $coverageFiles
}

SonarQube.Scanner.MSBuild.exe end /d:"sonar.login=${sonarLogin}"