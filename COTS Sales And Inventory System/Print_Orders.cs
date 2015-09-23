using System;
using System.Data;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Print_Orders : Form
    {
        private readonly orders order;

        public Print_Orders()
        {
        }

        public Print_Orders(DataSet ds)
        {
            order = new orders();
            order.SetDataSource(ds);
            InitializeComponent();
        }

        private void Print_Orders_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = order;
            crystalReportViewer1.Zoom(70);
        }
    }
}