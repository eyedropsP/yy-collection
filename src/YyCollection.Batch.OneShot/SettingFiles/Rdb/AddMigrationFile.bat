@echo off

set ASPNETCORE_ENVIRONMENT=Development
set OutputDirectory=SettingFiles/Rdb/Migrations
set MigrationName=Initialize

cd ../../
dotnet ef migrations add %MigrationName% --output-dir %OutputDirectory%

pause