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
    public partial class ForgotPass : Form
    {
        public ForgotPass()
        {
            InitializeComponent();
        }

        private void ForgotPass_Load(object sender, EventArgs e)
        {
            cueTextBox1.Text = Properties.Settings.Default.DefaultSecretQuest;
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var task = Task.Run(() => SendUsernameAndPassword());
            task.Wait();
            button1.Enabled = true;
        }

        private void SendUsernameAndPassword()
        {
            var emailAcc = Properties.Settings.Default.EmailUser;
            var emailPass = Properties.Settings.Default.EmailPassword;
            var subject = "Password Recovery";
            var body = "Username: "+Properties.Settings.Default.DefaultAdminAccount+"\n"
                +"Password: "+Properties.Settings.Default.DefaultAdminPassword;
            try
            {
                var email = new Email(emailAcc, emailPass, subject, body);
                email.Send();
                MessageBox.Show("Your Username/Password \nhas been sent to your email");
            }
            catch (Exception e)
            {
                
            }
        }

        private void cueTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cueTextBox2.Text.Equals(Properties.Settings.Default.DefaultSecretAnswer))
                {
                    button1.Enabled = true;
                }
            }
        }

    }
}
