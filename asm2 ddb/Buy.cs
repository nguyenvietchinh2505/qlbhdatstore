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
    public partial class fBuy : Form
    {
        string connectionString = @"Data Source=LAPTOP-C6H2FCNV\SQLEXPRESS;Initial Catalog=QLBH12;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        

        public fBuy()
        {
            InitializeComponent();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            fUser fUser = new fUser();
            fUser.ShowDialog();

        }

        private void fBuy_Load(object sender, EventArgs e)
        {

            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("select * from Product ", con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
          


        }
        private void ReloadFormData()
        {
            // Clear form fields
            txtnameproduct.Text = "";
            txtprice.Text = "";
            txtdescription.Text = "";
            txtproductid.Text = "";
            txtquantity.Text = "";
            txtnamecustumer.Text = "";
            txtphone.Text = "";
            txtaddress.Text = "";
            dateTimePicker1.Value = DateTime.Now;

            // Refresh product information
            dt.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from Product ", connection))
                {
                    adt = new SqlDataAdapter(command);
                    adt.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                connection.Close();
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
            txtnameproduct.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
         
            txtprice.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtdescription.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtproductid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtquantity.Text = "";


        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtquantity.Text))
            {
                if (int.TryParse(txtquantity.Text, out int newQuantity))
                {
                    decimal.TryParse(txtprice.Text, out decimal price);

                    decimal newPrice = price * newQuantity;
                    txtprice.Text = newPrice.ToString();
                }
                else
                {
                    MessageBox.Show("please enter number.");
                }
            }
            
        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {

        }
        private int GetQuantityInStock(int productId)
        {
            int quantityInStock = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT Quantity FROM Product WHERE ProductID = @ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);

                        connection.Open();
                        quantityInStock = Convert.ToInt32(command.ExecuteScalar());
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return quantityInStock;
        }
        private void UpdateStockQuantity(int productId, int purchasedQuantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UPDATE Product SET Quantity = Quantity - @PurchasedQuantity WHERE ProductID = @ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@PurchasedQuantity", purchasedQuantity);
                        command.Parameters.AddWithValue("@ProductID", productId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void btnbuy_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các ô textbox
            string customerName = txtnamecustumer.Text;
            string phoneNumber = txtphone.Text;
            string address = txtaddress.Text;
            string description = txtdescription.Text;
            string nameproduct = txtnameproduct.Text;
            int.TryParse(txtproductid.Text, out int productId);

            if (string.IsNullOrEmpty(txtnameproduct.Text))
            {
                MessageBox.Show("Please choose product.");
                return;
            }
            if (string.IsNullOrEmpty(txtnamecustumer.Text))
            {
                MessageBox.Show("Please enter name.");
                return;
            }
            if (string.IsNullOrEmpty(txtphone.Text))
            {
                MessageBox.Show("Please enter phone.");
                return;
            }
            if (!int.TryParse(txtphone.Text, out _))
            {
                MessageBox.Show("Please enter a number.");
                return;
            }
            if (string.IsNullOrEmpty(txtaddress.Text))
            {
                MessageBox.Show("Please enter address !");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtquantity.Text) || Convert.ToInt32(txtquantity.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than zero.");
                return;
            }

            int.TryParse(txtquantity.Text, out int quantity);
            int quantityInStock = GetQuantityInStock(productId);

           
            if (quantity > quantityInStock)
            {
                MessageBox.Show("The quantity purchased exceeds the quantity available in stock!");
                return;
            }

            decimal.TryParse(txtprice.Text, out decimal price);
           

            DateTime orderDate = dateTimePicker1.Value; 

       
       
           

        
            string insertCustomerQuery = @"
        INSERT INTO Customer (Name, Phone, Address) VALUES (@Name, @Phone, @Address);
        SELECT SCOPE_IDENTITY();";

            int customerID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertCustomerQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", customerName);
                    command.Parameters.AddWithValue("@Phone", phoneNumber);
                    command.Parameters.AddWithValue("@Address", address);

                    connection.Open();
                    customerID = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            string insertInvoiceQuery = @"
        INSERT INTO Invoice (ProductID, CustomerID, NameProduct, NameCustomer, PhoneNumber, Quantity, Address, Price, OrderDate, Description)
        VALUES (@ProductID, @CustomerID, @NameProduct, @NameCustomer, @PhoneNumber, @Quantity, @Address, @Price, @OrderDate, @Description);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertInvoiceQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductID",productId); 
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    command.Parameters.AddWithValue("@NameProduct", nameproduct);
                    command.Parameters.AddWithValue("@NameCustomer", customerName);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);
                    command.Parameters.AddWithValue("@Description", description);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            if (MessageBox.Show("Buy Successful ! , do you want to see the invoice? ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                InvoiceForm invoiceForm = new InvoiceForm(txtnamecustumer.Text, txtquantity.Text, txtaddress.Text, dateTimePicker1.Value, txtprice.Text, txtdescription.Text, txtphone.Text, txtnameproduct.Text);
                invoiceForm.Show();
                UpdateStockQuantity(productId, quantity);
                ReloadFormData();


            }



        }

        private void txtphone_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;

        }

        private void txtphone_KeyPress_1(object sender, KeyPressEventArgs e)
        {
              
        }
    }
}
