using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asm2_ddb
{
    public partial class InvoiceForm : Form
    {
        public InvoiceForm(string customerName, string quantity, string address, DateTime date, string price, string description,string phone,string nameproduct)
        {
            InitializeComponent();


            txtNameCustomer.Text = customerName;
            txtQuantity.Text = quantity;
            txtAddress.Text = address;
            dateTimePicker1.Value = date;
            txtPrice.Text = price;
            txtDescription.Text = description;
            txtphone.Text = phone;
            txtnameproduct.Text = nameproduct;
        }

        private void Invoice_Load(object sender, EventArgs e)
        {

        }
    }
}
