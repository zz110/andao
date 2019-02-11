using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class MonthlyPostAssessmentOutput
    {
        public string UserName { get; set; }

        public string OrgName { get; set; }

        public int EvaluateYear { get; set; }

        public int EvaluateMonth { get; set; }

        public decimal Score { get; set; }

        public decimal DepartmentMonthlyScore { get; set; }

        public decimal AssessmentScore
        {
            get
            {
                return Score * 0.5M + DepartmentMonthlyScore * 0.5M;
            }
        }
    }
}
