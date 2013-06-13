namespace CTWebMgmt.ContactInfo.AddStdV2
{
    partial class frmAddStdV2
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
            this.colCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnProcess = new System.Windows.Forms.Button();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.colZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastSent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkTickedOnly = new System.Windows.Forms.CheckBox();
            this.colLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdRecords = new System.Windows.Forms.DataGridView();
            this.colRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkOnlyNonCertified = new System.Windows.Forms.CheckBox();
            this.lblTickedRecords = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // colCity
            // 
            this.colCity.DataPropertyName = "strCity";
            this.colCity.HeaderText = "City";
            this.colCity.Name = "colCity";
            this.colCity.ReadOnly = true;
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(600, 566);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 20;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // colState
            // 
            this.colState.DataPropertyName = "strState";
            this.colState.HeaderText = "State";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // colAddress
            // 
            this.colAddress.DataPropertyName = "strAddress";
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Location = new System.Drawing.Point(671, 270);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(85, 13);
            this.lblRecordCount.TabIndex = 25;
            this.lblRecordCount.Text = "Record Count: 0";
            // 
            // colZip
            // 
            this.colZip.DataPropertyName = "strZip";
            this.colZip.HeaderText = "Zip";
            this.colZip.Name = "colZip";
            this.colZip.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "strName";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colLastSent
            // 
            this.colLastSent.DataPropertyName = "dteLastCertified";
            this.colLastSent.HeaderText = "Date Last Certified";
            this.colLastSent.Name = "colLastSent";
            this.colLastSent.ReadOnly = true;
            // 
            // colCompanyName
            // 
            this.colCompanyName.DataPropertyName = "strCompanyName";
            this.colCompanyName.HeaderText = "CompanyName";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.ReadOnly = true;
            this.colCompanyName.Visible = false;
            // 
            // chkTickedOnly
            // 
            this.chkTickedOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkTickedOnly.AutoSize = true;
            this.chkTickedOnly.Location = new System.Drawing.Point(15, 296);
            this.chkTickedOnly.Name = "chkTickedOnly";
            this.chkTickedOnly.Size = new System.Drawing.Size(212, 17);
            this.chkTickedOnly.TabIndex = 26;
            this.chkTickedOnly.Text = "Exclude other members of primary MOR";
            this.chkTickedOnly.UseVisualStyleBackColor = true;
            this.chkTickedOnly.CheckedChanged += new System.EventHandler(this.chkTickedOnly_CheckedChanged);
            // 
            // colLName
            // 
            this.colLName.DataPropertyName = "strLName";
            this.colLName.HeaderText = "LName";
            this.colLName.Name = "colLName";
            this.colLName.ReadOnly = true;
            this.colLName.Visible = false;
            // 
            // grdRecords
            // 
            this.grdRecords.AllowUserToAddRows = false;
            this.grdRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordID,
            this.colFName,
            this.colLName,
            this.colCompanyName,
            this.colName,
            this.colAddress,
            this.colCity,
            this.colState,
            this.colZip,
            this.colLastSent});
            this.grdRecords.Location = new System.Drawing.Point(15, 26);
            this.grdRecords.Name = "grdRecords";
            this.grdRecords.Size = new System.Drawing.Size(788, 241);
            this.grdRecords.TabIndex = 24;
            // 
            // colRecordID
            // 
            this.colRecordID.DataPropertyName = "lngRecordID";
            this.colRecordID.HeaderText = "Record ID";
            this.colRecordID.Name = "colRecordID";
            this.colRecordID.ReadOnly = true;
            // 
            // colFName
            // 
            this.colFName.DataPropertyName = "strFName";
            this.colFName.HeaderText = "FName";
            this.colFName.Name = "colFName";
            this.colFName.ReadOnly = true;
            this.colFName.Visible = false;
            // 
            // chkOnlyNonCertified
            // 
            this.chkOnlyNonCertified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkOnlyNonCertified.AutoSize = true;
            this.chkOnlyNonCertified.Location = new System.Drawing.Point(15, 273);
            this.chkOnlyNonCertified.Name = "chkOnlyNonCertified";
            this.chkOnlyNonCertified.Size = new System.Drawing.Size(186, 17);
            this.chkOnlyNonCertified.TabIndex = 23;
            this.chkOnlyNonCertified.Text = "Only process non-certified records";
            this.chkOnlyNonCertified.UseVisualStyleBackColor = true;
            this.chkOnlyNonCertified.CheckedChanged += new System.EventHandler(this.chkOnlyNonCertified_CheckedChanged);
            // 
            // lblTickedRecords
            // 
            this.lblTickedRecords.AutoSize = true;
            this.lblTickedRecords.Location = new System.Drawing.Point(12, 10);
            this.lblTickedRecords.Name = "lblTickedRecords";
            this.lblTickedRecords.Size = new System.Drawing.Size(270, 13);
            this.lblTickedRecords.TabIndex = 22;
            this.lblTickedRecords.Text = "The following ticked records will be sent for certification:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(681, 566);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Click the \'Process\' button to send addresses to Melissa Data.";
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(9, 376);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(747, 173);
            this.lstStatus.TabIndex = 27;
            // 
            // frmAddStdV2_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 601);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.chkTickedOnly);
            this.Controls.Add(this.grdRecords);
            this.Controls.Add(this.chkOnlyNonCertified);
            this.Controls.Add(this.lblTickedRecords);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmAddStdV2_1";
            this.Text = "Address Standardization - Select Records";
            this.Load += new System.EventHandler(this.frmAddStd_1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn colCity;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        public System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastSent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompanyName;
        private System.Windows.Forms.CheckBox chkTickedOnly;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLName;
        public System.Windows.Forms.DataGridView grdRecords;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFName;
        private System.Windows.Forms.CheckBox chkOnlyNonCertified;
        private System.Windows.Forms.Label lblTickedRecords;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstStatus;
    }
}