using System;
using System.Web.Mvc;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class A_CashReceiveStatementController : Controller
    {
        private ICashReceiveStatementManager _aManager;
        public A_CashReceiveStatementController()
        {
            _aManager=new CashReceiveStatementManager();
            //var data= _aManager.GetACashReceiveStatemtByDate(DateTime.Now, DateTime.Now);
        }
        // GET: Account/A_CashReceiveStatement
        public ActionResult ACashReceiveStatementSetting()
        {
            return View();
        }

        public JsonResult GetAllCashReceiveStatement(DateTime startDate, DateTime endDate)
        { 
 
             var data = _aManager.GetACashReceiveStatemtByDate(startDate, endDate);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
            //return null;
        }
    }
}