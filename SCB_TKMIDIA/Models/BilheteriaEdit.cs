using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCB_TKMIDIA.Models
{
    public class BilheteriaEdit
    {
        public long BIL_ID { get; set; }
        public string EMP_CD_ANCINE { get; set; }
        public string SAL_CD_ANCINE { get; set; }
        public System.DateTime BIL_DIA_CIN { get; set; }
        public string BIL_HOUVE_SES { get; set; }
        public string BIL_RETIF { get; set; }
        public string BIL_PROT { get; set; }
        public string BIL_ADIMP_SALA { get; set; }
        public string BIL_STATUS_PROT { get; set; }

        public long SEA_ID { get; set; }
        public string FIL_CD_ANCINE { get; set; }
        public System.DateTime SEA_DT_HR_INICIO { get; set; }
        public string SEA_MODAL { get; set; }
        public string SEA_FIL_NM { get; set; }
        public string SEA_FIL_TP_TELA { get; set; }
        public string SEA_FIL_DIGITAL { get; set; }
        public string SEA_FIL_TP_PROJECAO { get; set; }
        public string SEA_FIL_AUDIO { get; set; }
        public string SEA_FIL_LEG { get; set; }
        public string SEA_FIL_PRO_LIBRA { get; set; }
        public string SEA_FIL_LEG_DESC_CC { get; set; }
        public string SEA_FIL_AUDIO_DESC { get; set; }
        public long SEA_DIS_CNPJ { get; set; }
        public string SEA_DIS_NM { get; set; }
        public Nullable<long> SEA_VRE_CNPJ { get; set; }
        public string SEA_RZ_SOCIAL { get; set; }

        public long TTA_ID { get; set; }
        public string TTA_TP_ASSENTO { get; set; }
        public int TTA_QTD_DISP { get; set; }

        public long TCI_ID { get; set; }
        public int TCI_CAT { get; set; }
        public int TCI_QTD_ESPECT { get; set; }

        public long TMP_ID { get; set; }
        public int TMP_MOD_PAG { get; set; }
        public double TMP_VLR_ARR { get; set; }

    }
}