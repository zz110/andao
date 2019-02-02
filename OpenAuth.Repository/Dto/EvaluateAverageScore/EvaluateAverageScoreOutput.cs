using OpenAuth.Repository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class EvaluateAverageScoreOutput 
    {

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
	    /// 用户id
	    /// </summary>
        public string UserId { get; set; }
        /// <summary>
	    /// 组织结构id
	    /// </summary>
        public string OrgId { get; set; }
        /// <summary>
	    /// 评价年份
	    /// </summary>
        public int? EvaluateYear { get; set; }
        /// <summary>
	    /// 评价月份
	    /// </summary>
        public int? EvaluateMonth { get; set; }
        /// <summary>
	    /// 得分
	    /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public System.DateTime? Created { get; set; }

        public string CreateTime
        {
            get
            {
                if (Created.HasValue) {
                    return Created.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return "";
            }
        }

        
        /// <summary>
	    /// 
	    /// </summary>
        public System.DateTime? Updated { get; set; }

        public string UserName { get; set; }

        public string OrgName { get; set; }
    }
}
