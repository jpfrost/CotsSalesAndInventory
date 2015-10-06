using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Print_Inventory : Form
    {
        private DataSet ds;
        public Print_Inventory(DataSet inventoryset)
        {
            ds = inventoryset;
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
        }

        private void Print_Inventory_Load(object sender, EventArgs e)
        {
            var inventory = new PrintInventory();
            inventory.SetDataSource(ds);
            crystalReportViewer1.ReportSource = inventory;
        }
    }
}
