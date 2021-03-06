/*
   05 March 201411:58:44
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
ALTER TABLE dbo.UserPasswords SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserConnects
	DROP CONSTRAINT DF_UserConnects_Origin
GO
ALTER TABLE dbo.UserConnects
	DROP CONSTRAINT DF_UserConnects_CreateWhen
GO
CREATE TABLE dbo.Tmp_UserConnects
	(
	Id int NOT NULL IDENTITY (1, 1),
	UserId int NOT NULL,
	UserConnectStatusId int NOT NULL,
	UserPasswordId int NULL,
	Origin varchar(50) NOT NULL,
	Token nvarchar(250) NULL,
	LastUsed smalldatetime NULL,
	CreatedWhen smalldatetime NOT NULL,
	CreatedBy int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_UserConnects SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_UserConnects ADD CONSTRAINT
	DF_UserConnects_Origin DEFAULT ('web') FOR Origin
GO
ALTER TABLE dbo.Tmp_UserConnects ADD CONSTRAINT
	DF_UserConnects_CreateWhen DEFAULT (getdate()) FOR CreatedWhen
GO
SET IDENTITY_INSERT dbo.Tmp_UserConnects ON
GO
IF EXISTS(SELECT * FROM dbo.UserConnects)
	 EXEC('INSERT INTO dbo.Tmp_UserConnects (Id, UserId, UserConnectStatusId, Origin, Token, LastUsed, CreatedWhen, CreatedBy)
		SELECT Id, UserId, UserConnectStatusId, Origin, Token, LastUsed, CreatedWhen, CreatedBy FROM dbo.UserConnects WITH (HOLDLOCK TABLOCKX)')
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
ALTER TABLE dbo.UserConnects ADD CONSTRAINT
	FK_UserConnects_UserPasswords FOREIGN KEY
	(
	UserPasswordId
	) REFERENCES dbo.UserPasswords
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
