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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                
            }
            else if (keyData == Keys.F5)
            {
                RefreshGrid();
                RefreshFilters();
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            LoadOrderList();
        }

        private void ReceiveOrder()
        {
            var receive = new ReceiveOrder();
            if (receive.ShowDialog(this) == DialogResult.OK)
            {
                var orderListID = receive.cueTextBox1.Text;
                var findOrderlist = DatabaseConnection.DatabaseRecord.Tables["orderlist"].Select("idorderlist ='"
                    +orderListID+"'");
                if (findOrderlist.Length > 0)
                {
                    var orderReceive = Convert.ToBoolean(findOrderlist[0]["orderDelivered"]);
                    if (!orderReceive)
                    {
                        InputOrders(orderListID);
                        findOrderlist[0]["orderDelivered"] = true;
                        DatabaseConnection.UploadChanges();
                        MessageBox.Show("OrderList No.: "+orderListID+" has been receive");
                    }
                    else
                    {
                        MessageBox.Show("Orders already Delivered", "" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Order does not exist");
                }
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


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                button4.Enabled = false;
                PrintOrder();
                button4.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Please select and order number", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void PrintOrder()
        {
            var ds = new DataSet();
           var orders =
                DatabaseConnection.GetCustomTable(
                    "select orders.idorderList" +
                    ",items.Item_Name,size.Size" +
                    ",orders.OrderQty" +
                    ",distributor.DistroName " +
                    "from orderlist " +
                    "inner join orders on orderlist.idorderList=orders.idorderList" +
                    " inner join distributor on orders.DistroID=distributor.DistroID " +
                    "inner join size on orders.SizeID=size.SizeID " +
                    "inner join items on items.ItemID=size.ItemID " +
                    "where orders.idorderList="+listBox1.SelectedItem+";",
                    "receiveOrders");
            ds.Tables.Add(orders);
            ds.WriteXml("order.xml");
            var print = new Print_Orders(ds);
            print.Show();
        }

        private void FilterInvetoryByCategory()
        {
            if (!listBox1.SelectedItem.Equals("All"))
            {
                ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = ("[OrderList ID]='"
                 + listBox1.SelectedItem + "'");

            }
            else
            {
                ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Empty;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterInvetoryByCategory();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReceiveOrder(); //jasper fix this shit..

        }
    }
}
