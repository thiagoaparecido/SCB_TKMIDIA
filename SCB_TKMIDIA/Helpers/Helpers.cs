using SCBIntegration.Entities;
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCB_TKMIDIA.Models;
using SCB_TKMIDIA.Controllers;
using System.Data.Entity;
using System.Threading;

namespace SCB_TKMIDIA.Helpers
{
    public class Helpers
    {
        public string[] CalcSemanaCine()
        {
            //De quinta à quarta da próxima semana.
            string dtIni = DateTime.Now.ToShortDateString();
            string dtFim = DateTime.Now.ToShortDateString();
            string[] dateValues = { dtIni, dtFim };

            DateTime hoje = DateTime.Now.Date;


            if (hoje.DayOfWeek == DayOfWeek.Thursday)
            {
                dtIni = hoje.ToShortDateString();
                dtFim = hoje.AddDays(6).ToShortDateString();

                string[] dateValues1 = { dtIni, dtFim };
                return dateValues1;
            }
            else
            {
                if (hoje.DayOfWeek < DayOfWeek.Thursday)
                {
                    DateTime dtWhile = hoje;

                    while (dtWhile.DayOfWeek != DayOfWeek.Thursday)
                    {
                        dtWhile = dtWhile.AddDays(-1);
                    }

                    dtIni = dtWhile.ToShortDateString();
                    dtFim = dtWhile.AddDays(6).ToShortDateString();

                    string[] dateValues2 = { dtIni, dtFim };
                    return dateValues2;
                }
                else
                {
                    if (hoje.DayOfWeek > DayOfWeek.Thursday)
                    {

                        DateTime dtWhile = hoje;

                        while (dtWhile.DayOfWeek != DayOfWeek.Wednesday)
                        {
                            dtWhile = dtWhile.AddDays(1);
                        }

                        dtFim = dtWhile.ToShortDateString();
                        dtIni = dtWhile.AddDays(-6).ToShortDateString();

                        string[] dateValues3 = { dtIni, dtFim };
                        return dateValues3;
                    }
                }
            }
            return dateValues;
        }

        public int TrataRetornoStatusANCINE(ListaStatusRegistroBilheteria objReturnListaStatusRegistroBilheteria)
        {
            SCBEntities db = new SCBEntities();
            string emp_cd = "";
            string sal_cd = "";
            DateTime dia_cin;
            DateTime dtHoraIni;

            foreach (var objReturn in objReturnListaStatusRegistroBilheteria.StatusRegistroBilheteriaList)
            {
                if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                {
                    TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();



                    foreach (var msg in objReturn.mensagens)
                    {
                        emp_cd = objReturn.registroANCINEExibidor.ToString();
                        sal_cd = objReturn.registroANCINESala.ToString();
                        dia_cin = objReturn.diaCinematografico;

                        if (msg.dataHoraInicio == null)
                        {


                            var listaBilMsg = (from b in db.TB_BILHETERIA
                                               where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin
                                               select b);

                            foreach (var itemMsg in listaBilMsg)
                            {

                                //TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
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
                        else
                        {

                            dtHoraIni = Convert.ToDateTime(msg.dataHoraInicio);

                            var listaBilMsg = (from b in db.TB_BILHETERIA
                                               join s in db.TB_SESSAO_ANCINE on b.BIL_ID equals s.BIL_ID
                                               where b.EMP_CD_ANCINE == emp_cd && b.SAL_CD_ANCINE == sal_cd && b.BIL_DIA_CIN == dia_cin && s.SEA_DT_HR_INICIO == dtHoraIni
                                               select b);

                            foreach (var itemMsg in listaBilMsg)
                            {

                                //TB_MENSAGEM_ANCINE TB_MSG = new TB_MENSAGEM_ANCINE();
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

                        db.SaveChanges();


                    }

                    string emp_cd_ok = objReturn.registroANCINEExibidor.ToString();
                    string sal_cd_ok = objReturn.registroANCINESala.ToString();
                    DateTime dia_cin_ok = objReturn.diaCinematografico;

                    var listaBil_ok = (from b in db.TB_BILHETERIA
                                       where b.EMP_CD_ANCINE == emp_cd_ok && b.SAL_CD_ANCINE == sal_cd_ok && b.BIL_DIA_CIN == dia_cin_ok
                                       select b);
                    foreach (var item_ok in listaBil_ok)
                    {
                        TB_BILHETERIA TB_BIL_OK = db.TB_BILHETERIA.Find(item_ok.BIL_ID);
                        TB_BIL_OK.BIL_PROT = objReturn.numeroProtocolo;
                        TB_BIL_OK.BIL_STATUS_PROT = objReturn.statusProtocolo;
                        db.Entry(TB_BIL_OK).State = EntityState.Modified;

                    }
                }
            }

                return 0;
        }

        public string FormataCodigoObraGenerica(string CodigoObra)
        {
            string strAux = "";
            string strRetorno ="";

            if(CodigoObra.Length != 1 && CodigoObra.Length != 2)
            {
                strAux = CodigoObra.Substring(0 ,3);

                switch (strAux)
                {
                    case "G1|":
                    strRetorno = "G0000000000001";
                    break;

                    case "G2|":
                    strRetorno = "G0000000000002";
                    break;

                    case "G3|":
                    strRetorno = "G0000000000003";
                    break;

                    case "G4|":
                    strRetorno = "G0000000000004";
                    break;
                }

            }

            return strRetorno;
        }

        public void LogSCB(string strMensagem)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string arquivo = "LogSCB_";
            string strDataHora = String.Format("{0:ddMMyyyy}", DateTime.Now);
            arquivo = path + arquivo + strDataHora + ".txt";

            using (StreamWriter outputFile = System.IO.File.AppendText(arquivo))
            {
                string linha_ = DateTime.Now.ToString() + " => " + strMensagem;
                //string linha_ = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", DateTime.Now.ToString() + " => " + strMensagem);

                outputFile.WriteLine(linha_);

            }

        }

        public static implicit operator string(Helpers v)
        {
            throw new NotImplementedException();
        }

    }
}