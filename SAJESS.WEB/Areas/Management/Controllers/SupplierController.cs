using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Management;
using SAJESS.Manager.Manager.Management;

namespace SAJESS.WEB.Areas.Management.Controllers
{
    public class SupplierController : Controller
    {
        private ISupplierManager _aManager;

        public SupplierController()
        {
            _aManager = new SupplierManager();
        }
        //
        // GET: /Management/Supplier/
        public ActionResult Suppliers()
        {
            @ViewBag.Suppliers = "active";
            return View();
        }
        public ActionResult CreateSupplier(Supplier aObj)
        {
            string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var data = _aManager.CreateSupplier(aObj, currentUserId);
                 return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
              //  return null;
            }
            return RedirectToAction("Login", "Identity", new { area = "" });
            // return RedirectToAction(/Identity/Login);
        }
        public JsonResult GetAllSuppliers()
        {
            var data = _aManager.GetAllSuppliers();
            return Json(new { data = data.Data  }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Management/Supplier/SuppliersCombo
        public JsonResult SuppliersCombo()
        {
            var data = _aManager.SuppliersCombo();
            return Json(new { success = "Success", result = data.Data }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Management/Supplier/DistrictCombo
        public JsonResult DistrictCombo()
        {
            var data = _aManager.DistrictCombo();
            return Json(new { success = "Success", result = data.Data }, JsonRequestBehavior.AllowGet);
        }
    }
	
}