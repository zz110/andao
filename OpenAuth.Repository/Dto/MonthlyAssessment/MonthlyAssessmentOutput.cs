using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class MonthlyAssessmentOutput
    {

        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string OrgId { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public int? EvaluateYear { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public int? EvaluateMonth { get; set; }
        /// <summary>
	    /// 安管成绩
	    /// </summary>
        public decimal? AnntubeScore { get; set; }
        /// <summary>
	    /// 标准量化成绩
	    /// </summary>
        public decimal? QuantifyScore { get; set; }
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

        public string UserName { get; set; }

        public string OrgName { get; set; }

        public string CreateTime
        {
            get
            {
                if (this.Created.HasValue)
                {
                    return this.Created.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return "";
            }
        }
    }
}
