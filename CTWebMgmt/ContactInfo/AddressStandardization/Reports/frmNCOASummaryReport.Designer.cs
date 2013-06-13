namespace CTWebMgmt.ContactInfo.AddressStandardization.Reports
{
    partial class frmNCOASummaryReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNCOASummaryReport));
            this.btnGenerateRpt = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCert = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGenerateRpt
            // 
            this.btnGenerateRpt.Location = new System.Drawing.Point(228, 102);
            this.btnGenerateRpt.Name = "btnGenerateRpt";
            this.btnGenerateRpt.Size = new System.Drawing.Size(105, 32);
            this.btnGenerateRpt.TabIndex = 1;
            this.btnGenerateRpt.Text = "Generate Report";
            this.btnGenerateRpt.UseVisualStyleBackColor = true;
            this.btnGenerateRpt.Click += new System.EventHandler(this.btnGenerateRpt_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(460, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(106, 166);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(384, 20);
            this.txtURL.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Report Link:";
            // 
            // cboCert
            // 
            this.cboCert.FormattingEnabled = true;
            this.cboCert.Location = new System.Drawing.Point(38, 109);
            this.cboCert.Name = "cboCert";
            this.cboCert.Size = new System.Drawing.Size(173, 21);
            this.cboCert.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(475, 39);
            this.label2.TabIndex = 6;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // frmNCOASummaryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 236);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboCert);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGenerateRpt);
            this.Name = "frmNCOASummaryReport";
            this.Text = "NCOA Summary Report";
            this.Load += new System.EventHandler(this.frmNCOASummaryReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateRpt;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCert;
        private System.Windows.Forms.Label label2;
    }
}