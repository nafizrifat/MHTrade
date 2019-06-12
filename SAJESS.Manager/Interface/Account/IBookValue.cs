using System;
using SAJESS.Entities;

namespace SAJESS.Manager.Interface.Account
{
    public interface IBookValue
    {
        string GetFiscalYear();
        string GetPreviousFiscalYear();
        decimal GetOpeningBalance(string presentFiscalYear,int accountId);

        ResponseModel CreateFiscalYear(A_FiscalYear obj);
        ResponseModel SaveBookValue(DateTime start, DateTime end);
        ResponseModel ViewBookClosing();

    }
}
