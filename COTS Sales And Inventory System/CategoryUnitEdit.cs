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
    public partial class CategoryUnitEdit : Form
    {
        public CategoryUnitEdit()
        {
            InitializeComponent();
        }

        private void CategoryUnitEdit_Load(object sender, EventArgs e)
        {
            var x = Properties.Settings.Default.CategoryUnits;
            foreach (var units in x)
            {
                listBox1.Items.Add(units);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Are You sure you want to modify Category units","Modify Category Units",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                SaveToSettings();
            Dispose();
            }
        }

        private void SaveToSettings()
        {
            Properties.Settings.Default.CategoryUnits.Clear();
            foreach (string units in listBox1.Items)
            {
                Properties.Settings.Default.CategoryUnits.Add(units);
            }
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cueTextBox1.Text)) return;
            AddtoListbox();
        }

        private void AddtoListbox()
        {
            if (!listBox1.Items.Contains(cueTextBox1.Text))
            {
                listBox1.Items.Add(cueTextBox1.Text);
            }
            MessageBox.Show(cueTextBox1.Text+" has been added");
            cueTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var y = listBox1.SelectedItems.OfType<string>().ToList();
            var unitsList = CreateStringList(y);
            if (y.Count == 0) return;
            var dialog = MessageBox.Show("Are you sure you want to remove "+unitsList,"Remove Category Units",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

            if (dialog == DialogResult.Yes)
            {
                foreach (var x in y)
                {
                    listBox1.Items.Remove(x);
                }
            }

        }

        private string CreateStringList(List<string> list)
        {
            var x = new StringBuilder();
            foreach (var units in list)
            {
                x.Append(units);
                x.Append("\n");
            }
            return x.ToString();
        }
    }
}
