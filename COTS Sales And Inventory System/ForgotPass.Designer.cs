namespace COTS_Sales_And_Inventory_System
{
    partial class ForgotPass
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
            this.button1 = new System.Windows.Forms.Button();
            this.cueTextBox1 = new CueTextBox();
            this.cueTextBox2 = new CueTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(281, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "Email Username and Password";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cueTextBox1
            // 
            this.cueTextBox1.Cue = "Secret Question";
            this.cueTextBox1.Location = new System.Drawing.Point(12, 12);
            this.cueTextBox1.Name = "cueTextBox1";
            this.cueTextBox1.ReadOnly = true;
            this.cueTextBox1.Size = new System.Drawing.Size(281, 20);
            this.cueTextBox1.TabIndex = 1;
            // 
            // cueTextBox2
            // 
            this.cueTextBox2.Cue = "Secret Answer";
            this.cueTextBox2.Location = new System.Drawing.Point(12, 38);
            this.cueTextBox2.Name = "cueTextBox2";
            this.cueTextBox2.Size = new System.Drawing.Size(281, 20);
            this.cueTextBox2.TabIndex = 2;
            this.cueTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cueTextBox2_KeyDown);
            // 
            // ForgotPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 95);
            this.Controls.Add(this.cueTextBox2);
            this.Controls.Add(this.cueTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "ForgotPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot Password";
            this.Load += new System.EventHandler(this.ForgotPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private CueTextBox cueTextBox1;
        private CueTextBox cueTextBox2;

    }
}