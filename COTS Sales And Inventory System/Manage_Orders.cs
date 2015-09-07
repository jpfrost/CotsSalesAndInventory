using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeLib;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Manage_Orders : Form
    {
        public Manage_Orders()
        {
            InitializeComponent();
        }

        private void Manage_Orders_Load(object sender, EventArgs e)
        {
            LoadOrderList();
        }

        private void LoadOrderList()
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var add_Order = new Add_Orders();
            add_Order.Show();
        }

        private void RefreshData_Tick(object sender, EventArgs e)
        {
            RefreshGrid();
            RefreshFilters();
        }

        private void RefreshFilters()
        {
            
        }

        private void RefreshGrid()
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
