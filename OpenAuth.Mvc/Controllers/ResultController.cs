using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure;
using OpenAuth.App;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.Mvc.Models;
using OpenAuth.Repository.Domain;

namespace OpenAuth.Mvc.Controllers
{
    public class ResultController : Controller
    {
        public UserManagerApp Uapp { set; get; }
        public PlanApp Papp { set; get; }
        public RevelanceManagerApp Rapp { set; get; }
        public OrgManagerApp Oapp { set; get; }
        public AnswerApp Aapp { set; get; }
        // GET: Result
        public ActionResult Index()
        {
            return View();
        }
        public string login(string name, string pwd)
        {
            User result = Uapp.Repository.FindSingle(d => d.Account == name && d.Password == pwd);
            if (result != null)
            {
                return JsonHelper.Instance.Serialize(new { code = 1, msg = "登陆成功", user = result });
            }
            else
            {
                return JsonHelper.Instance.Serialize(new { code = 0, msg = "账号或密码错误" });
            }
        }
        public string change(string id, string pwd)
        {
            Uapp.Repository.Update(d => d.Id == id, d => new User { Password = pwd, BizCode = "1" });
            return JsonHelper.Instance.Serialize(new { code = 1, msg = "修改成功" });
        }
        public string JudgeList(string testId, string userId)
        {
            object ob = null;
            string where = "";
            if (!string.IsNullOrEmpty(testId))
            {
                where = " and PlanName='" + testId + "'";
            }

            string sql = @"select PlanId planid,PlanName,JudgeId,RatersId,UserId id,UserName name,o.name part,state from (

select a.*,isnull(an.[State],'待评价') state from (
select a.[Id] as PlanId
      ,[PlanName]
      ,[JudgeId]
	  ,RatersId,u.id as UserId,u.Name as UserName from (
SELECT [Id]
      ,[PlanName]
      ,[JudgeId]
		,'" + userId + @"' RatersId
  FROM [cp].[dbo].[Plan] where [State] =1 and PlanStart < getdate() and 
                                                    PlanEnd > getdate() and ratersid like '%" + userId + @"%' "+ where + @") a 
             
      left join [user] u on charindex( u.id,a.JudgeId)>0 ) a left join dbo.Answer an on a.PlanId = an.PlanId and a.RatersId=an.RatersId 
      and a.UserId = an.JudgeId
             where (an.State<>'已提交' or an.State is null)
             
             
             ) a left join dbo.Relevance r on a.UserId=r.firstId and r.[key]='UserOrg' left join dbo.Org o on o.id=r.secondId
  
  ";
            var vli = Papp.Repository.ExecuteQuerySql<CustomAll>(sql, new object[] { }).ToList<CustomAll>();

            //var list = Papp.Repository.Find(
            //                                        d => (testId == "" || d.PlanName == testId) &&
            //                                        d.State == 1 &&
            //                                        d.PlanStart < DateTime.Now &&
            //                                        d.PlanEnd > DateTime.Now &&
            //                                        d.RatersId.Contains(userId)).ToList<Plan>();
            //if (list != null)
            //{
            //    List<object> result = new List<object>();
            //    for (int k = 0; k < list.Count(); k++) {
            //        Plan p = list[k];
            //        string planId = p.Id;
            //        //被测评人Ids
            //        var ll = list[k].JudgeId.TrimEnd(',').Split(',');
            //        for (int i = 0; i < ll.Length; i++)
            //        {
            //            var temp = ll[i];


            //            //部门信息
            //            var part = Rapp.Repository.FindSingle(d => d.Key == "UserOrg" && d.FirstId == temp).SecondId;
            //            var part1 = Oapp.Repository.FindSingle(d => d.Id == part);
            //            var user = Uapp.Repository.FindSingle(d => d.Id == temp);

            //            var answer
            //                 = Aapp.Repository.FindSingle(d =>
            //                                                             d.PlanId == planId &&
            //                                                             d.JudgeId == temp &&
            //                                                             d.RatersId == userId);

            //            if (answer == null)
            //            {
            //                ob = new { name = user.Name, part = part1.Name, id = user.Id, state = "待评价", PlanName = list[k].PlanName, planid = list[k].Id };
            //                result.Add(ob);
            //            }
            //            else
            //            {
            //                if (answer.State != "已提交")
            //                {
            //                    ob = new { name = user.Name, part = part1.Name, id = user.Id, state = answer.State, PlanName = list[k].PlanName, planid = list[k].Id };
            //                    result.Add(ob);
            //                }
            //            }


            //        }

            //    }
            //    if (result.Count() > 0)
            //    {
            //        return JsonHelper.Instance.Serialize(new { code = 1, msg = "修改成功", list = vli.Distinct() });
            //    }
            //    else
            //    {
            //        return JsonHelper.Instance.Serialize(new { code = 0, msg = "没有获取到任何信息" });
            //    }

                

            //}
            //else
            //{
            //    return JsonHelper.Instance.Serialize(new { code = 0, msg = "所输入方案号不正确" });
            //}
            return JsonHelper.Instance.Serialize(new { code = 1, msg = "修改成功", list = vli.Distinct() });

        }
        /// <summary>
        /// 提交测评结果
        /// </summary>
        /// <param name="Q1"></param>
        /// <param name="Q2"></param>
        /// <param name="Q3"></param>
        /// <param name="Q4"></param>
        /// <param name="PlanId"></param>
        /// <param name="PlanName"></param>
        /// <param name="JudgeId"></param>
        /// <param name="RatersId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string sub(string Q1, string Q2, string Q3, string Q4, string Q5, string Q6, string PlanId, string PlanName, string JudgeId, string RatersId, string state)
        {
            Answer an = new Answer() { Q1 = Q1, Q2 = Q2, Q3 = Q3, Q4 = Q4, Q5 = Q5, Q6 = Q6,
                PlanId = PlanId, PlanName = PlanName,
                JudgeId = JudgeId, RatersId = RatersId,
                State = state
            };
            Answer model = null;
            try
            { model = Aapp.Repository.FindSingle(d => d.PlanId == PlanId && d.JudgeId == JudgeId && d.RatersId == RatersId); } catch (Exception ex) { }
            if (model != null)
            {
                Aapp.Repository.Update(d => d.PlanId == PlanId && d.JudgeId == JudgeId && d.RatersId == RatersId,
                    d => new Answer
                    {
                        Q1 = Q1,
                        Q2 = Q2,
                        Q3 = Q3,
                        Q4 = Q4,
                        Q5 = Q5,
                        Q6 = Q6,
                        PlanId = PlanId,
                        PlanName = PlanName,
                        JudgeId = JudgeId,
                        RatersId = RatersId,
                        State = state
                    });
            }
            else
            {
                try {
                    Aapp.Repository.Add(an);
                } catch (Exception ex) {
                    string error = ex.ToString();
                }

            }


            return JsonHelper.Instance.Serialize(new { code = 1, msg = "测评成功" });
        }
        public string GetZc(string PlanName, string JudgeId, string RatersId)
        {
            var model = Aapp.Repository.FindSingle(d => d.PlanName == PlanName && d.JudgeId == JudgeId && d.RatersId == RatersId && d.State == "已暂存");
            if (model != null)
            {
                return JsonHelper.Instance.Serialize(new { code = 1, msg = "成功", data = model });
            }
            else
            {
                return JsonHelper.Instance.Serialize(new { code = 0, msg = "失败", });
            }

        }
        public string changePwd(string oldpwd, string newpwd, string userId)
        {
            User result = Uapp.Repository.FindSingle(d => d.Id == userId && d.Password == oldpwd);
            if (result != null)
            {
                Uapp.Repository.Update(d => d.Id == userId, d => new User { Password = newpwd });
                return JsonHelper.Instance.Serialize(new { code = 1, msg = "成功", });
            }
            else
            {
                return JsonHelper.Instance.Serialize(new { code = 0, msg = "密码错误", });
            }
        }


    }

    public class CustomAll{
        public string planid { get; set; }
        public string PlanName { get; set; }
        public string JudgeId { get; set; }
        public string RatersId { get; set; }
        public string id { get; set; }

        public string name { get; set; }

        public string part { get; set; }

        public string state { get; set; }
    }
}