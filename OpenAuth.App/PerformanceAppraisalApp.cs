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
    public class PerformanceAppraisalApp : BaseApp<PerformanceAppraisal>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryPerformanceAppraisalListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(PerformanceAppraisal obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(PerformanceAppraisal obj)
        {
            UnitWork.Update<PerformanceAppraisal>(u => u.Id == obj.Id, u => new PerformanceAppraisal
            {
               //todo:要修改的字段赋值
            });

        }

        public object page(int limit, int offset, PerformanceAppraisal input)
        {

            offset += 1;
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by id) as num,*
                                                       from PerformanceAppraisal 
                                                      
                                                       
                            ) as t where num > ({limit}*({offset}-1))";

            var rows = Repository.ExecuteQuerySql<PerformanceAppraisal>(sql, input.ToParameters()).ToList();

            sql = @"select count(*) from PerformanceAppraisal ";

            int total = Repository.ExecuteQuerySql<int>(sql, input.ToParameters()).FirstOrDefault();

            return new
            {
                total = total,
                rows = rows
            };
        }

        public List<PerformanceAppraisalOutPut> List(string year, string type)
        {
            string sql = $@"select top 1000 JudgeId,JudgeName,
                            (select SUM(Score)/12 from MonthlyAssessment 
                            where UserId = JudgeId 
                            and EvaluateYear='{ year }') 
                            MonthlyAVG,
                            sum(
	                            case q1 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q1,
                            sum(
	                            case q2 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q2,
                            sum(
	                            case q3 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q3,
                            sum(
	                            case q4 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q4,
                            sum(
	                            case q5 
	                            when 10 then 1 
	                            when 11 then 0.7 
	                            when 12 then 0.3 
	                            else 0 
	                            end
                            ) q5,
                            sum(
	                            case q6
	                            when 10 then 1 
	                            when 11 then 0.8 
	                            when 12 then 0.6 
	                            when 13 then 0.3 
	                            else 0 
	                            end
                            ) q6,
                            sum(
	                            case 
	                            when q6 is not null then 1 
	                            else 0 
	                            end
                            ) q6count,
                            (select AccessmentScore from PerformanceAppraisal 
                            where JudgeId = JudgeId and YEAR(Optime) = { year }) AccessmentScore 
                            from(
                              select row_number() over(order by a.Optime) as num,
                              a.RatersId,a.JudgeId,uj.Name as RatersName,ur.Name as JudgeName,
                              a.q1,a.q2,a.q3,a.q4,a.q5,a.q6 from Answer a 
                              inner join [user] ur on ur.Id = a.JudgeId 
                              inner join [user] uj on uj.Id = a.RatersId 
                              where YEAR(a.Optime) = { year } and a.State = '已提交'
                            ) as t 
                            left join Relevance r on r.FirstId = t.JudgeId 
                            inner join [Role] ro on ro.Id = r.SecondId 
                            where num > 0 and ro.Name = '{ type }' and ro.Name 
                            group by JudgeId,JudgeName";
            var rows = Repository.ExecuteQuerySql<PerformanceAppraisalOutPut>(sql).ToList();

            return rows;
        }

    }
}