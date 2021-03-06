/*
   04 March 201419:08:40
   User: 
   Server: (local)
   Database: Budget
   Application: 
*/

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
ALTER TABLE dbo.UserConnects
	DROP CONSTRAINT DF_UserConnects_CreateWhen
GO
CREATE TABLE dbo.Tmp_UserConnects
	(
	Id int NOT NULL IDENTITY (1, 1),
	UserId int NOT NULL,
	UserConnectStatusId int NOT NULL,
	Origin varchar(50) NOT NULL,
	Token nvarchar(250) NULL,
	CreatedWhen smalldatetime NOT NULL,
	CreatedBy int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_UserConnects SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_UserConnects ADD CONSTRAINT
	DF_UserConnects_Origin DEFAULT 'web' FOR Origin
GO
ALTER TABLE dbo.Tmp_UserConnects ADD CONSTRAINT
	DF_UserConnects_CreateWhen DEFAULT (getdate()) FOR CreatedWhen
GO
SET IDENTITY_INSERT dbo.Tmp_UserConnects ON
GO
IF EXISTS(SELECT * FROM dbo.UserConnects)
	 EXEC('INSERT INTO dbo.Tmp_UserConnects (Id, UserId, UserConnectStatusId, CreatedWhen, CreatedBy)
		SELECT Id, UserId, UserConnectStatusId, CreatedWhen, CreatedBy FROM dbo.UserConnects WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_UserConnects OFF
GO
DROP TABLE dbo.UserConnects
GO
EXECUTE sp_rename N'dbo.Tmp_UserConnects', N'UserConnects', 'OBJECT' 
GO
ALTER TABLE dbo.UserConnects ADD CONSTRAINT
	PK_UserConnects PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
