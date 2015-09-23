using System;
using System.Data;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Print_Receipt : Form
    {
        private readonly receipt receipt;

        public Print_Receipt(DataSet ds)
        {
            receipt = new receipt();
            receipt.SetDataSource(ds);
            InitializeComponent();
        }

        private void Print_Receipt_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = receipt;
            crystalReportViewer1.Zoom(70);
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
        }
    }
}