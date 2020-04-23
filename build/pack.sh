#!/bin/sh

echo "=== INITIALIZING ==="
export GIT_COMMIT=$(git rev-parse HEAD)
export GIT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
echo "GIT_COMMIT=$GIT_COMMIT"
echo "GIT_BRANCH=$GIT_BRANCH"
echo ""

echo "=== PACKING ==="
rm -rf .build/nuget
dotnet pack -o .build/nuget --include-symbols -c Release --no-restore -p:RepositoryBranch=$GIT_BRANCH -p:RepositoryCommit=$GIT_COMMIT
echo ""
