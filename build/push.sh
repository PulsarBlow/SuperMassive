#!/bin/sh

echo "=== PUSHING ==="
dotnet nuget push .build/nuget/**/*.nupkg -k $NUGET_TOKEN -sk $NUGET_TOKEN --skip-duplicate -s https://api.nuget.org/v3/index.json
dotnet nuget push .build/nuget/**/*.snupkg -k $NUGET_TOKEN -sk $NUGET_TOKEN --skip-duplicate
echo ""
