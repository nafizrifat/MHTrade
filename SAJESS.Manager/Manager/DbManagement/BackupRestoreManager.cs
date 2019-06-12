using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using SAJESS.Entities;
using SAJESS.Manager.Interface.DbManagement;
using SAJESS.Repositories;


namespace SAJESS.Manager.Manager.DbManagement
{
    public class BackupRestoreManager : IBackupRestore
    {
        private Entities.Entities _db = null;
        private ResponseModel _aModel;
         private IGenericRepository<DataConfig> _aRepository;


        public BackupRestoreManager()
        {
            _db = new Entities.Entities();
            _aModel = new ResponseModel();

            _aRepository = new GenericRepositoryMms<DataConfig>();
        }
        public ResponseModel BackupWithFile(string fileLocation)
        {
            
            try
            {
                String date = System.DateTime.Now.ToShortDateString();
                String Path = @"C:\\Users\mahfuz\\OneDrive\\MH_Db";
                if (Directory.Exists(Path))
                {
                    System.IO.File.Delete(Path + "\\MHTradeInt" + date + ".bak");
                }
                if (!Directory.Exists(Path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path);
                }
                
                var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_db).ObjectContext;
                objCtx.ExecuteStoreCommand("BACKUP DATABASE PrescriptionGenerator TO DISK= 'C:\\Users\\mahfuz\\OneDrive\\MH_Db\\MHTradeInt" + date + ".bak';");


                return _aModel.Respons(true, "System Backup Successful");
            }
            catch (Exception ex)
            {
                return _aModel.Respons(false, "Sorry! Some Error Happened." + ex);
            }
        }
        public ResponseModel BackupWithoutFile()
        {
            try
            {

              var pathData=  _aRepository.SelectAll().FirstOrDefault(a => a.Key == "filePath");

                String date = System.DateTime.Now.ToString("dd_MM_yyyy");
                String Path = pathData.Value.ToString();
                if (pathData != null)
                {
                    if (Directory.Exists(Path))
                    {
                        System.IO.File.Delete(Path + "\\MHTrade_" + date + ".bak");
                    }
                    if (!Directory.Exists(Path))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(Path);
                    }
                }

                if (!Directory.Exists(Path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path);
                }
                _db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "BACKUP DATABASE MHTrade TO DISK= '" + Path + "\\MHTrade_" + date + ".bak';");


                return _aModel.Respons(true, "System Backup Successful");
            }
            catch (Exception ex)
            {
                return _aModel.Respons(false, "Sorry! Some Error Happened." + ex);
            }
        }


    }
}
