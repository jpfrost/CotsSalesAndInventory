namespace COTS_Sales_And_Inventory_System
{
    partial class Items
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cueTextBox4 = new CueTextBox();
            this.cueTextBox3 = new CueTextBox();
            this.cueTextBox2 = new CueTextBox();
            this.cueTextBox1 = new CueTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Tomato;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Location = new System.Drawing.Point(-2, -12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Management";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 170);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(149, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardValidInputs);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(12, 197);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(149, 21);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardValidInputs);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(12, 274);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(149, 21);
            this.comboBox3.TabIndex = 6;
            this.comboBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardValidInputs);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(171, 222);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(134, 23);
            this.button5.TabIndex = 10;
            this.button5.Text = "Delete Product Size";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(171, 195);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(134, 23);
            this.button6.TabIndex = 9;
            this.button6.Text = "Add Product Size";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 330);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Delete Item";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 301);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Add/Edit Item";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cueTextBox4
            // 
            this.cueTextBox4.Cue = "Price";
            this.cueTextBox4.Location = new System.Drawing.Point(12, 248);
            this.cueTextBox4.Name = "cueTextBox4";
            this.cueTextBox4.Size = new System.Drawing.Size(149, 20);
            this.cueTextBox4.TabIndex = 5;
            this.cueTextBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardOnlyDecimals);
            // 
            // cueTextBox3
            // 
            this.cueTextBox3.Cue = "Quantity";
            this.cueTextBox3.Location = new System.Drawing.Point(12, 224);
            this.cueTextBox3.Name = "cueTextBox3";
            this.cueTextBox3.Size = new System.Drawing.Size(149, 20);
            this.cueTextBox3.TabIndex = 4;
            this.cueTextBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardOnlyDigits);
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = "Product Name";
            this.cueTextBox2.Location = new System.Drawing.Point(12, 144);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.Size = new System.Drawing.Size(149, 20);
            this.cueTextBox2.TabIndex = 1;
            this.cueTextBox2.TextChanged += new System.EventHandler(this.cueTextBox2_Leave);
            this.cueTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductNameKeyDownEnter);
            this.cueTextBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardValidInputs);
            this.cueTextBox2.Leave += new System.EventHandler(this.cueTextBox2_Leave);
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.Cue = "Product Code";
            this.cueTextBox1.Location = new System.Drawing.Point(12, 118);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.Size = new System.Drawing.Size(149, 20);
            this.cueTextBox1.TabIndex = 0;
            this.cueTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductIdKeyDownEnter);
            this.cueTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyboardValidInputs);
            // 
            // Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cueTextBox4);
            this.Controls.Add(this.cueTextBox3);
            this.Controls.Add(this.cueTextBox2);
            this.Controls.Add(this.cueTextBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Items";
            this.Size = new System.Drawing.Size(317, 443);
            this.Load += new System.EventHandler(this.Items_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private CueTextBox cueTextBox1;
        private CueTextBox cueTextBox3;
        private CueTextBox cueTextBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private CueTextBox cueTextBox2;
    }
}