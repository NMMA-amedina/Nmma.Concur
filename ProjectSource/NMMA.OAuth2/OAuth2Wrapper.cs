using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

//generic OAuth2 authenticator
namespace NMMA.OAuth2
{
    public class OAuth2Wrapper
    {
        private string refreshToken { get; set; }
        private string apiToken { get; set; }

        public string refresh_token
        {
            get { return refreshToken; }
            set { refreshToken = value; }
        }
        public string token
        {
            get { return apiToken; }
            set { apiToken = value; }
        }
        
        public object Authenticate(string url, string postData, string method, string contentType)
        {
            string retString = "";
            try
            {
                HttpWebResponse response = MakeHttpTokenRequest_ContentSupport(url, method, postData, contentType);
                retString = getResponseContentResults(response);
                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string getResponseContentResults(HttpWebResponse resp)
        {
            XmlDocument xmlDoc = new XmlDocument();
            StringBuilder sb = new StringBuilder();
            int statCode = Convert.ToInt32(resp.StatusCode);
            sb.Append("HTTP Status:" + resp.StatusCode.ToString() + Environment.NewLine);
            sb.Append("HTTP Status Code:" + statCode.ToString() + Environment.NewLine);
            sb.Append("HTTP Method:" + resp.Method.ToString() + Environment.NewLine);
            sb.Append("Content Type:" + resp.ContentType.ToString() + Environment.NewLine);
            sb.Append("Content Length:" + resp.ContentLength.ToString() + Environment.NewLine);
           
            if ((resp.ContentLength > 0) && (resp.ContentType.Contains("application/json")))
            {
                string json = getJsonFromStreamtoString(resp.GetResponseStream());
                sb.Append(Environment.NewLine + json + Environment.NewLine);
            }
            else if (resp.ContentLength > 0)
            {
                string content = getStringFromStream(resp.GetResponseStream());
                sb.Append(Environment.NewLine + content + Environment.NewLine); ;
            }
           
            return sb.ToString();
        }

        private string getJsonFromStreamtoString(Stream stream)
        {
            string retString = "";

            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                retString = reader.ReadToEnd();
                token = ((dynamic)JsonConvert.DeserializeObject(retString)).access_token;
                retString = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(retString), Newtonsoft.Json.Formatting.Indented);

            }
            return retString;
        }

        private string getStringFromStream(Stream stream)
        {
            string retString = "";
            token = ((dynamic)JsonConvert.DeserializeObject(retString)).access_token;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                retString = reader.ReadToEnd();
            }
            return retString;
        }

        private HttpWebResponse MakeHttpTokenRequest_ContentSupport(string baseurl, string httpMethod, string reqdata, string contentType)
        {

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl);
                if (httpMethod.Equals("POST") || httpMethod.Equals("PUT") || httpMethod.Equals("DELETE"))
                {
                    request.Method = httpMethod.ToString();
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
                        using (StreamReader reader = new StreamReader(stream))
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
