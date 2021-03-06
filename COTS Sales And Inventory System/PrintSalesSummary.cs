﻿using System;
using System.Data;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class PrintSalesSummary : Form
    {
        private readonly DataSet _summaryReport;

        public PrintSalesSummary(DataSet summaryReport)
        {
            _summaryReport = summaryReport;
            InitializeComponent();
        }

        private void PrintSalesSummary_Load(object sender, EventArgs e)
        {
            var saleSum = new SalesSummary();
            saleSum.SetDataSource(_summaryReport);
            crystalReportViewer1.ReportSource = saleSum;
            crystalReportViewer1.Zoom(50);
        }
    }
}