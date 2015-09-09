﻿using System;
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
    public partial class Print_Receipt : Form
    {
        private receipt receipt;
        public Print_Receipt(DataSet ds)
        {
            receipt = new receipt();
            receipt.SetDataSource(ds);
            InitializeComponent();
        }

        private void Print_Receipt_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource=receipt;
            crystalReportViewer1.Zoom(70);
        }
    }
}