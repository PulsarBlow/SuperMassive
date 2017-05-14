@echo off
pushd %~dp0
@powershell -ExecutionPolicy Bypass -C .\build.ps1
popd
IF "%~1"=="-c" GOTO end
pause
:end