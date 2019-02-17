using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public  class EvaluationscoreOutput
    {
        public long Num { get; set; }

        public string Name { get; set; }

        public string OrgName { get; set; }

        public string XRank { get; set; }

        public int 德_好 { get; set; }
        public int 德_中 { get; set; }
        public int 德_差 { get; set; }

        public int 能_好 { get; set; }
        public int 能_中 { get; set; }
        public int 能_差 { get; set; }

        public int 勤_好 { get; set; }
        public int 勤_中 { get; set; }
        public int 勤_差 { get; set; }

        public int 绩_好 { get; set; }
        public int 绩_中 { get; set; }
        public int 绩_差 { get; set; }

        public int 廉_好 { get; set; }
        public int 廉_中 { get; set; }
        public int 廉_差 { get; set; }

        public decimal 要素得分 { get; set; }

    }
}
