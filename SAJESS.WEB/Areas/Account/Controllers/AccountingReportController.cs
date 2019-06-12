using System;
using System.Linq;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAJESS.Entities;


namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class AccountingReportController : Controller
    {
        private Entities.Entities _db;

        public AccountingReportController()
        {
            _db = new Entities.Entities();
        }
        // GET: Account/AccountingReport/Index
        public ActionResult Index()
        {
            return View();
        }
        //Account/AccountingReport/TrialBalanceData
        public JsonResult TrialBalanceData()
        {
            DateTime date = DateTime.Now;
            Session["rptSource"] = _db.spTrialBalance(date).ToList();
            int data = _db.spTrialBalance(date).ToList().Count();
            return Json(data > 0, JsonRequestBehavior.AllowGet);
        }
        // GET: Account/AccountingReport/TrialBalanceReport
        public void TrialBalanceReport()
        {
            using (ReportClass rptH = new ReportClass())
            {
                rptH.FileName = Server.MapPath("~/Areas/Account/AccountingReport/rptTrialBalance.rpt");
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                if (rptSource != null)
                {
                    rptH.Load();
                    rptH.SetDataSource(rptSource);
                    rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                }
                Session["rptSource"] = null;
            }
        }
        //Account/AccountingReport/GlDateWiseTransactionData
        public JsonResult GlDateWiseTransactionData(int id, DateTime startDate, DateTime endDate)
        {

            Session["rptSource"] = _db.spGl_DateWiseTransaction( startDate, endDate,id).ToList();
            int data = _db.spGl_DateWiseTransaction(startDate, endDate, id).ToList().Count();
            return Json(data > 0, JsonRequestBehavior.AllowGet);

        }
        // GET: Account/AccountingReport/GlDateWiseTransactionReport
        public void GlDateWiseTransactionReport()
        {
            using (ReportClass rptH = new ReportClass())
            {
                rptH.FileName = Server.MapPath("~/Areas/Account/AccountingReport/rptGLWiseTransactionReport.rpt");
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                if (rptSource != null)
                {
                    rptH.Load();
                    rptH.SetDataSource(rptSource);
                    rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                }
                Session["rptSource"] = null;
            }
        }


        //Account/AccountingReport/DateWiseTransactionData
        public JsonResult DateWiseTransactionData(DateTime startDate, DateTime endDate)
        {

            Session["rptSource"] = _db.spDateWiseTransaction(startDate, endDate).ToList();
            Session["FromDate"] = startDate;
            Session["ToDate"] = endDate;

            var data = _db.spDateWiseTransaction(startDate, endDate).ToList().Count();
            return Json(data > 0, JsonRequestBehavior.AllowGet);

        }
        // GET: Account/AccountingReport/DateWiseTransactionReport
        public void DateWiseTransactionReport()
        {
            using (ReportClass rptH = new ReportClass())
            {
                rptH.FileName = Server.MapPath("~/Areas/Account/AccountingReport/rptDateWiseTransaction.rpt");
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                DateTime startDate =Convert.ToDateTime( Session["FromDate"]);
                DateTime endDate = Convert.ToDateTime(Session["ToDate"]);
                if (rptSource != null)
                {
                    rptH.Load();
                    
                    rptH.SetDataSource(rptSource);
                    rptH.SetParameterValue("FromDate", startDate);
                    rptH.SetParameterValue("ToDate", endDate);

                    rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                }
                Session["rptSource"] = null;
                Session["FromDate"] = null;
                Session["ToDate"] = null;
            }
        }

        // GET:Account/AccountingReport/AccountingTopSheet
        //public JsonResult AccountingTopSheet(DateTime startDate, DateTime endDate, String Type)
        //{

        //    Session["rptSource"] = _db.spTopSheet(startDate, endDate, Type);
        //    Session["FromDate"] = startDate;
        //    Session["ToDate"] = endDate;

        //    int data = _db.spTopSheet(startDate, endDate, Type);
        //    return Json(data > 0, JsonRequestBehavior.AllowGet);

        //}
        // GET: Account/AccountingReport/AccountingTopSheetReport
        public void AccountingTopSheetReport()
        {
            using (ReportClass rptH = new ReportClass())
            {
                rptH.FileName = Server.MapPath("~/Areas/Account/AccountingReport/rptTopSheet.rpt");
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                DateTime startDate = Convert.ToDateTime(Session["FromDate"]);
                DateTime endDate = Convert.ToDateTime(Session["ToDate"]);
                if (rptSource != null)
                {
                    rptH.Load();

                    rptH.SetDataSource(rptSource);
                    rptH.SetParameterValue("FromDate", startDate);
                    rptH.SetParameterValue("ToDate", endDate);

                    rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                }
                Session["rptSource"] = null;
                Session["FromDate"] = null;
                Session["ToDate"] = null;
            }
        }
        //GET:Account/AccountingReport/PaymentSheet
        public JsonResult PaymentSheet(DateTime startDate, DateTime endDate)
        {
            Session["rptSource"] = _db.spCashOrBankPayment(startDate, endDate);
            Session["FromDate"] = startDate;
            Session["ToDate"] = endDate;

            var data = _db.spCashOrBankPayment(startDate, endDate).Count();
            return Json(data > 0, JsonRequestBehavior.AllowGet);
        }
        // GET: Account/AccountingReport/PaymentSheetReport
        public void PaymentSheetReport()
        {
            using (ReportClass rptH = new ReportClass())
            {
                rptH.FileName = Server.MapPath("~/Areas/Account/AccountingReport/rptCashOrBankPayment.rpt");
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                DateTime startDate = Convert.ToDateTime(Session["FromDate"]);
                DateTime endDate = Convert.ToDateTime(Session["ToDate"]);
                if (rptSource != null)
                {
                    rptH.Load();

                    rptH.SetDataSource(rptSource);
                    rptH.SetParameterValue("FromDate", startDate);
                    rptH.SetParameterValue("ToDate", endDate);

                    rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");

                }
                Session["rptSource"] = null;
                Session["FromDate"] = null;
                Session["ToDate"] = null;
            }
        }
    }
}