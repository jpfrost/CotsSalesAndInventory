using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
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
            SaveOrdersToDatabase();
            this.Dispose();
        }

        private void SaveOrdersToDatabase()
        {
            var orderListID = GetCurrentCount("orderlist", "idorderlist");
            CreateOrderList(orderListID);
            var dateID = GetCurrentCount("date", "DateID");
            CreateOrderDate(dateID);
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var OrderID = GetCurrentCount("orders", "OrderID");
                var distroId = CheckifDistroExist(dataGridView1.Rows[i].Cells[4].Value.ToString());
                CheckIfProductExist(dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    dataGridView1.Rows[i].Cells[1].Value.ToString());
                var sizeId = GetSizeID(dataGridView1.Rows[i].Cells[0].Value.ToString(),
                    dataGridView1.Rows[i].Cells[1].Value.ToString());
                
            }
        }

        private int GetSizeID(string productName, string size)
        {
            var itemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_name ='"
                +productName+"'");
            var found = DatabaseConnection.DatabaseRecord.Tables["size"].Select("itemID ='"
                +itemID[0]["itemid"]+"' and size ='"+size+"'");
            return Convert.ToInt32(found[0]["sizeID"]);
        }


        private int CheckifDistroExist(string distroName)
        {
            var searchDistro = DatabaseConnection.DatabaseRecord.Tables["distributor"].Select("distroName ='" +
                                                                                               distroName+"'");
            if (searchDistro.Length > 0)
            {
                return Convert.ToInt32(searchDistro[0]["DistroID"]);
            }
            var distroId= GetCurrentCount("distributor", "DistroID");
            CreateNewDistro(distroId,distroName);
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
            var newDate= DatabaseConnection.DatabaseRecord.Tables["date"].NewRow();
            newDate["dateID"] = dateId;
            newDate["Date"] = DateTime.Now.ToString("dd-MM-yyyy");
            DatabaseConnection.DatabaseRecord.Tables["date"].Rows.Add(newDate);
            DatabaseConnection.UploadChanges();
        }

        private void CheckIfProductExist(string productName, string productSize)
        {
            var findProductId = SearchData("items","Item_Name ='"+productName+"'");
            if (findProductId.Length > 0)
            {
                var findSizeId = SearchData("Size", "Size ='" + productSize + "' and" +
                                                    " itemID ='"
                                                    +findProductId[0]["ItemID"]+"'");
                if (findSizeId.Length == 0)
                {
                    CreateSize(productName,productSize);
                }
            }
            else
            {
                CreateProduct(productName);
                CreateSize(productName, productSize);
            }
        }

        private void CreateProduct(string productName)
        {
            var productRow = DatabaseConnection.DatabaseRecord.Tables["Items"].NewRow();
            var _productCode = DatabaseConnection.DatabaseRecord.Tables["items"].AsEnumerable()
                    .Max(maxValue => maxValue.Field<string>("itemID"));
           var newProdCode = (Convert.ToInt32(_productCode) + 1).ToString();
            productRow["itemID"] = newProdCode;
            productRow["Item_Name"] = productName;
            DatabaseConnection.DatabaseRecord.Tables["Items"].Rows.Add(productRow);
            DatabaseConnection.UploadChanges();
        }

        private void CreateSize(string productName, string productSize)
        {
            var sizeRow = DatabaseConnection.DatabaseRecord.Tables["Size"].NewRow();
            var ItemID = DatabaseConnection.DatabaseRecord.Tables["items"].Select("Item_name ='"
                +productName+"'");
            sizeRow["sizeID"] = GetCurrentCount("size","SizeId");
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
            else
            {
                value = (from DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows select (int)rows[columbName]).Concat(new[] { value }).Max();
            }
            return value + 1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateOrder();
        }

        public void CreateOrder()
        {
           
            var newOrder = FindifProductExistForSale() ?? dataGridView1.Rows.Add();
            dataGridView1.Rows[newOrder].Cells[0].Value = cueTextBox3.Text;
            dataGridView1.Rows[newOrder].Cells[1].Value = comboBox1.Text;
            dataGridView1.Rows[newOrder].Cells[2].Value = numericUpDown1.Text;
            dataGridView1.Rows[newOrder].Cells[3].Value = DateTime.Now.ToString("dd-MM-yyyy");
            dataGridView1.Rows[newOrder].Cells[4].Value = comboBox2.Text;
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
            
        }
    }
}
