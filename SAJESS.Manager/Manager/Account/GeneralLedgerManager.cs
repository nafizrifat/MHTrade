using System.Linq;
using SAJESS.Manager.Interface.Account;

namespace SAJESS.Manager.Manager.Account
{
    public class GeneralLedgerManager : IGeneralLedgerManager
    {
        private ResponseModel _aModel;
        private Entities.Entities _db;
        public GeneralLedgerManager()
        {
            _aModel = new ResponseModel();
            _db = new Entities.Entities();
        }
        public ResponseModel GetLedgerData(int parentId)
        {
            var data = (from p in _db.spGeneralLedgerReport(parentId)
                        select new
                        {
                            Name = p.Name,
                            Debit = p.Debit,
                            Credit = p.Credit,
                            OpeningAmount = 0,
                            ClosingAmount = 0
                        }).ToList();
            return _aModel.Respons(data);

        }
        //public ResponseModel GetTotal(int aParentId)
        //{
        //    var data = (from t in _db.spGLTotal(aParentId, DateTime.Now)
        //                select new
        //                {
        //                    Debit = t.TotalDebit,
        //                    Credit = t.TotalCredit
        //                }).ToList();


        //    return _aModel.Respons(data);

        //}

        public ResponseModel GetTransactionAllowedLedgerHeads()
        {
            var data = (from h in _db.A_GlAccount
                        where h.IsActive == true && h.TransactionAllowed == true
                        select new
                        {
                            id = h.A_GlAccountId,
                            text = h.Name + "-" + h.Code
                        }).ToList();
            return _aModel.Respons(data);
        }
    }
}
