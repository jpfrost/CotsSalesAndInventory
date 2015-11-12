using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using COTS_Sales_And_Inventory_System.Properties;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Add_Orders : Form
    {
        private readonly string _distro;

        public Add_Orders()
        {
            InitializeComponent();
        }

        public Add_Orders(string distro)
        {
            _distro = distro;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            if (dataGridView1.Rows.Count > 0)
            {
                var task = Task.Run(() => SaveOrdersToDatabase());
                task.Wait();
                MessageBox.Show("Orders Created");
                Dispose();
            }
            else
            {
                var dialog = MessageBox.Show("Do you want to close this window", "Close Window",MessageBoxButtons.YesNo,MessageBoxIcon.Hand);
                if (dialog == DialogResult.Yes)
                {
                    Dispose();
                }
                else
                {
                    button3.Enabled = true;
                }
            }
        }

        private void SaveOrdersToDatabase()
        {
            var orderListID = GetCurrentCount("orderlist", "idorderlist");
            CreateOrderList(orderListID);
            var dateID = GetCurrentCount("date", "DateID");
            CreateOrderDate(dateID);
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var categoryiD = GetCategoryID(dataGridView1.Rows[i].Cells[4].Value.ToString());
                var OrderID = GetCurrentCount("orders", "OrderID");
                var distroId = CheckifDistroExist(dataGridView1.Rows[i].Cells[3].Value.ToString());
                CheckIfProductExist(dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    dataGridView1.Rows[i].Cells[1].Value.ToString(), categoryiD,
                    dataGridView1.Rows[i].Cells[5].Value.ToString());
                var sizeId = GetSizeID(dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    dataGridView1.Rows[i].Cells[1].Value.ToString());
                var orderQty = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
               if (radioButton2.Checked)
               {
                   RemoveItemFromInventory(sizeId, orderQty);
               }
                CreateOrder(orderListID, dateID, OrderID, distroId, sizeId, orderQty);
            }
        }

        private Boolean RemoveItemFromInventory(int sizeId, int orderQty)
        {
            bool accepted = false;
            var size = DatabaseConnection.DatabaseRecord.Tables["size"].Select("sizeID ='" + sizeId + "'");
            var x = Convert.ToInt32(size[0]["Quantity"]);
            if (x >= orderQty)
            {
                x -= orderQty;
                size[0]["Quantity"] = x;
                DatabaseConnection.UploadChanges();
                accepted = true;
            }
            return accepted;
        }

        private int GetCategoryID(string category)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["category"].Select("categoryName ='" +
                                                                                    category + "'");
            if (found.Length > 0)
            {
                return Convert.ToInt32(found[0]["categoryid"]);
            }
            return CreatenewCategory(category);
        }

        private int CreatenewCategory(string category)
        {
            var categoryRow = DatabaseConnection.DatabaseRecord.Tables["category"].NewRow();
            categoryRow["categoryid"] = GetCurrentCount("category", "categoryID");
            categoryRow["categoryName"] = category;
            DatabaseConnection.DatabaseRecord.Tables["category"].Rows.Add(categoryRow);
            DatabaseConnection.UploadChanges();
            return Convert.ToInt32(categoryRow["categoryID"]);
        }

        private void CreateOrder(int orderListId, int dateId, int orderId, int distroId, int sizeId, int orderQty)
        {
            var newOrderlist = DatabaseConnection.DatabaseRecord.Tables["orders"].NewRow();
            newOrderlist["orderid"] = orderId;
            newOrderlist["dateID"] = dateId;
            newOrderlist["distroID"] = distroId;
            newOrderlist["idorderlist"] = orderListId;
            newOrderlist["sizeId"] = sizeId;
            newOrderlist["orderQty"] = orderQty;
            DatabaseConnection.DatabaseRecord.Tables["orders"].Rows.Add(newOrderlist);
            DatabaseConnection.UploadChanges();
            var crit = new ItemLevel(sizeId, orderQty);
            crit.InsertCriticalLevel();
        }

        private int GetSizeID(string productName, string size)
        {
            var itemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_name ='"
                                                                                  + productName + "'");
            var found = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='"
                                                                                + itemID[0]["itemid"] + "' and size ='" +
                                                                                size + "'");
            return Convert.ToInt32(found[0]["sizeID"]);
        }

        private int CheckifDistroExist(string distroName)
        {
            var searchDistro = DatabaseConnection.DatabaseRecord.Tables["distributor"].Select("distroName ='" +
                                                                                              distroName + "'");
            if (searchDistro.Length > 0)
            {
                return Convert.ToInt32(searchDistro[0]["DistroID"]);
            }
            var distroId = GetCurrentCount("distributor", "DistroID");
            CreateNewDistro(distroId, distroName);
            return distroId;
        }

        private void CreateNewDistro(int distroId, string distroName)
        {
            var newDistro = DatabaseConnection.DatabaseRecord.Tables["distributor"].NewRow();
            newDistro["DistroID"] = distroId;
            newDistro["distroName"] = distroName;
            DatabaseConnection.DatabaseRecord.Tables["distributor"].Rows.Add(newDistro);
            DatabaseConnection.UploadChanges();
        }

        private void CreateOrderList(int orderId)
        {
            var newOrderList = DatabaseConnection.DatabaseRecord.Tables["orderlist"].NewRow();
            newOrderList["idorderlist"] = orderId;
            newOrderList["orderDelivered"] = false;
            if (radioButton2.Checked)
            {
                newOrderList["orderdesc"] = "Back Order";
            }
            else
            {
                newOrderList["orderdesc"] = "Order";
            }
            DatabaseConnection.DatabaseRecord.Tables["orderlist"].Rows.Add(newOrderList);
            DatabaseConnection.UploadChanges();
        }

        private void CreateOrderDate(int dateId)
        {
            var newDate = DatabaseConnection.DatabaseRecord.Tables["date"].NewRow();
            newDate["dateID"] = dateId;
            newDate["Date"] = DateTime.Now.ToString("dd-MM-yyyy");
            DatabaseConnection.DatabaseRecord.Tables["date"].Rows.Add(newDate);
            DatabaseConnection.UploadChanges();
        }

        private void CheckIfProductExist(string productName, string productSize, int categoryiD,string productID)
        {
            var findProductId = SearchData("items", "Item_Name ='" + productName + "'");
            if (findProductId.Length > 0)
            {
                var findSizeId = SearchData("Size", "Size ='" + productSize + "' and" +
                                                    " itemID ='"
                                                    + findProductId[0]["ItemID"] + "'");
                if (findSizeId.Length == 0)
                {
                    CreateSize(productName, productSize);
                }
            }
            else
            {
                CreateProduct(productName, categoryiD,productID);
                CreateSize(productName, productSize);
            }
        }

        private void CreateProduct(string productName, int categoryiD, string productId)
        {
            var productRow = DatabaseConnection.DatabaseRecord.Tables["Items"].NewRow();
            if (!productId.Equals(""))
            {
                productRow["itemID"] = productId;
            }
            else
            {
                productRow["itemID"] = CreateNewProductID();
            }
            productRow["Item_Name"] = productName;
            productRow["categoryId"] = categoryiD;
            DatabaseConnection.DatabaseRecord.Tables["Items"].Rows.Add(productRow);
            DatabaseConnection.UploadChanges();
        }

        private string CreateNewProductID()
        {
            var x = GetCurrentCount("items_seq", "id");
            var newitemSeqRow = DatabaseConnection.DatabaseRecord.Tables["items_seq"].NewRow();
            newitemSeqRow["id"] = x;
            DatabaseConnection.DatabaseRecord.Tables["items_seq"].Rows.Add(newitemSeqRow);
            return "MANUAL" + x;
        }

        private void CreateSize(string productName, string productSize)
        {
            try
            {
                var sizeRow = DatabaseConnection.DatabaseRecord.Tables["Size"].NewRow();
                var ItemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_name ='"
                                                                                      + productName + "'");
                sizeRow["sizeID"] = GetCurrentCount("size", "SizeId");
                sizeRow["Size"] = productSize;
                sizeRow["itemID"] = ItemID[0]["ItemID"];
                sizeRow["sizeEnable"] = 1;
                DatabaseConnection.DatabaseRecord.Tables["size"].Rows.Add(sizeRow);
                DatabaseConnection.UploadChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private DataRow[] SearchData(string tablename, string query)
        {
            return DatabaseConnection.DatabaseRecord.Tables[tablename].Select(query);
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

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Add item(s) to order list?", "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AddItemToDataGrid();
            }
        }

        private void AddItemToDataGrid()
        {
            
                if (cueTextBox3.Text != "" && comboBox1.Text != "" && textBox1.Text != "" && comboBox3.Text != "" &&comboBox2.Text !="")
                {
                    if (radioButton2.Checked)
                    {
                        if (SearchifPossibleItem())
                        {
                            AddOrdertoGrid();
                        }
                        else
                        {
                            MessageBox.Show("Cannot Proceed with back order. order quantity Exceed stock quantity", "Cannot Proceed with order");
                        }
                    }
                    else
                    {
                        AddOrdertoGrid();
                    }
                }
                else
                {
                    MessageBox.Show("Please fill out all the Product Information", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            
        }

        private bool SearchifPossibleItem()
        {
            var notNegative = false;
            var itemName = FindItemID();
            var size = FindSizeId(itemName);
            if (ComputeQuantity(size) >= 0)
            {
                notNegative = true;
            }
            return notNegative;
        }

        private int ComputeQuantity(DataRow[] size)
        {
            var originalQuantity = Convert.ToInt32(size[0]["Quantity"]);
            var orderQuantity = Convert.ToInt32(numericUpDown1.Text);
            originalQuantity -= orderQuantity;
            return originalQuantity;
        }

        private DataRow[] FindSizeId(DataRow[] itemName)
        {
            var x = DatabaseConnection.GetCustomTable("SELECT * " +
                                                      "FROM size inner join items on " +
                                                      "size.ItemID=items.ItemID where " +
                                                      "items.ItemID='"+itemName[0]["itemID"]+"'",
                                                      "itemSize");

            return x.Select("size ='"+comboBox1.Text+"'");
        }

        public void AddOrdertoGrid()
        {
            var newOrder = FindifProductExistForSale() ?? dataGridView1.Rows.Add();
            dataGridView1.Rows[newOrder].Cells[0].Value = cueTextBox3.Text; //product name
            dataGridView1.Rows[newOrder].Cells[1].Value = comboBox2.Text+" "+comboBox1.Text; // product size
            dataGridView1.Rows[newOrder].Cells[2].Value = numericUpDown1.Text; //quantity
            dataGridView1.Rows[newOrder].Cells[3].Value = textBox1.Text; //distro
            dataGridView1.Rows[newOrder].Cells[4].Value = comboBox3.Text; //category
            dataGridView1.Rows[newOrder].Cells[5].Value = cueTextBox4.Text;//productID
            ClearFields();
            //LoadDistros();
            LoadCategory();
        }

        private int? FindifProductExistForSale()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(cueTextBox3.Text)
                    && row.Cells[1].Value != null && row.Cells[1].Value.Equals(comboBox1.SelectedItem))
                {
                    return row.Index;
                }
            }
            return null;
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

        private void Add_Orders_Load(object sender, EventArgs e)
        {
            //LoadDistros();
            textBox1.Text = _distro;
            LoadCategory();
            AutoCompleteItemName();
            radioButton1.Checked = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                cueTextBox4.Focus();
                return true;
            }
            if (keyData == Keys.F2)
            {
                cueTextBox3.Focus();
                return true;
            }
            if (keyData == Keys.F3)
            {
                numericUpDown1.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadCategory()
        {
            comboBox3.Items.Clear();
            var found = DatabaseConnection.DatabaseRecord.Tables["category"];
            foreach (DataRow row in found.Rows)
            {
                if (!comboBox3.Items.Contains(row["CategoryName"].ToString()))
                {
                    comboBox3.Items.Add(row["CategoryName"].ToString());
                }
            }
        }

        private void AutoCompleteItemName()
        {
            var autoCompleteCollectionProductName = new AutoCompleteStringCollection();
            foreach (DataRow dr in DatabaseConnection.DatabaseRecord.Tables["items"].Rows)
            {
                autoCompleteCollectionProductName.Add(dr["Item_Name"].ToString());
            }
            cueTextBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cueTextBox3.AutoCompleteCustomSource = autoCompleteCollectionProductName;
        }

        /*private void LoadDistros()
        {
            comboBox2.Items.Clear();
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Add(Settings.Default.DefaultSupplier);
            if (Settings.Default.AllowMultiSupplier)
            {
                var distro = DatabaseConnection.DatabaseRecord.Tables["distributor"].Select("distroEnable ='" + 1 + "'");
                foreach (DataRow distroRow in distro)
                {
                    comboBox2.Items.Add(distroRow["distroName"]);
                }
            }
            comboBox2.Refresh();
            comboBox2.SelectedIndex = 0;
        }*/

        private void cueTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadProduct();
                AddItemToDataGrid();
            }
        }

        private void LoadProduct()
        {
            var productCode = cueTextBox4.Text;
            InsertProductInfo(productCode);
            LoadItemInfo();
        }

        private void InsertProductInfo(string productCode)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["items"].Select("ItemID ='"
                                                                                 + productCode + "'");
            if (found.Length == 0)
            {
                
            }
            else
            {
                cueTextBox3.Text = found[0]["Item_Name"].ToString();
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove this item(s)", "Confirmation",
                MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
            }
        }

        private void cueTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadItemInfo();
            }
        }

        private void LoadItemInfo()
        {
            var itemID = FindItemID();
            //LoadSize(itemID);
            LoadItemCategory(itemID);
            LoadItemSize(itemID);

        }

        private void LoadItemSize(DataRow[] itemId)
        {
            
            try
            {
                comboBox2.Items.Clear();
                var size = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='" + itemId[0]["itemID"] + "'");
                foreach (var row in size)
                {
                    var split = row["size"].ToString().Split(' ');
                    comboBox2.Items.Add(split[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void LoadItemCategory(DataRow[] itemId)
        {
            try
            {
                var category = DatabaseConnection.DatabaseRecord.Tables["Category"].Select("CategoryID ='"+itemId[0]["CategoryID"]+"'");
                comboBox3.SelectedItem = category[0]["CategoryName"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /*private void LoadSize(DataRow[] itemId)
        {
            comboBox1.Items.Clear();
            var size = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='"+itemId[0]["itemID"]+"'");
            foreach (var row in size)
            {
                comboBox1.Items.Add(row["Size"]);
            }
            comboBox1.SelectedIndex = 0;
        }*/

        private DataRow[] FindItemID()
        {
            return DatabaseConnection.DatabaseRecord.Tables["items"].Select("item_Name ='"+cueTextBox3.Text+"'");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dialog = MessageBox.Show("Are you sure you want to remove item",
                    "Remove item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            if (!radiobutton.Checked)
            {
                var dialogResult = MessageBox.Show("Are you sure you want to change order type\nData in The gridbox will be remove...", "Change Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (dialogResult == DialogResult.Yes)
                {
                    UncheckOtherRadioButton();
                    radiobutton.Checked = true;
                    ClearDataGrid();
                }
            }
            else
            {
                radiobutton.Checked = false;
            }
        }

        private void ClearDataGrid()
        {
            dataGridView1.Rows.Clear();
        }

        private void UncheckOtherRadioButton()
        {
            foreach (var control in Controls)
            {
                var button = control as RadioButton;
                if (button != null)
                {
                    button.Checked = false;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllUnitsFromCategory();
        }

        private void GetAllUnitsFromCategory()
        {
            var x = DatabaseConnection.DatabaseRecord.Tables["tblsizes"].Select("categoryID ='"+GetCategoryID(comboBox3.Text)+"'");
            comboBox1.Items.Clear();
            foreach (var row in x)
            {
                comboBox1.Items.Add(row["sizesName"].ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cueTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cueTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void KeyOnlyDigits(object sender, KeyPressEventArgs e)
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
    }
    }
