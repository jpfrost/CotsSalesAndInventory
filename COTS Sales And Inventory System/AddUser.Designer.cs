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
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtRepass = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoStock = new System.Windows.Forms.RadioButton();
            this.rdoCashier = new System.Windows.Forms.RadioButton();
            this.rdoOwner = new System.Windows.Forms.RadioButton();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(62, 119);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(274, 47);
            this.txtUserName.TabIndex = 8;
            // 
            // txtRepass
            // 
            this.txtRepass.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepass.Location = new System.Drawing.Point(62, 225);
            this.txtRepass.Name = "txtRepass";
            this.txtRepass.Size = new System.Drawing.Size(274, 47);
            this.txtRepass.TabIndex = 10;
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(62, 172);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(274, 47);
            this.txtPass.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoStock);
            this.groupBox1.Controls.Add(this.rdoCashier);
            this.groupBox1.Controls.Add(this.rdoOwner);
            this.groupBox1.Location = new System.Drawing.Point(62, 287);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 53);
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
            this.btnAddUser.BackColor = System.Drawing.Color.Tomato;
            this.btnAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnAddUser.Location = new System.Drawing.Point(62, 356);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(274, 48);
            this.btnAddUser.TabIndex = 17;
            this.btnAddUser.Text = "Add me";
            this.btnAddUser.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(-3, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 86);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 35.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(93, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 54);
            this.label5.TabIndex = 0;
            this.label5.Text = "Add User";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAddUser
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(393, 443);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtRepass);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales And Inventory System";
            this.Load += new System.EventHandler(this.frmAddUser_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtRepass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoStock;
        private System.Windows.Forms.RadioButton rdoCashier;
        private System.Windows.Forms.RadioButton rdoOwner;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
    }
}

