using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAJESS.Entities;
using SAJESS.Manager.Interface.Management;
using SAJESS.Repositories;

namespace SAJESS.Manager.Manager.Management
{
    public class SupplierManager : ISupplierManager
    {
        private IGenericRepository<Supplier> _aRepository;
        private IGenericRepository<District> _aDistrictRepository;
        private ResponseModel _aModel;

        public SupplierManager()
        {
         _aRepository = new GenericRepositoryMms<Supplier>();
         _aDistrictRepository = new GenericRepositoryMms<District>();
            _aModel = new ResponseModel();
        }
        public ResponseModel CreateSupplier(Supplier aObj, string currentUserId)
        {
            try
            {
                
             
                if (aObj.SupplierId == 0)
                {
                   aObj.CreatedBy = currentUserId;
                    aObj.CreatedDate =DateTime.Now;
                    _aRepository.Insert(aObj);
                    _aRepository.Save();
                    return _aModel.Respons(true, "Data Successfully Saved");
                }
                else
                {
                    aObj.UpdatedBy = currentUserId;
                    aObj.UpdatedDate = DateTime.Now;
                    _aRepository.Update(aObj);
                    _aRepository.Save();
                    return _aModel.Respons(true, "Data Successfully Updated");
                }
            }
            catch (Exception ex)
            {

                return _aModel.Respons(false, "Sorry! Some Error Happened." + ex);
            }
        }

        public ResponseModel GetAllSuppliers()
        {
            try
            {
                var data = _aRepository.SelectAll().Where(a=>a.IsActive==true);
                var allDistrict = _aDistrictRepository.SelectAll();
              //  var allSuppliers = _aSupplierRepository.SelectAll();
            //    var allSupplierInvestments = _aRepository.SelectAll();

                var q = from c in data
                        join dep in allDistrict on c.DistrictId equals dep.DistrictId into ps
                        from dep in ps.DefaultIfEmpty()
                        select new
                        {

                            c.SupplierId,


                            DistrictName = dep == null ? "No District" : dep.DistrictName,
                            c.SupplierName,

                            c.MobileNo,
                            c.DistrictId,
                            c.Note,
                            c.IsActive


                        };

                var asadad = q.ToList();
                return _aModel.Respons(q);


                return _aModel.Respons(data);
            }
            catch (Exception ex)
            {

                return _aModel.Respons(false, "Sorry! Some Error Happened." + ex);
            }
        }

        public ResponseModel SuppliersCombo()
        {
            try
            {
                var data = _aRepository.SelectAll().Select(a => new
                {
                    id = a.SupplierId,
                    text = a.SupplierId +" - "+a.SupplierName 
                });

                var aaa = data.ToList();
                return _aModel.Respons(aaa);
            }
            catch (Exception)
            {

                throw;
            }
        
    }

        public ResponseModel DistrictCombo()
        {
            try
            {
                var data = _aDistrictRepository.SelectAll().Select(a => new
                {
                    id = a.DistrictId,
                    text = a.DistrictName
                });

                var aaa = data.ToList();
                return _aModel.Respons(aaa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
