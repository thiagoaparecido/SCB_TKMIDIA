using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SCB_TKMIDIA.Models;
using System.Data.Entity;
using SCBIntegration.Entities;
using System.Configuration;
using SCBIntegration;
using System.Web.Routing;
using SCB_TKMIDIA.Helpers;

//using System.Transactions;

namespace SCB_TKMIDIA.Controllers
{
    [Authorize]
    public class BilheteriaController : Controller
    {
        private SCBEntities db = new SCBEntities();
        List<MensagensANCINE> model = new List<MensagensANCINE>();
        List<BilheteriaEdit> modelBil = new List<BilheteriaEdit>();

        Helpers.Helpers clsHelper = new Helpers.Helpers();

        //**********************************************
        public ActionResult Index(DateTime? bil_dia_cin)
        {
            DateTime dtAux;

            if (bil_dia_cin == null)
            {
                if(ViewBag.bil_dia_cin == null)
                {
                    if(TempData["bil_dia_cin"] == null)
                    {

                        var bil_exists = from bil in db.TB_BILHETERIA
                                         where bil.BIL_ID != 0
                                         select bil;

                        if (bil_exists.Any())
                        {
                            bil_dia_cin = db.TB_BILHETERIA.Max(max => max.BIL_DIA_CIN);
                        }
                        else
                        {
                            bil_dia_cin = DateTime.Today;
                        }

                    }
                    else
                    {
                        bil_dia_cin = Convert.ToDateTime(TempData["bil_dia_cin"]);
                        //TempData.Remove("");
                    }


                }
                else
                {
                    bil_dia_cin = ViewBag.bil_dia_cin;
                }
                
            }
            dtAux = Convert.ToDateTime(bil_dia_cin);
            ViewBag.bil_dia_cin = dtAux.ToShortDateString();

            var listaBil = (from b in db.TB_BILHETERIA
                            join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                            join sl in db.TB_SALA on s.SAL_CD_ANCINE equals sl.SAL_CD_ANCINE
                            where b.BIL_DIA_CIN == bil_dia_cin
                            orderby b.BIL_PROT, b.BIL_DIA_CIN, sl.SAL_DESC, s.SEA_DT_HR_INICIO

                            select new MensagensANCINE()
                            {

                                BIL_ID = b.BIL_ID,
                                BIL_DIA_CIN = b.BIL_DIA_CIN,
                                SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                SAL_DESC = sl.SAL_DESC,
                                BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                BIL_PROT = b.BIL_PROT,
                                BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                BIL_RETIF = b.BIL_RETIF,
                                EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                                SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                                SEA_DIS_NM = s.SEA_DIS_NM,
                                SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                SEA_FIL_NM = s.SEA_FIL_NM,
                                SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                                SEA_MODAL = s.SEA_MODAL,
                                SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                                SEA_VRE_CNPJ = s.SEA_VRE_CNPJ
                            }).ToList();

            foreach (var item in listaBil)
            {
                model.Add(new MensagensANCINE()
                {
                    BIL_ID = item.BIL_ID,
                    BIL_DIA_CIN = item.BIL_DIA_CIN,
                    SAL_CD_ANCINE = item.SAL_CD_ANCINE,
                    SAL_DESC = item.SAL_DESC,
                    BIL_HOUVE_SES = item.BIL_HOUVE_SES,
                    BIL_ADIMP_SALA = item.BIL_ADIMP_SALA,
                    BIL_PROT = item.BIL_PROT,
                    BIL_STATUS_PROT = item.BIL_STATUS_PROT,
                    BIL_RETIF = item.BIL_RETIF,
                    EMP_CD_ANCINE = item.EMP_CD_ANCINE,
                    FIL_CD_ANCINE = item.FIL_CD_ANCINE,
                    SEA_DIS_CNPJ = item.SEA_DIS_CNPJ,
                    SEA_DIS_NM = item.SEA_DIS_NM,
                    SEA_DT_HR_INICIO = item.SEA_DT_HR_INICIO,
                    SEA_FIL_NM = item.SEA_FIL_NM,
                    SEA_FIL_TP_PROJECAO = item.SEA_FIL_TP_PROJECAO,
                    SEA_MODAL = item.SEA_MODAL,
                    SEA_RZ_SOCIAL = item.SEA_RZ_SOCIAL,
                    SEA_VRE_CNPJ = item.SEA_VRE_CNPJ
                });
            }

            var TB_BIL_SEN_SESS = (from b in db.TB_BILHETERIA
                                   join sl in db.TB_SALA on b.SAL_CD_ANCINE equals sl.SAL_CD_ANCINE
                                   where b.BIL_DIA_CIN == bil_dia_cin && b.BIL_HOUVE_SES == "N"
                                   orderby b.BIL_PROT, b.BIL_DIA_CIN

                                    select new MensagensANCINE()
                                    {
                                        BIL_ID = b.BIL_ID ,
                                        BIL_DIA_CIN = b.BIL_DIA_CIN ,
                                        SAL_CD_ANCINE = b.SAL_CD_ANCINE ,
                                        SAL_DESC = sl.SAL_DESC,
                                        BIL_HOUVE_SES = b.BIL_HOUVE_SES ,
                                        BIL_ADIMP_SALA = b.BIL_ADIMP_SALA ,
                                        BIL_PROT = b.BIL_PROT ,
                                        BIL_STATUS_PROT = b.BIL_STATUS_PROT ,
                                        BIL_RETIF = b.BIL_RETIF ,
                                        EMP_CD_ANCINE = b.EMP_CD_ANCINE ,
                                        FIL_CD_ANCINE = "" ,
                                        SEA_DIS_CNPJ = 0 ,
                                        SEA_DIS_NM = "" ,
                                        SEA_DT_HR_INICIO = b.BIL_DIA_CIN ,
                                        SEA_FIL_NM = "" ,
                                        SEA_FIL_TP_PROJECAO = "" ,
                                        SEA_MODAL = "" ,
                                        SEA_RZ_SOCIAL = "" ,
                                        SEA_VRE_CNPJ = 0
                                    }).ToList();

            foreach (var item in TB_BIL_SEN_SESS)
            {
                model.Add(new MensagensANCINE()
                {
                    BIL_ID = item.BIL_ID ,
                    BIL_DIA_CIN = item.BIL_DIA_CIN ,
                    SAL_CD_ANCINE = item.SAL_CD_ANCINE ,
                    SAL_DESC = item.SAL_DESC ,
                    BIL_HOUVE_SES = item.BIL_HOUVE_SES ,
                    BIL_ADIMP_SALA = item.BIL_ADIMP_SALA ,
                    BIL_PROT = item.BIL_PROT ,
                    BIL_STATUS_PROT = item.BIL_STATUS_PROT ,
                    BIL_RETIF = item.BIL_RETIF ,
                    EMP_CD_ANCINE = item.EMP_CD_ANCINE ,
                    FIL_CD_ANCINE = item.FIL_CD_ANCINE ,
                    SEA_DIS_CNPJ = item.SEA_DIS_CNPJ ,
                    SEA_DIS_NM = item.SEA_DIS_NM ,
                    SEA_DT_HR_INICIO = item.SEA_DT_HR_INICIO ,
                    SEA_FIL_NM = item.SEA_FIL_NM ,
                    SEA_FIL_TP_PROJECAO = item.SEA_FIL_TP_PROJECAO ,
                    SEA_MODAL = item.SEA_MODAL ,
                    SEA_RZ_SOCIAL = item.SEA_RZ_SOCIAL ,
                    SEA_VRE_CNPJ = item.SEA_VRE_CNPJ
                });
            }


            var myunionQuery = listaBil.Union(TB_BIL_SEN_SESS);

            return View(myunionQuery.ToList());
        }

        //*******************************
        public ActionResult IndexStatus()
        {
            var listaBil = (from b in db.TB_BILHETERIA
                            where b.BIL_STATUS_PROT != "V" && b.BIL_STATUS_PROT != ""
                            join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                            join sl in db.TB_SALA on s.SAL_CD_ANCINE equals sl.SAL_CD_ANCINE
                            orderby b.BIL_DIA_CIN descending, sl.SAL_DESC, s.SEA_DT_HR_INICIO

                            select new MensagensANCINE()
                            {

                                BIL_ID = b.BIL_ID,
                                BIL_DIA_CIN = b.BIL_DIA_CIN,
                                SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                SAL_DESC = sl.SAL_DESC,
                                BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                BIL_PROT = b.BIL_PROT,
                                BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                BIL_RETIF = b.BIL_RETIF,
                                EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                                SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                                SEA_DIS_NM = s.SEA_DIS_NM,
                                SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                SEA_FIL_NM = s.SEA_FIL_NM,
                                SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                                SEA_MODAL = s.SEA_MODAL,
                                SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                                SEA_VRE_CNPJ = s.SEA_VRE_CNPJ
                            }).ToList();

            foreach (var item in listaBil)
            {
                model.Add(new MensagensANCINE()
                {
                    BIL_ID = item.BIL_ID,
                    BIL_DIA_CIN = item.BIL_DIA_CIN,
                    SAL_CD_ANCINE = item.SAL_CD_ANCINE,
                    SAL_DESC = item.SAL_DESC,
                    BIL_HOUVE_SES = item.BIL_HOUVE_SES,
                    BIL_ADIMP_SALA = item.BIL_ADIMP_SALA,
                    BIL_PROT = item.BIL_PROT,
                    BIL_STATUS_PROT = item.BIL_STATUS_PROT,
                    BIL_RETIF = item.BIL_RETIF,
                    EMP_CD_ANCINE = item.EMP_CD_ANCINE,
                    FIL_CD_ANCINE = item.FIL_CD_ANCINE,
                    SEA_DIS_CNPJ = item.SEA_DIS_CNPJ,
                    SEA_DIS_NM = item.SEA_DIS_NM,
                    SEA_DT_HR_INICIO = item.SEA_DT_HR_INICIO,
                    SEA_FIL_NM = item.SEA_FIL_NM,
                    SEA_FIL_TP_PROJECAO = item.SEA_FIL_TP_PROJECAO,
                    SEA_MODAL = item.SEA_MODAL,
                    SEA_RZ_SOCIAL = item.SEA_RZ_SOCIAL,
                    SEA_VRE_CNPJ = item.SEA_VRE_CNPJ
                });
            }

            var TB_BIL_SEN_SESS = (from b in db.TB_BILHETERIA
                                   join sl in db.TB_SALA on b.SAL_CD_ANCINE equals sl.SAL_CD_ANCINE
                                   where b.BIL_HOUVE_SES == "N" && b.BIL_STATUS_PROT != "V" && b.BIL_STATUS_PROT != ""
                                   orderby b.BIL_DIA_CIN descending, sl.SAL_DESC

                                   select new MensagensANCINE()
                                   {
                                       BIL_ID = b.BIL_ID ,
                                       BIL_DIA_CIN = b.BIL_DIA_CIN ,
                                       SAL_CD_ANCINE = b.SAL_CD_ANCINE ,
                                       SAL_DESC = sl.SAL_DESC ,
                                       BIL_HOUVE_SES = b.BIL_HOUVE_SES ,
                                       BIL_ADIMP_SALA = b.BIL_ADIMP_SALA ,
                                       BIL_PROT = b.BIL_PROT ,
                                       BIL_STATUS_PROT = b.BIL_STATUS_PROT ,
                                       BIL_RETIF = b.BIL_RETIF ,
                                       EMP_CD_ANCINE = b.EMP_CD_ANCINE ,
                                       FIL_CD_ANCINE = "" ,
                                       SEA_DIS_CNPJ = 0 ,
                                       SEA_DIS_NM = "" ,
                                       SEA_DT_HR_INICIO = b.BIL_DIA_CIN ,
                                       SEA_FIL_NM = "" ,
                                       SEA_FIL_TP_PROJECAO = "" ,
                                       SEA_MODAL = "" ,
                                       SEA_RZ_SOCIAL = "" ,
                                       SEA_VRE_CNPJ = 0
                                   }).ToList();

            foreach (var item in TB_BIL_SEN_SESS)
            {
                model.Add(new MensagensANCINE()
                {
                    BIL_ID = item.BIL_ID ,
                    BIL_DIA_CIN = item.BIL_DIA_CIN ,
                    SAL_CD_ANCINE = item.SAL_CD_ANCINE ,
                    SAL_DESC = item.SAL_DESC ,
                    BIL_HOUVE_SES = item.BIL_HOUVE_SES ,
                    BIL_ADIMP_SALA = item.BIL_ADIMP_SALA ,
                    BIL_PROT = item.BIL_PROT ,
                    BIL_STATUS_PROT = item.BIL_STATUS_PROT ,
                    BIL_RETIF = item.BIL_RETIF ,
                    EMP_CD_ANCINE = item.EMP_CD_ANCINE ,
                    FIL_CD_ANCINE = item.FIL_CD_ANCINE ,
                    SEA_DIS_CNPJ = item.SEA_DIS_CNPJ ,
                    SEA_DIS_NM = item.SEA_DIS_NM ,
                    SEA_DT_HR_INICIO = item.SEA_DT_HR_INICIO ,
                    SEA_FIL_NM = item.SEA_FIL_NM ,
                    SEA_FIL_TP_PROJECAO = item.SEA_FIL_TP_PROJECAO ,
                    SEA_MODAL = item.SEA_MODAL ,
                    SEA_RZ_SOCIAL = item.SEA_RZ_SOCIAL ,
                    SEA_VRE_CNPJ = item.SEA_VRE_CNPJ
                });
            }

            var myunionQuery = listaBil.Union(TB_BIL_SEN_SESS);

            return View(myunionQuery.ToList());


            //return View(listaBil.ToList());
        }

        //*******************************
        public ActionResult SendReceive()
        {

            var listaBil = (from b in db.TB_BILHETERIA
                            where b.BIL_STATUS_PROT == "N" || b.BIL_STATUS_PROT == "" || (b.BIL_STATUS_PROT == "E" && b.BIL_RETIF == "S") || (b.BIL_STATUS_PROT == "R" && b.BIL_RETIF == "S")

                            group b by new { b.BIL_DIA_CIN, b.SAL_CD_ANCINE, b.BIL_HOUVE_SES, b.BIL_STATUS_PROT, b.BIL_RETIF } into pg
                            join sl in db.TB_SALA on pg.FirstOrDefault().SAL_CD_ANCINE equals sl.SAL_CD_ANCINE
                            select new MensagensANCINE()
                            {
                                BIL_ID = pg.FirstOrDefault().BIL_ID,
                                BIL_DIA_CIN = pg.FirstOrDefault().BIL_DIA_CIN,
                                SAL_CD_ANCINE = pg.FirstOrDefault().SAL_CD_ANCINE,
                                SAL_DESC = sl.SAL_DESC,
                                BIL_HOUVE_SES = pg.FirstOrDefault().BIL_HOUVE_SES,
                                BIL_ADIMP_SALA = pg.FirstOrDefault().BIL_ADIMP_SALA,
                                BIL_PROT = pg.FirstOrDefault().BIL_PROT,
                                BIL_STATUS_PROT = pg.FirstOrDefault().BIL_STATUS_PROT,
                                BIL_RETIF = pg.FirstOrDefault().BIL_RETIF,
                                EMP_CD_ANCINE = pg.FirstOrDefault().EMP_CD_ANCINE
                            }).OrderByDescending(tb => tb.BIL_DIA_CIN).ThenBy(tbsala => tbsala.SAL_DESC);

            foreach (var item in listaBil)
            {
                model.Add(new MensagensANCINE()
                {
                    BIL_ID = item.BIL_ID,
                    BIL_DIA_CIN = item.BIL_DIA_CIN,
                    SAL_CD_ANCINE = item.SAL_CD_ANCINE,
                    SAL_DESC = item.SAL_DESC,
                    BIL_HOUVE_SES = item.BIL_HOUVE_SES,
                    BIL_ADIMP_SALA = item.BIL_ADIMP_SALA,
                    BIL_PROT = item.BIL_PROT,
                    BIL_STATUS_PROT = item.BIL_STATUS_PROT,
                    BIL_RETIF = item.BIL_RETIF,
                    EMP_CD_ANCINE = item.EMP_CD_ANCINE
                });
            }

            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_NM_FANT");

            return View(listaBil.ToList());
        }

        //***************************************************************************************************
        public ActionResult Send(string bIL_DIA_CIN, string sAL_CD_ANCINE, string sal_desc, string houve_ses)
        {
            try
            {
                clsHelper.LogSCB("BilheteriaController - Entrou no método Send");

                Bilheteria objBilheteria = new Bilheteria();
                List<Sessao> listaSessoes = new List<Sessao>();

                DateTime bIL_DIA_CIN_aux = Convert.ToDateTime(bIL_DIA_CIN);

                long tta_idP = 0;
                string cdTipoAssentoP = "";
                int qtdDisponibilizadaP = 0;

                string[] strBil_Id_Inicio;
                string strBil_Id_Inicio_Aux = "";
                int Bil_Id_Inicio = 0;
                int qtdBil_Id_Inicio = 0;

                long tta_idE = 0;
                string cdTipoAssentoE = "";
                int qtdDisponibilizadaE = 0;

                int qtdEspectadoresIntP = 0;
                int qtdEspectadoresMeiaP = 0;
                int qtdEspectadoresCortP = 0;
                int qtdEspectadoresPromP = 0;

                int qtdEspectadoresIntE = 0;
                int qtdEspectadoresMeiaE = 0;
                int qtdEspectadoresCortE = 0;
                int qtdEspectadoresPromE = 0;

                //string strfil_cd_Aux = "";

                string[] TotaisModPagto;
                TotaisModPagto = new string[24];

                DateTime bIL_DIA_CIN_aux2 = Convert.ToDateTime(bIL_DIA_CIN_aux);

                if (houve_ses == "N")
                {
                    var listaBil = from b in db.TB_BILHETERIA

                                   where (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "N" && b.BIL_STATUS_PROT == "" )
                                      || (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "N" && b.BIL_STATUS_PROT == "N" )
                                      || (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "N" && b.BIL_STATUS_PROT == "E" && b.BIL_RETIF == "S")
                                      || (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "N" && b.BIL_STATUS_PROT == "R" && b.BIL_RETIF == "S")
                                   select b;

                    foreach (var Itembil in listaBil)
                    {
                        objBilheteria.registroANCINEExibidor = Convert.ToUInt16(Itembil.EMP_CD_ANCINE);
                        objBilheteria.registroANCINESala = Convert.ToUInt32(Itembil.SAL_CD_ANCINE);
                        objBilheteria.diaCinematografico = Itembil.BIL_DIA_CIN;
                        objBilheteria.houveSessoes = Itembil.BIL_HOUVE_SES;
                        objBilheteria.retificador = Itembil.BIL_RETIF;

                        goto Envio;
                    }

                }

                clsHelper.LogSCB("BilheteriaController - PEGA TODAS DAS RENDAS DO DIA E SALA");

                // PEGA TODAS DAS RENDAS DO DIA E SALA //
                var listaSes = from b in db.TB_BILHETERIA
                           join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID

                               where (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "S" && b.BIL_STATUS_PROT == "" )
                                  || (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "S" && b.BIL_STATUS_PROT == "N" )
                                  || (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "S" && b.BIL_STATUS_PROT == "E" && b.BIL_RETIF == "S")
                                  || (b.BIL_DIA_CIN == bIL_DIA_CIN_aux2 && b.SAL_CD_ANCINE == sAL_CD_ANCINE && b.BIL_HOUVE_SES == "S" && b.BIL_STATUS_PROT == "R" && b.BIL_RETIF == "S")

                               orderby b.BIL_DIA_CIN, b.SAL_CD_ANCINE
                           select new MensagensANCINE()
                           {
                               BIL_ID = b.BIL_ID,
                               BIL_DIA_CIN = b.BIL_DIA_CIN,
                               SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                               BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                               BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                               BIL_PROT = b.BIL_PROT,
                               BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                               BIL_RETIF = b.BIL_RETIF,
                               EMP_CD_ANCINE = b.EMP_CD_ANCINE,

                               SEA_ID = s.SEA_ID,
                               FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                               SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                               SEA_MODAL = s.SEA_MODAL,
                               SEA_FIL_NM = s.SEA_FIL_NM,
                               SEA_FIL_TP_TELA = s.SEA_FIL_TP_TELA,
                               SEA_FIL_DIGITAL = s.SEA_FIL_DIGITAL,
                               SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                               SEA_FIL_AUDIO = s.SEA_FIL_AUDIO,
                               SEA_FIL_LEG = s.SEA_FIL_LEG,
                               SEA_FIL_PRO_LIBRA = s.SEA_FIL_PRO_LIBRA,
                               SEA_FIL_LEG_DESC_CC = s.SEA_FIL_LEG_DESC_CC,
                               SEA_FIL_AUDIO_DESC = s.SEA_FIL_AUDIO_DESC,
                               SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                               SEA_DIS_NM = s.SEA_DIS_NM,
                               SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                               SEA_VRE_CNPJ = s.SEA_VRE_CNPJ
                           };

                foreach (var ses in listaSes)
                {
                    model.Add(new MensagensANCINE()
                    {
                        BIL_ID = ses.BIL_ID ,
                        BIL_DIA_CIN = ses.BIL_DIA_CIN ,
                        SAL_CD_ANCINE = ses.SAL_CD_ANCINE ,
                        BIL_HOUVE_SES = ses.BIL_HOUVE_SES ,
                        BIL_ADIMP_SALA = ses.BIL_ADIMP_SALA ,
                        BIL_PROT = ses.BIL_PROT ,
                        BIL_STATUS_PROT = ses.BIL_STATUS_PROT ,
                        BIL_RETIF = ses.BIL_RETIF ,
                        EMP_CD_ANCINE = ses.EMP_CD_ANCINE ,

                        SEA_ID = ses.SEA_ID ,
                        FIL_CD_ANCINE = ses.FIL_CD_ANCINE ,
                        SEA_DT_HR_INICIO = ses.SEA_DT_HR_INICIO ,
                        SEA_MODAL = ses.SEA_MODAL ,
                        SEA_FIL_NM = ses.SEA_FIL_NM ,
                        SEA_FIL_TP_TELA = ses.SEA_FIL_TP_TELA ,
                        SEA_FIL_DIGITAL = ses.SEA_FIL_DIGITAL ,
                        SEA_FIL_TP_PROJECAO = ses.SEA_FIL_TP_PROJECAO ,
                        SEA_FIL_AUDIO = ses.SEA_FIL_AUDIO ,
                        SEA_FIL_LEG = ses.SEA_FIL_LEG ,
                        SEA_FIL_PRO_LIBRA = ses.SEA_FIL_PRO_LIBRA ,
                        SEA_FIL_LEG_DESC_CC = ses.SEA_FIL_LEG_DESC_CC ,
                        SEA_FIL_AUDIO_DESC = ses.SEA_FIL_AUDIO_DESC ,
                        SEA_DIS_CNPJ = ses.SEA_DIS_CNPJ ,
                        SEA_DIS_NM = ses.SEA_DIS_NM ,
                        SEA_RZ_SOCIAL = ses.SEA_RZ_SOCIAL ,
                        SEA_VRE_CNPJ = ses.SEA_VRE_CNPJ
                    });

                }

                foreach (var ses in listaSes)
                {
                    qtdBil_Id_Inicio++;


                    objBilheteria.registroANCINEExibidor = Convert.ToUInt16(ses.EMP_CD_ANCINE);
                    objBilheteria.registroANCINESala = Convert.ToUInt32(ses.SAL_CD_ANCINE);
                    objBilheteria.diaCinematografico = ses.BIL_DIA_CIN;
                    objBilheteria.houveSessoes = ses.BIL_HOUVE_SES;

                    //if (ses.BIL_RETIF == "N" && (ses.BIL_STATUS_PROT == "N" || ses.BIL_STATUS_PROT == "E" || ses.BIL_STATUS_PROT == "R"))
                    //{
                    //    objBilheteria.retificador = "S";
                    //}
                    //else
                    //{
                        objBilheteria.retificador = ses.BIL_RETIF;
                    //}

                    if(ses.BIL_HOUVE_SES == "N")
                    {
                        ViewBag.ex = "Dia Cinematográfico para a Sala especificada com conflito na informação 'Houve Sessão'.";
                        continue;
                    }

                    // ---------------------------------------------------------------
                    // 1.a.i - SESSAO 1
                    // ---------------------------------------------------------------

                    Sessao sessao1 = new Sessao();

                    long cnpjAux = 0;
                    string[] FormatoHorarioInicio = ses.SEA_DT_HR_INICIO.GetDateTimeFormats();
                    sessao1.dataHoraInicio = FormatoHorarioInicio[47]; //[47]: "2017-04-01 21:00:00"

                    sessao1.modalidade = ses.SEA_MODAL;

                    sessao1.vendedorRemoto = new VendedorRemoto();
                    cnpjAux = Convert.ToUInt32(ses.SEA_VRE_CNPJ);
                    sessao1.vendedorRemoto.cnpj = cnpjAux.ToString("00000000000000");
                    sessao1.vendedorRemoto.razaoSocial = ses.SEA_RZ_SOCIAL;


                    // -----------------------------------------------------
                    // SESSAO 1 - INICIALIZA LISTA DE OBRAS DA SESSAO 1
                    // -----------------------------------------------------
                    List<Obra> listaObrasSessao1 = new List<Obra>();

                    if (listaObrasSessao1 != null)
                    {
                        // ------------------------------------
                        // OBRA 1 DA SESSAO 1
                        // ------------------------------------
                        Obra obra1_da_sessao1 = new Obra();

                        // TRATA OBRA COM CÓDIGO GENÉRICO
                        if (ses.FIL_CD_ANCINE.Substring(0,1) == "G")
                        {
                           // var clsHelper = new Helpers.Helpers();
                            obra1_da_sessao1.numeroObra = clsHelper.FormataCodigoObraGenerica(ses.FIL_CD_ANCINE);
                        }
                        else
                        {
                            obra1_da_sessao1.numeroObra = ses.FIL_CD_ANCINE;
                        }

                        obra1_da_sessao1.tituloObra = ses.SEA_FIL_NM;
                        obra1_da_sessao1.tipoTela = ses.SEA_FIL_TP_TELA;
                        obra1_da_sessao1.digital = ses.SEA_FIL_DIGITAL;
                        obra1_da_sessao1.tipoProjecao = Convert.ToUInt16(ses.SEA_FIL_TP_PROJECAO);
                        obra1_da_sessao1.audio = ses.SEA_FIL_AUDIO;
                        obra1_da_sessao1.legenda = ses.SEA_FIL_LEG;
                        obra1_da_sessao1.libras = ses.SEA_FIL_PRO_LIBRA;
                        obra1_da_sessao1.legendagemDescritiva = ses.SEA_FIL_LEG_DESC_CC;
                        obra1_da_sessao1.audioDescricao = ses.SEA_FIL_AUDIO_DESC;

                        // DISTRIBUIDOR DA OBRA 1
                        obra1_da_sessao1.distribuidor = new Distribuidor();
                        cnpjAux = Convert.ToInt64(ses.SEA_DIS_CNPJ);
                        obra1_da_sessao1.distribuidor.cnpj = cnpjAux.ToString("00000000000000");
                        obra1_da_sessao1.distribuidor.razaoSocial = ses.SEA_DIS_NM;

                        // -----------------------------------------------------
                        // ADICIONA A OBRA 1 DENTRO DA LISTA DE OBRAS DA SESSAO 1                        
                        listaObrasSessao1.Add(obra1_da_sessao1);
                        // -----------------------------------------------------

                    }

                    // PREENCHE O ARRAY DE OBRAS DENTRO DA SESSAO 1
                    sessao1.obras = listaObrasSessao1.ToArray();

                    clsHelper.LogSCB("BilheteriaController - LOTAÇÃO POR TIPO DE ASSENTO - P");

                    // ****************************
                    // LOTAÇÃO POR TIPO DE ASSENTO //
                    var listaTotTpAssP = (from t in db.TB_TOT_TP_ASSENTO
                                          where t.SEA_ID == ses.SEA_ID && t.TTA_TP_ASSENTO == "P" // ASSENTOS PADRÃO
                                          select t).FirstOrDefault();

                    tta_idP = listaTotTpAssP.TTA_ID;
                    cdTipoAssentoP = listaTotTpAssP.TTA_TP_ASSENTO;
                    qtdDisponibilizadaP = listaTotTpAssP.TTA_QTD_DISP;

                    clsHelper.LogSCB("BilheteriaController - LOTAÇÃO POR TIPO DE ASSENTO - E");

                    var listaTotTpAssE = (from t in db.TB_TOT_TP_ASSENTO
                                          where t.SEA_ID == ses.SEA_ID && t.TTA_TP_ASSENTO == "E" // ASSENTOS ESPECIAIS
                                          select t).FirstOrDefault();

                    tta_idE = listaTotTpAssE.TTA_ID;
                    cdTipoAssentoE = listaTotTpAssE.TTA_TP_ASSENTO;
                    qtdDisponibilizadaE = listaTotTpAssE.TTA_QTD_DISP;

                    // ***********************************************
                    // TOTAL CATEGORIA INGRESSO - ASSENTO PADRÃO //
                    // ***********************************************
                    var ListaTotCatIngP = (from t in db.TB_TOT_CATEG_ING
                                           where t.TTA_ID == tta_idP && t.TCI_CAT == 1 // INTEIRA
                                           select t).FirstOrDefault();
                    qtdEspectadoresIntP = ListaTotCatIngP.TCI_QTD_ESPECT;

                    var ListaTotMeioP1 = (from t in db.TB_TOT_MOD_PAGTO
                                          where t.TCI_ID == ListaTotCatIngP.TCI_ID && t.TMP_MOD_PAG == 1
                                          select t).FirstOrDefault();
                    TotaisModPagto[0] = ListaTotMeioP1.TMP_VLR_ARR.ToString();
                    TotaisModPagto[0] = FormataValorXML(TotaisModPagto[0]);

                    var ListaTotMeioP2 = (from t in db.TB_TOT_MOD_PAGTO
                                          where t.TCI_ID == ListaTotCatIngP.TCI_ID && t.TMP_MOD_PAG == 2
                                          select t).FirstOrDefault();
                    TotaisModPagto[1] = ListaTotMeioP2.TMP_VLR_ARR.ToString();
                    TotaisModPagto[1] = FormataValorXML(TotaisModPagto[1]);

                    var ListaTotMeioP3 = (from t in db.TB_TOT_MOD_PAGTO
                                          where t.TCI_ID == ListaTotCatIngP.TCI_ID && t.TMP_MOD_PAG == 3
                                          select t).FirstOrDefault();
                    TotaisModPagto[2] = ListaTotMeioP3.TMP_VLR_ARR.ToString();
                    TotaisModPagto[2] = FormataValorXML(TotaisModPagto[2]);

                    var ListaTotCatIngM = (from t in db.TB_TOT_CATEG_ING
                                           where t.TTA_ID == tta_idP && t.TCI_CAT == 2 // MEIA-ENTRADA
                                           select t).FirstOrDefault();
                    qtdEspectadoresMeiaP = ListaTotCatIngM.TCI_QTD_ESPECT;

                    var ListaTotMeioP4 = (from t in db.TB_TOT_MOD_PAGTO
                                          where t.TCI_ID == ListaTotCatIngM.TCI_ID && t.TMP_MOD_PAG == 1
                                          select t).FirstOrDefault();
                    TotaisModPagto[3] = ListaTotMeioP4.TMP_VLR_ARR.ToString();
                    TotaisModPagto[3] = FormataValorXML(TotaisModPagto[3]);

                    var ListaTotMeioP5 = (from t in db.TB_TOT_MOD_PAGTO
                                          where t.TCI_ID == ListaTotCatIngM.TCI_ID && t.TMP_MOD_PAG == 2
                                          select t).FirstOrDefault();
                    TotaisModPagto[4] = ListaTotMeioP5.TMP_VLR_ARR.ToString();
                    TotaisModPagto[4] = FormataValorXML(TotaisModPagto[4]);

                    var ListaTotMeioP6 = (from t in db.TB_TOT_MOD_PAGTO
                                          where t.TCI_ID == ListaTotCatIngM.TCI_ID && t.TMP_MOD_PAG == 3
                                          select t).FirstOrDefault();
                    TotaisModPagto[5] = ListaTotMeioP6.TMP_VLR_ARR.ToString();
                    TotaisModPagto[5] = FormataValorXML(TotaisModPagto[5]);

                    var ListaTotCatIngC = (from t in db.TB_TOT_CATEG_ING
                                           where t.TTA_ID == tta_idP && t.TCI_CAT == 3 // CORTESIA
                                           select t).FirstOrDefault();
                    qtdEspectadoresCortP = ListaTotCatIngC.TCI_QTD_ESPECT;

                    //var ListaTotMeioP7 = (from t in db.TB_TOT_MOD_PAGTO
                    //                      where t.TCI_ID == ListaTotCatIngC.TCI_ID && t.TMP_MOD_PAG == 1
                    //                      select t).FirstOrDefault();
                    //TotaisModPagto[6] = ListaTotMeioP7.TMP_VLR_ARR.ToString();
                    TotaisModPagto[6] = "0.00";

                    //var ListaTotMeioP8 = (from t in db.TB_TOT_MOD_PAGTO
                    //                      where t.TCI_ID == ListaTotCatIngC.TCI_ID && t.TMP_MOD_PAG == 2
                    //                      select t).FirstOrDefault();
                    //TotaisModPagto[7] = ListaTotMeioP8.TMP_VLR_ARR.ToString();
                    TotaisModPagto[7] = "0.00";

                    //var ListaTotMeioP9 = (from t in db.TB_TOT_MOD_PAGTO
                    //                      where t.TCI_ID == ListaTotCatIngC.TCI_ID && t.TMP_MOD_PAG == 3
                    //                      select t).FirstOrDefault();
                    //TotaisModPagto[8] = ListaTotMeioP9.TMP_VLR_ARR.ToString();
                    TotaisModPagto[8] = "0.00";

                    var ListaTotCatIngPR = (from t in db.TB_TOT_CATEG_ING
                                            where t.TTA_ID == tta_idP && t.TCI_CAT == 4 // PROMOCIONAL
                                            select t).FirstOrDefault();
                    qtdEspectadoresPromP = ListaTotCatIngPR.TCI_QTD_ESPECT;

                    var ListaTotMeioP10 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPR.TCI_ID && t.TMP_MOD_PAG == 1
                                           select t).FirstOrDefault();
                    TotaisModPagto[9] = ListaTotMeioP10.TMP_VLR_ARR.ToString();
                    TotaisModPagto[9] = FormataValorXML(TotaisModPagto[9]);

                    var ListaTotMeioP11 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPR.TCI_ID && t.TMP_MOD_PAG == 2
                                           select t).FirstOrDefault();
                    TotaisModPagto[10] = ListaTotMeioP11.TMP_VLR_ARR.ToString();
                    TotaisModPagto[10] = FormataValorXML(TotaisModPagto[10]);

                    var ListaTotMeioP12 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPR.TCI_ID && t.TMP_MOD_PAG == 3
                                           select t).FirstOrDefault();
                    TotaisModPagto[11] = ListaTotMeioP12.TMP_VLR_ARR.ToString();
                    TotaisModPagto[11] = FormataValorXML(TotaisModPagto[11]);

                    // ***********************************************
                    // TOTAL CATEGORIA INGRESSO - ASSENTO ESPECIAL //
                    // ***********************************************
                    var ListaTotCatIngPE = (from t in db.TB_TOT_CATEG_ING
                                            where t.TTA_ID == tta_idE && t.TCI_CAT == 1 // INTEIRA
                                            select t).FirstOrDefault();
                    qtdEspectadoresIntE = ListaTotCatIngPE.TCI_QTD_ESPECT;

                    var ListaTotMeioP13 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPE.TCI_ID && t.TMP_MOD_PAG == 1
                                           select t).FirstOrDefault();
                    TotaisModPagto[12] = ListaTotMeioP13.TMP_VLR_ARR.ToString();
                    TotaisModPagto[12] = FormataValorXML(TotaisModPagto[12]);

                    var ListaTotMeioP14 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotMeioP13.TCI_ID && t.TMP_MOD_PAG == 2
                                           select t).FirstOrDefault();
                    TotaisModPagto[13] = ListaTotMeioP14.TMP_VLR_ARR.ToString();
                    TotaisModPagto[13] = FormataValorXML(TotaisModPagto[13]);

                    var ListaTotMeioP15 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotMeioP13.TCI_ID && t.TMP_MOD_PAG == 3
                                           select t).FirstOrDefault();
                    TotaisModPagto[14] = ListaTotMeioP15.TMP_VLR_ARR.ToString();
                    TotaisModPagto[14] = FormataValorXML(TotaisModPagto[14]);

                    var ListaTotCatIngME = (from t in db.TB_TOT_CATEG_ING
                                            where t.TTA_ID == tta_idE && t.TCI_CAT == 2 // MEIA-ENTRADA
                                            select t).FirstOrDefault();
                    qtdEspectadoresMeiaE = ListaTotCatIngME.TCI_QTD_ESPECT;

                    var ListaTotMeioP16 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngME.TCI_ID && t.TMP_MOD_PAG == 1
                                           select t).FirstOrDefault();
                    TotaisModPagto[15] = ListaTotMeioP16.TMP_VLR_ARR.ToString();
                    TotaisModPagto[15] = FormataValorXML(TotaisModPagto[15]);

                    var ListaTotMeioP17 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngME.TCI_ID && t.TMP_MOD_PAG == 2
                                           select t).FirstOrDefault();
                    TotaisModPagto[16] = ListaTotMeioP17.TMP_VLR_ARR.ToString();
                    TotaisModPagto[16] = FormataValorXML(TotaisModPagto[16]);

                    var ListaTotMeioP18 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngME.TCI_ID && t.TMP_MOD_PAG == 3
                                           select t).FirstOrDefault();
                    TotaisModPagto[17] = ListaTotMeioP18.TMP_VLR_ARR.ToString();
                    TotaisModPagto[17] = FormataValorXML(TotaisModPagto[17]);

                    var ListaTotCatIngCE = (from t in db.TB_TOT_CATEG_ING
                                            where t.TTA_ID == tta_idE && t.TCI_CAT == 3 // CORTESIA
                                            select t).FirstOrDefault();
                    qtdEspectadoresCortE = ListaTotCatIngCE.TCI_QTD_ESPECT;

                    //var ListaTotMeioP19 = (from t in db.TB_TOT_MOD_PAGTO
                    //                       where t.TCI_ID == ListaTotCatIngCE.TCI_ID && t.TMP_MOD_PAG == 1
                    //                       select t).FirstOrDefault();
                    //TotaisModPagto[18] = ListaTotMeioP19.TMP_VLR_ARR.ToString();
                    TotaisModPagto[18] = "0.00";

                    //var ListaTotMeioP20 = (from t in db.TB_TOT_MOD_PAGTO
                    //                       where t.TCI_ID == ListaTotCatIngCE.TCI_ID && t.TMP_MOD_PAG == 2
                    //                       select t).FirstOrDefault();
                    //TotaisModPagto[19] = ListaTotMeioP20.TMP_VLR_ARR.ToString();
                    TotaisModPagto[19] = "0.00";

                    //var ListaTotMeioP21 = (from t in db.TB_TOT_MOD_PAGTO
                    //                       where t.TCI_ID == ListaTotCatIngCE.TCI_ID && t.TMP_MOD_PAG == 3
                    //                       select t).FirstOrDefault();
                    //TotaisModPagto[20] = ListaTotMeioP21.TMP_VLR_ARR.ToString();
                    TotaisModPagto[20] = "0.00";

                    var ListaTotCatIngPRE = (from t in db.TB_TOT_CATEG_ING
                                             where t.TTA_ID == tta_idE && t.TCI_CAT == 4 // PROMOCIONAL
                                             select t).FirstOrDefault();
                    qtdEspectadoresPromE = ListaTotCatIngPRE.TCI_QTD_ESPECT;

                    var ListaTotMeioP22 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPRE.TCI_ID && t.TMP_MOD_PAG == 1
                                           select t).FirstOrDefault();
                    TotaisModPagto[21] = ListaTotMeioP22.TMP_VLR_ARR.ToString();
                    TotaisModPagto[21] = FormataValorXML(TotaisModPagto[21]);

                    var ListaTotMeioP23 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPRE.TCI_ID && t.TMP_MOD_PAG == 2
                                           select t).FirstOrDefault();
                    TotaisModPagto[22] = ListaTotMeioP23.TMP_VLR_ARR.ToString();
                    TotaisModPagto[22] = FormataValorXML(TotaisModPagto[22]);

                    var ListaTotMeioP24 = (from t in db.TB_TOT_MOD_PAGTO
                                           where t.TCI_ID == ListaTotCatIngPRE.TCI_ID && t.TMP_MOD_PAG == 3
                                           select t).FirstOrDefault();
                    TotaisModPagto[23] = ListaTotMeioP24.TMP_VLR_ARR.ToString();
                    TotaisModPagto[23] = FormataValorXML(TotaisModPagto[23]);

                    // -----------------------------------------------------
                    // SESSAO 1 - ADICIONA OS DADOS DE TOTALIZAÇÃO PARA A SESSÃO 1
                    // -----------------------------------------------------
                    sessao1.totalizacoesTipoAssento = new TotalizacaoTipoAssento[] {

                            // ------------------------------------
                            // TOTALIZACAO TIPO ASSENTO "P"
                            // ------------------------------------

                            new TotalizacaoTipoAssento
                            {

                                codigoTipoAssento = "P",
                                quantidadeDisponibilizada = Convert.ToUInt16(qtdDisponibilizadaP),
                                totalizacoesCategoriaIngresso = new TotalizacaoCategoriaIngresso[]
                                {
                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 1, // INTEIRA
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresIntP) ,
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[0])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[1])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[2])
                                            }
                                        }
                                    },

                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 2, // MEIA-ENTRADA
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresMeiaP),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[3])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[4])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[5])
                                            }
                                        }
                                    },

                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 3, // CORTESIA
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresCortP),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[6])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[7])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[8])
                                            }
                                        }
                                    },

                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 4, // PROMOCIONAL
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresPromP),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[9])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[10])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[11])
                                            }
                                        }
                                    }
                                }
                            },

                            // ------------------------------------
                            // TOTALIZACAO TIPO ASSENTO "E"
                            // ------------------------------------
                            new TotalizacaoTipoAssento
                            {
                                codigoTipoAssento = "E",
                                quantidadeDisponibilizada = Convert.ToUInt16(qtdDisponibilizadaE),
                                totalizacoesCategoriaIngresso = new TotalizacaoCategoriaIngresso[]
                                {
                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 1, // INTEIRA
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresIntE),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[12])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[13])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[14])
                                            }
                                        }
                                    },

                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 2, // MEIA-ENTRADA
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresMeiaE),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[15])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[16])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[17])
                                            }
                                        }
                                    },

                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 3, // CORTESIA
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresCortE),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[18])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[19])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[20])
                                            }
                                        }
                                    },

                                    new TotalizacaoCategoriaIngresso
                                    {
                                        codigoCategoriaIngresso = 4, // PROMOCIONAL
                                        quantidadeEspectadores = Convert.ToUInt16(qtdEspectadoresPromE),
                                        totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                        {
                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 1, // MEIO TRADICIONAL
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[21])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 2, // VALE CULTURA
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[22])
                                            },

                                            new TotalizacaoModalidadePagamento
                                            {
                                                codigoModalidadePagamento = 3, // OUTROS
                                                valorArrecadado = Convert.ToDecimal(TotaisModPagto[23])
                                            }
                                        }
                                    }
                                }
                            }

                        };


                    // ADICIONA NA LISTA DE SESSOES
                    listaSessoes.Add(sessao1);
                }


                // PREENCHE O ARRAY DE SESSOES
                objBilheteria.sessoes = listaSessoes.ToArray();

                Envio:

                // BUSCA OS PARAMETROS NO APP.CONFIG, ou DEPOIS PODE BUSCAR EM ALGUMA TABELA DE PARAMETROS GLOBAIS
                string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
                string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

                // AQUI VOCÊ INSTANCIA O OBJETO 'MANAGER' DO SERVIÇO, PARA DEPOIS CHAMAR O MÉTODO DESEJADO
                // - VOCÊ JÁ TEM QUE ENVIAR A URL E O TOKEN
                SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);

                // AQUI VOCÊ CHAMA O MÉTODO, PASSANDO COMO PARAMETRO O OBJETO 'BILHETERIA' JÁ PREENCHIDO
                clsHelper.LogSCB("Send - MÉTODO objSCBIntegrationManager - BilheteriaController");
                StatusRelatorioBilheteria objReturn = objSCBIntegrationManager.RegistroBilheteriaSalaExibicao(objBilheteria);

                // VALIDA SE O RETORNO NÃO É NULO
                if (objReturn != null)
                {

                    string[] stringSeparators = new string[] { "," };
                    string[] strBilIds;
                    string strBilIdsAux = "";
                    string[] strProt;
                    string strProtAux = "";
                    string[] strStProt;
                    string strStProtAux = "";

                    //foreach (var objReturn2 in objReturn)
                    //{

                    //}



                    // EXIBE POSSIVEIS MENSAGENS DE RETORNO: I - Informativa; A - Alerta; E - Erro
                    if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                    {
                        foreach (var msg in objReturn.mensagens)
                        {
                            string emp_cd = objReturn.registroANCINEExibidor.ToString();
                            string sal_cd = objReturn.registroANCINESala.ToString();
                            DateTime dia_cin = objReturn.diaCinematografico;

                            // MENSAGEM COM SESSÃO ESPECIFICADA.
                            if (msg.dataHoraInicio != null)
                            {
                                DateTime dtHoraIni = Convert.ToDateTime(msg.dataHoraInicio);

                                var listaBilMsg = (from b in db.TB_BILHETERIA
                                                   join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID

                                                   where (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni && b.BIL_STATUS_PROT == "")
                                                   || (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni && b.BIL_STATUS_PROT == "N")
                                                   || (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni && b.BIL_STATUS_PROT == "E"  && b.BIL_RETIF == "S")
                                                   || (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni && b.BIL_STATUS_PROT == "R"  && b.BIL_RETIF == "S")

                                                   select new MensagensANCINE()
                                                   {
                                                       BIL_ID = b.BIL_ID,
                                                       BIL_DIA_CIN = b.BIL_DIA_CIN,
                                                       SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                                       BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                                       BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                                       BIL_PROT = b.BIL_PROT,
                                                       BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                                       BIL_RETIF = b.BIL_RETIF,
                                                       EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                                       SEA_ID = s.SEA_ID,
                                                       FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                                                       SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                                                       SEA_DIS_NM = s.SEA_DIS_NM,
                                                       SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                                       SEA_FIL_NM = s.SEA_FIL_NM,
                                                       SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                                                       SEA_MODAL = s.SEA_MODAL,
                                                       SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                                                       SEA_VRE_CNPJ = s.SEA_VRE_CNPJ
                                                   });

                                foreach (var itemMsg in listaBilMsg)
                                {

                                    TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
                                    TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                    TB_MSG.MSA_DT_MSG = DateTime.Now;
                                    TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                    TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                    TB_MSG.SEA_ID = itemMsg.SEA_ID;
                                    TB_MSG.SEA_DT_HR_INICIO = itemMsg.SEA_DT_HR_INICIO;
                                    TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                    TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                    TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                    db.TB_MENSAGEM_ANCINE.Add(TB_MSG);

                                    if (strBilIdsAux == "") { strBilIdsAux = itemMsg.BIL_ID.ToString(); }
                                    else { strBilIdsAux = strBilIdsAux + "," + itemMsg.BIL_ID.ToString(); }

                                    if (strStProtAux == "") { strStProtAux = objReturn.statusProtocolo; }
                                    else { strStProtAux = strStProtAux + "," + objReturn.statusProtocolo; }

                                    if (strProtAux == "")
                                    { strProtAux = (objReturn.numeroProtocolo == null ? "0": objReturn.numeroProtocolo); }
                                    else
                                    { strProtAux = strProtAux + "," + (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }

                                    if (strBil_Id_Inicio_Aux == "")
                                    {
                                        strBil_Id_Inicio_Aux = itemMsg.BIL_ID.ToString();
                                    }
                                    else
                                    {
                                        strBil_Id_Inicio_Aux = strBil_Id_Inicio_Aux + "," + itemMsg.BIL_ID.ToString();
                                    }

                                }
                                db.SaveChanges();

                                strBilIds = strBilIdsAux.Split(stringSeparators ,StringSplitOptions.None);
                                strStProt = strStProtAux.Split(stringSeparators ,StringSplitOptions.None);
                                strProt = strProtAux.Split(stringSeparators ,StringSplitOptions.None);
                                
                                TrataXMLSend(strBilIds ,strStProt ,strProt ,db);

                                strBilIdsAux = "";
                                strStProtAux = "";
                                strProtAux = "";

                            }

                            // ERRO SEM SESSÃO ESPECÍFICA.
                            else
                            {
                                var listaBilERRO = from b in db.TB_BILHETERIA

                                               where (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && b.BIL_STATUS_PROT == "")
                                               || (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && b.BIL_STATUS_PROT == "N")
                                               || (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && b.BIL_STATUS_PROT ==  "E"  && b.BIL_RETIF == "S")
                                               || (b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && b.BIL_STATUS_PROT ==  "R"  && b.BIL_RETIF == "S")

                                                   select new MensagensANCINE()
                                                {
                                                    BIL_ID = b.BIL_ID ,
                                                    BIL_DIA_CIN = b.BIL_DIA_CIN ,
                                                    SAL_CD_ANCINE = b.SAL_CD_ANCINE ,
                                                    BIL_HOUVE_SES = b.BIL_HOUVE_SES ,
                                                    BIL_ADIMP_SALA = b.BIL_ADIMP_SALA ,
                                                    BIL_PROT = b.BIL_PROT ,
                                                    BIL_STATUS_PROT = b.BIL_STATUS_PROT ,
                                                    BIL_RETIF = b.BIL_RETIF ,
                                                    EMP_CD_ANCINE = b.EMP_CD_ANCINE ,
                                                };


                                foreach (var itemMsg in listaBilERRO)
                                {
                                    TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
                                    TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                    TB_MSG.MSA_DT_MSG = DateTime.Now;
                                    TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                    TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                    TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                    TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                    TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                    db.TB_MENSAGEM_ANCINE.Add(TB_MSG);

                                    if (strBilIdsAux == "") {strBilIdsAux = itemMsg.BIL_ID.ToString();}
                                    else {strBilIdsAux = strBilIdsAux + "," + itemMsg.BIL_ID.ToString();}

                                    if(strStProtAux == "") { strStProtAux = objReturn.statusProtocolo; }
                                    else { strStProtAux = strStProtAux + "," + objReturn.statusProtocolo; }

                                    if (strProtAux == "") { strProtAux = (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }
                                    else {strProtAux = strProtAux + "," + (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }

                                    strBilIds = strBilIdsAux.Split(stringSeparators ,StringSplitOptions.None);
                                    strStProt = strStProtAux.Split(stringSeparators ,StringSplitOptions.None);
                                    strProt = strProtAux.Split(stringSeparators ,StringSplitOptions.None);
                                    
                                    TrataXMLSend(strBilIds ,strStProt ,strProt ,db);
                                }
                                db.SaveChanges();
                            }

                        }

                        ViewBag.registroANCINESala = sal_desc;
                        ViewBag.diaCinematografico = objReturn.diaCinematografico.ToShortDateString();
                        ViewBag.numeroProtocolo = objReturn.numeroProtocolo;
                        ViewBag.statusProtocolo = objReturn.statusProtocolo;

                        string emp_cd_ok = objReturn.registroANCINEExibidor.ToString();
                        string sal_cd_ok = objReturn.registroANCINESala.ToString();
                        DateTime dia_cin_ok = objReturn.diaCinematografico;

                        strBil_Id_Inicio = strBil_Id_Inicio_Aux.Split(stringSeparators ,StringSplitOptions.None);

                        var listaBil_ok = (from b in db.TB_BILHETERIA

                                           where (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "")
                                           || (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "N")
                                           || (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "E" && b.BIL_RETIF == "S")
                                           || (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "R" && b.BIL_RETIF == "S")

                                           select b);
                        foreach (var item in listaBil_ok)
                        {

                            //if (strBil_Id_Inicio_Aux.IndexOf(item.BIL_ID.ToString()) != 0)
                            //{
                                if (strBilIdsAux == "") { strBilIdsAux = item.BIL_ID.ToString(); }
                                else { strBilIdsAux = strBilIdsAux + "," + item.BIL_ID.ToString(); }

                                if (strStProtAux == "") { strStProtAux = objReturn.statusProtocolo; }
                                else { strStProtAux = strStProtAux + "," + objReturn.statusProtocolo; }

                                if (strProtAux == "") { strProtAux = (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }
                                else { strProtAux = strProtAux + "," + (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }
                            //}
                        }

                        strBilIds = strBilIdsAux.Split(stringSeparators ,StringSplitOptions.None);
                        strStProt = strStProtAux.Split(stringSeparators ,StringSplitOptions.None);
                        strProt = strProtAux.Split(stringSeparators ,StringSplitOptions.None);
                        TrataXMLSend(strBilIds ,strStProt ,strProt ,db);

                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.registroANCINESala = sal_desc;
                        ViewBag.diaCinematografico = objReturn.diaCinematografico.ToShortDateString();
                        ViewBag.numeroProtocolo = objReturn.numeroProtocolo;
                        ViewBag.statusProtocolo = objReturn.statusProtocolo;

                        string emp_cd_ok = objReturn.registroANCINEExibidor.ToString();
                        string sal_cd_ok = objReturn.registroANCINESala.ToString();
                        DateTime dia_cin_ok = objReturn.diaCinematografico;

                        var listaBil_ok = (from b in db.TB_BILHETERIA

                                           where (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "")
                                           || (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "N")
                                           || (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "E" && b.BIL_RETIF == "S")
                                           || (b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok && b.BIL_STATUS_PROT == "R" && b.BIL_RETIF == "S")

                                           select b);
                        foreach (var item in listaBil_ok)
                        {
                            TB_BILHETERIA TB_BIL_OK = db.TB_BILHETERIA.Find(item.BIL_ID);
                            TB_BIL_OK.BIL_PROT = objReturn.numeroProtocolo;
                            TB_BIL_OK.BIL_STATUS_PROT = objReturn.statusProtocolo;
                            db.Entry(TB_BIL_OK).State = EntityState.Modified;
                        }
                        db.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {
                //switch (ex.Number)
                //{
                //    case 2601:
                //    db.SaveChanges();
                //    break;

                //    default:
                //    throw;
                //}

                ViewBag.ex = ex.Message;
                clsHelper.LogSCB("Send - BilheteriaController: " + ex.Message);
                return View();
            }

            return View();


        }

        //*********************************************************
        public ActionResult DiaSemSessao(FormCollection collection)
        {
            try
            {
                var objBil = new TB_BILHETERIA();
                string[] stringSeparators = new string[] { "," };

                foreach (var key in collection.AllKeys)
                {
                    var value = collection[key];

                    if (key == "BIL_DIA_CIN") { objBil.BIL_DIA_CIN = Convert.ToDateTime(value); }
                    if (key == "EMP_CD_ANCINE") { objBil.EMP_CD_ANCINE = value; }
                    if (key == "SAL_CD_ANCINE") { objBil.SAL_CD_ANCINE = value; }
                }

                objBil.BIL_HOUVE_SES = "N";
                objBil.BIL_RETIF = "N";

                db.TB_BILHETERIA.Add(objBil);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex;
                return RedirectToAction("Index");
            }
        }

        //**************************
        public ActionResult Create()
        {
            DateTime bil_dia_cin;
            var bil_exists = from bil in db.TB_BILHETERIA
                             where bil.BIL_ID != 0
                             select bil;

            if (bil_exists.Any())
            {

                bil_dia_cin = db.TB_BILHETERIA.Max(max => max.BIL_DIA_CIN);
            }
            else
            {
                bil_dia_cin = DateTime.Today;
            }

            //ViewBag.bil_dia_cin = bil_dia_cin.ToShortDateString();
            TempData["bil_dia_cin"] = bil_dia_cin.ToShortDateString();
            ViewBag.FIL_CD_ANCINE = new SelectList(db.TB_FILME.OrderBy(t => t.FIL_NM) , "FIL_CD_ANCINE", "FIL_NM");
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO.OrderBy(t => t.EMP_NM_FANT) , "EMP_CD_ANCINE", "EMP_NM_FANT");

            return View();
        }

        [HttpPost]
        //***************************************************
        public ActionResult Create(FormCollection collection)
        {
            using (var context = new SCBEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var objBil = new TB_BILHETERIA();
                        var objSess = new TB_SESSAO_ANCINE();

                        //ASSENTO PADRÃO
                        var objTotCat = new TB_TOT_CATEG_ING();
                        var objTotCat2 = new TB_TOT_CATEG_ING();
                        var objTotCat3 = new TB_TOT_CATEG_ING();
                        var objTotCat4 = new TB_TOT_CATEG_ING();

                        //ASSENTOS ESPECIAL
                        var objTotCat5 = new TB_TOT_CATEG_ING();
                        var objTotCat6 = new TB_TOT_CATEG_ING();
                        var objTotCat7 = new TB_TOT_CATEG_ING();
                        var objTotCat8 = new TB_TOT_CATEG_ING();

                        var objTotMod = new TB_TOT_MOD_PAGTO();
                        var objTotMod2 = new TB_TOT_MOD_PAGTO();
                        var objTotMod3 = new TB_TOT_MOD_PAGTO();

                        var objTotTpAss = new TB_TOT_TP_ASSENTO();
                        var objTotTpAss2 = new TB_TOT_TP_ASSENTO();

                        string[] stringSeparators = new string[] { "," };

                        string strTCI_QTD_ESPECT_AUX = "";
                        string strTMP_VLR_ARR_TRAD_AUX = "";
                        string strTMP_VLR_ARR_VC_AUX = "";
                        string strTMP_VLR_ARR_OUT_AUX = "";

                        string strSAL_QTD_LUG_PDR_AUX = "";
                        string strSAL_QTD_LUG_PDR_ESP = "";

                        string[] strTCI_QTD_ESPECT;
                        string[] strTMP_VLR_ARR_TRAD;
                        string[] strTMP_VLR_ARR_VC;
                        string[] strTMP_VLR_ARR_OUT;
                        string DataAux = "";

                        string SalvarVoltar = "";
                        string DiasemSessao = "";

                        foreach (var key in collection.AllKeys)
                        {
                            var value = collection[key];

                            //.......TB_BILHETERIA........//
                            if (key == "BIL_DIA_CIN") { objBil.BIL_DIA_CIN = Convert.ToDateTime(value); }
                            if (key == "EMP_CD_ANCINE") { objBil.EMP_CD_ANCINE = value; }
                            if (key == "SAL_CD_ANCINE") { objBil.SAL_CD_ANCINE = value; }
                            if (key == "BIL_HOUVE_SES") { objBil.BIL_HOUVE_SES = value; }
                            if (key == "BIL_RETIF") { objBil.BIL_RETIF = value; }

                            //..........TB_SESSAO.........//
                            if (key == "SAL_CD_ANCINE") { objSess.SAL_CD_ANCINE = value; }
                            if (key == "FIL_CD_ANCINE") { objSess.FIL_CD_ANCINE = value; }
                            if (key == "SEA_DT_HR_INICIO")
                            {
                                DataAux = objBil.BIL_DIA_CIN.ToShortDateString();
                                objSess.SEA_DT_HR_INICIO = Convert.ToDateTime(DataAux + " " + value);
                            }
                            if (key == "SEA_MODAL") { objSess.SEA_MODAL = value; }
                            if (key == "SEA_FIL_TP_TELA") { objSess.SEA_FIL_TP_TELA = value; }
                            if (key == "SEA_FIL_DIGITAL") { objSess.SEA_FIL_DIGITAL = value; }
                            if (key == "SEA_FIL_TP_PROJECAO") { objSess.SEA_FIL_TP_PROJECAO = value; }
                            if (key == "SEA_FIL_AUDIO") { objSess.SEA_FIL_AUDIO = value; }
                            if (key == "SEA_FIL_LEG") { objSess.SEA_FIL_LEG = value; }
                            if (key == "SEA_FIL_PRO_LIBRA") { objSess.SEA_FIL_PRO_LIBRA = value; }
                            if (key == "SEA_FIL_LEG_DESC_CC") { objSess.SEA_FIL_LEG_DESC_CC = value; }
                            if (key == "SEA_FIL_AUDIO_DESC") { objSess.SEA_FIL_AUDIO_DESC = value; }
                            if (key == "VRE_CNPJ") { objSess.SEA_VRE_CNPJ = Convert.ToInt64(value); }

                            //............TB_TOT_CATEG_ING...........//
                            //if (key == "TCI_QTD_ESPECT") { strTCI_QTD_ESPECT = value.Split(stringSeparators, StringSplitOptions.None); }
                            if (key == "TCI_QTD_ESPECT") { strTCI_QTD_ESPECT_AUX = value; }
                            if (key == "TMP_VLR_ARR_TRAD") { strTMP_VLR_ARR_TRAD_AUX = value; }
                            if (key == "TMP_VLR_ARR_VC") { strTMP_VLR_ARR_VC_AUX = value; }
                            if (key == "TMP_VLR_ARR_OUT") { strTMP_VLR_ARR_OUT_AUX = value; }

                            if (key == "SAL_QTD_LUG_PDR") { strSAL_QTD_LUG_PDR_AUX = value; }
                            if (key == "SAL_QTD_LUG_ESP") { strSAL_QTD_LUG_PDR_ESP = value; }

                            if (key == "txtSalvarVoltar") { SalvarVoltar = value; }
                            if (key == "txtDiasemSessao") { DiasemSessao = value; }
                        }

                        // *************************** DIAS SEM SESSÃO ***************************//

                        if(DiasemSessao == "S")
                        {
                            objBil.BIL_HOUVE_SES = "N";

                            //.......TB_BILHETERIA...........//
                            objBil.BIL_ADIMP_SALA = "N";
                            objBil.BIL_STATUS_PROT = "";
                            objBil.BIL_PROT = "";

                            context.TB_BILHETERIA.Add(objBil);
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            return RedirectToAction("Index");

                        }



                        //.......TB_BILHETERIA...........//
                        objBil.BIL_ADIMP_SALA = "N";
                        objBil.BIL_STATUS_PROT = "";
                        objBil.BIL_PROT = "";

                        context.TB_BILHETERIA.Add(objBil);
                        context.SaveChanges();

                        //...........TB_SESSAO_ANCINE.........../

                        var filnm = context.TB_FILME.Find(objSess.FIL_CD_ANCINE);
                        var dist = context.TB_DISTRIBUIDORA.Find(filnm.DIS_CNPJ);
                        var vrem = context.TB_VENDEDOR_REMOTO.Find(objSess.SEA_VRE_CNPJ);

                        objSess.BIL_ID = objBil.BIL_ID;

                        objSess.SEA_FIL_NM = filnm.FIL_NM;
                        objSess.SEA_DIS_CNPJ = dist.DIS_CNPJ;
                        objSess.SEA_DIS_NM = dist.DIS_NM;
                        if (vrem != null)
                        {
                            objSess.SEA_VRE_CNPJ = vrem.VRE_CNPJ;
                            objSess.SEA_RZ_SOCIAL = vrem.VRE_RZ_SOCIAL;
                        }
                        else
                        {
                            objSess.SEA_VRE_CNPJ = null;
                            objSess.SEA_RZ_SOCIAL = null;
                        }

                        context.TB_SESSAO_ANCINE.Add(objSess);
                        context.SaveChanges();


                        strTCI_QTD_ESPECT = strTCI_QTD_ESPECT_AUX.Split(stringSeparators, StringSplitOptions.None);
                        strTMP_VLR_ARR_TRAD = strTMP_VLR_ARR_TRAD_AUX.Split(stringSeparators, StringSplitOptions.None);
                        strTMP_VLR_ARR_VC = strTMP_VLR_ARR_VC_AUX.Split(stringSeparators, StringSplitOptions.None);
                        strTMP_VLR_ARR_OUT = strTMP_VLR_ARR_OUT_AUX.Split(stringSeparators, StringSplitOptions.None);

                        TB_SALA tb_sala = db.TB_SALA.Find(objBil.SAL_CD_ANCINE);

                        if (tb_sala != null)
                        {
                            strSAL_QTD_LUG_PDR_AUX = tb_sala.SAL_QTD_LUG_PDR.ToString();
                            strSAL_QTD_LUG_PDR_ESP = tb_sala.SAL_QTD_LUG_ESP.ToString();
                        }
                        else
                        {
                            strSAL_QTD_LUG_PDR_AUX = "0";
                            strSAL_QTD_LUG_PDR_ESP = "0";
                        }

                        int i = 0;

                        foreach (var valor in strTCI_QTD_ESPECT)
                        {
                            if (valor == "")
                            {
                                strTCI_QTD_ESPECT[i] = "0";
                            }
                            i = i + 1;
                        }
                        i = 0;

                        foreach (var valor in strTMP_VLR_ARR_TRAD)
                        {
                            if (valor == "")
                            {
                                strTMP_VLR_ARR_TRAD[i] = "0";
                            }
                            i = i + 1;
                        }

                        i = 0;

                        foreach (var valor in strTMP_VLR_ARR_VC)
                        {
                            if (valor == "")
                            {
                                strTMP_VLR_ARR_VC[i] = "0";
                            }
                            i = i + 1;
                        }

                        i = 0;

                        foreach (var valor in strTMP_VLR_ARR_OUT)
                        {
                            if (valor == "")
                            {
                                strTMP_VLR_ARR_OUT[i] = "0";
                            }
                            i = i + 1;
                        }

                        //......TB_TOT_TP_ASSENTO........//
                        // LUGARES PADRÃO.
                        objTotTpAss.SEA_ID = objSess.SEA_ID;
                        objTotTpAss.TTA_TP_ASSENTO = "P";
                        objTotTpAss.TTA_QTD_DISP = Convert.ToInt16(strSAL_QTD_LUG_PDR_AUX);

                        context.TB_TOT_TP_ASSENTO.Add(objTotTpAss);
                        context.SaveChanges();

                        //.........TB_TOT_CATEG_ING..........//
                        objTotCat.TTA_ID = objTotTpAss.TTA_ID;

                    // TOTAIS CATEGORIA INTEIRA
                        objTotCat.TCI_CAT = 1;
                        objTotCat.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[0]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_TRAD[0]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_VC[0]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_OUT[0]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                    // TOTAIS CATEGORIA MEIA-ENTRADA
                        objTotCat2.TTA_ID = objTotTpAss.TTA_ID;
                        objTotCat2.TCI_CAT = 2;
                        objTotCat2.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[1]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat2);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat2.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_TRAD[1]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_VC[1]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_OUT[1]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();


                    // TOTAIS CATEGORIA PROMOCIONAL
                        objTotCat3.TTA_ID = objTotTpAss.TTA_ID;
                        objTotCat3.TCI_CAT = 4;
                        objTotCat3.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[2]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat3);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat3.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_TRAD[2]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_VC[2]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_OUT[2]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();


                    // TOTAIS CATEGORIA CORTESIAS
                        objTotCat4.TTA_ID = objTotTpAss.TTA_ID;
                        objTotCat4.TCI_CAT = 3;
                        objTotCat4.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[3]);
                        objTotCat4.TCI_ID = 0;

                        context.TB_TOT_CATEG_ING.Add(objTotCat4);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat4.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = 0;

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = 0;

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = 0;

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();


                        //**************************************************************************************************
                        //......TB_TOT_TP_ASSENTO........//
                        // LUGARES ESPECIAIS.
                        objTotTpAss2.SEA_ID = objSess.SEA_ID;
                        objTotTpAss2.TTA_TP_ASSENTO = "E";
                        objTotTpAss2.TTA_QTD_DISP = Convert.ToInt16(strSAL_QTD_LUG_PDR_ESP);

                        context.TB_TOT_TP_ASSENTO.Add(objTotTpAss2);
                        context.SaveChanges();

                        //.........TB_TOT_CATEG_ING..........//
                        objTotCat.TTA_ID = objTotTpAss2.TTA_ID;

                    // TOTAIS CATEGORIA INTEIRA
                        objTotCat.TCI_CAT = 1;
                        objTotCat.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[4]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_TRAD[3]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_VC[3]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_OUT[3]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                    // TOTAIS CATEGORIA MEIA-ENTRADA
                        objTotCat2.TTA_ID = objTotTpAss2.TTA_ID;
                        objTotCat2.TCI_CAT = 2;
                        objTotCat2.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[5]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat2);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat2.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_TRAD[4]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_VC[4]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_OUT[4]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();


                    // TOTAIS CATEGORIA PROMOCIONAL
                        objTotCat3.TTA_ID = objTotTpAss2.TTA_ID;
                        objTotCat3.TCI_CAT = 4;
                        objTotCat3.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[6]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat3);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat3.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_TRAD[5]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_VC[5]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = Convert.ToDouble(strTMP_VLR_ARR_OUT[5]);

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();


                    // TOTAIS CATEGORIA CORTESIAS
                        objTotCat4.TTA_ID = objTotTpAss2.TTA_ID;
                        objTotCat4.TCI_CAT = 3;
                        objTotCat4.TCI_QTD_ESPECT = Convert.ToInt16(strTCI_QTD_ESPECT[7]);

                        context.TB_TOT_CATEG_ING.Add(objTotCat4);
                        context.SaveChanges();

                        objTotMod.TCI_ID = objTotCat4.TCI_ID;

                        // TOTAL MODALIDADE DE PAGTO.
                        // MEIO TRADICIONAL
                        objTotMod.TMP_MOD_PAG = 1;
                        objTotMod.TMP_VLR_ARR = 0;

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // VALE CULTURA
                        objTotMod.TMP_MOD_PAG = 2;
                        objTotMod.TMP_VLR_ARR = 0;

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        // OUTROS
                        objTotMod.TMP_MOD_PAG = 3;
                        objTotMod.TMP_VLR_ARR = 0;

                        context.TB_TOT_MOD_PAGTO.Add(objTotMod);
                        context.SaveChanges();

                        dbContextTransaction.Commit();

                        if (SalvarVoltar == "N")
                        {
                            ViewBag.FIL_CD_ANCINE = new SelectList(db.TB_FILME.OrderBy(t => t.FIL_NM) , "FIL_CD_ANCINE", "FIL_NM", objSess.FIL_CD_ANCINE);
                            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO.OrderBy(t => t.EMP_NM_FANT) , "EMP_CD_ANCINE", "EMP_NM_FANT", objBil.EMP_CD_ANCINE);
                            ViewBag.SEA_DT_HR_INICIO = objSess.SEA_DT_HR_INICIO.ToShortTimeString();
                            //ViewBag.bil_dia_cin = objBil.BIL_DIA_CIN.ToShortDateString();
                            TempData["bil_dia_cin"] = objBil.BIL_DIA_CIN.ToShortDateString();
                            return View();
                        }
                        else
                        {
                            //ViewBag.bil_dia_cin = objBil.BIL_DIA_CIN.ToShortDateString();
                            TempData["bil_dia_cin"] = objBil.BIL_DIA_CIN.ToShortDateString();
                            return RedirectToAction("Index");
                        }

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();

                        ViewBag.FIL_CD_ANCINE = new SelectList(db.TB_FILME, "FIL_CD_ANCINE", "FIL_NM");
                        ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO, "EMP_CD_ANCINE", "EMP_NM_FANT");
                        ViewBag.ex = ex.Message;
                        return View();
                    }
                }
            }
        }

        //***********************************
        public ActionResult Retif(int bil_id)
        {
            try
            {
                TB_BILHETERIA objBil = db.TB_BILHETERIA.Find(bil_id);

                if(objBil != null)
                {
                    objBil.BIL_RETIF = "S";
                    TempData["bil_dia_cin"] = objBil.BIL_DIA_CIN.ToShortDateString();

                    db.Entry(objBil).State = EntityState.Modified;
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        //**********************************
        public ActionResult Edit(int bil_id)
        {
            var listaBil = (from b in db.TB_BILHETERIA
                            join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                            join tta in db.TB_TOT_TP_ASSENTO on s.SEA_ID equals tta.SEA_ID //into t1 from tta1 in t1.DefaultIfEmpty()
                            join tci in db.TB_TOT_CATEG_ING on tta.TTA_ID equals tci.TTA_ID //into t2 from tci1 in t2.DefaultIfEmpty()
                            join tmp in db.TB_TOT_MOD_PAGTO on tci.TCI_ID equals tmp.TCI_ID
                            where b.BIL_ID == bil_id



                            select new BilheteriaEdit()
                            {
                                BIL_ID = b.BIL_ID,
                                BIL_DIA_CIN = b.BIL_DIA_CIN,
                                SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                BIL_PROT = b.BIL_PROT,
                                BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                BIL_RETIF = b.BIL_RETIF,
                                EMP_CD_ANCINE = b.EMP_CD_ANCINE,

                                SEA_ID = s.SEA_ID,
                                FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                                SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                SEA_MODAL = s.SEA_MODAL,
                                SEA_FIL_NM = s.SEA_FIL_NM,
                                SEA_FIL_TP_TELA = s.SEA_FIL_TP_TELA,
                                SEA_FIL_DIGITAL = s.SEA_FIL_DIGITAL,
                                SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                                SEA_FIL_AUDIO = s.SEA_FIL_AUDIO,
                                SEA_FIL_LEG = s.SEA_FIL_LEG,
                                SEA_FIL_PRO_LIBRA = s.SEA_FIL_PRO_LIBRA,
                                SEA_FIL_LEG_DESC_CC = s.SEA_FIL_LEG_DESC_CC,
                                SEA_FIL_AUDIO_DESC = s.SEA_FIL_AUDIO_DESC,
                                SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                                SEA_DIS_NM = s.SEA_DIS_NM,
                                SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                                SEA_VRE_CNPJ = s.SEA_VRE_CNPJ,

                                TTA_ID = tta.TTA_ID,
                                TTA_TP_ASSENTO = tta.TTA_TP_ASSENTO,
                                TTA_QTD_DISP = tta.TTA_QTD_DISP,

                                TCI_ID = tci.TCI_ID,
                                TCI_CAT = tci.TCI_CAT,
                                TCI_QTD_ESPECT = tci.TCI_QTD_ESPECT,

                                TMP_ID = tmp.TMP_ID,
                                TMP_MOD_PAG = tmp.TMP_MOD_PAG,
                                TMP_VLR_ARR = tmp.TMP_VLR_ARR

                            });

            foreach (var item in listaBil)
            {
                modelBil.Add(new BilheteriaEdit()
                {
                    BIL_ID = item.BIL_ID,
                    BIL_DIA_CIN = item.BIL_DIA_CIN,
                    SAL_CD_ANCINE = item.SAL_CD_ANCINE,
                    BIL_HOUVE_SES = item.BIL_HOUVE_SES,
                    BIL_ADIMP_SALA = item.BIL_ADIMP_SALA,
                    BIL_PROT = item.BIL_PROT,
                    BIL_STATUS_PROT = item.BIL_STATUS_PROT,
                    BIL_RETIF = item.BIL_RETIF,
                    EMP_CD_ANCINE = item.EMP_CD_ANCINE,

                    SEA_ID = item.SEA_ID,
                    FIL_CD_ANCINE = item.FIL_CD_ANCINE,
                    SEA_DT_HR_INICIO = item.SEA_DT_HR_INICIO,
                    SEA_MODAL = item.SEA_MODAL,
                    SEA_FIL_NM = item.SEA_FIL_NM,
                    SEA_FIL_TP_TELA = item.SEA_FIL_TP_TELA,
                    SEA_FIL_DIGITAL = item.SEA_FIL_DIGITAL,
                    SEA_FIL_TP_PROJECAO = item.SEA_FIL_TP_PROJECAO,
                    SEA_FIL_AUDIO = item.SEA_FIL_AUDIO,
                    SEA_FIL_LEG = item.SEA_FIL_LEG,
                    SEA_FIL_PRO_LIBRA = item.SEA_FIL_PRO_LIBRA,
                    SEA_FIL_LEG_DESC_CC = item.SEA_FIL_LEG_DESC_CC,
                    SEA_FIL_AUDIO_DESC = item.SEA_FIL_AUDIO_DESC,
                    SEA_DIS_CNPJ = item.SEA_DIS_CNPJ,
                    SEA_DIS_NM = item.SEA_DIS_NM,
                    SEA_RZ_SOCIAL = item.SEA_RZ_SOCIAL,
                    SEA_VRE_CNPJ = item.SEA_VRE_CNPJ,

                    TTA_ID = item.TTA_ID,
                    TTA_TP_ASSENTO = item.TTA_TP_ASSENTO,
                    TTA_QTD_DISP = item.TTA_QTD_DISP,

                    TCI_ID = item.TCI_ID,
                    TCI_CAT = item.TCI_CAT,
                    TCI_QTD_ESPECT = item.TCI_QTD_ESPECT,

                    TMP_ID = item.TMP_ID,
                    TMP_MOD_PAG = item.TMP_MOD_PAG,
                    TMP_VLR_ARR = item.TMP_VLR_ARR
                });
            }

            var BIL_RETIF = new[]
                    {
                        new SelectListItem { Value = "N", Text = "Não" },
                        new SelectListItem { Value = "S", Text = "Sim" },
                    };
            ViewBag.BIL_RETIF1 = new SelectList(BIL_RETIF ,"Value" ,"Text");

            
            //if (listaBil.FirstOrDefault().BIL_STATUS_PROT == "" || listaBil.FirstOrDefault().BIL_STATUS_PROT == "N")
            //{
            //    if (listaBil.FirstOrDefault().BIL_STATUS_PROT == "N")
            //    {
            //        var BIL_RETIF = new[]
            //        {
            //            new SelectListItem { Value = "N", Text = "Não" },
            //            new SelectListItem { Value = "S", Text = "Sim" },
            //        };
            //        ViewBag.BIL_RETIF1 = new SelectList(BIL_RETIF ,"Value" ,"Text");
            //    }
            //    else
            //    {
            //        var BIL_RETIF = new[]
            //        {
            //            new SelectListItem { Value = "N", Text = "Não" },
            //        };
            //        ViewBag.BIL_RETIF1 = new SelectList(BIL_RETIF ,"Value" ,"Text");
            //    }
            //}
            //else
            //{
            //    var BIL_RETIF = new[]
            //    {
            //        new SelectListItem { Value = "S", Text = "Sim" },
            //    };
            //    ViewBag.BIL_RETIF1 = new SelectList(BIL_RETIF, "Value", "Text");
            //}

            var SEA_FIL_TP_PROJECAO = new[]
            {
                new SelectListItem { Value = "2", Text = "2D" },
                new SelectListItem { Value = "3", Text = "3D" },
            };
            ViewBag.SEA_FIL_TP_PROJECAO1 = new SelectList(SEA_FIL_TP_PROJECAO, "Value", "Text");

            var SEA_MODAL = new[]
            {
                new SelectListItem { Value = "A", Text = "Sessão Regular" },
                new SelectListItem { Value = "B", Text = "Pré-estreia" },
                new SelectListItem { Value = "C", Text = "Mostra ou Festival" },
                new SelectListItem { Value = "D", Text = "Sessão Privada" },
            };
            ViewBag.SEA_MODAL1 = new SelectList(SEA_MODAL, "Value", "Text");

            var SEA_FIL_TP_TELA = new[]
            {
                new SelectListItem { Value = "P", Text = "Padrão" },
                new SelectListItem { Value = "A", Text = "Ampliada" },
            };
            ViewBag.SEA_FIL_TP_TELA1 = new SelectList(SEA_FIL_TP_TELA, "Value", "Text");

            var SEA_FIL_DIGITAL = new[]
            {
                new SelectListItem { Value = "S", Text = "Sim" },
                new SelectListItem { Value = "N", Text = "Não" },
            };
            ViewBag.SEA_FIL_DIGITAL1 = new SelectList(SEA_FIL_DIGITAL, "Value", "Text");

            var SEA_FIL_AUDIO = new[]
            {
                new SelectListItem { Value = "O", Text = "Original" },
                new SelectListItem { Value = "D", Text = "Dublado" },
            };
            ViewBag.SEA_FIL_AUDIO1 = new SelectList(SEA_FIL_AUDIO, "Value", "Text");

            var SEA_FIL_LEG = new[]
            {
                new SelectListItem { Value = "S", Text = "Sim" },
                new SelectListItem { Value = "N", Text = "Não" },
            };
            ViewBag.SEA_FIL_LEG1 = new SelectList(SEA_FIL_LEG, "Value", "Text");

            var SEA_FIL_PRO_LIBRA = new[]
            {
                new SelectListItem { Value = "S", Text = "Sim" },
                new SelectListItem { Value = "N", Text = "Não" },
            };
            ViewBag.SEA_FIL_PRO_LIBRA1 = new SelectList(SEA_FIL_PRO_LIBRA, "Value", "Text");

            var SEA_FIL_LEG_DESC_CC = new[]
            {
                new SelectListItem { Value = "S", Text = "Sim" },
                new SelectListItem { Value = "N", Text = "Não" },
            };
            ViewBag.SEA_FIL_LEG_DESC_CC1 = new SelectList(SEA_FIL_LEG_DESC_CC, "Value", "Text");

            var SEA_FIL_AUDIO_DESC = new[]
            {
                new SelectListItem { Value = "S", Text = "Sim" },
                new SelectListItem { Value = "N", Text = "Não" },
            };
            ViewBag.SEA_FIL_AUDIO_DESC1 = new SelectList(SEA_FIL_AUDIO_DESC, "Value", "Text");

            ViewBag.FIL_CD_ANCINE = new SelectList(db.TB_FILME.OrderBy(t => t.FIL_NM) , "FIL_CD_ANCINE", "FIL_NM", listaBil.FirstOrDefault().FIL_CD_ANCINE);
            ViewBag.EMP_CD_ANCINE = new SelectList(db.TB_EMPRESA_COMPLEXO.OrderBy(t => t.EMP_NM_FANT) , "EMP_CD_ANCINE", "EMP_NM_FANT", listaBil.FirstOrDefault().EMP_CD_ANCINE);
            ViewBag.SAL_CD_ANCINE = new SelectList(db.TB_SALA.OrderBy(t => t.SAL_DESC), "SAL_CD_ANCINE", "SAL_DESC", listaBil.FirstOrDefault().SAL_CD_ANCINE);
            ViewBag.VRE_CNPJ = new SelectList(db.TB_VENDEDOR_REMOTO.OrderBy(t => t.VRE_RZ_SOCIAL), "VRE_CNPJ", "RZ_SOCIAL", listaBil.FirstOrDefault().SEA_VRE_CNPJ);

            return View(listaBil);
        }

        [HttpPost]
        //*************************************************
        public ActionResult Edit(FormCollection collection)
        {
            using (var context = new SCBEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        string[] BIL_ID;
                        string[] EMP_CD_ANCINE;
                        string[] SAL_CD_ANCINE;
                        string[] BIL_DIA_CIN;
                        string[] BIL_HOUVE_SES;
                        string[] BIL_RETIF;
                        string[] BIL_PROT;
                        string[] BIL_ADIMP_SALA;
                        string[] BIL_STATUS_PROT;

                        string strBIL_ID_aux = "";
                        string strEMP_CD_ANCINE_aux = "";
                        string strSAL_CD_ANCINE_aux = "";
                        string strBIL_DIA_CIN_aux = "";
                        string strBIL_HOUVE_SES_aux = "";
                        string strBIL_RETIF_aux = "";
                        string strBIL_PROT_aux = "";
                        string strBIL_ADIMP_SALA_aux = "";
                        string strBIL_STATUS_PROT_aux = "";

                        string[] SEA_ID;
                        string[] SEA_DT_HR_INICIO;
                        string[] SEA_MODAL;
                        string[] FIL_CD_ANCINE;
                        string[] SEA_FIL_TP_TELA;
                        string[] SEA_FIL_DIGITAL;
                        string[] SEA_FIL_TP_PROJECAO;
                        string[] SEA_FIL_AUDIO;
                        string[] SEA_FIL_LEG;
                        string[] SEA_FIL_PRO_LIBRA;
                        string[] SEA_FIL_LEG_DESC_CC;
                        string[] SEA_FIL_AUDIO_DESC;
                        string[] SEA_DIS_CNPJ;
                        string[] SEA_DIS_NM;
                        string[] SEA_VRE_CNPJ;
                        string[] SEA_RZ_SOCIAL;

                        string strSEA_ID_aux = "";
                        string strSEA_DT_HR_INICIO_aux = "";
                        string strSEA_MODAL_aux = "";
                        string strFIL_CD_ANCINE_aux = "";
                        string strSEA_FIL_TP_TELA_aux = "";
                        string strSEA_FIL_DIGITAL_aux = "";
                        string strSEA_FIL_TP_PROJECAO_aux = "";
                        string strSEA_FIL_AUDIO_aux = "";
                        string strSEA_FIL_LEG_aux = "";
                        string strSEA_FIL_PRO_LIBRA_aux = "";
                        string strSEA_FIL_LEG_DESC_CC_aux = "";
                        string strSEA_FIL_AUDIO_DESC_aux = "";
                        string strSEA_DIS_CNPJ_aux = "";
                        string strSEA_DIS_NM_aux = "";
                        string strSEA_VRE_CNPJ_aux = "";
                        string strSEA_RZ_SOCIAL_aux = "";

                        string[] TTA_ID;
                        string[] TCI_ID;
                        string[] TMP_ID;

                        string strTTA_ID_aux = "";
                        string strTCI_ID_aux = "";
                        string strTMP_ID_aux = "";

                        string[] TCI_QTD_ESPECT;
                        string[] TMP_VLR_ARR_TRAD;
                        string[] TMP_VLR_ARR_VC;
                        string[] TMP_VLR_ARR_OUT;

                        string strTCI_QTD_ESPECT_aux = "";
                        string strTMP_VLR_ARR_TRAD_aux = "";
                        string strTMP_VLR_ARR_VC_aux = "";
                        string strTMP_VLR_ARR_OUT_aux = "";
                        string strSAL_QTD_LUG_PDR_AUX = "";
                        string strSAL_QTD_LUG_PDR_ESP = "";

                        string DataAux = "";

                        string[] stringSeparators = new string[] { "," };

                        foreach (var key in collection.AllKeys)
                        {
                            var value = collection[key];

                            //.......TB_BILHETERIA........//
                            if (key == "BIL_ID") { strBIL_ID_aux = value; }
                            if (key == "EMP_CD_ANCINE") { strEMP_CD_ANCINE_aux = value; }
                            if (key == "SAL_CD_ANCINE") { strSAL_CD_ANCINE_aux = value; }
                            if (key == "BIL_DIA_CIN") { strBIL_DIA_CIN_aux = value; }
                            if (key == "BIL_HOUVE_SES") { strBIL_HOUVE_SES_aux = value; }
                            if (key == "BIL_RETIF") { strBIL_RETIF_aux = value; }
                            if (key == "BIL_PROT") { strBIL_PROT_aux = value; }
                            if (key == "BIL_ADIMP_SALA") { strBIL_ADIMP_SALA_aux = value; }
                            if (key == "BIL_STATUS_PROT") { strBIL_STATUS_PROT_aux = value; }

                            //..........TB_SESSAO.........//
                            if (key == "SEA_ID") { strSEA_ID_aux = value; }
                            if (key == "SEA_DT_HR_INICIO") { strSEA_DT_HR_INICIO_aux = value; }
                            if (key == "SEA_MODAL") { strSEA_MODAL_aux = value; }
                            if (key == "FIL_CD_ANCINE") { strFIL_CD_ANCINE_aux = value; }
                            if (key == "SEA_FIL_TP_TELA") { strSEA_FIL_TP_TELA_aux = value; }
                            if (key == "SEA_FIL_DIGITAL") { strSEA_FIL_DIGITAL_aux = value; }
                            if (key == "SEA_FIL_TP_PROJECAO") { strSEA_FIL_TP_PROJECAO_aux = value; }
                            if (key == "SEA_FIL_AUDIO") { strSEA_FIL_AUDIO_aux = value; }
                            if (key == "SEA_FIL_LEG") { strSEA_FIL_LEG_aux = value; }
                            if (key == "SEA_FIL_PRO_LIBRA") { strSEA_FIL_PRO_LIBRA_aux = value; }
                            if (key == "SEA_FIL_LEG_DESC_CC") { strSEA_FIL_LEG_DESC_CC_aux = value; }
                            if (key == "SEA_FIL_AUDIO_DESC") { strSEA_FIL_AUDIO_DESC_aux = value; }
                            if (key == "SEA_DIS_CNPJ") { strSEA_DIS_CNPJ_aux = value; }
                            if (key == "SEA_DIS_NM") { strSEA_DIS_NM_aux = value; }
                            if (key == "SEA_VRE_CNPJ") { strSEA_VRE_CNPJ_aux = value; }
                            if (key == "SEA_RZ_SOCIAL") { strSEA_RZ_SOCIAL_aux = value; }

                            if (key == "TTA_ID") { strTTA_ID_aux = value; }
                            if (key == "TCI_ID") { strTCI_ID_aux = value; }
                            if (key == "TMP_ID") { strTMP_ID_aux = value; }

                            if (key == "TCI_QTD_ESPECT") { strTCI_QTD_ESPECT_aux = value; }
                            if (key == "TMP_VLR_ARR_TRAD") { strTMP_VLR_ARR_TRAD_aux = value; }
                            if (key == "TMP_VLR_ARR_VC") { strTMP_VLR_ARR_VC_aux = value; }
                            if (key == "TMP_VLR_ARR_OUT") { strTMP_VLR_ARR_OUT_aux = value; }

                            //if (key == "SAL_QTD_LUG_PDR") { strSAL_QTD_LUG_PDR_aux = value; }
                            //if (key == "SAL_QTD_LUG_ESP") { strSAL_QTD_LUG_ESP_aux = value; }
                        }


                        BIL_ID = strBIL_ID_aux.Split(stringSeparators, StringSplitOptions.None);
                        EMP_CD_ANCINE = strEMP_CD_ANCINE_aux.Split(stringSeparators, StringSplitOptions.None);
                        SAL_CD_ANCINE = strSAL_CD_ANCINE_aux.Split(stringSeparators, StringSplitOptions.None);
                        BIL_DIA_CIN = strBIL_DIA_CIN_aux.Split(stringSeparators, StringSplitOptions.None);
                        BIL_HOUVE_SES = strBIL_HOUVE_SES_aux.Split(stringSeparators, StringSplitOptions.None);
                        BIL_RETIF = strBIL_RETIF_aux.Split(stringSeparators, StringSplitOptions.None);
                        BIL_PROT = strBIL_PROT_aux.Split(stringSeparators, StringSplitOptions.None);
                        BIL_ADIMP_SALA = strBIL_ADIMP_SALA_aux.Split(stringSeparators, StringSplitOptions.None);
                        BIL_STATUS_PROT = strBIL_STATUS_PROT_aux.Split(stringSeparators, StringSplitOptions.None);

                        SEA_ID = strSEA_ID_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_DT_HR_INICIO = strSEA_DT_HR_INICIO_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_MODAL = strSEA_MODAL_aux.Split(stringSeparators, StringSplitOptions.None);
                        FIL_CD_ANCINE = strFIL_CD_ANCINE_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_TP_TELA = strSEA_FIL_TP_TELA_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_DIGITAL = strSEA_FIL_DIGITAL_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_TP_PROJECAO = strSEA_FIL_TP_PROJECAO_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_AUDIO = strSEA_FIL_AUDIO_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_LEG = strSEA_FIL_LEG_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_PRO_LIBRA = strSEA_FIL_PRO_LIBRA_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_LEG_DESC_CC = strSEA_FIL_LEG_DESC_CC_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_FIL_AUDIO_DESC = strSEA_FIL_AUDIO_DESC_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_DIS_CNPJ = strSEA_DIS_CNPJ_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_DIS_NM = strSEA_DIS_NM_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_VRE_CNPJ = strSEA_VRE_CNPJ_aux.Split(stringSeparators, StringSplitOptions.None);
                        SEA_RZ_SOCIAL = strSEA_RZ_SOCIAL_aux.Split(stringSeparators, StringSplitOptions.None);
                        TCI_QTD_ESPECT = strTCI_QTD_ESPECT_aux.Split(stringSeparators, StringSplitOptions.None);
                        TMP_VLR_ARR_TRAD = strTMP_VLR_ARR_TRAD_aux.Split(stringSeparators, StringSplitOptions.None);
                        TMP_VLR_ARR_VC = strTMP_VLR_ARR_VC_aux.Split(stringSeparators, StringSplitOptions.None);
                        TMP_VLR_ARR_OUT = strTMP_VLR_ARR_OUT_aux.Split(stringSeparators, StringSplitOptions.None);

                        TTA_ID = strTTA_ID_aux.Split(stringSeparators, StringSplitOptions.None);
                        TCI_ID = strTCI_ID_aux.Split(stringSeparators, StringSplitOptions.None);
                        TMP_ID = strTMP_ID_aux.Split(stringSeparators, StringSplitOptions.None);

                        //SAL_QTD_LUG_PDR = strSAL_QTD_LUG_PDR_aux.Split(stringSeparators, StringSplitOptions.None);
                        //SAL_QTD_LUG_ESP = strSAL_QTD_LUG_ESP_aux.Split(stringSeparators, StringSplitOptions.None);

                        if (BIL_STATUS_PROT[0] == "V" && BIL_RETIF[0] == "S")
                        {
                            BIL_STATUS_PROT[0] = "";
                        }

                        //.......TB_BILHETERIA........//
                        long bil_id_find = 0;
                        bil_id_find = Convert.ToUInt16(BIL_ID[0]);
                        TB_BILHETERIA tb_bil = context.TB_BILHETERIA.Find(bil_id_find);
                        tb_bil.EMP_CD_ANCINE = EMP_CD_ANCINE[0];
                        tb_bil.SAL_CD_ANCINE = SAL_CD_ANCINE[0];
                        tb_bil.BIL_DIA_CIN = Convert.ToDateTime(BIL_DIA_CIN[0]);
                        tb_bil.BIL_HOUVE_SES = BIL_HOUVE_SES[0];
                        tb_bil.BIL_RETIF = BIL_RETIF[0];
                        tb_bil.BIL_PROT = BIL_PROT[0];
                        //tb_bil.BIL_ADIMP_SALA = BIL_ADIMP_SALA[0];
                        tb_bil.BIL_STATUS_PROT = BIL_STATUS_PROT[0];
                        context.Entry(tb_bil).State = EntityState.Modified;
                        context.SaveChanges();

                        var filnm = context.TB_FILME.Find(FIL_CD_ANCINE[0]);
                        var dist = context.TB_DISTRIBUIDORA.Find(filnm.DIS_CNPJ);

                        //..........TB_SESSAO.........//
                        long sea_id_find = 0;
                        sea_id_find = Convert.ToUInt16(SEA_ID[0]);
                        TB_SESSAO_ANCINE tb_sea = context.TB_SESSAO_ANCINE.Find(sea_id_find);

                        DataAux = tb_bil.BIL_DIA_CIN.ToShortDateString();
                        tb_sea.SEA_DT_HR_INICIO = Convert.ToDateTime(DataAux + " " + SEA_DT_HR_INICIO[0]);
                        tb_sea.SAL_CD_ANCINE = SAL_CD_ANCINE[0];
                        tb_sea.SEA_MODAL = SEA_MODAL[0];
                        tb_sea.FIL_CD_ANCINE = FIL_CD_ANCINE[0];
                        tb_sea.SEA_FIL_NM = filnm.FIL_NM;
                        tb_sea.SEA_FIL_TP_TELA = SEA_FIL_TP_TELA[0];
                        tb_sea.SEA_FIL_DIGITAL = SEA_FIL_DIGITAL[0];
                        tb_sea.SEA_FIL_TP_PROJECAO = SEA_FIL_TP_PROJECAO[0];
                        tb_sea.SEA_FIL_LEG = SEA_FIL_LEG[0];
                        tb_sea.SEA_FIL_AUDIO = SEA_FIL_AUDIO[0];
                        tb_sea.SEA_FIL_PRO_LIBRA = SEA_FIL_PRO_LIBRA[0];
                        tb_sea.SEA_FIL_LEG_DESC_CC = SEA_FIL_LEG_DESC_CC[0];
                        tb_sea.SEA_FIL_AUDIO_DESC = SEA_FIL_AUDIO_DESC[0];

                        tb_sea.SEA_DIS_CNPJ = dist.DIS_CNPJ;
                        tb_sea.SEA_DIS_NM = dist.DIS_NM;

                        tb_sea.SEA_VRE_CNPJ = (SEA_VRE_CNPJ[0] == "" ? 0 : Convert.ToUInt16(SEA_VRE_CNPJ[0]));
                        tb_sea.SEA_RZ_SOCIAL = SEA_RZ_SOCIAL[0];



                        context.Entry(tb_sea).State = EntityState.Modified;
                        context.SaveChanges();

                        TB_SALA tb_sala = db.TB_SALA.Find(SAL_CD_ANCINE[0]);
                        if (tb_sala != null)
                        {
                            strSAL_QTD_LUG_PDR_AUX = tb_sala.SAL_QTD_LUG_PDR.ToString();
                            strSAL_QTD_LUG_PDR_ESP = tb_sala.SAL_QTD_LUG_ESP.ToString();
                        }
                        else
                        {
                            strSAL_QTD_LUG_PDR_AUX = "0";
                            strSAL_QTD_LUG_PDR_ESP = "0";
                        }

                        long tta_id_find = Convert.ToUInt16(TTA_ID[0]);
                        TB_TOT_TP_ASSENTO tb_tta = context.TB_TOT_TP_ASSENTO.Find(tta_id_find);
                        tb_tta.TTA_TP_ASSENTO = "P";
                        tb_tta.TTA_QTD_DISP = Convert.ToUInt16(strSAL_QTD_LUG_PDR_AUX);
                        context.Entry(tb_tta).State = EntityState.Modified;

                        long tta_id_find2 = Convert.ToUInt16(TTA_ID[12]);
                        TB_TOT_TP_ASSENTO tb_tta2 = context.TB_TOT_TP_ASSENTO.Find(tta_id_find2);
                        tb_tta2.TTA_TP_ASSENTO = "E";
                        tb_tta2.TTA_QTD_DISP = Convert.ToUInt16(strSAL_QTD_LUG_PDR_ESP);
                        context.Entry(tb_tta2).State = EntityState.Modified;

                        //int iCount=0;

                        //..........TB_TOT_CATEG_ING.........//
                        // ASSENTO PADRÃO.

                        long tci_id_find = Convert.ToUInt16(TCI_ID[0]);
                        TB_TOT_CATEG_ING tb_tci = context.TB_TOT_CATEG_ING.Find(tci_id_find);
                        tb_tci.TCI_CAT = 1;
                        tb_tci.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[0]);
                        context.Entry(tb_tci).State = EntityState.Modified;

                        long tci_id_find2 = Convert.ToUInt16(TCI_ID[3]);
                        TB_TOT_CATEG_ING tb_tci2 = context.TB_TOT_CATEG_ING.Find(tci_id_find2);
                        tb_tci2.TCI_CAT = 2;
                        tb_tci2.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[1]);
                        context.Entry(tb_tci2).State = EntityState.Modified;

                        long tci_id_find3 = Convert.ToUInt16(TCI_ID[6]);
                        TB_TOT_CATEG_ING tb_tci3 = context.TB_TOT_CATEG_ING.Find(tci_id_find3);
                        tb_tci3.TCI_CAT = 4;
                        tb_tci3.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[2]);
                        context.Entry(tb_tci3).State = EntityState.Modified;

                        long tci_id_find4 = Convert.ToUInt16(TCI_ID[9]);
                        TB_TOT_CATEG_ING tb_tci4 = context.TB_TOT_CATEG_ING.Find(tci_id_find4);
                        tb_tci4.TCI_CAT = 3;
                        tb_tci4.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[3]);
                        context.Entry(tb_tci4).State = EntityState.Modified;

                        //..........TB_TOT_CATEG_ING.........//
                        // ASSENTO ESPECIAL.
                        long tci_id_find5 = Convert.ToUInt16(TCI_ID[12]);
                        TB_TOT_CATEG_ING tb_tci5 = context.TB_TOT_CATEG_ING.Find(tci_id_find5);
                        tb_tci5.TCI_CAT = 1;
                        tb_tci5.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[4]);
                        context.Entry(tb_tci5).State = EntityState.Modified;

                        long tci_id_find6 = Convert.ToUInt16(TCI_ID[15]);
                        TB_TOT_CATEG_ING tb_tci6 = context.TB_TOT_CATEG_ING.Find(tci_id_find6);
                        tb_tci6.TCI_CAT = 2;
                        tb_tci6.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[5]);
                        context.Entry(tb_tci6).State = EntityState.Modified;

                        long tci_id_find7 = Convert.ToUInt16(TCI_ID[18]);
                        TB_TOT_CATEG_ING tb_tci7 = context.TB_TOT_CATEG_ING.Find(tci_id_find7);
                        tb_tci7.TCI_CAT = 4;
                        tb_tci7.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[6]);
                        context.Entry(tb_tci7).State = EntityState.Modified;

                        long tci_id_find8 = Convert.ToUInt16(TCI_ID[21]);
                        TB_TOT_CATEG_ING tb_tci8 = context.TB_TOT_CATEG_ING.Find(tci_id_find8);
                        tb_tci8.TCI_CAT = 3;
                        tb_tci8.TCI_QTD_ESPECT = Convert.ToUInt16(TCI_QTD_ESPECT[7]);
                        context.Entry(tb_tci8).State = EntityState.Modified;




                        long tmp_id_find = Convert.ToUInt16(TMP_ID[0]);
                        TB_TOT_MOD_PAGTO tb_tmp = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find);
                        tb_tmp.TMP_MOD_PAG = 1;
                        tb_tmp.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_TRAD[0]);
                        context.Entry(tb_tmp).State = EntityState.Modified;

                        long tmp_id_find2 = Convert.ToUInt16(TMP_ID[1]);
                        TB_TOT_MOD_PAGTO tb_tmp2 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find2);
                        tb_tmp2.TMP_MOD_PAG = 2;
                        tb_tmp2.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_VC[0]);
                        context.Entry(tb_tmp2).State = EntityState.Modified;

                        long tmp_id_find3 = Convert.ToUInt16(TMP_ID[2]);
                        TB_TOT_MOD_PAGTO tb_tmp3 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find3);
                        tb_tmp3.TMP_MOD_PAG = 3;
                        tb_tmp3.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_OUT[0]);
                        context.Entry(tb_tmp3).State = EntityState.Modified;



                        long tmp_id_find4 = Convert.ToUInt16(TMP_ID[3]);
                        TB_TOT_MOD_PAGTO tb_tmp4 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find4);
                        tb_tmp4.TMP_MOD_PAG = 1;
                        tb_tmp4.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_TRAD[1]);
                        context.Entry(tb_tmp4).State = EntityState.Modified;

                        long tmp_id_find5 = Convert.ToUInt16(TMP_ID[4]);
                        TB_TOT_MOD_PAGTO tb_tmp5 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find5);
                        tb_tmp5.TMP_MOD_PAG = 2;
                        tb_tmp5.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_VC[1]);
                        context.Entry(tb_tmp5).State = EntityState.Modified;

                        long tmp_id_find6 = Convert.ToUInt16(TMP_ID[5]);
                        TB_TOT_MOD_PAGTO tb_tmp6 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find6);
                        tb_tmp6.TMP_MOD_PAG = 3;
                        tb_tmp6.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_OUT[1]);
                        context.Entry(tb_tmp6).State = EntityState.Modified;



                        long tmp_id_find7 = Convert.ToUInt16(TMP_ID[6]);
                        TB_TOT_MOD_PAGTO tb_tmp7 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find7);
                        tb_tmp7.TMP_MOD_PAG = 1;
                        tb_tmp7.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_TRAD[2]);
                        context.Entry(tb_tmp7).State = EntityState.Modified;

                        long tmp_id_find8 = Convert.ToUInt16(TMP_ID[7]);
                        TB_TOT_MOD_PAGTO tb_tmp8 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find8);
                        tb_tmp8.TMP_MOD_PAG = 2;
                        tb_tmp8.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_VC[2]);
                        context.Entry(tb_tmp8).State = EntityState.Modified;

                        long tmp_id_find9 = Convert.ToUInt16(TMP_ID[8]);
                        TB_TOT_MOD_PAGTO tb_tmp9 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find9);
                        tb_tmp9.TMP_MOD_PAG = 3;
                        tb_tmp9.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_OUT[2]);
                        context.Entry(tb_tmp9).State = EntityState.Modified;



                        long tmp_id_find10 = Convert.ToUInt16(TMP_ID[12]);
                        TB_TOT_MOD_PAGTO tb_tmp10 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find10);
                        tb_tmp10.TMP_MOD_PAG = 1;
                        tb_tmp10.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_TRAD[3]);
                        context.Entry(tb_tmp10).State = EntityState.Modified;

                        long tmp_id_find11 = Convert.ToUInt16(TMP_ID[13]);
                        TB_TOT_MOD_PAGTO tb_tmp11 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find11);
                        tb_tmp11.TMP_MOD_PAG = 2;
                        tb_tmp11.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_VC[3]);
                        context.Entry(tb_tmp11).State = EntityState.Modified;

                        long tmp_id_find12 = Convert.ToUInt16(TMP_ID[14]);
                        TB_TOT_MOD_PAGTO tb_tmp12 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find12);
                        tb_tmp12.TMP_MOD_PAG = 3;
                        tb_tmp12.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_OUT[3]);
                        context.Entry(tb_tmp12).State = EntityState.Modified;




                        long tmp_id_find13 = Convert.ToUInt16(TMP_ID[15]);
                        TB_TOT_MOD_PAGTO tb_tmp13 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find13);
                        tb_tmp13.TMP_MOD_PAG = 1;
                        tb_tmp13.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_TRAD[4]);
                        context.Entry(tb_tmp13).State = EntityState.Modified;

                        long tmp_id_find14 = Convert.ToUInt16(TMP_ID[16]);
                        TB_TOT_MOD_PAGTO tb_tmp14 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find14);
                        tb_tmp14.TMP_MOD_PAG = 2;
                        tb_tmp14.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_VC[4]);
                        context.Entry(tb_tmp14).State = EntityState.Modified;

                        long tmp_id_find15 = Convert.ToUInt16(TMP_ID[17]);
                        TB_TOT_MOD_PAGTO tb_tmp15 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find15);
                        tb_tmp15.TMP_MOD_PAG = 3;
                        tb_tmp15.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_OUT[4]);
                        context.Entry(tb_tmp15).State = EntityState.Modified;



                        long tmp_id_find16 = Convert.ToUInt16(TMP_ID[18]);
                        TB_TOT_MOD_PAGTO tb_tmp16 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find16);
                        tb_tmp16.TMP_MOD_PAG = 1;
                        tb_tmp16.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_TRAD[5]);
                        context.Entry(tb_tmp16).State = EntityState.Modified;

                        long tmp_id_find17 = Convert.ToUInt16(TMP_ID[19]);
                        TB_TOT_MOD_PAGTO tb_tmp17 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find17);
                        tb_tmp17.TMP_MOD_PAG = 2;
                        tb_tmp17.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_VC[5]);
                        context.Entry(tb_tmp17).State = EntityState.Modified;

                        long tmp_id_find18 = Convert.ToUInt16(TMP_ID[20]);
                        TB_TOT_MOD_PAGTO tb_tmp18 = context.TB_TOT_MOD_PAGTO.Find(tmp_id_find18);
                        tb_tmp18.TMP_MOD_PAG = 3;
                        tb_tmp18.TMP_VLR_ARR = Convert.ToDouble(TMP_VLR_ARR_OUT[5]);
                        context.Entry(tb_tmp18).State = EntityState.Modified;

                        context.SaveChanges();

                        dbContextTransaction.Commit();

                        //ViewBag.bil_dia_cin = BIL_DIA_CIN[0].ToString();
                        TempData["bil_dia_cin"] = BIL_DIA_CIN[0].ToString();
                        return RedirectToAction("Index");
                        //return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Bilheteria", action = "Index", bil_dia_cin = BIL_DIA_CIN[0].ToString() }));
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        ViewBag.ex = ex.Message;
                        return View();
                    }
                }
            }
        }

        //*******************************************************************************************************************************
        public ActionResult Delete(int bil_id, string houve_ses, string retif, string sala, string obra, string hora_ini, string dia_cin)
        {
            ViewBag.BIL_ID = bil_id;
            ViewBag.BIL_DIA_CIN = dia_cin;
            TempData["bil_dia_cin"] = dia_cin;
            ViewBag.SAL_DESC = sala;
            ViewBag.SEA_FIL_NM = obra;
            ViewBag.SEA_DT_HR_INICIO = hora_ini;
            ViewBag.BIL_HOUVE_SES = houve_ses;
            ViewBag.BIL_RETIF = retif;

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //*************************************************************************************
        public ActionResult Delete(int bil_id, string BIL_HOUVE_SES, FormCollection collection)
        {
            try
            {
                TB_BILHETERIA objBil = db.TB_BILHETERIA.Find(bil_id);

                if (objBil.BIL_HOUVE_SES == "N")
                {

                    //APAGA MENSAGENS
                    var TB_MSG_S = from msg in db.TB_MENSAGEM_ANCINE
                                 where msg.BIL_ID == bil_id
                                 select msg;

                    if (TB_MSG_S != null)
                    {
                        db.TB_MENSAGEM_ANCINE.RemoveRange(TB_MSG_S);
                    }

                    TempData["bil_dia_cin"] = objBil.BIL_DIA_CIN.ToShortDateString();
                    //ViewBag.bil_dia_cin = objBil.BIL_DIA_CIN.ToShortDateString();

                    db.TB_BILHETERIA.Remove(objBil);

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }


                var TB_SES = from sessao in db.TB_SESSAO_ANCINE
                             where sessao.BIL_ID == bil_id
                             select sessao;


                string[] stringSeparators = new string[] { "," };
                long ses_id_aux = TB_SES.FirstOrDefault().SEA_ID;
                string[] tta_id;
                string tta_id_aux = "";
                long tta_id_long = 0;
                string[] tci_id;
                string tci_id_aux = "";
                long tci_id_long = 0;
                long tmp_id_long = 0;

                // BUSCA TIPO DE ASSENTO.
                var TB_ASSENTO = from ass in db.TB_TOT_TP_ASSENTO
                                 where ass.SEA_ID == ses_id_aux
                                 select ass;

                foreach(var itemAss in TB_ASSENTO)
                {
                    if (tta_id_aux == "")
                    {
                        tta_id_aux = itemAss.TTA_ID.ToString();
                    }
                    else
                    {
                        tta_id_aux = tta_id_aux + "," + itemAss.TTA_ID.ToString();
                    }
                }

                tta_id = tta_id_aux.Split(stringSeparators ,StringSplitOptions.None);

                // BUSCA CATEGORIA DE INGRESSO
                for (var item = 0; item < tta_id.Length; item++)
                {
                    tta_id_long = Convert.ToUInt32(tta_id[item]);

                    var TB_CAT = from cat in db.TB_TOT_CATEG_ING
                                 where cat.TTA_ID == tta_id_long
                                 select cat;

                    foreach(var itemCat in TB_CAT)
                    {
                        if (tci_id_aux == "")
                        {
                            tci_id_aux = itemCat.TCI_ID.ToString();
                        }
                        else
                        {
                            tci_id_aux = tci_id_aux + "," + itemCat.TCI_ID.ToString();
                        }
                    }
                }

                // BUSCA MODALIDADE DE PAGAMENTO.
                tci_id = tci_id_aux.Split(stringSeparators ,StringSplitOptions.None);

                for (var item = 0; item < tci_id.Length; item++)
                {
                    tci_id_long = Convert.ToUInt32(tci_id[item]);

                    var TB_MODP = from mod in db.TB_TOT_CATEG_ING
                                  where mod.TCI_ID == tci_id_long
                                  select mod;

                    foreach (var itemMod in TB_MODP)
                    {
                        tci_id_long = Convert.ToUInt32(itemMod.TCI_ID);

                        var TB_TMP = from tmp in db.TB_TOT_MOD_PAGTO
                                     where tmp.TCI_ID == tci_id_long
                                     select tmp;

                        foreach (var itemTmp in TB_TMP)
                        {
                            tmp_id_long = Convert.ToUInt32(itemTmp.TMP_ID);

                            TB_TOT_MOD_PAGTO TB_TMP2 = db.TB_TOT_MOD_PAGTO.Find(tmp_id_long);
                            db.TB_TOT_MOD_PAGTO.Remove(TB_TMP2);
                        }
                    }
                }


                //********* VOLTANDO *********

                // APAGA CATEGORIA DE INGRESSO
                for (var item = 0; item < tci_id.Length; item++)
                {
                    tci_id_long = Convert.ToUInt32(tci_id[item]);
                    TB_TOT_CATEG_ING TB_CAT = db.TB_TOT_CATEG_ING.Find(tci_id_long);
                    db.TB_TOT_CATEG_ING.Remove(TB_CAT);

                }

                // APAGA TIPO DE ASSENTO
                for (var item = 0; item < tta_id.Length; item++)
                {
                    tta_id_long = Convert.ToUInt32(tta_id[item]);
                    TB_TOT_TP_ASSENTO TB_ASS = db.TB_TOT_TP_ASSENTO.Find(tta_id_long);
                    db.TB_TOT_TP_ASSENTO.Remove(TB_ASS);
                }

                // APAGA SESSÃO
                TB_SESSAO_ANCINE TB_SESSAO = db.TB_SESSAO_ANCINE.Find(ses_id_aux);
                db.TB_SESSAO_ANCINE.Remove(TB_SESSAO);

                //APAGA MENSAGENS
                var TB_MSG = from msg in db.TB_MENSAGEM_ANCINE
                             where msg.BIL_ID == bil_id
                             select msg;

                if(TB_MSG != null)
                {
                    db.TB_MENSAGEM_ANCINE.RemoveRange(TB_MSG);
                }

                TempData["bil_dia_cin"] = objBil.BIL_DIA_CIN.ToShortDateString();
                //ViewBag.bil_dia_cin = objBil.BIL_DIA_CIN.ToShortDateString();

                // APAGA BILHETERIA
                db.TB_BILHETERIA.Remove(objBil);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        //**********************************
        public ActionResult AtualizaAncine()
        {
            try
            {
                // DATAS COM SESSÃO //
                var listaBil = (from b in db.TB_BILHETERIA
                                join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                                where b.BIL_PROT != "" && b.BIL_STATUS_PROT == "A"
                                group b by new { b.BIL_DIA_CIN, b.SAL_CD_ANCINE, b.BIL_PROT } into pg
                                orderby pg.FirstOrDefault().BIL_DIA_CIN, pg.FirstOrDefault().SAL_CD_ANCINE
                                select new MensagensANCINE()
                                {
                                    BIL_DIA_CIN = pg.FirstOrDefault().BIL_DIA_CIN,
                                    SAL_CD_ANCINE = pg.FirstOrDefault().SAL_CD_ANCINE,
                                    BIL_PROT = pg.FirstOrDefault().BIL_PROT,
                                }).ToList();


                var TB_BIL_SEN_SESS = (from b in db.TB_BILHETERIA
                                       where b.BIL_HOUVE_SES == "N" && b.BIL_PROT != "" && b.BIL_STATUS_PROT == "A"
                                       orderby b.BIL_DIA_CIN, b.SAL_CD_ANCINE

                                       select new MensagensANCINE()
                                       {
                                           BIL_DIA_CIN = b.BIL_DIA_CIN ,
                                           SAL_CD_ANCINE = b.SAL_CD_ANCINE ,
                                           BIL_PROT = b.BIL_PROT ,

                                       }).ToList();

                var myunionQuery = listaBil.Union(TB_BIL_SEN_SESS);

                foreach (var item in myunionQuery)
                {
                    if (item.BIL_PROT != "")
                    {
                        var strRet = AtualizaProtocolo(item.BIL_DIA_CIN, item.SAL_CD_ANCINE, item.BIL_PROT);

                        if(strRet != "S")
                        {
                            ViewBag.ex = ViewBag.ex + "<br />" + strRet;
                        }

                    }
                }

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                ViewBag.ex = ViewBag.ex + "<br />" + ex.Message;
                return View();
            }
            finally
            {
                
            }

            return RedirectToAction("IndexStatus");
        }

        //******************************************************************************************
        public string AtualizaProtocolo(DateTime BilDiaCin, string salCdAncine , string IdProtocolo)
        {

        try
            {
                clsHelper.LogSCB("AtualizaProtocolo - BilheteriaController");

                string strBilIdsAux = "";
                string strStProtAux = "";
                string strProtAux = "";
                string[] strBilIds;
                string[] strStProt;
                string[] strProt;
                string[] stringSeparators = new string[] { "," };

                string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
                string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

                SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);
                clsHelper.LogSCB("AtualizaProtocolo - MÉTODO objSCBIntegrationManager - BilheteriaController");
                StatusRelatorioBilheteria objReturn = objSCBIntegrationManager.ConsultaProtocoloPorID(IdProtocolo);

                // ******* AGUARDA 5 SEGUNDOS PARA CONTINUAR - PALIATIVO ATÉ SOLUÇÃO DE MAIS PERFORMANCE ******//
                System.Threading.Thread.Sleep(5000);

                // VALIDA SE O RETORNO NÃO É NULO
                if (objReturn != null)
                {
                    // ATUALIZA TODOS OS REGISTROS DO PROTOCOLO COM O STATUS FINAL DO XML.
                    var listaBil = from b in db.TB_BILHETERIA
                                   where b.BIL_DIA_CIN == BilDiaCin && b.SAL_CD_ANCINE == salCdAncine && b.BIL_PROT == IdProtocolo
                                   select b;


                    foreach (var item in listaBil)
                    {
                        if (strBilIdsAux == "") { strBilIdsAux = item.BIL_ID.ToString(); }
                        else { strBilIdsAux = strBilIdsAux + "," + item.BIL_ID.ToString(); }

                        if (strStProtAux == "") { strStProtAux = objReturn.statusProtocolo; }
                        else { strStProtAux = strStProtAux + "," + objReturn.statusProtocolo; }

                        if (strProtAux == "")
                        { strProtAux = (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }
                        else
                        { strProtAux = strProtAux + "," + (objReturn.numeroProtocolo == null ? "0" : objReturn.numeroProtocolo); }
                    }
                    strBilIds = strBilIdsAux.Split(stringSeparators ,StringSplitOptions.None);
                    strStProt = strStProtAux.Split(stringSeparators ,StringSplitOptions.None);
                    strProt = strProtAux.Split(stringSeparators ,StringSplitOptions.None);

                    TrataXMLSend(strBilIds ,strStProt ,strProt ,db);

                    // TRATA MENSAGENS ///
                    if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                    {
                        foreach (var msg in objReturn.mensagens)
                        {
                            string emp_cd = objReturn.registroANCINEExibidor.ToString();
                            string sal_cd = objReturn.registroANCINESala.ToString();
                            DateTime dia_cin = objReturn.diaCinematografico;

                            // MENSAGEM COM SESSÃO ESPECIFICADA //
                            if (msg.dataHoraInicio != null)
                            {
                                DateTime dtHoraIni = Convert.ToDateTime(msg.dataHoraInicio);

                                var listaBilMsg = (from b in db.TB_BILHETERIA
                                                join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                                                where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni
                                                select new MensagensANCINE()
                                                {
                                                    BIL_ID = b.BIL_ID,
                                                    BIL_DIA_CIN = b.BIL_DIA_CIN,
                                                    SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                                    BIL_PROT = b.BIL_PROT,
                                                    BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                                    EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                                    SEA_ID = s.SEA_ID,
                                                    SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                                });

                                foreach (var itemMsg in listaBilMsg)
                                {
                                    TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
                                    TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                    TB_MSG.MSA_DT_MSG = DateTime.Now;
                                    TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                    TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                    TB_MSG.SEA_ID = itemMsg.SEA_ID;
                                    TB_MSG.SEA_DT_HR_INICIO = itemMsg.SEA_DT_HR_INICIO;
                                    TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                    TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                    TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                    db.TB_MENSAGEM_ANCINE.Add(TB_MSG);
                                }
                                //db.SaveChanges();
                            }
                            else
                            {
                                var listaBilMsg = (from b in db.TB_BILHETERIA
                                                   where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin
                                                   select b);

                                foreach (var itemMsg in listaBilMsg)
                                {
                                    TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
                                    TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                    TB_MSG.MSA_DT_MSG = DateTime.Now;
                                    TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                    TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                    TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                    TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                    TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                    db.TB_MENSAGEM_ANCINE.Add(TB_MSG);
                                }
                                //db.SaveChanges();
                            }
                        }
                        
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }

            return "S";
        }

        [HttpGet]
        public ActionResult AtualizaAncine_V1()
        {
            try
            {
                string[] stringSeparators = new string[] { "," };
                string[] strDias;
                string strDiasAux = "";
                DateTime dtHoraIni;

                // BUSCA AS DATAS ONDE EXISTEM BILHETERIAS PARA SEREM ENVIADAS.
                var listaBil = (from b in db.TB_BILHETERIA
                                where (b.BIL_STATUS_PROT != "" && b.BIL_STATUS_PROT != "V")
                                group b by new { b.BIL_DIA_CIN} into pg
                                join s in db.TB_SESSAO_ANCINE on pg.FirstOrDefault().BIL_ID equals s.BIL_ID
                                orderby pg.FirstOrDefault().BIL_DIA_CIN, pg.FirstOrDefault().SAL_CD_ANCINE, s.SEA_DT_HR_INICIO
                                select new MensagensANCINE()
                                { BIL_DIA_CIN = pg.FirstOrDefault().BIL_DIA_CIN });

                foreach (var item in listaBil)
                {
                    model.Add(new MensagensANCINE() { BIL_DIA_CIN = item.BIL_DIA_CIN });
                }


                foreach (var item in listaBil)
                {
                    if (strDiasAux == "")
                    {
                        strDiasAux = item.BIL_DIA_CIN.ToString();
                    }
                    else
                    {
                        strDiasAux = strDiasAux + "," + item.BIL_DIA_CIN.ToString();
                    }
                    
                }

                if (strDiasAux == "")
                {
                    ViewBag.msg = "Não há registros pendentes de atualização.";
                    return View();
                }

                strDias = strDiasAux.Split(stringSeparators ,StringSplitOptions.None);

                for (var item = 0; item < strDias.Length; item++)
                {
                    string emp_cd = "";
                    string sal_cd = "";
                    DateTime dia_cin;
                    
                    DateTime DataAux = Convert.ToDateTime(strDias[item]);
                    string strDataAux = "";

                    string[] FormatoHorarioInicio = DataAux.GetDateTimeFormats();
                    strDataAux = FormatoHorarioInicio[13]; //[47]: "2017-04-01 21:00:00"

                    string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
                    string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

                    SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);
                    DateTime dataDiaCinematografico = Convert.ToDateTime(strDataAux);
                    clsHelper.LogSCB("AtualizaAncine_V1 - MÉTODO objSCBIntegrationManager - BilheteriaController");
                    ListaStatusRegistroBilheteria objReturnListaStatusRegistroBilheteria = objSCBIntegrationManager.ConsultaProtocolosUmDiaCinematografico(dataDiaCinematografico);

                    if (objReturnListaStatusRegistroBilheteria != null)
                    {

                        // LOOP RETORNO SEM ERROS.
                        foreach (var objReturn in objReturnListaStatusRegistroBilheteria.StatusRegistroBilheteriaList)
                        {
                            emp_cd = objReturn.registroANCINEExibidor.ToString();
                            sal_cd = objReturn.registroANCINESala.ToString();
                            dia_cin = objReturn.diaCinematografico;

                            // RETORNO SEM ERROS.
                            if (objReturn.statusProtocolo == "V" && (objReturn.mensagens == null || objReturn.mensagens.Count() == 0)) 
                            {
                                var listaBilOK = (from b in db.TB_BILHETERIA
                                                  where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && b.BIL_PROT == objReturn.numeroProtocolo
                                                  select b);

                                foreach (var itemOK in listaBilOK)
                                {
                                    TB_BILHETERIA TB_BIL_OK = db.TB_BILHETERIA.Find(itemOK.BIL_ID);
                                    if (TB_BIL_OK != null)
                                    {
                                        TB_BIL_OK.BIL_PROT = objReturn.numeroProtocolo;
                                        TB_BIL_OK.BIL_STATUS_PROT = objReturn.statusProtocolo;
                                        db.Entry(TB_BIL_OK).State = EntityState.Modified;

                                        objReturn.registroANCINEExibidor = 0;
                                    }
                                }
                            }
                            

                        }
                        db.SaveChanges();

                        // LOOP RETORNO COM ERROS OU ALERTAS (MENSAGENS)
                        foreach (var objReturn in objReturnListaStatusRegistroBilheteria.StatusRegistroBilheteriaList)
                        {
                            emp_cd = objReturn.registroANCINEExibidor.ToString();
                            sal_cd = objReturn.registroANCINESala.ToString();
                            dia_cin = objReturn.diaCinematografico;

                            // RETORNO COM ERROS OU ALERTAS (MENSAGENS)
                            if (objReturn.mensagens != null || objReturn.mensagens.Count() > 0)
                            {
                                foreach (var msg in objReturn.mensagens)
                                {
                                    if (msg.dataHoraInicio != null)
                                    {
                                        //TEM MENSAGEM NUMA SESSÃO ESPECIFICA.
                                        dtHoraIni = Convert.ToDateTime(msg.dataHoraInicio);

                                        var listaBilMsg = (from b in db.TB_BILHETERIA
                                                           join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                                                           where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni && b.BIL_PROT == objReturn.numeroProtocolo
                                                           select new MensagensANCINE()
                                                           {
                                                               BIL_ID = b.BIL_ID,
                                                               BIL_DIA_CIN = b.BIL_DIA_CIN,
                                                               SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                                               BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                                               BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                                               BIL_PROT = b.BIL_PROT,
                                                               BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                                               BIL_RETIF = b.BIL_RETIF,
                                                               EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                                               SEA_ID = s.SEA_ID,
                                                               FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                                                               SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                                                               SEA_DIS_NM = s.SEA_DIS_NM,
                                                               SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                                               SEA_FIL_NM = s.SEA_FIL_NM,
                                                               SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                                                               SEA_MODAL = s.SEA_MODAL,
                                                               SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                                                               SEA_VRE_CNPJ = s.SEA_VRE_CNPJ
                                                           });


                                        foreach (var itemMsg in listaBilMsg)
                                        {
                                            TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
                                            TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                            TB_MSG.MSA_DT_MSG = DateTime.Now;
                                            TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                            TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                            TB_MSG.SEA_ID = itemMsg.SEA_ID;
                                            TB_MSG.SEA_DT_HR_INICIO = itemMsg.SEA_DT_HR_INICIO;
                                            TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                            TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                            TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                            db.TB_MENSAGEM_ANCINE.Add(TB_MSG);

                                            TB_BILHETERIA TB_BIL_ERRO = db.TB_BILHETERIA.Find(itemMsg.BIL_ID);
                                            if (TB_BIL_ERRO != null)
                                            {
                                                TB_BIL_ERRO.BIL_PROT = objReturn.numeroProtocolo;
                                                TB_BIL_ERRO.BIL_STATUS_PROT = objReturn.statusProtocolo;
                                                db.Entry(TB_BIL_ERRO).State = EntityState.Modified;

                                                objReturn.registroANCINEExibidor = 0;
                                            }

                                            //objReturn.mensagens  = null;
                                        }
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View();
            }

            return RedirectToAction("IndexStatus");
        }

        [HttpGet]
        public ActionResult AtualizaAncine_V0()
        {
            string statusMsg = "";
            string emp_cd = "";
            string sal_cd = "";
            DateTime dia_cin;
            string[] stringSeparators = new string[] { "," };

            try
            {

                var listaBil = (from b in db.TB_BILHETERIA
                                where (b.BIL_STATUS_PROT != "V" && b.BIL_STATUS_PROT != "")
                                group b by new { b.BIL_DIA_CIN} into pg
                                join s in db.TB_SESSAO_ANCINE on pg.FirstOrDefault().BIL_ID equals s.BIL_ID
                                orderby pg.FirstOrDefault().BIL_DIA_CIN, pg.FirstOrDefault().SAL_CD_ANCINE, s.SEA_DT_HR_INICIO
                                select new MensagensANCINE()
                                {
                                    //BIL_ID = pg.FirstOrDefault().BIL_ID,
                                    BIL_DIA_CIN = pg.FirstOrDefault().BIL_DIA_CIN,
                                    //SAL_CD_ANCINE = pg.FirstOrDefault().SAL_CD_ANCINE,
                                    //BIL_PROT = pg.FirstOrDefault().BIL_PROT,
                                    //BIL_STATUS_PROT = pg.FirstOrDefault().BIL_STATUS_PROT,
                                    //EMP_CD_ANCINE = pg.FirstOrDefault().EMP_CD_ANCINE,
                                    //SEA_ID = s.SEA_ID,
                                    //SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO

                                });

                foreach (var item in listaBil)
                {
                    model.Add(new MensagensANCINE()
                    {
                        //BIL_ID = item.BIL_ID,
                        BIL_DIA_CIN = item.BIL_DIA_CIN,
                        //SAL_CD_ANCINE = item.SAL_CD_ANCINE,
                        //BIL_PROT = item.BIL_PROT,
                        //BIL_STATUS_PROT = item.BIL_STATUS_PROT,
                        //EMP_CD_ANCINE = item.EMP_CD_ANCINE,
                        //SEA_DT_HR_INICIO = item.SEA_DT_HR_INICIO,
                        //SEA_ID = item.SEA_ID
                    });
                }

                foreach (var item in listaBil)
                {
                    DateTime DataAux = Convert.ToDateTime(item.BIL_DIA_CIN);
                    string strDataAux = "";
                    string[] strBilIdArray;
                    string strBilId = "";

                    string[] FormatoHorarioInicio = DataAux.GetDateTimeFormats();
                    strDataAux = FormatoHorarioInicio[13]; //[47]: "2017-04-01 21:00:00"

                    string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
                    string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

                    SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);
                    DateTime dataDiaCinematografico = Convert.ToDateTime(strDataAux);
                    clsHelper.LogSCB("AtualizaAncine_V0 - MÉTODO objSCBIntegrationManager - BilheteriaController");
                    ListaStatusRegistroBilheteria objReturnListaStatusRegistroBilheteria = objSCBIntegrationManager.ConsultaProtocolosUmDiaCinematografico(dataDiaCinematografico);

                    //var clsHelper = new Helpers.Helpers();
                    string strbil_string = "";


                    if (objReturnListaStatusRegistroBilheteria != null)
                    {
                        //clsHelper.TrataRetornoStatusANCINE(objReturnListaStatusRegistroBilheteria);

                        //foreach (var objReturn in objReturnListaStatusRegistroBilheteria.StatusRegistroBilheteriaList)
                        //{
                        //    if (objReturn.mensagens == null || objReturn.mensagens.Count() == 0)
                        //    {
                        //        var listaBilMsg = (from b in db.TB_BILHETERIA
                        //                           where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin
                        //                           select b);

                        //        foreach (var itemMsg in listaBilMsg)
                        //        {
                        //            TB_BILHETERIA TB_BIL_OK = db.TB_BILHETERIA.Find(itemMsg.BIL_ID);
                        //            TB_BIL_OK.BIL_PROT = objReturn.numeroProtocolo;
                        //            TB_BIL_OK.BIL_STATUS_PROT = objReturn.statusProtocolo;
                        //            db.Entry(TB_BIL_OK).State = EntityState.Modified;

                        //        }
                        //    }

                        //}

                        foreach (var objReturn in objReturnListaStatusRegistroBilheteria.StatusRegistroBilheteriaList)
                        {

                            emp_cd = objReturn.registroANCINEExibidor.ToString();
                            sal_cd = objReturn.registroANCINESala.ToString();
                            dia_cin = objReturn.diaCinematografico;


                            //TEM MENSAGEM DE ERRO.
                            if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                            {

                                TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
                                DateTime dtHoraIni;


                                foreach (var msg in objReturn.mensagens)
                                {

                                    if (msg.tipoMensagem == "I")
                                    {
                                        statusMsg = statusMsg + "I";
                                    }
                                    else if (msg.tipoMensagem == "A")
                                    {
                                        statusMsg = statusMsg + "A";
                                    }
                                    else if (msg.tipoMensagem == "E")
                                    {
                                        statusMsg = statusMsg + "E";
                                    }



                                    //TEM MENSAGEM NUMA SESSÃO ESPECIFICA.
                                    if (msg.dataHoraInicio != null)
                                    {
                                        dtHoraIni = Convert.ToDateTime(msg.dataHoraInicio);

                                        var listaBilMsg = (from b in db.TB_BILHETERIA
                                                           join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                                                           where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni && b.BIL_STATUS_PROT != "V" && b.BIL_STATUS_PROT != ""
                                                           select new MensagensANCINE()
                                                           {
                                                               BIL_ID = b.BIL_ID,
                                                               BIL_DIA_CIN = b.BIL_DIA_CIN,
                                                               SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                                               BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                                               BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                                               BIL_PROT = b.BIL_PROT,
                                                               BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                                               BIL_RETIF = b.BIL_RETIF,
                                                               EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                                               SEA_ID = s.SEA_ID,
                                                               FIL_CD_ANCINE = s.FIL_CD_ANCINE,
                                                               SEA_DIS_CNPJ = s.SEA_DIS_CNPJ,
                                                               SEA_DIS_NM = s.SEA_DIS_NM,
                                                               SEA_DT_HR_INICIO = s.SEA_DT_HR_INICIO,
                                                               SEA_FIL_NM = s.SEA_FIL_NM,
                                                               SEA_FIL_TP_PROJECAO = s.SEA_FIL_TP_PROJECAO,
                                                               SEA_MODAL = s.SEA_MODAL,
                                                               SEA_RZ_SOCIAL = s.SEA_RZ_SOCIAL,
                                                               SEA_VRE_CNPJ = s.SEA_VRE_CNPJ
                                                           });

                                        foreach (var itemMsg in listaBilMsg)
                                        {
                                            TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                            TB_MSG.MSA_DT_MSG = DateTime.Now;
                                            TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                            TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                            TB_MSG.SEA_ID = itemMsg.SEA_ID;
                                            TB_MSG.SEA_DT_HR_INICIO = itemMsg.SEA_DT_HR_INICIO;
                                            TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                            TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                            TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                            db.TB_MENSAGEM_ANCINE.Add(TB_MSG);

                                            TB_BILHETERIA TB_BIL_ERRO = db.TB_BILHETERIA.Find(itemMsg.BIL_ID);
                                            if (TB_BIL_ERRO != null)
                                            {
                                                TB_BIL_ERRO.BIL_PROT = objReturn.numeroProtocolo;
                                                TB_BIL_ERRO.BIL_STATUS_PROT = statusMsg;
                                                db.Entry(TB_BIL_ERRO).State = EntityState.Modified;

                                                if (strBilId == "")
                                                {
                                                    strBilId = strBilId + itemMsg.BIL_ID;
                                                }
                                                else
                                                {
                                                    strBilId = strBilId + "," + itemMsg.BIL_ID;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        strBilIdArray = strBilId.Split(stringSeparators ,StringSplitOptions.None);
                                        dia_cin = objReturn.diaCinematografico;

                                        var listaBilMsg = (from b in db.TB_BILHETERIA
                                                           join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                                                           where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && b.BIL_STATUS_PROT != "V" && b.BIL_STATUS_PROT != ""
                                                           select new MensagensANCINE()
                                                           {
                                                               BIL_ID = b.BIL_ID,
                                                               BIL_DIA_CIN = b.BIL_DIA_CIN,
                                                               SAL_CD_ANCINE = b.SAL_CD_ANCINE,
                                                               BIL_HOUVE_SES = b.BIL_HOUVE_SES,
                                                               BIL_ADIMP_SALA = b.BIL_ADIMP_SALA,
                                                               BIL_PROT = b.BIL_PROT,
                                                               BIL_STATUS_PROT = b.BIL_STATUS_PROT,
                                                               BIL_RETIF = b.BIL_RETIF,
                                                               EMP_CD_ANCINE = b.EMP_CD_ANCINE,
                                                           });

                                        foreach (var itemMsg in listaBilMsg)
                                        {
                                            strbil_string = itemMsg.BIL_ID.ToString();

                                            //var results = Array.FindAll(strBilIdArray, s => s.Equals(strbil_string));

                                            if (!strBilIdArray.Contains(strbil_string))
                                            {
                                            }
                                            else
                                            {
                                                TB_MSG.BIL_ID = itemMsg.BIL_ID;
                                                TB_MSG.MSA_DT_MSG = DateTime.Now;
                                                TB_MSG.MSA_DT_HORA_MSG = DateTime.Now;
                                                TB_MSG.SAL_CD_ANCINE = itemMsg.SAL_CD_ANCINE;
                                                TB_MSG.MSA_TP_MSG = msg.tipoMensagem;
                                                TB_MSG.MSA_CD_MSG = msg.codigoMensagem;
                                                TB_MSG.MSA_TXT_MSG = msg.textoMensagem;
                                                db.TB_MENSAGEM_ANCINE.Add(TB_MSG);
                                            }


                                        }

                                    }
                                } //LOOP MENSAGENS COM SESSÃO
                            }

                            //NÃO TEM MENSAGEM DE ERRO.
                            else
                            {
                                strBilIdArray = strBilId.Split(stringSeparators ,StringSplitOptions.None);

                                var listaBilOK = (from b in db.TB_BILHETERIA
                                                   where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin
                                                   select b);

                                foreach (var itemOK in listaBilOK)
                                {
                                    if (!strBilIdArray.Contains(strbil_string))
                                    {
                                        TB_BILHETERIA TB_BIL_OK = db.TB_BILHETERIA.Find(itemOK.BIL_ID);
                                        if (TB_BIL_OK != null)
                                        {
                                            TB_BIL_OK.BIL_PROT = objReturn.numeroProtocolo;
                                            TB_BIL_OK.BIL_STATUS_PROT = statusMsg;
                                            db.Entry(TB_BIL_OK).State = EntityState.Modified;

                                            if (strBilId == "")
                                            {
                                                strBilId = strBilId + itemOK.BIL_ID;
                                            }
                                            else
                                            {
                                                strBilId = strBilId + "," + itemOK.BIL_ID;
                                            }
                                        }


                                    }
                                }

                            }


                            // AQUI VOCÊ PODE PEGAR AS INFORMAÇÕES DE RETORNO, PARA GRAVAR EM BANCO, EXIBIR NA TELA, ETC...
                            //ViewBag.registroANCINEExibidor = objReturn.registroANCINEExibidor;
                            //ViewBag.registroANCINESala = objReturn.registroANCINESala;
                            ViewBag.diaCinematografico = objReturn.diaCinematografico.ToShortDateString();
                            //ViewBag.numeroProtocolo = objReturn.numeroProtocolo;
                            //ViewBag.statusProtocolo = objReturn.statusProtocolo;
                        }
                    }
                } // LOOP TB_BILHETERIA

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ex = ex.Message;
                return View();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        //********************************************
        public JsonResult SalaByEmpresa(string EMP_CD)
        {

            List<TB_SALA> _Salalst = GetSalasID(EMP_CD);
            List<SelectListItem> _liEmp = new List<SelectListItem>();

            var _context = new SCBEntities();

            foreach (var _itemsala in _Salalst)
            {
                _liEmp.Add(new SelectListItem { Value = _itemsala.SAL_CD_ANCINE, Text = _itemsala.SAL_DESC });
            }

            return Json(new SelectList(_liEmp, "value", "Text"), JsonRequestBehavior.AllowGet);
        }

        //********************************************
        public List<TB_SALA> GetSalasID(string EMP_CD)
        {
            var _context = new SCBEntities();
            List<TB_SALA> _lst = (from s in _context.TB_SALA.Where(x => x.EMP_CD_ANCINE == EMP_CD) orderby s.SAL_DESC select s).ToList();
            return _lst;
        }

        [HttpGet]
        public JsonResult VendRemotoByEmpresa(string EMP_CD)
        {

            List<TB_VENDEDOR_REMOTO> _VendRemlst = GetVendRemotoID(EMP_CD);
            List<SelectListItem> _liEmpresa = new List<SelectListItem>();

            var _context = new SCBEntities();

            foreach (var _itemVendRem in _VendRemlst)
            {
                _liEmpresa.Add(new SelectListItem { Value = _itemVendRem.VRE_CNPJ.ToString(), Text = _itemVendRem.VRE_RZ_SOCIAL });
            }

            return Json(new SelectList(_liEmpresa, "value", "Text"), JsonRequestBehavior.AllowGet);
        }

        public List<TB_VENDEDOR_REMOTO> GetVendRemotoID(string EMP_CD)
        {
            var _context = new SCBEntities();
            List<TB_VENDEDOR_REMOTO> _lst = (from s in _context.TB_VENDEDOR_REMOTO.Where(x => x.EMP_CD_ANCINE == EMP_CD) orderby s.VRE_RZ_SOCIAL select s).ToList();
            return _lst;
        }

        //[HttpGet]
        //public JsonResult GetQtdLugaresSala(string sal_cd_ancine)
        //{
        //    sal_cd_ancine = sal_cd_ancine.Replace("\n", "");
        //    TB_SALA tb_sala = db.TB_SALA.Find(sal_cd_ancine);

        //    if(sal_cd_ancine != null && sal_cd_ancine != "0" && sal_cd_ancine != "")
        //    {
        //        if(tb_sala == null)
        //        {
        //            var lugares = new
        //            {
        //                SAL_QTD_LUG_PDR = "",
        //                SAL_QTD_LUG_ESP = ""
        //            };
        //            return Json(lugares);
        //        }
        //        else
        //        {
        //            var lugares = new
        //            {
        //                SAL_QTD_LUG_PDR = tb_sala.SAL_QTD_LUG_PDR,
        //                SAL_QTD_LUG_ESP = tb_sala.SAL_QTD_LUG_ESP
        //            };
        //            return Json(lugares);
        //        }
        //    }
        //    else
        //    {
        //        var lugares = new
        //        {
        //            SAL_QTD_LUG_PDR = "",
        //            SAL_QTD_LUG_ESP = ""
        //        };
        //        return Json(lugares);
        //    }
        //}

        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string FormataValorXML(string valor)
            {
            string strValorAux = "";

            if (valor == "0" || valor == "") { strValorAux = "0.00"; }
            else
                {
                if (valor.Length == 1)
                    {
                    strValorAux = "0.0" + valor;
                    }

                if (valor.Length == 2)
                    {
                    strValorAux = "0." + valor;
                    }

                if (valor.Length > 2)
                    {
                    strValorAux = Left(valor ,(valor.Length - 2)) + "." + Right(valor ,2);
                    }
                }

            //for (var i = 0; i < valor.Length; i++)
            //    {

            //    }

            return strValorAux;
            }

        public static string TrataXMLSend(string[] strBilIds, string[] strStProt, string[] strProt,SCBEntities db)
        {
            try
            {
                
                //string retorno = "";
                int bilAux = 0;

                for (var item = 0; item < strBilIds.Length; item++)
                {
                    bilAux = Convert.ToInt16(strBilIds[item]);

                    TB_BILHETERIA TB_BIL = db.TB_BILHETERIA.Find(bilAux);
                    if (TB_BIL != null)
                    {
                        //if(strStProt[item] == "N" || strStProt[item] == "E" || strStProt[item] == "R")
                        //{
                        //    TB_BIL.BIL_RETIF = "N";
                        //}
                        TB_BIL.BIL_PROT = (strProt[item] == "0" ? "" : strProt[item]);
                        TB_BIL.BIL_STATUS_PROT = strStProt[item];
                        db.Entry(TB_BIL).State = EntityState.Modified;
                    }
                }
 
                //db.SaveChanges();

            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2601:
                    db.SaveChanges();
                    break;

                    default:
                    throw;
                }
                return ex.Message;
            }

            return "true";
        }

    }
}
