namespace COTS_Sales_And_Inventory_System
{
    partial class AddItemForSale
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
            this.cueTextBox1 = new CueTextBox();
            this.cueTextBox2 = new CueTextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cueTextBox3 = new CueTextBox();
            this.cueTextBox4 = new CueTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.Cue = "Product Name";
            this.cueTextBox1.Location = new System.Drawing.Point(12, 45);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.Size = new System.Drawing.Size(225, 20);
            this.cueTextBox1.TabIndex = 0;
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = "Price";
            this.cueTextBox2.Location = new System.Drawing.Point(13, 97);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.Size = new System.Drawing.Size(224, 20);
            this.cueTextBox2.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 123);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(225, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // cueTextBox3
            // 
            this.cueTextBox3.Cue = "Discount";
            this.cueTextBox3.Location = new System.Drawing.Point(13, 149);
            this.cueTextBox3.Name = "cueTextBox3";
            this.cueTextBox3.Size = new System.Drawing.Size(224, 20);
            this.cueTextBox3.TabIndex = 3;
            // 
            // cueTextBox4
            // 
            this.cueTextBox4.Cue = "Total";
            this.cueTextBox4.Location = new System.Drawing.Point(13, 176);
            this.cueTextBox4.Name = "cueTextBox4";
            this.cueTextBox4.Size = new System.Drawing.Size(224, 20);
            this.cueTextBox4.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Add Item";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 21);
            this.comboBox1.TabIndex = 7;
            // 
            // AddItemForSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 249);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cueTextBox4);
            this.Controls.Add(this.cueTextBox3);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.cueTextBox2);
            this.Controls.Add(this.cueTextBox1);
            this.Name = "AddItemForSale";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item For Sale";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddItemForSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CueTextBox cueTextBox1;
        private CueTextBox cueTextBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private CueTextBox cueTextBox3;
        private CueTextBox cueTextBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}