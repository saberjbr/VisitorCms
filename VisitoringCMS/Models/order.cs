//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisitoringCMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.Order_Products = new HashSet<Order_Products>();
        }
    
        public int Id { get; set; }
        public int Visitor_id { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string info { get; set; }
    
        public virtual Visitor Visitor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Products> Order_Products { get; set; }
    }
}
