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
    public partial class ManagementCustomer : Form
    {
        string connectionString = @"Data Source=LAPTOP-C6H2FCNV\SQLEXPRESS;Initial Catalog=QLBH12;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public ManagementCustomer()
        {
            InitializeComponent();
        }

        private void ManagementCustomer_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("select * from Customer ", con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void deletetextbox()
        {
            txtaddress.Text = "";
            
            txtnamecustomer.Text = "";
            txtphone.Text = "";
            txtcustomerid.Text = "";
           
        }
        private bool checktextbox()
        {


            if (txtnamecustomer.Text == "") { MessageBox.Show("Please enter name customer "); return false; }        
            if (txtphone.Text == "") { MessageBox.Show("Please enter phone "); return false; }
            if (txtaddress.Text == "") { MessageBox.Show("Please enter address"); return false; }



            return true;
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtcustomerid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtnamecustomer.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtphone.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtaddress.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void RefreshDataGridView()
        {
            try
            {
                dt.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    cmd = new SqlCommand("SELECT * FROM Customer", connection);
                    adt = new SqlDataAdapter(cmd);
                    adt.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btncreate_Click(object sender, EventArgs e)
        {

            if (checktextbox())
            {
                string name = txtnamecustomer.Text;
                string phone = txtphone.Text;
                string address = txtaddress.Text;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO Customer (Name, Phone, Address) VALUES (@Name, @Phone, @Address)", connection);

                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Address", address);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer created successfully!");
                            deletetextbox();
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create customer.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (checktextbox())
            {
                string name = txtnamecustomer.Text;
                string phone = txtphone.Text;
                string address = txtaddress.Text;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("UPDATE Customer SET Name = @Name, Phone = @Phone, Address = @Address WHERE CustomerID = @CustomerID", connection);

                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@CustomerID", txtcustomerid.Text); 
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer updated successfully!");
                            RefreshDataGridView();
                            deletetextbox();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update customer.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcustomerid.Text))
            {
                int customerID = int.Parse(txtcustomerid.Text);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE CustomerID = @CustomerID", connection);
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer deleted successfully!");
                            RefreshDataGridView();
                            deletetextbox();
                        }
                        else
                        {
                            MessageBox.Show("No customer found with the given Customer ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deletetextbox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            fAdmin fAdmin= new fAdmin();
            fAdmin.ShowDialog();
            this.Close();
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            
        }
    }
    
}
