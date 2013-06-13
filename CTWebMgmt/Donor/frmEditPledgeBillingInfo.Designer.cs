namespace CTWebMgmt.Donor
{
    partial class frmEditPledgeBillingInfo
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
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBillName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAcctNumber = new System.Windows.Forms.Label();
            this.lblCVV2 = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblBankName = new System.Windows.Forms.Label();
            this.lblRoutingNumber = new System.Windows.Forms.Label();
            this.lblExpDate = new System.Windows.Forms.Label();
            this.txtBillPhone = new System.Windows.Forms.TextBox();
            this.txtRoutingNumber = new System.Windows.Forms.TextBox();
            this.txtAcctNumber = new System.Windows.Forms.TextBox();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.txtExpDate = new System.Windows.Forms.TextBox();
            this.txtCVV2 = new System.Windows.Forms.TextBox();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.txtBillZip = new System.Windows.Forms.TextBox();
            this.txtBillCity = new System.Windows.Forms.TextBox();
            this.txtBillAddress = new System.Windows.Forms.TextBox();
            this.cboPaymentType = new System.Windows.Forms.ComboBox();
            this.cboBillState = new System.Windows.Forms.ComboBox();
            this.txtXCAlias = new System.Windows.Forms.TextBox();
            this.lblXCAlias = new System.Windows.Forms.Label();
            this.txtPNRef = new System.Windows.Forms.TextBox();
            this.lblPNRef = new System.Windows.Forms.Label();
            this.txtXCEFTAuthCode = new System.Windows.Forms.TextBox();
            this.txtXCEFTRefID = new System.Windows.Forms.TextBox();
            this.lblXCEFTAuthCode = new System.Windows.Forms.Label();
            this.lblXCEFTRefID = new System.Windows.Forms.Label();
            this.btnCopyFromBillingInfo = new System.Windows.Forms.Button();
            this.lblEPSPmtAcctID = new System.Windows.Forms.Label();
            this.txtEPSPmtAcctID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.Location = new System.Drawing.Point(421, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 13;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBillName
            // 
            this.txtBillName.Location = new System.Drawing.Point(125, 57);
            this.txtBillName.Name = "txtBillName";
            this.txtBillName.Size = new System.Drawing.Size(178, 20);
            this.txtBillName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Payment Type:";
            // 
            // lblAcctNumber
            // 
            this.lblAcctNumber.AutoSize = true;
            this.lblAcctNumber.Location = new System.Drawing.Point(24, 217);
            this.lblAcctNumber.Name = "lblAcctNumber";
            this.lblAcctNumber.Size = new System.Drawing.Size(90, 13);
            this.lblAcctNumber.TabIndex = 6;
            this.lblAcctNumber.Text = "Account Number:";
            // 
            // lblCVV2
            // 
            this.lblCVV2.AutoSize = true;
            this.lblCVV2.Location = new System.Drawing.Point(24, 217);
            this.lblCVV2.Name = "lblCVV2";
            this.lblCVV2.Size = new System.Drawing.Size(37, 13);
            this.lblCVV2.TabIndex = 7;
            this.lblCVV2.Text = "CVV2:";
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Location = new System.Drawing.Point(24, 191);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(72, 13);
            this.lblCardNumber.TabIndex = 8;
            this.lblCardNumber.Text = "Card Number:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Bill Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Bill Address:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Bill City, State, Zip:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Bill Phone:";
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(24, 191);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(66, 13);
            this.lblBankName.TabIndex = 13;
            this.lblBankName.Text = "Bank Name:";
            // 
            // lblRoutingNumber
            // 
            this.lblRoutingNumber.AutoSize = true;
            this.lblRoutingNumber.Location = new System.Drawing.Point(24, 243);
            this.lblRoutingNumber.Name = "lblRoutingNumber";
            this.lblRoutingNumber.Size = new System.Drawing.Size(87, 13);
            this.lblRoutingNumber.TabIndex = 14;
            this.lblRoutingNumber.Text = "Routing Number:";
            // 
            // lblExpDate
            // 
            this.lblExpDate.AutoSize = true;
            this.lblExpDate.Location = new System.Drawing.Point(24, 243);
            this.lblExpDate.Name = "lblExpDate";
            this.lblExpDate.Size = new System.Drawing.Size(54, 13);
            this.lblExpDate.TabIndex = 15;
            this.lblExpDate.Text = "Exp Date:";
            // 
            // txtBillPhone
            // 
            this.txtBillPhone.Location = new System.Drawing.Point(125, 135);
            this.txtBillPhone.Name = "txtBillPhone";
            this.txtBillPhone.Size = new System.Drawing.Size(100, 20);
            this.txtBillPhone.TabIndex = 5;
            // 
            // txtRoutingNumber
            // 
            this.txtRoutingNumber.Location = new System.Drawing.Point(125, 240);
            this.txtRoutingNumber.Name = "txtRoutingNumber";
            this.txtRoutingNumber.Size = new System.Drawing.Size(178, 20);
            this.txtRoutingNumber.TabIndex = 12;
            // 
            // txtAcctNumber
            // 
            this.txtAcctNumber.Location = new System.Drawing.Point(125, 214);
            this.txtAcctNumber.Name = "txtAcctNumber";
            this.txtAcctNumber.Size = new System.Drawing.Size(178, 20);
            this.txtAcctNumber.TabIndex = 11;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(125, 188);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(178, 20);
            this.txtBankName.TabIndex = 10;
            // 
            // txtExpDate
            // 
            this.txtExpDate.Location = new System.Drawing.Point(125, 240);
            this.txtExpDate.Name = "txtExpDate";
            this.txtExpDate.Size = new System.Drawing.Size(100, 20);
            this.txtExpDate.TabIndex = 9;
            // 
            // txtCVV2
            // 
            this.txtCVV2.Location = new System.Drawing.Point(125, 214);
            this.txtCVV2.Name = "txtCVV2";
            this.txtCVV2.Size = new System.Drawing.Size(100, 20);
            this.txtCVV2.TabIndex = 8;
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(125, 188);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(121, 20);
            this.txtCardNumber.TabIndex = 7;
            // 
            // txtBillZip
            // 
            this.txtBillZip.Location = new System.Drawing.Point(387, 109);
            this.txtBillZip.Name = "txtBillZip";
            this.txtBillZip.Size = new System.Drawing.Size(100, 20);
            this.txtBillZip.TabIndex = 4;
            // 
            // txtBillCity
            // 
            this.txtBillCity.Location = new System.Drawing.Point(125, 109);
            this.txtBillCity.Name = "txtBillCity";
            this.txtBillCity.Size = new System.Drawing.Size(178, 20);
            this.txtBillCity.TabIndex = 2;
            // 
            // txtBillAddress
            // 
            this.txtBillAddress.Location = new System.Drawing.Point(125, 83);
            this.txtBillAddress.Name = "txtBillAddress";
            this.txtBillAddress.Size = new System.Drawing.Size(178, 20);
            this.txtBillAddress.TabIndex = 1;
            // 
            // cboPaymentType
            // 
            this.cboPaymentType.FormattingEnabled = true;
            this.cboPaymentType.Location = new System.Drawing.Point(125, 161);
            this.cboPaymentType.Name = "cboPaymentType";
            this.cboPaymentType.Size = new System.Drawing.Size(121, 21);
            this.cboPaymentType.TabIndex = 6;
            this.cboPaymentType.SelectedIndexChanged += new System.EventHandler(this.cboPaymentType_SelectedIndexChanged);
            // 
            // cboBillState
            // 
            this.cboBillState.FormattingEnabled = true;
            this.cboBillState.Location = new System.Drawing.Point(309, 109);
            this.cboBillState.Name = "cboBillState";
            this.cboBillState.Size = new System.Drawing.Size(72, 21);
            this.cboBillState.TabIndex = 3;
            // 
            // txtXCAlias
            // 
            this.txtXCAlias.Location = new System.Drawing.Point(125, 318);
            this.txtXCAlias.Name = "txtXCAlias";
            this.txtXCAlias.Size = new System.Drawing.Size(178, 20);
            this.txtXCAlias.TabIndex = 16;
            // 
            // lblXCAlias
            // 
            this.lblXCAlias.AutoSize = true;
            this.lblXCAlias.Location = new System.Drawing.Point(24, 321);
            this.lblXCAlias.Name = "lblXCAlias";
            this.lblXCAlias.Size = new System.Drawing.Size(76, 13);
            this.lblXCAlias.TabIndex = 17;
            this.lblXCAlias.Text = "XCharge Alias:";
            // 
            // txtPNRef
            // 
            this.txtPNRef.Location = new System.Drawing.Point(125, 318);
            this.txtPNRef.Name = "txtPNRef";
            this.txtPNRef.Size = new System.Drawing.Size(178, 20);
            this.txtPNRef.TabIndex = 18;
            // 
            // lblPNRef
            // 
            this.lblPNRef.AutoSize = true;
            this.lblPNRef.Location = new System.Drawing.Point(24, 321);
            this.lblPNRef.Name = "lblPNRef";
            this.lblPNRef.Size = new System.Drawing.Size(45, 13);
            this.lblPNRef.TabIndex = 19;
            this.lblPNRef.Text = "PN Ref:";
            // 
            // txtXCEFTAuthCode
            // 
            this.txtXCEFTAuthCode.Location = new System.Drawing.Point(125, 266);
            this.txtXCEFTAuthCode.Name = "txtXCEFTAuthCode";
            this.txtXCEFTAuthCode.Size = new System.Drawing.Size(178, 20);
            this.txtXCEFTAuthCode.TabIndex = 21;
            // 
            // txtXCEFTRefID
            // 
            this.txtXCEFTRefID.Location = new System.Drawing.Point(125, 292);
            this.txtXCEFTRefID.Name = "txtXCEFTRefID";
            this.txtXCEFTRefID.Size = new System.Drawing.Size(178, 20);
            this.txtXCEFTRefID.TabIndex = 22;
            // 
            // lblXCEFTAuthCode
            // 
            this.lblXCEFTAuthCode.AutoSize = true;
            this.lblXCEFTAuthCode.Location = new System.Drawing.Point(24, 269);
            this.lblXCEFTAuthCode.Name = "lblXCEFTAuthCode";
            this.lblXCEFTAuthCode.Size = new System.Drawing.Size(83, 13);
            this.lblXCEFTAuthCode.TabIndex = 23;
            this.lblXCEFTAuthCode.Text = "EFT Auth Code:";
            // 
            // lblXCEFTRefID
            // 
            this.lblXCEFTRefID.AutoSize = true;
            this.lblXCEFTRefID.Location = new System.Drawing.Point(24, 295);
            this.lblXCEFTRefID.Name = "lblXCEFTRefID";
            this.lblXCEFTRefID.Size = new System.Drawing.Size(64, 13);
            this.lblXCEFTRefID.TabIndex = 20;
            this.lblXCEFTRefID.Text = "EFT Ref ID:";
            // 
            // btnCopyFromBillingInfo
            // 
            this.btnCopyFromBillingInfo.Location = new System.Drawing.Point(27, 28);
            this.btnCopyFromBillingInfo.Name = "btnCopyFromBillingInfo";
            this.btnCopyFromBillingInfo.Size = new System.Drawing.Size(127, 23);
            this.btnCopyFromBillingInfo.TabIndex = 24;
            this.btnCopyFromBillingInfo.Text = "Copy From Billing Info";
            this.btnCopyFromBillingInfo.UseVisualStyleBackColor = true;
            this.btnCopyFromBillingInfo.Click += new System.EventHandler(this.btnCopyFromBillingInfo_Click);
            // 
            // lblEPSPmtAcctID
            // 
            this.lblEPSPmtAcctID.AutoSize = true;
            this.lblEPSPmtAcctID.Location = new System.Drawing.Point(24, 321);
            this.lblEPSPmtAcctID.Name = "lblEPSPmtAcctID";
            this.lblEPSPmtAcctID.Size = new System.Drawing.Size(91, 13);
            this.lblEPSPmtAcctID.TabIndex = 25;
            this.lblEPSPmtAcctID.Text = "EPS Pmt Acct ID:";
            // 
            // txtEPSPmtAcctID
            // 
            this.txtEPSPmtAcctID.Location = new System.Drawing.Point(125, 318);
            this.txtEPSPmtAcctID.Name = "txtEPSPmtAcctID";
            this.txtEPSPmtAcctID.Size = new System.Drawing.Size(178, 20);
            this.txtEPSPmtAcctID.TabIndex = 26;
            // 
            // frmEditPledgeBillingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(508, 357);
            this.Controls.Add(this.txtEPSPmtAcctID);
            this.Controls.Add(this.lblEPSPmtAcctID);
            this.Controls.Add(this.btnCopyFromBillingInfo);
            this.Controls.Add(this.txtXCEFTAuthCode);
            this.Controls.Add(this.txtXCEFTRefID);
            this.Controls.Add(this.lblXCEFTAuthCode);
            this.Controls.Add(this.lblXCEFTRefID);
            this.Controls.Add(this.txtPNRef);
            this.Controls.Add(this.lblPNRef);
            this.Controls.Add(this.txtXCAlias);
            this.Controls.Add(this.lblXCAlias);
            this.Controls.Add(this.cboBillState);
            this.Controls.Add(this.cboPaymentType);
            this.Controls.Add(this.txtBillAddress);
            this.Controls.Add(this.txtBillCity);
            this.Controls.Add(this.txtBillZip);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.txtCVV2);
            this.Controls.Add(this.txtExpDate);
            this.Controls.Add(this.txtBankName);
            this.Controls.Add(this.txtAcctNumber);
            this.Controls.Add(this.txtRoutingNumber);
            this.Controls.Add(this.txtBillPhone);
            this.Controls.Add(this.lblExpDate);
            this.Controls.Add(this.lblRoutingNumber);
            this.Controls.Add(this.lblBankName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblCardNumber);
            this.Controls.Add(this.lblCVV2);
            this.Controls.Add(this.lblAcctNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBillName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Name = "frmEditPledgeBillingInfo";
            this.Text = "Edit Pledge Billing Info";
            this.Load += new System.EventHandler(this.frmEditPledgeBillingInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBillName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAcctNumber;
        private System.Windows.Forms.Label lblCVV2;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label lblRoutingNumber;
        private System.Windows.Forms.Label lblExpDate;
        private System.Windows.Forms.TextBox txtBillPhone;
        private System.Windows.Forms.TextBox txtRoutingNumber;
        private System.Windows.Forms.TextBox txtAcctNumber;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TextBox txtExpDate;
        private System.Windows.Forms.TextBox txtCVV2;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.TextBox txtBillZip;
        private System.Windows.Forms.TextBox txtBillCity;
        private System.Windows.Forms.TextBox txtBillAddress;
        private System.Windows.Forms.ComboBox cboPaymentType;
        private System.Windows.Forms.ComboBox cboBillState;
        private System.Windows.Forms.TextBox txtXCAlias;
        private System.Windows.Forms.Label lblXCAlias;
        private System.Windows.Forms.TextBox txtPNRef;
        private System.Windows.Forms.Label lblPNRef;
        private System.Windows.Forms.TextBox txtXCEFTAuthCode;
        private System.Windows.Forms.TextBox txtXCEFTRefID;
        private System.Windows.Forms.Label lblXCEFTAuthCode;
        private System.Windows.Forms.Label lblXCEFTRefID;
        private System.Windows.Forms.Button btnCopyFromBillingInfo;
        private System.Windows.Forms.Label lblEPSPmtAcctID;
        private System.Windows.Forms.TextBox txtEPSPmtAcctID;
    }
}