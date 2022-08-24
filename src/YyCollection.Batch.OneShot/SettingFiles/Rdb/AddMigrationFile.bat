@echo off

set OutputDirectory=SettingFiles/Rdb/Migrations
set MigrationName=

cd ../../
dotnet ef migrations add %MigrationName% --output-dir %OutputDirectory%
@REM dotnet ef migrations remove

pause