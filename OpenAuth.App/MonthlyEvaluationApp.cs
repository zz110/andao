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