using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAJESS.Entities;

namespace SAJESS.Manager.Interface.DbManagement
{
    public interface IBackupRestore
    {
        ResponseModel BackupWithoutFile();
        ResponseModel BackupWithFile(string fileLocation);
    }
}
