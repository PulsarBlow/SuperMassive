@echo off
pushd %~dp0
@powershell -ExecutionPolicy Bypass -C .\scripts\publish.ps1
popd
IF "%~1"=="-c" GOTO end
pause
:end
