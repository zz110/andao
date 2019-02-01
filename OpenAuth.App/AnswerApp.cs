using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;


namespace OpenAuth.App
{
    public class AnswerApp : BaseApp<Answer>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryAnswerListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(Answer obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(Answer obj)
        {
            UnitWork.Update<Answer>(u => u.Id == obj.Id, u => new Answer
            {
               //todo:要修改的字段赋值
               RatersId=obj.RatersId,
               JudgeId=obj.JudgeId,
               Q1=obj.Q1,
               Q2=obj.Q2,
               Q3=obj.Q3,
               Q4=obj.Q4,
               Optime=obj.Optime,
               State=obj.State,
               PlanName=obj.PlanName,
            });

        }

    }
}