using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Items : Form
    {

        private string productName;
        private string productCategory;
        private string productSize;
        private string productCode;
        private string productPrice;
        private string productQuantity;
        private string productdistro;
        private Form parentForm;

        public Items(string productName, string productCategory, string productSize, string productCode, string productPrice, string productQuantity, string productdistro, Form parentForm)
        {
            this.productName = productName;
            this.productCategory = productCategory;
            this.productSize = productSize;
            this.productCode = productCode;
            this.productPrice = productPrice;
            this.productQuantity = productQuantity;
            this.productdistro = productdistro;
            this.parentForm = parentForm;
            InitializeComponent();
        }

        public Items(Form parentForm)
        {
            this.parentForm = parentForm;
            InitializeComponent();
            AddHints();
        }

        private void AddHints()
        {
            foreach (var variable in Controls)
            {
                if (variable is TextBox)
                {
                    ((TextBox) variable).ForeColor = SystemColors.GrayText;
                    ((TextBox) variable).Enter += TextEnter;
                    ((TextBox) variable).Leave += TextLeave;
                }
            }
        }

        private void TextLeave(object sender, EventArgs e)
        {
            
        }

        private void TextEnter(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
