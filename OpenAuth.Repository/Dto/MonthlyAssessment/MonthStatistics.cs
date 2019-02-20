using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class TableColumns
    {
        public string field { get; set; }
        public string title { get; set; }
    }

    public class MonthStatistics
    {
        public int 统计 { set; get; }

        public string 结果 { set; get; }

        public string 一 { set; get; }

        public string 二 { set; get; }

        public string 三 { set; get; }
        public string 四 { set; get; }
        public string 五 { set; get; }
        public string 六 { set; get; }
        public string 七 { set; get; }
        public string 八 { set; get; }
        public string 九 { set; get; }
        public string 十 { set; get; }
        public string 十一 { set; get; }
        public string 十二 { set; get; }

    }
}
