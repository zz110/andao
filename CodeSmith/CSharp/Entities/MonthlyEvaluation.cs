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

namespace Cp.Data.Entities
{
    /// <summary>
	/// 
	/// </summary>
    public partial class MonthlyEvaluation : Entity
    {
        public MonthlyEvaluation()
        {
          this.Category= string.Empty;
          this.UserId= string.Empty;
          this.OrgId= string.Empty;
          this.Creator= string.Empty;
          this.Created= DateTime.Now;
          this.Updated= DateTime.Now;
          this.Notes= string.Empty;
          this.LessReason= string.Empty;
        }

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

    }
}