using System.Web.Mvc;
using SAJESS.Manager;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class GeneralLedgerController : Controller
    {
        
        ICoaManager _aCOA;
        private IGeneralLedgerManager _aManager;
        private ResponseModel _aModel;
        public GeneralLedgerController()
        {
            _aCOA = new CoaManager();
            _aModel = new ResponseModel();
            _aManager=new GeneralLedgerManager();
        }
        // GET: Account/GeneralLedger
        public ActionResult GeneralLedgerSetting()
        {
            var items = _aCOA.GetCartOfAccount();
            return View(items);
        }

        public JsonResult GetLedgerData(int id)
        {
            var data = _aManager.GetLedgerData(id);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
           
        }
        //public JsonResult GetTotal(int id)
        //{
        //    var data = _aManager.GetTotal(id);
        //    return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
           
        //}
        public JsonResult GetTransactionAllowedLedgerHeads()
        {
            var data = _aManager.GetTransactionAllowedLedgerHeads();
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
    }
}