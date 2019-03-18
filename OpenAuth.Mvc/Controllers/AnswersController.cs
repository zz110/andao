using System;
using System.Web.Http;
using System.Web.Mvc;
using Infrastructure;
using OpenAuth.App;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.Mvc.Models;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;

namespace OpenAuth.Mvc.Controllers
{
    public class AnswersController : BaseController
    {
        public AnswerApp App { get; set; }

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        //添加或修改
        [System.Web.Mvc.HttpPost]
        public string Add(Answer obj)
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
        public string Update(Answer obj)
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

        [System.Web.Mvc.HttpGet]
        public ActionResult page(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {
            var result = App.page(limit, offset, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public string Load(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {
            var result = App.page(limit, offset, input);
            return JsonHelper.Instance.Serialize(result);
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

        public ActionResult NoAnswers()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public string GetNoAnswers(int limit, int offset, AnswerSearch input)
        {
            var result = App.NoAnswers(1, 1, input);
            return JsonHelper.Instance.Serialize(result);
        }
    }
}