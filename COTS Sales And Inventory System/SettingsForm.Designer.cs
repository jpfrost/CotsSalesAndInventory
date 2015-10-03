namespace COTS_Sales_And_Inventory_System
{
    partial class SettingsForm
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
            this.adminPassword2 = new CueTextBox();
            this.adminPassword = new CueTextBox();
            this.adminUsername = new CueTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cueTextBox2 = new CueTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.emailSendReport = new System.Windows.Forms.CheckBox();
            this.ownerEmailPassword = new CueTextBox();
            this.ownerEmail = new CueTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.salesEnableDiscount = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numSalesTax = new System.Windows.Forms.NumericUpDown();
            this.chkPrintReceipt = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.enOrderMod = new System.Windows.Forms.CheckBox();
            this.enableQuanMod = new System.Windows.Forms.CheckBox();
            this.enablePriceMod = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.enablePrintSum = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.supplierAllowMulti = new System.Windows.Forms.CheckBox();
            this.defaultSupplierNo = new CueTextBox();
            this.defaultSupplierAdd = new CueTextBox();
            this.defaultSupplier = new CueTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtStoreContact = new CueTextBox();
            this.txtStoreAdd = new CueTextBox();
            this.txtStoreName = new CueTextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtMySqlPassword = new CueTextBox();
            this.txtMySqlUser = new CueTextBox();
            this.txtmySqlServer = new CueTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.adminGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSalesTax)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // adminGroupBox
            // 
            this.adminGroupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.adminGroupBox.Controls.Add(this.adminPassword2);
            this.adminGroupBox.Controls.Add(this.adminPassword);
            this.adminGroupBox.Controls.Add(this.adminUsername);
            this.adminGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adminGroupBox.Location = new System.Drawing.Point(23, 26);
            this.adminGroupBox.Name = "adminGroupBox";
            this.adminGroupBox.Size = new System.Drawing.Size(250, 135);
            this.adminGroupBox.TabIndex = 0;
            this.adminGroupBox.TabStop = false;
            this.adminGroupBox.Text = "Admin Control";
            // 
            // adminPassword2
            // 
            this.adminPassword2.Cue = "Re Enter Password";
            this.adminPassword2.Location = new System.Drawing.Point(32, 81);
            this.adminPassword2.Name = "adminPassword2";
            this.adminPassword2.PasswordChar = '*';
            this.adminPassword2.Size = new System.Drawing.Size(186, 22);
            this.adminPassword2.TabIndex = 2;
            // 
            // adminPassword
            // 
            this.adminPassword.Cue = "Password";
            this.adminPassword.Location = new System.Drawing.Point(32, 55);
            this.adminPassword.Name = "adminPassword";
            this.adminPassword.PasswordChar = '*';
            this.adminPassword.Size = new System.Drawing.Size(186, 22);
            this.adminPassword.TabIndex = 1;
            // 
            // adminUsername
            // 
            this.adminUsername.Cue = "Username";
            this.adminUsername.Location = new System.Drawing.Point(32, 29);
            this.adminUsername.Name = "adminUsername";
            this.adminUsername.Size = new System.Drawing.Size(186, 22);
            this.adminUsername.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.cueTextBox2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.emailSendReport);
            this.groupBox1.Controls.Add(this.ownerEmailPassword);
            this.groupBox1.Controls.Add(this.ownerEmail);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(282, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 248);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Owner Control";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Select Secret Question:";
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
            this.comboBox1.Location = new System.Drawing.Point(9, 163);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 24);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = "Secret Answer";
            this.cueTextBox2.Location = new System.Drawing.Point(9, 200);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.PasswordChar = '*';
            this.cueTextBox2.Size = new System.Drawing.Size(198, 22);
            this.cueTextBox2.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Tomato;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(6, 105);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Send Test Email";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // emailSendReport
            // 
            this.emailSendReport.AutoSize = true;
            this.emailSendReport.Location = new System.Drawing.Point(6, 83);
            this.emailSendReport.Name = "emailSendReport";
            this.emailSendReport.Size = new System.Drawing.Size(215, 20);
            this.emailSendReport.TabIndex = 2;
            this.emailSendReport.Text = "Send Email Before Exit/Logout?";
            this.emailSendReport.UseVisualStyleBackColor = true;
            this.emailSendReport.CheckedChanged += new System.EventHandler(this.emailSendReport_CheckedChanged);
            this.emailSendReport.Click += new System.EventHandler(this.emailSendReport_Click);
            // 
            // ownerEmailPassword
            // 
            this.ownerEmailPassword.Cue = "Enter Owner\'s Email Address Password";
            this.ownerEmailPassword.Location = new System.Drawing.Point(6, 54);
            this.ownerEmailPassword.Name = "ownerEmailPassword";
            this.ownerEmailPassword.PasswordChar = '*';
            this.ownerEmailPassword.Size = new System.Drawing.Size(199, 22);
            this.ownerEmailPassword.TabIndex = 1;
            this.ownerEmailPassword.TextChanged += new System.EventHandler(this.ownerEmail_TextChanged);
            // 
            // ownerEmail
            // 
            this.ownerEmail.Cue = "Enter Owner\'s Email Address (Gmail Only)";
            this.ownerEmail.Location = new System.Drawing.Point(6, 28);
            this.ownerEmail.Name = "ownerEmail";
            this.ownerEmail.Size = new System.Drawing.Size(199, 22);
            this.ownerEmail.TabIndex = 0;
            this.ownerEmail.TextChanged += new System.EventHandler(this.ownerEmail_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Controls.Add(this.salesEnableDiscount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numSalesTax);
            this.groupBox2.Controls.Add(this.chkPrintReceipt);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(23, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 164);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sales Control";
            // 
            // salesEnableDiscount
            // 
            this.salesEnableDiscount.AutoSize = true;
            this.salesEnableDiscount.Location = new System.Drawing.Point(32, 101);
            this.salesEnableDiscount.Name = "salesEnableDiscount";
            this.salesEnableDiscount.Size = new System.Drawing.Size(125, 20);
            this.salesEnableDiscount.TabIndex = 3;
            this.salesEnableDiscount.Text = "Enable Discount";
            this.salesEnableDiscount.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set Computation Tax (%):";
            // 
            // numSalesTax
            // 
            this.numSalesTax.Location = new System.Drawing.Point(181, 34);
            this.numSalesTax.Name = "numSalesTax";
            this.numSalesTax.Size = new System.Drawing.Size(58, 22);
            this.numSalesTax.TabIndex = 1;
            this.numSalesTax.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // chkPrintReceipt
            // 
            this.chkPrintReceipt.AutoSize = true;
            this.chkPrintReceipt.Location = new System.Drawing.Point(32, 69);
            this.chkPrintReceipt.Name = "chkPrintReceipt";
            this.chkPrintReceipt.Size = new System.Drawing.Size(207, 20);
            this.chkPrintReceipt.TabIndex = 0;
            this.chkPrintReceipt.Text = "Print Receipt After Transaction";
            this.chkPrintReceipt.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox3.Controls.Add(this.enOrderMod);
            this.groupBox3.Controls.Add(this.enableQuanMod);
            this.groupBox3.Controls.Add(this.enablePriceMod);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(23, 344);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 150);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Inventory Control";
            // 
            // enOrderMod
            // 
            this.enOrderMod.AutoSize = true;
            this.enOrderMod.Location = new System.Drawing.Point(20, 95);
            this.enOrderMod.Name = "enOrderMod";
            this.enOrderMod.Size = new System.Drawing.Size(173, 20);
            this.enOrderMod.TabIndex = 2;
            this.enOrderMod.Text = "Enable Ordering Module";
            this.enOrderMod.UseVisualStyleBackColor = true;
            // 
            // enableQuanMod
            // 
            this.enableQuanMod.AutoSize = true;
            this.enableQuanMod.Location = new System.Drawing.Point(20, 69);
            this.enableQuanMod.Name = "enableQuanMod";
            this.enableQuanMod.Size = new System.Drawing.Size(196, 20);
            this.enableQuanMod.TabIndex = 1;
            this.enableQuanMod.Text = "Enable Modifying of Quantity";
            this.enableQuanMod.UseVisualStyleBackColor = true;
            // 
            // enablePriceMod
            // 
            this.enablePriceMod.AutoSize = true;
            this.enablePriceMod.Location = new System.Drawing.Point(20, 37);
            this.enablePriceMod.Name = "enablePriceMod";
            this.enablePriceMod.Size = new System.Drawing.Size(179, 20);
            this.enablePriceMod.TabIndex = 0;
            this.enablePriceMod.Text = "Enable Modifying of Price";
            this.enablePriceMod.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox4.Controls.Add(this.enablePrintSum);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(278, 281);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(225, 61);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Summary Control";
            // 
            // enablePrintSum
            // 
            this.enablePrintSum.AutoSize = true;
            this.enablePrintSum.Location = new System.Drawing.Point(22, 29);
            this.enablePrintSum.Name = "enablePrintSum";
            this.enablePrintSum.Size = new System.Drawing.Size(191, 20);
            this.enablePrintSum.TabIndex = 2;
            this.enablePrintSum.Text = "Enable Printing of Summary";
            this.enablePrintSum.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tomato;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(513, 439);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 55);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox5.Controls.Add(this.supplierAllowMulti);
            this.groupBox5.Controls.Add(this.defaultSupplierNo);
            this.groupBox5.Controls.Add(this.defaultSupplierAdd);
            this.groupBox5.Controls.Add(this.defaultSupplier);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(278, 349);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(225, 145);
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
            this.supplierAllowMulti.Size = new System.Drawing.Size(161, 20);
            this.supplierAllowMulti.TabIndex = 3;
            this.supplierAllowMulti.Text = "Allow Multiple Supplier";
            this.supplierAllowMulti.UseVisualStyleBackColor = true;
            // 
            // defaultSupplierNo
            // 
            this.defaultSupplierNo.Cue = "Default Supplier No.";
            this.defaultSupplierNo.Location = new System.Drawing.Point(6, 80);
            this.defaultSupplierNo.Name = "defaultSupplierNo";
            this.defaultSupplierNo.Size = new System.Drawing.Size(205, 22);
            this.defaultSupplierNo.TabIndex = 2;
            // 
            // defaultSupplierAdd
            // 
            this.defaultSupplierAdd.Cue = "Default Supplier Address";
            this.defaultSupplierAdd.Location = new System.Drawing.Point(6, 54);
            this.defaultSupplierAdd.Name = "defaultSupplierAdd";
            this.defaultSupplierAdd.Size = new System.Drawing.Size(205, 22);
            this.defaultSupplierAdd.TabIndex = 1;
            // 
            // defaultSupplier
            // 
            this.defaultSupplier.Cue = "Default Supplier";
            this.defaultSupplier.Location = new System.Drawing.Point(6, 28);
            this.defaultSupplier.Name = "defaultSupplier";
            this.defaultSupplier.Size = new System.Drawing.Size(205, 22);
            this.defaultSupplier.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox6.Controls.Add(this.txtStoreContact);
            this.groupBox6.Controls.Add(this.txtStoreAdd);
            this.groupBox6.Controls.Add(this.txtStoreName);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(513, 182);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(217, 98);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Store Information";
            // 
            // txtStoreContact
            // 
            this.txtStoreContact.Cue = "Store Contact No.";
            this.txtStoreContact.Location = new System.Drawing.Point(6, 71);
            this.txtStoreContact.Name = "txtStoreContact";
            this.txtStoreContact.Size = new System.Drawing.Size(205, 22);
            this.txtStoreContact.TabIndex = 3;
            // 
            // txtStoreAdd
            // 
            this.txtStoreAdd.Cue = "Store Address";
            this.txtStoreAdd.Location = new System.Drawing.Point(6, 45);
            this.txtStoreAdd.Name = "txtStoreAdd";
            this.txtStoreAdd.Size = new System.Drawing.Size(205, 22);
            this.txtStoreAdd.TabIndex = 2;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Cue = "Store Name";
            this.txtStoreName.Location = new System.Drawing.Point(6, 19);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(205, 22);
            this.txtStoreName.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.txtMySqlPassword);
            this.groupBox7.Controls.Add(this.txtMySqlUser);
            this.groupBox7.Controls.Add(this.txtmySqlServer);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(513, 29);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(217, 147);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Mysql Connection";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Tomato;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(7, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(204, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "Test Sql Connection";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtMySqlPassword
            // 
            this.txtMySqlPassword.Cue = "MySql Password";
            this.txtMySqlPassword.Location = new System.Drawing.Point(6, 78);
            this.txtMySqlPassword.Name = "txtMySqlPassword";
            this.txtMySqlPassword.PasswordChar = '*';
            this.txtMySqlPassword.Size = new System.Drawing.Size(205, 22);
            this.txtMySqlPassword.TabIndex = 2;
            // 
            // txtMySqlUser
            // 
            this.txtMySqlUser.Cue = "MySql User";
            this.txtMySqlUser.Location = new System.Drawing.Point(6, 52);
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.Size = new System.Drawing.Size(205, 22);
            this.txtMySqlUser.TabIndex = 1;
            // 
            // txtmySqlServer
            // 
            this.txtmySqlServer.Cue = "MySql Server";
            this.txtmySqlServer.Location = new System.Drawing.Point(6, 26);
            this.txtmySqlServer.Name = "txtmySqlServer";
            this.txtmySqlServer.Size = new System.Drawing.Size(205, 22);
            this.txtmySqlServer.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox8.Controls.Add(this.button4);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(513, 286);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(217, 88);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "System Control (Warning)";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Tomato;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(12, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(199, 55);
            this.button4.TabIndex = 8;
            this.button4.Text = "Restore Defaults";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(742, 506);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.adminGroupBox);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            this.groupBox8.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox enOrderMod;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button button4;
    }
}