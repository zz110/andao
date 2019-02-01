USE [cp]
GO

/****** Object:  Table [dbo].[EvaluateAverageScore]    Script Date: 2019/2/1 10:16:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EvaluateAverageScore](
	[Id] [varchar](50) NULL,
	[UserId] [varchar](50) NULL,
	[OrgId] [varchar](50) NULL,
	[EvaluateYear] [int] NULL,
	[EvaluateMonth] [int] NULL,
	[Score] [decimal](18, 2) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluateAverageScore', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluateAverageScore', @level2type=N'COLUMN',@level2name=N'OrgId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评价年份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluateAverageScore', @level2type=N'COLUMN',@level2name=N'EvaluateYear'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评价月份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluateAverageScore', @level2type=N'COLUMN',@level2name=N'EvaluateMonth'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'得分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EvaluateAverageScore', @level2type=N'COLUMN',@level2name=N'Score'
GO


