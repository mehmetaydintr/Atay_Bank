//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HGS_REST_API
{
    using System;
    using System.Collections.Generic;
    
    public partial class kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kullanici()
        {
            this.hesap = new HashSet<hesap>();
            this.HGS_Banka = new HashSet<HGS_Banka>();
        }
    
        public long musteriNo { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string TCKN { get; set; }
        public string sifre { get; set; }
        public string email { get; set; }
        public string adres { get; set; }
        public string telefon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hesap> hesap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HGS_Banka> HGS_Banka { get; set; }
    }
}
