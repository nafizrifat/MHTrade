using System;

namespace SAJESS.Manager.Interface.Account
{
   public interface ICashReceiveStatementManager
   {
       ResponseModel GetACashReceiveStatemtByDate(DateTime fromDate, DateTime toDate);
   }
}
