namespace CTWebMgmt.Ind
{
    partial class frmUseRegHold
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboRegHoldID = new System.Windows.Forms.ComboBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 80);
            this.label1.TabIndex = 0;
            this.label1.Text = "There are some spots being held for this Block.\r\n\r\nTo use one of these spots, sel" +
                "ect the holder from the list below.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Registration Holder:";
            // 
            // cboRegHoldID
            // 
            this.cboRegHoldID.FormattingEnabled = true;
            this.cboRegHoldID.Location = new System.Drawing.Point(118, 103);
            this.cboRegHoldID.Name = "cboRegHoldID";
            this.cboRegHoldID.Size = new System.Drawing.Size(183, 21);
            this.cboRegHoldID.TabIndex = 2;
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(296, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 3;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // frmUseRegHold
            // 
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 186);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.cboRegHoldID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmUseRegHold";
            this.Text = "Use Registration Hold";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboRegHoldID;
        private System.Windows.Forms.Button btnContinue;
    }
}