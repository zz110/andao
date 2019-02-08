using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;


namespace OpenAuth.App
{
    public class MonthlyEvaluationApp : BaseApp<MonthlyEvaluation>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryMonthlyEvaluationListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(MonthlyEvaluation obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(MonthlyEvaluation obj)
        {
            UnitWork.Update<MonthlyEvaluation>(u => u.Id == obj.Id, u => new MonthlyEvaluation
            {
               //todo:要修改的字段赋值
            });

        }

    }
}