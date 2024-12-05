using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace asm2_ddb
{
    public partial class fUser : Form
    {
         string connectionString = @"Data Source=LAPTOP-C6H2FCNV\SQLEXPRESS;Initial Catalog=QLBH12;Integrated Security=True";
        SqlConnection con;
            SqlCommand cmd;
            SqlDataAdapter adt;
            DataTable dt =new DataTable();
        
        public fUser()
        {
            InitializeComponent();
        }

        private void fUser_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();

            string query = "SELECT * FROM Product";
            cmd = new SqlCommand(query, con);
            adt = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void tbnlogout_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you Log out ?","Notification",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Hide();
                fLogin fLogin = new fLogin();
                fLogin.ShowDialog();
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                string query = "SELECT * FROM Product";
                cmd = new SqlCommand(query, con);
                adt = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            this.Hide();
            fBuy fBuy = new fBuy();
            fBuy.ShowDialog();
            this.Close();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string productName = txtname.Text;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SEARCH_BY_Name", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@productname", productName);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource=dataTable;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }
    }
}
