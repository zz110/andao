INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('20a98ac1-f008-4c62-9364-cac05c864757', '.0.8.1.', '年度考核登记表', '/AnnualExaminationRegistrations/Index', '', 0, 0, 'A', 0, '年度考核登记表', '', 1, '11508925-ccff-4036-a29f-a7ffa47ecc97', 'AnnualExaminationRegistration')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('11508925-ccff-4036-a29f-a7ffa47ecc97', '.0.8.', '年度考核登记', '-', '', 0, 0, 'P', 0, '根节点', '', 1, NULL, NULL)
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('635dc3d5-251f-450c-b51a-2819fdb0ff2f', '.0.7.1.', '科室月度讲评维护', '/DepartmentMonthlyEvaluations/Index', '', 0, 0, 'A', 0, '科室月度讲评', '', 1, '4f30dcaf-07a5-4348-a13b-d590aac02af1', 'DepartmentMonthlyEvaluation')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('4f30dcaf-07a5-4348-a13b-d590aac02af1', '.0.7.', '科室月度讲评', '-', '', 0, 0, 'P', 0, '根节点', '', 1, NULL, NULL)
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('26fe03c3-b511-4a66-921e-0dce49404908', '.0.6.2.', '岗位履责考评结果', '/MonthlyAssessments/MonthlyPostAssessment', '', 0, 0, 'B', 0, '月度考核成绩', '', 2, '25801d1e-ec5f-4aa4-ae77-d7e34e945958', 'MonthlyPostAssessment')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('a9151eaa-6022-44be-b469-83387e1cdaa1', '.0.6.1.', '月度考核成绩维护', '/MonthlyAssessments/Index', '', 0, 0, 'A', 0, '月度考核成绩', '', 1, '25801d1e-ec5f-4aa4-ae77-d7e34e945958', 'MonthlyAssessment')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('25801d1e-ec5f-4aa4-ae77-d7e34e945958', '.0.6.', '月度考核成绩', '-', '', 0, 0, 'P', 0, '根节点', '', 1, NULL, NULL)
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('9dda0f0f-270d-4205-9bfa-59600064e1eb', '.0.5.1.', '月度评价排名维护', '/MonthlyEvaluations/Index', '', 0, 0, 'A', 0, '月度评价排名', '', 1, 'df40bd6e-2d56-405f-aa95-4986f34dea80', 'MonthlyEvaluations')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('df40bd6e-2d56-405f-aa95-4986f34dea80', '.0.5.', '月度评价排名', '-', '', 0, 0, 'P', 0, '根节点', '', 1, NULL, NULL)
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('0582e0ce-8d39-41ad-9252-4eca46519f92', '.0.4.14.', '统计分析', '/EvaluateAverageScores/statistic_analysis', '', 0, 0, 'B', 0, '考核平均分', '', 2, '7e847028-3cc4-4517-b087-b39c400898c2', 'evaluate_statistic_analysis')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('31994854-09a9-47e3-ba84-81f0062a13cd', '.0.4.13.', '平均分维护', '/EvaluateAverageScores/Index', '', 0, 0, 'A', 0, '考核平均分', '', 1, '7e847028-3cc4-4517-b087-b39c400898c2', 'evaluate_average_score')
GO
INSERT INTO dbo.Module (Id, CascadeId, Name, Url, HotKey, IsLeaf, IsAutoExpand, IconName, Status, ParentName, Vector, SortNo, ParentId, Code)
VALUES ('7e847028-3cc4-4517-b087-b39c400898c2', '.0.4.', '考核平均分', '-', '', 0, 0, 'P', 0, '根节点', '', 1, NULL, NULL)
GO
