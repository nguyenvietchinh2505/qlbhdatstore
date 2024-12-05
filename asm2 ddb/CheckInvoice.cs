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
    public partial class CheckInvoice : Form
    {
        string connectionString = @"Data Source=LAPTOP-C6H2FCNV\SQLEXPRESS;Initial Catalog=QLBH12;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public CheckInvoice()
        {
            InitializeComponent();
        }

        private void CheckInvoice_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            con.Open();
            cmd = new SqlCommand("select * from Invoice ", con);
            adt = new SqlDataAdapter(cmd);
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            LoadProductsIntoComboBox();
        }
        private void deletetextbox()
        {
            txtcustomer.Text = "";
          
            txtdescription.Text = "";
            txtprice.Text = "";
            txtquantity.Text = "";
            dateTimePicker1.Text = "";
            txtphone.Text = "";
            txtaddress.Text = "";
            cbbproductname.Text = "";
            txtcustomerid.Text = "";
            txtproductid.Text = "";
        }
        private void LoadProductsIntoComboBox()
        {
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT ProductID, ProductName FROM Product", con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbbproductname.DisplayMember = "ProductName";
                cbbproductname.ValueMember = "ProductID";
                cbbproductname.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            deletetextbox();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            txtaddress.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            txtquantity.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            txtprice.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            txtdescription.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            txtcustomer.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtphone.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            txtproductid.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtcustomerid.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            cbbproductname.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            UpdateProductInfo();


        }

        private void UpdateProductInfo()
        {
            if (!string.IsNullOrEmpty(txtproductid.Text))
            {
                int selectedProductID = int.Parse(txtproductid.Text);
                DataRowView selectedRow = null;

                
                foreach (DataRowView item in cbbproductname.Items)
                {
                    if ((int)item["ProductID"] == selectedProductID)
                    {
                        selectedRow = item;
                        break;
                    }
                }

                if (selectedRow != null)
                {
                  
                    cbbproductname.SelectedItem = selectedRow;
                }
            }
        }


        private bool checktextbox()
        {
            if (cbbproductname.Text == "") { MessageBox.Show("Please enter Productname"); return false; }
            if (txtaddress.Text == "") { MessageBox.Show("Please enter address "); return false; }
            if (txtquantity.Text == "") { MessageBox.Show("Please enter quantity "); return false; }
            if (string.IsNullOrWhiteSpace(txtquantity.Text) || Convert.ToInt32(txtquantity.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than zero.");
                return false;
            }

            
           

            if (txtdescription.Text == "") { MessageBox.Show("Please enter Description "); return false; }
            if (txtphone.Text == "") { MessageBox.Show("Please enter Phone "); return false; }
            if (string.IsNullOrWhiteSpace(txtphone.Text) || Convert.ToInt32(txtphone.Text) <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than zero.");
                return false;
            }

            if (dateTimePicker1.Text == "") { MessageBox.Show("Please enter Orderdate "); return false; }
            if (txtcustomer.Text == "") { MessageBox.Show("Please enter customer "); return false; }
            if (txtcustomerid.Text == "") { MessageBox.Show("Please enter customerid "); return false; }


            return true;
        }
        private void btncreate_Click(object sender, EventArgs e)
        {

            if (checktextbox())
            {
             
                int productID = int.Parse(txtproductid.Text); 
                string nameProduct = cbbproductname.Text;
                int customerID = int.Parse(txtcustomerid.Text); 
                string nameCustomer = txtcustomer.Text;
                string phoneNumber = txtphone.Text;
                int quantity = int.Parse(txtquantity.Text);
                string address = txtaddress.Text;
                decimal price = decimal.Parse(txtprice.Text);
                DateTime orderDate = DateTime.Parse(dateTimePicker1.Text);
                string description = txtdescription.Text;


                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Invoice (ProductID, NameProduct, CustomerID, NameCustomer, PhoneNumber, Quantity, Address, Price, OrderDate, Description) " +
                                                    "VALUES (@ProductID, @NameProduct, @CustomerID, @NameCustomer, @PhoneNumber, @Quantity, @Address, @Price, @OrderDate, @Description)", con);

                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@NameProduct", nameProduct);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@NameCustomer", nameCustomer);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                    cmd.Parameters.AddWithValue("@Description", description);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Invoice created successfully!");

               
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
            if (checktextbox())
            {
                int productID = int.Parse(txtproductid.Text);
                string nameProduct = cbbproductname.Text;
                int customerID = int.Parse(txtcustomerid.Text);
                string nameCustomer = txtcustomer.Text;
                string phoneNumber = txtphone.Text;
                int quantity = int.Parse(txtquantity.Text);
                string address = txtaddress.Text;
                decimal price = decimal.Parse(txtprice.Text);
                DateTime orderDate = DateTime.Parse(dateTimePicker1.Text);
                string description = txtdescription.Text;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("UPDATE Invoice SET NameProduct = @NameProduct, CustomerID = @CustomerID, NameCustomer = @NameCustomer, " +
                                                             "PhoneNumber = @PhoneNumber, Quantity = @Quantity, Address = @Address, Price = @Price, " +
                                                             "OrderDate = @OrderDate, Description = @Description WHERE ProductID = @ProductID", connection);

                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.Parameters.AddWithValue("@NameProduct", nameProduct);
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@NameCustomer", nameCustomer);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@OrderDate", orderDate);
                        command.Parameters.AddWithValue("@Description", description);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Invoice updated successfully!");

                        dt.Clear();
                        adt.Fill(dt);
                        dataGridView1.DataSource = dt;

                        deletetextbox();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void cbbproductname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbproductname.SelectedItem != null)
            {
                DataRowView selectedProduct = (DataRowView)cbbproductname.SelectedItem;
                int selectedProductID = (int)selectedProduct["ProductID"];
                string selectedProductName = selectedProduct["ProductName"].ToString();
                decimal selectedProductPrice = GetProductPrice(selectedProductID); 

               
                txtproductid.Text = selectedProductID.ToString();
                txtprice.Text = selectedProductPrice.ToString(); 
            }
        }

        //Ham lay gia tien tu ProuducID
        private decimal GetProductPrice(int productID)
        {
            decimal price = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Price FROM Product WHERE ProductID = @ProductID", con);
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        price = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return price;
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            UpdatePriceFromQuantity();

        }

       //ham cap nhat so luong
        private void UpdatePriceFromQuantity()
        {
            if (!string.IsNullOrEmpty(txtprice.Text) && !string.IsNullOrEmpty(txtquantity.Text))
            {
                decimal pricePerUnit = decimal.Parse(txtprice.Text);
                int quantity = int.Parse(txtquantity.Text);
                decimal totalPrice = pricePerUnit * quantity;
                txtprice.Text = totalPrice.ToString();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtproductid.Text))
            {
                int productID = int.Parse(txtproductid.Text);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("DELETE FROM Invoice WHERE ProductID = @ProductID", connection);
                        command.Parameters.AddWithValue("@ProductID", productID);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Invoice deleted successfully!");

                            dt.Clear();
                            adt.Fill(dt);
                            dataGridView1.DataSource = dt;

                            deletetextbox();
                        }
                        else
                        {
                            MessageBox.Show("No invoice found with the given Product ID.");
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
            fAdmin fAdmin=new fAdmin();
            fAdmin.ShowDialog();
            this.Close();

        }

        private void txtaddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
         
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
           
        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtquantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            // Tạo mới một workbook
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;
            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = "Danh sách";

            // tao phan tieu de
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "K1");
            head.MergeCells = true;
            head.Value2 =  "List of invoices";
            head.Font.Bold = true;
            head.Font.Name = "Times New Roman";
            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tieu đề cột

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
            cl1.Value2 = "InvoiceID";
            cl1.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
            cl2.Value2 = "ProductID";
            cl2.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
            cl3.Value2 = "CustomerID";
            cl3.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
            cl4.Value2 = "Name Product";
            cl4.ColumnWidth = 35.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
            cl5.Value2 = "Name Customer";
            cl5.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
            cl6.Value2 = "Phone Number";
            cl6.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
            cl7.Value2 = "Quantity";
            cl7.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
            cl8.Value2 = "Address";
            cl8.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
            cl9.Value2 = "Price";
            cl9.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
            cl10.Value2 = "OrderDate";
            cl10.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
            cl11.Value2 = "Description ";
            cl11.ColumnWidth = 35.0;

            




            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "K3");
            rowHead.Font.Bold = true;

            // ke vien

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            // Thiết lập màu nền
            rowHead.Interior.ColorIndex = 6;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // tạo mảng theo đb


            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Invoice", con);
            adapter.Fill(dt);
            
            if(dt.Rows.Count > 0)
            {
                for(int i =0;i< dt.Rows.Count;i++)
                {
                    var InvoiceID =dt.Rows[i]["InvoiceID"].ToString();
                    var ProductID = dt.Rows[i]["ProductID"].ToString();
                    var CustomerID = dt.Rows[i]["CustomerID"].ToString();
                    var NameProduct = dt.Rows[i]["NameProduct"].ToString();
                    var NameCustomer = dt.Rows[i]["NameCustomer"].ToString();
                    var PhoneNumber = dt.Rows[i]["PhoneNumber"].ToString();
                    var Quantity = dt.Rows[i]["Quantity"].ToString();
                    var Address = dt.Rows[i]["Address"].ToString();
                    var Price = dt.Rows[i]["Price"].ToString();
                    var OrderDate = dt.Rows[i]["OrderDate"].ToString();
                    var Description = dt.Rows[i]["Description"].ToString();


                }
            }

            object[,] arr = new object[dt.Rows.Count, 11];

            for (int row = 0; row < dt.Rows.Count ; row++)
            {
                arr[row, 0] = dt.Rows[row]["InvoiceID"].ToString();
                arr[row, 1] = dt.Rows[row]["ProductID"].ToString();
                arr[row, 2] = dt.Rows[row]["CustomerID"].ToString();
                arr[row, 3] = dt.Rows[row]["NameProduct"].ToString();
                arr[row, 4] = dt.Rows[row]["NameCustomer"].ToString();
                arr[row, 5] = dt.Rows[row]["PhoneNumber"].ToString();
                arr[row, 6] = dt.Rows[row]["Quantity"].ToString();
                arr[row, 7] = dt.Rows[row]["Address"].ToString();
                arr[row, 8] = dt.Rows[row]["Price"].ToString();
                arr[row, 9] = dt.Rows[row]["OrderDate"].ToString();
             
                arr[row, 10] = dt.Rows[row]["Description"].ToString();


            }


            int rowStart = 4;
            int columnStart = 1;
            int columnEnd = 11;
            int rowEnd = rowStart + dt.Rows.Count - 1;

            // ô bắt đầu du lieu
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // ô kết thức dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // lay ve vung dien du lieu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            // dien du lieu vao vung da thiet lap

            range.Value2 = arr;

            // ke vien
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // can giua ca bang

            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtphone_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
