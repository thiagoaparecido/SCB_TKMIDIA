//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCB_TKMIDIA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_TOT_TP_ASSENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_TOT_TP_ASSENTO()
        {
            this.TB_TOT_CATEG_ING = new HashSet<TB_TOT_CATEG_ING>();
        }
    
        public long TTA_ID { get; set; }
        public long SEA_ID { get; set; }
        public string TTA_TP_ASSENTO { get; set; }
        public int TTA_QTD_DISP { get; set; }
    
        public virtual TB_SESSAO_ANCINE TB_SESSAO_ANCINE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_TOT_CATEG_ING> TB_TOT_CATEG_ING { get; set; }
    }
}
