using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Logon : Form
    {
        DatabaseConnection dataCon = new DatabaseConnection();
        public Logon()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            EnterMain();
        }

        private void EnterMain()
        {
            var username = cueTextBox1.Text;
            var password = cueTextBox2.Text;
            var defaultuser = Properties.Settings.Default.DefaultAdminAccount;
            var defaultpass = Properties.Settings.Default.DefaultAdminPassword;

            if (username.Equals(Properties.Settings.Default.DefaultAdminAccount)
                && password.Equals(Properties.Settings.Default.DefaultAdminPassword))
            {
                GoToMain(username, "Admin");
            }
            else if (username != "" && password != "")
            {
                var found = SearchforAccount(username);

                if (found.Length > 0)
                {
                    if (found[0]["accountName"].ToString().Equals(username))
                    {
                        if (found[0]["accountpassword"].ToString().Equals(password))
                        {
                            GoToMain(username, GetAccountType(found[0]["AccountType"].ToString()));
                        }
                        else
                        {
                            MessageBox.Show("incorrect password", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Account Does Not Exist", "Account Doesn't Exist"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("incorrect username/password", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetAccountType(string accountType)
        {
            var account = Convert.ToInt32(accountType);
            switch (account)
            {
                case 1:
                    return "Sales Person";
                case 2:
                    return "Sales Manager";
                case 3:
                    return "Stock Man";
                case 4:
                    return "Stock Manager";
                case 5:
                    return "Administrator";
            }
            return "Admin";
        }

        private DataRow[] SearchforAccount(string username)
        {
            var found = DatabaseConnection.DatabaseRecord.Tables["account"].Select("AccountName ='"
                +username+"'");
            return found;
        }

        private void GoToMain( string username, string accounttype)
        {
                Hide();
                var main = new Main(this,username,accounttype);
                main.Show();
        }

        private void lnkForget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecoverPass();
        }

        private void RecoverPass()
        {
            ForgotPass forgotPass = new ForgotPass();
            forgotPass.Show();
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LoginKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                EnterMain();
            }
        }

    }
}
