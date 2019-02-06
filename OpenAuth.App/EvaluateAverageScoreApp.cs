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