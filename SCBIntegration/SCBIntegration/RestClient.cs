using System;
using System.IO;
using System.Net;
using System.Text;

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}

namespace SCBIntegration
{
    public class RestClient
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public NetworkCredential Credential { get; set; }

        public string AuthorizationToken { get; set; }

        public RestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            PostData = "";
        }
        public RestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = "";
        }

        public RestClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = postData;
        }

        public RestClient(string endpoint, HttpVerb method, string postData, string authorizationToken)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            PostData = postData;
            AuthorizationToken = authorizationToken;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }
         
        public string MakeRequest(string parameters)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

                request.Method = Method.ToString();
                request.Headers.Add(HttpRequestHeader.Authorization, AuthorizationToken);
                request.ContentLength = 0;
                request.ContentType = ContentType;
                if (Credential != null) request.Credentials = Credential;
                if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
                {
                    var encoding = new UTF8Encoding();
                    var bytes = encoding.GetBytes(PostData); //Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created && ((int)response.StatusCode) != 422)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null && ex.Response is HttpWebResponse)
                {
                    if (((int)((HttpWebResponse)ex.Response).StatusCode) == 400)
                    {
                        throw new Exception("Erro 400 – Requisição ruim: A requisição não pôde ser interpretada pelo servidor em razão de erros de formato/sintaxe.", ex);
                    }
                    else if (((int)((HttpWebResponse)ex.Response).StatusCode) == 401)
                    {
                        throw new Exception("Erro 401 – Não autorizado: A requisição requer autenticação por parte do cliente e as informações de autenticação não foram localizadas ou não são válidas.", ex);
                    }
                    else if (((int)((HttpWebResponse)ex.Response).StatusCode) == 403)
                    {
                        throw new Exception("Erro 403 – Proibido: A requisição foi realizada com informações de autenticação válidas mas está tentando realizar uma ação ou acessar um recurso não autorizado para o cliente.", ex);
                    }
                    else if (((int)((HttpWebResponse)ex.Response).StatusCode) == 404)
                    {
                        throw new Exception("Erro 404 – Não encontrado: O servidor não localizou o recurso solicitado.", ex);
                    }
                    else if (((int)((HttpWebResponse)ex.Response).StatusCode) == 405)
                    {
                        throw new Exception("Erro 405 – Método não permitido: O método HTTP utilizado não é permitido para o recurso identificado na URL.", ex);
                    }
                    else if (((int)((HttpWebResponse)ex.Response).StatusCode) == 422)
                    {
                        HttpWebResponse resp = (HttpWebResponse)ex.Response;
                        using (var responseStream = resp.GetResponseStream())
                        {
                            if (responseStream != null)
                                using (var reader = new StreamReader(responseStream))
                                {
                                    return reader.ReadToEnd();
                                }
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

    } // class

}
