using System;
using System.Web.Http;
using System.Web.Mvc;
using Infrastructure;
using OpenAuth.App;
using OpenAuth.App.Request;
using OpenAuth.App.Response;
using OpenAuth.Mvc.Models;
using OpenAuth.Repository;
using OpenAuth.Repository.Domain;
using OpenAuth.Repository.Dto;
using OpenAuth.App.SSO;
using System.Linq;

namespace OpenAuth.Mvc.Controllers
{
    public class AnnualExaminationRegistrationsController : BaseController
    {
        public AnnualExaminationRegistrationApp App { get; set; }

        public RevelanceManagerApp A { get; set; }

        //
        [Authenticate]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnnualExaminationRegistrationForm(string id = "")
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
                if (string.IsNullOrEmpty(result.Creator))
                {
                    result.Creator = _User.Id;
                    result.UserId = _User.Id;
                    result.Name = _User.Name;
                    string cardId = _User.CardId;
                    string year = _User.CardId.Substring(6, 4);
                    string month = _User.CardId.Substring(10, 2);
                    string date = _User.CardId.Substring(12, 2);
                    result.Birthday = Convert.ToDateTime(year + "-" + month + "-" + date);
                    result.Sex = _User.Sex == 1 ? "男" : "女";
                    result.OrgId =  A.Repository.FindSingle(i=>i.FirstId ==_User.Id && i.Key== "UserOrg")?.SecondId;
                    result.OrgName = AuthUtil.GetCurrentUser().Orgs.Find(i => i.Id == result.OrgId).Name;
                    result.Created = DateTime.Now;
                    result.RegistrationTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
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
        public ActionResult page(int limit, int offset, AnnualExaminationRegistrationQueryInput input)
        {

            input.Creator = _User.Id;
            var result = App.page(limit, offset, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [System.Web.Mvc.HttpPost]
        public ActionResult save(AnnualExaminationRegistration input)
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