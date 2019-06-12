using System;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Account;
using SAJESS.Repositories;

namespace SAJESS.Manager.Manager.Account
{
    public class FiscalYearConfigManager : IFiscalYearConfig
    {
        private ResponseModel _aModel;
        private IGenericRepository<A_FiscalYear> _aRepository;
        private Entities.Entities _db;

        public FiscalYearConfigManager()
        {
            _aModel = new ResponseModel();
            _aRepository = new GenericRepositoryMms<A_FiscalYear>();
            _db = new Entities.Entities();
        }
        public ResponseModel CreateFiscalYear(A_FiscalYear aObj)
        {
            if (aObj.FYId==0)
            {
                _aRepository.Insert(aObj);
                _aRepository.Save();
                return _aModel.Respons(true, "New fiscal year saved successfully.");
            }
            else
            {
                A_FiscalYear aYear = new A_FiscalYear()
                {
                    
                    FiscalYear = aObj.FiscalYear,
                    FromDate = aObj.FromDate,
                    ToDate = aObj.ToDate,
                    IsBooked = true,
                    UserId = aObj.UserId,
                    BookedDate = DateTime.Now,
                };
                _aRepository.Insert(aYear);
                _aRepository.Save();
                A_FiscalYear aYearUpdate = _aRepository.SelectedById(aObj.FYId);
                aYearUpdate.IsBooked = true;
                aYearUpdate.BookedDate = DateTime.Now;
                _aRepository.Update(aYearUpdate);

                _aRepository.Save();
                return _aModel.Respons(true, "Fiscal year updated successfully");
            }
        }

        public ResponseModel DeleteFiscalYear(int yearId)
        {
            var aYearDel = _aRepository.SelectedById(yearId);
            aYearDel.IsBooked = false;
            aYearDel.BookedDate = DateTime.Now;
            _aRepository.Update(aYearDel);
            _aRepository.Save();
            return _aModel.Respons(true, "The bill slab deleted successfully.");
        }

        public ResponseModel GetAllFiscalYear()
        {
            var allFiscalYear = _aRepository.SelectAll();
            var data = from A in allFiscalYear
                       where A.IsBooked == true
                       select new A_FiscalYear
                       {
                           FYId = A.FYId,
                           FiscalYear = A.FiscalYear,
                           FromDate = A.FromDate,
                           ToDate = A.ToDate,
                           IsBooked = A.IsBooked,
                           UserId = A.UserId,
                           BookedDate = A.BookedDate
                       };
            return _aModel.Respons(data);
        }

        public ResponseModel IsBooked(int yearID)
        {
            bool data;
            var aYear = (from bd in _db.A_BookValue
                         where bd.FYId == yearID
                         select bd).FirstOrDefault();
            if (aYear != null)
            {

                data = true;
            }
            else
            {
                data = false;
            }
            return _aModel.Respons(data);
        }
    }
}
