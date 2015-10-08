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
    public partial class newOrderDialogDistro : Form
    {
        public newOrderDialogDistro()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void newOrderDialogDistro_Load(object sender, EventArgs e)
        {
            LoadDistro();
        }

        private void LoadDistro()
        {
            foreach (DataRow row in DatabaseConnection.DatabaseRecord.Tables["distributor"].Rows)
            {
                comboBox1.Items.Add(row["DistroName"].ToString());
            }
            try
            {
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var AddOrders = new Add_Orders(comboBox1.Text);
            AddOrders.ShowDialog();
            Dispose();
        }
    }
}
