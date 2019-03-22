using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;

using System.Data.SqlClient;
using System.Data;

namespace OpenAuth.App
{
    public class MonthlyAssessmentApp : BaseApp<MonthlyAssessment>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }



        public object page(int limit, int offset, MonthlyAssessmentQueryInput input)
        {
            User user = AuthUtil.GetCurrentUser().User;
            List<Relevance> rli = ReleManagerApp.Repository.Find(i => i.FirstId == user.Id && i.Key == "UserOrg").ToList<Relevance>();
            string orgids = "";
            for (int i = 0; i < rli.Count; i++)
            {
                orgids = orgids + "'" +rli[i].SecondId + "',";
            }
            if (orgids != "")
                orgids = orgids.Substring(0, orgids.Length - 1);
            else
                orgids = "''";
            offset += 1;
            //string sql = $@"select top {limit} * from(
            //                  select row_number() over(order by a.created) as num,a.*,b.Name as OrgName,c.Name as UserName
            //                                           from MonthlyAssessment a left join Org b
            //                                           on a.OrgId=b.Id
            //                                           left join [User] c
            //                                           on a.UserId=c.Id 
            //                                           where a.Creator=@Creator 
            //                                           and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
            //                                           and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
            //                                           and (c.Name like '%'+@UserName+'%' or @UserName is null)
            //                                           and (b.Name like '%'+@OrgName+'%' or @OrgName is null)

            //                ) as t where num > ({limit}*({offset}-1))";

            //var rows = Repository.ExecuteQuerySql<MonthlyAssessmentOutput>(sql, input.ToParameters()).ToList();

            //sql = @"select count(*) from MonthlyAssessment a left join [User] c  on a.UserId=c.Id 
            //        left join Org b
            //        on a.OrgId=b.Id
            //        where a.Creator=@Creator 
            //        and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
            //        and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
            //        and (c.Name like '%'+@UserName+'%'  or @UserName is null)
            //        and (b.Name like '%'+@OrgName+'%' or @OrgName is null)";

            //int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            //return new
            //{
            //    total = total,
            //    rows = rows
            //};



            string sql = $@"select top {limit} * from(


select row_number() over(order by c.Name) as num,

 a.[Id]
      ,c.id [UserId]
      ,b.id [OrgId]
      ,a.[EvaluateYear]
      ,a.[EvaluateMonth]
      ,a.[AnntubeScore]
      ,a.[QuantifyScore]
      ,a.[Score]
      ,a.[Creator]
      ,a.[Created]
      ,a.[Updated]
      ,a.[Reason1]
      ,a.[Reason2]
,b.Name as OrgName,c.Name as UserName from( select *  
                                                       from MonthlyAssessment a where  (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null) ) a 
                                                       right join [User] c
                                                       on a.UserId=c.Id inner join dbo.Relevance as r on r.firstid=c.id 
                                                       and r.[key]='UserOrg' inner join Org b
                                                       on b.Id=r.SecondId 
                                                       inner join dbo.Relevance as r1 on r1.firstid=c.id 
                                                       and r1.[key]='UserRole' inner join Role ro
                                                       on ro.Id=r1.SecondId and 
                                                        (ro.Name=@role or @role is null)
                                                       where  (b.id in ({orgids}) or {orgids}='') and 
                                                        (c.Name like '%'+@UserName+'%' or @UserName is null)
                                                       and (b.Name like '%'+@OrgName+'%' or @OrgName is null)
                                                        and (b.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType=''))  
                                                       
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<MonthlyAssessmentOutput>(sql, input.ToParameters()).ToList();

            sql = $@"select count(*) from( select *  
                                                       from MonthlyAssessment a where a.Creator=@Creator 
                                                       and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null) ) a 
                                                       right join [User] c
                                                       on a.UserId=c.Id inner join dbo.Relevance as r on r.firstid=c.id 
                                                       and r.[key]='UserOrg' inner join Org b
                                                       on b.Id=r.SecondId  inner join dbo.Relevance as r1 on r1.firstid=c.id 
                                                       and r1.[key]='UserRole' inner join Role ro
                                                       on ro.Id=r1.SecondId and 
                                                        (ro.Name=@role or @role is null) 
                                                       where (b.id in ({orgids}) or {orgids} = '') and 
                                                        (c.Name like '%'+@UserName+'%' or @UserName is null)
                                                       and (b.Name like '%'+@OrgName+'%' or @OrgName is null)  and (b.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType=''))";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new
            {
                total = total,
                rows = rows
            };

        }

        public object GetMonthlyPostAssessment(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {

            string sql = @"select d.Name as UserName,c.Name as OrgName,a.EvaluateYear,a.EvaluateMonth,isnull(a.Score,0.00) as Score,isnull(b.Score,0.00) as DepartmentMonthlyScore,a.Reason1,a.Reason2
                            FROM MonthlyAssessment a left join DepartmentMonthlyEvaluation b
                            on a.OrgId=b.OrgId and a.EvaluateYear=b.EvaluateYear and a.EvaluateMonth=b.EvaluateMonth
                            left join Org c
                            on a.OrgId=c.Id
                            left join [User] as d
                            on a.UserId=d.Id
                            where  (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                            and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null) 
                            and (d.Name like '%'+@UserName+'%'  or @UserName is null)
                            and (c.Name like '%'+@OrgName+'%' or @OrgName is null)
                            and (a.UserId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null)
                            and (c.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType=''))";

            var rows = Repository.ExecuteQuerySql<MonthlyPostAssessmentOutput>(sql, input.ToParameters()).ToList();
            return new
            {
                total = 10000,
                rows = rows
            };
        }

        public object GetTableColumns4MonthlyStatisticsAssessment2(string deptType)
        {
            try
            {
                var haha = new SqlParameter("@BizCode", DateTime.Now.ToString());

                var sql = "select 'YueFen' as field,'月份' as title,-1 as SortNo union all ";
                sql += "select '['+[Name]+']' as field,[Name] as title,[SortNo] from [Org] ";
                if (!string.IsNullOrEmpty(deptType))
                    sql += "where [BizCode]='"+ deptType + "' ";
                sql += "order by [SortNo] ";

                var rows = Repository.ExecuteQuerySql<TableColumns>(sql, haha).ToList();
                return new { total = 10000, rows = rows };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object GetMonthlyStatisticsAssessment2(int queryYear, string role, string DeptType)
        {
            try
            {
                var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["OpenAuthDBContext"].ToString();

                using (SqlConnection sqlConn = new SqlConnection(connStr))
                {
                    sqlConn.Open();

                    SqlCommand sqlComm = new SqlCommand("PROC_TMP1", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add(new SqlParameter("@QueryYear", SqlDbType.VarChar));
                    sqlComm.Parameters["@QueryYear"].Value = queryYear.ToString();

                    sqlComm.Parameters.Add(new SqlParameter("@RoleType", SqlDbType.NVarChar));
                    sqlComm.Parameters["@RoleType"].Value = role;

                    sqlComm.Parameters.Add(new SqlParameter("@OrgType", SqlDbType.NVarChar));
                    sqlComm.Parameters["@OrgType"].Value = DeptType;

                    var dt = new DataTable();
                    SqlDataReader sqlReader = sqlComm.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(sqlReader);
                    sqlReader.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object GetMonthlyStatisticsAssessment(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {

            string sql = @"  if object_id('tempdb..##ttemp') is not null  
	drop table ##ttemp 
create table ##ttemp(gid nvarchar(50),username nvarchar(50),
    evaluateyear int,EvaluateMonth int,core numeric(18, 4),Reason1 nvarchar(550),Reason2 nvarchar(550),ge nvarchar(50))   
      
insert into ##ttemp     

select NEWID(),*,case when core>=95 then '1优秀' when core > 75 and core <85 then '合格' when core < 75 then '失格' end ge from (
select username,evaluateyear,EvaluateMonth,(Score+DepartmentMonthlyScore)*0.5 core,Reason1,Reason2 from (

select d.Name as UserName,c.Name as OrgName,a.EvaluateYear,a.EvaluateMonth,isnull(a.Score,0.00) as Score,isnull(b.Score,0.00) as DepartmentMonthlyScore,mo.LessReason Reason1,Reason2 
                            FROM MonthlyAssessment a left join DepartmentMonthlyEvaluation b
                            on a.OrgId=b.OrgId and a.EvaluateYear=b.EvaluateYear and a.EvaluateMonth=b.EvaluateMonth
                            left join Org c
                            on a.OrgId=c.Id
                            left join [User] as d
                            on a.UserId=d.Id  left join  [dbo].[MonthlyEvaluation] as mo on mo.UserId= a.UserId and mo.EvaluateMonth=a.EvaluateMonth  
where (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null) and 
                             (c.Name like '%'+@OrgName+'%' or @OrgName is null) and 
                             (d.Name like '%'+@UserName+'%' or @UserName is null)
                            and (a.UserId in(
                                select distinct a.FirstId from Relevance a  join [Role] b
                                on a.SecondId=b.Id
                                where a.[Key]='UserRole' and b.Name=@role

                            ) or @role is null)
                            and (c.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType=''))  
                             ) as a  ) as a
                            
     select SUM(统计) 统计,ge 结果,MAX(s.[1]) as 一,MAX(s.[2]) as 二,MAX(s.[3]) as 三,MAX(s.[4]) as 四,MAX(s.[5]) as 五
     ,MAX(s.[6]) as 六,MAX(s.[7]) as 七,MAX(s.[8]) as 八,MAX(s.[9]) as 九
     ,MAX(s.[10]) as 十,MAX(s.[11]) as 十一,MAX(s.[12]) as 十二 from (                      
   select * from (   
      select * from (  
     select COUNT(*) 统计,
  EvaluateMonth,ge,
     Names=stuff((select ','+username from ##ttemp t  
     where t.EvaluateMonth=##ttemp.EvaluateMonth  and t.ge=##ttemp.ge   
     for xml path('')), 1, 1, ''),Names1=stuff((select '/'+username from ##ttemp t  
     where t.EvaluateMonth=##ttemp.EvaluateMonth  and t.ge=##ttemp.ge   
     for xml path('')), 1, 1, '')   
from  
##ttemp  where ge is not null
group by 
EvaluateMonth,ge  
 )  as p  
                           
PIVOT 
(
   max(Names) FOR 
    p.EvaluateMonth IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
) AS T ) t ) as s group by ge union all select COUNT(*)-1 统计,'减分原因',MAX(s.[1]) as 一,MAX(s.[2]) as 二,MAX(s.[3]) as 三,MAX(s.[4]) as 四,MAX(s.[5]) as 五
     ,MAX(s.[6]) as 六,MAX(s.[7]) as 七,MAX(s.[8]) as 八,MAX(s.[9]) as 九
     ,MAX(s.[10]) as 十,MAX(s.[11]) as 十一,MAX(s.[12]) as 十二 from (                      
   select * from (   
      select * from (  
     select COUNT(*) 统计,
  EvaluateMonth,ge,
     Names=stuff((select ','+username+':'+Reason1 from ##ttemp t  
     where t.EvaluateMonth=##ttemp.EvaluateMonth  and t.ge=##ttemp.ge   
     for xml path('')), 1, 1, ''),Names1=stuff((select '/'+username from ##ttemp t  
     where t.EvaluateMonth=##ttemp.EvaluateMonth  and t.ge=##ttemp.ge   
     for xml path('')), 1, 1, '')   
from  
##ttemp  where ge='失格' 
group by 
EvaluateMonth,ge  
 )  as p  
                           
PIVOT 
(
   max(Names) FOR 
    p.EvaluateMonth IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
) AS T ) t ) as s group by ge  


";

            var rows = Repository.ExecuteQuerySql<MonthStatistics>(sql, input.ToParameters()).ToList();
            return new
            {
                total = 10000,
                rows = rows
            };
        }


        public MonthlyAssessmentOutput get(string id)
        {

            var model = this.Get(id);
            if (model == null)
            {
                return new MonthlyAssessmentOutput();
            }
            else
            {
                MonthlyAssessmentOutput result = model.MapTo<MonthlyAssessmentOutput>();
                if (!string.IsNullOrEmpty(result.UserId))
                {
                    result.UserName = UnitWork.Find<User>(w => w.Id.Equals(result.UserId))?.FirstOrDefault()?.Name;
                }

                if (!string.IsNullOrEmpty(result.OrgId))
                {
                    result.OrgName = UnitWork.Find<Org>(w => w.Id.Equals(result.OrgId))?.FirstOrDefault()?.Name;
                }
                return result;
            }
        }

        public bool Exists(MonthlyAssessment obj)
        {

            return Repository.Find(w =>
                          w.UserId.Equals(obj.UserId) &&
                          w.OrgId.Equals(obj.OrgId) &&
                          w.EvaluateYear.Value.Equals(obj.EvaluateYear.Value) &&
                          w.EvaluateMonth.Value.Equals(obj.EvaluateMonth.Value)).Count() > 0;

        }


        public string Add(MonthlyAssessment obj)
        {
            return Repository.AddAndReturnId(obj);
        }

        public void Update(MonthlyAssessment obj)
        {
            UnitWork.Update<MonthlyAssessment>(u => u.Id == obj.Id, u => new MonthlyAssessment
            {
                //todo:要修改的字段赋值
                AnntubeScore = obj.AnntubeScore,
                QuantifyScore = obj.QuantifyScore,
                Score = obj.Score,
                EvaluateMonth = obj.EvaluateMonth,
                EvaluateYear = obj.EvaluateYear,
                OrgId = obj.OrgId,
                UserId = obj.UserId,
                Updated = DateTime.Now,
                Reason1 = obj.Reason1,
                Reason2 = obj.Reason2
            });

        }

    }
}