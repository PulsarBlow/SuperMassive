#!/bin/sh
SONARQUBE_PROJECTVERSION="$(date +%Y%m%d%H%M%S).$(git rev-parse --short HEAD)"
echo "SonarQube Project Version=${SONARQUBE_PROJECTVERSION}"

# Update (or install) Scanner
dotnet tool update dotnet-sonarscanner --tool-path ./.sonar/scanner

# Start scanner
./.sonar/scanner/dotnet-sonarscanner begin \
    /v:$SONARQUBE_PROJECTVERSION \
    /k:"PulsarBlow_SuperMassive" \
    /o:"pulsarblow-github" \
    /d:sonar.login=$SONAR_TOKEN \
    /d:sonar.host.url="https://sonarcloud.io" \
    /d:sonar.cs.opencover.reportsPaths=./.build/coverage/coverage.opencover.xml \
    /d:sonar.coverage.exclusions=**/SuperMassive.ExceptionHandling/**,**/SuperMassive.ExceptionHandling.Logging/**,**/SuperMassive.Logging/**,**/SuperMassive.Logging.AzureTable/**

# Build & Test
./build/build-release.sh

# Stop scanner
./.sonar/scanner/dotnet-sonarscanner end /d:sonar.login=$SONAR_TOKEN

# Use this if you want to uninstall scanner
# dotnet tool uninstall  dotnet-sonarscanner --tool-path ./.sonar/scanner
