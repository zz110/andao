using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public  class EvaluationIntegrationOutput
    {
        public long Num { get; set; }

        public string Name { get; set; }

        public string OrgName { get; set; }


        public string XRank { get; set; }

        public int 优秀 { get; set; }
        public int 称职 { get; set; }
        public int 基本称职 { get; set; }
        public int 不称职 { get; set; }
      
        public decimal 优秀称职率 { get; set; }
        public decimal 不称职率 { get; set; }
        
        public decimal 综合得分 { get; set; }

    }
}
