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
    public partial class CategoryUnits : Form
    {
        private readonly string _category;

        public CategoryUnits(string category)
        {
            _category = category;
            InitializeComponent();
        }

        private void CategoryUnits_Load(object sender, EventArgs e)
        {
            cueTextBox1.Text = _category;
            AddUnitsFromDatabase();
            foreach (var units in Properties.Settings.Default.CategoryUnits)
            {
                if (!listBox2.Items.Contains(units))
                {
                    listBox1.Items.Add(units);
                }
            }
            

        }

        private void AddUnitsFromDatabase()
        {
            foreach (var row in DatabaseConnection.DatabaseRecord.Tables["tblsizes"].Select("CategoryID ='"+FindCategoryId()+"'"))
            {
                listBox2.Items.Add(row["sizesName"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = listBox1.SelectedItems.OfType<string>().ToList();
            foreach (var selected in x)
            {
                try
                {
                    if (!listBox2.Items.Contains(selected))
                    {
                        listBox2.Items.Add(selected);
                        listBox1.Items.Remove(selected);
                    }
                }
                catch (Exception va)
                {
                    Console.WriteLine(va);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var x = listBox2.SelectedItems.OfType<string>().ToList();
            foreach (var selected in x)
            {
                listBox2.Items.Remove(selected);
                listBox1.Items.Add(selected);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveToDatabase();
            Dispose();
        }

        private void SaveToDatabase()
        {
            var catID = FindCategoryId();
            var table = DatabaseConnection.DatabaseRecord.Tables["tblsizes"].Select("categoryID ='" + catID + "'");
            DeleteOldData(table);
            foreach (var units in listBox2.Items)
            {
                var sizeName = units.ToString();
                CreateNewData(sizeName,catID);
            }
            
        }

        private void CreateNewData(string sizeName, int catId)
        {
            var newtblSizesRow= DatabaseConnection.DatabaseRecord.Tables["tblsizes"].NewRow();
            newtblSizesRow["sizesID"] = GetCurrentCount("tblsizes", "sizesID");
            newtblSizesRow["sizesName"] = sizeName;
            newtblSizesRow["categoryID"] = catId;
            DatabaseConnection.DatabaseRecord.Tables["tblsizes"].Rows.Add(newtblSizesRow);
            DatabaseConnection.UploadChanges();
        }

        private void DeleteOldData(DataRow[] table)
        {
            foreach (DataRow row in table)
            {
                row.Delete();
            }
            DatabaseConnection.UploadChanges();
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
                 select (int)rows[columbName]).Concat(new[] { value }).Max();
            return value + 1;
        }

        private int FindCategoryId()
        {
            var x=DatabaseConnection.DatabaseRecord.Tables["Category"].Select("CategoryName ='"+cueTextBox1.Text+"'");
            return Convert.ToInt32(x[0]["categoryID"]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
