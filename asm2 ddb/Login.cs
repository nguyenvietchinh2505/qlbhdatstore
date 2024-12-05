using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace asm2_ddb
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        } 

        private void btnexit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you exit the app ?", "Notification", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
           
        }

        private void btgcreate_Click(object sender, EventArgs e)
        {
            fCreateAC f = new fCreateAC();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        Modify modify=new Modify();
       

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string uname = "admin";
            string pword = "admin";
            string username=txtusername.Text;
            string password=txtpassword.Text;
            if(username.Trim()=="") { MessageBox.Show("Please enter username !"); }
            else if (password.Trim()=="") { MessageBox.Show("Please enter password !"); }
            else
            {
                string query = "Select *from account where Username_ ='" + username + "' and password_='" + password + "'";
               
                if (modify.Acount(query).Count !=0)
                {
                    MessageBox.Show("Login Successful!","Notification",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    if(txtusername.Text==uname && txtpassword.Text == pword)
                    {
                        this.Hide();
                        fAdmin fAdmin = new fAdmin();
                        fAdmin.ShowDialog();
                        this.Close();
                            
                    }
                    else
                    {
                        this.Hide();
                        fUser fUser = new fUser();
                        fUser.ShowDialog();
                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Login Failed!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}

