using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    
    public partial class Loading_Screen : Form
    {

        public int progressValue
        {
            get { return progressBar1.Value; }
            set { progressBar1.Value = value; }
        }
        public Loading_Screen()
        {
            InitializeComponent();
        }

        private void Loading_Screen_Load(object sender, EventArgs e)
        {
        }

        private int _noOfDots;
        private string _dots;
        private void timer1_Tick(object sender, EventArgs e)
        {
            _dots += ".";
            
            label1.Text = "Loading Application Please Wait" + _dots;
            label1.Update();

            if (_noOfDots >= 3)
            {
                _dots = "";
                _noOfDots = 0;
            }
            _noOfDots++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            /*progressBar1.Increment(5);
            
            if (progressBar1.Value == 100)
            {
                timer2.Stop();
                Thread.Sleep(2000);
                Close();
            }*/
        }

        public void UpdateProgressBar(int value)
        {
            progressBar1.Value = value;
            progressBar1.Refresh();
            Thread.Sleep(300);
            if (value == 100)
            {
                Close();
            }
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
               
    }
}
