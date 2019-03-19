using System;
using System.IO;
using System.Net;
using System.Text;

namespace NMMA.Concur
{
    public class RequestWrapper
    {
        public HttpWebResponse MakeHttpRequestBasic(string baseurl, string httpMethod, string credentials)
        {
            string authString = Convert.ToBase64String(Encoding.Default.GetBytes(credentials));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl);
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }            
        }
        public HttpWebResponse MakeHttpTokenRequest(string baseurl, string httpMethod, string reqdata, string token)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl);
            request.Method = httpMethod;
            
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/xml";

            if (httpMethod.Equals("POST"))
            {
                request.Method = httpMethod;
                StringBuilder reqbody = new StringBuilder();
                reqbody.Append(reqdata);
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(reqbody.ToString());            
                request.ContentLength = data.Length;
                using (Stream newStream = request.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }                
            }

            try
            {                
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public HttpWebResponse MakeHttpTokenRequest_ContentSupport(string baseurl, string httpMethod, string reqdata, string contentType)
        {

            try
            {
                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl);
                if (httpMethod.Equals("POST") || httpMethod.Equals("PUT") || httpMethod.Equals("DELETE"))
                {
                    request.Method = httpMethod.ToString();
                    //request.Headers["Cache-Control"] = "no-cache";
                    request.ContentType = contentType;
                    Byte[] data = System.Text.Encoding.UTF8.GetBytes(reqdata);
                    request.ContentLength = data.Length;
                                    
                    using (Stream newStream = request.GetRequestStream())
                    {
                        newStream.Write(data, 0, data.Length);
                    }
                }
                          
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message.ToString());
                StringBuilder sb = new StringBuilder();
                sb.Append(ex.Message.ToString() + Environment.NewLine);
                if (ex.Response.ContentLength != 0)
                {
                    using (Stream stream = ex.Response.GetResponseStream())
                    {
                        using (StreamReader reader= new StreamReader(stream))
                        {
                            sb.Append(reader.ReadToEnd());
                            throw new System.Exception(sb.ToString());
                        }
                    }
                }
            }
            return null;
        }
    }
}

