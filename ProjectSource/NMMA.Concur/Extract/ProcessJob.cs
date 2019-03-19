using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Net;
using System;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace NMMA.Concur
{
    public class ProcessJob
    {
        #region Private Variables
            private string privateId = "";
            private string privateStatusLink = "";
            private string privateStartTime = "";
            private string privateStopTime = "";
            private string privateStatus = "";
            private string privateFileLink = "";
            private string privateFileFolder = "";
            private string privateFileName = "";
        #endregion

        public static RequestWrapper HttpWrapper = new RequestWrapper();

        #region Public Properties
        public string Id
            {
                get { return privateId; }
                set { privateId = value; }
            }

        public string StatusLink
        {
            get { return privateStatusLink; }
            set { privateStatusLink = value; }
        }

        public string StartTime
        {
            get { return privateStartTime; }
            set { privateStartTime = value; }
        }

        public string StopTime
        {
            get { return privateStopTime; }
            set { privateStopTime = value; }
        }

        public string Status
        {
            get { return privateStatus; }
            set { privateStatus = value; }
        }

        public string FileLink
        {
            get { return privateFileLink; }
            set { privateFileLink = value; }
        }

        public string FileFolder
        {
            get { return privateFileFolder; }
            set { privateFileFolder = value; }

        }
                
        public string FileName
        {
            get { return privateFileName; }
            set { privateFileName = value; }
        }

        private string logPath;

        public string LogPath
        {
            get { return logPath; }
            set { logPath = value; }
        }

        private string concurFileString;

        public string ExtractFileString
        {
            get { return concurFileString; }
            set { concurFileString = value; }
        }


        #endregion

        #region Public Functions
        public JobList getJobs(string uri, string token, string httpMethod = "GET")
        {
            try
            {
                Utilities util = new Utilities();
                HttpWebResponse response = HttpWrapper.MakeHttpTokenRequest(uri, httpMethod, "", token);
                string xml = util.getStringFromStream(response.GetResponseStream());
                JobList jobs = new JobList();
                if (xml == "")
                {
                    return null;
                }
                XmlReader xmlReader = XmlReader.Create(new StringReader(xml));
                jobs.parseJobs(xmlReader);
                return jobs;
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        public ProcessJob getIndividualJob(string uri, string token)
        {
            JobList jobs = getJobs(uri, token);
            if (jobs == null)
            {
                return null;
            }
            else if (jobs.Count == 0)
            {
                return null;
            }
            else
            {
                return jobs[0];
            }
        }

        public ProcessJob createJob(string uri, string data, string token)
        {
            // Send the request
            JobList jobs = getJobs(uri, token, "POST");
            if (jobs == null)
            {
                return null;
            }
            else if (jobs.Count == 0)
            {
                return null;
            }
            else
            {
                return jobs[0];
            }
            
        }

        public void getFile(string token)
        {
            try
            { 
                if (FileLink == null || FileLink.Length == 0)
                {
                    return;
                }

                FileName = "";
                DateTime dateTime = DateTime.UtcNow;            
                string folder = dateTime.Date.ToString("yyyyMMdd");
                string fn = dateTime.ToString("yyyyMMddHHmmss");
                string FileOut = FileFolder + folder + @"\";
                string filetype = ".txt";                            
                Directory.CreateDirectory(FileOut);

                Utilities.LogMessageToFile(LogPath, "Downloading File to: " + FileOut);
                HttpWebResponse response = HttpWrapper.MakeHttpTokenRequest(FileLink, "GET", "", token);
                Stream resp = response.GetResponseStream();
                if (response.ContentType.Equals("application/zip"))
                {
                    filetype = ".zip";
                    FileName = FileOut + fn + filetype;
                    //create file from stream                              
                    using (Stream s = File.Create(FileName))
                    {
                        resp.CopyTo(s);
                    }

                    ZipExtract(FileOut);

                }
                else if (response.ContentType.Equals("text/csv"))
                {
                    filetype = ".txt";
                    FileName = FileOut + fn + filetype;
                    //create file from stream                              
                    using (Stream s = File.Create(FileName))
                    {
                        resp.CopyTo(s);
                    }
                    TxtExtract();
                }
               
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void ZipExtract(string FileOut)
        {
            Utilities.LogMessageToFile(LogPath, "Extracting Zip: " + FileName);
            using (ZipArchive archive = ZipFile.OpenRead(FileName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.StartsWith(ExtractFileString, StringComparison.OrdinalIgnoreCase))
                    {
                        FileName = Path.Combine(FileOut, entry.FullName).ToString();
                        entry.ExtractToFile(FileName, true);
                        FileName = Path.Combine(FileFolder, ExtractFileString + ".txt").ToString();
                        entry.ExtractToFile(FileName, true);
                    }
                }
            }
        }

        private void TxtExtract()
        {            
            string filePath = Path.Combine(FileFolder, ExtractFileString + ".txt").ToString();
            Utilities.LogMessageToFile(LogPath, "Copying text file: " + filePath);
            File.Copy(FileName, filePath, true);
            FileName = filePath;
        }
        #endregion 

    }
}
