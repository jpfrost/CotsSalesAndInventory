using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeLib;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Manage_Orders : Form
    {
        public Manage_Orders()
        {
            InitializeComponent();
        }

        private void Manage_Orders_Load(object sender, EventArgs e)
        {
            LoadOrderList();
        }

        private void LoadOrderList()
        {
            RefreshGrid();
            RefreshFilters();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var add_Order = new Add_Orders();
            add_Order.Show();
        }

        private void RefreshData_Tick(object sender, EventArgs e)
        {
            RefreshGrid();
            RefreshFilters();
        }

        private void RefreshFilters()
        {
            listBox1.Items.Clear();
            listBox1.BeginUpdate();
            var datatable = DatabaseConnection.GetCustomTable
                ("SELECT * FROM cotsalesinventory.orderlist where orderDelivered=false;","orderlist");
            foreach (DataRow row in datatable.Rows)
            {
                listBox1.Items.Add(row["idorderlist"].ToString());
            }
            listBox1.EndUpdate();
        }

        private void RefreshGrid()
        {
            var datatable =
                DatabaseConnection.GetCustomTable(
                    "SELECT orders.idorderlist as 'OrderList ID'" +
                    ",OrderID as 'Order ID'" +
                    ",items.Item_Name as 'Product Name'" +
                    ",size.Size as 'product size'" +
                    ",OrderQty as 'Quantity'" +
                    ",date_format(date.Date, '%d/%m/%Y') as 'Date'" +
                    "FROM cotsalesinventory.orders " +
                    "inner join Size on orders.SizeID=Size.SizeID " +
                    "inner join items on items.ItemID=Size.ItemID " +
                    "inner join date on date.DateID=orders.DateID " +
                    "inner join orderlist on orders.idorderList=orderlist.idorderList " +
                    "where orderDelivered=false;","tblOrder");
            dataGridView1.DataSource = datatable;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveOrder();
        }

        private void ReceiveOrder()
        {
            var receive = new ReceiveOrder();
            if (receive.ShowDialog(this) == DialogResult.OK)
            {
                var orderListID = receive.cueTextBox1.Text;
                InputOrders(orderListID);
            }
           
        }

        private void InputOrders(string orderListId)
        {
            var orders =
                DatabaseConnection.GetCustomTable(
                    "SELECT items.ItemID," +
                    "size.sizeID," +
                    "orders.OrderQty," +
                    "orders.idorderlist"+
                    " FROM items " +
                    "inner join Size on items.ItemID=size.ItemID " +
                    "inner join Orders on Size.SizeID=Orders.SizeID " +
                    "inner join orderlist on Orders.idorderList=orderlist.idorderList; ",
                    "receiveOrders");
            var receiveOrders = orders.Select("idorderlist ='"+ orderListId+"'");
            InsertOrders(receiveOrders);

        }

        private void InsertOrders(DataRow[] receiveOrders)
        {
            foreach (DataRow orderRow in receiveOrders)
            {
                var itemID = orderRow["itemID"].ToString();
                var sizeID = Convert.ToInt32(orderRow["sizeID"]);
                var quantity = Convert.ToInt32(orderRow["orderQty"]);
                var Size = DatabaseConnection.DatabaseRecord.Tables["Size"].Select("itemID ='"+itemID
                    +"' and sizeId ='"+sizeID+"'");
                int initialQty = GetQuantity(Size[0]["quantity"]);
                initialQty += quantity;
                Size[0]["quantity"] = initialQty;
                DatabaseConnection.UploadChanges();
            }
            
        }

        private int GetQuantity(object o)
        {
            return o != DBNull.Value ? Convert.ToInt32(o) : 0;
        }
    }
}
