using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using IronXL;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class ReadWriteExcel
    {
        //Class fields:

        public static string users_file_path = AppDomain.CurrentDomain.BaseDirectory +"USERS.xlsx";
        public static string usersPath = "C:\\Users\\elido\\Desktop\\ProjectExcelDBFiles";

        private string _filePath = AppDomain.CurrentDomain.BaseDirectory; 
        public static int Counter = 2;
        public WorkBook wb;
        public WorkSheet ws;


        
        //Constructor
        public ReadWriteExcel(string filePath)
        {
            _filePath = filePath;
            wb = WorkBook.Load(_filePath);
            ws = wb.DefaultWorkSheet;
        }

        /*
         Function that writes into the Excel file, at a given content, row and column.
         */
        public void writeExcel(string data, string column, string row)
        {
            Range rangeCell = ws[column + row];
            ws[column + row].Value = data;
            wb.SaveAs(_filePath);

        }

        /*
         Function that searches the username given as an argument.
         Returns the index of the cell. Example: "A4".
         */
        public string searchUsername(string username)
        {
            for (int i = 1; i <= ws.Rows.Count(); i++)
            {
                if (username == ws["A" + i].Value.ToString())
                {
                    return "A" + i;
                }
            }
            return "Username wasn't found in the Database";
        }

        /*
         Function that searches the password given as an argument.
         Returns the index of the cell. Example: "B4".
         */
        public string searchPassword(string password)
        {
            for (int i = 1; i <= ws.Rows.Count(); i++)
            {
                if (password == ws["B" + i].Value.ToString())
                {
                    return "B" + i;
                }
            }
            return "Password wasn't found in the Database";
        }

        /*
         Function that searches a blood value in a file which contains blood values format.
         */
        public string getBloodValues(string column, string importPath)
        {
            
            WorkBook bloodWorkBook = WorkBook.Load(importPath);
            WorkSheet bloodSheet = bloodWorkBook.DefaultWorkSheet;

            string returnedData = bloodSheet[column + "2"].Value.ToString();
            if (int.TryParse(returnedData, out int value))
            {
                return returnedData;
            }
            if(double.TryParse(returnedData, out double value2))
            {
                return returnedData;
            }
            return null;
            
        }
        
        public void addPatientToExcelFile(Patient patient, DiagnosisInfo diagnose)
        {

            
            writeExcel("First name: ", "A", "1");
            writeExcel("Last name: ", "B", "1");
            writeExcel("ID: ", "C", "1");
            writeExcel("Age: ", "D", "1");
            writeExcel("Gender: ", "E", "1");
            writeExcel("Blood Pressure: ", "F", "1");
            writeExcel("Fever: ", "G", "1");
            writeExcel("Pulse: ", "H", "1");
            writeExcel("Patient's Type: ", "I", "1");
            writeExcel("WBC: ", "J", "1");
            writeExcel("Neut: ", "K", "1");
            writeExcel("Lymph: ", "L", "1");
            writeExcel("RBC: ", "M", "1");
            writeExcel("HCT: ", "N", "1");
            writeExcel("Urea: ", "O", "1");
            writeExcel("Hb: ", "P", "1");
            writeExcel("Crtn: ", "Q", "1");
            writeExcel("Iron: ", "R", "1");
            writeExcel("HDL: ", "S", "1");
            writeExcel("AP: ", "T", "1");
            writeExcel("Diagnosis: ", "U", "1");
            writeExcel("Recommendations: ", "V", "1");
            writeExcel("Extra Information: ", "X", "1");


            writeExcel(patient.getFirstName().ToString(),"A","2");
            writeExcel(patient.getLastName(),"B","2");
            writeExcel(patient.getID(),"C","2");
            writeExcel(patient.getAge().ToString(),"D","2");
            writeExcel(patient.getGender().ToString(),"E","2");
            writeExcel(patient.getBloodPress(),"F","2");
            writeExcel(patient.getFever(),"G","2");
            writeExcel(patient.getPulse(),"H","2");
            writeExcel(patient.GetType().ToString(),"I","2"); //unnecessary for now.
            writeExcel(patient.BloodValues.WBC, "J", "2");
            writeExcel(patient.BloodValues.Neut+"%", "K", "2");
            writeExcel(patient.BloodValues.Lymph+"%", "L", "2");
            writeExcel(patient.BloodValues.RBC, "M", "2");
            writeExcel(patient.BloodValues.HCT+"%", "N", "2");
            writeExcel(patient.BloodValues.Urea, "O", "2");
            writeExcel(patient.BloodValues.Hb, "P", "2");
            writeExcel(patient.BloodValues.Crtn, "Q", "2");
            writeExcel(patient.BloodValues.Iron, "R", "2");
            writeExcel(patient.BloodValues.HDL, "S", "2");
            writeExcel(patient.BloodValues.AP, "T", "2");

            for (int i = 0; i < diagnose.TotalTreatments.Count; i++)
            {
                writeExcel(diagnose.TotalTreatments[i].Name, "U", (i + 2).ToString());
                writeExcel(diagnose.TotalTreatments[i].Details, "V", (i + 2).ToString());
            }
            
            for (int i = 0; i < diagnose.TotalExtraInfo.Count; i++)
            {
                writeExcel(diagnose.TotalExtraInfo[i], "X", (i+ 2).ToString());
            }

        }
    }
}
