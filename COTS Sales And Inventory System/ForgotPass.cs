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
    public partial class ForgotPass : Form
    {
        public ForgotPass()
        {
            InitializeComponent();
        }

        private void ForgotPass_Load(object sender, EventArgs e)
        {
            LoadSelection();
        }

        private void LoadSelection()
        {
            var values = GetSelection();
            InsertValues(values);
        }

        private void InsertValues(string[] values)
        {
            foreach (var value in values)
            {
                comboBox1.Items.Add(value);
            }
        }


        private string[] GetSelection()
        {
            var selection = new String[2];
            selection[0] = "Email";
            selection[1] = "Secret Question";

            return selection;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectTab(comboBox1.SelectedIndex);
        }

        private void SelectTab(int selectedIndex)
        {
            
        }
    }
}
