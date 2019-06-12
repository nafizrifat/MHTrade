using System;
using System.Collections.Generic;
using SAJESS.Entities.ViewModel;

namespace SAJESS.Manager.Interface.Account
{
    public interface ICashOrBankReceivePaymentManager
    {
        ResponseModel LoadAllAccountHead(int type);
        ResponseModel LoadOtherHeads(int id);
        ResponseModel SaveReceiveTransaction(List<ManualJournal> data,int cashOrBankHead, Int32 voucherNo, DateTime transactionDate, string particular);
        ResponseModel SavePaymentTransaction(List<ManualJournal> data, int cashOrBankHead, Int32 voucherNo, DateTime transactionDate, string particular);
    }
}
