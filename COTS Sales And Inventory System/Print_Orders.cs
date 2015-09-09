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
    public partial class Print_Orders : Form
    {
        public Print_Orders()
        {
            
        }

        private orders order;
        public Print_Orders(DataSet ds)
        {
             order = new orders();
            order.SetDataSource(ds);
            InitializeComponent();
        }

        private void Print_Orders_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = order;
        }
    }
}
