using System.Collections.Generic;
using System.Xml;

namespace NMMA.Concur
{
    public class DefinitionList: List<Definition>
    {
        private string privatedefxmlns;

        public string defxmlns
        {
            get { return privatedefxmlns; }
            set { privatedefxmlns = value; }
        }
     
        public void parseDefinitions(XmlReader xmlreader)
        {
            Definition currentDef = null;
            do
            {
                if(defxmlns == "") {
                    defxmlns = xmlreader.GetAttribute(0).ToString();
                }

                if (xmlreader.NodeType == XmlNodeType.Element)
                {
                    if (xmlreader.Name.Equals("definition") && xmlreader.IsStartElement())
                    {
                        currentDef = new Definition();
                        currentDef.defxmlns = defxmlns;
                    }
                    else if (xmlreader.Name.Equals("id"))
                    {
                        xmlreader.Read();
                        currentDef.Id = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("job-link"))
                    {
                        xmlreader.Read();
                        currentDef.JobLink = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("name"))
                    {
                        xmlreader.Read();
                        currentDef.Name = xmlreader.Value;
                    }
                }
                else if (xmlreader.NodeType == XmlNodeType.EndElement)
                {
                    if (xmlreader.Name.Equals("definition") && currentDef != null)
                    {
                        Add(currentDef);
                        currentDef = null;
                    }
                }
            } while (xmlreader.Read());
        }
    }
}
