using System.Web.Mvc;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class FiscalYearController : Controller
    {
        private IFiscalYearConfig _aManager;


        public FiscalYearController()
        {
            _aManager = new FiscalYearConfigManager();
        }
        // GET: Account/FiscalYear
        public ActionResult FiscalyearSetting()
        {
            return View();
        }
        // GET: Account/FiscalYear/GetAllFiscalYear
        public JsonResult  GetAllFiscalYear()
        {
            var data = _aManager.GetAllFiscalYear();
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
        //SET: Account/FiscalYear/CreateFiscalYear
        public JsonResult CreateFiscalYear(A_FiscalYear aObj)
        {
            var data = _aManager.CreateFiscalYear(aObj);
            return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
        }
    }
}