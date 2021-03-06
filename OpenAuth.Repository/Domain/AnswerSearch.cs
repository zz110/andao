﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Domain
{
    public class AnswerSearch 
    {
        public AnswerSearch()
        {
            this.RatersId = string.Empty;
            this.JudgeId = string.Empty;
            this.Q1 = string.Empty;
            this.Q2 = string.Empty;
            this.Q3 = string.Empty;
            this.Q4 = string.Empty;
            this.Q5 = string.Empty;
            this.Q6 = string.Empty;
            this.Optime = DateTime.Now;
            this.State = string.Empty;
            this.PlanId = string.Empty;
            this.PlanName = string.Empty;
            this.RatersName = string.Empty;
            this.JudgeName = string.Empty;

        }

        /// <summary>
	    /// 评测人
	    /// </summary>
        public string RatersId { get; set; }
        /// <summary>
	    /// 被评测人
	    /// </summary>
        public string JudgeId { get; set; }
        /// <summary>
	    /// 答题1
	    /// </summary>
        public string Q1 { get; set; }
        /// <summary>
	    /// 答题2
	    /// </summary>
        public string Q2 { get; set; }
        /// <summary>
	    /// 答题3
	    /// </summary>
        public string Q3 { get; set; }
        /// <summary>
	    /// 答题4
	    /// </summary>
        public string Q4 { get; set; }
        /// <summary>
        /// 答题5
        /// </summary>
        public string Q5 { get; set; }
        /// <summary>
        /// 答题6
        /// </summary>
        public string Q6 { get; set; }
        /// <summary>
	    /// 答题完成时间
	    /// </summary>
        public System.DateTime? Optime { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string State { get; set; }
        /// <summary>
	    /// 方案ID
	    /// </summary>
        public string PlanId { get; set; }
        /// <summary>
	    /// 方案名称
	    /// </summary>
        public string PlanName { get; set; }

        public string RatersName { get; set; }

        public string JudgeName { get; set; }

    }
}
