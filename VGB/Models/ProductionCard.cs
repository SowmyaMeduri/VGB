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
    
    public partial class ProductionCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionCard()
        {
            this.Deliveries = new HashSet<Delivery>();
        }
    
        public int productionId { get; set; }
        public int JobWorkId { get; set; }
        public string Created_By { get; set; }
        public string Rsize { get; set; }
        public string Gsm { get; set; }
        public string Colour { get; set; }
        public string PCol { get; set; }
        public string Type { get; set; }
        public string BagSize { get; set; }
        public string Kgs { get; set; }
        public string NoOfPCS { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string partyName { get; set; }
        public string shift { get; set; }
        public string status { get; set; }
        public string PLNo { get; set; }
        public string DCNO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual JobWork JobWork { get; set; }
    }
}
