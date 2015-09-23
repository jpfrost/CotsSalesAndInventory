﻿using System;
using System.Data;
using System.Linq;
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
                MessageBox.Show("Please fill out all the Product Information");
                Dispose();
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
                CreateOrder(orderListID, dateID, OrderID, distroId, sizeId, orderQty);
            }
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
            return "MANUAL" + x;
        }

        private void CreateSize(string productName, string productSize)
        {
            var sizeRow = DatabaseConnection.DatabaseRecord.Tables["Size"].NewRow();
            var ItemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_name ='"
                                                                                  + productName + "'");
            sizeRow["sizeID"] = GetCurrentCount("size", "SizeId");
            sizeRow["Size"] = productSize;
            sizeRow["itemID"] = ItemID[0]["ItemID"];
            DatabaseConnection.DatabaseRecord.Tables["size"].Rows.Add(sizeRow);
            DatabaseConnection.UploadChanges();
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
                if (cueTextBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
                {
                    AddOrdertoGrid();
                }
                else
                {
                    MessageBox.Show("Please fill out all the Product Information", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        public void AddOrdertoGrid()
        {
            var newOrder = FindifProductExistForSale() ?? dataGridView1.Rows.Add();
            dataGridView1.Rows[newOrder].Cells[0].Value = cueTextBox3.Text; //product name
            dataGridView1.Rows[newOrder].Cells[1].Value = comboBox1.Text; // product size
            dataGridView1.Rows[newOrder].Cells[2].Value = numericUpDown1.Text; //quantity
            dataGridView1.Rows[newOrder].Cells[3].Value = comboBox2.Text; //distro
            dataGridView1.Rows[newOrder].Cells[4].Value = comboBox3.Text; //category
            dataGridView1.Rows[newOrder].Cells[5].Value = cueTextBox4.Text;//productID
            ClearFields();
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
            LoadDistros();
        }

        private void LoadDistros()
        {
            var distro = DatabaseConnection.DatabaseRecord.Tables["distributor"];
            foreach (DataRow distroRow in distro.Rows)
            {
                comboBox2.Items.Add(distroRow["distroName"]);
            }
            comboBox2.Refresh();
        }

        private void cueTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadProduct();
            }
        }

        private void LoadProduct()
        {
            var productCode = cueTextBox4.Text;
            InsertProductInfo(productCode);
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
            }
        }
    }
}