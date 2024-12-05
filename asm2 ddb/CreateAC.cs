using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace asm2_ddb
{
    public partial class fCreateAC : Form
    {
        public fCreateAC()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public bool checkAccount(string account)//check mat khau va ten tai khoan
        {
            return Regex.IsMatch(account, "^[a-zA-Z0-9]{5,25}$");
        }
        public bool checkemail(string email)
        {   

            //check email bat buoc phai co @ va .
            return Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        Modify modify = new Modify();
        private void btncreate_Click(object sender, EventArgs e)
        {
               string Username=txtusername.Text;
            string password=txtpassword.Text;
            string enterpassword=txtenterpassword.Text;
            string email = txtemail.Text;
            if(!checkAccount(Username)) { MessageBox.Show("Please enter username about 6 to 25 words with alphanumeric characters, uppercase or lowercase letters", "Notification"); return; }
            if (!checkAccount(password)) { MessageBox.Show("Please enter password about 6 to 25 words with alphanumeric characters, uppercase or lowercase letters", "Notification"); return; }
                
            if (enterpassword!= password) { MessageBox.Show("Please confirm the correct password", "Notification"); return; }
            if (!checkemail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Notification");
                return;
            }
            if (modify.Acount("Select *from account where email='"+email+"'").Count !=0) {
                MessageBox.Show("This email has been used", "notification"); return;
            }
            try
            {
                string query = " insert into account   values ('" + Username + "','" + password + "','" + email + "')";
                modify.Command(query);
                if(MessageBox.Show("Create successful! Are you login ?","Notification",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("This account is already in use. Please create another account");

            }
               
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fCreateAC_Load(object sender, EventArgs e)
        {

        }
    }
}
