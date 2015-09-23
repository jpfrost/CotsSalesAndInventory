using System;
using System.Data;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class AddItemForSale : Form
    {
        private readonly String _productName;
        private DataRow[] _itemSizes;

        public AddItemForSale(string s)
        {
            _productName = s;
            InitializeComponent();
        }

        private void AddItemForSale_Load(object sender, EventArgs e)
        {
            cueTextBox1.Text = _productName;
            var found = GetProductDetails();
            _itemSizes = GetProductSizes(found[0]["itemID"].ToString());
            LoadSizesInComboBox(_itemSizes);
        }

        private DataRow[] GetProductSizes(string itemId)
        {
            var itemSizes = DatabaseConnection.DatabaseRecord.Tables["size"].Select("ItemID ='"
                                                                                    + itemId + "'");
            return itemSizes;
        }

        private void LoadSizesInComboBox(DataRow[] itemSizes)
        {
            foreach (var sizeRow in itemSizes)
            {
                comboBox1.Items.Add(sizeRow["Size"].ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private DataRow[] GetProductDetails()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_Name ='"
                                                                                 + _productName + "'");
            return found;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}