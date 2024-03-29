//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VGB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobWork()
        {
            this.PackingLists = new HashSet<PackingList>();
            this.ProductionCards = new HashSet<ProductionCard>();
        }
    
        public int JobWorkId { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CustomerName { get; set; }
        public string PLNo { get; set; }
        public string DCNO { get; set; }
        public Nullable<int> InvoiceNo { get; set; }
        public string NetWt { get; set; }
        public string TotalRolls { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackingList> PackingLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionCard> ProductionCards { get; set; }
    }
}
