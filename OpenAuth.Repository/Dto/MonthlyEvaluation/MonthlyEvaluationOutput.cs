using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class MonthlyEvaluationOutput
    {

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
	    /// 类别
	    /// </summary>
        public string Category { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string UserId { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string OrgId { get; set; }
        /// <summary>
	    /// 年份
	    /// </summary>
        public int? EvaluateYear { get; set; }
        /// <summary>
	    /// 月份
	    /// </summary>
        public int? EvaluateMonth { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string Creator { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public System.DateTime? Created { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public System.DateTime? Updated { get; set; }
        /// <summary>
	    /// 等级 1 优秀,2 合格,3 失格
	    /// </summary>
        public int? Grade { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
        public string Notes { get; set; }
        /// <summary>
	    /// 减分原因
	    /// </summary>
        public string LessReason { get; set; }

        public string UserName { get; set; }

        public string OrgName { get; set; }

        public string CreateTime
        {
            get
            {
                if (this.Created.HasValue) {
                    return this.Created.Value.ToString("yyyy-MM-dd HH:mm:ss"); 
                }
                return "";
            }
        }
    }
}
