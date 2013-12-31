/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.PeriodTypes
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name nvarchar(50) NOT NULL,
	Description nvarchar(512) NULL,
	Hours smallint,
	Days smallint,
	Weeks smallint,
	WorkingDays smallint,
	Monthes smallint,
	Quarters smallint,
	Years smallint
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PeriodTypes ADD CONSTRAINT
	PK_PeriodTypes PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.PeriodTypes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


INSERT INTO [Budget].[dbo].[PeriodTypes] ([Name],[Days])
     VALUES
           (N'Каждый день'
           ,1)
GO
INSERT INTO [Budget].[dbo].[PeriodTypes] ([Name],[WorkingDays])
     VALUES
           (N'Каждый рабочий день'
           ,1)
GO
INSERT INTO [Budget].[dbo].[PeriodTypes] ([Name],[Weeks])
     VALUES
           (N'Каждую неделю'
           ,1)
GO
INSERT INTO [Budget].[dbo].[PeriodTypes] ([Name],[Monthes])
     VALUES
           (N'Каждый месяц'
           ,1)
GO
INSERT INTO [Budget].[dbo].[PeriodTypes] ([Name],[Quarters])
     VALUES
           (N'Каждый квартал'
           ,1)
GO
INSERT INTO [Budget].[dbo].[PeriodTypes] ([Name],[Years])
     VALUES
           (N'Каждый год'
           ,1)
GO
