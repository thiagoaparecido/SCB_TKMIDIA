using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCB_TKMIDIA.Models
{
    public class SalaBilheteria
    {
        public int QTD_SES_SALA_DIA { get; set; }
        public string BIL_STATUS_NAO_ACA { get; set; }
        public string BIL_STATUS_ANALISE { get; set; }
        public string BIL_STATUS_ERRO { get; set; }
        public string BIL_STATUS_VAL { get; set; }
        public string BIL_STATUS_RECUSADO { get; set; }

        public string EMP_CD_ANCINE { get; set; }

        public string SAL_CD_ANCINE { get; set; }
        public string SAL_DESC { get; set; }
        public Nullable<int> SAL_QTD_LUG_PDR { get; set; }
        public Nullable<int> SAL_QTD_LUG_ESP { get; set; }

        public System.DateTime BIL_DIA_CIN { get; set; }
        public string BIL_HOUVE_SES { get; set; }
        public string BIL_RETIF { get; set; }
        public string BIL_PROT { get; set; }
        public string BIL_ADIMP_SALA { get; set; }
        public string BIL_STATUS_PROT { get; set; }
    }
}