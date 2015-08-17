using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public partial class Logon : Form
    {
        public Logon()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            GoToMain(true);
        }

        private void GoToMain(Boolean allowed)
        {
            if (allowed)
            {
                Hide();
                Main main = new Main();
                main.Show();
            }
        }

        private void lnkForget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecoverPass();
        }

        private void RecoverPass()
        {
            this.Hide();
            ForgotPass forgotPass = new ForgotPass();
            forgotPass.Show();
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            var dataCon = new DatabaseConnection();
        }

    }
}
