using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NMMA.Concur
{
    public class JobList: List<ProcessJob>
    {
        public void parseJobs(XmlReader xmlreader)
        {
            ProcessJob currentJob = null;
            do
            {
                if (xmlreader.NodeType == XmlNodeType.Element)
                {
                    if (xmlreader.Name.Equals("job") && xmlreader.IsStartElement())
                    {
                        currentJob = new ProcessJob();                        
                    }
                    else if (xmlreader.Name.Equals("id"))
                    {
                        xmlreader.Read();
                        currentJob.Id = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("status-link"))
                    {
                        xmlreader.Read();
                        currentJob.StatusLink = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("start-time"))
                    {
                        xmlreader.Read();
                        currentJob.StartTime = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("stop-time"))
                    {
                        xmlreader.Read();
                        currentJob.StopTime = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("status"))
                    {
                        xmlreader.Read();
                        currentJob.Status = xmlreader.Value;
                    }
                    else if (xmlreader.Name.Equals("file-link"))
                    {
                        xmlreader.Read();
                        currentJob.FileLink = xmlreader.Value;
                    }
                }
                else if (xmlreader.NodeType == XmlNodeType.EndElement)
                {
                    if (xmlreader.Name.Equals("job") && currentJob != null)
                    {
                        Add(currentJob);
                        currentJob = null;
                    }
                }
            } while (xmlreader.Read());
        }
    }
}
