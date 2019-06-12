using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAJESS.Entities.ViewModel
{
 public   class ManagementReportModel
    {
     public int SupplierId { get; set; }
     public bool AllDate { get; set; }
     public String FromDate { get; set; }
     public String ToDate { get; set; }
    }
}
