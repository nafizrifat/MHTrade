using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class CashBankReceivePaymentController : Controller
    {
        private ICashOrBankReceivePaymentManager _aManager;
        public CashBankReceivePaymentController()
        {
            _aManager = new CashOrBankReceivePaymentManager();
        }
        // GET: Account/Cash_BankReceive
        public ActionResult Cash_BankReceive()
        {
            return View();
        }
        // GET: Account/Cash_BankPayment
        public ActionResult Cash_BankPayment()
        {
            return View();
        }
        // GET: Account/CashBankReceivePayment/LoadAllAccountHead
        public JsonResult LoadAllAccountHead(int type)
        {
            var data = _aManager.LoadAllAccountHead(type);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
        // GET: Account/CashBankReceivePaymentr/LoadOtherHeads
        public JsonResult LoadOtherHeads(int id)
        {
            var data = _aManager.LoadOtherHeads(id);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
        // POST: Account/CashBankReceivePayment/SaveReceiveTransaction
        public JsonResult SaveReceiveTransaction(List<ManualJournal> data,int cashOrBankHead,Int32 voucherNo,DateTime transactionDate,string particular)
        {
            var Data = _aManager.SaveReceiveTransaction(data, cashOrBankHead, voucherNo, transactionDate, particular);
            return Json(new { success = Data.Status, Data }, JsonRequestBehavior.AllowGet);
        }
        // POST: Account/CashBankReceivePayment/SavePaymentTransaction
        public JsonResult SavePaymentTransaction(List<ManualJournal> data, int cashOrBankHead, Int32 voucherNo, DateTime transactionDate, string particular)
        {
            var Data = _aManager.SavePaymentTransaction(data, cashOrBankHead, voucherNo, transactionDate, particular);
            return Json(new { success = Data.Status, Data }, JsonRequestBehavior.AllowGet);
        }
        
    }
}