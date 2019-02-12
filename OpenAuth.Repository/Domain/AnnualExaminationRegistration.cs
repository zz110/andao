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
    public partial class AnnualExaminationRegistration : Entity
    {
        public AnnualExaminationRegistration()
        {
          this.Name= string.Empty;
          this.UserId= string.Empty;
          this.OrgId= string.Empty;
          this.Sex= string.Empty;
          this.Birthday= DateTime.Now;
          this.Position= string.Empty;
          this.Officetime= DateTime.Now;
          this.RegistrationTime= DateTime.Now;
          this.Nation= string.Empty;
          this.Politicalaffiliation= string.Empty;
          this.DegreeEdu= string.Empty;
          this.Conclusion= string.Empty;
          this.Reward= string.Empty;
          this.RewardTime= DateTime.Now;
          this.RewardReasons= string.Empty;
          this.PenaltyTime= DateTime.Now;
          this.Penalty= string.Empty;
          this.PenaltyReasons= string.Empty;
          this.Scope= string.Empty;
          this.OfficialName= string.Empty;
          this.OfficialTime= DateTime.Now;
          this.HRAdvice= string.Empty;
          this.HRTime= DateTime.Now;
          this.UnitAdvice= string.Empty;
          this.UnitTime= DateTime.Now;
          this.Notes= string.Empty;
          this.Creator= string.Empty;
          this.Created= DateTime.Now;
          this.Updated= DateTime.Now;
        }

        /// <summary>
	    /// 姓名
	    /// </summary>
        public string Name { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string UserId { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public string OrgId { get; set; }
        /// <summary>
	    /// 性别
	    /// </summary>
        public string Sex { get; set; }
        /// <summary>
	    /// 生日
	    /// </summary>
        public System.DateTime? Birthday { get; set; }
        /// <summary>
	    /// 职务
	    /// </summary>
        public string Position { get; set; }
        /// <summary>
	    /// 任职时间
	    /// </summary>
        public System.DateTime? Officetime { get; set; }
        /// <summary>
	    /// 登记时间
	    /// </summary>
        public System.DateTime? RegistrationTime { get; set; }
        /// <summary>
	    /// 民族
	    /// </summary>
        public string Nation { get; set; }
        /// <summary>
	    /// 政治面貌
	    /// </summary>
        public string Politicalaffiliation { get; set; }
        /// <summary>
	    /// 文化程度
	    /// </summary>
        public string DegreeEdu { get; set; }
        /// <summary>
	    /// 个人总结
	    /// </summary>
        public string Conclusion { get; set; }
        /// <summary>
	    /// 是否奖励 0 否 1 是
	    /// </summary>
        public int? IsReward { get; set; }
        /// <summary>
	    /// 奖励
	    /// </summary>
        public string Reward { get; set; }
        /// <summary>
	    /// 奖励时间
	    /// </summary>
        public System.DateTime? RewardTime { get; set; }
        /// <summary>
	    /// 奖励原因
	    /// </summary>
        public string RewardReasons { get; set; }
        /// <summary>
	    /// 是否惩罚 0 否 1 是
	    /// </summary>
        public int? IsPenalty { get; set; }
        /// <summary>
	    /// 惩罚时间
	    /// </summary>
        public System.DateTime? PenaltyTime { get; set; }
        /// <summary>
	    /// 惩罚
	    /// </summary>
        public string Penalty { get; set; }
        /// <summary>
	    /// 惩罚原因
	    /// </summary>
        public string PenaltyReasons { get; set; }
        /// <summary>
	    /// 被测评人员范围
	    /// </summary>
        public string Scope { get; set; }
        /// <summary>
	    /// 被测评人数
	    /// </summary>
        public int? EvaluationCount { get; set; }
        /// <summary>
	    /// 要素测评得分
	    /// </summary>
        public decimal? FactorScore { get; set; }
        /// <summary>
	    /// 优秀称职率
	    /// </summary>
        public decimal? Rate { get; set; }
        /// <summary>
	    /// 排名顺位
	    /// </summary>
        public int? Rank { get; set; }
        /// <summary>
	    /// 1 优秀,2 职称,3 基本称职,4 不称职
	    /// </summary>
        public int? Officialadvice { get; set; }
        /// <summary>
	    /// 正职名称
	    /// </summary>
        public string OfficialName { get; set; }
        /// <summary>
	    /// 
	    /// </summary>
        public System.DateTime? OfficialTime { get; set; }
        /// <summary>
	    /// 人事部意见
	    /// </summary>
        public string HRAdvice { get; set; }
        /// <summary>
	    /// 人事部时间
	    /// </summary>
        public System.DateTime? HRTime { get; set; }
        /// <summary>
	    /// 单位意见
	    /// </summary>
        public string UnitAdvice { get; set; }
        /// <summary>
	    /// 单位时间
	    /// </summary>
        public System.DateTime? UnitTime { get; set; }
        /// <summary>
	    /// 备注
	    /// </summary>
        public string Notes { get; set; }
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