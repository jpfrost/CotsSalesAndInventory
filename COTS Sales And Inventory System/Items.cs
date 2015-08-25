using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Items : Form
    {
        private Form _parentForm;
        private string _productCategory;
        private string _productCode;
        private string _productdistro;
        private string _productName;
        private string _productPrice;
        private string _productQuantity;
        private string _productSize;

        public Items(string productName, string productCategory, string productSize, string productCode,
            string productPrice, string productQuantity, string productdistro, Form parentForm)
        {
            this._productName = productName;
            this._productCategory = productCategory;
            this._productSize = productSize;
            this._productCode = productCode;
            this._productPrice = productPrice;
            this._productQuantity = productQuantity;
            this._productdistro = productdistro;
            this._parentForm = parentForm;
            InitializeComponent();
        }

        public Items(Form parentForm)
        {
            this._parentForm = parentForm;
            InitializeComponent();
            LoadComboBox("category","categoryName",comboBox1);
            LoadComboBox("distributor","distroName",comboBox3);
        }

        private void LoadComboBox(string table, string columb, ComboBox comboBox)
        {
            var dataCategory = DatabaseConnection.DatabaseRecord.Tables[table];
            foreach (DataRow rows in dataCategory.Rows)
            {
                comboBox.Items.Add(rows[columb].ToString());
            }
        }

        private void LoadDistro()
        {
            
        }

        private void LoadCategory()
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _parentForm.Refresh();
            Dispose();
        }

        private void Items_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProductExist())
                {
                    var prodId= GetProductId();
                    cueTextBox1.Text = prodId;
                }
                AddEditItemSize();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private String GetProductId()
        {
            var found = FindProductId();
           return found[0]["itemID"].ToString();
        }

        private Boolean ProductExist()
        {
            var found = FindProductId();

            return found.Length > 0;
        }

        private DataRow[] FindProductId()
        {
           return DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_Name ='"
                + cueTextBox2.Text + "'");
        }

        private void AddEditItemSize()
        {
            var exist= FindIfSizeExist();
            if (exist)
            {
               var foundSize= FindIfSize();
                foreach (DataRow row in foundSize)
                {
                    row["Price"] = cueTextBox4.Text;
                    row["Quantity"] = cueTextBox3.Text;
                    DatabaseConnection.UploadChanges();
                }
            }
            else
            {
                CreateNewSize();
            }
        }

        private void CreateNewSize()
        {
            var newRow = DatabaseConnection.DatabaseRecord.Tables["size"].NewRow();
            newRow["ItemID"] = cueTextBox1.Text;
            newRow["Size"] = comboBox2.Text;
            if (cueTextBox3.Text != "") newRow["Quantity"] = Convert.ToInt32(cueTextBox3.Text);
            if (cueTextBox4.Text != "") newRow["Price"] = Convert.ToDouble(cueTextBox4.Text);
            DatabaseConnection.DatabaseRecord.Tables["size"].Rows.Add(newRow);
            DatabaseConnection.UploadChanges();
            comboBox2.Items.Add(comboBox2.Text);
        }

        private bool FindIfSizeExist()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID = '" 
                + cueTextBox1.Text + "' and Size = '"
                + comboBox2.SelectedItem+"'" );
            return found.Length > 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ProductExist())
            {
                EditProduct();
                MessageBox.Show("Edit Successful");
            }
            else
            {
                AddNewItem();
                CreateNewSize();
                MessageBox.Show("New Product Added");
            }
        }

        

        private void EditProduct()
        {
            var exist = FindIfSizeExist();
            if (exist)
            {
                EditSize();
            }
            else
            {
                CreateNewSize();
            }
        }

        private void EditSize()
        {
            var found = FindIfSize();
            found[0]["Quantity"] = Convert.ToInt32(cueTextBox3.Text);
            found[0]["price"] = Convert.ToDouble(cueTextBox4.Text);
            DatabaseConnection.UploadChanges();

        }

        private DataRow[] FindIfSize()
        {
            return DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID = '" 
                + cueTextBox1.Text + "' and Size = '"
                + comboBox2.SelectedItem+"'" );
        }

        private void AddNewItem()
        {
            var newProdCode = "";
            var newItem = DatabaseConnection.DatabaseRecord.Tables["items"].NewRow();
            if (cueTextBox1.Text.Equals(""))
            {
                _productCode = DatabaseConnection.DatabaseRecord.Tables["items"].AsEnumerable()
                    .Max(maxValue => maxValue.Field<string>("itemID"));
                newProdCode = (Convert.ToInt32(_productCode) + 1).ToString();
            }
            else
            {
                newProdCode = cueTextBox1.Text;
            }
            newItem["itemID"] = newProdCode;
            newItem["item_Name"] = cueTextBox2.Text;
            if (comboBox1.SelectedItem != null) newItem["CategoryID"] = FindCategoryID();
            DatabaseConnection.DatabaseRecord.Tables["items"].Rows.Add(newItem);
            DatabaseConnection.UploadChanges();
            
        }

        private Int32 FindCategoryID()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["category"].Select("CategoryName ='"
                +comboBox1.Text+"'");

            return Convert.ToInt32(found[0]["CategoryID"].ToString());
        }

        private void ProductNameKeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    cueTextBox1.Text = (string)FindProductId()[0]["itemID"];
                    LoadSize();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void ProductIdKeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void cueTextBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                cueTextBox1.Text = (string)FindProductId()[0]["itemID"];
                LoadSize();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void LoadSize()
        {
            comboBox2.Items.Clear();
            var sizeRow = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='"
                +cueTextBox1.Text+"'");
            foreach (DataRow row in sizeRow.Where(row => !comboBox2.Items.Contains(row["Size"])))
            {
                comboBox2.Items.Add(row["size"]);
            }
            comboBox2.Refresh();
        }

        private void cueTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}