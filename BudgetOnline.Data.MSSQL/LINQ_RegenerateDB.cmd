@echo off

if "%ProgramFiles(x86)%XXX"=="XXX" goto x86
@echo on

"%PROGRAMFILES(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\SqlMetal.exe" /conn:"Server=.;Database=Budget;Integrated Security=SSPI;" /dbml:BudgetOnlineDB.dbml /context:BudgetOnlineDBDataContext /pluralize /serialization:Unidirectional /namespace:BudgetOnline.Data.MSSQL /language:C# /views
"%PROGRAMFILES(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\SqlMetal.exe" /conn:"Server=.;Database=Budget;Integrated Security=SSPI;" /code:BudgetOnlineDB.designer.cs /context:BudgetOnlineDBDataContext /pluralize /serialization:Unidirectional /namespace:BudgetOnline.Data.MSSQL /language:C# /views

goto checkdone
 

:x86
@echo on

"%PROGRAMFILES%\Microsoft SDKs\Windows\v7.0A\Bin\SqlMetal.exe" /conn:"Server=.;Database=Budget;Integrated Security=SSPI;" /dbml:BudgetOnlineDB.dbml /context:BudgetOnlineDBDataContext /pluralize /serialization:Unidirectional /namespace:BudgetOnline.Data.MSSQL /language:C# /views
"%PROGRAMFILES%\Microsoft SDKs\Windows\v7.0A\Bin\SqlMetal.exe" /conn:"Server=.;Database=Budget;Integrated Security=SSPI;" /code:BudgetOnlineDB.designer.cs /context:BudgetOnlineDBDataContext /pluralize /serialization:Unidirectional /namespace:BudgetOnline.Data.MSSQL /language:C# /views
 
:checkdone

pause