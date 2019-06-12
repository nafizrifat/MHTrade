using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SAJESS.Entities;
using SAJESS.Manager.Interface.DbManagement;
using SAJESS.Manager.Manager.DbManagement;
using SAJESS.Manager.Manager.Management;

namespace SAJESS.WEB.Areas.DbManagement.Controllers
{
  
    public class BackupRestoreController : Controller
    {
        private IBackupRestore _aManager;
        private Entities.Entities _db;
        public BackupRestoreController()
        {
            _db=new Entities.Entities();
            _aManager = new BackupRestoreManager();

        }
        // GET: DbManagement/BackupRestore/Index
        public ActionResult Index()
        {
            @ViewBag.BackupDatabse = "active";
            return View();
        }
        [Authorize(Roles = "sa, admin,accountant")]
        public ActionResult BackupWithoutFile()
        {
            string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var data = _aManager.BackupWithoutFile();
                return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Identity", new { area = "" });
            }

        }
        [Authorize(Roles = "sa, admin,accountant")]
        public ActionResult BackupWithFile(String fileLocation)
        {

            string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var data = _aManager.BackupWithoutFile();
                return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Identity", new { area = "" });
            }

        }

    }
}