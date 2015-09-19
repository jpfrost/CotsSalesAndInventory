namespace COTS_Sales_And_Inventory_System
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.adminGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.emailSendReport = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.salesEnableDiscount = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numSalesTax = new System.Windows.Forms.NumericUpDown();
            this.chkPrintReceipt = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.enableQuanMod = new System.Windows.Forms.CheckBox();
            this.enablePriceMod = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.enablePrintSum = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.supplierAllowMulti = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMySqlPassword = new CueTextBox();
            this.txtMySqlUser = new CueTextBox();
            this.txtmySqlServer = new CueTextBox();
            this.txtStoreContact = new CueTextBox();
            this.txtStoreAdd = new CueTextBox();
            this.txtStoreName = new CueTextBox();
            this.defaultSupplierNo = new CueTextBox();
            this.defaultSupplierAdd = new CueTextBox();
            this.defaultSupplier = new CueTextBox();
            this.cueTextBox2 = new CueTextBox();
            this.ownerEmailPassword = new CueTextBox();
            this.ownerEmail = new CueTextBox();
            this.adminPassword2 = new CueTextBox();
            this.adminPassword = new CueTextBox();
            this.adminUsername = new CueTextBox();
            this.adminGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalesTax)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminGroupBox
            // 
            this.adminGroupBox.Controls.Add(this.adminPassword2);
            this.adminGroupBox.Controls.Add(this.adminPassword);
            this.adminGroupBox.Controls.Add(this.adminUsername);
            this.adminGroupBox.Location = new System.Drawing.Point(23, 26);
            this.adminGroupBox.Name = "adminGroupBox";
            this.adminGroupBox.Size = new System.Drawing.Size(250, 135);
            this.adminGroupBox.TabIndex = 0;
            this.adminGroupBox.TabStop = false;
            this.adminGroupBox.Text = "Admin Control";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.cueTextBox2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.emailSendReport);
            this.groupBox1.Controls.Add(this.ownerEmailPassword);
            this.groupBox1.Controls.Add(this.ownerEmail);
            this.groupBox1.Location = new System.Drawing.Point(282, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 284);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Owner Control";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Send Test Email";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // emailSendReport
            // 
            this.emailSendReport.AutoSize = true;
            this.emailSendReport.Location = new System.Drawing.Point(6, 83);
            this.emailSendReport.Name = "emailSendReport";
            this.emailSendReport.Size = new System.Drawing.Size(177, 17);
            this.emailSendReport.TabIndex = 2;
            this.emailSendReport.Text = "Send Email Before Exit/Logout?";
            this.emailSendReport.UseVisualStyleBackColor = true;
            this.emailSendReport.CheckedChanged += new System.EventHandler(this.emailSendReport_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.salesEnableDiscount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numSalesTax);
            this.groupBox2.Controls.Add(this.chkPrintReceipt);
            this.groupBox2.Location = new System.Drawing.Point(23, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 176);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sales Control";
            // 
            // salesEnableDiscount
            // 
            this.salesEnableDiscount.AutoSize = true;
            this.salesEnableDiscount.Location = new System.Drawing.Point(32, 101);
            this.salesEnableDiscount.Name = "salesEnableDiscount";
            this.salesEnableDiscount.Size = new System.Drawing.Size(104, 17);
            this.salesEnableDiscount.TabIndex = 3;
            this.salesEnableDiscount.Text = "Enable Discount";
            this.salesEnableDiscount.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set Computation Tax (%):";
            // 
            // numSalesTax
            // 
            this.numSalesTax.Location = new System.Drawing.Point(160, 34);
            this.numSalesTax.Name = "numSalesTax";
            this.numSalesTax.Size = new System.Drawing.Size(58, 20);
            this.numSalesTax.TabIndex = 1;
            this.numSalesTax.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // chkPrintReceipt
            // 
            this.chkPrintReceipt.AutoSize = true;
            this.chkPrintReceipt.Location = new System.Drawing.Point(32, 69);
            this.chkPrintReceipt.Name = "chkPrintReceipt";
            this.chkPrintReceipt.Size = new System.Drawing.Size(171, 17);
            this.chkPrintReceipt.TabIndex = 0;
            this.chkPrintReceipt.Text = "Print Receipt After Transaction";
            this.chkPrintReceipt.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.enableQuanMod);
            this.groupBox3.Controls.Add(this.enablePriceMod);
            this.groupBox3.Location = new System.Drawing.Point(23, 353);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 98);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Inventory Control";
            // 
            // enableQuanMod
            // 
            this.enableQuanMod.AutoSize = true;
            this.enableQuanMod.Location = new System.Drawing.Point(20, 69);
            this.enableQuanMod.Name = "enableQuanMod";
            this.enableQuanMod.Size = new System.Drawing.Size(161, 17);
            this.enableQuanMod.TabIndex = 1;
            this.enableQuanMod.Text = "Enable Modifying of Quantity";
            this.enableQuanMod.UseVisualStyleBackColor = true;
            // 
            // enablePriceMod
            // 
            this.enablePriceMod.AutoSize = true;
            this.enablePriceMod.Location = new System.Drawing.Point(20, 37);
            this.enablePriceMod.Name = "enablePriceMod";
            this.enablePriceMod.Size = new System.Drawing.Size(146, 17);
            this.enablePriceMod.TabIndex = 0;
            this.enablePriceMod.Text = "Enable Modifying of Price";
            this.enablePriceMod.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.enablePrintSum);
            this.groupBox4.Location = new System.Drawing.Point(513, 277);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(217, 140);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Summary Control";
            // 
            // enablePrintSum
            // 
            this.enablePrintSum.AutoSize = true;
            this.enablePrintSum.Location = new System.Drawing.Point(22, 29);
            this.enablePrintSum.Name = "enablePrintSum";
            this.enablePrintSum.Size = new System.Drawing.Size(155, 17);
            this.enablePrintSum.TabIndex = 2;
            this.enablePrintSum.Text = "Enable Printing of Summary";
            this.enablePrintSum.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(655, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.supplierAllowMulti);
            this.groupBox5.Controls.Add(this.defaultSupplierNo);
            this.groupBox5.Controls.Add(this.defaultSupplierAdd);
            this.groupBox5.Controls.Add(this.defaultSupplier);
            this.groupBox5.Location = new System.Drawing.Point(282, 317);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(217, 134);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Program Defaults";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // supplierAllowMulti
            // 
            this.supplierAllowMulti.AutoSize = true;
            this.supplierAllowMulti.Location = new System.Drawing.Point(6, 106);
            this.supplierAllowMulti.Name = "supplierAllowMulti";
            this.supplierAllowMulti.Size = new System.Drawing.Size(131, 17);
            this.supplierAllowMulti.TabIndex = 3;
            this.supplierAllowMulti.Text = "Allow Multiple Supplier";
            this.supplierAllowMulti.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtStoreContact);
            this.groupBox6.Controls.Add(this.txtStoreAdd);
            this.groupBox6.Controls.Add(this.txtStoreName);
            this.groupBox6.Location = new System.Drawing.Point(513, 173);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(217, 98);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Store Information";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.txtMySqlPassword);
            this.groupBox7.Controls.Add(this.txtMySqlUser);
            this.groupBox7.Controls.Add(this.txtmySqlServer);
            this.groupBox7.Location = new System.Drawing.Point(513, 29);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(217, 135);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Mysql Connection";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(7, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(204, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Test Sql Connection";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "What is your mothers name?",
            "Who is your first crush?",
            "Name of your pet?",
            "What is your mobile no?"});
            this.comboBox1.Location = new System.Drawing.Point(6, 165);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Select Secret Question:";
            // 
            // txtMySqlPassword
            // 
            this.txtMySqlPassword.Cue = "MySql Password";
            this.txtMySqlPassword.Location = new System.Drawing.Point(6, 78);
            this.txtMySqlPassword.Name = "txtMySqlPassword";
            this.txtMySqlPassword.PasswordChar = '*';
            this.txtMySqlPassword.Size = new System.Drawing.Size(205, 20);
            this.txtMySqlPassword.TabIndex = 2;
            // 
            // txtMySqlUser
            // 
            this.txtMySqlUser.Cue = "MySql User";
            this.txtMySqlUser.Location = new System.Drawing.Point(6, 52);
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.Size = new System.Drawing.Size(205, 20);
            this.txtMySqlUser.TabIndex = 1;
            // 
            // txtmySqlServer
            // 
            this.txtmySqlServer.Cue = "MySql Server";
            this.txtmySqlServer.Location = new System.Drawing.Point(6, 26);
            this.txtmySqlServer.Name = "txtmySqlServer";
            this.txtmySqlServer.Size = new System.Drawing.Size(205, 20);
            this.txtmySqlServer.TabIndex = 0;
            // 
            // txtStoreContact
            // 
            this.txtStoreContact.Cue = "Store Contact No.";
            this.txtStoreContact.Location = new System.Drawing.Point(6, 71);
            this.txtStoreContact.Name = "txtStoreContact";
            this.txtStoreContact.Size = new System.Drawing.Size(205, 20);
            this.txtStoreContact.TabIndex = 3;
            // 
            // txtStoreAdd
            // 
            this.txtStoreAdd.Cue = "Store Address";
            this.txtStoreAdd.Location = new System.Drawing.Point(6, 45);
            this.txtStoreAdd.Name = "txtStoreAdd";
            this.txtStoreAdd.Size = new System.Drawing.Size(205, 20);
            this.txtStoreAdd.TabIndex = 2;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Cue = "Store Name";
            this.txtStoreName.Location = new System.Drawing.Point(6, 19);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(205, 20);
            this.txtStoreName.TabIndex = 1;
            // 
            // defaultSupplierNo
            // 
            this.defaultSupplierNo.Cue = "Default Supplier No.";
            this.defaultSupplierNo.Location = new System.Drawing.Point(6, 80);
            this.defaultSupplierNo.Name = "defaultSupplierNo";
            this.defaultSupplierNo.Size = new System.Drawing.Size(205, 20);
            this.defaultSupplierNo.TabIndex = 2;
            // 
            // defaultSupplierAdd
            // 
            this.defaultSupplierAdd.Cue = "Default Supplier Address";
            this.defaultSupplierAdd.Location = new System.Drawing.Point(6, 54);
            this.defaultSupplierAdd.Name = "defaultSupplierAdd";
            this.defaultSupplierAdd.Size = new System.Drawing.Size(205, 20);
            this.defaultSupplierAdd.TabIndex = 1;
            // 
            // defaultSupplier
            // 
            this.defaultSupplier.Cue = "Default Supplier";
            this.defaultSupplier.Location = new System.Drawing.Point(6, 28);
            this.defaultSupplier.Name = "defaultSupplier";
            this.defaultSupplier.Size = new System.Drawing.Size(205, 20);
            this.defaultSupplier.TabIndex = 0;
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = "Secret Answer";
            this.cueTextBox2.Location = new System.Drawing.Point(7, 192);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.PasswordChar = '*';
            this.cueTextBox2.Size = new System.Drawing.Size(198, 20);
            this.cueTextBox2.TabIndex = 9;
            // 
            // ownerEmailPassword
            // 
            this.ownerEmailPassword.Cue = "Enter Owner\'s Email Address Password";
            this.ownerEmailPassword.Location = new System.Drawing.Point(6, 54);
            this.ownerEmailPassword.Name = "ownerEmailPassword";
            this.ownerEmailPassword.PasswordChar = '*';
            this.ownerEmailPassword.Size = new System.Drawing.Size(199, 20);
            this.ownerEmailPassword.TabIndex = 1;
            // 
            // ownerEmail
            // 
            this.ownerEmail.Cue = "Enter Owner\'s Email Address (Gmail Only)";
            this.ownerEmail.Location = new System.Drawing.Point(6, 28);
            this.ownerEmail.Name = "ownerEmail";
            this.ownerEmail.Size = new System.Drawing.Size(199, 20);
            this.ownerEmail.TabIndex = 0;
            // 
            // adminPassword2
            // 
            this.adminPassword2.Cue = "Re Enter Password";
            this.adminPassword2.Location = new System.Drawing.Point(32, 81);
            this.adminPassword2.Name = "adminPassword2";
            this.adminPassword2.PasswordChar = '*';
            this.adminPassword2.Size = new System.Drawing.Size(186, 20);
            this.adminPassword2.TabIndex = 2;
            // 
            // adminPassword
            // 
            this.adminPassword.Cue = "Password";
            this.adminPassword.Location = new System.Drawing.Point(32, 55);
            this.adminPassword.Name = "adminPassword";
            this.adminPassword.PasswordChar = '*';
            this.adminPassword.Size = new System.Drawing.Size(186, 20);
            this.adminPassword.TabIndex = 1;
            // 
            // adminUsername
            // 
            this.adminUsername.Cue = "Username";
            this.adminUsername.Location = new System.Drawing.Point(32, 29);
            this.adminUsername.Name = "adminUsername";
            this.adminUsername.Size = new System.Drawing.Size(186, 20);
            this.adminUsername.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 457);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.adminGroupBox);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.adminGroupBox.ResumeLayout(false);
            this.adminGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalesTax)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox adminGroupBox;
        private CueTextBox adminPassword2;
        private CueTextBox adminPassword;
        private CueTextBox adminUsername;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox salesEnableDiscount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSalesTax;
        private System.Windows.Forms.CheckBox chkPrintReceipt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox emailSendReport;
        private CueTextBox ownerEmailPassword;
        private CueTextBox ownerEmail;
        private System.Windows.Forms.CheckBox enableQuanMod;
        private System.Windows.Forms.CheckBox enablePriceMod;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox enablePrintSum;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox supplierAllowMulti;
        private CueTextBox defaultSupplierNo;
        private CueTextBox defaultSupplierAdd;
        private CueTextBox defaultSupplier;
        private System.Windows.Forms.GroupBox groupBox6;
        private CueTextBox txtStoreContact;
        private CueTextBox txtStoreAdd;
        private CueTextBox txtStoreName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox7;
        private CueTextBox txtMySqlPassword;
        private CueTextBox txtMySqlUser;
        private CueTextBox txtmySqlServer;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private CueTextBox cueTextBox2;
    }
}