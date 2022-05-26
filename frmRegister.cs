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
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class frmRegister : Form
    {
        public ReadWriteExcel ReadWriteExcel;
        
        public frmRegister()
        {
            ReadWriteExcel = new ReadWriteExcel(ReadWriteExcel.users_file_path);
            InitializeComponent();
        }


        /*Returns true if Username and Password are in valid range. Otherwise, shows a message and returns false.*/
        private bool UsernamePassRangeCheck()
        {
            if (txtUsername.Text.Length < 6 || txtUsername.Text.Length > 8)
            {
                MessageBox.Show("Username has to contain 6-8 characters.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtUsername.Focus();
                return false;
            }
            if (txtPassword.Text.Length < 8 || txtPassword.Text.Length > 10)
            {
                MessageBox.Show("Password has to contain 8-10 characters.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        //Returns true if Username contains maximum 2 digits. Otherwise false.
        private bool usernameDigitCheck(string username)
        {
            int digit = 0;

            foreach (char x in txtUsername.Text)
            {
                if (char.IsDigit(x))
                {
                    digit++;
                }
            }
            if (digit > 2)
            {
                MessageBox.Show("Username has to contain maximum 2 numbers.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtUsername.Focus();
                return false;
            }
            return true;
        }

        //Returns true if Password contains atleast 1 digit. Otherwise false.
        private bool passwordDigitCheck(string password)
        {
            foreach (char x in password)
            {
                if (char.IsDigit(x))
                {
                    return true;
                }
            }
            MessageBox.Show("Password has to contain atleast 1 number.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtPassword.Focus();
            return false;           
        }

        //Returns true if Password contains atleast 1 special character.
        private bool hasSpecialChars(string password)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (password.Contains(item)) return true;
            }
            MessageBox.Show("Password has to contain atleast 1 special character.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtPassword.Focus();
            return false;
        }

        //Returns true if the Username contains only letters and numbers.
        private bool usernameLetterCheck(string username)
        {
            foreach (char x in username)
            {
                if (!char.IsLetterOrDigit(x))              
                {

                    MessageBox.Show("Username has to contain only English letters in addition to maximum 2 digits.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Text = "";
                    txtUsername.Focus();
                    return false;
                }
            }
            return true;
        }

        //Returns true if the Password contains atleast 1 letter.
        private bool passLetterCheck(string password)
        {
            foreach(char x in password)
            {
                if (char.IsLetter(x))
                {
                    return true;
                }
            }
            return false;
        }
        
        //Returns true if the password has atleast 1 letter, 1 special char and 1 digit.
        private bool passwordFinalCheck(string password)
        {
            if(passwordDigitCheck(password) && hasSpecialChars(password) && passLetterCheck(password))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Password has to contain atleast 1 English letter, 1 special char and 1 digit.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
                return false;
            }
        }
        
        //Returns true if Username and Password fields are not empty. Otherwise false.
        private bool fieldsCheck()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtConPassword.Text == "")
            {
                MessageBox.Show("Username / Password fields are empty!", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtUsername.Text = "";
                txtConPassword.Text = "";
                txtUsername.Focus();
                return false;
            }
            return true;
        }

        //Returns true if Username doesn't exist in the Database, otherwise returns false.
        private bool doesntExist()
        {
            if (ReadWriteExcel.searchUsername(txtUsername.Text) != "Username wasn't found in the Database")
            {
                MessageBox.Show("Username already in use by another account!");
                txtPassword.Text = "";
                txtUsername.Text = "";
                txtConPassword.Text = "";
                txtUsername.Focus();
                return false;
            }
            return true;
        }

        //Returns true if the Password equals to the Confirmed Password. Otherwise false.
        private bool equalPasswords()
        {
            if (txtConPassword.Text == txtPassword.Text)
            {
                return true;
            }
            MessageBox.Show("Passwords does not match, please re-enter it properly", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtPassword.Focus();
            return false;
        }

        //This function creates the account with the data that was received in the TextBoxes.
        private void createAccount()
        {
            ReadWriteExcel.Counter = int.Parse(ReadWriteExcel.ws["D1"].Value.ToString()) + 1;
            ReadWriteExcel.writeExcel(txtUsername.Text, "A", ReadWriteExcel.Counter.ToString());
            ReadWriteExcel.writeExcel(txtPassword.Text, "B", ReadWriteExcel.Counter.ToString());
            ReadWriteExcel.writeExcel(ReadWriteExcel.Counter.ToString(), "D", "1");
            MessageBox.Show("Your account has been successfully created!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        //Registration Button => once all terms are true, the account will be created.
        private void registerButton_Click(object sender, EventArgs e)
        {
            if(fieldsCheck() && UsernamePassRangeCheck() && usernameDigitCheck(txtUsername.Text) && usernameLetterCheck(txtUsername.Text) && doesntExist() && equalPasswords() && passwordFinalCheck(txtPassword.Text))
            {
                createAccount();
            }
        }


        //This function will hides user's password when typed.
        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        //Clears all textbox fields.
        private void clearButton_Click_1(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUsername.Focus();
        }
        
        //LOGIN button - when clicked, the login page will appear and the registration page will be hided.
        private void label2_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();  
            this.Hide();

        }

  
        /*Empty funcs to be deleted later*/
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
