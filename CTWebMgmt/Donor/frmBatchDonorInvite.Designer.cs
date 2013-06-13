namespace CTWebMgmt.Donor
{
    partial class frmBatchDonorInvite
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
            this.chkTicked = new System.Windows.Forms.CheckBox();
            this.fraDateAdded = new System.Windows.Forms.GroupBox();
            this.radDateRange = new System.Windows.Forms.RadioButton();
            this.radOneDate = new System.Windows.Forms.RadioButton();
            this.radAllDates = new System.Windows.Forms.RadioButton();
            this.dpkDate1 = new System.Windows.Forms.DateTimePicker();
            this.dpkDate2 = new System.Windows.Forms.DateTimePicker();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtMsgBody = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtFromEMail = new System.Windows.Forms.TextBox();
            this.txtFromAlias = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.btnUpdateDef = new System.Windows.Forms.Button();
            this.chkHTML = new System.Windows.Forms.CheckBox();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDefaultCategory = new System.Windows.Forms.ComboBox();
            this.cboDefaultCampaign = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkUseSSL = new System.Windows.Forms.CheckBox();
            this.fraDateAdded.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkTicked
            // 
            this.chkTicked.AutoSize = true;
            this.chkTicked.Location = new System.Drawing.Point(18, 110);
            this.chkTicked.Name = "chkTicked";
            this.chkTicked.Size = new System.Drawing.Size(120, 17);
            this.chkTicked.TabIndex = 0;
            this.chkTicked.Text = "Only Ticked Donors";
            this.chkTicked.UseVisualStyleBackColor = true;
            // 
            // fraDateAdded
            // 
            this.fraDateAdded.Controls.Add(this.radDateRange);
            this.fraDateAdded.Controls.Add(this.radOneDate);
            this.fraDateAdded.Controls.Add(this.radAllDates);
            this.fraDateAdded.Location = new System.Drawing.Point(12, 12);
            this.fraDateAdded.Name = "fraDateAdded";
            this.fraDateAdded.Size = new System.Drawing.Size(148, 92);
            this.fraDateAdded.TabIndex = 1;
            this.fraDateAdded.TabStop = false;
            this.fraDateAdded.Text = "Filter by Date Added";
            // 
            // radDateRange
            // 
            this.radDateRange.AutoSize = true;
            this.radDateRange.Location = new System.Drawing.Point(6, 65);
            this.radDateRange.Name = "radDateRange";
            this.radDateRange.Size = new System.Drawing.Size(83, 17);
            this.radDateRange.TabIndex = 2;
            this.radDateRange.Text = "Date Range";
            this.radDateRange.UseVisualStyleBackColor = true;
            this.radDateRange.CheckedChanged += new System.EventHandler(this.radDateRange_CheckedChanged);
            // 
            // radOneDate
            // 
            this.radOneDate.AutoSize = true;
            this.radOneDate.Location = new System.Drawing.Point(6, 42);
            this.radOneDate.Name = "radOneDate";
            this.radOneDate.Size = new System.Drawing.Size(89, 17);
            this.radOneDate.TabIndex = 1;
            this.radOneDate.Text = "Specific Date";
            this.radOneDate.UseVisualStyleBackColor = true;
            this.radOneDate.CheckedChanged += new System.EventHandler(this.radOneDate_CheckedChanged);
            // 
            // radAllDates
            // 
            this.radAllDates.AutoSize = true;
            this.radAllDates.Checked = true;
            this.radAllDates.Location = new System.Drawing.Point(6, 19);
            this.radAllDates.Name = "radAllDates";
            this.radAllDates.Size = new System.Drawing.Size(67, 17);
            this.radAllDates.TabIndex = 0;
            this.radAllDates.TabStop = true;
            this.radAllDates.Text = "All Dates";
            this.radAllDates.UseVisualStyleBackColor = true;
            this.radAllDates.CheckedChanged += new System.EventHandler(this.radAllDates_CheckedChanged);
            // 
            // dpkDate1
            // 
            this.dpkDate1.Location = new System.Drawing.Point(230, 52);
            this.dpkDate1.Name = "dpkDate1";
            this.dpkDate1.Size = new System.Drawing.Size(200, 20);
            this.dpkDate1.TabIndex = 2;
            // 
            // dpkDate2
            // 
            this.dpkDate2.Location = new System.Drawing.Point(230, 75);
            this.dpkDate2.Name = "dpkDate2";
            this.dpkDate2.Size = new System.Drawing.Size(200, 20);
            this.dpkDate2.TabIndex = 3;
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(166, 56);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(58, 13);
            this.lblDate1.TabIndex = 4;
            this.lblDate1.Text = "Start Date:";
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(166, 79);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(55, 13);
            this.lblDate2.TabIndex = 5;
            this.lblDate2.Text = "End Date:";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(527, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(608, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtMsgBody
            // 
            this.txtMsgBody.Location = new System.Drawing.Point(12, 238);
            this.txtMsgBody.Multiline = true;
            this.txtMsgBody.Name = "txtMsgBody";
            this.txtMsgBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsgBody.Size = new System.Drawing.Size(540, 148);
            this.txtMsgBody.TabIndex = 8;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(82, 193);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(470, 20);
            this.txtSubject.TabIndex = 9;
            // 
            // txtFromEMail
            // 
            this.txtFromEMail.Location = new System.Drawing.Point(82, 141);
            this.txtFromEMail.Name = "txtFromEMail";
            this.txtFromEMail.Size = new System.Drawing.Size(180, 20);
            this.txtFromEMail.TabIndex = 10;
            // 
            // txtFromAlias
            // 
            this.txtFromAlias.Location = new System.Drawing.Point(82, 167);
            this.txtFromAlias.Name = "txtFromAlias";
            this.txtFromAlias.Size = new System.Drawing.Size(180, 20);
            this.txtFromAlias.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "From Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "From Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Subject:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Message Body:";
            // 
            // lstStatus
            // 
            this.lstStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 392);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(540, 69);
            this.lstStatus.TabIndex = 16;
            // 
            // btnUpdateDef
            // 
            this.btnUpdateDef.Location = new System.Drawing.Point(427, 139);
            this.btnUpdateDef.Name = "btnUpdateDef";
            this.btnUpdateDef.Size = new System.Drawing.Size(125, 23);
            this.btnUpdateDef.TabIndex = 17;
            this.btnUpdateDef.Text = "&Update Defaults";
            this.btnUpdateDef.UseVisualStyleBackColor = true;
            this.btnUpdateDef.Click += new System.EventHandler(this.btnUpdateDef_Click);
            // 
            // chkHTML
            // 
            this.chkHTML.AutoSize = true;
            this.chkHTML.Checked = true;
            this.chkHTML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHTML.Location = new System.Drawing.Point(427, 166);
            this.chkHTML.Name = "chkHTML";
            this.chkHTML.Size = new System.Drawing.Size(97, 17);
            this.chkHTML.TabIndex = 18;
            this.chkHTML.Text = "HTML Format?";
            this.chkHTML.UseVisualStyleBackColor = true;
            // 
            // lstFields
            // 
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(558, 257);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(120, 199);
            this.lstFields.TabIndex = 20;
            this.lstFields.DoubleClick += new System.EventHandler(this.lstFields_DoubleClick);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(558, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 32);
            this.label5.TabIndex = 21;
            this.label5.Text = "Double-click list to insert special field:";
            // 
            // cboDefaultCategory
            // 
            this.cboDefaultCategory.FormattingEnabled = true;
            this.cboDefaultCategory.Location = new System.Drawing.Point(563, 53);
            this.cboDefaultCategory.Name = "cboDefaultCategory";
            this.cboDefaultCategory.Size = new System.Drawing.Size(121, 21);
            this.cboDefaultCategory.TabIndex = 22;
            // 
            // cboDefaultCampaign
            // 
            this.cboDefaultCampaign.FormattingEnabled = true;
            this.cboDefaultCampaign.Location = new System.Drawing.Point(563, 80);
            this.cboDefaultCampaign.Name = "cboDefaultCampaign";
            this.cboDefaultCampaign.Size = new System.Drawing.Size(121, 21);
            this.cboDefaultCampaign.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Default Category:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(463, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Default Campaign:";
            // 
            // chkUseSSL
            // 
            this.chkUseSSL.AutoSize = true;
            this.chkUseSSL.Location = new System.Drawing.Point(347, 166);
            this.chkUseSSL.Name = "chkUseSSL";
            this.chkUseSSL.Size = new System.Drawing.Size(74, 17);
            this.chkUseSSL.TabIndex = 26;
            this.chkUseSSL.Text = "Use SSL?";
            this.chkUseSSL.UseVisualStyleBackColor = true;
            // 
            // frmBatchDonorInvite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 466);
            this.Controls.Add(this.chkUseSSL);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboDefaultCampaign);
            this.Controls.Add(this.cboDefaultCategory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.chkHTML);
            this.Controls.Add(this.btnUpdateDef);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFromAlias);
            this.Controls.Add(this.txtFromEMail);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtMsgBody);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblDate2);
            this.Controls.Add(this.lblDate1);
            this.Controls.Add(this.dpkDate2);
            this.Controls.Add(this.dpkDate1);
            this.Controls.Add(this.fraDateAdded);
            this.Controls.Add(this.chkTicked);
            this.Name = "frmBatchDonorInvite";
            this.Text = "Batch Donor Invitation";
            this.Load += new System.EventHandler(this.frmBatchDonorInvite_Load);
            this.fraDateAdded.ResumeLayout(false);
            this.fraDateAdded.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTicked;
        private System.Windows.Forms.GroupBox fraDateAdded;
        private System.Windows.Forms.RadioButton radDateRange;
        private System.Windows.Forms.RadioButton radOneDate;
        private System.Windows.Forms.RadioButton radAllDates;
        private System.Windows.Forms.DateTimePicker dpkDate1;
        private System.Windows.Forms.DateTimePicker dpkDate2;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtMsgBody;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtFromEMail;
        private System.Windows.Forms.TextBox txtFromAlias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Button btnUpdateDef;
        private System.Windows.Forms.CheckBox chkHTML;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboDefaultCategory;
        private System.Windows.Forms.ComboBox cboDefaultCampaign;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkUseSSL;
    }
}