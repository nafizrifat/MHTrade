using System;
using System.Collections.Generic;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;
using SAJESS.Repositories;

namespace SAJESS.Manager.Manager.Account
{
    public class JournalManager : IJournalManager
    {
        private Entities.Entities _db;
        private GenericRepositoryAccount<A_GlAccount> _aGlAccount;
        private GenericRepositoryAccount<A_GlTransaction> _aGlTransaction;
        private GenericRepositoryAccount<A_GlTransactionDetails> _aGlTranDetails;
        private ResponseModel _aModel;

        public JournalManager()
        {
            _db = new Entities.Entities();
            _aModel = new ResponseModel();
            _aGlAccount = new GenericRepositoryAccount<A_GlAccount>();
            _aGlTransaction = new GenericRepositoryAccount<A_GlTransaction>();
            _aGlTranDetails = new GenericRepositoryAccount<A_GlTransactionDetails>();

        }
        public ResponseModel A_GlAccountCombo()
        {
            var data = _aGlAccount.SelectAll().Where(gl => gl.TransactionAllowed == true).Select(a => new
            {
                id = a.A_GlAccountId,
                text = a.Name + "(" + a.Code + ")"
            }).ToList();

            //var aaa = data.ToList();
            return _aModel.Respons(data);
        }

        public ResponseModel SaveJournalTransaction(List<ManualJournal> a_objList)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var vouchers = a_objList.Select(x => x.VoucharNumber).ToList().Distinct();
                    foreach (Int32 v in vouchers)
                    {
                        var d = (from e in a_objList
                            where e.VoucharNumber == v
                            select
                            new
                            {
                                e.TransactionDate,
                                e.Particulars
                            }).FirstOrDefault();
                        A_GlTransaction aGlTransaction = new A_GlTransaction()
                        {
                            TransactionDate = d.TransactionDate,
                            EntryMethod = 1,
                            VoucharNumber = v,
                            Description = d.Particulars

                        };
                        _db.A_GlTransaction.Add(aGlTransaction);
                        _db.SaveChanges();
                        List<ManualJournal> manualJournals = (from o in a_objList
                                                              where o.VoucharNumber == v
                                                              select o).ToList();

                        foreach (var o in manualJournals)
                        {
                            A_GlTransactionDetails aGlTransactionDetails = new A_GlTransactionDetails()
                            {
                                A_GlAccountId = o.A_GlAccountId,
                                A_GlTransactionId = aGlTransaction.A_GlTransactionId,
                                DebitAmount = o.DebitAmount,
                                CreditAmount = o.CreditAmount,
                                TransactionDate = o.TransactionDate,
                                ChequeNumber = o.ChequeNumber

                            };
                            _db.A_GlTransactionDetails.Add(aGlTransactionDetails);
                            _db.SaveChanges();
                        }



                    }
                    transaction.Commit();
                    return _aModel.Respons(true, "Journal Saved");

                }
                catch (Exception)
                {
                    
                    transaction.Rollback();
                    return _aModel.Respons(false, "Journal not saved");
                }
            }
        }

        public ResponseModel Loadvoucher()
        {
            //int data = (from j in _db.A_GlTransaction
            //    select j.VoucharNumber).Count();
            // var aVoucher = (data + 1).ToString();
            //return _aModel.Respons(aVoucher);
            var maxValue = _db.A_GlTransaction.Max(x => x.VoucharNumber);
            if (maxValue == null)
            {
                maxValue = 1;
                return _aModel.Respons(maxValue);
            }
            else
            {
                return _aModel.Respons(maxValue + 1);

            }
        }

        public ResponseModel SecondA_GlComboLoad()
        {
            var data = (from a in _db.A_GlAccount
                where a.TransactionAllowed == true && a.IsActive == true
                select new
                {
                    id = a.A_GlAccountId,
                    text = a.Name + "(" + a.Code+")"
                }).ToList();
            return _aModel.Respons(data);
        }
    }
}
