using OpenAuth.App.Response;
using OpenAuth.App.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenAuth.Mvc.Controllers
{
    public class JudgeController : Controller
    {
        // GET: Judge
        public ActionResult Index()
        {
            ViewBag.user = AuthUtil.GetCurrentUser().User;
            return View();
        }
    }
}