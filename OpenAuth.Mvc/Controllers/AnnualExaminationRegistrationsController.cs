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
using OpenAuth.Mvc.Utils;
using System.Collections.Generic;

namespace OpenAuth.Mvc.Controllers
{
    public class AnnualExaminationRegistrationsController : BaseController
    {
        public AnnualExaminationRegistrationApp App { get; set; }

        public StatisticalAnalysisApp Appp { get; set; }

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
                    User uu = App.Repository.ExecuteQuerySql<User>(@"SELECT u.Id,
      [Account]
      ,[Password]
      , u.Name
      ,[Sex]
      ,[CrateId]
      , ro.Name[TypeName]
      , o.BizCode[TypeId]
      ,[Age]
      ,[XRank]
      ,[ZRank]
      ,[CardId]
      ,[Politicalaffiliation]
      ,[Position]
      ,[DegreeEdu]
      ,[Nation]
      ,[Officetime],u.Status,u.BizCode,u.CreateTime
  FROM[cp].[dbo].[User] u left join dbo.Relevance r1 on r1.FirstId = u.Id and r1.[Key] = 'UserRole' left join Role ro on ro.Id = r1.SecondId
  left join dbo.Relevance r2 on r2.FirstId = u.Id and r2.[Key] = 'UserOrg'  left join dbo.Org o on o.Id = r2.SecondId
   where u.Id = '" + result.UserId + "'").ToList<User>()[0];
                    result.Politicalaffiliation = uu.Politicalaffiliation;
                    result.Position = uu.Position;
                    result.DegreeEdu = uu.DegreeEdu;
                    result.Nation = uu.Nation;
                    if (uu.Officetime == null)
                        uu.Officetime = DateTime.Now.ToString("yyyy-MM-dd");
                    result.Officetime =Convert.ToDateTime(uu.Officetime);
                    EvaluationscoreQueryInput input = new EvaluationscoreQueryInput();
                    input.role = uu.TypeName;
                    input.DeptType = uu.TypeId;
                    input.EvaluateYear = DateTime.Now.Year;

                    List<EvaluationTotalscoreOutput> li = Appp.get_totalscore_data_all(input);
                    
                   
                    EvaluationTotalscoreOutput ue = (EvaluationTotalscoreOutput)li.FirstOrDefault(i => i.Id == uu.Id);
                    if (ue != null)
                    {
                        result.FactorScore = ue.要素;
                        result.EvaluationCount = ue.总票数;
                        List<EvaluationTotalscoreOutput> phb = li.FindAll(i => i.总分 > ue.总分).ToList<EvaluationTotalscoreOutput>();
                        result.Rank = phb.Count() + 1;
                        result.Rate = ue.优秀率;
                    }

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
            if(_User.Id!= "00000000-0000-0000-0000-000000000000")
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

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileContentResult ExportWord(string id = "") {


            if (string.IsNullOrEmpty(id)) return null;

             var result = App.get(id);

            string FileLocation = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //模板路径
            string TemplatePath = FileLocation + "\\template\\evaluation_export_template.docx";

            ExportWord Word = new ExportWord(TemplatePath);
            try
            {
                Word.FillTable(result);
                Word.ReplaceKey("OrgName", result.OrgName);
                Word.ReplaceKey("RegistrationTime", result.RegistrationTime_);
                Word.ReplaceKey("_Officialadvice", result._Officialadvice);
                Word.ReplaceKey("HRAdvice_", result.HRAdvice);
                Word.ReplaceKey("Notes", result.Notes);
                if (!string.IsNullOrEmpty(result.RewardPunishment))
                {
                    Word.ReplaceKey("RewardPunishment", result.RewardPunishment);
                }
                else
                {
                    Word.ReplaceKey("RewardPunishment", "无");
                }
                
           
                var doc = Word.GetWord();

                return File(doc, "application/octet-stream", Server.UrlEncode("管理人员和专业技术人员年度考核登记表.doc"));
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                Word.Close();
            }

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