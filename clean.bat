ECHO OFF
git clean -xfd -e *.csproj.user -e .vs -e *.suo -e *.ps1 -e *.secret.txt --dry-run
ECHO PROCEED AND CLEAN THE PREVIOUS LIST ?
ECHO WARNING: THIS IS UNRECOVERABLE
PAUSE
ECHO CONFIRM AGAIN
PAUSE
git clean -xfd -e *.csproj.user -e .vs -e *.suo -e *.ps1 -e *.secret.txt