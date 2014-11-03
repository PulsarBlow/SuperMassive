@ECHO OFF
ECHO Delete Existing Packages
for /r %%f in (*.nupkg) do del %%f
ECHO Packing Projects
NuGet Pack ..\Source\SuperMassive\SuperMassive.csproj -OutputDirectory ".\SuperMassive" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ..\Source\SuperMassive.Fakers\SuperMassive.Fakers.csproj -OutputDirectory ".\SuperMassive.Fakers" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
