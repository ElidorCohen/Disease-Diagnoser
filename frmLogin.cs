using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
        public ReadWriteExcel ReadWriteExcel;
        public int wrongPassCounter = 4;

        public frmLogin()
        {
            ReadWriteExcel = new ReadWriteExcel(ReadWriteExcel.users_file_path);
            InitializeComponent();
        }

        //Checks that Username and Password fields are filled.
        private bool fieldsCheck()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtUsername.Text = "";
                txtUsername.Focus();
                return false;
            }
            return true;
        }
        //Searches the Username and Password in the Database. Once data was found, the account will be successfully logged. Otherwise, it won't.
        private bool searchData()
        {
            ReadWriteExcel.Counter = int.Parse(ReadWriteExcel.ws["D1"].Value.ToString());
            string UsernameResult = ReadWriteExcel.searchUsername(txtUsername.Text);
            string PassResult = ReadWriteExcel.searchPassword(txtPassword.Text);
            string u = UsernameResult.Remove(0, 1);
            string p = PassResult.Remove(0, 1);

            if (u == p)
            {
                DoctorUI docUIform = new DoctorUI();
                docUIform.Show();
                this.Hide();
                return true;
            }
            return false;
        }
        /*
         This function will be called only if login failed.
         Shows the user how many tries he got left, if all tries are failed, the system
         will be automatically blocked for 10 seconds.
         */
        private void loginFailed()
        {
            MessageBox.Show("Either username or password was wrong! " + wrongPassCounter + " Tried left before temporary system block.");
            wrongPassCounter--;
            txtPassword.Text = "";
            txtUsername.Text = "";
            txtUsername.Focus();

            if (wrongPassCounter == -1)
            {
                SystemBlock systemBlock = new SystemBlock();
                systemBlock.ShowDialog();
                Thread.Sleep(10000);
                systemBlock.Hide();
                MessageBox.Show("System block has been RELEASED for you.\nYou may now try again.");
                wrongPassCounter = 4;
            }
        }

        //Login button
        private void loginButton_Click(object sender, EventArgs e)
        {
            fieldsCheck();
            
            if(txtUsername.Text != "" && txtPassword.Text != "")
            {
                if (searchData())
                {
                    MessageBox.Show("Welcome back "+txtUsername.Text+", enjoy using the system!");
                }
                else 
                {
                    loginFailed();
                }       
            }  
        }

        //This function will hides user's password when typed
        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        //BACK TO REGISTER button.
        private void label2_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        //Clears all textbox fields.
        private void cleanButton_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUsername.Text = "";
        }
        
        //Exit and stop the application.
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }


        //Empty function to be deleted later.
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
