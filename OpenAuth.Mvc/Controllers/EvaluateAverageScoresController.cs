using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Infrastructure;
using OpenAuth.App;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using OpenAuth.Mvc.Models;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;

namespace OpenAuth.Mvc.Controllers
{
    public class EvaluateAverageScoresController : BaseController
    {
        public EvaluateAverageScoreApp App { get; set; }
        
        private User _User
        {
            get
            {
                return AuthUtil.GetCurrentUser().User;
            }
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 平均分维护
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EvaluateAverageScoreForm(string id = "")
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 统计分析
        /// </summary>
        /// <returns></returns>
        public ActionResult statistic_analysis() {

            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult get_statistic_analysis_data(EvaluateStatisticAnalysisQueryInput input)
        {
            var result = App.get_statistic_analysis_data(input);
            return Json(result, JsonRequestBehavior.AllowGet);
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
                if (string.IsNullOrEmpty(result.Creator)) {
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
        public ActionResult page(int limit, int offset, EvaluateAverageScoreQueryInput input) {

            input.Creator = AuthUtil.GetCurrentUser().User.Id;
            var result = App.page(limit, offset,  input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult save(EvaluateAverageScore input) {

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
                else {
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

        /// <summary>
        /// 加载列表
        /// </summary>
        public string Load([FromUri]QueryEvaluateAverageScoreListReq request)
        {
            return JsonHelper.Instance.Serialize(App.Load(request));
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(string[] ids)
        {
            Response<object> response = new Response<object>("删除失败");
            try
            {
                App.Delete(ids);
                response.Message = "删除成功";
                response.Code = Response<object>.SUCCESS_CODE;
            }
            catch (Exception ex)
            {
                response.Code = Response<object>.ERROR_CODE;
            }
            return Json(response);
        }
    }
}