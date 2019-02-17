using OpenAuth.App;
using OpenAuth.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenAuth.Mvc.Controllers
{
    /// <summary>
    /// 统计分析
    /// </summary>
    public class StatisticalAnalysisController : Controller
    {
        public StatisticalAnalysisApp App { get; set; }

        #region 测评得分
        /// <summary>
        /// 测评得分
        /// </summary>
        /// <returns></returns>
        public ActionResult evaluationscore()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult get_evaluationscore_data(EvaluationscoreQueryInput input)
        {
            var result = App.get_evaluationscore_data(input);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 综合得分
        /// <summary>
        /// 综合得分
        /// </summary>
        /// <returns></returns>
        public ActionResult integrationscore()
        {
            return View();
        }


        [System.Web.Mvc.HttpGet]
        public ActionResult get_integration_score_data(EvaluationscoreQueryInput input)
        {
            var result = App.get_integration_score_data(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 总分
        /// <summary>
        /// 总分
        /// </summary>
        /// <returns></returns>
        public ActionResult totalscore()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult get_totalscore_data(EvaluationscoreQueryInput input)
        {
            var result = App.get_totalscore_data(input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}