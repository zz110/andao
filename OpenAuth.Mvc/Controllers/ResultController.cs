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
            if (result!=null)
            {
                 return JsonHelper.Instance.Serialize(new {code=1, msg="登陆成功",user=result});
            }
            else
            {
                return JsonHelper.Instance.Serialize(new {code=0, msg = "账号或密码错误" });
            }
        }
        public string change(string id, string pwd)
        {
            Uapp.Repository.Update(d => d.Id == id, d => new User { Password = pwd, BizCode = "1" });
            return   JsonHelper.Instance.Serialize(new { code = 1, msg = "修改成功" });
        }
        public string JudgeList(string testId,string userId)
        {
            object ob = null;

            var list = Papp.Repository.FindSingle(
                                                    d => d.PlanName == testId &&
                                                    d.State == 1 &&
                                                    d.PlanStart < DateTime.Now &&
                                                    d.PlanEnd > DateTime.Now &&
                                                    d.RatersId.Contains(userId));
            if (list!=null)
            {
                List<object> result = new List<object>();
                //被测评人Ids
                var ll = list.JudgeId.TrimEnd(',').Split(',');
                for (int i = 0; i < ll.Length; i++)
                {
                    var temp = ll[i];
                    //部门信息
                    var part = Rapp.Repository.FindSingle(d => d.Key == "UserOrg" && d.FirstId == temp).SecondId;
                    var part1 = Oapp.Repository.FindSingle(d => d.Id == part);
                    var user = Uapp.Repository.FindSingle(d => d.Id == temp);
                    var answer = Aapp.Repository.FindSingle(d =>
                                                                 d.PlanName == testId &&
                                                                 d.State != "已提交" &&
                                                                 d.JudgeId == temp &&
                                                                 d.RatersId == userId);
                    if (answer==null)
                    {
                         ob = new { name = user.Name, part = part1.Name, id = user.Id, state = "待评价" };
                    }
                    else
                    {
                         ob = new { name = user.Name, part = part1.Name, id = user.Id, state =  answer.State  };
                    }
                    
                    result.Add(ob);
                }
                if (ll.Length > 0)
                {
                    return JsonHelper.Instance.Serialize(new { code = 1, msg = "修改成功", list = result.Distinct(), planid = list.Id });
                }
                else
                {
                    return JsonHelper.Instance.Serialize(new { code = 0, msg = "所输入方案号不正确" });
                }
            }
            else
            {
                return JsonHelper.Instance.Serialize(new { code = 0, msg = "所输入方案号不正确" });
            }
            
            
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
        public string sub(string Q1, string Q2, string Q3, string Q4, string Q5,string Q6,string PlanId, string PlanName, string JudgeId, string RatersId,string state)
        {
            Answer an = new Answer() { Q1 = Q1, Q2 = Q2, Q3 = Q3, Q4 = Q4, Q5 = Q5, Q6 = Q6,
                PlanId = PlanId, PlanName = PlanName,
                JudgeId = JudgeId, RatersId = RatersId,
                State = state };
            Answer model = Aapp.Repository.FindSingle(d => d.PlanId == PlanId);
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
                Aapp.Repository.Add(an);
            }
            
          
            return JsonHelper.Instance.Serialize(new { code = 1, msg = "测评成功" });
        }
        public string GetZc(string PlanName, string JudgeId, string RatersId)
        {
            var model = Aapp.Repository.FindSingle(d => d.PlanName == PlanName && d.JudgeId == JudgeId && d.RatersId == RatersId&&d.State=="已暂存");
            if (model!=null)
            {
                return JsonHelper.Instance.Serialize(new { code = 1, msg = "成功",data=model });
            }
            else
            {
                return JsonHelper.Instance.Serialize(new { code = 0, msg = "失败", });
            }
            
        }
        public string changePwd(string oldpwd, string newpwd, string userId)
        {
            User result = Uapp.Repository.FindSingle(d => d.Id == userId && d.Password == oldpwd);
            if (result!=null)
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
}