using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace COTS_Sales_And_Inventory_System
{
    public partial class FrmAdmin : Form
    {
        private readonly Main _main;

        public FrmAdmin(Main main)
        {
            _main = main;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var foundAccount = accountTable.Select("AccountName ='" + cueTextBox1.Text+"'");
            if (foundAccount.Length == 0)
            {
                if (cueTextBox2.Text.Equals(cueTextBox3.Text))
                {
                    var newAccountRow = accountTable.NewRow();
                    newAccountRow["accountName"] = cueTextBox1.Text;
                    newAccountRow["accountPassword"] = cueTextBox2.Text;
                    newAccountRow["AccountType"] = SetAccountInt(comboBox1.Text);
                    DatabaseConnection.DatabaseRecord.Tables["account"].Rows.Add(newAccountRow);
                    DatabaseConnection.UploadChanges();
                    MessageBox.Show("Account has been created", "Account Created"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please ReEnter Password", "Password Mismatch"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Account already exist", "Account Exist!!!"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int SetAccountInt(string accountType)
        {
            switch (accountType)
            {
                case ("Sales Person"):
                    return 1;
                case("Sales Manager"):
                    return 2;
                case("Stock Man"):
                    return 3;
                case("Stock Mananger"):
                    return 4;
                case("Administrator"):
                    return 5;
            }
            return 0;
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

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            comboBox1.SelectedIndex = 0;
        }

        private DataTable accountTable = DatabaseConnection.DatabaseRecord.Tables["account"];
        private void LoadAccounts()
        {
            foreach (DataRow accountRow in accountTable.Rows)
            {
                if (!listBox1.Items.Contains(accountRow["AccountName"].ToString()))
                {
                    listBox1.Items.Add(accountRow["AccountName"].ToString());
                }
            }
        }
    }
}
