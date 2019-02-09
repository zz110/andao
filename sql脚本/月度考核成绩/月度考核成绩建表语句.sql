USE [cp]
GO

/****** Object:  Table [dbo].[MonthlyAssessment]    Script Date: 2019/2/9 20:08:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MonthlyAssessment](
	[Id] [varchar](50) NOT NULL,
	[UserId] [varchar](50) NULL,
	[OrgId] [varchar](50) NULL,
	[EvaluateYear] [int] NULL,
	[EvaluateMonth] [int] NULL,
	[AnntubeScore] [decimal](18, 2) NULL,
	[QuantifyScore] [decimal](18, 2) NULL,
	[Score] [decimal](18, 2) NULL,
	[Creator] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK_MonthlyAssessment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安管成绩' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyAssessment', @level2type=N'COLUMN',@level2name=N'AnntubeScore'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标准量化成绩' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MonthlyAssessment', @level2type=N'COLUMN',@level2name=N'QuantifyScore'
GO


