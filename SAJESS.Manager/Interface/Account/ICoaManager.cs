using System.Collections.Generic;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;

namespace SAJESS.Manager.Interface.Account
{
    public interface ICoaManager
    {
        List<A_GlAccountViewModel> GetCartOfAccount();

        ResponseModel FillParentPropertyUsingId(int id);
        ResponseModel CreateNode(A_GlAccount aObj);
    }
}
