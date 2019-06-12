using SAJESS.Entities;

namespace SAJESS.Manager.Interface.Account
{
    public interface IFiscalYearConfig
    {
        ResponseModel CreateFiscalYear(A_FiscalYear  aObj);
        ResponseModel GetAllFiscalYear();
        ResponseModel DeleteFiscalYear(int yearId);
        ResponseModel IsBooked(int yearID);
    }
}
