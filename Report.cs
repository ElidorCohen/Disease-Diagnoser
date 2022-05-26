using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Report
    {
        public Treatment[] Treatments;
        public string extraInformation;

        public Report(Treatment[] treatments, string extraInfo = null)
        {
            Treatments = treatments;
            extraInformation = extraInfo;
        }
        public Report(string extraInfo)
        {
            extraInformation = extraInfo;
        }
    }
    
}
