using System;
using System.Collections.Generic;
using System.Linq;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Repository.Domain;


namespace OpenAuth.App
{
    public class EvaluateAverageScoreApp : BaseApp<EvaluateAverageScore>
    {
        public RevelanceManagerApp ReleManagerApp { get; set; }

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryEvaluateAverageScoreListReq request)
        {
             return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "Id desc")
            };
        }

        public void Add(EvaluateAverageScore obj)
        {
            Repository.Add(obj);
        }
        
        public void Update(EvaluateAverageScore obj)
        {
            UnitWork.Update<EvaluateAverageScore>(u => u.Id == obj.Id, u => new EvaluateAverageScore
            {
                //todo:要修改的字段赋值
                UserId = obj.UserId,
                OrgId = obj.OrgId,
                Updated = obj.Updated,
                EvaluateYear = obj.EvaluateYear,
                EvaluateMonth = obj.EvaluateMonth,
                Score = obj.Score
            });

        }

    }
}