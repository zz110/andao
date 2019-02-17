using Infrastructure;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.App
{
    public class PerformanceAppraisalApp : BaseApp<DepartmentMonthlyEvaluation>
    {
        public object page(int limit, int offset, PerformanceAppraisalQueryInput input)
        {
            string sql = $@"select top {limit} * from(
                              select row_number() over(order by pa.Optime) as num,* from PerformanceAppraisal pa 
                            ) as t where num > ({limit}*({offset}-1))";
            var rows = Repository.ExecuteQuerySql<PerformanceAppraisalOutPut>(sql, input.ToParameters()).ToList();
            return new
            {
                total = 10000,
                rows = rows
            };
        }
        public List<PerformanceAppraisalOutPut> List(string year, PerformanceAppraisalQueryInput input)
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
                            where JudgeId = JudgeId) AccessmentScore 
                            from(
                              select row_number() over(order by a.Optime) as num,
                              a.RatersId,a.JudgeId,uj.Name as RatersName,ur.Name as JudgeName,
                              a.q1,a.q2,a.q3,a.q4,a.q5,a.q6 from Answer a 
                              inner join [user] ur on ur.Id = a.JudgeId 
                              inner join [user] uj on uj.Id = a.RatersId 
                            ) as t 
                            left join Relevance r on r.FirstId = t.JudgeId 
                            inner join [Role] ro on ro.Id = r.SecondId 
                            where num > 0 and ro.Name = '正职' 
                            group by JudgeId,JudgeName";
            var rows = Repository.ExecuteQuerySql<PerformanceAppraisalOutPut>(sql, input.ToParameters()).ToList();

            return rows;
        }
    }
}
