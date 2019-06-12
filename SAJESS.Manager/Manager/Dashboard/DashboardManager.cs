using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Dashboard;
using SAJESS.Repositories;

namespace SAJESS.Manager.Manager.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private IGenericRepository<SupplierInvestment> _aRepository;
        private IGenericRepository<Supplier> _aSupplierRepository;
        private ResponseModel _aModel;
        public DashboardManager()
        {
            _aRepository = new GenericRepositoryMms<SupplierInvestment>();
            _aSupplierRepository = new GenericRepositoryMms<Supplier>();
            _aModel = new ResponseModel();
        }

        public ResponseModel GetTotalByDateDashboard()
        {
            var data = _aRepository.SelectAll().GroupBy(l => l.TransactionDate)
            .Select(cl => new
            {
                date = cl.Key,
                value = cl.Sum(c => c.Amount),
            }).OrderBy(x=>x.date);

             var finalData = data.Select(a => new
                    {
                        date = a.date.ToString("dd-MM-yyyy"),
                        value = a.value
                    });
             var caaa = finalData.ToList();

            return _aModel.Respons(finalData);

            //var data = aConnection.SupplierInvestments.GroupBy(l => l.TransactionDate)
            //.Select(cl => new
            //{
            //    date = cl.Key,
            //    value = cl.Sum(c => c.Amount),
            //}).ToList();

            //        var aa = data.Select(a => new
            //        {
            //            date = a.date.ToString("dd-MM-yyyy"),
            //            value = a.value
            //        });
            //        var caaa = aa.ToList();
        }

        public ResponseModel GetSupplierWiseTotalPaymentDashboard()
        {
            var suppliers = _aSupplierRepository.SelectAll();
            var data = _aRepository.SelectAll().GroupBy(l => l.SupplierId)
            .Select(cl => new
            {
                category = cl.Key,
                value = cl.Sum(c => c.Amount),
            }).OrderBy(x => x.value);


            var findalRessult = from d in data
                join s in suppliers on d.category equals s.SupplierId
                select new
                {
                    category=s.SupplierName+"("+s.SupplierId+")",
                    value=d.value
                };


            //var finalData = data.Select(a => new
            //{
            //    date = a.date.ToString("dd-MM-yyyy"),
            //    value = a.value
            //});
            var caaa = findalRessult.ToList();

            return _aModel.Respons(findalRessult);
        }
    }
}
