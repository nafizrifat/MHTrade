using System.Collections.Generic;
using SAJESS.Entities.ViewModel;

namespace SAJESS.Manager.Interface.Account
{
    public interface IJournalManager
    {
        ResponseModel A_GlAccountCombo();
        ResponseModel SaveJournalTransaction(List<ManualJournal> a_objList);

        ResponseModel Loadvoucher();
        ResponseModel SecondA_GlComboLoad();
    }
}
