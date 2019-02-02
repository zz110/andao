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

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EvaluateAverageScoreForm(string id = "")
        {
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
        public ActionResult page(int limit, int offset) {

            //List<object> dataList = new List<object>();

            //for (int i = 0; i < 100; i++)
            //{
            //    dataList.Add(new
            //    {
            //        UserName = i.ToString(),
            //        DeptName = i.ToString(),
            //        EvaluateMonth = 12,
            //        Score = 90.12,
            //        Created = DateTime.Now
            //    });
            //}
            //int total = dataList.Count;

            var result = App.page(limit, offset);

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