using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;

namespace OpenAuth.App
{
    public class MonthlyEvaluationApp : BaseApp<MonthlyEvaluation>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }


        public object page(int limit, int offset, MonthlyEvaluationQueryInput input) {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.created) as num,a.*,b.Name as OrgName,c.Name as UserName 
                                                       from MonthlyEvaluation a left join Org b
                                                       on a.OrgId=b.Id
                                                       left join [User] c
                                                       on a.UserId=c.Id 
                                                       where a.Creator=@Creator 
                                                       and (a.Category=@Category or @Category is null)
                                                       and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
                                                       and (c.Name like '%'+@UserName+'%' or @UserName is null)
                                                       and (b.Name like '%'+@OrgName+'%' or @OrgName is null)
                                                       
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<MonthlyEvaluationOutput>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from MonthlyEvaluation a left join [User] c  on a.UserId=c.Id 
                    left join Org b
                    on a.OrgId=b.Id
                    where a.Creator=@Creator 
                    and (a.Category=@Category or @Category is null)
                    and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                    and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
                    and (c.Name like '%'+@UserName+'%'  or @UserName is null)
                    and (b.Name like '%'+@OrgName+'%' or @OrgName is null)";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new
            {
                total = total,
                rows = rows
            };
        }


        public MonthlyEvaluationOutput get(string id)
        {

            var model = this.Get(id);
            if (model == null)
            {
                return new MonthlyEvaluationOutput();
            }
            else
            {
                MonthlyEvaluationOutput result = model.MapTo<MonthlyEvaluationOutput>();
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

        public bool Exists(MonthlyEvaluation obj) {

            return Repository.Find(w =>
                          w.Category.Equals(obj.Category) &&
                          w.UserId.Equals(obj.UserId) &&
                          w.OrgId.Equals(obj.OrgId) &&
                          w.EvaluateYear.Value.Equals(obj.EvaluateYear.Value) &&
                          w.EvaluateMonth.Value.Equals(obj.EvaluateMonth.Value)).Count() > 0;

        }

        public string Add(MonthlyEvaluation obj)
        {
            if (obj.Score.HasValue)
            {
                if (obj.Score.Value >= 95M) obj.Grade = 1;
                if (obj.Score.Value >= 75M && obj.Score.Value < 95) obj.Grade = 2;
                if (obj.Score.Value < 75) obj.Grade = 3;
            }
            return Repository.AddAndReturnId(obj);
        }
        
        public void Update(MonthlyEvaluation obj)
        {
            if (obj.Score.HasValue)
            {
                if (obj.Score.Value >= 95M) obj.Grade = 1;
                if (obj.Score.Value >= 75M && obj.Score.Value < 95) obj.Grade = 2;
                if (obj.Score.Value < 75) obj.Grade = 3;
            }

            UnitWork.Update<MonthlyEvaluation>(u => u.Id == obj.Id, u => new MonthlyEvaluation
            {
                //todo:要修改的字段赋值
                Category = obj.Category,
                EvaluateMonth = obj.EvaluateMonth,
                EvaluateYear = obj.EvaluateYear,
                LessReason = obj.LessReason,
                Notes = obj.Notes,
                UserId = obj.UserId,
                OrgId = obj.OrgId,
                Score = obj.Score,
                Updated = DateTime.Now,
                Grade = obj.Grade,

            });

        }

    }
}