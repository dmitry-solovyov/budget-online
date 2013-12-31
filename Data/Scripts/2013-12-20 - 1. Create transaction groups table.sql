USE [Budget]
GO

/****** Object:  Table [dbo].[TransactionGroups]    Script Date: 12/20/2013 19:51:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TransactionGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[TransactionTypeId] [int] NOT NULL,
	[CategoryId] [int] NULL,
	[FromAccountId] [int] NULL,
	[ToAccountId] [int] NULL,
	[FromSum] [numeric](18, 2) NULL,
	[ToSum] [numeric](18, 2) NULL,
	[FromCurrencyId] [int] NULL,
	[ToCurrenyId] [int] NULL,
	[Description] [nvarchar](512) NULL,
	[Tags] [nvarchar](512) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedWhen] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedWhen] [datetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_TransactionGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Accounts] FOREIGN KEY([FromAccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Accounts]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Accounts1] FOREIGN KEY([ToAccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Accounts1]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Categories]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Currencies] FOREIGN KEY([ToCurrenyId])
REFERENCES [dbo].[Currencies] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Currencies]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Currencies1] FOREIGN KEY([FromCurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Currencies1]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Sections] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Sections]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_TransactionTypes] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionTypes] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_TransactionTypes]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Users]
GO

ALTER TABLE [dbo].[TransactionGroups]  WITH CHECK ADD  CONSTRAINT [FK_TransactionGroups_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[TransactionGroups] CHECK CONSTRAINT [FK_TransactionGroups_Users1]
GO

ALTER TABLE [dbo].[TransactionGroups] ADD  CONSTRAINT [DF_TransactionGroups_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO

ALTER TABLE [dbo].[TransactionGroups] ADD  CONSTRAINT [DF_TransactionGroups_CreatedWhen]  DEFAULT (getdate()) FOR [CreatedWhen]
GO


