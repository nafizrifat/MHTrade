using System;

namespace SAJESS.Entities.ViewModel
{
    public class ManualJournal
    {
        public int Id { get; set; }
        public int A_GlAccountId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EntryType { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Particulars { get; set; }
        public DateTime TransactionDate { get; set; }
        public Int32 VoucharNumber { set; get; }
        public string ChequeNumber { set; get; }

    }
}
