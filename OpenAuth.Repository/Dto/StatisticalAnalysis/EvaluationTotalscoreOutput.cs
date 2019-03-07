using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public  class EvaluationTotalscoreOutput
    {
        public long Num { get; set; }

        public string Name { get; set; }

        public string OrgName { get; set; }

        public string XRank { get; set; }

        public decimal 要素 { get; set; }
        public decimal 综合 { get; set; }
        public decimal 总分 { get; set; }

        public string Id { get; set; }

    }
}
