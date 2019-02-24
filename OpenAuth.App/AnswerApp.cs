using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;


namespace OpenAuth.App
{
    public class AnswerApp : BaseApp<Answer>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        public object page(int limit, int offset, AnswerSearch input)
        {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by a.id) as num,a.*,c.Name as RatersName,c1.Name as JudgeName
                                                       from Answer a 
                                                       left join [User] c
                                                       on a.RatersId=c.Id left join [User] c1 on a.JudgeId = c1.Id 
                                                       
                                                       
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<AnswerSearch>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from Answer ";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new TableData
            {
                count = total,
                data = rows
            };
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryAnswerListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(Answer obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(Answer obj)
        {
            UnitWork.Update<Answer>(u => u.Id == obj.Id, u => new Answer
            {
                //todo:要修改的字段赋值
                RatersId = obj.RatersId,
                JudgeId = obj.JudgeId,
                Q1 = obj.Q1,
                Q2 = obj.Q2,
                Q3 = obj.Q3,
                Q4 = obj.Q4,
                Q5 = obj.Q5,
                Q6 = obj.Q6,
                Optime = obj.Optime,
                State = obj.State,
                PlanName = obj.PlanName,
            });

        }

    }
}