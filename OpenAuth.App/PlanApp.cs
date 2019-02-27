using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;


namespace OpenAuth.App
{
    public class PlanApp : BaseApp<Plan>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryPlanListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "PlanName")
            };
        }

        public void Add(Plan obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(Plan obj)
        {
            UnitWork.Update<Plan>(u => u.Id == obj.Id, u => new Plan
            {
                //todo:要修改的字段赋值
                PlanName = obj.PlanName,
                PlanStart = obj.PlanStart,
                PlanEnd = obj.PlanEnd,
                OpTime = DateTime.Now.ToString("yyyy-MM-dd"),
                State=obj.State


            });

        }

    }
}