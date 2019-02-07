using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;
using Infrastructure;

namespace OpenAuth.App
{
    public class EvaluateAverageScoreApp : BaseApp<EvaluateAverageScore>
    {
        
        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryEvaluateAverageScoreListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public object page(int limit, int offset, EvaluateAverageScoreQueryInput input) {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.created) as num,a.*,b.Name as OrgName,c.Name as UserName 
                                                       from EvaluateAverageScore a left join Org b
                                                       on a.OrgId=b.Id
                                                       left join [User] c
                                                       on a.UserId=c.Id 
                                                       where a.Creator=@Creator 
                                                       and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
                                                       and (c.Name like '%'+@UserName+'%' or @UserName is null)
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<EvaluateAverageScoreOutput>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from EvaluateAverageScore a left join [User] c  on a.UserId=c.Id 
                    where a.Creator=@Creator 
                    and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                    and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
                    and (c.Name like '%'+@UserName+'%'  or @UserName is null)";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new
            {
                total = total,
                rows = rows
            };
        }


        public object get_statistic_analysis_data(EvaluateStatisticAnalysisQueryInput input)
        {

            string sql = @"SELECT 
                           row_number() over(order by T1.Average desc) as Num,
                           UserName,
                           OrgName,
                           isnull([1],0.00) as [_1],
	                       isnull([2],0.00) as [_2],
	                       isnull([3],0.00) as [_3],
	                       isnull([4],0.00) as [_4],
	                       isnull([5],0.00) as [_5],
	                       isnull([6],0.00) as [_6],
	                       isnull([7],0.00) as [_7],
	                       isnull([8],0.00) as [_8],
	                       isnull([9],0.00) as [_9],
	                       isnull([10],0.00) as [_10],
	                       isnull([11],0.00) as [_11],
	                       isnull([12],0.00) as [_12],
	                       T1.Average
	                       FROM (
	                    select a.UserId,a.OrgId,c.Name as UserName,b.Name as OrgName,a.EvaluateMonth,a.Score
	                    from EvaluateAverageScore a left join Org b
	                    on a.OrgId=b.Id
	                    left join [User] c
	                    on a.UserId=c.Id 
                        where (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null) 
                        and (c.Name like '%'+@search+'%' or b.Name like '%'+@search+'%'  or @search is null)
                        and (a.UserId in(
                             select distinct a.FirstId from Relevance a  join [Role] b
                             on a.SecondId=b.Id
                             where a.[Key]='UserRole' and b.Name=@role

                        ) or @role is null)
                    ) 
                    AS P
                    PIVOT 
                    (
                        SUM(Score) FOR 
                        p.EvaluateMonth IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
                    ) AS T
                    left join (
                       select a1.UserId,a1.OrgId,convert(decimal(18,2),avg(Score)) as Average from EvaluateAverageScore as a1
                       left join  Org b1
                       on a1.OrgId=b1.Id
                       left join [User] c1
                       on a1.UserId=c1.Id 
                       where (a1.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                       and (c1.Name like '%'+@search+'%' or b1.Name like '%'+@search+'%'  or @search is null)
                       and (a1.UserId in(
                             select distinct a.FirstId from Relevance a  join [Role] b
                             on a.SecondId=b.Id
                             where a.[Key]='UserRole' and b.Name=@role

                        ) or @role is null)
                       group by a1.UserId,a1.OrgId
                    ) as T1
                    on  T1.UserId=T.UserId and T1.OrgId=T.OrgId";


            var rows = Repository.ExecuteQuerySql<EvaluateStatisticAnalysisOutput>(sql, input.ToParameters()).ToList();
            return new
            {
                rows = rows,
                total = 10000
            };
        }


        public EvaluateAverageScoreOutput get(string id) {


            var model = this.Get(id);
            if (model == null)
            {
                return new EvaluateAverageScoreOutput();
            }
            else {
                EvaluateAverageScoreOutput result= model.MapTo<EvaluateAverageScoreOutput>();
                if (!string.IsNullOrEmpty(result.UserId)) {
                    result.UserName = UnitWork.Find<User>(w => w.Id.Equals(result.UserId))?.FirstOrDefault()?.Name;
                }

                if (!string.IsNullOrEmpty(result.OrgId)) {
                    result.OrgName = UnitWork.Find<Org>(w => w.Id.Equals(result.OrgId))?.FirstOrDefault()?.Name;
                }
                return result;
            }
        }


        public bool Exists(EvaluateAverageScore obj)
        {
            return Repository.Find(w => w.UserId.Equals(obj.UserId) &&
                                w.OrgId.Equals(obj.OrgId) &&
                                w.EvaluateYear.Value.Equals(obj.EvaluateYear.Value) &&
                                w.EvaluateMonth.Value.Equals(obj.EvaluateMonth.Value)).Count() > 0;
        }

        public string Add(EvaluateAverageScore obj)
        {
            return Repository.AddAndReturnId(obj);
        }
        
        public void Update(EvaluateAverageScore obj)
        {
            UnitWork.Update<EvaluateAverageScore>(u => u.Id == obj.Id, u => new EvaluateAverageScore
            {
                //todo:要修改的字段赋值
                UserId = obj.UserId,
                OrgId = obj.OrgId,
                Updated = DateTime.Now,
                EvaluateYear = obj.EvaluateYear,
                EvaluateMonth = obj.EvaluateMonth,
                Score = obj.Score,
            });

        }

    }
}