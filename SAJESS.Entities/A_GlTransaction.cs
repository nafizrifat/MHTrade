//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAJESS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class A_GlTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public A_GlTransaction()
        {
            this.A_GlTransactionDetails = new HashSet<A_GlTransactionDetails>();
        }
    
        public long A_GlTransactionId { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int EntryMethod { get; set; }
        public string Description { get; set; }
        public Nullable<int> A_CostCenterId { get; set; }
        public Nullable<int> VoucharNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<A_GlTransactionDetails> A_GlTransactionDetails { get; set; }
    }
}