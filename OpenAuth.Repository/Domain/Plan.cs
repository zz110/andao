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
    public partial class Plan : Entity
    {
        public Plan()
        {
          this.PlanName= string.Empty;
          this.PlanStart= DateTime.Now;
          this.PlanEnd= DateTime.Now;
          this.RatersId= string.Empty;
          this.JudgeId= string.Empty;
          this.OpTime= string.Empty;
          this.OpAdmin= string.Empty;
          this.QuestionId= string.Empty;
        }

        /// <summary>
	    /// 方案名称
	    /// </summary>
        public string PlanName { get; set; }
        /// <summary>
	    /// 方案开始时间
	    /// </summary>
        public System.DateTime? PlanStart { get; set; }
        /// <summary>
	    /// 方案结束时间
	    /// </summary>
        public System.DateTime? PlanEnd { get; set; }
        /// <summary>
	    /// 状态
	    /// </summary>
        public int? State { get; set; }
        /// <summary>
	    /// 评测人IDs
	    /// </summary>
        public string RatersId { get; set; }
        /// <summary>
	    /// 被评人Ids
	    /// </summary>
        public string JudgeId { get; set; }
        /// <summary>
	    /// 操作时间
	    /// </summary>
        public string OpTime { get; set; }
        /// <summary>
	    /// 操作人
	    /// </summary>
        public string OpAdmin { get; set; }
        /// <summary>
	    /// 试题组
	    /// </summary>
        public string QuestionId { get; set; }

    }
}