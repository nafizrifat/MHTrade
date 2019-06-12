using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAJESS.Entities;

namespace SAJESS.Manager.Interface.Management
{
   public interface ISupplierManager
    {
       ResponseModel CreateSupplier(Supplier aObj, string currentUserId);
       ResponseModel GetAllSuppliers();
       ResponseModel SuppliersCombo();
       ResponseModel DistrictCombo();
    }
}
