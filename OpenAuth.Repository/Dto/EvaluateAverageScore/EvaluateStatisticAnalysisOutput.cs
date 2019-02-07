using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class EvaluateStatisticAnalysisOutput
    {
        /// <summary>
        /// 排名
        /// </summary>
        public Int64 Num { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 1月
        /// </summary>
        public decimal _1 { get; set; }
        /// <summary>
        /// 2月
        /// </summary>
        public decimal _2 { get; set; }
        /// <summary>
        /// 3月
        /// </summary>
        public decimal _3 { get; set; }
        /// <summary>
        /// 4月
        /// </summary>
        public decimal _4 { get; set; }
        /// <summary>
        /// 5月
        /// </summary>
        public decimal _5 { get; set; }
        /// <summary>
        /// 6月
        /// </summary>
        public decimal _6 { get; set; }
        /// <summary>
        /// 7月
        /// </summary>
        public decimal _7 { get; set; }
        /// <summary>
        /// 8月
        /// </summary>
        public decimal _8 { get; set; }
        /// <summary>
        /// 9月
        /// </summary>
        public decimal _9 { get; set; }
        /// <summary>
        /// 10月
        /// </summary>
        public decimal _10 { get; set; }
        /// <summary>
        /// 11月
        /// </summary>
        public decimal _11 { get; set; }
        /// <summary>
        /// 12月
        /// </summary>
        public decimal _12 { get; set; }
        /// <summary>
        /// 平均分
        /// </summary>
        public decimal Average { get; set; }
    }
}
