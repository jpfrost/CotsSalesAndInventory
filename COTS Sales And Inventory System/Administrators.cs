using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class FrmAdmin : Form
    {
        private readonly Main _main;
        private DataTable _dataaccountTable = DatabaseConnection.DatabaseRecord.Tables["account"];

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
            if (
                !(string.IsNullOrWhiteSpace(cueTextBox1.Text) || string.IsNullOrWhiteSpace(cueTextBox2.Text) ||
                  string.IsNullOrWhiteSpace(cueTextBox3.Text)))
            {
                var foundAccount = _dataaccountTable.Select("AccountName ='" + cueTextBox1.Text + "'");
                if (foundAccount.Length == 0)
                {
                    if (cueTextBox2.Text.Equals(cueTextBox3.Text))
                    {
                        var newAccountRow = _dataaccountTable.NewRow();
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
            else
            {
                MessageBox.Show("Please enter the required Inputs","Missing Fields",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            LoadAccounts();
            listBox1.Update();
            listBox1.Refresh();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
       
            if (keyData == Keys.F5)
            {
                LoadAccounts();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadAccounts()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["account"];
            listBox1.Items.Clear();
            foreach (DataRow accountRow in found.Rows)
            {
                if (!listBox1.Items.Contains(accountRow["AccountName"].ToString()))
                {
                    listBox1.Items.Add(accountRow["AccountName"].ToString());
                }
            }
            listBox1.Update();
            listBox1.Refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAccountInfo();
        }

        private void DisplayAccountInfo()
        {
            try
            {
                var found = _dataaccountTable.Select("accountName ='"
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
            listBox1.Update();
            listBox1.Refresh();
        }

        private void UpdateAccount()
        {
            if (!(string.IsNullOrWhiteSpace(cueTextBox1.Text) || string.IsNullOrWhiteSpace(cueTextBox2.Text) ||
                  string.IsNullOrWhiteSpace(cueTextBox3.Text)))
            {
                var found = _dataaccountTable.Select("accountName ='"
                                                + listBox1.SelectedItem + "'");

                if (found.Length > 0)
                {
                    try
                    {
                        found[0]["accountName"] = cueTextBox1.Text;
                        found[0]["accountPassword"] = cueTextBox2.Text;
                        found[0]["AccountType"] = SetAccountInt(comboBox1.Text);
                        DatabaseConnection.UploadChanges();
                        listBox1.Items.Clear();
                        LoadAccounts();
                        listBox1.Items.Clear();
                        MessageBox.Show(cueTextBox1.Text + " has been updated");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error updating account");
                    }
                }
                else
                {
                    MessageBox.Show("Error in Updating Account", "Updating Account Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
                }
            }
            else
            {
                MessageBox.Show("Please enter the required Inputs", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cueTextBox1.Text))
            {
                try
                {
                    var found =
                        DatabaseConnection.DatabaseRecord.Tables["account"].Select("AccountName ='" + cueTextBox1.Text +
                                                                                   "'");
                    found[0].Delete();
                    DatabaseConnection.UploadChanges();
                    MessageBox.Show("Account: " + cueTextBox1.Text + " has been remove...");
                    LoadAccounts();
                    ClearField();
                    listBox1.Refresh();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                MessageBox.Show("Account Deletion Failed", "Problem with Account Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            }
            listBox1.Update();
            listBox1.Refresh();
        }
    }
}