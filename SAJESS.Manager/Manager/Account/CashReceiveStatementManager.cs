using System;
using SAJESS.Manager.Interface.Account;

namespace SAJESS.Manager.Manager.Account
{
  public  class CashReceiveStatementManager: ICashReceiveStatementManager
  {
      private Entities.Entities _db;
      private ResponseModel _aModel;

      public CashReceiveStatementManager()
      {
          _db=new Entities.Entities();
           _aModel=new ResponseModel();
      }
      public ResponseModel GetACashReceiveStatemtByDate(DateTime startDate, DateTime endDate)
      {
            var data = _db.spCashReceiveStatement(startDate, endDate);
            return _aModel.Respons(data);

      }
    }
}
