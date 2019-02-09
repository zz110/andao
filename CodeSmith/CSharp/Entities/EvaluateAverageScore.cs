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
    public partial class EvaluateAverageScore : Entity
    {
        public EvaluateAverageScore()
        {
          this.UserId= string.Empty;
          this.OrgId= string.Empty;
          this.Creator= string.Empty;
          this.Created= DateTime.Now;
          this.Updated= DateTime.Now;
        }

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

    }
}