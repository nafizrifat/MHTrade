namespace SAJESS.Entities.ViewModel
{
  public class GeneralLedgerViewModel
    {
        public string Name { set; get; }
        public decimal OpeningBalance { set; get; }
        public decimal DebitAmount { set; get; }
        public decimal CreditAmount { set; get; }
        public decimal ClosingBalance { set; get; }
    }
}
