using NMMA.OAuth2;
using System;
using System.Configuration;
using System.Net;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.IntegrationServices;
using System.IO;
using System.Data;
using System.Linq;
using System.Threading;

namespace NMMA.Concur
{
    public class ConcurProcess
    {
        #region Private Variables
        
        private string concurOAuthtoken = "";
        private string concurOAuthURL = "";
        private string concurClientID = "";
        private string concurClientSecret = "";      
        private string concurUser = "";
        private string concurPassword = "";
        //private string concurRefreshToken = "";
        private string concurExtractURL = "";
        private string concurExtractJobID = "";
        private string concurExtractJobName = "";
        private string concurExtractFilePath = "";
        private string targetConnString = "";
        private string JobName = "";
        private string JobStep = "";
        private string ExtractFileString = "";
        private string LogPath = "";

        #endregion

        private ProcessJob job;
        private ManageExtracts extMgr;

        public ConcurProcess(string mode)
        {
            if (mode != "")
            {
                mode = mode + ".";
            }
            else
            {
                mode = "";
            }
            var appSettings = ConfigurationManager.AppSettings;                       
            concurOAuthURL = appSettings["ConcurOAuthServer"];
            concurClientID = appSettings["ConcurClientID"];                        
            concurClientSecret = appSettings["ConcurClientSecret"];
            concurUser = appSettings["ConcurUserID"];
            concurPassword = appSettings["ConcurPassword"];
            targetConnString = appSettings["SSISConnectionString"];

            concurExtractURL = appSettings[mode + "ConcurExtractURL"];
            concurExtractJobName = appSettings[mode + "ExtractJobName"];
            concurExtractFilePath = appSettings[mode + "ExtractFilePath"];
            JobName = appSettings[mode + "SSISJob"];
            JobStep = appSettings[mode + "SSISJobStep"];
            ExtractFileString = appSettings[mode + "ExtractFileString"];
            LogPath = appSettings[mode + "LogPath"];
        }
     
        public bool Authenticate()
        {
            Utilities.LogMessageToFile(LogPath,"Requesting Token / Authentication");
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                string METHOD = "POST";
                string CONTENT = "application/x-www-form-urlencoded"; 
                string SERVER = concurOAuthURL;
                string CLIENT_ID = "client_id=" + concurClientID;
                string CLIENT_SRT = "client_secret=" + concurClientSecret;                
                string GRANT = "grant_type=password"; 
                string CRED = "cred_type=password";
                string USER = "username=" + concurUser;
                string PASS = "password=" + concurPassword;
                //string REFRESH = "refresh_token=" + concurRefreshToken;
                //string GRANT = "grant_type=refresh_token";
                var o = new OAuth2Wrapper();
                var xml = o.Authenticate(SERVER, CLIENT_ID + "&" + CLIENT_SRT + "&" + GRANT + "&" + USER + "&" + PASS + "&" + CRED, METHOD, CONTENT).ToString();
                concurOAuthtoken = o.token;
                
                Utilities.LogMessageToFile(LogPath, "Authenticated");
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }          
        }

        public bool Extract()
        {
            try
            {               
                Utilities.LogMessageToFile(LogPath, "Requesting Job run.");
                string ExtractURL = concurExtractJobID + "/job";
                job = extMgr.PostExtractJobRequest(ExtractURL);
                job.LogPath = LogPath;
                if (job.Id != "")
                {
                    Utilities.LogMessageToFile(LogPath, "Job ID: " + job.Id);
                    Utilities.LogMessageToFile(LogPath, "Job Start: " + job.StartTime);
                    Utilities.LogMessageToFile(LogPath, "Job Status: " + job.Status);
                    string joblink = job.Id;
                    
                    int maxLoop = 30, startloop = 0;
                    TimeSpan interval = new TimeSpan(0, 0, 5);                                     
                    while (startloop < maxLoop && (job == null || job.Status != "Completed"))
                    {
                        startloop++;
                        job = extMgr.GetExtractJobStatus(joblink);
                        Thread.Sleep(interval);
                    }
                    
                    Utilities.LogMessageToFile(LogPath, "Job Status: " + job.Status);
                    Utilities.LogMessageToFile(LogPath, "Job End: " + job.StopTime);
                    Utilities.LogMessageToFile(LogPath, "Job File Path: " + job.FileLink);
                }
                job.FileFolder = concurExtractFilePath;
                job.ExtractFileString = ExtractFileString;
                job.LogPath = LogPath;
                extMgr.GetExtractJobFile(job);
                Utilities.LogMessageToFile(LogPath, "NMMA File: " + job.FileName);
                //string line1 = File.ReadLines(job.FileName).First();
                //string batchID = line1.Split(new[] { '|' }, 3).Skip(1).FirstOrDefault();
                //Utilities.LogMessageToFile("Batch ID: " + batchID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void CallSQLJob()
        {
            SqlConnection DbConn = new SqlConnection(targetConnString);
            try
            {

                SqlCommand ExecJob = new SqlCommand();
                ExecJob.CommandType = CommandType.StoredProcedure;
                ExecJob.CommandText = "msdb.dbo.sp_start_job";
                ExecJob.Parameters.AddWithValue("@job_name", JobName);
                ExecJob.Parameters.AddWithValue("@step_name", JobStep);
                ExecJob.Connection = DbConn; //assign the connection to the command.

                using (DbConn)
                {
                    DbConn.Open();
                    using (ExecJob)
                    {
                        ExecJob.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Utilities.LogMessageToFile(LogPath, ex.Message);
            }
            finally
            {
                if (DbConn != null)
                {
                    if (DbConn.State == ConnectionState.Open)
                    {
                        DbConn.Close();
                    }
                    DbConn.Dispose();
                }
            }

        }

        public void Process()
        {           
            try
            {
                Utilities.LogMessageToFile(LogPath, "Starting Concur Process.");
                if (Authenticate() && !string.IsNullOrEmpty(concurOAuthtoken))
                {
                    extMgr = new ManageExtracts(concurOAuthtoken);

                    if (GetJobID())
                    {
                        if (Extract())
                        {
                            Utilities.LogMessageToFile(LogPath, "Executing SSIS Package.");
                        }
                    }
                    else
                    {
                        Utilities.LogMessageToFile(LogPath, "Could not get Job ID. Please check configuration of Job Name.");
                    }
                }
                else
                {
                    Utilities.LogMessageToFile(LogPath, "Failed to Authenticate");
                }
            }
            catch (Exception ex)
            {
                Utilities.LogMessageToFile(LogPath, ex.Message);
                throw ex;
            }
            
        }

        public bool GetJobID()
        {
            Utilities.LogMessageToFile(LogPath, "Requesting Job definitions.");
            try
            {
                DefinitionList defList = extMgr.GetExtractDefinitionList(concurExtractURL);

                foreach (Definition def in defList)
                {
                    if (def.Name.Contains(concurExtractJobName))
                    {
                        Utilities.LogMessageToFile(LogPath, "Job ID found.");
                        concurExtractJobID = def.Id;                       
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utilities.LogMessageToFile(LogPath, ex.Message);
                throw ex;
            }           
        }

        public void GetJobIDExecutions()
        {
           try { 
                ManageExtracts extMgr = new ManageExtracts(concurOAuthtoken);
                DefinitionList defList = extMgr.GetExtractDefinitionList(concurExtractURL);

                foreach (Definition def in defList)
                {
                    if (def.Name.Contains(concurExtractJobName))
                    {
                        JobList jobLst = extMgr.GetExtractJobList(def);
                        foreach (ProcessJob job in jobLst)
                        {
                            if (job.Id != "" && job.Status == "Completed")
                            {
                                //Console.WriteLine("ID: " + job.Id);
                                //Console.WriteLine("Status: " + job.Status);
                                //Console.WriteLine("Status Link: " + job.StatusLink);
                                //Console.WriteLine("File Link: " + job.FileLink);
                                //Console.WriteLine("Start Time: " + job.StartTime);
                                //Console.WriteLine("************************************");
                            }
                        }
                        //Console.WriteLine();
                        //Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.LogMessageToFile(LogPath, ex.Message);
                throw ex;
            }

        }


    }
}
