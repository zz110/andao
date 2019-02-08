USE [cp]
GO

/****** Object:  Table [dbo].[MonthlyEvaluation]    Script Date: 2019/2/8 17:59:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MonthlyEvaluation](
	[Id] [varchar](50) NULL,
	[Category] [varchar](50) NULL,
	[UserId] [varchar](50) NULL,
	[OrgId] [varchar](50) NULL,
	[EvaluateYear] [int] NULL,
	[EvaluateMonth] [int] NULL,
	[Score] [decimal](18, 2) NULL,
	[Creator] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[Grade] [int] NULL,
	[Notes] [varchar](255) NULL,
	[LessReason] [varchar](255) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyEvaluation', @level2type=N'COLUMN',@level2name=N'Category'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyEvaluation', @level2type=N'COLUMN',@level2name=N'EvaluateYear'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'月份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyEvaluation', @level2type=N'COLUMN',@level2name=N'EvaluateMonth'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等级 1 优秀,2 合格,3 失格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyEvaluation', @level2type=N'COLUMN',@level2name=N'Grade'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyEvaluation', @level2type=N'COLUMN',@level2name=N'Notes'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'减分原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyEvaluation', @level2type=N'COLUMN',@level2name=N'LessReason'
GO


