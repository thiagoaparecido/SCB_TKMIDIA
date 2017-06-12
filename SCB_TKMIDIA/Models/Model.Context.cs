﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SCBEntities : DbContext
    {
        public SCBEntities()
            : base("name=SCBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_BILHETERIA> TB_BILHETERIA { get; set; }
        public virtual DbSet<TB_DISTRIBUIDORA> TB_DISTRIBUIDORA { get; set; }
        public virtual DbSet<TB_EMPRESA_COMPLEXO> TB_EMPRESA_COMPLEXO { get; set; }
        public virtual DbSet<TB_FILME> TB_FILME { get; set; }
        public virtual DbSet<TB_FUNCAO> TB_FUNCAO { get; set; }
        public virtual DbSet<TB_GRP_CIN> TB_GRP_CIN { get; set; }
        public virtual DbSet<TB_INGRESSO_TIPO> TB_INGRESSO_TIPO { get; set; }
        public virtual DbSet<TB_MENSAGEM_ANCINE> TB_MENSAGEM_ANCINE { get; set; }
        public virtual DbSet<TB_MODULO> TB_MODULO { get; set; }
        public virtual DbSet<TB_PERFIL_ACESSO> TB_PERFIL_ACESSO { get; set; }
        public virtual DbSet<TB_PROGRAMA> TB_PROGRAMA { get; set; }
        public virtual DbSet<TB_SALA> TB_SALA { get; set; }
        public virtual DbSet<TB_SESSAO_ANCINE> TB_SESSAO_ANCINE { get; set; }
        public virtual DbSet<TB_TOT_CATEG_ING> TB_TOT_CATEG_ING { get; set; }
        public virtual DbSet<TB_TOT_MOD_PAGTO> TB_TOT_MOD_PAGTO { get; set; }
        public virtual DbSet<TB_TOT_TP_ASSENTO> TB_TOT_TP_ASSENTO { get; set; }
        public virtual DbSet<TB_USUARIO> TB_USUARIO { get; set; }
        public virtual DbSet<TB_VENDEDOR_REMOTO> TB_VENDEDOR_REMOTO { get; set; }
    
        public virtual int up_TB_PROGRAMA_F(string eMP_CD_ANCINE, Nullable<System.DateTime> pRO_DT_INI, Nullable<System.DateTime> pRO_DT_FIM, string fIL_CD_ANCINE, string sAL_CD_ANCINE, string eMP_CD_ANCINE2, Nullable<System.DateTime> pRO_DT_INI2, Nullable<System.DateTime> pRO_DT_FIM2, string fIL_CD_ANCINE2, string sAL_CD_ANCINE2)
        {
            var eMP_CD_ANCINEParameter = eMP_CD_ANCINE != null ?
                new ObjectParameter("EMP_CD_ANCINE", eMP_CD_ANCINE) :
                new ObjectParameter("EMP_CD_ANCINE", typeof(string));
    
            var pRO_DT_INIParameter = pRO_DT_INI.HasValue ?
                new ObjectParameter("PRO_DT_INI", pRO_DT_INI) :
                new ObjectParameter("PRO_DT_INI", typeof(System.DateTime));
    
            var pRO_DT_FIMParameter = pRO_DT_FIM.HasValue ?
                new ObjectParameter("PRO_DT_FIM", pRO_DT_FIM) :
                new ObjectParameter("PRO_DT_FIM", typeof(System.DateTime));
    
            var fIL_CD_ANCINEParameter = fIL_CD_ANCINE != null ?
                new ObjectParameter("FIL_CD_ANCINE", fIL_CD_ANCINE) :
                new ObjectParameter("FIL_CD_ANCINE", typeof(string));
    
            var sAL_CD_ANCINEParameter = sAL_CD_ANCINE != null ?
                new ObjectParameter("SAL_CD_ANCINE", sAL_CD_ANCINE) :
                new ObjectParameter("SAL_CD_ANCINE", typeof(string));
    
            var eMP_CD_ANCINE2Parameter = eMP_CD_ANCINE2 != null ?
                new ObjectParameter("EMP_CD_ANCINE2", eMP_CD_ANCINE2) :
                new ObjectParameter("EMP_CD_ANCINE2", typeof(string));
    
            var pRO_DT_INI2Parameter = pRO_DT_INI2.HasValue ?
                new ObjectParameter("PRO_DT_INI2", pRO_DT_INI2) :
                new ObjectParameter("PRO_DT_INI2", typeof(System.DateTime));
    
            var pRO_DT_FIM2Parameter = pRO_DT_FIM2.HasValue ?
                new ObjectParameter("PRO_DT_FIM2", pRO_DT_FIM2) :
                new ObjectParameter("PRO_DT_FIM2", typeof(System.DateTime));
    
            var fIL_CD_ANCINE2Parameter = fIL_CD_ANCINE2 != null ?
                new ObjectParameter("FIL_CD_ANCINE2", fIL_CD_ANCINE2) :
                new ObjectParameter("FIL_CD_ANCINE2", typeof(string));
    
            var sAL_CD_ANCINE2Parameter = sAL_CD_ANCINE2 != null ?
                new ObjectParameter("SAL_CD_ANCINE2", sAL_CD_ANCINE2) :
                new ObjectParameter("SAL_CD_ANCINE2", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("up_TB_PROGRAMA_F", eMP_CD_ANCINEParameter, pRO_DT_INIParameter, pRO_DT_FIMParameter, fIL_CD_ANCINEParameter, sAL_CD_ANCINEParameter, eMP_CD_ANCINE2Parameter, pRO_DT_INI2Parameter, pRO_DT_FIM2Parameter, fIL_CD_ANCINE2Parameter, sAL_CD_ANCINE2Parameter);
        }
    
        public virtual int up_TB_PROGRAMA_I(string eMP_CD_ANCINE, string eMP_RZ_SOCIAL, string eMP_NM_FANT, string fIL_CD_ANCINE, string fIL_NM, string dIS_NM, string sAL_CD_ANCINE, string sAL_DESC, string pRO_SOM, string sAL_TP_PROJECAO, string sAL_SERIAL_PROJETOR, Nullable<System.DateTime> pRO_DT_INI, Nullable<System.DateTime> pRO_DT_FIM, Nullable<System.DateTime> pRO_DT_INC, Nullable<int> pRO_QTD_SESSAO, string pRO_STATUS, Nullable<System.DateTime> pRO_DT_ALT, Nullable<int> pRO_SEM_CIN, string pRO_AUDIO, string pRO_AUDIO_DESC, string pRO_TP_TELA, string pRO_LEG, string pRO_LEG_DESC_CC, string pRO_LIBRA, string pRO_VPF, Nullable<System.DateTime> pRO_DT_DES, string pRO_MOT_DES)
        {
            var eMP_CD_ANCINEParameter = eMP_CD_ANCINE != null ?
                new ObjectParameter("EMP_CD_ANCINE", eMP_CD_ANCINE) :
                new ObjectParameter("EMP_CD_ANCINE", typeof(string));
    
            var eMP_RZ_SOCIALParameter = eMP_RZ_SOCIAL != null ?
                new ObjectParameter("EMP_RZ_SOCIAL", eMP_RZ_SOCIAL) :
                new ObjectParameter("EMP_RZ_SOCIAL", typeof(string));
    
            var eMP_NM_FANTParameter = eMP_NM_FANT != null ?
                new ObjectParameter("EMP_NM_FANT", eMP_NM_FANT) :
                new ObjectParameter("EMP_NM_FANT", typeof(string));
    
            var fIL_CD_ANCINEParameter = fIL_CD_ANCINE != null ?
                new ObjectParameter("FIL_CD_ANCINE", fIL_CD_ANCINE) :
                new ObjectParameter("FIL_CD_ANCINE", typeof(string));
    
            var fIL_NMParameter = fIL_NM != null ?
                new ObjectParameter("FIL_NM", fIL_NM) :
                new ObjectParameter("FIL_NM", typeof(string));
    
            var dIS_NMParameter = dIS_NM != null ?
                new ObjectParameter("DIS_NM", dIS_NM) :
                new ObjectParameter("DIS_NM", typeof(string));
    
            var sAL_CD_ANCINEParameter = sAL_CD_ANCINE != null ?
                new ObjectParameter("SAL_CD_ANCINE", sAL_CD_ANCINE) :
                new ObjectParameter("SAL_CD_ANCINE", typeof(string));
    
            var sAL_DESCParameter = sAL_DESC != null ?
                new ObjectParameter("SAL_DESC", sAL_DESC) :
                new ObjectParameter("SAL_DESC", typeof(string));
    
            var pRO_SOMParameter = pRO_SOM != null ?
                new ObjectParameter("PRO_SOM", pRO_SOM) :
                new ObjectParameter("PRO_SOM", typeof(string));
    
            var sAL_TP_PROJECAOParameter = sAL_TP_PROJECAO != null ?
                new ObjectParameter("SAL_TP_PROJECAO", sAL_TP_PROJECAO) :
                new ObjectParameter("SAL_TP_PROJECAO", typeof(string));
    
            var sAL_SERIAL_PROJETORParameter = sAL_SERIAL_PROJETOR != null ?
                new ObjectParameter("SAL_SERIAL_PROJETOR", sAL_SERIAL_PROJETOR) :
                new ObjectParameter("SAL_SERIAL_PROJETOR", typeof(string));
    
            var pRO_DT_INIParameter = pRO_DT_INI.HasValue ?
                new ObjectParameter("PRO_DT_INI", pRO_DT_INI) :
                new ObjectParameter("PRO_DT_INI", typeof(System.DateTime));
    
            var pRO_DT_FIMParameter = pRO_DT_FIM.HasValue ?
                new ObjectParameter("PRO_DT_FIM", pRO_DT_FIM) :
                new ObjectParameter("PRO_DT_FIM", typeof(System.DateTime));
    
            var pRO_DT_INCParameter = pRO_DT_INC.HasValue ?
                new ObjectParameter("PRO_DT_INC", pRO_DT_INC) :
                new ObjectParameter("PRO_DT_INC", typeof(System.DateTime));
    
            var pRO_QTD_SESSAOParameter = pRO_QTD_SESSAO.HasValue ?
                new ObjectParameter("PRO_QTD_SESSAO", pRO_QTD_SESSAO) :
                new ObjectParameter("PRO_QTD_SESSAO", typeof(int));
    
            var pRO_STATUSParameter = pRO_STATUS != null ?
                new ObjectParameter("PRO_STATUS", pRO_STATUS) :
                new ObjectParameter("PRO_STATUS", typeof(string));
    
            var pRO_DT_ALTParameter = pRO_DT_ALT.HasValue ?
                new ObjectParameter("PRO_DT_ALT", pRO_DT_ALT) :
                new ObjectParameter("PRO_DT_ALT", typeof(System.DateTime));
    
            var pRO_SEM_CINParameter = pRO_SEM_CIN.HasValue ?
                new ObjectParameter("PRO_SEM_CIN", pRO_SEM_CIN) :
                new ObjectParameter("PRO_SEM_CIN", typeof(int));
    
            var pRO_AUDIOParameter = pRO_AUDIO != null ?
                new ObjectParameter("PRO_AUDIO", pRO_AUDIO) :
                new ObjectParameter("PRO_AUDIO", typeof(string));
    
            var pRO_AUDIO_DESCParameter = pRO_AUDIO_DESC != null ?
                new ObjectParameter("PRO_AUDIO_DESC", pRO_AUDIO_DESC) :
                new ObjectParameter("PRO_AUDIO_DESC", typeof(string));
    
            var pRO_TP_TELAParameter = pRO_TP_TELA != null ?
                new ObjectParameter("PRO_TP_TELA", pRO_TP_TELA) :
                new ObjectParameter("PRO_TP_TELA", typeof(string));
    
            var pRO_LEGParameter = pRO_LEG != null ?
                new ObjectParameter("PRO_LEG", pRO_LEG) :
                new ObjectParameter("PRO_LEG", typeof(string));
    
            var pRO_LEG_DESC_CCParameter = pRO_LEG_DESC_CC != null ?
                new ObjectParameter("PRO_LEG_DESC_CC", pRO_LEG_DESC_CC) :
                new ObjectParameter("PRO_LEG_DESC_CC", typeof(string));
    
            var pRO_LIBRAParameter = pRO_LIBRA != null ?
                new ObjectParameter("PRO_LIBRA", pRO_LIBRA) :
                new ObjectParameter("PRO_LIBRA", typeof(string));
    
            var pRO_VPFParameter = pRO_VPF != null ?
                new ObjectParameter("PRO_VPF", pRO_VPF) :
                new ObjectParameter("PRO_VPF", typeof(string));
    
            var pRO_DT_DESParameter = pRO_DT_DES.HasValue ?
                new ObjectParameter("PRO_DT_DES", pRO_DT_DES) :
                new ObjectParameter("PRO_DT_DES", typeof(System.DateTime));
    
            var pRO_MOT_DESParameter = pRO_MOT_DES != null ?
                new ObjectParameter("PRO_MOT_DES", pRO_MOT_DES) :
                new ObjectParameter("PRO_MOT_DES", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("up_TB_PROGRAMA_I", eMP_CD_ANCINEParameter, eMP_RZ_SOCIALParameter, eMP_NM_FANTParameter, fIL_CD_ANCINEParameter, fIL_NMParameter, dIS_NMParameter, sAL_CD_ANCINEParameter, sAL_DESCParameter, pRO_SOMParameter, sAL_TP_PROJECAOParameter, sAL_SERIAL_PROJETORParameter, pRO_DT_INIParameter, pRO_DT_FIMParameter, pRO_DT_INCParameter, pRO_QTD_SESSAOParameter, pRO_STATUSParameter, pRO_DT_ALTParameter, pRO_SEM_CINParameter, pRO_AUDIOParameter, pRO_AUDIO_DESCParameter, pRO_TP_TELAParameter, pRO_LEGParameter, pRO_LEG_DESC_CCParameter, pRO_LIBRAParameter, pRO_VPFParameter, pRO_DT_DESParameter, pRO_MOT_DESParameter);
        }
    
        public virtual ObjectResult<up_TB_PROGRAMA_R_Result> up_TB_PROGRAMA_R()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<up_TB_PROGRAMA_R_Result>("up_TB_PROGRAMA_R");
        }
    
        public virtual int up_TB_PROGRAMA_U(Nullable<long> pRO_CD, string eMP_CD_ANCINE, string eMP_RZ_SOCIAL, string eMP_NM_FANT, string fIL_CD_ANCINE, string fIL_NM, string dIS_NM, string sAL_CD_ANCINE, string sAL_DESC, string pRO_SOM, string sAL_TP_PROJECAO, string sAL_SERIAL_PROJETOR, Nullable<System.DateTime> pRO_DT_INI, Nullable<System.DateTime> pRO_DT_FIM, Nullable<System.DateTime> pRO_DT_INC, Nullable<int> pRO_QTD_SESSAO, string pRO_STATUS, Nullable<System.DateTime> pRO_DT_ALT, Nullable<int> pRO_SEM_CIN, string pRO_AUDIO, string pRO_AUDIO_DESC, string pRO_TP_TELA, string pRO_LEG, string pRO_LEG_DESC_CC, string pRO_LIBRA, string pRO_VPF, Nullable<System.DateTime> pRO_DT_DES, string pRO_MOT_DES)
        {
            var pRO_CDParameter = pRO_CD.HasValue ?
                new ObjectParameter("PRO_CD", pRO_CD) :
                new ObjectParameter("PRO_CD", typeof(long));
    
            var eMP_CD_ANCINEParameter = eMP_CD_ANCINE != null ?
                new ObjectParameter("EMP_CD_ANCINE", eMP_CD_ANCINE) :
                new ObjectParameter("EMP_CD_ANCINE", typeof(string));
    
            var eMP_RZ_SOCIALParameter = eMP_RZ_SOCIAL != null ?
                new ObjectParameter("EMP_RZ_SOCIAL", eMP_RZ_SOCIAL) :
                new ObjectParameter("EMP_RZ_SOCIAL", typeof(string));
    
            var eMP_NM_FANTParameter = eMP_NM_FANT != null ?
                new ObjectParameter("EMP_NM_FANT", eMP_NM_FANT) :
                new ObjectParameter("EMP_NM_FANT", typeof(string));
    
            var fIL_CD_ANCINEParameter = fIL_CD_ANCINE != null ?
                new ObjectParameter("FIL_CD_ANCINE", fIL_CD_ANCINE) :
                new ObjectParameter("FIL_CD_ANCINE", typeof(string));
    
            var fIL_NMParameter = fIL_NM != null ?
                new ObjectParameter("FIL_NM", fIL_NM) :
                new ObjectParameter("FIL_NM", typeof(string));
    
            var dIS_NMParameter = dIS_NM != null ?
                new ObjectParameter("DIS_NM", dIS_NM) :
                new ObjectParameter("DIS_NM", typeof(string));
    
            var sAL_CD_ANCINEParameter = sAL_CD_ANCINE != null ?
                new ObjectParameter("SAL_CD_ANCINE", sAL_CD_ANCINE) :
                new ObjectParameter("SAL_CD_ANCINE", typeof(string));
    
            var sAL_DESCParameter = sAL_DESC != null ?
                new ObjectParameter("SAL_DESC", sAL_DESC) :
                new ObjectParameter("SAL_DESC", typeof(string));
    
            var pRO_SOMParameter = pRO_SOM != null ?
                new ObjectParameter("PRO_SOM", pRO_SOM) :
                new ObjectParameter("PRO_SOM", typeof(string));
    
            var sAL_TP_PROJECAOParameter = sAL_TP_PROJECAO != null ?
                new ObjectParameter("SAL_TP_PROJECAO", sAL_TP_PROJECAO) :
                new ObjectParameter("SAL_TP_PROJECAO", typeof(string));
    
            var sAL_SERIAL_PROJETORParameter = sAL_SERIAL_PROJETOR != null ?
                new ObjectParameter("SAL_SERIAL_PROJETOR", sAL_SERIAL_PROJETOR) :
                new ObjectParameter("SAL_SERIAL_PROJETOR", typeof(string));
    
            var pRO_DT_INIParameter = pRO_DT_INI.HasValue ?
                new ObjectParameter("PRO_DT_INI", pRO_DT_INI) :
                new ObjectParameter("PRO_DT_INI", typeof(System.DateTime));
    
            var pRO_DT_FIMParameter = pRO_DT_FIM.HasValue ?
                new ObjectParameter("PRO_DT_FIM", pRO_DT_FIM) :
                new ObjectParameter("PRO_DT_FIM", typeof(System.DateTime));
    
            var pRO_DT_INCParameter = pRO_DT_INC.HasValue ?
                new ObjectParameter("PRO_DT_INC", pRO_DT_INC) :
                new ObjectParameter("PRO_DT_INC", typeof(System.DateTime));
    
            var pRO_QTD_SESSAOParameter = pRO_QTD_SESSAO.HasValue ?
                new ObjectParameter("PRO_QTD_SESSAO", pRO_QTD_SESSAO) :
                new ObjectParameter("PRO_QTD_SESSAO", typeof(int));
    
            var pRO_STATUSParameter = pRO_STATUS != null ?
                new ObjectParameter("PRO_STATUS", pRO_STATUS) :
                new ObjectParameter("PRO_STATUS", typeof(string));
    
            var pRO_DT_ALTParameter = pRO_DT_ALT.HasValue ?
                new ObjectParameter("PRO_DT_ALT", pRO_DT_ALT) :
                new ObjectParameter("PRO_DT_ALT", typeof(System.DateTime));
    
            var pRO_SEM_CINParameter = pRO_SEM_CIN.HasValue ?
                new ObjectParameter("PRO_SEM_CIN", pRO_SEM_CIN) :
                new ObjectParameter("PRO_SEM_CIN", typeof(int));
    
            var pRO_AUDIOParameter = pRO_AUDIO != null ?
                new ObjectParameter("PRO_AUDIO", pRO_AUDIO) :
                new ObjectParameter("PRO_AUDIO", typeof(string));
    
            var pRO_AUDIO_DESCParameter = pRO_AUDIO_DESC != null ?
                new ObjectParameter("PRO_AUDIO_DESC", pRO_AUDIO_DESC) :
                new ObjectParameter("PRO_AUDIO_DESC", typeof(string));
    
            var pRO_TP_TELAParameter = pRO_TP_TELA != null ?
                new ObjectParameter("PRO_TP_TELA", pRO_TP_TELA) :
                new ObjectParameter("PRO_TP_TELA", typeof(string));
    
            var pRO_LEGParameter = pRO_LEG != null ?
                new ObjectParameter("PRO_LEG", pRO_LEG) :
                new ObjectParameter("PRO_LEG", typeof(string));
    
            var pRO_LEG_DESC_CCParameter = pRO_LEG_DESC_CC != null ?
                new ObjectParameter("PRO_LEG_DESC_CC", pRO_LEG_DESC_CC) :
                new ObjectParameter("PRO_LEG_DESC_CC", typeof(string));
    
            var pRO_LIBRAParameter = pRO_LIBRA != null ?
                new ObjectParameter("PRO_LIBRA", pRO_LIBRA) :
                new ObjectParameter("PRO_LIBRA", typeof(string));
    
            var pRO_VPFParameter = pRO_VPF != null ?
                new ObjectParameter("PRO_VPF", pRO_VPF) :
                new ObjectParameter("PRO_VPF", typeof(string));
    
            var pRO_DT_DESParameter = pRO_DT_DES.HasValue ?
                new ObjectParameter("PRO_DT_DES", pRO_DT_DES) :
                new ObjectParameter("PRO_DT_DES", typeof(System.DateTime));
    
            var pRO_MOT_DESParameter = pRO_MOT_DES != null ?
                new ObjectParameter("PRO_MOT_DES", pRO_MOT_DES) :
                new ObjectParameter("PRO_MOT_DES", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("up_TB_PROGRAMA_U", pRO_CDParameter, eMP_CD_ANCINEParameter, eMP_RZ_SOCIALParameter, eMP_NM_FANTParameter, fIL_CD_ANCINEParameter, fIL_NMParameter, dIS_NMParameter, sAL_CD_ANCINEParameter, sAL_DESCParameter, pRO_SOMParameter, sAL_TP_PROJECAOParameter, sAL_SERIAL_PROJETORParameter, pRO_DT_INIParameter, pRO_DT_FIMParameter, pRO_DT_INCParameter, pRO_QTD_SESSAOParameter, pRO_STATUSParameter, pRO_DT_ALTParameter, pRO_SEM_CINParameter, pRO_AUDIOParameter, pRO_AUDIO_DESCParameter, pRO_TP_TELAParameter, pRO_LEGParameter, pRO_LEG_DESC_CCParameter, pRO_LIBRAParameter, pRO_VPFParameter, pRO_DT_DESParameter, pRO_MOT_DESParameter);
        }
    }
}
