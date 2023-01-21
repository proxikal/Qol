@echo off


REM Restore + Build
dotnet build "QRepl" --nologo || exit /b

REM Run
dotnet run -p "QRepl" --no-build