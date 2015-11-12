using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using COTS_Sales_And_Inventory_System.Properties;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using ListBox = System.Windows.Forms.ListBox;
using TextBox = System.Windows.Forms.TextBox;

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
        private DataTable _itemWithNoPrice;
        private double _totalPayment;
        private string groupQuery = "";
        private string recordedInput;
        private string rtextboxData;
        private double totalSold;
        private int x;
        private int y;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
        public Main(Form loginForm, string username, string accountType)
        {
            _loginForm = loginForm;
            _username = username;
            _accountType = accountType;
            InitializeComponent();
        }

        

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private Loading_Screen _loading=new Loading_Screen();
        private void Main_Load(object sender, EventArgs e)
        {
            Hide();
            backgroundLoadingScreen.RunWorkerAsync();
            _loading.ShowDialog();
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

        private void LoadSettings()
        {
            comboBox2.Text = Settings.Default.DefaultSupplier;
            textBox10.Text = Settings.Default.DefaultSupplierNo;
            textBox9.Text = Settings.Default.DefaultSupplierAddress;
            cueTxtDiscount.Enabled = Settings.Default.SalesDiscount;
            button13.Enabled = Settings.Default.EnableOrdering;
            button10.Enabled = Settings.Default.AllowMultiSupplier;
            button8.Enabled = Settings.Default.AllowMultiSupplier;
            if (Settings.Default.EnCompanyLogo)
            {
                try
                {
                    pictureBox1.ImageLocation = @"logo.png";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (!Settings.Default.AllowMultiSupplier)
            {
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                textBox10.Enabled = false;
                textBox9.Enabled = false;
            }
            comboBox5.SelectedIndex = 0;
        }

        private void CriticalItemShow()
        {
            label3.Text = "";
        }

        private void ChangeCritLevelText(DataRow dataRow)
        {
            label3.Text = dataRow["product"] + " " + dataRow["size"] + " only have " + dataRow["quantity"]
                          + " left please order immediately";
            InsertToSummary(label3.Text,richTextBox1);
        }

        private void ChangeItemWithNoPriceText(DataRow dataRow)
        {
            label20.Text = dataRow["item_Name"] + " " +
                           dataRow["size"] + " has no price yet...";
            InsertToSummary(label20.Text,richTextBox3);
        }

        private void InsertToSummary(string text,RichTextBox richtext)
        {
            BeginInvoke(new Action(() =>
            {
                var index = richtext.Find(text);
                if (index == -1)
                {
                    richtext.AppendText(text + Environment.NewLine);
                }
            }));
        }

        private void LoadData()
        {
            LoadInventory();
        }

        private void LoadInventory()
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
            LoadItemWithNoPrice();
            FillSummary();
            CriticalItemShow();
        }

        private void FillSummary()
        {
            richTextBox1.Clear();
            richTextBox3.Clear();
            foreach (DataRow row in _criticalTable.Rows)
            {
               var x= row["product"] + " " + row["size"] + " only have " + row["quantity"]
                          + " left please order immediately";
               InsertToSummary(x,richTextBox1);
            }
            foreach (DataRow row in _itemWithNoPrice.Rows)
            {
                var x = row["item_Name"] + " " +
                           row["size"] + " has no price yet...";
                InsertToSummary(x, richTextBox3);
            }
        }

        private void LoadItemWithNoPrice()
        {
            _itemWithNoPrice =
                DatabaseConnection.GetCustomTable(
                    "select Item_Name, Size from items inner join size on items.ItemID=size.ItemID where price is Null;",
                    "nullPrices");
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
            BeginInvoke(new Action(() => FillInventory()));
        }

        private void FillInventory()
        {
            var dt =
                DatabaseConnection.GetCustomTable(
                    "select Item_Name as 'Product', Size, FORMAT(Price,2) as Price, Quantity, CategoryName as 'Category' from items inner join size on items.ItemID=size.ItemID inner join category on items.CategoryID=category.CategoryID where sizeEnable ='1' and Quantity > 0 and price <> \"\";"
                    , "SizeAndItemTable");
            invetoryGridView.DataSource = dt;
            invetoryGridView.Update();
            invetoryGridView.Refresh();
            AddAutoCompleteForSalesTextBox();
        }

        private void FillCategoryComboBox()
        {
            foreach (var catName in listBox1.Items)
            {
                comboBox1.Items.Add(catName);
            }
            if (!Settings.Default.AllowMultiSupplier) return;
            foreach (
                DataRow rows in
                    DatabaseConnection.DatabaseRecord.Tables["distributor"].Select("distroEnable ='" + 1 + "'"))
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
            listBox2.BeginUpdate();
            listBox2.Items.Clear();
            listBox1.Items.Clear();
            listBox1.Items.Add("All");
            listBox2.Items.Add("All");
            foreach (DataRow rows in DatabaseConnection.DatabaseRecord.Tables["category"].Rows)
            {
                if (!listBox1.Items.Contains(rows["categoryName"].ToString()))
                {
                    listBox1.Items.Add(rows["categoryName"].ToString());
                    listBox2.Items.Add(rows["categoryName"].ToString());
                }
            }
            listBox1.EndUpdate();
            listBox2.EndUpdate();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!comboBox1.Text.Equals("")&& !comboBox1.Text.Equals("All") && !String.IsNullOrWhiteSpace(comboBox1.Text))
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
            var newCategory = false;
            var catPk = GetCurrentCount("category", "CategoryID");
            var catName = comboBox1.Text;
            var found = FindRow("category", "categoryName = '" + catName + "'");
            if (found.Length == 0)
            {
                InsertNewCategory(catPk, catName);
                newCategory = true;
            }
            var dialog = new CategoryUnits(comboBox1.Text);
            if (dialog.ShowDialog() == DialogResult.OK && newCategory==false)
            {
                MessageBox.Show(catName + " has been modified", "Category Modified", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            if (newCategory)
            {
                MessageBox.Show("Added Category " + catName, "Category Added", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            comboBox1.Text = "";
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
        }

        private void ClearCategory()
        {
            try
            {
                listBox1.Items.Clear();
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
        {       var del = comboBox1.Text;
            if (!del.Equals("All"))
            {

                var found = FindRow("category", "categoryName ='" + del + "'");
                RemoveCategoryFromItems(del);
                DisableItemInComboBox(comboBox1, found);
                DatabaseConnection.GetCustomTable("DELETE FROM category WHERE `CategoryName`='" + del + "'", "test");
                MessageBox.Show(del + @" has been deleted");
                DatabaseConnection.UploadChanges();
                listBox1.Items.Remove(del);
                comboBox1.Items.Remove(del);
            }
            else
            {
                MessageBox.Show("Cannot Remove All");
            }
        }

        

        private void RemoveCategoryFromItems(string del)
        {
            var categoryRow = DatabaseConnection.DatabaseRecord.Tables["category"].Select("CategoryName ='"+del+"'");
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("CategoryID ='"+categoryRow[0]["categoryID"]+"'");
            foreach (DataRow row in found)
            {
                row["CategoryID"] = DBNull.Value;
            }
            
            DatabaseConnection.UploadChanges();
        }

        private void DisableItemInComboBox(ComboBox cb, DataRow[] found)
        {
            foreach (var rows in found)
            {
                try
                {
                    rows["distroEnable"] = 0;
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            DatabaseConnection.UploadChanges();
            cb.Text = "";
            RefreshData();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddDistributor();
        }

        private void AddDistributor()
        {

            if (!(comboBox2.Text.Equals("") || textBox10.Text.Equals("") || textBox9.Text.Equals("") ||
                  string.IsNullOrWhiteSpace(comboBox2.Text) || string.IsNullOrWhiteSpace(textBox10.Text) ||
                  string.IsNullOrWhiteSpace(textBox9.Text)))
            {
                var distroId = GetCurrentCount("distributor", "DistroID");
                var distroName = comboBox2.Text;
                var distroEmail = textBox9.Text;
                var distroNumber = textBox10.Text;
                var found = FindRow("distributor", "DistroName = '" + distroName + "'");
                if (found.Length > 0)
                {
                    if (found[0]["distroEnable"] == DBNull.Value || Convert.ToInt32(found[0]["distroEnable"]) == 0)
                    {
                        found[0]["distroEnable"] = 1;
                        DatabaseConnection.UploadChanges();
                        MessageBox.Show("Found Existing Distributor It will be Enabled");
                    }
                    else
                    {
                        MessageBox.Show(@"Distro Exist this distributor will not be created", @"Distributor Exist");
                    }
                }
                else
                {
                    InsertNewDistro(distroId, distroName, distroEmail, distroNumber);
                }
            }
            else
            {
                MessageBox.Show("Please Fill a the suppliers form","Enter Required field",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void InsertNewDistro(int distroId, string distroName, string distroEmail, string distroNumber)
        {
            var newDistro = DatabaseConnection.DatabaseRecord.Tables["distributor"].NewRow();
            newDistro[0] = distroId;
            newDistro[1] = distroName;
            newDistro[2] = distroEmail;
            newDistro[3] = distroNumber;
            newDistro[4] = 1;
            DatabaseConnection.DatabaseRecord.Tables["distributor"].Rows.Add(newDistro);
            DatabaseConnection.UploadChanges();
            MessageBox.Show(@"New distributor added...");
            RefreshData();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var del = comboBox2.SelectedItem;
            var found = FindRow("distributor", "DistroName ='" + del + "'");
            DisableItemInComboBox(comboBox2, found);
            textBox9.Text = "";
            textBox10.Text = "";
            MessageBox.Show(@"Distributor has been disabled");
            RefreshData();
        }

        private void ShowHideInventory(object sender, EventArgs e)
        {
            if (mainTab.SelectedIndex == 1 || mainTab.SelectedIndex == 0 || tabControl2.SelectedIndex==1)
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
            if (!invetoryGridView.Visible) return;
            invetoryGridView.Visible = false;
        }

        private void ShowInventoryGrid()
        {
            if (invetoryGridView.Visible) return;
            invetoryGridView.Visible = true;
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
            if (Settings.Default.EmailSendMessage)
            {
                SendReportToEmail();
            }
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
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
            for (var x = 0; x < invetoryGridView.Rows.Count; x++)
            {
                var itemName = invetoryGridView.Rows[x].Cells[0].Value.ToString();
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
            txtBoxProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBoxProductName.AutoCompleteCustomSource = autoCompleteCollectionProductName;
            cueTextBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cueTextBox2.AutoCompleteCustomSource = autoCompleteCollectionProductName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Settings.Default.EmailSendMessage)
            {
                SendReportToEmail();
            }
            _loginForm.Show();
            Dispose();
        }

        private void SendReportToEmail()
        {
            var emailAcc = Settings.Default.EmailUser;
            var emailPass = Settings.Default.EmailPassword;
            var subject = "Session Report/Total Sales";
            var body = CreateEmailReportBody();
            var email = new Email(emailAcc, emailPass, subject, body);
            email.Send();
        }

        private string CreateEmailReportBody()
        {
            try
            {
                comboBox4.SelectedIndex = 0;
                checkBox1.Checked = true;
            }
            catch (Exception e)
            {
                //ignored
            }
            var body = new StringBuilder();
            body.Append(richTextBox2.Text);
            body.Append("\n\n\n");
            body.Append(@"Total Cash: "+textBox3.Text);

            return body.ToString();
        }

        private string ConvertReportToPDF()
        {
            var summaryReport = CreateSummarySalesReport();
            InsertSummaryData(summaryReport);
            summaryReport.WriteXml("summaryReport.xml");
            var saleSum = new SalesSummary();
            saleSum.SetDataSource(summaryReport);
            try
            {
                ExportOptions crOptions;
                DiskFileDestinationOptions crDestinationOptions=new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions pdfRtf=new PdfRtfWordFormatOptions();
                crDestinationOptions.DiskFileName = @"saleSummary.pdf";
                crOptions = saleSum.ExportOptions;
                {
                    crOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    crOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    crOptions.DestinationOptions = crDestinationOptions;
                    crOptions.FormatOptions = pdfRtf;
                }
                saleSum.Export();
                return @"saleSummary.pdf";
            }
            catch (Exception e)
            {
                //ignore
            }
            return null;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox1.SelectedItem = listBox1.SelectedItem.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            FilterInvetoryByCategory(listBox1);
        }

        private void FilterInvetoryByCategory(ListBox listBox)
        {
            try
            {
                if (!listBox.SelectedItem.Equals("All"))
                {
                    ((DataTable) invetoryGridView.DataSource).DefaultView.RowFilter = ("Category ='"
                                                                                    + listBox.SelectedItem + "'");
                }
                else
                {
                    ((DataTable) invetoryGridView.DataSource).DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            invetoryGridView.DoDragDrop(invetoryGridView.CurrentCell.Value.ToString(), DragDropEffects.Copy);
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.Text) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void DragDrop(object sender, DragEventArgs e)
        {
            txtBoxProductName.Text = e.Data.GetData(DataFormats.Text) as string;
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
                                                                                     + cuetxtBoxProductCode.Text + "'");
                txtBoxProductName.Text = found[0]["Item_Name"].ToString();
                cuetxtBoxProductCode.Text = "";
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
            if (timer.ElapsedMilliseconds <= 70 && !txtBoxProductName.Text.Equals(""))
            {
                try
                {
                    cuetxtBoxProductCode.Text = recordedInput;
                    FindProductName();
                    LoadProductInfo();
                    AddItemToGridView();
                    txtBoxProductName.Clear();
                    cBoxSize.Items.Clear();
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
                btnAdd.Focus();
            }
        }

        private void LoadProductInfo()
        {
            try
            {
                var tablename = "items";
                var query = "Item_Name ='" + txtBoxProductName.Text + "'";
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
            cBoxSize.Items.Clear();
            foreach (var row in foundItemData)
            {
                cBoxSize.Items.Add(row["Size"]);
            }
            cBoxSize.SelectedIndex = 0;
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
                txtBoxProductName.Text + "' and Size='" +
                cBoxSize.SelectedItem + "'";
            var productPrice = DatabaseConnection.GetCustomTable(query, "productPrice");
            textBox1.Text = productPrice.Rows[0][0].ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddItem();
            txtBoxProductName.Text = "";
            cBoxSize.Items.Clear();
        }

        private void AddItem()
        {
            if (txtBoxProductName.Text == "" && cBoxSize.Text == "")
            {
                MessageBox.Show("Please fill out all the Product Information", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
               
                    try
                    {
                        

                        var size = FindSizeID(txtBoxProductName.Text, cBoxSize.Text);
                        if (Convert.ToInt16(size["Quantity"]) < Convert.ToInt16(numUpQuantity.Text))
                        {
                            MessageBox.Show("quantity exceed stocks", "Oppppps...", MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                        }
                        else
                        {
                            try
                            {
                                if (textBox11.Text.Equals("0.00") || textBox11.Text.Equals(""))
                                {
                                    if (Convert.ToInt16(numUpQuantity.Text) <= 0)
                                    {
                                        MessageBox.Show("Cannot order 0 or negative quantities");
                                    }

                                    if (!txtBoxProductName.Text.Equals("") && Convert.ToInt16(numUpQuantity.Text) > 0)
                                    {
                                        AddItemToGridView();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(
                                        @"You cannot add more items until you complete the previous transaction...",@"Unable to add Item",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                                   Convert.ToInt32(numUpQuantity.Text);
            var stockQuantity = FindStockQuantity();
            if (purchaseQuantity <= stockQuantity)
            {
                dataGridView2.Rows[row].Cells[0].Value = txtBoxProductName.Text;
                dataGridView2.Rows[row].Cells[1].Value = cBoxSize.SelectedItem;
                dataGridView2.Rows[row].Cells[2].Value = textBox1.Text;
                dataGridView2.Rows[row].Cells[3].Value = Convert.ToInt32(dataGridView2.Rows[row].Cells[3].Value)
                                                         + Convert.ToInt32(numUpQuantity.Text);
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
                if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(txtBoxProductName.Text)
                    && row.Cells[1].Value != null && row.Cells[1].Value.Equals(cBoxSize.SelectedItem))
                {
                    return row.Index;
                }
            }
            return null;
        }

        private int FindStockQuantity()
        {
            var itemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("item_name ='"
                                                                                  + txtBoxProductName.Text + "'");
            var size = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemid ='"
                                                                               + itemID[0]["itemId"] + "' and size ='" +
                                                                               cBoxSize.Text + "'");
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
                cuetxtBoxProductCode.Focus();
                return true;
            }
            if (keyData == Keys.F2)
            {
                txtBoxProductName.Focus();
                return true;
            }
            if (keyData == Keys.F3)
            {
                numUpQuantity.Focus();
                return true;
            }
            if (keyData == Keys.F6)
            {
                cueTxtDiscount.Focus();
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
            if (dataGridView2.Rows.Count == 0)
            {
                textBox11.Enabled = false;
            }
            else
            {
                textBox11.Enabled = true;
            }
        }

        private void CountOverAllTotal()
        {
            _totalPayment = 0;
            double total = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                total += Convert.ToDouble(row.Cells[4].Value);
            }
            _totalPayment = total;
            if (_totalPayment > 10.00)
            {
                textBox6.Text = (string.Format("{0:0,0.00}", total));
            }
            else
            {
                textBox6.Text = (string.Format("{0:0.00}", total));
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var x = Convert.ToDouble(textBox11.Text);

                    if (x > 10.00)
                    {
                        textBox11.Text = (string.Format("{0:0,0.00}", Convert.ToDouble(x)));
                    }
                    else
                    {
                        textBox11.Text = (string.Format("{0:0.00}", Convert.ToDouble(x)));
                    }
                    CountChange();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void CountChange()
        {
            try
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
                    textBox2.Text = change > 10.00 ? (string.Format("{0:0,0.00}", change)) : (string.Format("{0:0.00}", change));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            btnSell.Enabled = false;
            if (textBox11.Text.Equals(""))
            {
                var dialog = MessageBox.Show("Are you sure you want to proceed \n" +
                                             "with this transaction? \nThis will consider that \n" +
                                             "the customers paid the exact amount", "Proceed with Transaction",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No)
                {
                    MessageBox.Show("Transaction Cancelled");
                    btnSell.Enabled = true;
                    return;
                }
                textBox11.Text = textBox6.Text;
            }
            CountChange();
            if (!textBox2.Text.Equals(""))
            {
                SellItems();
                ClearSellDatagrid();
            }
            btnSell.Enabled = true;
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
            if (Settings.Default.SalesReceipt)
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
                var line = row["item"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["Quantity"] + " is sold for "
                           + string.Format("{0:0,0.00}", row["Total"]) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                totalSold +=
                    Convert.ToDouble(row["total"]);
                rtextboxData = richTextBox2.Text;
                textBox3.Text = (string.Format("{0:0,0.00}", totalSold));
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
            newMiscRow["storeName"] = Settings.Default.storeName;
            newMiscRow["storeAddress"] = Settings.Default.storeAdd;
            newMiscRow["storeNo"] = Settings.Default.storeNo;
            newMiscRow["grandTotal"] = textBox6.Text;
            newMiscRow["cashier"] = _username;
            newMiscRow["custName"] = cueTextBox4.Text;
            newMiscRow["custRemark"] = textBox5.Text;
            receiptDataset.Tables["ReceiptInfo"].Rows.Add(newMiscRow);
        }

        private string CalculateVatSale()
        {
            var x = Convert.ToDouble(textBox6.Text);
            var y = (100 - Convert.ToDouble(Settings.Default.SalesTax))/100;
            return string.Format("{0:0,0.00}", x*y);
        }

        private string CalculateTax()
        {
            var x = Convert.ToDouble(textBox6.Text);
            var y = Convert.ToDouble(Settings.Default.SalesTax);
            var z = x*(y/100);
            return string.Format("{0:0,0.00}", z);
        }

        private void PrintTransaction(DataSet receiptDataset)
        {
            var print = new Print_Receipt(receiptDataset);
            print.ShowDialog();
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
            dt.Columns.Add("tax", typeof (Double));
            dt.Columns.Add("vatSale", typeof (Double));
            dt.Columns.Add("storeName", typeof (String));
            dt.Columns.Add("storeAddress", typeof (String));
            dt.Columns.Add("storeNo", typeof (String));
            dt.Columns.Add("grandTotal", typeof (String));
            dt.Columns.Add("cashier", typeof (String));
            dt.Columns.Add("custName", typeof (String));
            dt.Columns.Add("custRemark", typeof (String));
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
                var dialog = MessageBox.Show("Are you sure you want to remove selected item",
                    "Remove item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var manageOrder = new Manage_Orders();
            manageOrder.ShowDialog();
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
            label20.ForeColor = label20.ForeColor == Color.Red ? Color.Black : Color.Red;
        }

        private void textChangeTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ChangeCritLevelText(_criticalTable.Rows[x]);
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
            txtBoxProductName.Clear();
            textBox1.Clear();
            cBoxSize.Items.Clear();
            numUpQuantity.Text = "1";
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
                admin.ShowDialog();
            }
            else
            {
                MessageBox.Show("You dont have admin rights", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ChangeSalesSummaryDisplay();
            if (comboBox4.SelectedIndex < 2)
            {
                summaryDatePicker.Enabled = false;
            }
            else
            {
                summaryDatePicker.Enabled = true;
            }
        }

        private void ChangeSalesSummaryDisplay()
        {
            richTextBox2.Text = "";
            textBox3.Text = "0.00";
            if (checkBox1.Checked)
            {
                switch (comboBox4.SelectedIndex)
                {
                    case 0:
                        DisplayCurrentSales();
                        break;
                    case 1:
                        DisplayCurrentDaySalesGroupBy();
                        break;
                    case 2:
                        DisplaySelectedDaySaleGroupBy();
                        break;
                    case 3:
                        DisplaySelectedWeekGroupBy();
                        break;
                    case 4:
                        DisplaySelectedMonthGroupBy();
                        break;
                    case 5:
                        DisplaySelectedYearGroupBy();
                        break;
                    case 6:
                        DisplayAllSalesGroupBy();
                        break;
                }
            }
            else
            {
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
        }

        private void DisplayAllSalesGroupBy()
        {
            
            var dateselected = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,sum(count) as count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "WHERE date > DATE_SUB('" + dateselected + "', INTERVAL 1 WEEK)  " + "group by Size.sizeID");
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"])*Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
        }

        private void DisplaySelectedYearGroupBy()
        {

            var dateselected = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,sum(count) as count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "WHERE date > DATE_SUB('" + dateselected + "', INTERVAL 1 WEEK)  " + "group by Size.sizeID");
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"]) * Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
        }

        private void DisplaySelectedMonthGroupBy()
        {

            var dateselected = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,sum(count) as count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "WHERE date > DATE_SUB('" + dateselected + "', INTERVAL 1 WEEK)  " + "group by Size.sizeID");
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"]) * Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
        }

        private void DisplaySelectedWeekGroupBy()
        {

            var dateselected = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,sum(count) as count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "WHERE date > DATE_SUB('" + dateselected + "', INTERVAL 1 WEEK)  " + "group by Size.SizeID");
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"]) * Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
        }

        private void DisplaySelectedDaySaleGroupBy()
        {

            var dateToday = summaryDatePicker.Value.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,sum(count) as count,price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "where date like '%" + dateToday + "%' " + "group by Size.SizeID;");
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"]) * Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
        }

        private void DisplayCurrentDaySalesGroupBy()
        {
            var dateToday = DateTime.Now.ToString("yy-MM-dd");
            var query = ("select Item_Name,size,sum(count) as count, price,date " +
                         "from date inner join receiptid on date.DateID=receiptid.DateID " +
                         "inner join sale on sale.receiptid=receiptid.receiptid " +
                         "inner join size on sale.SizeID=size.SizeID " +
                         "inner join items on size.ItemID=items.ItemID " +
                         "where date like '%" + dateToday + "%' " +
                         "group by Size.SizeID;");
            var dt = DatabaseConnection.GetCustomTable(query, "summaryQuery");
            var total = 0.00;
            foreach (DataRow row in dt.Rows)
            {
                var totalSold = Convert.ToDouble(row["count"]) * Convert.ToDouble(row["price"]);
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
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
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
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
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
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
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
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
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ". on " +
                           Convert.ToDateTime(row["date"]).ToString("yy-MM-dd");
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
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
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
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
                var line = row["item_name"] + " " + row["size"] + " price @ " + string.Format("{0:0,0.00}", row["price"]) +
                           " with a quantity of " + row["count"] + " is sold for "
                           + string.Format("{0:0,0.00}", totalSold) + ".";
                richTextBox2.AppendText(line + Environment.NewLine);
                total += totalSold;
            }
            textBox3.Text = string.Format("{0:0,0.00}", total);
        }

        private void DisplayCurrentSales()
        {
            richTextBox2.Text = rtextboxData;
            textBox3.Text = (string.Format("{0:0,0.00}", totalSold));
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
            ChangeSalesSummaryDisplay();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new SettingsForm();
            settings.ShowDialog();
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
                var x = Convert.ToDouble(cueTxtDiscount.Text);
                var total = _totalPayment;

                if (x < 100.00)
                {
                    x /= 100.00;
                    total *= x;
                    var stringTotal = (string.Format("{0:0,0.00}", total));
                    cueTextBox3.Text = stringTotal;
                    textBox6.Text = (string.Format("{0:0,0.00}", _totalPayment - total));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void cueTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsertProductNametoCuetextBox2();
            }
        }

        private void InsertProductNametoCuetextBox2()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("itemID ='" + cueTextBox1.Text + "'");
            if (found.Length > 0)
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

        private void nullIPriceItem_Tick(object sender, EventArgs e)
        {
            try
            {
                label20.Text = "";
                ChangeItemWithNoPriceText(_itemWithNoPrice.Rows[y]);
                y++;
                if (y >= _itemWithNoPrice.Rows.Count) y = 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Default.EmailSendMessage)
            {
                SendReportToEmail();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDistrosInformation();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DataSet inventorySet = CreateDatasetInventory();
            foreach (DataGridViewRow row in invetoryGridView.Rows)
            {
                var x= inventorySet.Tables["inventory"].NewRow();
                x["ItemName"] = row.Cells[0].Value.ToString();
                x["size"] = row.Cells[1].Value.ToString();
                x["quantity"] = Convert.ToInt32(row.Cells[3].Value);
                x["Price"] = Convert.ToDouble(row.Cells[2].Value);
                inventorySet.Tables["Inventory"].Rows.Add(x);

            }
            inventorySet.WriteXml("invetory.xml");
            var printInventory = new Print_Inventory(inventorySet);
            printInventory.Show();
        }

        private DataSet CreateDatasetInventory()
        {
            var ds = new DataSet();
            var dt = new DataTable("inventory");
            dt.Columns.Add("ItemName", typeof (String));
            dt.Columns.Add("Size", typeof (String));
            dt.Columns.Add("Quantity", typeof (int));
            dt.Columns.Add("Price", typeof (Double));
            ds.Tables.Add(dt);
            return ds;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FilterInvetoryByCategory(listBox2);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void backgroundLoadingScreen_DoWork(object sender, DoWorkEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                LoadDummyReport();
                _loading.UpdateProgressBar(20);
                toolStripStatusLabel1.Text = "Current User: " + _username;
                toolStripStatusLabel2.Text = "User Rights: " + _accountType;
                _colorlist.Add(Color.Red);
                _colorlist.Add(Color.Black);
                _loading.UpdateProgressBar(40);
                var x = Task.Run(() => { LoadData(); });
                x.Wait();
                _loading.UpdateProgressBar(60);
                panel1.Controls.Add(_items);
                _items.Hide();
                LoadSettings();
                _loading.UpdateProgressBar(80);
                dateTime.Start();
                timerDataRefresh.Start();
                comboBox4.SelectedIndex = 0;
                LoadAccountSettings();
                _loading.UpdateProgressBar(99);
                Thread.Sleep(3000);
                _loading.UpdateProgressBar(100);
                Show();
            }));
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CountOverAllTotal();
            if (dataGridView2.Rows.Count == 0)
            {
                textBox11.Enabled = false;
            }
            else
            {
                textBox11.Enabled = true;
            }
        }
    }
}