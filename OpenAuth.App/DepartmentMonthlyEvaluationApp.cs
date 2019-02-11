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
    public class DepartmentMonthlyEvaluationApp : BaseApp<DepartmentMonthlyEvaluation>
    {

        public object page(int limit, int offset, DepartmentMonthlyEvaluationQueryInput input)
        {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.created) as num,a.*,b.Name as OrgName,c.Name as UserName 
                                                       from DepartmentMonthlyEvaluation a left join Org b
                                                       on a.OrgId=b.Id
                                                       left join [User] c
                                                       on a.UserId=c.Id 
                                                       where a.Creator=@Creator 
                                                       and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null)
                                                       and (c.Name like '%'+@UserName+'%' or @UserName is null)
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<DepartmentMonthlyEvaluationOutput>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from DepartmentMonthlyEvaluation a left join [User] c  on a.UserId=c.Id 
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

       
        public DepartmentMonthlyEvaluationOutput get(string id)
        {


            var model = this.Get(id);
            if (model == null)
            {
                return new DepartmentMonthlyEvaluationOutput();
            }
            else
            {
                DepartmentMonthlyEvaluationOutput result = model.MapTo<DepartmentMonthlyEvaluationOutput>();
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


        public bool Exists(DepartmentMonthlyEvaluation obj)
        {
            return Repository.Find(w => w.UserId.Equals(obj.UserId) &&
                                w.OrgId.Equals(obj.OrgId) &&
                                w.EvaluateYear.Value.Equals(obj.EvaluateYear.Value) &&
                                w.EvaluateMonth.Value.Equals(obj.EvaluateMonth.Value)).Count() > 0;
        }


        public void Add(DepartmentMonthlyEvaluation obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(DepartmentMonthlyEvaluation obj)
        {
            UnitWork.Update<DepartmentMonthlyEvaluation>(u => u.Id == obj.Id, u => new DepartmentMonthlyEvaluation
            {
                //todo:要修改的字段赋值
                EvaluateMonth = obj.EvaluateMonth,
                EvaluateYear = obj.EvaluateYear,
                OrgId = obj.OrgId,
                UserId = obj.UserId,
                Score = obj.Score,
                Updated = DateTime.Now
            });

        }

    }
}