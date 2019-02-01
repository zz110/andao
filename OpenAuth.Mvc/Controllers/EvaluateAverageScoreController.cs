using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OpenAuth.Mvc.Controllers
{
    /// <summary>
    /// haozheng 2019-01-31
    /// 评价平均分
    /// 平均分每月进行录入，系统自动计算平均分,
    /// 例如:系统中已经录入1-5月份数据，那么自动计算5个月的平均数
    /// </summary>
    public class EvaluateAverageScoreController : Controller
    {
        // GET: EvaluateAverageScore
        public ActionResult Index()
        {
            return View();
        }
    }
}