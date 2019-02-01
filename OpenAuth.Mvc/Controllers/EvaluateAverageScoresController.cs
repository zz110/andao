using System;
using System.Collections.Generic;
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
    public class EvaluateAverageScoresController : BaseController
    {
        public EvaluateAverageScoreApp App { get; set; }

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult page(int limit, int offset) {

            List<object> dataList = new List<object>();

            for (int i = 0; i < 100; i++)
            {
                dataList.Add(new
                {
                    UserName = i.ToString(),
                    DeptName = i.ToString(),
                    EvaluateMonth = 12,
                    Score = 90.12,
                    Created = DateTime.Now
                });
            }
            int total = dataList.Count;
            return Json(new { total = total, rows = dataList }, JsonRequestBehavior.AllowGet);
        }

        //添加或修改
        [System.Web.Mvc.HttpPost]
        public string Add(EvaluateAverageScore obj)
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
        public string Update(EvaluateAverageScore obj)
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
        public string Load([FromUri]QueryEvaluateAverageScoreListReq request)
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
    }
}