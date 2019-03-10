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
                              select row_number() over(order by a.created) as num,a.*,b.Name as OrgName
                                                       from DepartmentMonthlyEvaluation a left join Org b
                                                       on a.OrgId=b.Id
                                                       where a.Creator=@Creator 
                                                       and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null) 
                                                       and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null) 
                                                       and (b.Name like '%{input.OrgName}%' or @OrgName is null)
                                                       and (b.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType=''))

                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<DepartmentMonthlyEvaluationOutput>(sql, input.ToParameters()).ToList();

            sql = $@"select count(*) from DepartmentMonthlyEvaluation a left join Org b on a.OrgId=b.Id
                    where a.Creator=@Creator 
                    and (a.EvaluateYear=@EvaluateYear or @EvaluateYear is null)
                    and (a.EvaluateMonth=@EvaluateMonth or @EvaluateMonth is null) and (b.Name like '%{input.OrgName}%' or @OrgName is null)
                    and (b.BizCode=@DeptType or (@DeptType='' or @DeptType is null or @DeptType=''))";

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
  
                if (!string.IsNullOrEmpty(result.OrgId))
                {
                    result.OrgName = UnitWork.Find<Org>(w => w.Id.Equals(result.OrgId))?.FirstOrDefault()?.Name;
                }
                return result;
            }
        }


        public bool Exists(DepartmentMonthlyEvaluation obj)
        {
            return Repository.Find(w =>
                                w.OrgId.Equals(obj.OrgId) &&
                                w.EvaluateYear.Value.Equals(obj.EvaluateYear.Value) &&
                                w.EvaluateMonth.Value.Equals(obj.EvaluateMonth.Value)).Count() > 0;
        }


        public string Add(DepartmentMonthlyEvaluation obj)
        {
            return Repository.AddAndReturnId(obj);
        }
        
        public void Update(DepartmentMonthlyEvaluation obj)
        {
            UnitWork.Update<DepartmentMonthlyEvaluation>(u => u.Id == obj.Id, u => new DepartmentMonthlyEvaluation
            {
                //todo:要修改的字段赋值
                EvaluateMonth = obj.EvaluateMonth,
                EvaluateYear = obj.EvaluateYear,
                OrgId = obj.OrgId,
                Score = obj.Score,
                Updated = DateTime.Now
            });

        }

    }
}