using System.Web.Mvc;

namespace Apricot.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            //reached here means User is Authenticated, Send him to Dashboard
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}