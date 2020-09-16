#!/bin/sh
rm -rf .build
dotnet restore
dotnet build --configuration Release --no-restore

# -m:1 parameters is mandatory because mergeWith requires a sequential test report generation
# as it consolidates the final report over each previous report
dotnet test  --no-restore -m:1 \
    /p:Exclude=\"[coverlet.*.tests?]*,[*]Coverlet.Core*\" \
    /p:ExcludeByAttribute=\"Obsolete,GeneratedCodeAttribute,CompilerGeneratedAttribute\" \
    /p:Include=\"[supermassive.*]*,[*]SuperMassive*\" \
    /p:CollectCoverage=true \
    /p:CoverletOutput="../../.build/coverage/" \
    /p:CoverletOutputFormat=\"opencover,cobertura,json\" \
    /p:MergeWith="../../.build/coverage/coverage.json"
