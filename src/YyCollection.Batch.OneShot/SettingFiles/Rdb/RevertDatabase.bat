@echo off

set MigrationName= 

cd ../../
dotnet ef database update 0
@REM dotnet ef database update %MigrationName%

pause