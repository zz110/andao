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
    }
}
