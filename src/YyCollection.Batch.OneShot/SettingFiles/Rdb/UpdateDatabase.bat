@echo off

set ASPNETCORE_ENVIRONMENT=Development

cd ../../
dotnet ef database update 

pause