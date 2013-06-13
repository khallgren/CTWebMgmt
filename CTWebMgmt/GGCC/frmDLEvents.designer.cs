namespace CTWebMgmt.GGCC
{
    partial class frmDLEvents
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
            this.btnDL = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnDL
            // 
            this.btnDL.Location = new System.Drawing.Point(57, 12);
            this.btnDL.Name = "btnDL";
            this.btnDL.Size = new System.Drawing.Size(141, 23);
            this.btnDL.TabIndex = 0;
            this.btnDL.Text = "Download Registrations";
            this.btnDL.UseVisualStyleBackColor = true;
            this.btnDL.Click += new System.EventHandler(this.btnDL_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(204, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 41);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(267, 199);
            this.lstStatus.TabIndex = 2;
            // 
            // frmDLEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 259);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDL);
            this.Name = "frmDLEvents";
            this.Text = "Download Event Registrations";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDL;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstStatus;
    }
}