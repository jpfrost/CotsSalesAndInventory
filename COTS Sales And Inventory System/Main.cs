using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Main : Form
    {

        
        private readonly Form _loginForm;
        Items _items = new Items();


        public Main(Form loginForm)
        {
            _loginForm = loginForm;
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            var x = Task.Run(() => { LoadData(); });
            panel1.Controls.Add(_items);
            _items.Hide();
            dateTime.Start();
            timerDataRefresh.Start();
        }

        private void LoadData()
        {
            LoadInventory();
            
        }


        public void LoadInventory()
        {
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                ClearCategory();
                FillCategoryListBox();
                FillCategoryComboBox();
                LoadFromDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void LoadFromDatabase()
        {
            BeginInvoke(new Action(FillInventory));
        }

        private void FillInventory()
        {
            var dt = DatabaseConnection.GetCustomTable("select Item_Name as 'Product', Size, Price, Quantity, CategoryName as 'Category' from items inner join size on items.ItemID=size.ItemID inner join category on items.CategoryID=category.CategoryID;"
                ,"SizeAndItemTable");
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();
            AddAutoCompleteForSalesTextBox();
        }

        private void FillCategoryComboBox()
        {
            foreach (var catName in listBox1.Items)
            {
                comboBox1.Items.Add(catName);
            }
            foreach (DataRow rows in DatabaseConnection.DatabaseRecord.Tables["distributor"].Rows)
            {
                try
                {
                    if (!comboBox2.Items.Contains(rows["distroName"]))
                    {
                        comboBox2.Items.Add(rows["distroName"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }

        private void FillCategoryListBox()
        {
            listBox1.BeginUpdate();
            listBox1.Items.Add("All");
            foreach (DataRow rows in DatabaseConnection.DatabaseRecord.Tables["category"].Rows)
            {
                if (!listBox1.Items.Contains(rows["categoryName"].ToString()))
                {
                    listBox1.Items.Add(rows["categoryName"].ToString());
                }
            }
            listBox1.EndUpdate();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            AddCategory();
            LoadData();
        }

        private void AddCategory()
        {
            var catPk = GetCurrentCount("category", "CategoryID");
            var catName = comboBox1.Text;
            var found = FindRow("category", "categoryName = '" + catName + "'");
            if (found.Length > 0)
            {
                MessageBox.Show(@"Category Exist this category will not be created", @"Category Exist");
            }
            else
            {
                InsertNewCategory(catPk, catName);
            }
        }

        private DataRow[] FindRow(string tablename, string query)
        {
            return DatabaseConnection.DatabaseRecord.Tables[tablename].Select(query);
        }

        private void InsertNewCategory(int catPk, string catName)
        {
            var newCategory = DatabaseConnection.DatabaseRecord.Tables["category"].NewRow();
            newCategory[0] = catPk;
            newCategory[1] = catName;
            DatabaseConnection.DatabaseRecord.Tables["category"].Rows.Add(newCategory);
            DatabaseConnection.UploadChanges();
            RefreshData();
            MessageBox.Show(@"Category Added");
        }

        private void ClearCategory()
        {
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
        }

        private int GetCurrentCount(string tableName, string columbName)
        {
            var value = 0;
            if (DatabaseConnection.DatabaseRecord.Tables[tableName].Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                value = (from DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows select (int) rows[columbName]).Concat(new[] {value}).Max();
            }
            return value + 1;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var del = comboBox1.SelectedItem;
            var found = FindRow("category", "categoryName ='" + del + "'");
            DeleteComboBox(comboBox1, found);
        }

        private void DeleteComboBox(ComboBox cb, DataRow[] found)
        {

            foreach (DataRow rows in found)
            {
                rows.Delete();
            }
            DatabaseConnection.UploadChanges();
            MessageBox.Show(@"Delete Successful");
            cb.Text = "";
            RefreshData();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddDistributor();
        }

        private void AddDistributor()
        {
            var distroId = GetCurrentCount("distributor", "DistroID");
            var distroName = comboBox2.Text;
            var distroEmail = textBox9.Text;
            var distroNumber = textBox10.Text;
            var found = FindRow("distributor", "DistroName = '" + distroName + "'");
            if (found.Length > 0)
            {
                MessageBox.Show(@"Distro Exist this distributor will not be created", @"Distributor Exist");
            }
            else
            {
                InsertNewDistro(distroId, distroName,distroEmail,distroNumber);
            }

        }

        private void InsertNewDistro(int distroId, string distroName, string distroEmail, string distroNumber)
        {
            var newDistro = DatabaseConnection.DatabaseRecord.Tables["distributor"].NewRow();
            newDistro[0] = distroId;
            newDistro[1] = distroName;
            newDistro[2] = distroEmail;
            newDistro[3] = distroNumber;
            DatabaseConnection.DatabaseRecord.Tables["distributor"].Rows.Add(newDistro);
            DatabaseConnection.UploadChanges();
            MessageBox.Show(@"New distributor added...");
            RefreshData();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            var del = comboBox2.SelectedItem;
            var found = FindRow("distributor", "DistroName ='" + del + "'");
            DeleteComboBox(comboBox2, found);
            RefreshData();
        }

        private void ShowHideInventory(object sender, EventArgs e)
        {
            if (mainTab.SelectedIndex == 1 || mainTab.SelectedIndex == 0)
            {
                ShowInventoryGrid();
            }
            else
            {
                HideInventoryGrid();
            }
        }

        private void HideInventoryGrid()
        {
            if (!label11.Visible && !dataGridView1.Visible) return;
            label11.Visible = false;
            dataGridView1.Visible = false;
        }

        private void ShowInventoryGrid()
        {
            if (label11.Visible && dataGridView1.Visible) return;
            label11.Visible = true;
            dataGridView1.Visible = true;
        }

        private void TxtboxSearchEnter(object sender, EventArgs e)
        {
            if (((TextBox) sender).Text.Equals(@"Search"))
            {
                ((TextBox) sender).ForeColor = SystemColors.WindowText;
                ((TextBox) sender).Text = "";
            }
        }

        private void TxtboxSearchLeave(object sender, EventArgs e)
        {
            if (!((TextBox) sender).Text.Equals(@"Search"))
            {
                ((TextBox) sender).ForeColor = SystemColors.GrayText;
                ((TextBox) sender).Text = @"Search";
            }
        }

        private void KillApplication(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            _items.InsertProductName(cueTextBox2.Text);
            SetItemFormVisibility();
        }

        private void SetItemFormVisibility()
        {
            if (!_items.Visible)
            {
                _items.Show();
            }
            else
            {
                _items.Hide();
                _items.ClearInputs();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetDistrosInformation(); 
                
            }
            catch(Exception aException)
            {

            }

            
            
            
        }

        private void GetDistrosInformation()
        {
            var found = FindRow("distributor", "DistroName ='" + comboBox2.SelectedItem + "'");
            textBox10.Text = found[0][3].ToString();
            textBox9.Text = found[0][2].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetItemFormVisibility();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTime_Tick(object sender, EventArgs e)
        {
            var dateNow = DateTime.Now;
            lblTime.Text = dateNow.ToString();

        }

        private void timerDataRefresh_Tick(object sender, EventArgs e)
        {
            LoadFromDatabase();
        }

        private void AddAutoCompleteForSalesTextBox()
        {
            var autoCompleteCollectionProductName = new AutoCompleteStringCollection();
            foreach (DataRow dr in DatabaseConnection.DatabaseRecord.Tables["items"].Rows)
            {
                autoCompleteCollectionProductName.Add(dr["Item_Name"].ToString());
            }
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteCustomSource = autoCompleteCollectionProductName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _loginForm.Show();
            this.Dispose();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterInvetoryByCategory();
        }

        private void FilterInvetoryByCategory()
        {
            if (!listBox1.SelectedItem.Equals("All"))
            {
                ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = ("Category ='"
                 + listBox1.SelectedItem + "'");
            }
            else
            {
                ((DataTable) dataGridView1.DataSource).DefaultView.RowFilter = string.Empty;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void KeyboardOnlyDecimals(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            var txtDecimal = sender as TextBox;
            if (txtDecimal != null && (e.KeyChar == '.' && txtDecimal.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        

        private void DragPaste(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(String)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void DragnDrop(object sender, DragEventArgs e)
        {
            
            Point clientPoint = dataGridView2.PointToClient(new Point(e.X, e.Y));

            
            if (e.Effect == DragDropEffects.Copy)
            {
                string cellvalue = e.Data.GetData(typeof(string)) as string;
                var hittest = dataGridView2.HitTest(clientPoint.X, clientPoint.Y);
                if (hittest.ColumnIndex != -1
                    && hittest.RowIndex != -1)
                    dataGridView2[hittest.ColumnIndex, hittest.RowIndex].Value = cellvalue;

            }
        }
       

        

        private void MouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.DoDragDrop(dataGridView1.CurrentCell.Value.ToString(), DragDropEffects.Copy);
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void DragDrop(object sender, DragEventArgs e)
        {
            textBox4.Text = e.Data.GetData(DataFormats.Text) as string;
        }

        

        private void cueTextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    FindProductName();
                    LoadProductInfo();
                    /*textBox4.Clear();*/
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void FindProductName()
        {
            try
            {
                var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("ItemID ='"
                                                                                     +cueTextBox6.Text+"'");
                textBox4.Text= found[0]["Item_Name"].ToString();
                cueTextBox6.Text = "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ClearCueBox(object sender, EventArgs e)
        {
            ((TextBox) sender).Text = "";
        }

        private void DragAddItemPromt(object sender, DragEventArgs e)
        {
            AddSaleItem(e.Data.GetData(DataFormats.Text) as string);
        }

        private void AddSaleItem(string s)
        {
            var addItem = new AddItemForSale(s);
            addItem.Show();
        }

        private string recordedInput;
        private Stopwatch timer = new Stopwatch();

        

        private void ReadKeyInput()
        {
            timer.Stop();
            BeginInvoke(new Action(InsertToProductCode));
            
        }

        private void InsertToProductCode()
        {
            if (timer.ElapsedMilliseconds<=70 && !textBox4.Text.Equals(""))
            {
                try
                {
                    cueTextBox6.Text = recordedInput;
                    FindProductName();
                    LoadProductInfo();
                    AddItemToGridView();
                    /*textBox4.Clear();
                    comboBox3.Items.Clear();*/

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            recordedInput = "";
            timer.Reset();
            
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            timer.Start();
            if (e.KeyCode==Keys.Enter)
            {
                var x = Task.Run(() => { ReadKeyInput(); });
            }
            else
            {
                var keyConvert = new KeysConverter();
                recordedInput += keyConvert.ConvertToString(e.KeyCode);
            }
        }

        private void ProductNameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                LoadProductInfo();
            }
        }



        private void LoadProductInfo()
        {

            try
            {
                var tablename = "items";
                var query = "Item_Name ='" + textBox4.Text + "'";
                var foundItemData= FindData(tablename,query);
                var itemID = foundItemData[0]["ItemID"].ToString();
                var foundItemSize = FindData("size","itemID ='"+itemID+"'");
                InsertSizeListSalesComboBox(foundItemSize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

        }

        private void InsertSizeListSalesComboBox(DataRow[] foundItemData)
        {
            comboBox3.Items.Clear();
            foreach (DataRow row in foundItemData)
            {
                comboBox3.Items.Add(row["Size"]);
            }
            comboBox3.SelectedIndex = 0;
        }

        private DataRow[] FindData(string tablename, string query)
        {
            return DatabaseConnection.DatabaseRecord.Tables[tablename].Select(query);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertPrice();
        }

        private void InsertPrice()
        {
            
            var query =
                "select Price from Size inner join Items on " +
                "Size.ItemID=items.ItemID where Item_Name ='" +
                textBox4.Text + "' and Size='" +
                comboBox3.SelectedItem+"'";
            var productPrice = DatabaseConnection.GetCustomTable(query,"productPrice");
            textBox1.Text = productPrice.Rows[0][0].ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (!textBox4.Text.Equals(""))
                {
                    AddItemToGridView();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void AddItemToGridView()
        {
            var row = FindifProductExistForSale() ?? dataGridView2.Rows.Add();
                dataGridView2.Rows[row].Cells[0].Value = textBox4.Text;
                dataGridView2.Rows[row].Cells[1].Value = comboBox3.SelectedItem;
                dataGridView2.Rows[row].Cells[2].Value = textBox1.Text;
                dataGridView2.Rows[row].Cells[3].Value =Convert.ToInt32(dataGridView2.Rows[row].Cells[3].Value)
            + Convert.ToInt32(numericUpDown1.Text);
                dataGridView2.Rows[row].Cells[4].Value = CountTotal(dataGridView2.Rows[row]);
            
        }

        private int? FindifProductExistForSale()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(textBox4.Text)
                    && row.Cells[1].Value != null && row.Cells[1].Value.Equals(comboBox3.SelectedItem))
                {
                    return row.Index;
                }
            }
            return null;
        }

        private double CountTotal(DataGridViewRow Row)
        {
            var price = Convert.ToDouble(Row.Cells[2].Value);
            var quantity = Convert.ToInt32(Row.Cells[3].Value);
            return price*quantity;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                cueTextBox6.Focus();
                return true;
            }
            else if (keyData == Keys.F5)
            {
                LoadFromDatabase();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CountTotal(object sender, DataGridViewCellEventArgs e)
        {
            CountOverAllTotal();
        }

        private void CountOverAllTotal()
        {
            double total = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                total += Convert.ToDouble(row.Cells[4].Value);
            }

            textBox6.Text = total.ToString();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountChange();
            }
        }

        private void CountChange()
        {
            double total, payment, change;
            total = Convert.ToDouble(textBox6.Text);
            payment = Convert.ToDouble(textBox11.Text);
            change = payment - total;
            textBox2.Text = change.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SellItems();
        }

        private void SellItems()
        {
            RecordPurchasedItems();
        }

        private void RecordPurchasedItems()
        {
            int receiptID= CreateReceipt();
            int dateID = CreateDateId();
            var newReceiptRow= DatabaseConnection.DatabaseRecord.Tables["receiptID"].NewRow();
            newReceiptRow["receiptID"] = receiptID;
            newReceiptRow["dateID"] = dateID;
            DatabaseConnection.DatabaseRecord.Tables["receiptID"].Rows.Add(newReceiptRow);
            for (int index = 0; index < dataGridView2.Rows.Count-1; index++)
            {
                DataGridViewRow row = dataGridView2.Rows[index];
                int salesId = CreateSalesID();
                int sizeId = GetSizeID(row);
                var newSales = DatabaseConnection.DatabaseRecord.Tables["sale"].NewRow();
                newSales["saleID"] = salesId;
                newSales["receiptID"] = receiptID;
                newSales["count"] = Convert.ToInt32(row.Cells[3].Value);
                newSales["SizeID"] = sizeId;
                DatabaseConnection.DatabaseRecord.Tables["sale"].Rows.Add(newSales);
                LessInSales(sizeId, Convert.ToInt32(row.Cells[3].Value));
                DatabaseConnection.UploadChanges();
            }
            MessageBox.Show("transaction complete");
            ClearSales();
        }

        private void ClearSales()
        {
            foreach (var control in tabPage1.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                if (control is ComboBox)
                {
                    ((ComboBox)control).Items.Clear();
                }
                if (control is CueTextBox)
                {
                    ((CueTextBox)control).Clear();
                }
                
            }
        }

        private void LessInSales(int sizeId, int count)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["Size"].Select("SizeID ='"
                +sizeId+"'");
            var quantity = Convert.ToInt32(found[0]["quantity"].ToString());
            quantity -= count;
            found[0]["quantity"] = quantity.ToString();
            DatabaseConnection.UploadChanges();
        }

        private int GetSizeID(DataGridViewRow row)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_Name ='"
                +row.Cells[0].Value+"'");
            var itemID = found[0]["itemID"].ToString();
            found = DatabaseConnection.DatabaseRecord.Tables["size"].Select("ItemID ='" +
            itemID + "' and Size ='" + row.Cells[1].Value+"'");
            return Convert.ToInt32(found[0]["SizeID"]);
        }

        private int CreateSalesID()
        {
            var x = GetCurrentCount("sale","saleID");
            return x;
        }

        public int CreateDateId()
        {
            var x = GetCurrentCount("date","dateID");
            var dateTable = DatabaseConnection.DatabaseRecord.Tables["date"].NewRow();
            dateTable["DateID"] = x;
            dateTable["Date"] = DateTime.Now;
            DatabaseConnection.DatabaseRecord.Tables["date"].Rows.Add(dateTable);
            DatabaseConnection.UploadChanges();
            return x;
        }

        private int CreateReceipt()
        {
            var x = GetCurrentCount("receiptid","receiptid");
            return x;
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void DataGridDelete(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView2.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var manageOrder = new Manage_Orders();
            manageOrder.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SetItemFormVisibility();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            SetItemFormVisibility();
        }
    }
    }

