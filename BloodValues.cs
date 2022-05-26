using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class BloodValues
    {
        public string WBC;
        public string Neut;
        public string Lymph;
        public string RBC;
        public string HCT;
        public string Urea;
        public string Hb;
        public string Crtn;
        public string Iron;
        public string HDL;
        public string AP;


        public BloodValues(string WBC, string Neut, string Lymph, string RBC, string HCT, string Urea, string Hb, string Crtn, string Iron, string HDL, string AP)
        {   
            this.WBC = WBC;
            this.Neut = Neut;
            this.HCT = HCT;
            this.Urea = Urea;
            this.HDL = HDL;
            this.AP = AP;
            this.Lymph = Lymph;
            this.RBC = RBC;
            this.Hb = Hb;
            this.Crtn = Crtn;
            this.Iron = Iron;

        }
    }
}
