using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCBIntegration;
using SCBIntegration.Entities;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;

namespace WindowsFormsTest
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void btnTesteRest_RegistroBilheteriaSalaExibicao_Click(object sender, EventArgs e)
        {
            Bilheteria objBilheteria = new Bilheteria();

            // ============================================================================================
            // 1 - PREENCHE O OBJETO BILHETERIA
            // ============================================================================================

            objBilheteria.registroANCINEExibidor = 13286;
            objBilheteria.registroANCINESala = 5002156;
            objBilheteria.diaCinematografico = DateTime.Now;
            objBilheteria.houveSessoes = "S";
            objBilheteria.retificador = "N";

            // ===================================================================================
            // 1.a - INICIALIZA A LISTA DE SESSOES
            // ===================================================================================
            List<Sessao> listaSessoes = new List<Sessao>();

            if (listaSessoes != null)
            {
                // ---------------------------------------------------------------
                // 1.a.i - SESSAO 1
                // ---------------------------------------------------------------
                Sessao sessao1 = new Sessao();
                sessao1.dataHoraInicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //2017-03-31 21:00:00
                sessao1.modalidade = "A";
                sessao1.vendedorRemoto = new VendedorRemoto();

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
                    obra1_da_sessao1.numeroObra = "E1600764000000";
                    obra1_da_sessao1.tituloObra = "FRAGMENTADO";
                    obra1_da_sessao1.tipoTela = "P";
                    obra1_da_sessao1.digital = "S";
                    obra1_da_sessao1.tipoProjecao = 2;
                    obra1_da_sessao1.audio = "D";
                    obra1_da_sessao1.legenda = "N";
                    obra1_da_sessao1.libras = "N";
                    obra1_da_sessao1.legendagemDescritiva = "N";
                    obra1_da_sessao1.audioDescricao = "N";

                    // DISTRIBUIDOR DA OBRA 1
                    obra1_da_sessao1.distribuidor = new Distribuidor();
                    obra1_da_sessao1.distribuidor.cnpj = 24810280000161;
                    obra1_da_sessao1.distribuidor.razaoSocial = "UNIVERSAL PICTURES INTERNATIONAL BRAZIL LTDA.";

                    // -----------------------------------------------------
                    // ADICIONA A OBRA 1 DENTRO DA LISTA DE OBRAS DA SESSAO 1                        
                    listaObrasSessao1.Add(obra1_da_sessao1);
                    // -----------------------------------------------------

                }

                // PREENCHE O ARRAY DE OBRAS DENTRO DA SESSAO 1
                sessao1.obras = listaObrasSessao1.ToArray();


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
                        quantidadeDisponibilizada = 236,
                        totalizacoesCategoriaIngresso = new TotalizacaoCategoriaIngresso[]
                        {
                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 1,
                                quantidadeEspectadores = 85,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 1120
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 70
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
                                    }
                                }
                            },

                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 2,
                                quantidadeEspectadores = 102,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 1400
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 28
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
                                    }
                                }
                            },

                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 3,
                                quantidadeEspectadores = 0,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
                                    }
                                }
                            },

                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 4,
                                quantidadeEspectadores = 0,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
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
                        quantidadeDisponibilizada = 4,
                        totalizacoesCategoriaIngresso = new TotalizacaoCategoriaIngresso[]
                        {
                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 1,
                                quantidadeEspectadores = 1,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 8
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
                                    }
                                }
                            },

                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 2,
                                quantidadeEspectadores = 0,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
                                    }
                                }
                            },

                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 3,
                                quantidadeEspectadores = 0,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
                                    }
                                }
                            },

                            new TotalizacaoCategoriaIngresso
                            {
                                codigoCategoriaIngresso = 4,
                                quantidadeEspectadores = 0,
                                totalizacoesModalidadePagamento = new TotalizacaoModalidadePagamento[]
                                {
                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 1,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 2,
                                        valorArrecadado = 0
                                    },

                                    new TotalizacaoModalidadePagamento
                                    {
                                        codigoModalidadePagamento = 3,
                                        valorArrecadado = 0
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


            // AQUI VOCÊ BUSCA OS PARAMETROS NO APP.CONFIG, ou DEPOIS PODE BUSCAR EM ALGUMA TABELA DE PARAMETROS GLOBAIS - FICA À SUA ESCOLHA
            string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
            string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

            // AQUI VOCÊ INSTANCIA O OBJETO 'MANAGER' DO SERVIÇO, PARA DEPOIS CHAMAR O MÉTODO DESEJADO
            // - VOCÊ JÁ TEM QUE ENVIAR A URL E O TOKEN
            SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);

            try
            {
                // AQUI VOCÊ CHAMA O MÉTODO, PASSANDO COMO PARAMETRO O OBJETO 'BILHETERIA' JÁ PREENCHIDO
                StatusRelatorioBilheteria objReturn = objSCBIntegrationManager.RegistroBilheteriaSalaExibicao(objBilheteria);

                // VALIDA SE O RETORNO NÃO É NULO
                if (objReturn != null)
                {
                    // EXIBE POSSIVEIS MENSAGENS DE RETORNO: I - Informativa; A - Alerta; E - Erro
                    if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                    {
                        foreach(var msg in objReturn.mensagens)
                        {
                            // AQUI VOCÊ DEVE TRATAR AS MENSAGENS DE RETORNO CONFORME SUA NECESSIDADE
                            // - OBS: Campo "tipoMensagem" = Código que especifica o tipo da mensagem. Sendo: I - Informativa; A - Alerta; E - Erro
                            if(msg.tipoMensagem == "I")
                            {
                                MessageBox.Show(msg.textoMensagem, "Informativo: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (msg.tipoMensagem == "A")
                            {
                                MessageBox.Show(msg.textoMensagem, "Alerta: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else if (msg.tipoMensagem == "E")
                            {
                                MessageBox.Show(msg.textoMensagem, "Erro: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }

                    // AQUI VOCÊ PODE PEGAR AS INFORMAÇÕES DE RETORNO, PARA GRAVAR EM BANCO, EXIBIR NA TELA, ETC...
                    var campo1 = objReturn.registroANCINEExibidor;
                    var campo2 = objReturn.registroANCINESala;
                    var campo3 = objReturn.diaCinematografico;
                    var campo4 = objReturn.numeroProtocolo;
                    var campo5 = objReturn.statusProtocolo;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar Bilheteria: \n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
         
        private void btn_ConsultaProtocoloPorID_Click(object sender, EventArgs e)
        {
            // AQUI VOCÊ BUSCA OS PARAMETROS NO APP.CONFIG, ou DEPOIS PODE BUSCAR EM ALGUMA TABELA DE PARAMETROS GLOBAIS - FICA À SUA ESCOLHA
            string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
            string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

            // AQUI VOCÊ INSTANCIA O OBJETO 'MANAGER' DO SERVIÇO, PARA DEPOIS CHAMAR O MÉTODO DESEJADO
            // - VOCÊ JÁ TEM QUE ENVIAR A URL E O TOKEN
            SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);


            try
            {
                // PREENCHE O ID DO PROTOCOLO QUE DESEJA CONSULTAR
                string IdProtocolo = "5002156.1.31032017.001";

                // AQUI VOCÊ CHAMA O MÉTODO, PASSANDO COMO PARAMETRO O ID DO PROTOCOLO DESEJADO                
                StatusRelatorioBilheteria objReturn = objSCBIntegrationManager.ConsultaProtocoloPorID(IdProtocolo);

                // VALIDA SE O RETORNO NÃO É NULO
                if (objReturn != null)
                {
                    // EXIBE POSSIVEIS MENSAGENS DE RETORNO: I - Informativa; A - Alerta; E - Erro
                    if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                    {
                        foreach (var msg in objReturn.mensagens)
                        {
                            // AQUI VOCÊ DEVE TRATAR AS MENSAGENS DE RETORNO CONFORME SUA NECESSIDADE
                            // - OBS: Campo "tipoMensagem" = Código que especifica o tipo da mensagem. Sendo: I - Informativa; A - Alerta; E - Erro
                            if (msg.tipoMensagem == "I")
                            {
                                MessageBox.Show(msg.textoMensagem, "Informativo: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (msg.tipoMensagem == "A")
                            {
                                MessageBox.Show(msg.textoMensagem, "Alerta: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else if (msg.tipoMensagem == "E")
                            {
                                MessageBox.Show(msg.textoMensagem, "Erro: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    // AQUI VOCÊ PODE PEGAR AS INFORMAÇÕES DE RETORNO, PARA GRAVAR EM BANCO, EXIBIR NA TELA, ETC...
                    var campo1 = objReturn.registroANCINEExibidor;
                    var campo2 = objReturn.registroANCINESala;
                    var campo3 = objReturn.diaCinematografico;
                    var campo4 = objReturn.numeroProtocolo;
                    var campo5 = objReturn.statusProtocolo;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar Bilheteria: \n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ProtocolosUmDiaCinematografico_Click(object sender, EventArgs e)
        {
            // AQUI VOCÊ BUSCA OS PARAMETROS NO APP.CONFIG, ou DEPOIS PODE BUSCAR EM ALGUMA TABELA DE PARAMETROS GLOBAIS - FICA À SUA ESCOLHA
            string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
            string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

            // AQUI VOCÊ INSTANCIA O OBJETO 'MANAGER' DO SERVIÇO, PARA DEPOIS CHAMAR O MÉTODO DESEJADO
            // - VOCÊ JÁ TEM QUE ENVIAR A URL E O TOKEN
            SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);


            try
            {
                // PREENCHE A DATA COM O DIA CINEMATOGRAFICO, QUE SERÁ UTILIZADO NA CONSULTA DE PROTOCOLOS
                DateTime dataDiaCinematografico = Convert.ToDateTime("2017-03-31");

                // AQUI VOCÊ CHAMA O MÉTODO, PASSANDO COMO PARAMETRO A DATA COM O DIA CINEMATOGRAFICO
                ListaStatusRegistroBilheteria objReturnListaStatusRegistroBilheteria = objSCBIntegrationManager.ConsultaProtocolosUmDiaCinematografico(dataDiaCinematografico);

                // VALIDA SE O RETORNO NÃO É NULO
                if (objReturnListaStatusRegistroBilheteria != null)
                {
                    // ESSE MÉTODO EM ESPECÍFICO RETORNA UMA LISTA DE 'StatusRegistroBilheteria'
                    foreach (var objReturn in objReturnListaStatusRegistroBilheteria.StatusRegistroBilheteriaList)
                    {
                        // EXIBE POSSIVEIS MENSAGENS DE RETORNO: I - Informativa; A - Alerta; E - Erro
                        if (objReturn.mensagens != null && objReturn.mensagens.Count() > 0)
                        {
                            foreach (var msg in objReturn.mensagens)
                            {
                                // AQUI VOCÊ DEVE TRATAR AS MENSAGENS DE RETORNO CONFORME SUA NECESSIDADE
                                // - OBS: Campo "tipoMensagem" = Código que especifica o tipo da mensagem. Sendo: I - Informativa; A - Alerta; E - Erro
                                if (msg.tipoMensagem == "I")
                                {
                                    MessageBox.Show(msg.textoMensagem, "Informativo: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (msg.tipoMensagem == "A")
                                {
                                    MessageBox.Show(msg.textoMensagem, "Alerta: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else if (msg.tipoMensagem == "E")
                                {
                                    MessageBox.Show(msg.textoMensagem, "Erro: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        // AQUI VOCÊ PODE PEGAR AS INFORMAÇÕES DE RETORNO, PARA GRAVAR EM BANCO, EXIBIR NA TELA, ETC...
                        var campo1 = objReturn.registroANCINEExibidor;
                        var campo2 = objReturn.registroANCINESala;
                        var campo3 = objReturn.diaCinematografico;
                        var campo4 = objReturn.numeroProtocolo;
                        var campo5 = objReturn.statusProtocolo;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar Bilheteria: \n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ConsultaAdimplencia_Click(object sender, EventArgs e)
        {
            // AQUI VOCÊ BUSCA OS PARAMETROS NO APP.CONFIG, ou DEPOIS PODE BUSCAR EM ALGUMA TABELA DE PARAMETROS GLOBAIS - FICA À SUA ESCOLHA
            string str_SCB_URL_Endpoint = ConfigurationManager.AppSettings["SCB_URL_Endpoint"];
            string str_SCB_AuthorizationToken = ConfigurationManager.AppSettings["SCB_AuthorizationToken"];

            // AQUI VOCÊ INSTANCIA O OBJETO 'MANAGER' DO SERVIÇO, PARA DEPOIS CHAMAR O MÉTODO DESEJADO
            // - VOCÊ JÁ TEM QUE ENVIAR A URL E O TOKEN
            SCBIntegrationManager objSCBIntegrationManager = new SCBIntegrationManager(str_SCB_URL_Endpoint, str_SCB_AuthorizationToken);

            try
            {
                // PREENCHE A DATA COM O DIA DA ADIMPLENCIA, QUE SERÁ UTILIZADO NA CONSULTA DE ADIMPLENCIA
                DateTime dataAdimplencia = DateTime.Now;

                // AQUI VOCÊ CHAMA O MÉTODO, PASSANDO COMO PARAMETRO A DATA COM O DIA CINEMATOGRAFICO
                AdimplenciaExibidor objReturn = objSCBIntegrationManager.ConsultaSituacaoAdimplencia(dataAdimplencia);

                // VALIDA SE O RETORNO NÃO É NULO
                if (objReturn != null)
                {
                    // PRIMEIRO VALIDA SE RETORNOU ALGUMA MENSAGEM
                    if(objReturn.statusRelatorioBilheteria != null)
                    {
                        if (objReturn.statusRelatorioBilheteria.mensagens != null && objReturn.statusRelatorioBilheteria.mensagens.Count() > 0)
                        {
                            foreach (var msg in objReturn.statusRelatorioBilheteria.mensagens)
                            {
                                // AQUI VOCÊ DEVE TRATAR AS MENSAGENS DE RETORNO CONFORME SUA NECESSIDADE
                                // - OBS: Campo "tipoMensagem" = Código que especifica o tipo da mensagem. Sendo: I - Informativa; A - Alerta; E - Erro
                                if (msg.tipoMensagem == "I")
                                {
                                    MessageBox.Show(msg.textoMensagem, "Informativo: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (msg.tipoMensagem == "A")
                                {
                                    MessageBox.Show(msg.textoMensagem, "Alerta: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else if (msg.tipoMensagem == "E")
                                {
                                    MessageBox.Show(msg.textoMensagem, "Erro: " + msg.codigoMensagem, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                    }

                    // AQUI VOCÊ PODE PEGAR AS INFORMAÇÕES DE RETORNO, PARA GRAVAR EM BANCO, EXIBIR NA TELA, ETC...
                    var campo1 = objReturn.registroANCINEExibidor;
                    var campo2 = objReturn.diaCinematografico;
                    var campo3 = objReturn.diaCinematografico;
                    
                    if(objReturn.adimplenciaSalas != null && objReturn.adimplenciaSalas.Count() > 0)
                    {
                        // PERCORRE A LISTA DE POSSIVEIS RETORNOS DE OBJETOS DO TIPO 'ADIMPLENCIA SALAS'
                        foreach(var adimplenciaSala in objReturn.adimplenciaSalas)
                        {
                            // POSSÍVEIS CAMPOS PARA MANIPULAÇÃO
                            var campo1_AdimplenciaSala = adimplenciaSala.registroANCINESala;
                            var campo2_AdimplenciaSala = adimplenciaSala.situacaoSala;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar Bilheteria: \n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        
    }
}
