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
    
    public partial class TB_FUNCAO
    {
        public int MOD_CD { get; set; }
        public int FUN_CD { get; set; }
        public string FUN_DESC { get; set; }
    
        public virtual TB_MODULO TB_MODULO { get; set; }
    }
}
