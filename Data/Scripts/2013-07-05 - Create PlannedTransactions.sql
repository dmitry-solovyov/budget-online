USE [Budget]
GO

/****** Object:  Table [dbo].[PlannedTransactions]    Script Date: 07/05/2013 15:01:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlannedTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionId] [int] NOT NULL,
	[PeriodTypeId] [int] NOT NULL,
	[TransactionTypeId] [int] NOT NULL,
	[AccountId] [int] NULL,
	[CategoryId] [int] NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NULL,
	[Amount] [numeric](18, 2) NOT NULL,
	[Sum] [numeric](18, 2) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Description] [nvarchar](512) NULL,
	[Tags] [nvarchar](512) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedWhen] [smalldatetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedWhen] [smalldatetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_PlannedTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_Accounts]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_Categories]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_Currencies] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_Currencies]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_PeriodTypes] FOREIGN KEY([PeriodTypeId])
REFERENCES [dbo].[PeriodTypes] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_PeriodTypes]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Sections] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_Sections]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_TransactionTypes] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionTypes] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_TransactionTypes]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_Users]
GO

ALTER TABLE [dbo].[PlannedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PlannedTransactions_Users_Updated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[PlannedTransactions] CHECK CONSTRAINT [FK_PlannedTransactions_Users_Updated]
GO

ALTER TABLE [dbo].[PlannedTransactions] ADD  CONSTRAINT [DF_PlannedTransactions_Amount]  DEFAULT ((1)) FOR [Amount]
GO

ALTER TABLE [dbo].[PlannedTransactions] ADD  CONSTRAINT [DF_PlannedTransactions_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO

ALTER TABLE [dbo].[PlannedTransactions] ADD  CONSTRAINT [DF_PlannedTransactions_CreatedWhen]  DEFAULT (getdate()) FOR [CreatedWhen]
GO


