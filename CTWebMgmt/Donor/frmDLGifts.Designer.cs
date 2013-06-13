namespace CTWebMgmt.Donor
{
    partial class frmDLGifts
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
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDLGifts = new System.Windows.Forms.Button();
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 40);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(469, 316);
            this.lstStatus.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(405, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDLGifts
            // 
            this.btnDLGifts.Location = new System.Drawing.Point(324, 13);
            this.btnDLGifts.Name = "btnDLGifts";
            this.btnDLGifts.Size = new System.Drawing.Size(75, 23);
            this.btnDLGifts.TabIndex = 2;
            this.btnDLGifts.Text = "&Download";
            this.btnDLGifts.UseVisualStyleBackColor = true;
            this.btnDLGifts.Click += new System.EventHandler(this.btnDLGifts_Click);
            // 
            // txtDebug
            // 
            this.txtDebug.Location = new System.Drawing.Point(12, 362);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(468, 11);
            this.txtDebug.TabIndex = 3;
            this.txtDebug.Visible = false;
            // 
            // frmDLGifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 379);
            this.Controls.Add(this.txtDebug);
            this.Controls.Add(this.btnDLGifts);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstStatus);
            this.Name = "frmDLGifts";
            this.Text = "Download Gifts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDLGifts;
        private System.Windows.Forms.TextBox txtDebug;
    }
}