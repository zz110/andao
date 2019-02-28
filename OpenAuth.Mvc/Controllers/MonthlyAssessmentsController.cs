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
    /// <summary>
    /// 月度考核成绩
    /// </summary>
    public class MonthlyAssessmentsController : BaseController
    {
        public MonthlyAssessmentApp App { get; set; }

        /// <summary>
        /// 月度考核成绩
        /// </summary>
        /// <returns></returns>
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult MonthlyAssessmentForm(string id)
        {
            ViewBag.Id = id;
            return View();
        }



        /// <summary>
        /// 月度岗位履责考评结果
        /// </summary>
        /// <returns></returns>
        public ActionResult MonthlyPostAssessment()
        {

            return View();
        }

        /// <summary>
        /// 月度岗位履责考评结果数据
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        public ActionResult GetMonthlyPostAssessment(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {
            var result = App.GetMonthlyPostAssessment(limit, offset, input);
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
        public ActionResult page(int limit, int offset, MonthlyAssessmentQueryInput input)
        {

            input.Creator = _User.Id;
            var result = App.page(10000, 0, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult save(MonthlyAssessment input)
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

        public ActionResult MonthlyPostAssessmentNoDepartmentScore()
        {
            return View();
        }

        public ActionResult MonthStatistics()
        {
            return View();
        }

        public JsonResult GetMonthStatistics(int limit, int offset, MonthlyPostAssessmentQueryInput input)
        {
            var result = App.GetMonthlyStatisticsAssessment(limit, offset, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonthStatistics2()
        {
            return View();
        }

        public JsonResult GetTableColumns4MonthlyStatisticsAssessment2(string deptType)
        {
            try
            {
                var rslt = App.GetTableColumns4MonthlyStatisticsAssessment2(deptType);

                return Json(rslt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ContentResult OutDataTmp1(int queryYear,string role,string DeptType)
        {
            try
            {
                var rslt = App.GetMonthlyStatisticsAssessment2(queryYear,role,DeptType) as System.Data.DataTable;

                var tmp = Newtonsoft.Json.JsonConvert.SerializeObject(rslt);

                var rsltStr = "{\"total\":" + rslt.Rows.Count + ",\"rows\":" + tmp + "}";

                return Content(rsltStr);

                //return Json(rslt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public JsonResult Add(MonthlyAssessment model) {
            model.Id = Guid.NewGuid().ToString();
            model.Creator = _User.Id;
            App.Add(model);
            return Json(new { result = true, msg = "保存成功" });
        }

        public JsonResult Update(MonthlyAssessment model)
        {
            App.Update(model);
            return Json(new { result = true, msg = "保存成功" });
        }

    }

    public class Reply<T>
    {
        public int total { get; set; } = 0;

        public System.Collections.Generic.List<T> rows { get; set; } = new System.Collections.Generic.List<T>();
    }
}