using System;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Account;

namespace SAJESS.Manager.Manager.Account
{
    public class UtilityManager:IUtility
    {
        private ResponseModel _aModel;
        private Entities.Entities _db;

        public UtilityManager()
        {
            _aModel=new ResponseModel();
           _db=new Entities.Entities();
        }

        public string GetGetAccountType(Int32 Id)
        {
            IQueryable<A_GlAccount> _aGlAccount = _db.A_GlAccount.Where(i => i.A_GlAccountId == Id);
           return _aGlAccount.FirstOrDefault().AccountType;
        }
    }
}
