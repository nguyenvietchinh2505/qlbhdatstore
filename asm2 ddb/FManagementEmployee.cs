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

namespace asm2_ddb
{
    public partial class FManagementEmployee : Form
    {
        string connectionString = @"Data Source=LAPTOP-C6H2FCNV\SQLEXPRESS;Initial Catalog=QLBH12;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public FManagementEmployee()
        {
            InitializeComponent();
        }

        private void FManagementEmployee_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("select * from Employee ", con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }
        private void deletetextbox()
        {
            txtemployeeid.Text = "";
            txtname.Text = "";
            txtsex .Text= "";
            dateTimePicker1.Text = "";
            txtemail.Text = "";
            txtaddress.Text = "";


        }
        private bool checktextbox()
        {
            if (txtemployeeid.Text == "") { MessageBox.Show("Plese enter ID"); return false; }
            if (txtname.Text == "") { MessageBox.Show("Plese enter name "); return false; }
            if (txtsex.Text == "") { MessageBox.Show("Plese enter sex "); return false; }
            if (dateTimePicker1.Text == "") { MessageBox.Show("Plese enter birthday "); return false; }
            if (txtaddress.Text == "") { MessageBox.Show("Plese enter address "); return false; }
            if (txtemail.Text == "") { MessageBox.Show("Plese enter email "); return false; }
            return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           txtemployeeid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtsex.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtaddress.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtemail.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btncreate_Click(object sender, EventArgs e)
        {
            string employeeid = txtemployeeid.Text;
            string name = txtname.Text;
            string sex=txtsex.Text;
            string birthday=dateTimePicker1.Text;
            string email =txtemail.Text;
            string address = txtaddress.Text;
          
           

            if (checktextbox())
            {
               
                string query = "Insert into Employee values ('" + employeeid + "','" + name + "','" + sex + "','" + birthday + "','" + address + "','"+email+"')";

                try
                {
                    con = new SqlConnection(connectionString);
                    con.Open();

                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery(); 
                    if (MessageBox.Show("Do you want to Create it  ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        MessageBox.Show("Create Successful");
                    deletetextbox();
                    dt.Clear();
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
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            string employeeid = txtemployeeid.Text;
            string name = txtname.Text;
            string sex = txtsex.Text;
            string birthday = dateTimePicker1.Text;
            string email = txtemail.Text;
            string address = txtaddress.Text;

            if (checktextbox())
            {
                try
                {
                    con = new SqlConnection(connectionString);
                    con.Open();

                    string query = "UPDATE Employee SET Name = @Name, Sex = @Sex, Birthday = @Birthday, Address = @Address,Email=@Email  WHERE EmpId = @EmployeeID";
                    cmd = new SqlCommand(query, con);

                 
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Sex", sex);
                    cmd.Parameters.AddWithValue("@Birthday", birthday);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeid);

                    cmd.ExecuteNonQuery();
                    if (MessageBox.Show("Do you want to Edit it  ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        MessageBox.Show("Update Successful");
                    deletetextbox();

                
                    dt.Clear();
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
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            string employeeid = txtemployeeid.Text;

            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                string query = "DELETE FROM Employee WHERE EmpId = @EmployeeID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeid);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    if (MessageBox.Show("Do you want to Delete it  ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        MessageBox.Show("Delete Successful");
                    deletetextbox();
                    dt.Clear();
                    adt.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Employee with ID " + employeeid + " not found.");
                }
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            fAdmin fAdmin=new fAdmin();
            fAdmin.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deletetextbox();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
