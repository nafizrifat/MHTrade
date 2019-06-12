using System.Collections.Generic;
using System.Web.Mvc;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class ProcessJournalController : Controller
    {
        private IProcessJournalManager _aManager;

        public ProcessJournalController() 
        {
            _aManager=new ProcessJurnalManager();
        }
        // GET: Account/ProcessJournal
        public ActionResult ProcessJournalSetting()
        {
            return View();
        }

        public JsonResult GetAllCostCenterDd()
        {
            var data = _aManager.GetAllCostCenterDd();
            return Json(new {data = data.Data}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllUnprocessedData(int costCenterId)
        {
            var data = _aManager.GetAllUnprocessedData(costCenterId);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProcessUnprocessedData(List<UnprocessedData> unprocessedData, int costCenterId)
        {
            var data = _aManager.ProcessUnprocessedData(unprocessedData, costCenterId);
            return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReprocessedData()
        {
            var data = _aManager.ReprocessedData();
            return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
        }
    }
}