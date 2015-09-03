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
    public partial class Add_Orders : Form
    {
        public Add_Orders()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertData();
            CreateOrder();
        }

        public void CreateOrder()
        {
            var newOrder = (DataGridViewRow) dataGridView1.Rows[0].Clone();
            newOrder.Cells[0].Value = productName;
            newOrder.Cells[1].Value = productSize;
            newOrder.Cells[2].Value = productQuantity;
            newOrder.Cells[3].Value = DateTime.Now.ToString();
            newOrder.Cells[4].Value = distroID;
            dataGridView1.Rows.Add(newOrder);
            ClearFields();

        }

        private void ClearFields()
        {
            foreach (var control in Controls)
            {
                var box = control as CueTextBox;
                if (box != null)
                {
                    box.Text = "";
                }
                else
                {
                    var comboBox = control as ComboBox;
                    if (comboBox != null)
                    {
                        comboBox.Items.Clear();
                    }
                    else
                    {
                        var down = control as NumericUpDown;
                        if (down != null)
                        {
                            down.Text = 0.ToString();
                        }
                    }
                }
            }
        }

        private  string productCode;
        private  string productName;
        private string productSize;
        private string distroID;
        private int productQuantity;


        private void InsertData()
        {
            productCode = cueTextBox4.Text;
            productName = cueTextBox3.Text;
            productQuantity = Convert.ToInt32(numericUpDown1.Value);
            productSize = comboBox1.SelectedText;
            distroID = comboBox2.SelectedText;
        }
    }
}
