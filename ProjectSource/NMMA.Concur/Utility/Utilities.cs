using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace NMMA.Concur
{
    public class Utilities
    {
        public string getStringFromStreamtoString(Stream stream)
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Auto;
            StringBuilder strBuilder = new StringBuilder(10000);
            XmlWriter writer = XmlWriter.Create(strBuilder, writerSettings);
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
            doc = XDocument.Load(stream);
            doc.WriteTo(writer);

            return doc.ToString();
        }

        public string getStringFromStream(Stream stream)
        {

            string retString = "";

            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                retString = reader.ReadToEnd();
            }
            return retString;
        }

        public XmlDocument getxmlFromStream(Stream stream)
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            writerSettings.ConformanceLevel = ConformanceLevel.Auto;
            StringBuilder strBuilder = new StringBuilder(10000);
            XmlWriter writer = XmlWriter.Create(strBuilder, writerSettings);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            doc.WriteTo(writer);

            return doc;
        }

        public static void LogMessageToFile(string path,string msg)
        {
            Thread.Sleep(1000);
            DateTime dateTime = DateTime.UtcNow;           
            string fn = dateTime.ToString("yyyyMMdd") + ".txt";
            Directory.CreateDirectory(path);

            StreamWriter sw = File.AppendText(path + fn);
            string logLine = System.String.Format("{0:G}: {1}.", System.DateTime.Now, msg);
            sw.WriteLine(logLine);
            sw.Close();
        }
    }
}
