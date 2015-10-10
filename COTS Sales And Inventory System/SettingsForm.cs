using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using COTS_Sales_And_Inventory_System.Properties;
using MySql.Data.MySqlClient;

namespace COTS_Sales_And_Inventory_System
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
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
            LoadLogo();
            LoadCritLevel();
            button2.Enabled = false;
            emailSendReport.Enabled = false;
            emailSendReport.Checked = false;
        }

        private void LoadCritLevel()
        {
            cueTxtQuanMedian.Text = Settings.Default.CritMedian.ToString();
            trackLowQuanItem.Value = Convert.ToInt32(Settings.Default.critLowLevel);
            trackHighLevelQuan.Value = Convert.ToInt32(Settings.Default.critHighLevel);
            numericUpDown1.Value = Convert.ToInt32(Settings.Default.critLowLevel);
            numericUpDown2.Value = Convert.ToInt32(Settings.Default.critHighLevel);
            trackLowQuanItem.Refresh();
            trackHighLevelQuan.Refresh();
        }

        private void LoadLogo()
        {
            try
            {
                pictureBox1.ImageLocation = @"logo.png";
                checkBox1.Checked = Settings.Default.EnCompanyLogo;
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        private void LoadSecret()
        {
            try
            {
                comboBox1.Text = Settings.Default.DefaultSecretQuest;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            cueTextBox2.Text = Settings.Default.DefaultSecretAnswer;
        }

        private void LoadMySqlSettings()
        {
            txtmySqlServer.Text = Settings.Default.MysqlServer;
            txtMySqlUser.Text = Settings.Default.MysqlUser;
            txtMySqlPassword.Text = Settings.Default.MysqlPass;
        }

        private void LoadStoreInfo()
        {
            txtStoreName.Text = Settings.Default.storeName;
            txtStoreAdd.Text = Settings.Default.storeAdd;
            txtStoreContact.Text = Settings.Default.storeNo;
        }

        private void LoadSummarySettings()
        {
            enablePrintSum.Checked = Settings.Default.printSum;
        }

        private void LoadInventorySettings()
        {
            enablePriceMod.Checked = Settings.Default.priceMod;
            enableQuanMod.Checked = Settings.Default.quantMod;
            enOrderMod.Checked = Settings.Default.EnableOrdering;
        }

        private void LoadSalesComputation()
        {
            numSalesTax.Text = Settings.Default.SalesTax.ToString();
            chkPrintReceipt.Checked = Settings.Default.SalesReceipt;
            salesEnableDiscount.Checked = Settings.Default.SalesDiscount;
        }

        private void LoadSupplierSettings()
        {
            defaultSupplier.Text = Settings.Default.DefaultSupplier;
            defaultSupplierAdd.Text = Settings.Default.DefaultSupplierAddress;
            defaultSupplierNo.Text = Settings.Default.DefaultSupplierNo;
            supplierAllowMulti.Checked = Settings.Default.AllowMultiSupplier;
        }

        private void LoadEmailSettings()
        {
            ownerEmail.Text = Settings.Default.EmailUser;
            ownerEmailPassword.Text = Settings.Default.EmailPassword;
            emailSendReport.Checked = Settings.Default.EmailSendMessage;
        }

        private void LoadAdminSettings()
        {
            adminUsername.Text = Settings.Default.DefaultAdminAccount;
            adminPassword.Text = Settings.Default.DefaultAdminPassword;
            adminPassword2.Text = Settings.Default.DefaultAdminPassword;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Settings.Default.FirstRun)
            {
                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }
            var dialog = MessageBox.Show("Do you want to save settings?", "Save Settings?", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                SaveSettings();
                var result = MessageBox.Show("Settings have been change\n " +
                                             "Please Close the application and Open Again?", "Close now"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
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
            SaveLogo();
            SaveCritLevel();
        }

        private void SaveCritLevel()
        {
            try
            {
                Settings.Default.CritMedian = Convert.ToInt32(cueTxtQuanMedian.Text);
                Settings.Default.critLowLevel = Convert.ToInt32(trackLowQuanItem.Value);
                Settings.Default.critHighLevel = Convert.ToInt32(trackHighLevelQuan.Value);
                Settings.Default.Save();
            }
            catch (Exception e)
            {
                //ignored
            }
        }

        private void SaveLogo()
        {
            pictureBox1.Image.Save(@"logo.png", ImageFormat.Png);
            Settings.Default.EnCompanyLogo = checkBox1.Checked;
            Settings.Default.Save();
        }

        private void SaveSecret()
        {
            Settings.Default.DefaultSecretQuest = comboBox1.Text;
            Settings.Default.DefaultSecretAnswer = cueTextBox2.Text;
            Settings.Default.Save();
        }

        private void SaveMySqlSettings()
        {
            Settings.Default.MysqlServer = txtmySqlServer.Text;
            Settings.Default.MysqlUser = txtMySqlUser.Text;
            Settings.Default.MysqlPass = txtMySqlPassword.Text;
            Settings.Default.Save();
        }

        private void SaveStoreInfo()
        {
            Settings.Default.storeName = txtStoreName.Text;
            Settings.Default.storeAdd = txtStoreAdd.Text;
            Settings.Default.storeNo = txtStoreContact.Text;
            Settings.Default.Save();
        }

        private void SaveSummarySettings()
        {
            Settings.Default.printSum = enablePrintSum.Checked;
            Settings.Default.Save();
        }

        private void SaveInventorySettings()
        {
            Settings.Default.priceMod = enablePriceMod.Checked;
            Settings.Default.quantMod = enableQuanMod.Checked;
            Settings.Default.EnableOrdering = enOrderMod.Checked;
            Settings.Default.Save();
        }

        private void SaveSalesComputation()
        {
            Settings.Default.SalesTax = Convert.ToInt32(numSalesTax.Text);
            Settings.Default.SalesReceipt = chkPrintReceipt.Checked;
            Settings.Default.SalesDiscount = salesEnableDiscount.Checked;
            Settings.Default.Save();
        }

        private void SaveSupplierSettings()
        {
            Settings.Default.DefaultSupplier = defaultSupplier.Text;
            Settings.Default.DefaultSupplierAddress = defaultSupplierAdd.Text;
            Settings.Default.DefaultSupplierNo = defaultSupplierNo.Text;
            Settings.Default.AllowMultiSupplier = supplierAllowMulti.Checked;
            Settings.Default.Save();
            ModifydistroDatabase();
        }

        private void ModifydistroDatabase()
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["distributor"].Select("DistroID ='" + 1 + "'");
            if (found.Length == 0)
            {
                CreateDefaultDistro();
            }
            else
            {
                found[0]["distroName"] = defaultSupplier.Text;
                found[0]["DistroAddress"] = defaultSupplierAdd.Text;
                found[0]["DistroContact"] = defaultSupplierNo.Text;
                found[0]["distroEnable"] = 1;
                DatabaseConnection.UploadChanges();
            }
        }

        private void CreateDefaultDistro()
        {
            var newDistroRow = DatabaseConnection.DatabaseRecord.Tables["distributor"].NewRow();
            newDistroRow["distroID"] = 1;
            newDistroRow["distroName"] = defaultSupplier.Text;
            newDistroRow["DistroAddress"] = defaultSupplierAdd.Text;
            newDistroRow["DistroContact"] = defaultSupplierNo.Text;
            newDistroRow["distroEnable"] = 1;
            DatabaseConnection.DatabaseRecord.Tables["distributor"].Rows.Add(newDistroRow);
            DatabaseConnection.UploadChanges();
        }

        private void SaveEmailSettings()
        {
            Settings.Default.EmailUser = ownerEmail.Text;
            Settings.Default.EmailPassword = ownerEmailPassword.Text;
            Settings.Default.EmailSendMessage = emailSendReport.Checked;
            Settings.Default.Save();
        }

        private void SaveAdminSettings()
        {
            Settings.Default.DefaultAdminAccount = adminUsername.Text;
            Settings.Default.DefaultAdminPassword = adminPassword.Text;
            Settings.Default.DefaultAdminPassword = adminPassword2.Text;
            Settings.Default.Save();
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
                MessageBox.Show(@"Message sending Sucess", @"Message Sent!",
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
                if (conn.State.ToString().Equals("Open"))
                {
                    conn.Close();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetSystem();
        }

        private void ResetSystem()
        {
            var dialog = MessageBox.Show("This will restore the system to its default values", "System Reset",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                Settings.Default.DefaultAdminAccount = "admin";
                Settings.Default.DefaultAdminPassword = "password";
                Settings.Default.DefaultSecretAnswer = "";
                Settings.Default.DefaultSecretQuest = "";
                Settings.Default.DefaultSupplier = "";
                Settings.Default.DefaultSupplierAddress = "";
                Settings.Default.DefaultSupplierNo = "";
                Settings.Default.EmailUser = "";
                Settings.Default.EmailPassword = "";
                Settings.Default.storeName = "";
                Settings.Default.storeAdd = "";
                Settings.Default.storeNo = "";
                Settings.Default.FirstRun = true;
                Settings.Default.Save();
                Application.Exit();
            }
        }

        private void emailSendReport_Click(object sender, EventArgs e)
        {
        }

        private void ownerEmail_TextChanged(object sender, EventArgs e)
        {
            if (!ownerEmail.Text.Equals("") && !ownerEmailPassword.Text.Equals(""))
            {
                button2.Enabled = true;
                emailSendReport.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                emailSendReport.Enabled = false;
                emailSendReport.Checked = false;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            button5.Enabled = false;
            var dialog =
                MessageBox.Show(
                    "Are you sure you want to remove all data from the database \nThis data cannot be recovered..."
                    , @"Remove All Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                DatabaseConnection.RecreateMysqlDatabase();
                MessageBox.Show("All the Data from the Database has been remove", "Database Cleared",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            button5.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BrowseAndChangeImage();
        }

        private void BrowseAndChangeImage()
        {
            var image = new OpenFileDialog();
            image.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (image.ShowDialog() != DialogResult.OK) return;
            var img = new Bitmap(image.OpenFile());
            pictureBox1.Image = img;
        }

        private void cueTxtQuanMedian_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar=='\b'))
            {
                e.Handled = true;
            }
        }

        private void trackLowQuanItem_Scroll(object sender, EventArgs e)
        {
        }

        private void trackLowQuanItem_Scroll_1(object sender, EventArgs e)
        {
            ChangeCueTextValue(trackLowQuanItem, numericUpDown1);
        }

        private void ChangeCueTextValue(TrackBar trackBar, NumericUpDown numeric)
        {
            numeric.Text = trackBar.Value.ToString();
        }

        private void trackHighLevelQuan_Scroll(object sender, EventArgs e)
        {
            ChangeCueTextValue(trackHighLevelQuan,numericUpDown2);
        }

        private void NumTextToTrack(NumericUpDown numeric, TrackBar trackBar)
        {
            try
            {
                trackBar.Value = Convert.ToInt32(numeric.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            NumTextToTrack(numericUpDown1,trackLowQuanItem);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            NumTextToTrack(numericUpDown2,trackHighLevelQuan);
        }

        private void numericUpDown2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                NumTextToTrack(numericUpDown2, trackHighLevelQuan);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                NumTextToTrack(numericUpDown1, trackLowQuanItem);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var dialog = new CategoryUnitEdit();
            dialog.ShowDialog();
        }


    }
}