@ECHO OFF
ECHO Publishing to NuGet.org
for /R %%f in (*.nupkg) do (
	NuGet push %%f
)