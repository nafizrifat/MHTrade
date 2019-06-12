using SAJESS.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SAJESS.Entities;

namespace SAJESS.WEB.Areas.Management.Controllers
{
    public class ManagementReportController : Controller
    {
        private SAJESS.Entities.Entities _db = null;
        public ManagementReportController()
        {
            _db = new SAJESS.Entities.Entities();
        }
        //
        // GET: /Management/ManagementReport/SuppliersInvestmentSummary
        public ActionResult SuppliersInvestmentSummary()
        {
            @ViewBag.SuppliersInvestmentSummary = "active";
            return View();
        }

        // GET: /Management/ManagementReport/ManagementReport/ReportManagementReportSummaryData
        public JsonResult ReportManagementReportSummaryData(ManagementReportModel aObj)
        {
            Session["rptSource"] = _db.sp_SupplierWiseInvestment(aObj.SupplierId, DateTime.ParseExact(aObj.FromDate, "dd/MM/yyyy", null), DateTime.ParseExact(aObj.ToDate, "dd/MM/yyyy", null)).ToList();
            int data = _db.sp_SupplierWiseInvestment(aObj.SupplierId, DateTime.ParseExact(aObj.FromDate, "dd/MM/yyyy", null), DateTime.ParseExact(aObj.ToDate, "dd/MM/yyyy", null)).ToList().Count();
            return Json(data > 0, JsonRequestBehavior.AllowGet);
        }
        // GET: /Management/Reports/ReportManagementReportSummaryReport
        public void ReportManagementReportSummaryReport()
        {
            using (ReportClass rptH = new ReportClass())
            {
                rptH.FileName = Server.MapPath("~/Areas/Management/Reports/rptSupplierWiseInvestment.rpt");
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                if (rptSource != null)
                {
                    rptH.Load();
                    rptH.SetDataSource(rptSource);
                    rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "SupplierWiseInvestmentReport_"+System.DateTime.Now.ToString("ddmmyyyy"));

                }
                Session["rptSource"] = null;
            }
        }


    }
}