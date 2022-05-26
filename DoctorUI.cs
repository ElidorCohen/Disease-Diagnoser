using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronXL;

namespace WindowsFormsApp1
{
    public partial class DoctorUI : Form
    {
        public ReadWriteExcel ReadWriteExcel;
        public DiagnosisForm diagnoseform;
        
        public DoctorUI()
        {
            InitializeComponent();
        }
  
        //Creating Excel File with all the information of the appointment, like the output example file format.
        private void addPatientBtn_Click(object sender, EventArgs e)
        {
            exportAppointmentInfo();
        }
        
        

        private void startDiagnosis_Clicked(object sender, EventArgs e)
        {
            if(areNotEmpty())
            {
                /*
                START DISGNOSTING
                */
                int temp = int.Parse(txtAge.Text);
                PatientType t = PatientType.Child;
                if (temp > 0 && temp <= 3)
                {
                    t = PatientType.Baby;
                }
                if (temp >= 4 && temp <= 17)
                {
                    t = PatientType.Child;
                }
                if(temp >= 18 && temp <= 59)
                {
                    t = PatientType.Adult;
                }
                

                PatientGender gender;
                if (comboBoxGender.Text == "Male")
                {
                    gender = PatientGender.Male;
                }
                else
                {
                    gender = PatientGender.Female;
                }
                BloodValues bv = new BloodValues(textBox17.Text, textBox16.Text, textBox15.Text, textBox14.Text, textBox13.Text, textBox12.Text, textBox4.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text);
                Patient patient = new Patient(txtID.Text, txtFirstname.Text, txtLastname.Text, temp, gender, txtFever.Text, txtBloodPress.Text, txtPulse.Text, t, bv);

                diagnoseform = new DiagnosisForm(patient);
                diagnoseform.Show();
            }
            else
            {
                MessageBox.Show("Cannot diagnose. Please fill all patient info field.");
            }
        }

        //Import file button
        private void button3_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox_filePath.Text = ofd.FileName;
            }
            ReadWriteExcel = new ReadWriteExcel(textBox_filePath.Text);
        }

        //Reading values from Excel-file, showing it in the textboxes.
        public void setTxtBoxValues()
        {
            textBox17.Text = ReadWriteExcel.getBloodValues("A", textBox_filePath.Text);
            textBox16.Text = ReadWriteExcel.getBloodValues("B", textBox_filePath.Text);
            textBox15.Text = ReadWriteExcel.getBloodValues("C", textBox_filePath.Text);
            textBox14.Text = ReadWriteExcel.getBloodValues("D", textBox_filePath.Text);
            textBox13.Text = ReadWriteExcel.getBloodValues("E", textBox_filePath.Text);
            textBox12.Text = ReadWriteExcel.getBloodValues("F", textBox_filePath.Text);
            textBox4.Text = ReadWriteExcel.getBloodValues("G", textBox_filePath.Text);
            textBox8.Text = ReadWriteExcel.getBloodValues("H", textBox_filePath.Text);
            textBox9.Text = ReadWriteExcel.getBloodValues("I", textBox_filePath.Text);
            textBox10.Text = ReadWriteExcel.getBloodValues("J", textBox_filePath.Text);
            textBox11.Text = ReadWriteExcel.getBloodValues("K", textBox_filePath.Text);
        }
        
        //Clears all textBoxes.
        public void clearAllBoxes()
        {
            textBox17.Text = "";
            textBox16.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox4.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
        }

        //Returns true if one of the text fields is empty.
        private bool isNotFilled()
        {
            if (textBox17.Text == "" || textBox16.Text == "" || textBox15.Text == "" || textBox14.Text == "" || textBox13.Text == "" || textBox12.Text == "" || textBox4.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "")
            {
                return true;
            }
            return false;
        }
        private bool areNotEmpty()
        {
            if(txtFirstname.Text == "" || txtLastname.Text == "" || txtID.Text == "" || txtAge.Text == "" || txtBloodPress.Text == "" || txtFever.Text == "" || txtPulse.Text == "" || comboBoxGender.Text == "")
            {
                return false;
            }
            return true;
        }

        //Checks if the file path text field is filled.
        private void filePathFilled()
        {
            if (textBox_filePath.Text == "")
            {
                MessageBox.Show("Choose a file path to import patient's blood values");
                textBox_filePath.Focus();
            }
        }

        //Load file button
        private void button2_Click(object sender, EventArgs e)
        {
            filePathFilled();
            setTxtBoxValues();

            if (isNotFilled())
            {
                MessageBox.Show("Apparently you've imported a wrong file, or one of the fields is empty.");
                clearAllBoxes();
            }
            else
            {
                setTxtBoxValues();
            }
        }

        public void createPatient()
        {
            int temp = int.Parse(txtAge.Text);
            PatientType t;
            if (temp > 0 && temp <= 4)
            {
                t = PatientType.Baby;
            }
            if (temp > 4 && temp <= 18)
            {
                t = PatientType.Child;
            }
            else
            {
                t = PatientType.Adult;
            }

            PatientGender gender;
            if (comboBoxGender.Text == "Male")
            {
                gender = PatientGender.Male;
            }
            else
            {
                gender = PatientGender.Female;
            }

            
            BloodValues bv = new BloodValues(textBox17.Text, textBox16.Text, textBox15.Text, textBox14.Text, textBox13.Text, textBox12.Text, textBox4.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text);
            Patient patient = new Patient(txtID.Text, txtFirstname.Text, txtLastname.Text, temp, gender, txtFever.Text, txtBloodPress.Text, txtPulse.Text, t, bv);
        }
        
        
        
        //Exporting all the information gathered in the meeting into a new Excel file.
        private void exportAppointmentInfo()
        {

            //THIRD STEP - CREATING EXCEL FILE WITH ALL THE INFORMATION OF OBJECTS
            WorkBook xlsWorkbook = WorkBook.Create(ExcelFileFormat.XLSX);
            xlsWorkbook.Metadata.Author = "IronXL";
            WorkSheet xlsSheet = xlsWorkbook.CreateWorkSheet("new_sheet");

            //FOURTH STEP - ADDING DATA TO THE FILE
            string newFilePath = AppDomain.CurrentDomain.BaseDirectory + txtID.Text + ".xlsx";
            xlsWorkbook.SaveAs(newFilePath);
            ReadWriteExcel = new ReadWriteExcel(newFilePath);
            ReadWriteExcel.addPatientToExcelFile(diagnoseform.Patient, diagnoseform.info);
            MessageBox.Show("Process Done!");

        }



        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }
        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void DoctorUI_Load(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
        private void treatmentRecommendation_Clicked(object sender, EventArgs e)
        {

        }
    }
}
