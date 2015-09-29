using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public Loading_Screen()
        {
            InitializeComponent();
        }

        private void Loading_Screen_Load(object sender, EventArgs e)
        {
            timer2.Start();
            var task = Task.Run(() => LoadCrystal());
            while (true)
            {
                if (task.IsCompleted)
                {
                    progressBar1.Value = progressBar1.Maximum;
                    Dispose();
                }
            }
            
        }


        private void LoadCrystal()
        {
            var report = new Print_Orders(new DataSet());
        }

        private int _noOfDots;
        private string _dots;
        private void timer1_Tick(object sender, EventArgs e)
        {
            _dots += ".";
            
            label1.Text = "Loading Data Please Wait" + _dots;
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
            progressBar1.Increment(1);
            if (progressBar1.Value == 500)
            {
                timer2.Stop();
            }
        }
    }
}
