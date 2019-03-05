using System.Text;
using System.Web.Mvc;

namespace OpenAuth.Mvc.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.u = _User;
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
       
       
        
        public ActionResult Git()
        {
            return View();
        }

      
    }
}