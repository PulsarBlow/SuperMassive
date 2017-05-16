$pushSource = "https://www.nuget.org/api/v2/package"
$packageDirectory = "..\build"
$nugetExe = "nuget.exe"

function PushPackages() {
    ""
    "Getting all *.nupkg files to push to $pushSource"

    $files = Get-ChildItem $packageDirectory -Filter *.nupkg

    if ($files.Count -eq 0) {
        ""
        "**** No nupkg files found in the directory: $packageDirectory"
        "Terminating process."
        throw;
    }

    "Found: " + $files.Count + " files"

    foreach ($file in $files) {
        &$nugetExe push ($file.FullName) -Source $pushSource
    }
}

PushPackages
