using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMMA.Concur
{
    public class ReturnType
    {
        private string typeStr_Renamed = "";

        public string typeStr
        {
            get { return typeStr_Renamed = ""; }
            set { typeStr_Renamed = value; }
        }

        private string strValue_Renamed = "";

        public string strValue
        {
            get { return strValue_Renamed; }
            set { strValue_Renamed = value; }
        }


    }
}
