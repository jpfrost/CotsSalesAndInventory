using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class FrmAdmin : Form
    {
        private readonly Main _main;
        private readonly DataTable accountTable = DatabaseConnection.DatabaseRecord.Tables["account"];

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
            var foundAccount = accountTable.Select("AccountName ='" + cueTextBox1.Text + "'");
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
                    LoadAccounts();
                    ClearField();
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

        private void ClearField()
        {
            foreach (var control in Controls)
            {
                var box = control as CueTextBox;
                if (box != null)
                {
                    box.Text = "";
                }
            }
        }

        private int SetAccountInt(string accountType)
        {
            switch (accountType)
            {
                case ("Sales Person"):
                    return 1;
                case ("Sales Manager"):
                    return 2;
                case ("Stock Man"):
                    return 3;
                case ("Stock Mananger"):
                    return 4;
                case ("Administrator"):
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
            value =
                (from DataRow rows in DatabaseConnection.DatabaseRecord.Tables[tableName].Rows
                    select (int) rows[columbName]).Concat(new[] {value}).Max();
            return value + 1;
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            comboBox1.SelectedIndex = 0;
        }

        private void LoadAccounts()
        {
            listBox1.Items.Clear();
            foreach (DataRow accountRow in accountTable.Rows)
            {
                if (!listBox1.Items.Contains(accountRow["AccountName"].ToString()))
                {
                    listBox1.Items.Add(accountRow["AccountName"].ToString());
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAccountInfo();
        }

        private void DisplayAccountInfo()
        {
            try
            {
                var found = accountTable.Select("accountName ='"
                                                + listBox1.SelectedItem + "'");
                cueTextBox1.Text = found[0]["accountName"].ToString();
                cueTextBox2.Text = found[0]["accountPassword"].ToString();
                cueTextBox3.Text = found[0]["accountPassword"].ToString();
                comboBox1.SelectedIndex = Convert.ToInt32(found[0]["accounttype"]) - 1;
            }
            catch (Exception C)
            {
                Console.WriteLine(C);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private void UpdateAccount()
        {
            var found = accountTable.Select("accountName ='"
                                            + listBox1.SelectedItem + "'");
            try
            {
                found[0]["accountName"] = cueTextBox1.Text;
                found[0]["accountPassword"] = cueTextBox2.Text;
                found[0]["AccountType"] = SetAccountInt(comboBox1.Text);
                DatabaseConnection.UploadChanges();
                listBox1.Items.Clear();
                LoadAccounts();
                MessageBox.Show(cueTextBox1.Text + " has been updated");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating account");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var found = DatabaseConnection.DatabaseRecord.Tables["account"].Select("AccountName ='"+cueTextBox1.Text+"'");
                found[0].Delete();
                DatabaseConnection.UploadChanges();
                MessageBox.Show("Account: "+cueTextBox1.Text+" has been remove...");
                LoadAccounts();
                ClearField();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}