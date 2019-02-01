using System;
using System.Web.Http;
using System.Web.Mvc;
using Infrastructure;
using OpenAuth.App;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.Mvc.Models;
using OpenAuth.Repository.Domain;

namespace OpenAuth.Mvc.Controllers
{
    public class PlansController : BaseController
    {
        public PlanApp App { get; set; }
        public UserManagerApp Uapp { get; set; }

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        //添加或修改
        [System.Web.Mvc.HttpPost]
        public string Add(Plan obj)
        {
            try
            {
                App.Add(obj);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        //添加或修改
        [System.Web.Mvc.HttpPost]
        public string Update(Plan obj)
        {
            try
            {
                App.Update(obj);

            }
            catch (Exception ex)
            {
                Result.Code = 500;
                Result.Message = ex.Message;
            }
            return JsonHelper.Instance.Serialize(Result);
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public string Load([FromUri]QueryPlanListReq request)
        {
            return JsonHelper.Instance.Serialize(App.Load(request));
        }

        [System.Web.Mvc.HttpPost]
        public string Delete(string[] ids)
        {
            try
            {
                App.Delete(ids);
            }
            catch (Exception e)
            {
                Result.Code = 500;
                Result.Message = e.Message;
            }

            return JsonHelper.Instance.Serialize(Result);
        }
        public ViewResult Access()
        {
            return View();
        }
        public ViewResult Check()
        {
            return View();
        }
        public string Up(string ids,string id)
        {
            App.Repository.Update(d => d.Id == id, d => new Plan { RatersId = ids });
            return JsonHelper.Instance.Serialize(new { msg = "成功" });
        }
        public string CheckUp(string ids, string id)
        {
            App.Repository.Update(d => d.Id == id, d => new Plan { JudgeId = ids });
            return JsonHelper.Instance.Serialize(new { msg = "成功" });
        }
        public string LoadUser(string Id)
        {
            var list = App.Repository.FindSingle(d => d.Id == Id).RatersId;
            var result = list.TrimEnd(',');
            return JsonHelper.Instance.Serialize(result);
        }
    }
}