using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;
using SAJESS.Repositories;


namespace SAJESS.Manager.Manager.Account
{
    public class CoaManager : ICoaManager
    {
        private Entities.Entities _db;
        private GenericRepositoryAccount<A_GlAccount> _aGlAccount;
        private A_GlAccountViewModel _coaVM;
        private ResponseModel _aModel;

        public CoaManager()
        {
            _aGlAccount = new GenericRepositoryAccount<A_GlAccount>();
            _coaVM = new A_GlAccountViewModel();
            _aModel = new ResponseModel();
            _db=new Entities.Entities();
        }
        List<A_GlAccountViewModel> a_GlList = new List<A_GlAccountViewModel>();
        public ResponseModel GetGlCombo()
        {

            var data = _aGlAccount.SelectAll().Where(x => x.ParentId != null).Select(a => new
            {
                id = a.A_GlAccountId,
                text = a.Name + "(" + a.Code + ")"
            }).ToList();

            var aaa = data.ToList();
            return _aModel.Respons(data);
        }
        public List<A_GlAccountViewModel> GetCartOfAccount()
        {
            List<A_GlAccount> SuperParrentAccount = _aGlAccount.SelectAll().Where(x => x.ParentId==null).Select(a => new A_GlAccount
            {
                A_GlAccountId = a.A_GlAccountId,
                ParentId = a.ParentId,
                Code=a.Code,
                Name = a.Name + "(" + a.Code + ")",
                TransactionAllowed = a.TransactionAllowed,
                IsActive = a.IsActive
            }).ToList();
            foreach (var aGlAccount in SuperParrentAccount)
            {
                _coaVM = new A_GlAccountViewModel();
                _coaVM.Id = aGlAccount.A_GlAccountId;
                _coaVM.Code = aGlAccount.Code;
                _coaVM.Parent = Convert.ToInt32(aGlAccount.ParentId);
                _coaVM.AccountName = aGlAccount.Name;
                _coaVM.transactionAllowed = aGlAccount.TransactionAllowed;
                a_GlList.Add(_coaVM);
                GetChild(aGlAccount.A_GlAccountId);
            }
            return (a_GlList);
        }

        public ResponseModel FillParentPropertyUsingId(int id)
        {
            string codeOfLastChild = (from ac in _db.A_GlAccount
                                         orderby ac.A_GlAccountId descending
                                         where ac.ParentId == id
                                         select ac.Code).FirstOrDefault();

            string codeOfNewChild = (codeOfLastChild != null) ? (Convert.ToInt32(codeOfLastChild) + 1).ToString():null;
            var data = (from ac in _db.A_GlAccount
                       where ac.A_GlAccountId == id
                select new
                {
                   Name= ac.Name,
                   Code= ac.Code,
                   ParentId=ac.A_GlAccountId,
                   CodeOfNewChild = codeOfNewChild ?? ac.Code + "001"

        }).FirstOrDefault();
            return _aModel.Respons(data);
        }

        public ResponseModel CreateNode(A_GlAccount aObj)
        {
            if(aObj!=null)
            {
                A_GlAccount nameEsist = _db.A_GlAccount.FirstOrDefault(x => x.Name == aObj.Name);
                if (nameEsist != null)
                {
                    return _aModel.Respons(false, "Given Name Exists In The Chart Of Account");
                }
            }
            try
            {
                A_GlAccount aParent = _db.A_GlAccount.FirstOrDefault(x => x.A_GlAccountId == aObj.ParentId);
                if (aParent != null)
                {
                    aParent.TransactionAllowed = false;
                    _db.A_GlAccount.Attach(aParent);
                    _db.Entry(aParent).State = EntityState.Modified;
                    aObj.TransactionAllowed = true;
                    aObj.CreatedDate = DateTime.Now;
                    aObj.AccountType = aParent.AccountType;
                    aObj.BalanceType = aParent.BalanceType;
                }
                aObj.IsActive = true;
                aObj.IsDeleted = false;
                _db.A_GlAccount.Add(aObj);
                _db.SaveChanges();
                return _aModel.Respons(true," Congratulation : A new node created successfully");
            }
            catch (Exception ex)
            {

                return _aModel.Respons(false, ex.Message);
            }
           
        }

        private void GetChild(int parrentId)
        {
            var ChildAsParrentAccount = _aGlAccount.SelectAll().Where(x => x.ParentId == parrentId)
                .Select(a => new A_GlAccount
            {
                A_GlAccountId = a.A_GlAccountId,
                ParentId = a.ParentId,
                Name = a.Name + "(" + a.Code.ToString() + ")",
                TransactionAllowed = a.TransactionAllowed,
                IsActive = a.IsActive
            }).ToList();

            foreach (var coa in ChildAsParrentAccount)
            {
                _coaVM = new A_GlAccountViewModel();
                _coaVM.Id = coa.A_GlAccountId;
                _coaVM.Code = coa.Code;
                _coaVM.Parent = Convert.ToInt32(coa.ParentId);
                _coaVM.AccountName = coa.Name;
                _coaVM.transactionAllowed = coa.TransactionAllowed;
                if (_coaVM.transactionAllowed)
                {
                    _coaVM.Children = true;
                }
                else
                {
                    _coaVM.Children = false;
                }
                a_GlList.Add(_coaVM);
                GetChild(coa.A_GlAccountId);
            }
        }
    }
}
