using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace asm2_ddb
{
    public partial class fAdmin : Form
    {
        string connectionString = @"Data Source=DESKTOP-4GBUN81\SQLEXPRESS;Initial Catalog=QLBH;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();

        public fAdmin()
        {
            InitializeComponent();
        }

        private void tbnlogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Log out ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {


                this.Hide();
                fLogin fLogin = new fLogin();
                fLogin.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nameproduct = txtnameproduct.Text;
            string quatity = txtquatity.Text;
            string price = txtprice.Text;
            string description = txtdescription.Text;

            if (checktextbox())
            {
                int productid = int.Parse(txtproductid.Text);
                string query = "Insert into Product values ('" + productid + "','" + nameproduct + "','" + quatity + "','" + price + "','" + description + "')";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("select * from Product ", con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtproductid_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnameproduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtquatity_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtdescription_TextChanged(object sender, EventArgs e)
        {

        }
        private void deletetextbox()
        {
            txtproductid.Text = "";
            txtnameproduct.Text = "";
            txtdescription.Text = "";
            txtprice.Text = "";
            txtquatity.Text = "";


        }
        private bool checktextbox()
        {
            if (txtproductid.Text == "") { MessageBox.Show("Plese enter ID"); return false; }
            if (txtnameproduct.Text =="") { MessageBox.Show("Plese enter nameproduct "); return false; }
            if(txtquatity.Text=="") { MessageBox.Show("Plese enter quantity "); return false; }
            if (string.IsNullOrWhiteSpace(txtquatity.Text) || Convert.ToInt32(txtquatity.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than zero.");
                return false;
            }

            if ( txtprice.Text =="") { MessageBox.Show("Plese enter price "); return false; }
            if (string.IsNullOrWhiteSpace(txtprice.Text) || Convert.ToInt32(txtprice.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than zero.");
                return false;
            }
            if (txtdescription.Text=="") { MessageBox.Show("Plese enter Description "); return false; }
            return true;
        }
        private void txtproductid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            int productid = int.Parse(txtproductid.Text);
            string nameproduct = txtnameproduct.Text;
            string quantity = txtquatity.Text;
            string price = txtprice.Text;
            string description = txtdescription.Text;

            if (checktextbox())
            {
                try
                {
                    con = new SqlConnection(connectionString);
                    con.Open();

                    string query = "UPDATE Product SET Productname = @Productname, Quantity = @Quantity, Price = @Price, description = @Description WHERE ProductID = @ProductID";
                    cmd = new SqlCommand(query, con);

                   
                    cmd.Parameters.AddWithValue("@Productname", nameproduct);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@ProductID", productid);

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtproductid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtnameproduct.Text= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtquatity.Text= dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtprice.Text= dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtdescription.Text= dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            
            int productid = int.Parse(txtproductid.Text);

            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                string query = "DELETE FROM Product WHERE ProductID = @ProductID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID", productid);

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
                    MessageBox.Show("Product with ID " + productid + " not found.");
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string productName = txtsearch.Text;
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SEARCH_BY_Name", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@productname", productName);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnload_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            deletetextbox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FManagementEmployee fManagementEmployee= new FManagementEmployee();
            fManagementEmployee.ShowDialog();
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckInvoice checkInvoice = new CheckInvoice();
            checkInvoice.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagementCustomer managementCustomer = new ManagementCustomer();
            managementCustomer.ShowDialog();
            this.Close();
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtquatity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
