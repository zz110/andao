USE [cp]
GO

/****** Object:  Table [dbo].[AnnualExaminationRegistration]    Script Date: 2019/2/11 23:05:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AnnualExaminationRegistration](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[UserId] [varchar](50) NULL,
	[OrgId] [varchar](50) NULL,
	[Sex] [varchar](50) NULL,
	[Birthday] [datetime] NULL,
	[Position] [varchar](50) NULL,
	[Officetime] [datetime] NULL,
	[RegistrationTime] [datetime] NULL,
	[Nation] [varchar](50) NULL,
	[Politicalaffiliation] [varchar](50) NULL,
	[DegreeEdu] [varchar](50) NULL,
	[Conclusion] [varchar](max) NULL,
	[IsReward] [int] NULL,
	[Reward] [varchar](500) NULL,
	[RewardReasons] [varchar](500) NULL,
	[IsPenalty] [int] NULL,
	[Penalty] [varchar](500) NULL,
	[PenaltyReasons] [varchar](500) NULL,
	[Scope] [varchar](50) NULL,
	[EvaluationCount] [int] NULL,
	[FactorScore] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Rank] [int] NULL,
	[Officialadvice] [int] NULL,
	[OfficialName] [varchar](50) NULL,
	[OfficialTime] [datetime] NULL,
	[HRAdvice] [varchar](500) NULL,
	[HRTime] [datetime] NULL,
	[UnitAdvice] [varchar](500) NULL,
	[UnitTime] [datetime] NULL,
	[Notes] [varchar](500) NULL,
	[Creator] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK_AnnualExaminationRegistration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Sex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Birthday'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职务' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Position'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任职时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Officetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登记时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'RegistrationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'民族' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Nation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政治面貌' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Politicalaffiliation'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文化程度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'DegreeEdu'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否奖励 0 否 1 是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'IsReward'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'奖励' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Reward'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'奖励原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'RewardReasons'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否惩罚 0 否 1 是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'IsPenalty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'惩罚' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Penalty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'惩罚原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'PenaltyReasons'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被测评人员范围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Scope'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被测评人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'EvaluationCount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要素测评得分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'FactorScore'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优秀称职率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Rate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排名顺位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Rank'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 优秀,2 职称,3 基本称职,4 不称职' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Officialadvice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'正职名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'OfficialName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'人事部意见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'HRAdvice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'人事部时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'HRTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位意见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'UnitAdvice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'UnitTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AnnualExaminationRegistration', @level2type=N'COLUMN',@level2name=N'Notes'
GO


