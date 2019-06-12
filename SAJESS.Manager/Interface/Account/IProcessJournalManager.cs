using System.Collections.Generic;
using SAJESS.Entities.ViewModel;

namespace SAJESS.Manager.Interface.Account
{
   public interface IProcessJournalManager
   {
       ResponseModel GetAllCostCenterDd();
        ResponseModel GetAllUnprocessedData(int costCenterId);
        ResponseModel ProcessUnprocessedData(List<UnprocessedData> datas, int costCenterId);
       ResponseModel ReprocessedData();
   }
}
