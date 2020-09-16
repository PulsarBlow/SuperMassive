#!/bin/sh

# Remove sonar scanner
dotnet tool uninstall  dotnet-sonarscanner --tool-path ./.sonar/scanner
