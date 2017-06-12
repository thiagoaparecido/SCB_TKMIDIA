using SCBIntegration.Entities;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SCBIntegration
{
    public class SCBIntegrationManager
    {
        private string EndpointURL { get; set; }

        private string AuthorizationToken { get; set; }

        public SCBIntegrationManager(string endpointURL, string authorizationToken)
        {
            EndpointURL = endpointURL;
            AuthorizationToken = authorizationToken;
        }

        public StatusRelatorioBilheteria RegistroBilheteriaSalaExibicao(Bilheteria objBilheteria)
        {
            StatusRelatorioBilheteria objStatusRelatorioBilheteria = null;
            string resourceURL = "/scb/v1.0/bilheterias/";
            try
            {
                Debug.WriteLine("----------- " + DateTime.Now + "CHAMOU COMPONENTE XML");

                RestClient objRestClient = new RestClient("");
                objRestClient.EndPoint = EndpointURL + resourceURL;
                objRestClient.Method = HttpVerb.POST;
                objRestClient.PostData = objBilheteria.ToXml();
                objRestClient.ContentType = "application/xml";
                objRestClient.AuthorizationToken = AuthorizationToken;

                var returnObject = objRestClient.MakeRequest();
                if (!string.IsNullOrEmpty(returnObject))
                {
                    XmlSerializer serializer = serializer = new XmlSerializer(typeof(StatusRelatorioBilheteria));
                    
                    using (TextReader reader = new StringReader(returnObject))
                    {
                        objStatusRelatorioBilheteria = (StatusRelatorioBilheteria)serializer.Deserialize(reader);
                    }
                }
            }
            catch (WebException ex)
            {                
                throw ex;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
             
            return objStatusRelatorioBilheteria;            
        }

        public StatusRelatorioBilheteria ConsultaProtocoloPorID(string numeroProtocolo)
        {
            StatusRelatorioBilheteria objStatusRelatorioBilheteria = null;
            string resourceURL = "/scb/v1.0/protocolos/" + numeroProtocolo;
            try
            {
                RestClient objRestClient = new RestClient("");
                objRestClient.EndPoint = EndpointURL + resourceURL;
                objRestClient.Method = HttpVerb.GET;
                objRestClient.ContentType = "application/xml";
                objRestClient.AuthorizationToken = AuthorizationToken;

                var returnObject = objRestClient.MakeRequest();

                if (!string.IsNullOrEmpty(returnObject))
                {
                    XmlSerializer serializer = serializer = new XmlSerializer(typeof(StatusRelatorioBilheteria));
                    
                    using (TextReader reader = new StringReader(returnObject))
                    {
                        objStatusRelatorioBilheteria = (StatusRelatorioBilheteria)serializer.Deserialize(reader);
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objStatusRelatorioBilheteria;
        }

        public ListaStatusRegistroBilheteria ConsultaProtocolosUmDiaCinematografico(DateTime diaCinematografico)
        {
            ListaStatusRegistroBilheteria objListaStatusRegistroBilheteria = null;
            string resourceURL = "/scb/v1.0/protocolos/?diaCinematografico=" + diaCinematografico.ToString("yyyy-MM-dd");
            try
            {
                RestClient objRestClient = new RestClient("");
                objRestClient.EndPoint = EndpointURL + resourceURL;
                objRestClient.Method = HttpVerb.GET;
                objRestClient.ContentType = "application/xml";
                objRestClient.AuthorizationToken = AuthorizationToken;

                var returnObject = objRestClient.MakeRequest();
                
                if (!string.IsNullOrEmpty(returnObject))
                {
                    if (returnObject.Contains("statusRelatorioBilheteria"))
                    {
                        XmlSerializer serializer = serializer = new XmlSerializer(typeof(StatusRelatorioBilheteria));

                        using (TextReader reader = new StringReader(returnObject))
                        {
                            StatusRelatorioBilheteria objStatusRelatorioBilheteria = (StatusRelatorioBilheteria)serializer.Deserialize(reader);

                            objListaStatusRegistroBilheteria = new ListaStatusRegistroBilheteria();

                            StatusRegistroBilheteria objStatusRegistroBilheteria = new StatusRegistroBilheteria();
                            objStatusRegistroBilheteria.diaCinematografico = objStatusRelatorioBilheteria.diaCinematografico;
                            objStatusRegistroBilheteria.mensagens = objStatusRelatorioBilheteria.mensagens;
                            objStatusRegistroBilheteria.numeroProtocolo = objStatusRelatorioBilheteria.numeroProtocolo;
                            objStatusRegistroBilheteria.registroANCINEExibidor = objStatusRelatorioBilheteria.registroANCINEExibidor;
                            objStatusRegistroBilheteria.statusProtocolo = objStatusRelatorioBilheteria.statusProtocolo;

                            List<StatusRegistroBilheteria> auxList = new List<StatusRegistroBilheteria>();
                            auxList.Add(objStatusRegistroBilheteria);

                            objListaStatusRegistroBilheteria.StatusRegistroBilheteriaList = auxList.ToArray();
                        }
                    }
                    else
                    {
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(returnObject);
                        string xmlAux = xmldoc.ChildNodes[1].InnerXml;

                        xmlAux = xmldoc.FirstChild.OuterXml + "<StatusRegistroBilheteriaRoot><StatusRegistroBilheteriaList>" + xmlAux + "</StatusRegistroBilheteriaList></StatusRegistroBilheteriaRoot>";

                        XmlSerializer serializer = serializer = new XmlSerializer(typeof(ListaStatusRegistroBilheteria));

                        using (TextReader reader = new StringReader(xmlAux))
                        {
                            objListaStatusRegistroBilheteria = (ListaStatusRegistroBilheteria)serializer.Deserialize(reader);
                        }
                    }                    
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objListaStatusRegistroBilheteria;
        }

        public AdimplenciaExibidor ConsultaSituacaoAdimplencia(DateTime diaAdimplencia)
        {
            AdimplenciaExibidor objAdimplenciaExibidor = null;
            string resourceURL = "/scb/v1.0/adimplencia/" + diaAdimplencia.ToString("yyyy-MM-dd");
            try
            {
                RestClient objRestClient = new RestClient("");
                objRestClient.EndPoint = EndpointURL + resourceURL;
                objRestClient.Method = HttpVerb.GET;
                objRestClient.ContentType = "application/xml";
                objRestClient.AuthorizationToken = AuthorizationToken;

                var returnObject = objRestClient.MakeRequest();

                if (!string.IsNullOrEmpty(returnObject))
                {
                    if (returnObject.Contains("statusRelatorioBilheteria"))
                    {
                        XmlSerializer serializer = serializer = new XmlSerializer(typeof(StatusRelatorioBilheteria));

                        using (TextReader reader = new StringReader(returnObject))
                        {
                            StatusRelatorioBilheteria objStatusRelatorioBilheteria = (StatusRelatorioBilheteria)serializer.Deserialize(reader);

                            objAdimplenciaExibidor = new AdimplenciaExibidor();

                            objAdimplenciaExibidor.statusRelatorioBilheteria = objStatusRelatorioBilheteria;

                        }
                    }
                    else
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(AdimplenciaExibidor));
                        using (TextReader reader = new StringReader(returnObject))
                        {
                            objAdimplenciaExibidor = (AdimplenciaExibidor)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objAdimplenciaExibidor;
        }

    }
}
