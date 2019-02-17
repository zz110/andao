﻿using System;
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
    public class PerformanceAppraisalsController : BaseController
    {
        public PerformanceAppraisalApp App { get; set; }

        
        public ActionResult Index()
        {
            return View();
        }

        //添加或修改
        [System.Web.Mvc.HttpPost]
        public ActionResult Add(PerformanceAppraisal input)
        {
            Response<object> response = new Response<object>("服务器错误");
            input.Optime = DateTime.Now;
            string sql = " insert into PerformanceAppraisal (JudgeId,AccessmentScore) values('" + input.JudgeId+"','"+input.AccessmentScore+"')";
            App.Update(input);
            response.Message = "";
            response.Code = Response<object>.SUCCESS_CODE;
            return Json(response);
        }

        //添加或修改
        [System.Web.Mvc.HttpPost]
        public string Update(PerformanceAppraisal obj)
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
    }
}