@ECHO OFF
ECHO Delete Existing Packages
for /r %%f in (*.nupkg) do del %%f
ECHO Packing Projects
NuGet Pack ..\Source\SuperMassive\SuperMassive.csproj -OutputDirectory ".\SuperMassive" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ..\Source\SuperMassive.ExceptionHandling\SuperMassive.ExceptionHandling.csproj -OutputDirectory ".\SuperMassive.ExceptionHandling" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ..\Source\SuperMassive.ExceptionHandling.Logging\SuperMassive.ExceptionHandling.Logging.csproj -OutputDirectory ".\SuperMassive.ExceptionHandling.Logging" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ..\Source\SuperMassive.Fakers\SuperMassive.Fakers.csproj -OutputDirectory ".\SuperMassive.Fakers" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ..\Source\SuperMassive.Logging\SuperMassive.Logging.csproj -OutputDirectory ".\SuperMassive.Logging" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
NuGet Pack ..\Source\SuperMassive.Logging.AzureTable\SuperMassive.Logging.AzureTable.csproj -OutputDirectory ".\SuperMassive.Logging.AzureTable" -Build -IncludeReferencedProjects -Prop Configuration=Release -Symbols
