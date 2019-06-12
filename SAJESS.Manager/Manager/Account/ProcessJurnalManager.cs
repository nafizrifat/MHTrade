using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface.Account;

namespace SAJESS.Manager.Manager.Account
{
    public class ProcessJurnalManager : IProcessJournalManager
    {
        private ResponseModel _aModel;
        private Entities.Entities _db;

        public ProcessJurnalManager()
        {
            _aModel = new ResponseModel();
            _db = new Entities.Entities();
        }

        public ResponseModel GetAllCostCenterDd()
        {
            var data = (from cc in _db.A_CostCenter
                        select new
                        {
                            id = cc.A_CostCenterId,
                            text = cc.CostCenterName
                        }).Distinct().ToList();
            return _aModel.Respons(data);
        }

        public ResponseModel GetAllUnprocessedData(int costCenterId)
        {

            if (costCenterId == 1)
            {
                var data = (from tr in _db.SupplierInvestments
                            where tr.ProcessStatus == 1
                            select new
                            {
                                Id = tr.SupplierInvestmentId,
                                Particular = tr.Particular,
                                Amount = tr.Amount,
                                A_GlTransactionId = tr.A_GlTransactionId,
                                ProcessStatus = tr.ProcessStatus,

                            }).ToList();
                return _aModel.Respons(data);
            }
            //if (costCenterId == 2)
            //{
            //    var data = (from es in _db.Employee_Salary
            //                where es.ProcessStatus == 1
            //                select new
            //                {
            //                    Id = es.Employee_SalaryId,
            //                    Particulars = "Employee Monthly Salary",
            //                    Amount = es.TotalDisburse,
            //                    ProcessStatus = es.ProcessStatus

            //                }).ToList();
            //    return _aModel.Respons(data);
            //}
            //if (costCenterId == 3)
            //{
            //    var data = (from bill in _db.Shop_Meter_Reading_Bill
            //                where bill.ProcessStatus == 1
            //                select new
            //                {
            //                    Id = bill.BillId,
            //                    Particulars = "Electric Bill",
            //                    Amount = bill.SubTotalBill,
            //                    ProcessStatus = bill.ProcessStatus

            //                }).ToList();
            //    return _aModel.Respons(data);
            //}
            //if (costCenterId == 4)
            //{
            //    var data = (from ss in _db.Shop_ServiceCharge
            //                where ss.ProcessStatus == 1
            //                select new
            //                {
            //                    Id = ss.Id,
            //                    Particulars = "Shop Service Charge",
            //                    Amount = ss.TotalBill,
            //                    ProcessStatus = ss.ProcessStatus

            //                }).ToList();
            //    return _aModel.Respons(data);
            //}
            return null;
        }

        public ResponseModel ProcessUnprocessedData(List<UnprocessedData> unprocessedData, int costCenterId)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    string debtAccountCode = null;
                    string creditAccountCode = null;
                    if (costCenterId == 1)
                    {
                        foreach (var data in unprocessedData)
                        {
                            debtAccountCode = _db.A_CostCenter.SingleOrDefault(x => x.A_CostCenterId == costCenterId && x.Particular == data.Particular).DebitAccount.ToString();
                            creditAccountCode = _db.A_CostCenter.SingleOrDefault(x => x.A_CostCenterId == costCenterId && x.Particular == data.Particular).CreditAccount.ToString();
                            if (String.IsNullOrWhiteSpace(debtAccountCode) || String.IsNullOrWhiteSpace(creditAccountCode))
                            {
                                return _aModel.Respons(false, "Debit or credit account in Cost Center Not Found");
                            }
                            else
                            {
                                if (data.A_GLTransactionId > 0)
                                {
                                    Int32 A_GlAccountId=0;
                                    A_GlAccountId = _db.A_GlAccount.SingleOrDefault(x => x.Code == debtAccountCode).A_GlAccountId;

                                    A_GlTransactionDetails aGlTransactionDetailsDebit = _db.A_GlTransactionDetails.SingleOrDefault(i => i.A_GlTransactionId == data.A_GLTransactionId && i.A_GlAccountId== A_GlAccountId);
                                    aGlTransactionDetailsDebit.DebitAmount = data.Amount;
                                    _db.A_GlTransactionDetails.Attach(aGlTransactionDetailsDebit);
                                    _db.Entry(aGlTransactionDetailsDebit).State = EntityState.Modified;
                                    _db.SaveChanges();

                                    A_GlAccountId = _db.A_GlAccount.SingleOrDefault(x => x.Code == creditAccountCode).A_GlAccountId;

                                    A_GlTransactionDetails aGlTransactionDetailsCredit = _db.A_GlTransactionDetails.SingleOrDefault(i => i.A_GlTransactionId == data.A_GLTransactionId && i.A_GlAccountId == A_GlAccountId);
                                    aGlTransactionDetailsCredit.CreditAmount = data.Amount;
                                    _db.A_GlTransactionDetails.Attach(aGlTransactionDetailsCredit);
                                    _db.Entry(aGlTransactionDetailsCredit).State = EntityState.Modified;
                                    _db.SaveChanges();
                                    SupplierInvestment aInvestment = _db.SupplierInvestments.SingleOrDefault(x => x.SupplierInvestmentId == data.Id);
                                    if (aInvestment != null)
                                    {
                                        aInvestment.ProcessStatus = 2;
                                        aInvestment.A_GlTransactionId = data.A_GLTransactionId;
                                        _db.SupplierInvestments.Attach(aInvestment);
                                        _db.Entry(aInvestment).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    A_GlTransaction aGlTransaction = new A_GlTransaction()
                                    {

                                        TransactionDate = DateTime.Now,
                                        EntryMethod = 2,
                                        Description = data.Particular,
                                        A_CostCenterId = costCenterId
                                    };
                                    _db.A_GlTransaction.Add(aGlTransaction);
                                    _db.SaveChanges();
                                    A_GlTransactionDetails aGlTransactionDetailsforDebt = new A_GlTransactionDetails()
                                    {
                                        A_GlAccountId = _db.A_GlAccount.SingleOrDefault(x => x.Code == debtAccountCode).A_GlAccountId,
                                        A_GlTransactionId = aGlTransaction.A_GlTransactionId,
                                        DebitAmount = data.Amount,
                                        CreditAmount = 0,
                                        TransactionDate = DateTime.Now,
                                        Particular = data.Particular
                                    };
                                    _db.A_GlTransactionDetails.Add(aGlTransactionDetailsforDebt);
                                    _db.SaveChanges();
                                    A_GlTransactionDetails aGlTransactionDetailsforCredit = new A_GlTransactionDetails()
                                    {
                                        A_GlAccountId = _db.A_GlAccount.SingleOrDefault(x => x.Code == creditAccountCode).A_GlAccountId,
                                        A_GlTransactionId = aGlTransaction.A_GlTransactionId,
                                        DebitAmount = 0,
                                        CreditAmount = data.Amount,
                                        TransactionDate = DateTime.Now,
                                        Particular = data.Particular
                                    };
                                    _db.A_GlTransactionDetails.Add(aGlTransactionDetailsforCredit);
                                    _db.SaveChanges();
                                    SupplierInvestment aInvestment = _db.SupplierInvestments.SingleOrDefault(x => x.SupplierInvestmentId == data.Id);
                                    if (aInvestment != null)
                                    {
                                        aInvestment.ProcessStatus = 2;
                                        aInvestment.A_GlTransactionId = aGlTransaction.A_GlTransactionId;

                                        _db.SupplierInvestments.Attach(aInvestment);
                                        _db.Entry(aInvestment).State = EntityState.Modified;
                                        _db.SaveChanges();
                                    }
                                }
                            }
                        }

                    }
                   
                    transaction.Commit();
                    return _aModel.Respons(true, "Data processing done successfully");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return _aModel.Respons(false, "Sorry ! Some error occured");
                }
            }

        }

        public ResponseModel ReprocessedData()
        {
            List<A_CostCenter> costCenterList = _db.A_CostCenter.ToList();
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_db).ObjectContext;
            objCtx.ExecuteStoreCommand("SELECT * INTO #Temp_A_GlTransaction FROM A_GlTransaction Where EntryMethod=1 AND A_CostCenterId IS NULL ;" +
                                       "SELECT * INTO #Temp_A_GlTransactionDetails FROM A_GlTransactionDetails Where A_GlTransactionId in (SELECT A_GlTransactionId FROM #Temp_A_GlTransaction);" +
                                       "TRUNCATE TABLE A_GlTransactionDetails;DELETE FROM A_GlTransaction;" +
                                       "DBCC CHECKIDENT('A_GlTransaction',RESEED,0);" +
                                       "INSERT INTO A_GlTransaction SELECT TransactionDate,EntryMethod,Description,A_CostCenterId,VoucharNumber FROM #Temp_A_GlTransaction;" +
                                       "INSERT INTO A_GlTransactionDetails  SELECT A_GlAccountId,A_GlTransactionId,DebitAmount,CreditAmount,TransactionDate,ChequeNumber,Particular FROM #Temp_A_GlTransactionDetails;" +
                                       "DROP TABLE #Temp_A_GlTransaction,#Temp_A_GlTransactionDetails;");

            foreach (var aCostCenter in costCenterList)
            {
                switch (aCostCenter.A_CostCenterId)
                {
                    case 1:
                        if (aCostCenter.Particular == "Supplier Investment")
                        {
                            var supplierInvestMentList = _db.SupplierInvestments.ToList();
                            if (supplierInvestMentList.Count == 0)
                            {
                                return _aModel.Respons(false, "Debit or credit account in Cost Center Not Found");
                            }
                            var costCenterAH = _db.A_CostCenter.SingleOrDefault(x => x.A_CostCenterId == 1 && x.Particular == "Supplier Investment");
                            if (costCenterAH != null)
                            {
                                string debtAccountCode = costCenterAH.DebitAccount.ToString();
                                string creditAccountCode = costCenterAH.CreditAccount.ToString();
                                foreach (var supplierInvestment in supplierInvestMentList)
                                {
                                    using (var transaction = _db.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            A_GlTransaction aGlTransaction = new A_GlTransaction()
                                            {
                                                TransactionDate = supplierInvestment.TransactionDate,
                                                EntryMethod = 2,
                                                Description = supplierInvestment.Particular,
                                                A_CostCenterId = 1
                                            };
                                            _db.A_GlTransaction.Add(aGlTransaction);
                                            _db.SaveChanges();
                                            A_GlTransactionDetails aGlTransactionDetailsforDebt = new A_GlTransactionDetails()
                                            {
                                                A_GlAccountId = _db.A_GlAccount.SingleOrDefault(x => x.Code == debtAccountCode).A_GlAccountId,
                                                A_GlTransactionId = aGlTransaction.A_GlTransactionId,
                                                DebitAmount = supplierInvestment.Amount,
                                                CreditAmount = 0,
                                                TransactionDate = supplierInvestment.TransactionDate,
                                            };
                                            _db.A_GlTransactionDetails.Add(aGlTransactionDetailsforDebt);
                                            _db.SaveChanges();
                                            A_GlTransactionDetails aGlTransactionDetailsforCredit = new A_GlTransactionDetails()
                                            {
                                                A_GlAccountId = _db.A_GlAccount.SingleOrDefault(x => x.Code == creditAccountCode).A_GlAccountId,
                                                A_GlTransactionId = aGlTransaction.A_GlTransactionId,
                                                DebitAmount = 0,
                                                CreditAmount = supplierInvestment.Amount,
                                                TransactionDate = supplierInvestment.TransactionDate,
                                            };
                                            _db.A_GlTransactionDetails.Add(aGlTransactionDetailsforCredit);
                                            _db.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            transaction.Rollback();
                                            return _aModel.Respons(false, "Sorry ! Some error occured");
                                        }
                                        transaction.Commit();
                                    }
                                }
                                
                                return _aModel.Respons(true, "Data processing done successfully");
                            }

                            break;
                        }
                        break;
                    case 2:

                        break;
                    default:

                        break;
                }

            }
            return _aModel.Respons(true, "Data Re-processing done successfully");
        }
    }
}
