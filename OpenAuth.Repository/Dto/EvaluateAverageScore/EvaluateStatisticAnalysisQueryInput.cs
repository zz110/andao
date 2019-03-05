using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class EvaluateStatisticAnalysisQueryInput
    {
        /// <summary>
        /// 评价年份
        /// </summary>
        public int? EvaluateYear { get; set; }
        /// <summary>
        /// 搜索
        /// </summary>
        public string search { get; set; }
        /// <summary>
        /// 职务角色
        /// </summary>
        public string role { get; set; }


        public string DeptType { get; set; }
    }
}
