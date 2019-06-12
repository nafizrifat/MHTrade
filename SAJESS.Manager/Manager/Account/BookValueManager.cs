using System;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Account;

namespace SAJESS.Manager.Manager.Account
{
    public class BookValueManager : IBookValue
        {
        private Entities.Entities _db;
        private ResponseModel _aModel;

         public BookValueManager()
        {
            _db = new Entities.Entities();
            _aModel = new ResponseModel();
        }
        public string GetFiscalYear()
        {
            int temp = DateTime.Now.Year;
            DateTime tempStartTime = new DateTime(temp, 7, 1);
            DateTime tempEndTime = new DateTime(temp+1, 6, 30);
            return (temp+"-"+ (temp + 1));
        }

        public string GetPreviousFiscalYear()
        {
            int temp = DateTime.Now.Year;
            return ((temp - 1) + "-" + temp);
        }

        public ResponseModel ViewBookClosing()
        {
            var bookClosing = _db.A_BookValue.ToString();
            return _aModel.Respons(bookClosing);
        }

        public ResponseModel SaveBookValue(DateTime start, DateTime end)
        {
            //string fiscalYear = GetFiscalYear();
            //if (_db.A_FiscalYear.Where(x=>x.FiscalYear==fiscalYear).Any())
            //{
            //    _db.A_BookValue.RemoveRange(_db.A_BookValue.Where(x => x.FiscalYear == fiscalYear).ToList());
            //}
            //A_BookValue a_BookValue;
            //List<A_GlAccount> a_GlList = _db.A_GlAccount.Where(x => x.TransactionAllowed == true).ToList();
            //foreach (A_GlAccount item in a_GlList)
            //{
            //    Int32 A_GlAccountId = item.A_GlAccountId;
            //    decimal totalDebit = 0;
            //    decimal totalCredit = 0;
            //    decimal closingBalance = 0;
            //    decimal openingBalance = GetOpeningBalance(fiscalYear, A_GlAccountId);

            //    List<A_GlTransactionDetails> tranDetailsList = _db.A_GlTransactionDetails.Where(x => x.A_GlAccountId == A_GlAccountId && x.TransactionDate>start && x.TransactionDate<end).ToList();
            //    totalDebit = tranDetailsList.Sum(m => m.DebitAmount);
            //    totalCredit = tranDetailsList.Sum(m => m.CreditAmount);
            //    closingBalance = openingBalance + totalDebit - totalCredit;

            //    a_BookValue = new A_BookValue();
            //    a_BookValue.A_GlAccountId = A_GlAccountId;
            //    a_BookValue.OpeningBalance = openingBalance;
            //    a_BookValue.ClosingBalance = closingBalance;
            //    a_BookValue.FiscalYear = GetFiscalYear();
            //    a_BookValue.Status = true;
            //        _db.A_BookValue.Add(a_BookValue);
                   

            //}
            //_db.SaveChanges();
            return _aModel.Respons("Data saved successfuly.");

        }

        public decimal GetOpeningBalance(string previousFiscalYear, int accountId)
        {
            //decimal? openingBalance = 0;
            //previousFiscalYear = GetPreviousFiscalYear();
            //if (_db.A_BookValue.Where(x => x.FiscalYear == previousFiscalYear).Any())
            //{
            //    openingBalance = _db.A_BookValue.Where(x => x.A_GlAccountId == accountId && x.FiscalYear == previousFiscalYear).Select(x => x.ClosingBalance).FirstOrDefault();

            //}
            //return Convert.ToDecimal(openingBalance);
            return 0;
        }

        public ResponseModel CreateFiscalYear(A_FiscalYear obj)
        {
            try
            {
                _db.A_FiscalYear.Attach(obj);
                _db.A_FiscalYear.Add(obj);
                _db.SaveChanges();
                return _aModel.Respons("Data saved successfuly.");
            }
            catch (Exception ex)
            {

                return _aModel.Respons(ex.Message);
            }
            

            
        }
    }
}
