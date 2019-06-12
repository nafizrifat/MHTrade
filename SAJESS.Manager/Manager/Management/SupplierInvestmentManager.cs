using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAJESS.Entities;
using SAJESS.Entities.ViewModel;
using SAJESS.Manager.Interface;
using SAJESS.Manager.Interface.Management;
using SAJESS.Repositories;

namespace SAJESS.Manager.Manager.Management
{
    public class SupplierInvestmentManager : ISupplierInvestmentManager
    {
        private IGenericRepository<SupplierInvestment> _aRepository;
        private IGenericRepository<Supplier> _aSupplierRepository;
        private ResponseModel _aModel;
        private Entities.Entities _db = null;

        public SupplierInvestmentManager()
        {
            _aRepository = new GenericRepositoryMms<SupplierInvestment>();
            _aSupplierRepository = new GenericRepositoryMms<Supplier>();
            _aModel = new ResponseModel();
            _db = new Entities.Entities();
        }

        public ResponseModel CreatSupplierInvestment(SupplierInvestment aObj, string currentUserId)
        {
            try
            {
               
                if (aObj.SupplierInvestmentId == 0)
                {
                    var data =
                   _db.SupplierInvestments.Where(
                       i => i.TransactionDate == aObj.TransactionDate && i.Amount == aObj.Amount && i.SupplierId == aObj.SupplierId && i.Reason == aObj.Reason);

                    var a = data.ToList();
                    if (a.Count > 0)
                    {
                        throw new Exception("This Value will be Duplicated please check");
                    }

                    //static data add
                    aObj.ProcessStatus = 1;
                    aObj.Particular = "Supplier Investment";
                    aObj.IsActive = true;
                    aObj.IsDelete = false;
                    //
                    aObj.CreatedBy = currentUserId;
                    aObj.CreatedDate = DateTime.Now;
                    _aRepository.Insert(aObj);
                    _aRepository.Save();
                    return _aModel.Respons(true, "Data Saved Successfully ");
                }
                else
                {
                   // var data =
                   //_db.SupplierInvestments.Where(
                   //    i => i.TransactionDate.Year == aObj.TransactionDate.Year && i.TransactionDate.Month == aObj.TransactionDate.Month && i.TransactionDate.Day == aObj.TransactionDate.Day && i.Amount == aObj.Amount && i.SupplierId == aObj.SupplierId && i.Reason == aObj.Reason && i.SupplierInvestmentId != aObj.SupplierInvestmentId);

                    var data =
                   _db.SupplierInvestments.Where(
                       i => i.TransactionDate == aObj.TransactionDate && i.Amount == aObj.Amount && i.SupplierId == aObj.SupplierId && i.Reason == aObj.Reason && i.SupplierInvestmentId != aObj.SupplierInvestmentId);

                    var a = data.ToList();
                    if (a.Count > 0)
                    {
                        throw new Exception("This Value will be Duplicated please check");
                    }

                    var aData = _aRepository.SelectedById(aObj.SupplierInvestmentId);
                    aData.A_GlTransactionId = aObj.A_GlTransactionId;

                    aData.Amount = aObj.Amount;
                    aData.MemberName = aObj.MemberName;
                    aData.MemberNid = aObj.MemberNid;
                    aData.Particular = aObj.Particular;
                    aData.ProcessStatus = aObj.ProcessStatus;
                    aData.Reason = aObj.Reason;
                    aData.SupplierId = aObj.SupplierId;
                    aData.TransactionDate = aObj.TransactionDate;

                    aData.UpdatedBy = currentUserId;
                    aData.UpdatedDate = DateTime.Now;
                    aData.ProcessStatus = 1;
                    aData.Particular = "Supplier Investment";
                    _aRepository.Update(aData);
                    _aRepository.Save();
                    return _aModel.Respons(true, "Data Updated Successfully ");
                }
            }
            catch (Exception ex)
            {
                return _aModel.Respons(false, "Sorry! Some Error Happened." + ex.Message);
            }

        }
        public ResponseModel DeleteSupplierInvestment(SupplierInvestment aObj, string currentUserId)
        {
            try
            {
                var aData = _aRepository.SelectedById(aObj.SupplierInvestmentId);
                aData.A_GlTransactionId = aObj.A_GlTransactionId;
                aData.Amount = aObj.Amount;
                aData.MemberName = aObj.MemberName;
                aData.MemberNid = aObj.MemberNid;
                aData.Particular = aObj.Particular;
                aData.ProcessStatus = aObj.ProcessStatus;
                aData.Reason = aObj.Reason;
                aData.SupplierId = aObj.SupplierId;
                aData.TransactionDate = aObj.TransactionDate;
                aData.IsActive = false;
                aData.IsDelete = true;
                aData.DeletedDate= DateTime.Now;
                aData.UpdatedBy = currentUserId;
                aData.UpdatedDate = DateTime.Now;
                aData.ProcessStatus = 1;
                aData.Particular = "Supplier Investment";
                _aRepository.Update(aData);
                _aRepository.Save();
                return _aModel.Respons(true, "Data Deleted Successfully ");
            }
            catch (Exception ex)
            {
                return _aModel.Respons(false, "Sorry! Some Error Happened." + ex.Message);
            }

        }

        public ResponseModel GetAllSupplierInvestment(DateObj dateObj)
        {
            DateTime fromDate = DateTime.ParseExact(dateObj.FromDate, "dd/MM/yyyy", null);
            DateTime toDate = DateTime.ParseExact(dateObj.ToDate, "dd/MM/yyyy", null);

            var allSuppliers = _aSupplierRepository.SelectAll();
            //var allSupplierInvestments = _aRepository.SelectAll();
            var allSupplierInvestments = _aRepository.SelectAll().Where(a => a.TransactionDate.Date >= fromDate.Date && a.TransactionDate.Date <= toDate.Date);

            var q = (from c in allSupplierInvestments
                     join dep in allSuppliers on c.SupplierId equals dep.SupplierId into ps
                     from dep in ps.DefaultIfEmpty() 
                     select new
                     {
                         c.SupplierInvestmentId,
                         c.SupplierId,
                         SupplierName = dep == null ? "No Supplier" : dep.SupplierName,
                         c.TransactionDate,
                         c.Reason,
                         c.Amount,
                         c.MemberName,
                         c.MemberNid,
                         c.A_GlTransactionId
                         //c.SupplierName
                     }).OrderByDescending(c => c.SupplierInvestmentId);

            return _aModel.Respons(q);
        }

        public ResponseModel GetAllSupplierInvestmentBySupplierId(int supplierId)
        {
            var allSuppliers = _aSupplierRepository.SelectAll();
            var allSupplierInvestments = _aRepository.SelectAll().Where(a => a.SupplierId == supplierId);

            var q = (from c in allSupplierInvestments
                     join dep in allSuppliers on c.SupplierId equals dep.SupplierId into ps
                     from dep in ps.DefaultIfEmpty()
                     where c.IsActive == true
                     select new
                     {
                         c.SupplierInvestmentId,
                         c.SupplierId,
                         SupplierName = dep == null ? "No Supplier" : dep.SupplierName,
                         c.TransactionDate,
                         c.Reason,
                         c.Amount,
                         c.MemberName,
                         c.MemberNid,
                         c.A_GlTransactionId
                         //c.SupplierName
                     }).OrderByDescending(c => c.SupplierInvestmentId);

            var asadad = q.ToList();
            return _aModel.Respons(q);
        }
    }
}
