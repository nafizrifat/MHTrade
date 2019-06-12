using System;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;

namespace SAJESS.Manager.Interface.Management
{
  public  interface ISupplierInvestmentManager
    {
      ResponseModel CreatSupplierInvestment(SupplierInvestment aObj, string currentUserId);
      ResponseModel GetAllSupplierInvestment(DateObj dateObj);
      ResponseModel GetAllSupplierInvestmentBySupplierId(int supplierId);
      ResponseModel DeleteSupplierInvestment(SupplierInvestment aObj, String currentUserId);
    }
}
