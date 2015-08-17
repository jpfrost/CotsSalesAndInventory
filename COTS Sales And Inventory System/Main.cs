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
            LoadData();
        }

        private void LoadData()
        {
            LoadInventory();
        }

        private void LoadInventory()
        {
            BindInventoryRecordsToDatagrid(dataGridView1);
            FillCategory();
        }

        private void FillCategory()
        {
            ClearCategory();
            FillCategoryListBox();
            FillCategoryComboBOx();
        }

        private void FillCategoryComboBOx()
        {
            foreach (var catName in listBox1.Items)
            {
                comboBox1.Items.Add(catName);
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

        private void BindInventoryRecordsToDatagrid(DataGridView gridView1)
        {
            gridView1.DataSource = DatabaseConnection.databaseRecord.Tables["Inventory"];
            gridView1.Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddCategory();
        }

        private void AddCategory()
        {
            var catPK= GetCategoryCurrentCount();
            var catName = comboBox1.Text;
            if (DatabaseConnection.databaseRecord.Tables["category"].Columns.Contains(catName))
            {
                MessageBox.Show(@"Category Exist this category will not be created", @"Category Exist");
            }
            else
            {
                var newCategory= DatabaseConnection.databaseRecord.Tables["category"].NewRow();
                newCategory[0] = catPK;
                newCategory[1] = catName;
                DatabaseConnection.databaseRecord.Tables["category"].Rows.Add(newCategory);
                DatabaseConnection.UploadChanges();
                FillCategory();
                MessageBox.Show(@"Category Added");
            }
        }

        private void ClearCategory()
        {
            listBox1.Items.Clear();
            comboBox1.Items.Clear();
        }

        private int GetCategoryCurrentCount()
        {
            var value = 0;
            if (DatabaseConnection.databaseRecord.Tables["category"].Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                foreach (DataRow rows in DatabaseConnection.databaseRecord.Tables["category"].Rows)
                {
                    if (value < (int) rows["CategoryID"])
                    {
                        value = (int) rows["CategoryID"];
                    }
                }
            }
            return value+1;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DeleteCategory();
        }

        private void DeleteCategory()
        {
            var delCat = comboBox1.SelectedItem;
            var found= DatabaseConnection.databaseRecord.Tables["category"].Select("CategoryName = '"+delCat+"'");
            found[0].Delete();
            DatabaseConnection.UploadChanges();
            MessageBox.Show(@"Delete Successful");
            FillCategory();
        }
    }
}
