using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class MonthlyAssessmentQueryInput
    {
        public int? EvaluateYear { get; set; }

        public int? EvaluateMonth { get; set; }

        public string UserName { get; set; }

        public string OrgName { get; set; }

        public string Creator { get; set; }

        public string role { get; set; }

        public string DeptType { get; set; }

        public string Reason1 { get; set; }

        public string Reason2 { get; set; }
    }
}
