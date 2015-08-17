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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            
            BindInventoryRecordsToDatagrid(dataGridView1);
        }

        private void BindInventoryRecordsToDatagrid(DataGridView gridView1)
        {
            gridView1.DataSource = DatabaseConnection.databaseRecord.Tables["Inventory"];
        }
    }
}
