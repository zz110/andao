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
    public class MonthlyAssessmentApp : BaseApp<MonthlyAssessment>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }



        public object page(int limit, int offset, MonthlyAssessmentQueryInput input)
        {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.created) as num,a.*,b.Name as OrgName,c.Name as UserName 
                                                       from MonthlyAssessment a left join Org b
                                                       on a.OrgId=b.Id
                                                       left join [User] c
                                                       on a.UserId=c.Id 
                                                       where a.Creator=@Creator 
                                                       and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
                                                       and (c.Name like '%'+@UserName+'%' or @UserName is null)
                                                       and (b.Name like '%'+@OrgName+'%' or @OrgName is null)
                                                       
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<MonthlyAssessmentOutput>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from MonthlyAssessment a left join [User] c  on a.UserId=c.Id 
                    left join Org b
                    on a.OrgId=b.Id
                    where a.Creator=@Creator 
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
            decimal score = 0.00M;

            if (!obj.AnntubeScore.HasValue) obj.AnntubeScore = 0.00M;
            if (!obj.QuantifyScore.HasValue) obj.QuantifyScore = 0.00M;

            score = obj.AnntubeScore.Value * 0.3M + obj.QuantifyScore.Value * 0.7M;

            return Repository.AddAndReturnId(obj);
        }
        
        public void Update(MonthlyAssessment obj)
        {
            decimal score = 0.00M;

            if (!obj.AnntubeScore.HasValue) obj.AnntubeScore = 0.00M;
            if (!obj.QuantifyScore.HasValue) obj.QuantifyScore = 0.00M;

            score = obj.AnntubeScore.Value * 0.3M + obj.QuantifyScore.Value * 0.7M;

            UnitWork.Update<MonthlyAssessment>(u => u.Id == obj.Id, u => new MonthlyAssessment
            {
                //todo:要修改的字段赋值
                AnntubeScore = obj.AnntubeScore,
                QuantifyScore = obj.QuantifyScore,
                Score = score,
                EvaluateMonth = obj.EvaluateMonth,
                EvaluateYear = obj.EvaluateYear,
                OrgId = obj.OrgId,
                UserId = obj.UserId,
                Updated = DateTime.Now

            });

        }

    }
}