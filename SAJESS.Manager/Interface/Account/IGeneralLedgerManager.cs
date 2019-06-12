namespace SAJESS.Manager.Interface.Account
{
  public  interface IGeneralLedgerManager
    {
       ResponseModel GetLedgerData(int id);
       // ResponseModel GetTotal(int id);
        ResponseModel GetTransactionAllowedLedgerHeads();
    }
}
