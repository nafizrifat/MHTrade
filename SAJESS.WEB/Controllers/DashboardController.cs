using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SAJESS.Manager.Interface.Dashboard;
using SAJESS.Manager.Manager.Dashboard;

namespace SAJESS.WEB.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private IDashboardManager _aManager;

        public DashboardController()
        {
            _aManager = new DashboardManager();
        }

        // GET: /Dashboard/GetTotalByDateDashboard
        public ActionResult GetTotalByDateDashboard()
        {

            var data = _aManager.GetTotalByDateDashboard();
            return Json(data.Data, JsonRequestBehavior.AllowGet);
        }
        // GET: /Dashboard/GetTotalByDateDashboard
        public ActionResult GetSupplierWiseTotalPaymentDashboard()
        {

            var data = _aManager.GetSupplierWiseTotalPaymentDashboard();
            return Json(data.Data, JsonRequestBehavior.AllowGet);
        }

        
	}
}