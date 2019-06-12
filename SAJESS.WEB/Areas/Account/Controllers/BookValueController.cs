using System;
using System.Web.Mvc;
using SAJESS.Entities;
using SAJESS.Manager;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class BookValueController : Controller
    {
        private IBookValue _aManager;

        public BookValueController()
        {
            _aManager = new BookValueManager();
        }
        // GET: Account/BookValue
        public ActionResult BookValueSettings()
        {
            
            return View();
        }
        public ResponseModel SaveBookValue()
        {
            DateTime start = Convert.ToDateTime("2016-7-1");
            DateTime end = Convert.ToDateTime("2017-6-30");

            return _aManager.SaveBookValue(start,end);
        }
        // GET : Account/BookValue/GetFiscalYear
        public String GetFiscalYear()
        {
            return _aManager.GetFiscalYear().ToString();
        }
        //// POST : Account/BookValue/CreateFiscalYear
        //public ActionResult CreateFiscalYear()
        //{
        //    return View();
        //}
        public JsonResult CreateFiscalYear(A_FiscalYear aObj)
        {
            var data = _aManager.CreateFiscalYear(aObj);
            return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
        }
    }
}