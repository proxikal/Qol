@echo off


REM Restore + Build
dotnet build "QCompiler" --nologo || exit /b

REM Run
dotnet run --project "QCompiler" -- %*