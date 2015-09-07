﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace COTS_Sales_And_Inventory_System
{
    public class CueComboBox : ComboBox
    {
        [Localizable(true)]
        public string Cue
        {
            get { return _mCue; }
            set { _mCue = value; UpdateCue(); }
        }

        private void UpdateCue()
        {
            if (this.IsHandleCreated && _mCue != null)
            {
                SendMessage(this.Handle, 0x1501, (IntPtr)1, _mCue);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateCue();
        }
        private string _mCue;

        // PInvoke
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
    }
}