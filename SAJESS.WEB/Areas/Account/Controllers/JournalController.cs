using System.Collections.Generic;
using System.Web.Mvc;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{

    public class JournalController : Controller
    {
        IJournalManager _aMngr;
        public JournalController()
        {
            _aMngr = new JournalManager();
        }
        // GET: Account/Journal/LedgerCombo
        public JsonResult A_GlComboLoad()
        {
            var data = _aMngr.A_GlAccountCombo();
            return Json(new { success = "Success", result = data.Data }, JsonRequestBehavior.AllowGet);
        }
        // GET: Account/Journal/JournalPosting
        public ActionResult JournalPosting()
        {
            return View();
        }

        public JsonResult SaveTransaction(List<ManualJournal> journalList)
        {
            var data = _aMngr.SaveJournalTransaction(journalList);
            return Json(new { success = "Success", result = data.Data }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Loadvoucher()
        {
            var data = _aMngr.Loadvoucher();
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SecondA_GlComboLoad()
        {
            var data = _aMngr.SecondA_GlComboLoad();
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
    }
}