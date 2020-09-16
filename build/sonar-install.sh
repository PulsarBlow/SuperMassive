#!/bin/sh

# Update (or install) Scanner
dotnet tool update dotnet-sonarscanner --tool-path ./.sonar/scanner
