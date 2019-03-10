using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class AnnualExaminationRegistrationQueryInput
    {
        public int? EvaluateYear { get; set; }
        
        public string Creator { get; set; }

        public string role { get; set; }

        public string DeptType { get; set; }

        public string UserName { get; set; }

        public string OrgName { get; set; }

    }
}
