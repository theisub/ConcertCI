//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConcertCI
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblConcerts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblConcerts()
        {
            this.tblNotifications = new HashSet<tblNotifications>();
        }
    
        public int concert_id { get; set; }
        public int group_id { get; set; }
        public string concert_title { get; set; }
        public string concert_city { get; set; }
        public string concert_place { get; set; }
        public System.DateTime concert_date { get; set; }
        public string concert_link { get; set; }
    
        public virtual tblGroups tblGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblNotifications> tblNotifications { get; set; }
    }
}
