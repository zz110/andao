﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAuth.Repository.Domain
{
    /// <summary>
	/// 
	/// </summary>
    public partial class MonthlyAssessment : Entity
    {
        public MonthlyAssessment()
        {
            this.UserId = string.Empty;
            this.OrgId = string.Empty;
            this.Creator = string.Empty;
            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
        }

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

        /// <summary>
        /// 减分原因
        /// </summary>
        public string Reason1 { get; set; }

        /// <summary>
        /// 不合格原因
        /// </summary>
        public string Reason2 { get; set; }
    }
}