using System;
using System.Collections.Generic;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;

namespace SAJESS.Manager.Manager.Account
{
    public class CashOrBankReceivePaymentManager : ICashOrBankReceivePaymentManager
    {
        private Entities.Entities _db;
        private ResponseModel _aModel;
        private UtilityManager _uMng ;

        public CashOrBankReceivePaymentManager()
        {
            _db = new Entities.Entities();
            _aModel = new ResponseModel();
            _uMng=new UtilityManager();
        }
        public ResponseModel LoadAllAccountHead(int parentId)
        {
            var accounts = new Object();
            if (parentId == 1)
            {
                accounts = (from h in _db.A_GlAccount
                            where h.TransactionAllowed == true && h.Name.Contains("Cash") && h.AccountType=="A"
                            select new
                            {
                                id = h.A_GlAccountId,
                                text = h.Name + " (" + h.Code + ")"
                            }).ToList();

            }else if(parentId == 2)
            { 
            accounts = (from h in _db.A_GlAccount
                        where h.TransactionAllowed == true && h.Name.Contains("Bank") && h.AccountType == "A"
                        select new
                        {
                            id = h.A_GlAccountId,
                            text = h.Name + " (" + h.Code + ")"
                        }).ToList();
            }
            return _aModel.Respons(accounts);
        }

        public ResponseModel LoadOtherHeads(int id)
        {
            var data = (from oh in _db.A_GlAccount
                        where oh.TransactionAllowed == true && oh.A_GlAccountId != id
                        select new
                        {
                            id = oh.A_GlAccountId,
                            text = oh.Name + " (" + oh.Code + ")"
                        }).ToList();
            return _aModel.Respons(data);
        }

        public ResponseModel SaveReceiveTransaction(List<ManualJournal> data, int cashOrBankHead, Int32 voucherNo, DateTime transactionDate, string particular)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    long aGlTransactionId = 0;
                    decimal total = 0;
                    if (data.Any())
                    {
                        A_GlTransaction aGlTransaction = new A_GlTransaction()
                        {
                            Description = particular,
                            EntryMethod = 2,
                            TransactionDate = transactionDate,
                            VoucharNumber = voucherNo
                        };
                        _db.A_GlTransaction.Add(aGlTransaction);
                        _db.SaveChanges();
                        aGlTransactionId = aGlTransaction.A_GlTransactionId;
                    }
                    if (aGlTransactionId > 0)
                    {
                        foreach (ManualJournal t in data)
                        {
                            var aGlTransactionDetails = new A_GlTransactionDetails()
                            {
                                A_GlAccountId = t.A_GlAccountId,
                                A_GlTransactionId = aGlTransactionId,
                                DebitAmount = 0,
                                CreditAmount = t.CreditAmount,
                                Particular =t.Particulars,
                                TransactionDate = t.TransactionDate,
                                ChequeNumber = t.ChequeNumber
                            };
                            total = total + t.CreditAmount;
                            _db.A_GlTransactionDetails.Add(aGlTransactionDetails);

                        }
                        _db.SaveChanges();
                        var aGlDetailsCashBank = new A_GlTransactionDetails()
                        {
                            A_GlAccountId = cashOrBankHead,
                            A_GlTransactionId = aGlTransactionId,
                            DebitAmount = total,
                            CreditAmount = 0,
                            Particular = particular,
                            TransactionDate = transactionDate
                        };
                        _db.A_GlTransactionDetails.Add(aGlDetailsCashBank);
                        _db.SaveChanges();
                    }

                    transaction.Commit();
                    return _aModel.Respons(true, " Transaction saved successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return _aModel.Respons(false, "Sorry ! An error occur ."+ ex);
                }
            }
        }

   
        public ResponseModel SavePaymentTransaction(List<ManualJournal> data, int cashOrBankHead, Int32 voucherNo, DateTime transactionDate, string particular)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    long aGlTransactionId = 0;
                    decimal total = 0;
                    if (data.Any())
                    {
                        A_GlTransaction aGlTransaction = new A_GlTransaction()
                        {
                            Description = particular,
                            EntryMethod = 2,
                            TransactionDate = transactionDate,
                            VoucharNumber = voucherNo
                        };
                        _db.A_GlTransaction.Add(aGlTransaction);
                        _db.SaveChanges();
                        aGlTransactionId = aGlTransaction.A_GlTransactionId;
                    }
                    if (aGlTransactionId > 0)
                    {

                        foreach (ManualJournal t in data)
                        {
                            String accountType = _uMng.GetGetAccountType(t.A_GlAccountId);
                            if (accountType == "A" || accountType == "E")
                            {
                                var aGlTransactionDetails = new A_GlTransactionDetails()
                                {
                                    A_GlAccountId = t.A_GlAccountId,
                                    A_GlTransactionId = aGlTransactionId,
                                    DebitAmount = t.CreditAmount,
                                    CreditAmount = 0,
                                    Particular = t.Particulars,
                                    TransactionDate = t.TransactionDate,
                                    ChequeNumber = t.ChequeNumber
                                };
                                total = total + t.CreditAmount;
                                _db.A_GlTransactionDetails.Add(aGlTransactionDetails);
                            }
                            else
                            {
                                var aGlTransactionDetails = new A_GlTransactionDetails()
                                {
                                    A_GlAccountId = t.A_GlAccountId,
                                    A_GlTransactionId = aGlTransactionId,
                                    DebitAmount = 0,
                                    CreditAmount = t.CreditAmount,
                                    Particular = particular,
                                    TransactionDate = t.TransactionDate,
                                    ChequeNumber = t.ChequeNumber
                                };
                                total = total + t.CreditAmount;
                                _db.A_GlTransactionDetails.Add(aGlTransactionDetails);
                            }
                        }

                    }
                    _db.SaveChanges();
                    var aGlDetailsCashBank = new A_GlTransactionDetails()
                    {
                        A_GlAccountId = cashOrBankHead,
                        A_GlTransactionId = aGlTransactionId,
                        DebitAmount = 0,
                        CreditAmount = total,
                        TransactionDate = transactionDate
                    };
                    _db.A_GlTransactionDetails.Add(aGlDetailsCashBank);
                    _db.SaveChanges();
                    transaction.Commit();
                    return _aModel.Respons(true, " Transaction saved successfully");
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    return _aModel.Respons(false, "Sorry ! An error occur ."+ ex);
                }
            }
        }
    }
}
