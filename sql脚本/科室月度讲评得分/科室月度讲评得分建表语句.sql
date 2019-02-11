USE [cp]
GO

/****** Object:  Table [dbo].[DepartmentMonthlyEvaluation]    Script Date: 2019/2/11 15:11:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DepartmentMonthlyEvaluation](
	[Id] [varchar](50) NOT NULL,
	[UserId] [varchar](50) NULL,
	[OrgId] [varchar](50) NULL,
	[EvaluateYear] [int] NULL,
	[EvaluateMonth] [int] NULL,
	[Score] [decimal](18, 2) NULL,
	[Creator] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK_DepartmentMonthlyEvaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


