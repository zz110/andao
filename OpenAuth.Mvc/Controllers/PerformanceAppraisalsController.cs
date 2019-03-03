using System;
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
    public class PerformanceAppraisalsController : BaseController
    {
        public PerformanceAppraisalApp App { get; set; }

        
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult page(int limit, int offset, PerformanceAppraisalQueryInput input)
        {
            var result = App.page(limit, offset, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public string Load([FromUri]QueryPerformanceAppraisalListReq request)
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

        public ActionResult PerformanceForm()
        {
            return View();
        }

        public JsonResult Add(PerformanceAppraisal model)
        {
            model.Id = Guid.NewGuid().ToString();
            model.Optime = Convert.ToDateTime(model.State + "-" + DateTime.Now.ToString("MM-dd"));
            model.RatersId = _User.Id;
            string id = App.Add(model);
            return Json(new { result = true, msg = "保存成功", id = id });
        }

        public JsonResult Update(PerformanceAppraisal model)
        {
            PerformanceAppraisal old = App.Get(model.Id);
            old.Optime = Convert.ToDateTime(model.State + "-" + DateTime.Now.ToString("MM-dd"));
            App.Update(model);
            return Json(new { result = true, msg = "保存成功" });
        }
    }
}