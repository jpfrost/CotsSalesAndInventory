using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private UserControl _items;


        public Main()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            var x = Task.Run(() => { LoadData(); });
            _items= new Items(this);
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
            ClearCategory();
            FillCategoryListBox();
            FillCategoryComboBox();
            LoadFromDatabase();
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
                foreach (DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows)
                {
                    if (value < (int) rows[columbName])
                    {
                        value = (int) rows[columbName];
                    }
                }
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
            if (_items.Visible)
            {
                _items.Hide();
            }
            else
            {
                _items.Show();
            }

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

    }

    
    }

