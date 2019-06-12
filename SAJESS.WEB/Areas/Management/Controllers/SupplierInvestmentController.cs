using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager;
using SAJESS.Manager.Interface;
using SAJESS.Manager.Interface.Management;
using SAJESS.Manager.Manager.Management;

namespace SAJESS.WEB.Areas.Management.Controllers
{
    public class SupplierInvestmentController : Controller
    {
        private ISupplierInvestmentManager _aManager;

        public SupplierInvestmentController()
        {
            _aManager = new SupplierInvestmentManager();

        }
        //
        // GET: /Management/SupplierInvestment/SupplierInvestmentSettings
        public ActionResult SupplierInvestmentSettings()
        {
            @ViewBag.SupplierInvestmentSettings = "active";
            return View();
        }
        // GET: /Management/SupplierInvestment/CreatSupplierInvestment
        public ActionResult CreatSupplierInvestment(SupplierInvestment aObj)
        {
            string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var data = _aManager.CreatSupplierInvestment(aObj, currentUserId);
                return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Identity", new { area = "" });
            }

        }
        // GET: /Management/SupplierInvestment/DeleteSupplierInvestment
        public ActionResult DeleteSupplierInvestment(SupplierInvestment aObj)
        {
            string currentUserId = User.Identity.GetUserId();
            bool isAdmin = User.IsInRole("admin");

            if (currentUserId != null && isAdmin)
            {
                var data = _aManager.DeleteSupplierInvestment(aObj, currentUserId);
                return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ResponseModel data = new ResponseModel();
                data.Status = false;
                data.Message = "You are not authorized to DELETE.";
                return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);

               // return RedirectToAction("Login", "Identity", new { area = "" });
            }

        }
        // GET: /Management/SupplierInvestment/GetAllSupplierInvestment
        public JsonResult GetAllSupplierInvestment(DateObj dateObj)
        // public JsonResult GetAllSupplierInvestment()
        {
            var data = _aManager.GetAllSupplierInvestment(dateObj);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Management/SupplierInvestment/ReviseSupplierInvestment
       public ActionResult ReviseSupplierInvestment()
        {
            try
            {
                @ViewBag.ReviseSupplierInvestment = "active";
                bool isAdmin = User.IsInRole("admin");
                if (isAdmin)
                    @ViewBag.DeleteButtonDisplay = "";
                else
                {
                    @ViewBag.DeleteButtonDisplay = "none";
                }
                return View();
            }
            catch (Exception cx)
            {
                
                throw;
            }
        }
        // GET: /Management/SupplierInvestment/GetAllSupplierInvestmentBySupplierId
        public JsonResult GetAllSupplierInvestmentBySupplierId(int supplierId)
        {
            var data = _aManager.GetAllSupplierInvestmentBySupplierId(supplierId);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);

        }

    }



}