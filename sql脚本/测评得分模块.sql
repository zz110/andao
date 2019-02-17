
--插入模块
INSERT INTO [dbo].[Module]([Id], [CascadeId], [Name], [Url], [HotKey], [IsLeaf], [IsAutoExpand], [IconName], [Status], [ParentName], [Vector], [SortNo], [ParentId], [Code]) 
VALUES ('960c8597-c5ec-473a-9fa1-359d52782334', '.0.3.5.', '综合得分', '/StatisticalAnalysis/integrationscore', '', '0', '0', 'E', 0, '测评管理', '', 5, 'f6aaf98f-0593-487d-8e2a-b52ab3befae6', 'integrationscore');

INSERT INTO [dbo].[Module]([Id], [CascadeId], [Name], [Url], [HotKey], [IsLeaf], [IsAutoExpand], [IconName], [Status], [ParentName], [Vector], [SortNo], [ParentId], [Code]) 
VALUES ('c5bbdfe7-f2cd-413f-ab2f-9a6e59125924', '.0.3.6.', '总分', '/StatisticalAnalysis/totalscore', '', '0', '0', 'F', 0, '测评管理', '', 6, 'f6aaf98f-0593-487d-8e2a-b52ab3befae6', 'totalscore');

INSERT INTO [dbo].[Module]([Id], [CascadeId], [Name], [Url], [HotKey], [IsLeaf], [IsAutoExpand], [IconName], [Status], [ParentName], [Vector], [SortNo], [ParentId], [Code]) 
VALUES ('fda1e68c-c5e9-4abc-8722-4436a74e9cba', '.0.3.4.', '测评得分', '/StatisticalAnalysis/evaluationscore', '', '0', '0', 'D', 0, '测评管理', '', 4, 'f6aaf98f-0593-487d-8e2a-b52ab3befae6', 'evaluationscore');

--插入模块按钮
INSERT INTO []([Id], [DomId], [Name], [Attr], [Script], [Icon], [Class], [Remark], [Sort], [ModuleId], [TypeName], [TypeId]) 
VALUES ('1fc55cfc-f44a-4188-b5c3-e7b3c232c93c', 'btnExport', '导出', '', '', '', 'btn-sm btn-success', '', 1, 'fda1e68c-c5e9-4abc-8722-4436a74e9cba', N'', '');

INSERT INTO []([Id], [DomId], [Name], [Attr], [Script], [Icon], [Class], [Remark], [Sort], [ModuleId], [TypeName], [TypeId]) 
VALUES ('bc806ec5-8b69-480f-9548-d5fa110075f2', 'btnExport', '导出', '', '', '', 'btn-sm btn-success', '', 1, 'c5bbdfe7-f2cd-413f-ab2f-9a6e59125924', N'', '');

INSERT INTO []([Id], [DomId], [Name], [Attr], [Script], [Icon], [Class], [Remark], [Sort], [ModuleId], [TypeName], [TypeId]) 
VALUES ('d2810806-fffc-455e-9ef0-8a9029a3ae3f', 'btnExport', '导出', '', '', '', 'btn-sm btn-success', '', 1, '960c8597-c5ec-473a-9fa1-359d52782334', N'', '');
