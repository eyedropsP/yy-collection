@echo off

set OutputDirectory=SettingFiles/Rdb/Migrations
set MigrationName=Initialize

cd ../../
dotnet ef migrations add %MigrationName% --output-dir %OutputDirectory%

pause