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
    }
}
