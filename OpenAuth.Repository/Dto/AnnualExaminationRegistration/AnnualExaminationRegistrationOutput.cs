using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    public class AnnualExaminationRegistrationOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

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


        public string _Birthday
        {
            get
            {
                if (Birthday.HasValue)
                {
                    return Birthday.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }

        

        public string _Officetime
        {
            get
            {
                if (Officetime.HasValue)
                {
                    return Officetime.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }

        

        public string _RegistrationTime
        {
            get
            {
                if (RegistrationTime.HasValue)
                {
                    return RegistrationTime.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }


        public string RegistrationTime_
        {
            get
            {
                if (RegistrationTime.HasValue)
                {
                    return RegistrationTime.Value.ToString("yyyy年MM月dd日");
                }
                return "";
            }
        }



        public string _OfficialTime
        {
            get
            {
                if (OfficialTime.HasValue)
                {
                    return OfficialTime.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }

      

        public string _HRTime
        {
            get
            {
                if (HRTime.HasValue)
                {
                    return HRTime.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }
        

        public string _UnitTime
        {
            get
            {
                if (UnitTime.HasValue)
                {
                    return UnitTime.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }
        

        public string OrgName { get; set; }

        public string CreatedTime
        {
            get
            {
                if (Created.HasValue)
                {
                    return Created.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
        }

        public string _Officialadvice {
            get {
                
                if (Officialadvice == 1) return "同意该同志考核评定为:优秀";
                if (Officialadvice == 2) return "同意该同志考核评定为:称职";
                if (Officialadvice == 3) return "同意该同志考核评定为:基本称职";
                if (Officialadvice == 4) return "同意该同志考核评定为:不称职";
                return "";
            }
        }


        public string HRAdvice_
        {
            get
            {
                return $"依据唐山供电段《干部综合考核评价实施办法》，该同志考核评定为：{HRAdvice}";
            }
        }
        


        public string RewardPunishment {

            get {

                string result = "";

                if (RegistrationTime.HasValue)
                {
                    result += $"{RewardReasons}" + Environment.NewLine;
                }

                if (PenaltyTime.HasValue)
                {
                    result += $"{PenaltyReasons}" + Environment.NewLine;
                }

                return result;
            }
        }
    }
}
