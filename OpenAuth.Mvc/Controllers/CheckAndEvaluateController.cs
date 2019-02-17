using OpenAuth.App;
using OpenAuth.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenAuth.Mvc.Controllers
{
    public class CheckAndEvaluateController : Controller
    {
        public PerformanceAppraisalApp App { get; set; }

        // GET: CheckAndEvaluate
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        //int limit, int offset, PerformanceAppraisalQueryInput input
        public ActionResult List(string year,string type)
        {
            PerformanceAppraisalQueryInput input = new PerformanceAppraisalQueryInput();
            //var result = App.List("970edfb5-53c7-4dc8-a086-08a28ba4686a", "2019", input);
            var result = App.List(year, input);
            List<PerformanceAppraisalOutPut> list = result as List<PerformanceAppraisalOutPut>;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].q6 = list[i].q6 * 100 / list[i].q6count;
                if (type == "正职")
                {
                    list[i].pingce = (list[i].q1 + list[i].q2 + list[i].q3 + list[i].q4 +
                        list[i].q5) * (decimal)0.6 + list[i].q6 * (decimal)0.4;
                }
                else
                {
                    list[i].pingce = list[i].q6;
                }
                list[i].kaopingdefen = list[i].pingce * (decimal)0.4 +
                    (list[i].MonthlyAVG + list[i].AccessmentScore) * (decimal)0.3;
            }
            return Json(new { total = 10000, rows = result }, JsonRequestBehavior.AllowGet);
        }
    }
}