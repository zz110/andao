using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class EvaluateAverageScoreQueryInput
    {
        /// <summary>
        /// 评价年份
        /// </summary>
        public int? EvaluateYear { get; set; }
        /// <summary>
	    /// 评价月份
	    /// </summary>
        public int? EvaluateMonth { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
        

    }
}
