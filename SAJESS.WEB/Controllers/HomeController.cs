using System.Web.Mvc;

namespace SAJESS.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //test
            return View();
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