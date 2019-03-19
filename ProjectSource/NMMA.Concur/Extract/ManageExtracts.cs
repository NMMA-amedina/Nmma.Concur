using System;

namespace NMMA.Concur
{
    public class ManageExtracts
    {
        private string ConcurToken;
        public string token
        {
            get { return ConcurToken; }
            set { ConcurToken = value; }
        }

        public ManageExtracts(string tkn)
        {
            token = tkn;
        }

        public DefinitionList GetExtractDefinitionList(string Endpoint)
        {
            try
            {
                Definition def = new Definition();
                DefinitionList defList = def.getDefinitions(Endpoint, token);
                if (defList == null)
                {
                    return null;
                }
                return defList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JobList GetExtractJobList(Definition def)
        {
            try
            {
                ProcessJob processJob = new ProcessJob();
                JobList job = processJob.getJobs(def.JobLink.ToString(), token);

                return job;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcessJob PostExtractJobRequest(string Endpoint)
        {
            try
            {
                ProcessJob processJob = new ProcessJob();
                ProcessJob job = processJob.createJob(Endpoint, "", token);
                return job;
            }            
            catch (Exception e)
            {
                throw e;
            }
        }

        public ProcessJob GetExtractJobStatus(string Endpoint)
        {
            try
            {
                ProcessJob processJob = new ProcessJob();
                processJob = processJob.getIndividualJob(Endpoint, token);
                return processJob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetExtractJobFile(ProcessJob processJob)
        {
            try
            {                
                processJob.getFile(token);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
