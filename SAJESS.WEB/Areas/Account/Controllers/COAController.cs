using System.Web.Mvc;
using SAJESS.Entities;
using SAJESS.Manager;
using SAJESS.Manager.Interface.Account;
using SAJESS.Manager.Manager.Account;

namespace SAJESS.WEB.Areas.Account.Controllers
{
    public class COAController : Controller
    {
        ICoaManager _aCOA;
        private ResponseModel _aModel;
        public COAController()
        {
            _aCOA = new CoaManager();
            _aModel=new ResponseModel();
        }

        // GET: Account/COA/AccountTree
        public ActionResult AccountTree()
        {
            var items = _aCOA.GetCartOfAccount();
            return View(items);
        }
        // POST: Account/COA/CreateGlAccount
        //public ActionResult CreateGlAccount()
        //{
        //    return View();
        //}

        public JsonResult FillParentPropertyUsingId(int id)
        {
            var data = _aCOA.FillParentPropertyUsingId(id);
            return Json(new { data = data.Data }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateNode(A_GlAccount aObj)
        {
            var data = _aCOA.CreateNode(aObj);
            return Json(new { success = data.Status, data }, JsonRequestBehavior.AllowGet);
        }
    }
}