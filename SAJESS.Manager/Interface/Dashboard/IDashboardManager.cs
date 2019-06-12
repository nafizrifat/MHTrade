using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAJESS.Manager.Interface.Dashboard
{
 public   interface IDashboardManager
    {
     ResponseModel GetTotalByDateDashboard();
     ResponseModel GetSupplierWiseTotalPaymentDashboard();
    }
}
