@echo off


REM Restore + Build
dotnet build --nologo || exit /b

REM Test
dotnet test "QolTest" --nologo --no-build