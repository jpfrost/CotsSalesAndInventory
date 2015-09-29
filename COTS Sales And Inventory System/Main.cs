using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Main : Form
    {
        private readonly string _accountType;
        private readonly List<Color> _colorlist = new List<Color>();
        private readonly Items _items = new Items();
        private readonly Form _loginForm;
        private readonly string _username;
        private readonly Stopwatch timer = new Stopwatch();
        private int _critCount = 0;
        private DataTable _criticalTable;
        private string groupQuery = "";
        private string recordedInput;
        private string rtextboxData;
        private double totalSold;
        private int x;

        public Main(Form loginForm, string username, string accountType)
        {
            _loginForm = loginForm;
            _username = username;
            _accountType = accountType;
            InitializeComponent();
            Hide();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
            LoadDummyReport();
            toolStripStatusLabel1.Text = "Current User: " + _username;
            toolStripStatusLabel2.Text = "User Rights: " + _accountType;
            _colorlist.Add(Color.Red);
            _colorlist.Add(Color.Black);
            var x = Task.Run(() => { LoadData(); });
            x.Wait();
            panel1.Controls.Add(_items);
            _items.Hide();
            LoadDefaults();
            dateTime.Start();
            timerDataRefresh.Start();
            comboBox4.SelectedIndex = 0;
            LoadAccountSettings();
            Show();
        }

        private void LoadDummyReport()
        {
            var report = new Print_Receipt(new DataSet());
            report.Dispose();
        }

        private void LoadAccountSettings()
        {
            accountsToolStripMenuItem.Enabled = false;
            configToolStripMenuItem.Enabled = false;
            switch (_accountType)
            {
                case "Sales Person":
                    mainTab.TabPages.Remove(tabPage2);
                    mainTab.TabPages.Remove(tabPage3);
                    break;
                case "Sales Manager":
                    mainTab.TabPages.Remove(tabPage2);
                    tabControl2.TabPages.Remove(tabPage5);
                    break;
                case "Stock Man":
                    mainTab.TabPages.Remove(tabPage1);
                    mainTab.TabPages.Remove(tabPage3);
                    break;
                case "Stock Manager":
                    mainTab.TabPages.Remove(tabPage1);
                    tabControl2.TabPages.Remove(tabPage4);
                    break;
                case "Admin":
                    accountsToolStripMenuItem.Enabled = true;
                    configToolStripMenuItem.Enabled = true;
                    break;
            }
        }

        private void LoadDefaults()
        {
            comboBox2.Text = Properties.Settings.Default.DefaultSupplier;
            textBox10.Text = Properties.Settings.Default.DefaultSupplierNo;
            textBox9.Text = Properties.Settings.Default.DefaultSupplierAddress;
            cueTextBox5.Enabled = Properties.Settings.Default.SalesDiscount;
            comboBox5.SelectedIndex = 0;
        }

        private void CriticalItemShow()
        {
            label3.Text = "";
        }

        private void ChangeLabelText(DataRow dataRow)
        {
            label3.Text = dataRow["product"] + " " + dataRow["size"] + " only have " + dataRow["quantity"]
                          + " left please order immediately";
            InsertToSummary(label3.Text);
        }

        private void InsertToSummary(string text)
        {
            var index = richTextBox1.Find(text);
            if (index == -1)
            {
                richTextBox1.AppendText(text + Environment.NewLine);
            }
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
            ClearCategory();
            FillCategoryListBox();
            FillCategoryComboBox();
            LoadFromDatabase();
            LoadCriticalLevel();
            CriticalItemShow();
        }

        private void LoadCriticalLevel()
        {
            _criticalTable = DatabaseConnection.GetCustomTable(
                "select Item_Name as 'Product',Size,quantity,itemMinLevel " +
                "from items inner join size on items.ItemID=size.ItemID " +
                "inner join itemlevel on itemlevel.SizeID=size.SizeID " +
                "where quantity <= itemMinLevel;", "criticalItems");
        }

        private void LoadFromDatabase()
        {
            BeginInvoke(new Action(FillInventory));
        }

        private void FillInventory()
        {
            var dt =
                DatabaseConnection.GetCustomTable(
                    "select Item_Name as 'Product', Size, Price, Quantity, CategoryName as 'Category' from items inner join size on items.ItemID=size.ItemID inner join category on items.CategoryID=category.CategoryID where Quantity > 0 and price <> \"\";"
                    , "SizeAndItemTable");
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
            if (!comboBox1.Text.Equals(""))
            {
                AddCategory();
                LoadData();
            }
            else
            {
                MessageBox.Show("Please Enter a Category Name\n Before you click Add Category");
            }
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
            value =
                (from DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows
                    select (int) rows[columbName]).Concat(new[] {value}).Max();
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
            foreach (var rows in found)
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
                InsertNewDistro(distroId, distroName, distroEmail, distroNumber);
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
            catch (Exception aException)
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
            RefreshData();
        }

        private void AddAutoCompleteForSalesTextBox()
        {
            var autoCompleteCollectionProductName = new AutoCompleteStringCollection();
            for (int x = 0; x < dataGridView1.Rows.Count;x++)
            {
                var itemName = dataGridView1.Rows[x].Cells[0].Value.ToString();
                if (!autoCompleteCollectionProductName.Contains(itemName))
                {
                    autoCompleteCollectionProductName.Add(itemName);
                }
            }
            //iniba ko para sa datagrid gagawa ng auto complete
            /*foreach (DataRow dr in DatabaseConnection.DatabaseRecord.Tables["items"].Rows)
            {
                autoCompleteCollectionProductName.Add(dr["Item_Name"].ToString());
            }*/
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteCustomSource = autoCompleteCollectionProductName;
            cueTextBox2.AutoCompleteSource=AutoCompleteSource.CustomSource;
            cueTextBox2.AutoCompleteCustomSource = autoCompleteCollectionProductName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _loginForm.Show();
            Dispose();
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
                                                                                     + cueTextBox6.Text + "'");
                textBox4.Text = found[0]["Item_Name"].ToString();
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

        private void ReadKeyInput()
        {
            timer.Stop();
            BeginInvoke(new Action(InsertToProductCode));
        }

        private void InsertToProductCode()
        {
            if (timer.ElapsedMilliseconds <= 70 && !textBox4.Text.Equals(""))
            {
                try
                {
                    cueTextBox6.Text = recordedInput;
                    FindProductName();
                    LoadProductInfo();
                    AddItemToGridView();
                    textBox4.Clear();
                    comboBox3.Items.Clear();
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
            if (e.KeyCode == Keys.Enter)
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
            if (e.KeyCode == Keys.Enter)
            {
                LoadProductInfo();
                button11.Focus();
            }
        }

        private void LoadProductInfo()
        {
            try
            {
                var tablename = "items";
                var query = "Item_Name ='" + textBox4.Text + "'";
                var foundItemData = FindData(tablename, query);
                var itemID = foundItemData[0]["ItemID"].ToString();
                var foundItemSize = FindData("size", "itemID ='" + itemID + "'");
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
            foreach (var row in foundItemData)
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
                comboBox3.SelectedItem + "'";
            var productPrice = DatabaseConnection.GetCustomTable(query, "productPrice");
            textBox1.Text = productPrice.Rows[0][0].ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void AddItem()
        {
            if (textBox4.Text == "" && comboBox3.Text == "")
            {
                MessageBox.Show("Please fill out all the Product Information", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var size = FindSizeID(textBox4.Text, comboBox3.Text);
                    if (Convert.ToInt16(size["Quantity"]) < Convert.ToInt16(numericUpDown1.Text))
                    {
                        MessageBox.Show("quantity exceed stocks", "Oppppps...", MessageBoxButtons.OK,
                            MessageBoxIcon.Stop);
                    }
                    else
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
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private DataRow FindSizeID(string item, string size)
        {
            var itemId = DatabaseConnection.DatabaseRecord.Tables["items"].Select("item_name ='"
                                                                                  + item + "'");
            var sizeData = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='"
                                                                                   + itemId[0]["itemID"] +
                                                                                   "' and size ='" + size + "'");

            return sizeData[0];
        }

        private void AddItemToGridView()
        {
            var row = FindifProductExistForSale() ?? dataGridView2.Rows.Add();
            var purchaseQuantity = Convert.ToInt32(dataGridView2.Rows[row].Cells[3].Value) +
                                   Convert.ToInt32(numericUpDown1.Text);
            var stockQuantity = FindStockQuantity();
            if (purchaseQuantity <= stockQuantity)
            {
                dataGridView2.Rows[row].Cells[0].Value = textBox4.Text;
                dataGridView2.Rows[row].Cells[1].Value = comboBox3.SelectedItem;
                dataGridView2.Rows[row].Cells[2].Value = textBox1.Text;
                dataGridView2.Rows[row].Cells[3].Value = Convert.ToInt32(dataGridView2.Rows[row].Cells[3].Value)
                                                         + Convert.ToInt32(numericUpDown1.Text);
                dataGridView2.Rows[row].Cells[4].Value = CountTotal(dataGridView2.Rows[row]);
            }
            else
            {
                MessageBox.Show("Cannot Exceed items quantity",
                    "Purchase Quantity Exceed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
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

        private int FindStockQuantity()
        {
            var itemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("item_name ='"
                                                                                  + textBox4.Text + "'");
            var size = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemid ='"
                                                                               + itemID[0]["itemId"] + "' and size ='" +
                                                                               comboBox3.Text + "'");
            var x = Convert.ToInt32(size[0]["Quantity"]);
            return x;
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
            if (keyData == Keys.F2)
            {
                textBox4.Focus();
                return true;
            }
            if (keyData == Keys.F3)
            {
                numericUpDown1.Focus();
                return true;
            }
            if (keyData == Keys.F5)
            {
                LoadFromDatabase();
                RefreshData();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CountTotal(object sender, DataGridViewCellEventArgs e)
        {
            CountOverAllTotal();
        }

        private double _totalPayment;
        private void CountOverAllTotal()
        {
            _totalPayment = 0;
            double total = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                total += Convert.ToDouble(row.Cells[4].Value);
            }
            _totalPayment = total;
            textBox6.Text = (string.Format("{0:0.00}", total));
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox11.Text = (string.Format("{0:0.00}", Convert.ToDouble(textBox11.Text)));
                CountChange();
            }
        }

        private void CountChange()
        {
            var total = Convert.ToDouble(textBox6.Text);
            var payment = Convert.ToDouble(textBox11.Text);
            var change = payment - total;
            if (change < 0)
            {
                MessageBox.Show("Insufficient Payment");
            }
            else
            {
                textBox2.Text = (string.Format("{0:0.00}", change));
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            if (textBox11.Text.Equals(""))
            {
                textBox11.Text = textBox6.Text;
            }
            if (textBox2.Text.Equals(""))
            {
                CountChange();
            }
            SellItems();
            ClearSellDatagrid();
            button12.Enabled = true;
        }

        private void ClearSellDatagrid()
        {
            dataGridView2.Rows.Clear();
            foreach (Control cont in groupBox6.Controls)
            {
                if (cont is TextBox)
                {
                    cont.Text = "";
                }
                if (cont is CueTextBox)
                {
                    cont.Text = "";
                }
                var box = cont as ComboBox;
                if (box != null)
                {
                    box.Items.Clear();
                }
            }
        }

        private void SellItems()
        {
            RecordPurchasedItems();
        }

        private void RecordPurchasedItems()
        {
            var receiptID = CreateReceipt();
            var dateID = CreateDateId();
            var receiptDataset = CreateReceiptDataset();
            var newReceiptRow = DatabaseConnection.DatabaseRecord.Tables["receiptID"].NewRow();
            newReceiptRow["receiptID"] = receiptID;
            newReceiptRow["dateID"] = dateID;
            DatabaseConnection.DatabaseRecord.Tables["receiptID"].Rows.Add(newReceiptRow);
            for (var index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var row = dataGridView2.Rows[index];
                var salesId = CreateSalesId();
                var sizeId = GetSizeId(row);
                var newSales = DatabaseConnection.DatabaseRecord.Tables["sale"].NewRow();
                newSales["saleID"] = salesId;
                newSales["receiptID"] = receiptID;
                newSales["count"] = Convert.ToInt32(row.Cells[3].Value);
                newSales["SizeID"] = sizeId;
                DatabaseConnection.DatabaseRecord.Tables["sale"].Rows.Add(newSales);
                LessInSales(sizeId, Convert.ToInt32(row.Cells[3].Value));
                DatabaseConnection.UploadChanges();
                InsertToReceiptDataset(receiptDataset, row);
            }
            InsertTranscationToReceipt(receiptDataset, receiptID);
            if (File.Exists("receipt.xml"))
            {
                File.Delete("receipt.xml");
            }
            receiptDataset.WriteXml("receipt.xml");
            if (Properties.Settings.Default.SalesReceipt)
            {
                PrintTransaction(receiptDataset);
            }
            else
            {
                MessageBox.Show("Sales Transaction Complete");
            }
            ClearSales();
            WriteToSummary(receiptDataset);
        }

        private void WriteToSummary(DataSet receiptDataset)
        {
            foreach (DataRow row in receiptDataset.Tables["SoldItems"].Rows)
            {
                var line = row["item"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["Quantity"] + " is sold for "
                           + string.Format("{0:0.00}", row["Total"]) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                totalSold +=
                    Convert.ToDouble(row["total"]);
                rtextboxData = richTextBox2.Text;
                textBox3.Text = (string.Format("{0:0.00}", totalSold));
            }
        }

        private void InsertTranscationToReceipt(DataSet receiptDataset, int receiptId)
        {
            var newMiscRow = receiptDataset.Tables["ReceiptInfo"].NewRow();
            newMiscRow["receiptID"] = receiptId;
            newMiscRow["date"] = DateTime.Now.ToString("dd-MM-yyyy");
            newMiscRow["payment"] = Convert.ToDouble(textBox11.Text);
            newMiscRow["change"] = Convert.ToDouble(textBox2.Text);
            newMiscRow["tax"] = Convert.ToDouble(CalculateTax());
            newMiscRow["vatSale"] = Convert.ToDouble(CalculateVatSale());
            newMiscRow["storeName"] = Properties.Settings.Default.storeName;
            newMiscRow["storeAddress"] = Properties.Settings.Default.storeAdd;
            newMiscRow["storeNo"] = Properties.Settings.Default.storeNo;
            newMiscRow["grandTotal"] = textBox6.Text;
            receiptDataset.Tables["ReceiptInfo"].Rows.Add(newMiscRow);
        }

        private string CalculateVatSale()
        {
            var x = Convert.ToDouble(textBox6.Text);
            var y = (100-Convert.ToDouble(Properties.Settings.Default.SalesTax))/100;
             return string.Format("{0:0.00}",x*y);
        }

        private string CalculateTax()
        {
            var x = Convert.ToDouble(textBox6.Text);
            var y = Convert.ToDouble(Properties.Settings.Default.SalesTax);
            var z = x*(y/100);
            return string.Format("{0:0.00}",z);
        }

        private void PrintTransaction(DataSet receiptDataset)
        {
            var print = new Print_Receipt(receiptDataset);
            print.Show();
        }

        private void InsertToReceiptDataset(DataSet receiptDataset, DataGridViewRow row)
        {
            var newSalesRow = receiptDataset.Tables["SoldItems"].NewRow();
            newSalesRow["item"] = row.Cells[0].Value.ToString();
            newSalesRow["size"] = row.Cells[1].Value.ToString();
            newSalesRow["price"] = Convert.ToDouble(row.Cells[2].Value);
            newSalesRow["Quantity"] = Convert.ToInt32(row.Cells[3].Value);
            newSalesRow["Total"] = Convert.ToDouble(row.Cells[2].Value)
                                   *Convert.ToInt32(row.Cells[3].Value);
            receiptDataset.Tables["SoldItems"].Rows.Add(newSalesRow);
        }

        private DataSet CreateReceiptDataset()
        {
            var ds = new DataSet();
            var dt = new DataTable("SoldItems");
            dt.Columns.Add("item", typeof (string));
            dt.Columns.Add("size", typeof (string));
            dt.Columns.Add("Quantity", typeof (Int32));
            dt.Columns.Add("price", typeof (Double));
            dt.Columns.Add("Total", typeof (Double));
            ds.Tables.Add(dt);
            dt = new DataTable("ReceiptInfo");
            dt.Columns.Add("receiptID", typeof (Int32));
            dt.Columns.Add("date", typeof (DateTime));
            dt.Columns.Add("payment", typeof (Double));
            dt.Columns.Add("change", typeof (Double));
            dt.Columns.Add("tax", typeof(Double));
            dt.Columns.Add("vatSale", typeof(Double));
            dt.Columns.Add("storeName", typeof(String));
            dt.Columns.Add("storeAddress", typeof(String));
            dt.Columns.Add("storeNo", typeof(String));
            dt.Columns.Add("grandTotal", typeof (String));
            ds.Tables.Add(dt);
            
            return ds;
        }

        private void ClearSales()
        {
            foreach (var control in tabPage1.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox) control).Clear();
                }
                if (control is ComboBox)
                {
                    ((ComboBox) control).Items.Clear();
                }
                if (control is CueTextBox)
                {
                    ((CueTextBox) control).Clear();
                }
            }
        }

        private void LessInSales(int sizeId, int count)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["Size"].Select("SizeID ='"
                                                                                + sizeId + "'");
            var quantity = Convert.ToInt32(found[0]["quantity"].ToString());
            quantity -= count;
            found[0]["quantity"] = quantity.ToString();
            DatabaseConnection.UploadChanges();
        }

        private int GetSizeId(DataGridViewRow row)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_Name ='"
                                                                                 + row.Cells[0].Value + "'");
            var itemID = found[0]["itemID"].ToString();
            found = DatabaseConnection.DatabaseRecord.Tables["size"].Select("ItemID ='" +
                                                                            itemID + "' and Size ='" +
                                                                            row.Cells[1].Value + "'");
            return Convert.ToInt32(found[0]["SizeID"]);
        }

        private int CreateSalesId()
        {
            var x = GetCurrentCount("sale", "saleID");
            return x;
        }

        public int CreateDateId()
        {
            var x = GetCurrentCount("date", "dateID");
            var dateTable = DatabaseConnection.DatabaseRecord.Tables["date"].NewRow();
            dateTable["DateID"] = x;
            dateTable["Date"] = DateTime.Now;
            DatabaseConnection.DatabaseRecord.Tables["date"].Rows.Add(dateTable);
            DatabaseConnection.UploadChanges();
            return x;
        }

        private int CreateReceipt()
        {
            var x = GetCurrentCount("receiptid", "receiptid");
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
                var dialog = MessageBox.Show("Are you sure you want to remove item",
                    "Remove item",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
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
            if (!cueTextBox2.Text.Equals(""))
            {
                _items.InsertProductName(cueTextBox2.Text);
            }
            SetItemFormVisibility();
        }

        private void label25_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            CreateOrders();
        }

        private void CreateOrders()
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangeLabelColor();
        }

        private void ChangeLabelColor()
        {
            label3.ForeColor = label3.ForeColor == Color.Red ? Color.Black : Color.Red;
        }

        private void textChangeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ChangeLabelText(_criticalTable.Rows[x]);
                x++;
                if (x >= _criticalTable.Rows.Count) x = 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
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

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox1.Clear();
            comboBox3.Items.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _loginForm.Show();
            Dispose();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_accountType.Equals("Admin"))
            {
                var admin = new FrmAdmin(this);
                admin.Show();
            }
            else
            {
                MessageBox.Show("You dont have admin rights", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeSalesSummaryDisplay();
        }

        private void ChangeSalesSummaryDisplay()
        {
            richTextBox2.Text = "";
            textBox3.Text = "0.00";
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    DisplayCurrentSales();
                    break;
                case 1:
                    DisplayCurrentDaySales();
                    break;
                case 2:
                    DisplaySelectedDaySale();
                    break;
                case 3:
                    DisplaySelectedWeek();
                    break;
                case 4:
                    DisplaySelectedMonth();
                    break;
                case 5:
                    DisplaySelectedYear();
                    break;
                case 6:
                    DisplayAllSales();
                    break;
            }
        }

        private void DisplayAllSales()
        {
            var query = ("select Item_Name,size,count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID  " + groupQuery);
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0.00}", total);
        }

        private void DisplaySelectedYear()
        {
            var dateselected = summaryDatePicker.Value.ToString("yy");
            var query = ("select Item_Name,size,count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "where date like '%" + dateselected + "%' " + groupQuery);
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0.00}", total);
        }

        private void DisplaySelectedMonth()
        {
            var dateselected = summaryDatePicker.Value.ToString("yy-MM");
            var query = ("select Item_Name,size,count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "where date like '%" + dateselected + "%' order by date  " + groupQuery);
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0.00}", total);
        }

        private void DisplaySelectedWeek()
        {
            var dateselected = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "WHERE date > DATE_SUB('" + dateselected + "', INTERVAL 1 WEEK)  " + groupQuery);
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0.00}", total);
        }

        private void DisplaySelectedDaySale()
        {
            var dateToday = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "where date like '%" + dateToday + "%' " + groupQuery);
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0.00}", totalSold) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0.00}", total);
        }

        private void DisplayCurrentDaySales()
        {
            var dateToday = DateTime.Now.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "where date like '%" + dateToday + "%' " + groupQuery);
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0.00}", totalSold) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0.00}", total);
        }

        private void DisplayCurrentSales()
        {
            richTextBox2.Text = rtextboxData;
            textBox3.Text = (string.Format("{0:0.00}", totalSold));
        }

        private void summaryDatePicker_ValueChanged(object sender, EventArgs e)
        {
            ChangeSalesSummaryDisplay();
        }

        private void btnPrintSummarySales_Click(object sender, EventArgs e)
        {
            btnPrintSummarySales.Enabled = false;
            PrintSummary();
            btnPrintSummarySales.Enabled = true;
        }

        private void PrintSummary()
        {
            var summaryReport = CreateSummarySalesReport();
            InsertSummaryData(summaryReport);
            summaryReport.WriteXml("summaryReport.xml");
            var print = new PrintSalesSummary(summaryReport);
            print.Show();
        }

        private void InsertSummaryData(DataSet summaryReport)
        {
            foreach (var textLine in richTextBox2.Lines)
            {
                var summaryLine = summaryReport.Tables["SalesLine"].NewRow();
                summaryLine["textLine"] = textLine;
                summaryReport.Tables["SalesLine"].Rows.Add(summaryLine);
            }
            var miscReportData = summaryReport.Tables["SummaryData"].NewRow();
            miscReportData["date"] = summaryDatePicker.Value.ToString("dd-MM-yyyy");
            miscReportData["mode"] = comboBox4.Text;
            miscReportData["total"] = textBox3.Text;
            summaryReport.Tables["SummaryData"].Rows.Add(miscReportData);
        }

        private DataSet CreateSummarySalesReport()
        {
            var ds = new DataSet();
            var dt = new DataTable("SalesLine");
            dt.Columns.Add("textLine", typeof (string));
            ds.Tables.Add(dt);
            dt = new DataTable("SummaryData");
            dt.Columns.Add("date", typeof (string));
            dt.Columns.Add("mode", typeof (string));
            dt.Columns.Add("total", typeof (string));
            ds.Tables.Add(dt);
            return ds;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupQuery = "";
            }
            else
            {
                groupQuery = "";
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new Settings();
            settings.Show();
        }

        private void cueTextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BeginInvoke(new Action(ComputeDiscount));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ComputeDiscount()
        {
            try
            {
                var x= Convert.ToDouble(cueTextBox5.Text);
                var total = _totalPayment;

                if (x < 100.00)
                {
                    x /= 100.00;
                    total *= x;
                    var stringTotal =(string.Format("{0:0.00}", total));
                    cueTextBox3.Text = stringTotal;
                    textBox6.Text = (string.Format("{0:0.00}", _totalPayment-total));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private void cueTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                InsertProductNametoCuetextBox2();
            }
        }

        private void InsertProductNametoCuetextBox2()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("itemID ='"+cueTextBox1.Text+"'");
            if (found.Length>0)
            {
                cueTextBox2.Text = found[0]["item_Name"].ToString();
            }
        }

        private void cueTextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }
    }
}