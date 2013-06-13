namespace CTWebMgmt.Admin
{
    partial class frmAddCustomFieldDefReg
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
            this.cboFieldType = new System.Windows.Forms.ComboBox();
            this.lblDropdownOptions = new System.Windows.Forms.Label();
            this.txtDropdownOptions = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblWebCaption = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWebCaption = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.txtSortOrder = new System.Windows.Forms.TextBox();
            this.chkUseOnline = new System.Windows.Forms.CheckBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkUseLocal = new System.Windows.Forms.CheckBox();
            this.txtLocalCaption = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCharge = new System.Windows.Forms.Label();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboFieldType
            // 
            this.cboFieldType.FormattingEnabled = true;
            this.cboFieldType.Items.AddRange(new object[] {
            "FIELD",
            "MULTI-LINE TEXT",
            "FLAG",
            "DROPDOWN"});
            this.cboFieldType.Location = new System.Drawing.Point(103, 55);
            this.cboFieldType.Name = "cboFieldType";
            this.cboFieldType.Size = new System.Drawing.Size(147, 21);
            this.cboFieldType.TabIndex = 60;
            this.cboFieldType.SelectedIndexChanged += new System.EventHandler(this.cboFieldType_SelectedIndexChanged);
            // 
            // lblDropdownOptions
            // 
            this.lblDropdownOptions.AutoSize = true;
            this.lblDropdownOptions.Location = new System.Drawing.Point(423, 99);
            this.lblDropdownOptions.Name = "lblDropdownOptions";
            this.lblDropdownOptions.Size = new System.Drawing.Size(98, 13);
            this.lblDropdownOptions.TabIndex = 59;
            this.lblDropdownOptions.Text = "Dropdown Options:";
            // 
            // txtDropdownOptions
            // 
            this.txtDropdownOptions.Location = new System.Drawing.Point(426, 115);
            this.txtDropdownOptions.Multiline = true;
            this.txtDropdownOptions.Name = "txtDropdownOptions";
            this.txtDropdownOptions.Size = new System.Drawing.Size(229, 161);
            this.txtDropdownOptions.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Field Type:";
            // 
            // lblWebCaption
            // 
            this.lblWebCaption.AutoSize = true;
            this.lblWebCaption.Location = new System.Drawing.Point(11, 242);
            this.lblWebCaption.Name = "lblWebCaption";
            this.lblWebCaption.Size = new System.Drawing.Size(79, 13);
            this.lblWebCaption.TabIndex = 56;
            this.lblWebCaption.Text = "Online Caption:";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(11, 307);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(78, 13);
            this.lblHeader.TabIndex = 55;
            this.lblHeader.Text = "Online Header:";
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(11, 372);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(70, 13);
            this.lblFooter.TabIndex = 54;
            this.lblFooter.Text = "Online Footer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Sort Order:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Local Caption:";
            // 
            // txtWebCaption
            // 
            this.txtWebCaption.Location = new System.Drawing.Point(103, 239);
            this.txtWebCaption.Multiline = true;
            this.txtWebCaption.Name = "txtWebCaption";
            this.txtWebCaption.Size = new System.Drawing.Size(303, 59);
            this.txtWebCaption.TabIndex = 51;
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(103, 304);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(303, 59);
            this.txtHeader.TabIndex = 50;
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(103, 369);
            this.txtFooter.Multiline = true;
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(303, 59);
            this.txtFooter.TabIndex = 49;
            // 
            // txtSortOrder
            // 
            this.txtSortOrder.Location = new System.Drawing.Point(103, 141);
            this.txtSortOrder.Name = "txtSortOrder";
            this.txtSortOrder.Size = new System.Drawing.Size(100, 20);
            this.txtSortOrder.TabIndex = 48;
            // 
            // chkUseOnline
            // 
            this.chkUseOnline.AutoSize = true;
            this.chkUseOnline.Location = new System.Drawing.Point(14, 206);
            this.chkUseOnline.Name = "chkUseOnline";
            this.chkUseOnline.Size = new System.Drawing.Size(86, 17);
            this.chkUseOnline.TabIndex = 47;
            this.chkUseOnline.Text = "Show Online";
            this.chkUseOnline.UseVisualStyleBackColor = true;
            this.chkUseOnline.CheckedChanged += new System.EventHandler(this.chkUseOnline_CheckedChanged);
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(12, 452);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(69, 17);
            this.chkRequired.TabIndex = 46;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // chkUseLocal
            // 
            this.chkUseLocal.AutoSize = true;
            this.chkUseLocal.Location = new System.Drawing.Point(14, 88);
            this.chkUseLocal.Name = "chkUseLocal";
            this.chkUseLocal.Size = new System.Drawing.Size(74, 17);
            this.chkUseLocal.TabIndex = 44;
            this.chkUseLocal.Text = "Use Local";
            this.chkUseLocal.UseVisualStyleBackColor = true;
            // 
            // txtLocalCaption
            // 
            this.txtLocalCaption.Location = new System.Drawing.Point(103, 115);
            this.txtLocalCaption.Name = "txtLocalCaption";
            this.txtLocalCaption.Size = new System.Drawing.Size(181, 20);
            this.txtLocalCaption.TabIndex = 43;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(580, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 29);
            this.btnCancel.TabIndex = 42;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(482, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 29);
            this.btnClose.TabIndex = 41;
            this.btnClose.Text = "&Save and Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCharge
            // 
            this.lblCharge.AutoSize = true;
            this.lblCharge.Location = new System.Drawing.Point(11, 170);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(44, 13);
            this.lblCharge.TabIndex = 62;
            this.lblCharge.Text = "Charge:";
            // 
            // txtCharge
            // 
            this.txtCharge.Location = new System.Drawing.Point(103, 167);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(100, 20);
            this.txtCharge.TabIndex = 61;
            // 
            // frmAddCustomFieldDefReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 578);
            this.Controls.Add(this.lblCharge);
            this.Controls.Add(this.txtCharge);
            this.Controls.Add(this.cboFieldType);
            this.Controls.Add(this.lblDropdownOptions);
            this.Controls.Add(this.txtDropdownOptions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblWebCaption);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWebCaption);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.txtFooter);
            this.Controls.Add(this.txtSortOrder);
            this.Controls.Add(this.chkUseOnline);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.chkUseLocal);
            this.Controls.Add(this.txtLocalCaption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Name = "frmAddCustomFieldDefReg";
            this.Text = "Add Custom Reg Field Definition";
            this.Load += new System.EventHandler(this.frmAddCustomFieldDefReg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboFieldType;
        private System.Windows.Forms.Label lblDropdownOptions;
        private System.Windows.Forms.TextBox txtDropdownOptions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblWebCaption;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWebCaption;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.TextBox txtSortOrder;
        private System.Windows.Forms.CheckBox chkUseOnline;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.CheckBox chkUseLocal;
        private System.Windows.Forms.TextBox txtLocalCaption;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblCharge;
        private System.Windows.Forms.TextBox txtCharge;
    }
}