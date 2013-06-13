namespace CTWebMgmt.Admin
{
    partial class frmAddCustomFieldDefIR
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
            this.chkUseProfile = new System.Windows.Forms.CheckBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkUseCamper = new System.Windows.Forms.CheckBox();
            this.chkUseLocal = new System.Windows.Forms.CheckBox();
            this.txtLocalCaption = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboFieldType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblDropdownOptions
            // 
            this.lblDropdownOptions.AutoSize = true;
            this.lblDropdownOptions.Location = new System.Drawing.Point(423, 99);
            this.lblDropdownOptions.Name = "lblDropdownOptions";
            this.lblDropdownOptions.Size = new System.Drawing.Size(98, 13);
            this.lblDropdownOptions.TabIndex = 39;
            this.lblDropdownOptions.Text = "Dropdown Options:";
            // 
            // txtDropdownOptions
            // 
            this.txtDropdownOptions.Location = new System.Drawing.Point(426, 115);
            this.txtDropdownOptions.Multiline = true;
            this.txtDropdownOptions.Name = "txtDropdownOptions";
            this.txtDropdownOptions.Size = new System.Drawing.Size(229, 161);
            this.txtDropdownOptions.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Field Type:";
            // 
            // lblWebCaption
            // 
            this.lblWebCaption.AutoSize = true;
            this.lblWebCaption.Location = new System.Drawing.Point(11, 237);
            this.lblWebCaption.Name = "lblWebCaption";
            this.lblWebCaption.Size = new System.Drawing.Size(79, 13);
            this.lblWebCaption.TabIndex = 35;
            this.lblWebCaption.Text = "Online Caption:";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(11, 302);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(78, 13);
            this.lblHeader.TabIndex = 34;
            this.lblHeader.Text = "Online Header:";
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(11, 367);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(70, 13);
            this.lblFooter.TabIndex = 33;
            this.lblFooter.Text = "Online Footer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Sort Order:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Local Caption:";
            // 
            // txtWebCaption
            // 
            this.txtWebCaption.Location = new System.Drawing.Point(103, 234);
            this.txtWebCaption.Multiline = true;
            this.txtWebCaption.Name = "txtWebCaption";
            this.txtWebCaption.Size = new System.Drawing.Size(303, 59);
            this.txtWebCaption.TabIndex = 30;
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(103, 299);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(303, 59);
            this.txtHeader.TabIndex = 29;
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(103, 364);
            this.txtFooter.Multiline = true;
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(303, 59);
            this.txtFooter.TabIndex = 28;
            // 
            // txtSortOrder
            // 
            this.txtSortOrder.Location = new System.Drawing.Point(103, 141);
            this.txtSortOrder.Name = "txtSortOrder";
            this.txtSortOrder.Size = new System.Drawing.Size(100, 20);
            this.txtSortOrder.TabIndex = 27;
            // 
            // chkUseProfile
            // 
            this.chkUseProfile.AutoSize = true;
            this.chkUseProfile.Location = new System.Drawing.Point(14, 183);
            this.chkUseProfile.Name = "chkUseProfile";
            this.chkUseProfile.Size = new System.Drawing.Size(121, 17);
            this.chkUseProfile.TabIndex = 26;
            this.chkUseProfile.Text = "Show Online, Profile";
            this.chkUseProfile.UseVisualStyleBackColor = true;
            this.chkUseProfile.CheckedChanged += new System.EventHandler(this.chkUseProfileCamper_CheckedChanged);
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(12, 447);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(69, 17);
            this.chkRequired.TabIndex = 25;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // chkUseCamper
            // 
            this.chkUseCamper.AutoSize = true;
            this.chkUseCamper.Location = new System.Drawing.Point(14, 206);
            this.chkUseCamper.Name = "chkUseCamper";
            this.chkUseCamper.Size = new System.Drawing.Size(128, 17);
            this.chkUseCamper.TabIndex = 24;
            this.chkUseCamper.Text = "Show Online, Camper";
            this.chkUseCamper.UseVisualStyleBackColor = true;
            this.chkUseCamper.CheckedChanged += new System.EventHandler(this.chkUseProfileCamper_CheckedChanged);
            // 
            // chkUseLocal
            // 
            this.chkUseLocal.AutoSize = true;
            this.chkUseLocal.Location = new System.Drawing.Point(14, 88);
            this.chkUseLocal.Name = "chkUseLocal";
            this.chkUseLocal.Size = new System.Drawing.Size(74, 17);
            this.chkUseLocal.TabIndex = 23;
            this.chkUseLocal.Text = "Use Local";
            this.chkUseLocal.UseVisualStyleBackColor = true;
            // 
            // txtLocalCaption
            // 
            this.txtLocalCaption.Location = new System.Drawing.Point(103, 115);
            this.txtLocalCaption.Name = "txtLocalCaption";
            this.txtLocalCaption.Size = new System.Drawing.Size(181, 20);
            this.txtLocalCaption.TabIndex = 22;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(580, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 29);
            this.btnCancel.TabIndex = 21;
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
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "&Save and Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.cboFieldType.TabIndex = 40;
            this.cboFieldType.SelectedIndexChanged += new System.EventHandler(this.cboFieldType_SelectedIndexChanged);
            // 
            // frmAddCustomFieldDefIR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 578);
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
            this.Controls.Add(this.chkUseProfile);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.chkUseCamper);
            this.Controls.Add(this.chkUseLocal);
            this.Controls.Add(this.txtLocalCaption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Name = "frmAddCustomFieldDefIR";
            this.Text = "Add Custom IR Field Definition";
            this.Load += new System.EventHandler(this.frmAddCustomFieldDefIR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.CheckBox chkUseProfile;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.CheckBox chkUseCamper;
        private System.Windows.Forms.CheckBox chkUseLocal;
        private System.Windows.Forms.TextBox txtLocalCaption;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboFieldType;
    }
}