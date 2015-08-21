using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Main : Form
    {
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
        }

        private void FillCategoryComboBox()
        {
            foreach (var catName in listBox1.Items)
            {
                comboBox1.Items.Add(catName);
            }
            foreach (DataRow rows in DatabaseConnection.databaseRecord.Tables["distributor"].Rows)
            {
                if (!comboBox2.Items.Contains(rows["distroName"]))
                {
                    comboBox2.Items.Add(rows["distroName"]);
                }
            }

        }

        private void FillCategoryListBox()
        {
            listBox1.BeginUpdate();
            foreach (DataRow rows in DatabaseConnection.databaseRecord.Tables["category"].Rows)
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
            return DatabaseConnection.databaseRecord.Tables[tablename].Select(query);
        }

        private void InsertNewCategory(int catPk, string catName)
        {
            var newCategory = DatabaseConnection.databaseRecord.Tables["category"].NewRow();
            newCategory[0] = catPk;
            newCategory[1] = catName;
            DatabaseConnection.databaseRecord.Tables["category"].Rows.Add(newCategory);
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
            if (DatabaseConnection.databaseRecord.Tables[tableName].Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                foreach (DataRow rows in DatabaseConnection.databaseRecord.Tables[tableName].Rows)
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
            var distroID = GetCurrentCount("distributor", "DistroID");
            var distroName = comboBox2.Text;
            var found = FindRow("distributor", "DistroName = '" + distroName + "'");
            if (found.Length > 0)
            {
                MessageBox.Show(@"Category Exist this category will not be created", @"Category Exist");
            }
            else
            {
                InsertNewDistro(distroID, distroName);
            }

        }

        private void InsertNewDistro(int distroid, string distroname)
        {
            var newDistro = DatabaseConnection.databaseRecord.Tables["distributor"].NewRow();
            newDistro[0] = distroid;
            newDistro[1] = distroname;
            DatabaseConnection.databaseRecord.Tables["distributor"].Rows.Add(newDistro);
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
    }

    
    }

