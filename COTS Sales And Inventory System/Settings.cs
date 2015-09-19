using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            LoadAdminSettings();
            LoadSecret();
            LoadEmailSettings();
            LoadMySqlSettings();
            LoadSupplierSettings();
            LoadSalesComputation();
            LoadInventorySettings();
            LoadSummarySettings();
            LoadStoreInfo();
        }

        private void LoadSecret()
        {
            try
            {
                comboBox1.Text = Properties.Settings.Default.DefaultSecretQuest;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            cueTextBox2.Text = Properties.Settings.Default.DefaultSecretAnswer;
        }

        private void LoadMySqlSettings()
        {
            txtmySqlServer.Text = Properties.Settings.Default.MysqlServer;
            txtMySqlUser.Text = Properties.Settings.Default.MysqlUser;
            txtMySqlPassword.Text = Properties.Settings.Default.MysqlPass;
        }

        private void LoadStoreInfo()
        {
            txtStoreName.Text = Properties.Settings.Default.storeName;
            txtStoreAdd.Text = Properties.Settings.Default.storeAdd;
            txtStoreContact.Text = Properties.Settings.Default.storeNo;
        }

        private void LoadSummarySettings()
        {
            enablePrintSum.Checked = Properties.Settings.Default.printSum;
        }

        private void LoadInventorySettings()
        {
            enablePriceMod.Checked = Properties.Settings.Default.priceMod;
            enableQuanMod.Checked = Properties.Settings.Default.quantMod;
        }

        private void LoadSalesComputation()
        {
            numSalesTax.Text = Properties.Settings.Default.SalesTax.ToString();
            chkPrintReceipt.Checked = Properties.Settings.Default.SalesReceipt;
            salesEnableDiscount.Checked = Properties.Settings.Default.SalesDiscount;
        }

        private void LoadSupplierSettings()
        {
            defaultSupplier.Text = Properties.Settings.Default.DefaultSupplier;
            defaultSupplierAdd.Text = Properties.Settings.Default.DefaultSupplierAddress;
            defaultSupplierNo.Text = Properties.Settings.Default.DefaultSupplierNo;
            supplierAllowMulti.Checked = Properties.Settings.Default.AllowMultiSupplier;
        }

        private void LoadEmailSettings()
        {
            ownerEmail.Text = Properties.Settings.Default.EmailUser;
            ownerEmailPassword.Text = Properties.Settings.Default.EmailPassword;
            emailSendReport.Checked = Properties.Settings.Default.EmailSendMessage;
        }

        private void LoadAdminSettings()
        {
            adminUsername.Text = Properties.Settings.Default.DefaultAdminAccount;
            adminPassword.Text = Properties.Settings.Default.DefaultAdminPassword;
            adminPassword2.Text = Properties.Settings.Default.DefaultAdminPassword;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Do you want to save settings?","Save Settings?",MessageBoxButtons.YesNo,MessageBoxIcon.Hand);
            if (dialog == DialogResult.Yes)
            {
                SaveSettings();
                var result = MessageBox.Show("Settings have been change\n " +
                                 "do you want to restart the application?", "Restart now"
                                 , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
            Dispose();
        }

        private void SaveSettings()
        {
            SaveAdminSettings();
            SaveSecret();
            SaveEmailSettings();
            SaveMySqlSettings();
            SaveSupplierSettings();
            SaveSalesComputation();
            SaveInventorySettings();
            SaveSummarySettings();
            SaveStoreInfo();
        }

        private void SaveSecret()
        {
            Properties.Settings.Default.DefaultSecretQuest = comboBox1.SelectedItem.ToString();
            Properties.Settings.Default.DefaultSecretAnswer = cueTextBox2.Text;
            Properties.Settings.Default.Save();
        }

        private void SaveMySqlSettings()
        {
            Properties.Settings.Default.MysqlServer= txtmySqlServer.Text;
            Properties.Settings.Default.MysqlUser = txtMySqlUser.Text;
            Properties.Settings.Default.MysqlPass=txtMySqlPassword.Text;
            Properties.Settings.Default.Save();
        }

        private void SaveStoreInfo()
        {
           Properties.Settings.Default.storeName= txtStoreName.Text  ;
           Properties.Settings.Default.storeAdd= txtStoreAdd.Text  ;
           Properties.Settings.Default.storeNo=txtStoreContact.Text;
           Properties.Settings.Default.Save();
        }

        private void SaveSummarySettings()
        {
           Properties.Settings.Default.printSum= enablePrintSum.Checked  ;
           Properties.Settings.Default.Save();
        }

        private void SaveInventorySettings()
        {
            Properties.Settings.Default.priceMod=enablePriceMod.Checked  ;
          Properties.Settings.Default.quantMod=  enableQuanMod.Checked  ;
          Properties.Settings.Default.Save();
        }

        private void SaveSalesComputation()
        {
            Properties.Settings.Default.SalesTax= Convert.ToInt32(numSalesTax.Text) ;
           Properties.Settings.Default.SalesReceipt= chkPrintReceipt.Checked  ;
           Properties.Settings.Default.SalesDiscount=salesEnableDiscount.Checked;
           Properties.Settings.Default.Save();
        }

        private void SaveSupplierSettings()
        {
           Properties.Settings.Default.DefaultSupplier= defaultSupplier.Text  ;
           Properties.Settings.Default.DefaultSupplierAddress= defaultSupplierAdd.Text  ;
           Properties.Settings.Default.DefaultSupplierNo= defaultSupplierNo.Text  ;
           Properties.Settings.Default.AllowMultiSupplier= supplierAllowMulti.Checked  ;
           Properties.Settings.Default.Save();
        }

        private void SaveEmailSettings()
        {
           Properties.Settings.Default.EmailUser= ownerEmail.Text  ;
           Properties.Settings.Default.EmailPassword= ownerEmailPassword.Text  ;
           Properties.Settings.Default.EmailSendMessage= emailSendReport.Checked  ;
           Properties.Settings.Default.Save();
        }

        private void SaveAdminSettings()
        {
            Properties.Settings.Default.DefaultAdminAccount = adminUsername.Text;
            Properties.Settings.Default.DefaultAdminPassword=adminPassword.Text;
            Properties.Settings.Default.DefaultAdminPassword= adminPassword2.Text;
            Properties.Settings.Default.Save();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void emailSendReport_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            var task = Task.Run(() => SendTestMessage());
            task.Wait();
            button2.Enabled = true;
        }

        private void SendTestMessage()
        {
            try
            {
                var mailSender = ownerEmail.Text;
                var mailPassowrd = ownerEmailPassword.Text;
                var subject = "Test Email";
                var body = "This is a test email from Cots Sales and Inventory";
                var email = new Email(mailSender, mailPassowrd, subject, body);
                email.Send();
                MessageBox.Show("Message sending Sucess", "Message Sent!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Message sending error\n" + exception, "Sending Error"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestMySqlConnection();
        }

        private void TestMySqlConnection()
        {
            var mysqlServer = txtmySqlServer.Text;
            var mysqlUser = txtMySqlUser.Text;
            var mysqlPass = txtMySqlPassword.Text;
            var conString = "SERVER=" + mysqlServer + ";" +
                        "DATABASE=" + "cotsalesinventory" + ";" +
                        "UID=" + mysqlUser + ";" +
                        "PASSWORD=" + mysqlPass;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(conString);
                conn.Open();
                MessageBox.Show("MySql Connected");
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to connect to Database");
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
