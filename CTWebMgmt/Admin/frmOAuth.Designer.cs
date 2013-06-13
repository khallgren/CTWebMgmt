namespace CTWebMgmt.Admin
{
    partial class frmOAuth
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
            this.brsOAuth = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // brsOAuth
            // 
            this.brsOAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brsOAuth.Location = new System.Drawing.Point(0, 0);
            this.brsOAuth.MinimumSize = new System.Drawing.Size(20, 20);
            this.brsOAuth.Name = "brsOAuth";
            this.brsOAuth.Size = new System.Drawing.Size(684, 439);
            this.brsOAuth.TabIndex = 2;
            this.brsOAuth.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.brsOAuth_DocumentCompleted);
            // 
            // frmOAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 439);
            this.Controls.Add(this.brsOAuth);
            this.Name = "frmOAuth";
            this.Text = "Google Authorization";
            this.Load += new System.EventHandler(this.frmOAuth_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser brsOAuth;
    }
}