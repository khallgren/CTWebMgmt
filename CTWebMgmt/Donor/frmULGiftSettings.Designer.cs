namespace CTWebMgmt.Donor
{
    partial class frmULGiftSettings
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnULGiftSettings = new System.Windows.Forms.Button();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(307, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnULGiftSettings
            // 
            this.btnULGiftSettings.Location = new System.Drawing.Point(182, 12);
            this.btnULGiftSettings.Name = "btnULGiftSettings";
            this.btnULGiftSettings.Size = new System.Drawing.Size(119, 23);
            this.btnULGiftSettings.TabIndex = 1;
            this.btnULGiftSettings.Text = "Upload Gift Settings";
            this.btnULGiftSettings.UseVisualStyleBackColor = true;
            this.btnULGiftSettings.Click += new System.EventHandler(this.btnULGiftSettings_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 41);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(370, 290);
            this.lstStatus.TabIndex = 2;
            // 
            // frmULGiftSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 339);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.btnULGiftSettings);
            this.Controls.Add(this.btnClose);
            this.Name = "frmULGiftSettings";
            this.Text = "Upload Gift Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnULGiftSettings;
        private System.Windows.Forms.ListBox lstStatus;
    }
}