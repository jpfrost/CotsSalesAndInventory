using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Items : UserControl
    {
        private static string _productName;
        private string _productCategory;
        private string _productCode;
        private string _productdistro;
        private string _productPrice;
        private string _productQuantity;
        private string _productSize;

        public Items()
        {
            InitializeComponent();
            LoadAllComboBoxData();
        }

        private void LoadAllComboBoxData()
        {
            LoadComboBox("category", "categoryName", comboBox1);
            LoadComboBox("distributor", "distroName", comboBox3);
        }

        private void LoadComboBox(string table, string columb, ComboBox comboBox)
        {
            var dataCategory = DatabaseConnection.DatabaseRecord.Tables[table];
            foreach (DataRow rows in dataCategory.Rows)
            {
                comboBox.Items.Add(rows[columb].ToString());
            }
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
                    var prodId = GetProductId();
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
            var exist = FindIfSizeExist();
            if (exist)
            {
                var foundSize = FindSize();
                foreach (var row in foundSize)
                {
                    row["Price"] = cueTextBox4.Text;
                    row["Quantity"] = cueTextBox3.Text;
                    DatabaseConnection.UploadChanges();
                }
                MessageBox.Show("Item Edit Successful");
            }
            else
            {
                CreateNewSize();
                MessageBox.Show("Item  new Size Created");
            }
        }

        private void CreateNewSize()
        {
            var newRow = DatabaseConnection.DatabaseRecord.Tables["size"].NewRow();
            if (cueTextBox1.Text.Equals("")) GetItemID();
            newRow["ItemID"] = cueTextBox1.Text;
            newRow["Size"] = comboBox2.Text;
            if (cueTextBox3.Text != "") newRow["Quantity"] = Convert.ToInt32(cueTextBox3.Text);
            if (cueTextBox4.Text != "") newRow["Price"] = Convert.ToDouble(cueTextBox4.Text);
            DatabaseConnection.DatabaseRecord.Tables["size"].Rows.Add(newRow);
            DatabaseConnection.UploadChanges();
            comboBox2.Items.Add(comboBox2.Text);
        }

        private void GetItemID()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_Name ='"
                                                                                 + cueTextBox2.Text + "'");
            cueTextBox1.Text = found[0]["itemID"].ToString();
        }

        private bool FindIfSizeExist()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID = '"
                                                                                + cueTextBox1.Text + "' and Size = '"
                                                                                + comboBox2.SelectedItem + "'");
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

            ClearInputs();
        }

        public void ClearInputs()
        {
            foreach (var control in Controls)
            {
                var box = control as TextBox;
                if (box != null)
                {
                    box.Text = "";
                }
                var combo = control as ComboBox;
                if (combo != null)
                {
                    combo.Items.Clear();
                    combo.Text = "";
                }
            }
            LoadAllComboBoxData();
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
            var found = FindSize();
            found[0]["Quantity"] = Convert.ToInt32(cueTextBox3.Text);
            found[0]["price"] = Convert.ToDouble(cueTextBox4.Text);
            DatabaseConnection.UploadChanges();
        }

        private DataRow[] FindSize()
        {
            return DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID = '"
                                                                           + cueTextBox1.Text + "' and Size = '"
                                                                           + comboBox2.SelectedItem + "'");
        }

        private void AddNewItem()
        {
            var newProdCode = "";
            var newItem = DatabaseConnection.DatabaseRecord.Tables["items"].NewRow();
            if (!cueTextBox1.Text.Equals(""))
            {
                newProdCode = cueTextBox1.Text;
                newItem["itemID"] = newProdCode;
            }

            newItem["item_Name"] = cueTextBox2.Text;
            if (comboBox1.SelectedItem != null) newItem["CategoryID"] = FindCategoryId();
            DatabaseConnection.DatabaseRecord.Tables["items"].Rows.Add(newItem);
            DatabaseConnection.UploadChanges();
            DatabaseConnection.DatabaseRecord.Tables.Remove("items");
            DatabaseConnection.DatabaseRecord.Tables.Add(
                DatabaseConnection.GetCustomTable(
                    DatabaseConnection.CreateSelectStatement("items"), "items"));
        }

        private int GetCurrentCount(string tableName, string columbName)
        {
            var value = 0;
            if (DatabaseConnection.DatabaseRecord.Tables[tableName].Rows.Count == 0)
            {
                return 1;
            }
            value =
                (from DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows
                    select (int) rows[columbName]).Concat(new[] {value}).Max();
            return value + 1;
        }

        private Int32 FindCategoryId()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["category"].Select("CategoryName ='"
                                                                                    + comboBox1.Text + "'");

            return Convert.ToInt32(found[0]["CategoryID"].ToString());
        }

        private void ProductNameKeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            try
            {
                cueTextBox1.Text = (string) FindProductId()[0]["itemID"];
                LoadSize();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ProductIdKeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (FindProductName().Length <= 0) return;
            try
            {
                LoadItemData();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void InsertProductName(string itemName)
        {
            cueTextBox2.Text = itemName;
        }

        private DataRow[] FindProductName()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("itemID ='"
                                                                                 + cueTextBox1.Text + "'");
            return found;
        }

        private void cueTextBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                cueTextBox1.Text = (string) FindProductId()[0]["itemID"];
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
                                                                                  + cueTextBox1.Text + "'");
            foreach (var row in sizeRow.Where(row => !comboBox2.Items.Contains(row["Size"])))
            {
                comboBox2.Items.Add(row["size"]);
            }
            comboBox2.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (FindIfSizeExist())
            {
                DeleteItemSize();
            }
            MessageBox.Show("Size has been deleted");
        }

        private void DeleteItemSize()
        {
            var found = FindSize();
            foreach (var dataRow in found)
            {
                dataRow.Delete();
            }
            DatabaseConnection.UploadChanges();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void KeyboardValidInputs(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b' || e.KeyChar == Convert.ToChar((" ")))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void KeyboardOnlyDigits(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || (e.KeyChar == '\b'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void KeyboardOnlyDecimals(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char) Keys.Back || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
            var txtDecimal = sender as TextBox;
            if (txtDecimal != null && (e.KeyChar == '.' && txtDecimal.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void LoadItemData()
        {
            var itemData = FindItem();
            var itemSize = FindSizes();
            FillForm(itemData, itemSize);
        }

        private void FillForm(DataRow[] itemData, DataRow[] itemSize)
        {
            cueTextBox2.Text = itemData[0]["Item_Name"].ToString();
            SelectCategory(itemData);
            FillSizeSelection(itemSize);
        }

        private void FillSizeSelection(DataRow[] itemSize)
        {
            ClearSizeSelection();
            foreach (var rowData in itemSize)
            {
                comboBox2.Items.Add(rowData["Size"]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void ClearSizeSelection()
        {
            comboBox2.Items.Clear();
        }

        private void SelectCategory(DataRow[] itemData)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["category"].Select("categoryID ='"
                                                                                    + itemData[0]["categoryID"] + "'");
            comboBox1.SelectedItem = found[0]["categoryName"];
        }

        private DataRow[] FindSizes()
        {
            return DatabaseConnection.DatabaseRecord.Tables["size"].Select("ItemID ='"
                                                                           + cueTextBox1.Text + "'");
        }

        private DataRow[] FindItem()
        {
            return DatabaseConnection.DatabaseRecord.Tables["Items"].Select("ItemID ='"
                                                                            + cueTextBox1.Text + "'");
        }

        private void LoadSizeData(object sender, EventArgs e)
        {
            var found = FindSizeId();
            cueTextBox3.Text = found[0]["Quantity"].ToString();
            cueTextBox4.Text = found[0]["Price"].ToString();
        }

        private DataRow[] FindSizeId()
        {
            return DatabaseConnection.DatabaseRecord.Tables["size"].Select("ItemID ='"
                                                                           + cueTextBox1.Text + "' and Size ='" +
                                                                           comboBox2.SelectedItem + "'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}