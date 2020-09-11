#!/bin/sh

# Begin Sonar scan
./.sonar/scanner/dotnet-sonarscanner begin \
    /k:"PulsarBlow_SuperMassive" /o:"pulsarblow-github" \
    /d:sonar.login=$SONAR_TOKEN \
    /d:sonar.host.url="https://sonarcloud.io"
    /d:sonar.cs.opencover.reportsPaths=./.build/coverage/coverage.opencover.xml

# Build & Test
dotnet build
dotnet test --no-build /p:CollectCoverage=true  /p:CoverletOutput=../../.build/coverage/ /p:MergeWith="../../.build/coverage/coverage.json" /p:CoverletOutputFormat=\"opencover,json\" -m:1

# End Sonar scan
./.sonar/scanner/dotnet-sonarscanner end /d:sonar.login=$SONAR_TOKEN
