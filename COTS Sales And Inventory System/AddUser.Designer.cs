namespace COTS_Sales_And_Inventory_System
{
    partial class frmAddUser
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtFname = new System.Windows.Forms.TextBox();
            this.txtRepass = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSecAns = new System.Windows.Forms.TextBox();
            this.cmbSecQues = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoStock = new System.Windows.Forms.RadioButton();
            this.rdoCashier = new System.Windows.Forms.RadioButton();
            this.rdoOwner = new System.Windows.Forms.RadioButton();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "User Name: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Full Name: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Password: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Re-Type Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "E-mail Address: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 233);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Security Question:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Security Answer:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(84, 32);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(169, 20);
            this.txtUserName.TabIndex = 8;
            // 
            // txtFname
            // 
            this.txtFname.Location = new System.Drawing.Point(84, 73);
            this.txtFname.Name = "txtFname";
            this.txtFname.Size = new System.Drawing.Size(169, 20);
            this.txtFname.TabIndex = 9;
            // 
            // txtRepass
            // 
            this.txtRepass.Location = new System.Drawing.Point(118, 151);
            this.txtRepass.Name = "txtRepass";
            this.txtRepass.Size = new System.Drawing.Size(135, 20);
            this.txtRepass.TabIndex = 10;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(84, 113);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(169, 20);
            this.txtPass.TabIndex = 11;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(118, 193);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(135, 20);
            this.txtEmail.TabIndex = 12;
            // 
            // txtSecAns
            // 
            this.txtSecAns.Location = new System.Drawing.Point(104, 269);
            this.txtSecAns.Name = "txtSecAns";
            this.txtSecAns.Size = new System.Drawing.Size(149, 20);
            this.txtSecAns.TabIndex = 14;
            // 
            // cmbSecQues
            // 
            this.cmbSecQues.FormattingEnabled = true;
            this.cmbSecQues.Location = new System.Drawing.Point(118, 230);
            this.cmbSecQues.Name = "cmbSecQues";
            this.cmbSecQues.Size = new System.Drawing.Size(135, 21);
            this.cmbSecQues.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoStock);
            this.groupBox1.Controls.Add(this.rdoCashier);
            this.groupBox1.Controls.Add(this.rdoOwner);
            this.groupBox1.Location = new System.Drawing.Point(15, 304);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 54);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Admin Rights";
            // 
            // rdoStock
            // 
            this.rdoStock.AutoSize = true;
            this.rdoStock.Location = new System.Drawing.Point(140, 19);
            this.rdoStock.Name = "rdoStock";
            this.rdoStock.Size = new System.Drawing.Size(98, 17);
            this.rdoStock.TabIndex = 2;
            this.rdoStock.TabStop = true;
            this.rdoStock.Text = "Stock Manager";
            this.rdoStock.UseVisualStyleBackColor = true;
            // 
            // rdoCashier
            // 
            this.rdoCashier.AutoSize = true;
            this.rdoCashier.Location = new System.Drawing.Point(74, 19);
            this.rdoCashier.Name = "rdoCashier";
            this.rdoCashier.Size = new System.Drawing.Size(60, 17);
            this.rdoCashier.TabIndex = 1;
            this.rdoCashier.TabStop = true;
            this.rdoCashier.Text = "Cashier";
            this.rdoCashier.UseVisualStyleBackColor = true;
            // 
            // rdoOwner
            // 
            this.rdoOwner.AutoSize = true;
            this.rdoOwner.Location = new System.Drawing.Point(12, 19);
            this.rdoOwner.Name = "rdoOwner";
            this.rdoOwner.Size = new System.Drawing.Size(56, 17);
            this.rdoOwner.TabIndex = 0;
            this.rdoOwner.TabStop = true;
            this.rdoOwner.Text = "Owner";
            this.rdoOwner.UseVisualStyleBackColor = true;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(197, 375);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 17;
            this.btnAddUser.Text = "Add";
            this.btnAddUser.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(15, 375);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear Fields";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // frmAddUser
            // 
            this.ClientSize = new System.Drawing.Size(284, 410);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbSecQues);
            this.Controls.Add(this.txtSecAns);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtRepass);
            this.Controls.Add(this.txtFname);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add User";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtFname;
        private System.Windows.Forms.TextBox txtRepass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSecAns;
        private System.Windows.Forms.ComboBox cmbSecQues;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoStock;
        private System.Windows.Forms.RadioButton rdoCashier;
        private System.Windows.Forms.RadioButton rdoOwner;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnClear;
    }
}

