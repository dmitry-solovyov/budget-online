USE [Budget]
GO

/****** Object:  Table [dbo].[CurrencyRates]    Script Date: 07/10/2013 14:34:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CurrencyRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SectionId] [int] NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[BaseCurrencyId] [int] NOT NULL,
	[TargetCurrencyId] [int] NOT NULL,
	[Rate] [numeric](18, 2) NOT NULL,
	[IsDisabled] [bit] NOT NULL,
	[CreatedWhen] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedWhen] [datetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_CurrencyRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CurrencyRates]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRates_Currencies] FOREIGN KEY([BaseCurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO

ALTER TABLE [dbo].[CurrencyRates] CHECK CONSTRAINT [FK_CurrencyRates_Currencies]
GO

ALTER TABLE [dbo].[CurrencyRates]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRates_Currencies1] FOREIGN KEY([TargetCurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO

ALTER TABLE [dbo].[CurrencyRates] CHECK CONSTRAINT [FK_CurrencyRates_Currencies1]
GO

ALTER TABLE [dbo].[CurrencyRates]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRates_Sections] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Sections] ([Id])
GO

ALTER TABLE [dbo].[CurrencyRates] CHECK CONSTRAINT [FK_CurrencyRates_Sections]
GO

ALTER TABLE [dbo].[CurrencyRates]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRates_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[CurrencyRates] CHECK CONSTRAINT [FK_CurrencyRates_Users]
GO

ALTER TABLE [dbo].[CurrencyRates]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyRates_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[CurrencyRates] CHECK CONSTRAINT [FK_CurrencyRates_Users1]
GO

ALTER TABLE [dbo].[CurrencyRates] ADD  CONSTRAINT [DF_CurrencyRates_IsDisabled]  DEFAULT ((0)) FOR [IsDisabled]
GO

ALTER TABLE [dbo].[CurrencyRates] ADD  CONSTRAINT [DF_CurrencyRates_CreatedWhen]  DEFAULT (getdate()) FOR [CreatedWhen]
GO


