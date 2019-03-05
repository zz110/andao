using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class PerformanceAppraisalOutPut
    {
        public string Id { get; set; }

        public decimal AccessmentScore { get; set; }

        public string RatersId { get; set; }

        public string RatersName { get; set; }

        public string JudgeId { get; set; }

        public string JudgeName { get; set; }

        public DateTime Optime { get; set; }

        public string State { get; set; }

        public string DeptType { get; set; }
        //
        public decimal q1 { get; set; }
        public decimal q2 { get; set; }
        public decimal q3 { get; set; }
        public decimal q4 { get; set; }
        public decimal q5 { get; set; }
        public decimal q6 { get; set; }
        public int q6count { get; set; }

        public decimal pingce { get; set; }
        public decimal MonthlyAVG { get; set; }
        public decimal pingcedefenavg { get; set; }
        public decimal kaopingdefen { get; set; }
        public int Index { get; set; }
    }
}
