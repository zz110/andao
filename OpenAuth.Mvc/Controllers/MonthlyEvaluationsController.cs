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
    public class MonthlyEvaluationsController : BaseController
    {
        public MonthlyEvaluationApp App { get; set; }

        /// <summary>
        /// 月度评价排名维护
        /// </summary>
        /// <returns></returns>
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 月度评价排名维护
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MonthlyEvaluationForm(string id) {

            ViewBag.Id = id;
            return View();
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public ActionResult get(string id = "")
        {
            Response<object> response = new Response<object>();
            try
            {
                var result = App.get(id);
                if (string.IsNullOrEmpty(result.Creator))
                {
                    result.Creator = _User.Id;
                    result.UserId = "";
                    result.OrgId = "";
                    result.EvaluateYear = DateTime.Now.Year;
                    result.EvaluateMonth = DateTime.Now.Month;
                    result.Created = DateTime.Now;
                }
                response.Result = result;
                response.Message = "";
                response.Code = Response<object>.SUCCESS_CODE;
            }
            catch (Exception ex)
            {
                response.Code = Response<object>.ERROR_CODE;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult page(int limit, int offset, MonthlyEvaluationQueryInput input)
        {

            input.Creator = _User.Id;
            var result = App.page(limit, offset, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult save(MonthlyEvaluation input)
        {

            Response<object> response = new Response<object>("服务器错误");
            try
            {
                if (!ModelState.IsValid)
                {
                    response.Message = this.GetErrors();
                    return Json(response);
                }

                if (string.IsNullOrEmpty(input.Id))
                {
                    if (!App.Exists(input))
                    {
                        response.Result = App.Add(input);
                    }
                    else
                    {
                        response.Message = "数据已存在，请勿重复添加";
                        return Json(response);
                    }
                }
                else
                {
                    App.Update(input);
                    response.Result = input.Id;
                }
                response.Message = "";
                response.Code = Response<object>.SUCCESS_CODE;
            }
            catch (Exception ex)
            {
                response.Code = Response<object>.ERROR_CODE;
                response.Message = ex.Message;
            }
            return Json(response);
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