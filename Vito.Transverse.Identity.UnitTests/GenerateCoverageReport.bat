::================================================================================================================
:: File: GenerateCoverageReport.bat
:: Created Date: 2023-Aug-22
:: This Bat File Collet Unit Test Coverage Data using Dot Net Tool and Generate a Friendly Formatted of the Coverage 
::
:: Obs: Thi File use ReportGenerator Nutget published by Nutget.org under Apache-2.0 license - Url: https://www.nuget.org/packages/ReportGenerator
::================================================================================================================
::Remove Older Folder TestResults
rmdir /s /q TestResults
::Remove Older Folder TestResults
rmdir /s /q CoverageReport
:: Execute Dot Net Tool to generate Unit Test Code Coverage
dotnet test --collect:"XPlat Code Coverage"

::Execute RportGenerator for a Friendly Formatted of the Coverage 
:: filefilters property allow +Include -Exclude Folders
"%UserProfile%\.nuget\packages\reportgenerator\5.1.24\tools\net6.0\ReportGenerator.exe" -reports:TestResults\*\coverage.cobertura.xml -targetdir:CoverageReport 
::-filefilters:"-*Configuration.cs;-*Constants*;-*Contracts*;-*DataBase*;-*Extensions*;-*Helpers*;-*Intake*;-*Localization*;-*Logging*;-*Migrations*;-*Models*;-*Notifications*;-*Sources*;-*Filters*;-*Validators*"

::Run Coverage Report
"CoverageReport/index.html"