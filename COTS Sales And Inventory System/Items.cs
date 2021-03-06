﻿using System;
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
            AutoCompleteItemName();
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            radioButton1.Checked = true;
            if (!Properties.Settings.Default.quantMod)
            {
                cueTextBox5.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                button1.Enabled = false;
            }
            cueTextBox4.Enabled = Properties.Settings.Default.priceMod;
            if (!Properties.Settings.Default.EnableOrdering)
            {
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox3.DropDownStyle=ComboBoxStyle.DropDown;
            }
        }

        private void AutoCompleteItemName()
        {
            var autoCompleteCollectionProductName = new AutoCompleteStringCollection();
            foreach (DataRow dr in DatabaseConnection.DatabaseRecord.Tables["items"].Rows)
            {
                autoCompleteCollectionProductName.Add(dr["Item_Name"].ToString());
            }
            cueTextBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cueTextBox2.AutoCompleteCustomSource = autoCompleteCollectionProductName;
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

        private void CreateNewSize()
        {
            var newRow = DatabaseConnection.DatabaseRecord.Tables["size"].NewRow();
            if (cueTextBox1.Text.Equals("")) GetItemID();
            newRow["ItemID"] = cueTextBox1.Text;
            newRow["Size"] = comboBox3.Text+" "+comboBox2.Text;
            newRow["sizeEnable"] = 1;
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
                                                                                + cueTextBox1.Text + "' and Size = '"+comboBox3.Text+" "
                                                                                + comboBox2.Text + "'");
            return found.Length > 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cueTextBox2.Text.Equals("") || comboBox1.Text.Equals("") || comboBox2.Text.Equals("") || comboBox3.Text.Equals(""))
            {
                MessageBox.Show("Please enter required inputs");
                return;
            }
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
            try
            {
                var found = FindSize();
                found[0]["Quantity"] = Convert.ToInt32(cueTextBox3.Text);
                found[0]["price"] = Convert.ToDouble(cueTextBox4.Text);
                found[0]["sizeEnable"] = 1;
                DatabaseConnection.UploadChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private DataRow[] FindSize()
        {
            return DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID = '"
                                                                           + cueTextBox1.Text + "' and Size = '"+comboBox3.Text+" "
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
            else
            {
                newItem["itemID"] = CreateNewProductID();
            }

            newItem["item_Name"] = cueTextBox2.Text;
            newItem["CategoryID"] = FindCategoryId();
            DatabaseConnection.DatabaseRecord.Tables["items"].Rows.Add(newItem);
            DatabaseConnection.UploadChanges();
            /*DatabaseConnection.DatabaseRecord.Tables.Remove("items");
            DatabaseConnection.DatabaseRecord.Tables.Add(
                DatabaseConnection.GetCustomTable(
                    DatabaseConnection.CreateSelectStatement("items"), "items"));*/
        }

        private string CreateNewProductID()
        {
            var x = GetCurrentCount("items_seq", "id");
            var newitemSeqRow = DatabaseConnection.DatabaseRecord.Tables["items_seq"].NewRow();
            newitemSeqRow["id"] = x;
            DatabaseConnection.DatabaseRecord.Tables["items_seq"].Rows.Add(newitemSeqRow);
            return "MANUAL" + x;
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
           
            var catID=    Convert.ToInt32(found[0]["CategoryID"].ToString());
            

            return catID;
        }

        private int CreateNewCategory()
        {
            var newCategoryRow = DatabaseConnection.DatabaseRecord.Tables["category"].NewRow();
            newCategoryRow["CategoryID"] = GetCurrentCount("category", "categoryID");
            newCategoryRow["CategoryName"] = comboBox1.Text;
            DatabaseConnection.DatabaseRecord.Tables["category"].Rows.Add(newCategoryRow);

            return Convert.ToInt32(newCategoryRow["CategoryID"]);
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
                SelectCategory(FindProductId());
                LoadSize();
            }
            catch (Exception exception)
            {
                cueTextBox1.Text="";
            }
        }

        private void LoadSize()
        {
            comboBox2.Items.Clear();
            var sizeRow = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='"
                                                                                  + cueTextBox1.Text + "'");
            foreach (var row in sizeRow.Where(row => !comboBox2.Items.Contains(row["Size"])))
            {
                var x = row["size"].ToString().Split(' ');
                if (!comboBox2.Items.Contains(x[1]))
                {
                    comboBox2.Items.Add(x[1]);
                }
                if (!comboBox3.Items.Contains(x[0]))
                {
                    comboBox3.Items.Add(x[0]);
                }
            }
            comboBox2.Refresh();
            comboBox2.SelectedIndex = 0;
            comboBox3.Refresh();
            comboBox3.SelectedIndex = 0;
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
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char) Keys.Back || e.KeyChar == '.' || e.KeyChar!= ' '))
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
            try
            {
                cueTextBox2.Text = itemData[0]["Item_Name"].ToString();
                SelectCategory(itemData);
                FillSizeSelection(itemSize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void FillSizeSelection(DataRow[] itemSize)
        {
            ClearSizeSelection();
            foreach (var rowData in itemSize)
            {
                var x = rowData["size"].ToString().Split(' ');
                comboBox2.Items.Add(x[1]);
                comboBox3.Items.Add(x[0]);
            }
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void ClearSizeSelection()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
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
            try
            {
                var found = FindSizeId();
                cueTextBox3.Text = found[0]["Quantity"].ToString();
                cueTextBox4.Text = found[0]["Price"].ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private DataRow[] FindSizeId()
        {
            return DatabaseConnection.DatabaseRecord.Tables["size"].Select("ItemID ='"
                                                                           + cueTextBox1.Text + "' and Size ='" +comboBox3.Text+" "+
                                                                           comboBox2.Text + "'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var itemDisabled = false;
            try
            {
                var Item = DatabaseConnection.DatabaseRecord.Tables["items"].Select("item_name ='"+cueTextBox2.Text+"'");
                var itemID = Item[0]["ItemID"].ToString();
                var size = DatabaseConnection.DatabaseRecord.Tables["size"].Select("size ='"
                                                                                  +comboBox3.Text+" " +comboBox2.Text+"' and itemID ='"+itemID+"'");

                size[0]["sizeEnable"] = 0;
                itemDisabled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            DatabaseConnection.UploadChanges();
            if (itemDisabled)
            {
                MessageBox.Show("Product has been disabled");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Are you sure you want to edit items quantity", "Edit Quantity",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dialog != DialogResult.Yes) return;
            var x = 0;
            try
            {
                x = Convert.ToInt32(cueTextBox3.Text);
            }
            catch (Exception a)
            {
                // ignored
            }
            var y = Convert.ToInt32(cueTextBox5.Text);

            if (radioButton1.Checked)
            {
                cueTextBox3.Text = (x + y).ToString();
            }
            else
            {
                if ((x - y) < 0)
                {
                    MessageBox.Show("Can't be negative value");
                }
                else
                {
                    cueTextBox3.Text = (x - y).ToString();
                }
            }
            cueTextBox5.Clear();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadSizeForCategory();
        }

        private void LoadSizeForCategory()
        {
            var x = comboBox1.Text;
            comboBox2.Items.Clear();
            var database = DatabaseConnection.GetCustomTable("SELECT * FROM cotsalesinventory.category inner join tblsizes on tblsizes.CategoryID=category.CategoryID where CategoryName='"+x+"';","categoryUnits");
            foreach (DataRow row in database.Rows)
            {
                if (!comboBox2.Items.Contains(row["sizesName"]))
                {
                    comboBox2.Items.Add(row["sizesName"]);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSizeForCategory();
        }
    }
}