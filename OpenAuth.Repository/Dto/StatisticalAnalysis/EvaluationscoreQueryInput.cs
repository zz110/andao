using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.Repository.Dto
{
    /// <summary>
    /// 评价得分插叙参数
    /// </summary>
    public class EvaluationscoreQueryInput
    {
        /// <summary>
        /// 角色职务
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// 测评年份
        /// </summary>
        public int? EvaluateYear { get; set; }
        /// <summary>
        /// 部门类型
        /// </summary>
        public string DeptType { get; set; }

        public string UserName { get; set; }

        public string OrgName { get; set; }

    }
}
