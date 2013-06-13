namespace CTWebMgmt.GGCC
{
    partial class frmProcessGGCCReg
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
            this.grdGGCCReg = new System.Windows.Forms.DataGridView();
            this.lngGGCCRegistrationWebID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strLastCoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strGGCCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dteDateRegistered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dteLastModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdGGCCReg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(627, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grdGGCCReg
            // 
            this.grdGGCCReg.AllowUserToAddRows = false;
            this.grdGGCCReg.AllowUserToDeleteRows = false;
            this.grdGGCCReg.AllowUserToOrderColumns = true;
            this.grdGGCCReg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdGGCCReg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdGGCCReg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lngGGCCRegistrationWebID,
            this.strFirstName,
            this.strLastCoName,
            this.strCompanyName,
            this.strGGCCName,
            this.dteDateRegistered,
            this.dteLastModified,
            this.colDetails});
            this.grdGGCCReg.Location = new System.Drawing.Point(12, 41);
            this.grdGGCCReg.MultiSelect = false;
            this.grdGGCCReg.Name = "grdGGCCReg";
            this.grdGGCCReg.ReadOnly = true;
            this.grdGGCCReg.Size = new System.Drawing.Size(690, 324);
            this.grdGGCCReg.TabIndex = 1;
            this.grdGGCCReg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.btnDetails_Click);
            // 
            // lngGGCCRegistrationWebID
            // 
            this.lngGGCCRegistrationWebID.DataPropertyName = "lngGGCCRegistrationWebID";
            this.lngGGCCRegistrationWebID.HeaderText = "lngGGCCRegistrationWebID";
            this.lngGGCCRegistrationWebID.Name = "lngGGCCRegistrationWebID";
            this.lngGGCCRegistrationWebID.ReadOnly = true;
            this.lngGGCCRegistrationWebID.Visible = false;
            // 
            // strFirstName
            // 
            this.strFirstName.DataPropertyName = "strFirstName";
            this.strFirstName.HeaderText = "First Name";
            this.strFirstName.Name = "strFirstName";
            this.strFirstName.ReadOnly = true;
            // 
            // strLastCoName
            // 
            this.strLastCoName.DataPropertyName = "strLastCoName";
            this.strLastCoName.HeaderText = "Last Name";
            this.strLastCoName.Name = "strLastCoName";
            this.strLastCoName.ReadOnly = true;
            // 
            // strCompanyName
            // 
            this.strCompanyName.DataPropertyName = "strCompanyName";
            this.strCompanyName.HeaderText = "Company";
            this.strCompanyName.Name = "strCompanyName";
            this.strCompanyName.ReadOnly = true;
            // 
            // strGGCCName
            // 
            this.strGGCCName.DataPropertyName = "strGGCCName";
            this.strGGCCName.HeaderText = "Event Name";
            this.strGGCCName.Name = "strGGCCName";
            this.strGGCCName.ReadOnly = true;
            // 
            // dteDateRegistered
            // 
            this.dteDateRegistered.DataPropertyName = "dteDateRegistered";
            this.dteDateRegistered.HeaderText = "Date Registered";
            this.dteDateRegistered.Name = "dteDateRegistered";
            this.dteDateRegistered.ReadOnly = true;
            // 
            // dteLastModified
            // 
            this.dteLastModified.DataPropertyName = "dteLastModified";
            this.dteLastModified.HeaderText = "Last Modified";
            this.dteLastModified.Name = "dteLastModified";
            this.dteLastModified.ReadOnly = true;
            // 
            // colDetails
            // 
            this.colDetails.HeaderText = "Details";
            this.colDetails.Name = "colDetails";
            this.colDetails.ReadOnly = true;
            this.colDetails.Text = "Process Reg";
            this.colDetails.UseColumnTextForButtonValue = true;
            // 
            // frmProcessGGCCReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 377);
            this.Controls.Add(this.grdGGCCReg);
            this.Controls.Add(this.btnClose);
            this.Name = "frmProcessGGCCReg";
            this.Text = "Process Event Registrations";
            this.Load += new System.EventHandler(this.frmProcessGGCCReg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdGGCCReg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView grdGGCCReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn lngGGCCRegistrationWebID;
        private System.Windows.Forms.DataGridViewTextBoxColumn strFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn strLastCoName;
        private System.Windows.Forms.DataGridViewTextBoxColumn strCompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn strGGCCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dteDateRegistered;
        private System.Windows.Forms.DataGridViewTextBoxColumn dteLastModified;
        private System.Windows.Forms.DataGridViewButtonColumn colDetails;
    }
}