using System.IO;
using System.Net;
using System.Xml;

namespace NMMA.Concur
{
    public class Definition
    {
        private string privateId;
        private string privatedefxmlns;
        private string privateName;
        private string privateJobLink;

        public static RequestWrapper HttpWrapper = new RequestWrapper();

        public string Id
        {
            get { return privateId; }
            set { privateId = value; }
        }                

        public string Name
        {
            get { return privateName; }
            set { privateName = value; }
        }
        
        public string JobLink
        {
            get { return privateJobLink; }
            set { privateJobLink = value; }
        }       

        public string defxmlns
        {
            get { return privatedefxmlns; }
            set { privatedefxmlns = value; }
        }

        public DefinitionList getDefinitions(string uri, string token)
        {
            Utilities util = new Utilities();
            HttpWebResponse response = HttpWrapper.MakeHttpTokenRequest(uri, "GET", "", token);
            string xml = util.getStringFromStream(response.GetResponseStream());
            DefinitionList definitions = new DefinitionList();
            if (xml == "")
            {
                return null;
            }

            XmlReader xmlReader = XmlReader.Create(new StringReader(xml));
            definitions.parseDefinitions(xmlReader);
            return definitions;
        }

        public Definition getDefinitionDetail(string uri, string token)
        {
            DefinitionList definitions = getDefinitions(uri, token);
            if (definitions == null)
            {
                return null;
            }
            else if (definitions.Count == 0)
            {
                return null;
            }
            else
            {
                return definitions[0];
            }
        }
    }
}
