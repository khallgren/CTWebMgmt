namespace CTWebMgmt.Ind
{
    partial class frmSyncTrans
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
            this.lnkCTSync = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Synchronization functionality has been moved to the new CTSync utility.";
            // 
            // lnkCTSync
            // 
            this.lnkCTSync.AutoSize = true;
            this.lnkCTSync.Location = new System.Drawing.Point(166, 103);
            this.lnkCTSync.Name = "lnkCTSync";
            this.lnkCTSync.Size = new System.Drawing.Size(219, 13);
            this.lnkCTSync.TabIndex = 1;
            this.lnkCTSync.TabStop = true;
            this.lnkCTSync.Text = "Click here to install and run the CTSync utility";
            this.lnkCTSync.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCTSync_LinkClicked);
            // 
            // frmSyncTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 213);
            this.Controls.Add(this.lnkCTSync);
            this.Controls.Add(this.label1);
            this.Name = "frmSyncTrans";
            this.Text = "Synchronize Transactions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkCTSync;

    }
}