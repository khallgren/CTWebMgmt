namespace CTWebMgmt.ContactInfo
{
    partial class frmRecordInformation
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
            this.txtLName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMI = new System.Windows.Forms.TextBox();
            this.panHead = new System.Windows.Forms.Panel();
            this.btnAdvFind = new System.Windows.Forms.Button();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.txtFindByID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRecordID = new System.Windows.Forms.TextBox();
            this.tabRecordInfo = new System.Windows.Forms.TabControl();
            this.pagContactInfo = new System.Windows.Forms.TabPage();
            this.chkUseMORAddress = new System.Windows.Forms.CheckBox();
            this.txtInformalGiftSal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFormalGiftSal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtInformalParentSal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtParentSalutation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pagRegistrations = new System.Windows.Forms.TabPage();
            this.grdRegistrations = new System.Windows.Forms.DataGridView();
            this.colRegistrationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBlockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colEmailConf = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPrintConf = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pagTransactions = new System.Windows.Forms.TabPage();
            this.grdTransactions = new System.Windows.Forms.DataGridView();
            this.colTransactionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCredit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransBlockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtFindByName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panHead.SuspendLayout();
            this.tabRecordInfo.SuspendLayout();
            this.pagContactInfo.SuspendLayout();
            this.pagRegistrations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegistrations)).BeginInit();
            this.pagTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(155, 100);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(100, 20);
            this.txtLName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Last Name:";
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(155, 123);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(100, 20);
            this.txtFName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "First Name, MI:";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(155, 146);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(100, 20);
            this.txtCompanyName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Company Name:";
            // 
            // txtMI
            // 
            this.txtMI.Location = new System.Drawing.Point(261, 123);
            this.txtMI.Name = "txtMI";
            this.txtMI.Size = new System.Drawing.Size(29, 20);
            this.txtMI.TabIndex = 17;
            // 
            // panHead
            // 
            this.panHead.BackColor = System.Drawing.Color.Transparent;
            this.panHead.Controls.Add(this.txtFindByName);
            this.panHead.Controls.Add(this.label12);
            this.panHead.Controls.Add(this.btnAdvFind);
            this.panHead.Controls.Add(this.btnSaveAndClose);
            this.panHead.Controls.Add(this.txtFindByID);
            this.panHead.Controls.Add(this.label1);
            this.panHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHead.Location = new System.Drawing.Point(0, 0);
            this.panHead.Name = "panHead";
            this.panHead.Size = new System.Drawing.Size(906, 63);
            this.panHead.TabIndex = 25;
            // 
            // btnAdvFind
            // 
            this.btnAdvFind.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdvFind.Location = new System.Drawing.Point(12, 12);
            this.btnAdvFind.Name = "btnAdvFind";
            this.btnAdvFind.Size = new System.Drawing.Size(48, 23);
            this.btnAdvFind.TabIndex = 6;
            this.btnAdvFind.Text = "&Find";
            this.btnAdvFind.UseVisualStyleBackColor = true;
            this.btnAdvFind.Click += new System.EventHandler(this.btnAdvFind_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAndClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveAndClose.Location = new System.Drawing.Point(805, 10);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(89, 26);
            this.btnSaveAndClose.TabIndex = 5;
            this.btnSaveAndClose.Text = "Save && &Close";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // txtFindByID
            // 
            this.txtFindByID.Location = new System.Drawing.Point(168, 14);
            this.txtFindByID.Name = "txtFindByID";
            this.txtFindByID.Size = new System.Drawing.Size(100, 20);
            this.txtFindByID.TabIndex = 4;
            this.txtFindByID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindByID_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Find by Record ID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Record ID:";
            // 
            // txtRecordID
            // 
            this.txtRecordID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRecordID.Enabled = false;
            this.txtRecordID.Location = new System.Drawing.Point(155, 74);
            this.txtRecordID.Name = "txtRecordID";
            this.txtRecordID.Size = new System.Drawing.Size(100, 13);
            this.txtRecordID.TabIndex = 27;
            // 
            // tabRecordInfo
            // 
            this.tabRecordInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabRecordInfo.Controls.Add(this.pagContactInfo);
            this.tabRecordInfo.Controls.Add(this.pagRegistrations);
            this.tabRecordInfo.Controls.Add(this.pagTransactions);
            this.tabRecordInfo.Location = new System.Drawing.Point(0, 172);
            this.tabRecordInfo.Name = "tabRecordInfo";
            this.tabRecordInfo.SelectedIndex = 0;
            this.tabRecordInfo.Size = new System.Drawing.Size(906, 361);
            this.tabRecordInfo.TabIndex = 28;
            // 
            // pagContactInfo
            // 
            this.pagContactInfo.Controls.Add(this.chkUseMORAddress);
            this.pagContactInfo.Controls.Add(this.txtInformalGiftSal);
            this.pagContactInfo.Controls.Add(this.label10);
            this.pagContactInfo.Controls.Add(this.txtFormalGiftSal);
            this.pagContactInfo.Controls.Add(this.label9);
            this.pagContactInfo.Controls.Add(this.cboState);
            this.pagContactInfo.Controls.Add(this.txtZip);
            this.pagContactInfo.Controls.Add(this.txtInformalParentSal);
            this.pagContactInfo.Controls.Add(this.label8);
            this.pagContactInfo.Controls.Add(this.txtParentSalutation);
            this.pagContactInfo.Controls.Add(this.label7);
            this.pagContactInfo.Controls.Add(this.txtCity);
            this.pagContactInfo.Controls.Add(this.label6);
            this.pagContactInfo.Controls.Add(this.txtAddress);
            this.pagContactInfo.Controls.Add(this.label5);
            this.pagContactInfo.Location = new System.Drawing.Point(4, 22);
            this.pagContactInfo.Name = "pagContactInfo";
            this.pagContactInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pagContactInfo.Size = new System.Drawing.Size(898, 335);
            this.pagContactInfo.TabIndex = 0;
            this.pagContactInfo.Text = "Contact Information";
            this.pagContactInfo.UseVisualStyleBackColor = true;
            // 
            // chkUseMORAddress
            // 
            this.chkUseMORAddress.AutoSize = true;
            this.chkUseMORAddress.Location = new System.Drawing.Point(294, 8);
            this.chkUseMORAddress.Name = "chkUseMORAddress";
            this.chkUseMORAddress.Size = new System.Drawing.Size(122, 17);
            this.chkUseMORAddress.TabIndex = 39;
            this.chkUseMORAddress.Text = "Using MOR Address";
            this.chkUseMORAddress.UseVisualStyleBackColor = true;
            // 
            // txtInformalGiftSal
            // 
            this.txtInformalGiftSal.Location = new System.Drawing.Point(151, 121);
            this.txtInformalGiftSal.Name = "txtInformalGiftSal";
            this.txtInformalGiftSal.Size = new System.Drawing.Size(159, 20);
            this.txtInformalGiftSal.TabIndex = 38;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Informal/Gift Salutation:";
            // 
            // txtFormalGiftSal
            // 
            this.txtFormalGiftSal.Location = new System.Drawing.Point(151, 98);
            this.txtFormalGiftSal.Name = "txtFormalGiftSal";
            this.txtFormalGiftSal.Size = new System.Drawing.Size(159, 20);
            this.txtFormalGiftSal.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Formal/Gift Salutation:";
            // 
            // cboState
            // 
            this.cboState.FormattingEnabled = true;
            this.cboState.Location = new System.Drawing.Point(257, 29);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(53, 21);
            this.cboState.TabIndex = 34;
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(316, 29);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(100, 20);
            this.txtZip.TabIndex = 33;
            // 
            // txtInformalParentSal
            // 
            this.txtInformalParentSal.Location = new System.Drawing.Point(151, 75);
            this.txtInformalParentSal.Name = "txtInformalParentSal";
            this.txtInformalParentSal.Size = new System.Drawing.Size(159, 20);
            this.txtInformalParentSal.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Informal Parent/Salutation:";
            // 
            // txtParentSalutation
            // 
            this.txtParentSalutation.Location = new System.Drawing.Point(151, 52);
            this.txtParentSalutation.Name = "txtParentSalutation";
            this.txtParentSalutation.Size = new System.Drawing.Size(159, 20);
            this.txtParentSalutation.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Parent/Salutation:";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(151, 29);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(100, 20);
            this.txtCity.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "City, State, Zip:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(151, 6);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(137, 20);
            this.txtAddress.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Address:";
            // 
            // pagRegistrations
            // 
            this.pagRegistrations.Controls.Add(this.grdRegistrations);
            this.pagRegistrations.Location = new System.Drawing.Point(4, 22);
            this.pagRegistrations.Name = "pagRegistrations";
            this.pagRegistrations.Padding = new System.Windows.Forms.Padding(3);
            this.pagRegistrations.Size = new System.Drawing.Size(898, 335);
            this.pagRegistrations.TabIndex = 1;
            this.pagRegistrations.Text = "Registrations";
            this.pagRegistrations.UseVisualStyleBackColor = true;
            // 
            // grdRegistrations
            // 
            this.grdRegistrations.AllowUserToAddRows = false;
            this.grdRegistrations.AllowUserToDeleteRows = false;
            this.grdRegistrations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRegistrations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRegistrationID,
            this.colBlockCode,
            this.colRegDate,
            this.colStartEnd,
            this.colDetails,
            this.colEmailConf,
            this.colPrintConf});
            this.grdRegistrations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRegistrations.Location = new System.Drawing.Point(3, 3);
            this.grdRegistrations.Name = "grdRegistrations";
            this.grdRegistrations.Size = new System.Drawing.Size(892, 329);
            this.grdRegistrations.TabIndex = 0;
            // 
            // colRegistrationID
            // 
            this.colRegistrationID.DataPropertyName = "lngRegistrationID";
            this.colRegistrationID.HeaderText = "Reg ID";
            this.colRegistrationID.Name = "colRegistrationID";
            this.colRegistrationID.ReadOnly = true;
            // 
            // colBlockCode
            // 
            this.colBlockCode.DataPropertyName = "strBlockCode";
            this.colBlockCode.HeaderText = "Block Code";
            this.colBlockCode.Name = "colBlockCode";
            this.colBlockCode.ReadOnly = true;
            // 
            // colRegDate
            // 
            this.colRegDate.DataPropertyName = "dteRegistrationDate";
            this.colRegDate.HeaderText = "Registration Date";
            this.colRegDate.Name = "colRegDate";
            this.colRegDate.ReadOnly = true;
            // 
            // colStartEnd
            // 
            this.colStartEnd.DataPropertyName = "strStartEnd";
            this.colStartEnd.HeaderText = "Start/End Dates";
            this.colStartEnd.Name = "colStartEnd";
            this.colStartEnd.ReadOnly = true;
            // 
            // colDetails
            // 
            this.colDetails.DataPropertyName = "lngRegistrationID";
            this.colDetails.HeaderText = "";
            this.colDetails.Name = "colDetails";
            this.colDetails.Text = "Details";
            this.colDetails.UseColumnTextForButtonValue = true;
            // 
            // colEmailConf
            // 
            this.colEmailConf.DataPropertyName = "lngRegistrationID";
            this.colEmailConf.HeaderText = "";
            this.colEmailConf.Name = "colEmailConf";
            this.colEmailConf.Text = "Email Conf";
            this.colEmailConf.UseColumnTextForButtonValue = true;
            // 
            // colPrintConf
            // 
            this.colPrintConf.DataPropertyName = "lngRegistrationID";
            this.colPrintConf.HeaderText = "";
            this.colPrintConf.Name = "colPrintConf";
            this.colPrintConf.Text = "Print Conf";
            this.colPrintConf.UseColumnTextForButtonValue = true;
            // 
            // pagTransactions
            // 
            this.pagTransactions.Controls.Add(this.grdTransactions);
            this.pagTransactions.Location = new System.Drawing.Point(4, 22);
            this.pagTransactions.Name = "pagTransactions";
            this.pagTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.pagTransactions.Size = new System.Drawing.Size(898, 335);
            this.pagTransactions.TabIndex = 2;
            this.pagTransactions.Text = "Transactions";
            this.pagTransactions.UseVisualStyleBackColor = true;
            // 
            // grdTransactions
            // 
            this.grdTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTransactionID,
            this.colCredit,
            this.colDebit,
            this.colTransType,
            this.colTransBlockCode,
            this.colDesc,
            this.colTransDetails});
            this.grdTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTransactions.Location = new System.Drawing.Point(3, 3);
            this.grdTransactions.Name = "grdTransactions";
            this.grdTransactions.Size = new System.Drawing.Size(892, 329);
            this.grdTransactions.TabIndex = 0;
            // 
            // colTransactionID
            // 
            this.colTransactionID.HeaderText = "Trans ID";
            this.colTransactionID.Name = "colTransactionID";
            this.colTransactionID.ReadOnly = true;
            // 
            // colCredit
            // 
            this.colCredit.HeaderText = "Credit";
            this.colCredit.Name = "colCredit";
            this.colCredit.ReadOnly = true;
            // 
            // colDebit
            // 
            this.colDebit.HeaderText = "Debit";
            this.colDebit.Name = "colDebit";
            this.colDebit.ReadOnly = true;
            // 
            // colTransType
            // 
            this.colTransType.HeaderText = "Trans Type/Payment Type";
            this.colTransType.Name = "colTransType";
            this.colTransType.ReadOnly = true;
            // 
            // colTransBlockCode
            // 
            this.colTransBlockCode.HeaderText = "Block Code";
            this.colTransBlockCode.Name = "colTransBlockCode";
            this.colTransBlockCode.ReadOnly = true;
            // 
            // colDesc
            // 
            this.colDesc.HeaderText = "Description";
            this.colDesc.Name = "colDesc";
            this.colDesc.ReadOnly = true;
            // 
            // colTransDetails
            // 
            this.colTransDetails.HeaderText = "Details";
            this.colTransDetails.Name = "colTransDetails";
            // 
            // txtFindByName
            // 
            this.txtFindByName.Location = new System.Drawing.Point(168, 40);
            this.txtFindByName.Name = "txtFindByName";
            this.txtFindByName.Size = new System.Drawing.Size(100, 20);
            this.txtFindByName.TabIndex = 8;
            this.txtFindByName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindByName_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(87, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Find by Name:";
            // 
            // frmRecordInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.ClientSize = new System.Drawing.Size(906, 532);
            this.Controls.Add(this.tabRecordInfo);
            this.Controls.Add(this.txtRecordID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panHead);
            this.Controls.Add(this.txtMI);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.label2);
            this.Name = "frmRecordInformation";
            this.Text = "Record Information";
            this.Load += new System.EventHandler(this.frmRecordInformation_Load);
            this.panHead.ResumeLayout(false);
            this.panHead.PerformLayout();
            this.tabRecordInfo.ResumeLayout(false);
            this.pagContactInfo.ResumeLayout(false);
            this.pagContactInfo.PerformLayout();
            this.pagRegistrations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRegistrations)).EndInit();
            this.pagTransactions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMI;
        private System.Windows.Forms.Panel panHead;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.TextBox txtFindByID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdvFind;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRecordID;
        private System.Windows.Forms.TabControl tabRecordInfo;
        private System.Windows.Forms.TabPage pagContactInfo;
        private System.Windows.Forms.CheckBox chkUseMORAddress;
        private System.Windows.Forms.TextBox txtInformalGiftSal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFormalGiftSal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtInformalParentSal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtParentSalutation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage pagRegistrations;
        private System.Windows.Forms.TabPage pagTransactions;
        private System.Windows.Forms.DataGridView grdRegistrations;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegistrationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBlockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartEnd;
        private System.Windows.Forms.DataGridViewButtonColumn colDetails;
        private System.Windows.Forms.DataGridViewButtonColumn colEmailConf;
        private System.Windows.Forms.DataGridViewButtonColumn colPrintConf;
        private System.Windows.Forms.DataGridView grdTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCredit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransBlockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewButtonColumn colTransDetails;
        private System.Windows.Forms.TextBox txtFindByName;
        private System.Windows.Forms.Label label12;
    }
}