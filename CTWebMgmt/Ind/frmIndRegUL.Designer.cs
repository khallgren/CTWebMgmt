namespace CTWebMgmt.Ind
{
    partial class frmIndRegUL
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
            this.btnUL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(327, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUL
            // 
            this.btnUL.Location = new System.Drawing.Point(246, 12);
            this.btnUL.Name = "btnUL";
            this.btnUL.Size = new System.Drawing.Size(75, 23);
            this.btnUL.TabIndex = 1;
            this.btnUL.Text = "&Upload";
            this.btnUL.UseVisualStyleBackColor = true;
            this.btnUL.Click += new System.EventHandler(this.btnUL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Click \'Upload\' to send block definitions to the web server.";
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(15, 54);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(386, 316);
            this.lstStatus.TabIndex = 3;
            // 
            // frmIndRegUL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 390);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUL);
            this.Controls.Add(this.btnClose);
            this.Name = "frmIndRegUL";
            this.Text = "Upload Block Definitions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstStatus;
    }
}