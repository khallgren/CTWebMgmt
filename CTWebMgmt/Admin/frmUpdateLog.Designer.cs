namespace CTWebMgmt.Admin
{
    partial class frmUpdateLog
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
            this.txtUpdateLog = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUpdateLog
            // 
            this.txtUpdateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpdateLog.Location = new System.Drawing.Point(12, 41);
            this.txtUpdateLog.Multiline = true;
            this.txtUpdateLog.Name = "txtUpdateLog";
            this.txtUpdateLog.ReadOnly = true;
            this.txtUpdateLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUpdateLog.Size = new System.Drawing.Size(560, 569);
            this.txtUpdateLog.TabIndex = 0;
            this.txtUpdateLog.WordWrap = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(497, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUpdateLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 622);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtUpdateLog);
            this.Name = "frmUpdateLog";
            this.Text = "Update Log";
            this.Load += new System.EventHandler(this.frmUpdateLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUpdateLog;
        private System.Windows.Forms.Button btnClose;
    }
}