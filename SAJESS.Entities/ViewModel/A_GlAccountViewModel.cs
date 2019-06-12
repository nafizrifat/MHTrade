using System;

namespace SAJESS.Entities.ViewModel
{
    public class A_GlAccountViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<int> Parent { get; set; }
        public string AccountName { get; set; }
        public bool Children { get; set; } // if node has sub-nodes set true or not set false

        public bool transactionAllowed { get; set; } // if node has sub-nodes set true or not set false
    }
}
