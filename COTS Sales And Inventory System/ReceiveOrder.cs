using System;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class ReceiveOrder : Form
    {
        private int _orderListID;
        public ReceiveOrder()
        {
            InitializeComponent();
        }

        public ReceiveOrder(int orderlistID)
        {
            _orderListID = orderlistID;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void ReceiveOrder_Load(object sender, EventArgs e)
        {
            cueTextBox1.Text = _orderListID.ToString();
        }

        private void cueTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void cueTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}