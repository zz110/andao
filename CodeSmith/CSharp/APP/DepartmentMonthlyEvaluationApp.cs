using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;


namespace OpenAuth.App
{
    public class DepartmentMonthlyEvaluationApp : BaseApp<DepartmentMonthlyEvaluation>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryDepartmentMonthlyEvaluationListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(DepartmentMonthlyEvaluation obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(DepartmentMonthlyEvaluation obj)
        {
            UnitWork.Update<DepartmentMonthlyEvaluation>(u => u.Id == obj.Id, u => new DepartmentMonthlyEvaluation
            {
               //todo:要修改的字段赋值
            });

        }

    }
}