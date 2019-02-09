using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class MonthlyEvaluationQueryInput
    {
        public int? EvaluateYear { get; set; }

        public int? EvaluateMonth { get; set; }

        public string Category { get; set; }

        public string Creator { get; set; }

        public string UserName { get; set; }

        public string OrgName { get; set; }
    }
}
