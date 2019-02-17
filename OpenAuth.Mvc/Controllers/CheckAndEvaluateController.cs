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
        public ActionResult List(int limit, int offset, PerformanceAppraisalQueryInput input)
        {
            var result = App.page(limit, offset, input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}