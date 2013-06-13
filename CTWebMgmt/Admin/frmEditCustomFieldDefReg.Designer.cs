namespace CTWebMgmt.Admin
{
    partial class frmEditCustomFieldDefReg
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLocalCaption = new System.Windows.Forms.TextBox();
            this.chkUseLocal = new System.Windows.Forms.CheckBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.chkUseOnline = new System.Windows.Forms.CheckBox();
            this.txtSortOrder = new System.Windows.Forms.TextBox();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtWebCaption = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblWebCaption = new System.Windows.Forms.Label();
            this.lblFieldType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDropdownOptions = new System.Windows.Forms.Label();
            this.txtDropdownOptions = new System.Windows.Forms.TextBox();
            this.lblCharge = new System.Windows.Forms.Label();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(494, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 29);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Save and Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(592, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtLocalCaption
            // 
            this.txtLocalCaption.Location = new System.Drawing.Point(118, 115);
            this.txtLocalCaption.Name = "txtLocalCaption";
            this.txtLocalCaption.Size = new System.Drawing.Size(181, 20);
            this.txtLocalCaption.TabIndex = 2;
            // 
            // chkUseLocal
            // 
            this.chkUseLocal.AutoSize = true;
            this.chkUseLocal.Location = new System.Drawing.Point(29, 88);
            this.chkUseLocal.Name = "chkUseLocal";
            this.chkUseLocal.Size = new System.Drawing.Size(74, 17);
            this.chkUseLocal.TabIndex = 3;
            this.chkUseLocal.Text = "Use Local";
            this.chkUseLocal.UseVisualStyleBackColor = true;
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(27, 453);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(69, 17);
            this.chkRequired.TabIndex = 5;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // chkUseOnline
            // 
            this.chkUseOnline.AutoSize = true;
            this.chkUseOnline.Location = new System.Drawing.Point(29, 213);
            this.chkUseOnline.Name = "chkUseOnline";
            this.chkUseOnline.Size = new System.Drawing.Size(86, 17);
            this.chkUseOnline.TabIndex = 6;
            this.chkUseOnline.Text = "Show Online";
            this.chkUseOnline.UseVisualStyleBackColor = true;
            this.chkUseOnline.CheckedChanged += new System.EventHandler(this.chkUseProfileCamper_CheckedChanged);
            // 
            // txtSortOrder
            // 
            this.txtSortOrder.Location = new System.Drawing.Point(118, 141);
            this.txtSortOrder.Name = "txtSortOrder";
            this.txtSortOrder.Size = new System.Drawing.Size(100, 20);
            this.txtSortOrder.TabIndex = 7;
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(118, 370);
            this.txtFooter.Multiline = true;
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(303, 59);
            this.txtFooter.TabIndex = 8;
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(118, 305);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(303, 59);
            this.txtHeader.TabIndex = 9;
            // 
            // txtWebCaption
            // 
            this.txtWebCaption.Location = new System.Drawing.Point(118, 240);
            this.txtWebCaption.Multiline = true;
            this.txtWebCaption.Name = "txtWebCaption";
            this.txtWebCaption.Size = new System.Drawing.Size(303, 59);
            this.txtWebCaption.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Local Caption:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Sort Order:";
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Location = new System.Drawing.Point(26, 373);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(70, 13);
            this.lblFooter.TabIndex = 13;
            this.lblFooter.Text = "Online Footer";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(26, 308);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(78, 13);
            this.lblHeader.TabIndex = 14;
            this.lblHeader.Text = "Online Header:";
            // 
            // lblWebCaption
            // 
            this.lblWebCaption.AutoSize = true;
            this.lblWebCaption.Location = new System.Drawing.Point(26, 243);
            this.lblWebCaption.Name = "lblWebCaption";
            this.lblWebCaption.Size = new System.Drawing.Size(79, 13);
            this.lblWebCaption.TabIndex = 15;
            this.lblWebCaption.Text = "Online Caption:";
            // 
            // lblFieldType
            // 
            this.lblFieldType.AutoSize = true;
            this.lblFieldType.Location = new System.Drawing.Point(115, 58);
            this.lblFieldType.Name = "lblFieldType";
            this.lblFieldType.Size = new System.Drawing.Size(68, 13);
            this.lblFieldType.TabIndex = 16;
            this.lblFieldType.Text = "FIELD TYPE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Field Type:";
            // 
            // lblDropdownOptions
            // 
            this.lblDropdownOptions.AutoSize = true;
            this.lblDropdownOptions.Location = new System.Drawing.Point(438, 99);
            this.lblDropdownOptions.Name = "lblDropdownOptions";
            this.lblDropdownOptions.Size = new System.Drawing.Size(98, 13);
            this.lblDropdownOptions.TabIndex = 19;
            this.lblDropdownOptions.Text = "Dropdown Options:";
            // 
            // txtDropdownOptions
            // 
            this.txtDropdownOptions.Location = new System.Drawing.Point(441, 115);
            this.txtDropdownOptions.Multiline = true;
            this.txtDropdownOptions.Name = "txtDropdownOptions";
            this.txtDropdownOptions.Size = new System.Drawing.Size(229, 161);
            this.txtDropdownOptions.TabIndex = 18;
            // 
            // lblCharge
            // 
            this.lblCharge.AutoSize = true;
            this.lblCharge.Location = new System.Drawing.Point(26, 170);
            this.lblCharge.Name = "lblCharge";
            this.lblCharge.Size = new System.Drawing.Size(44, 13);
            this.lblCharge.TabIndex = 21;
            this.lblCharge.Text = "Charge:";
            // 
            // txtCharge
            // 
            this.txtCharge.Location = new System.Drawing.Point(118, 167);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(100, 20);
            this.txtCharge.TabIndex = 20;
            // 
            // frmEditCustomFieldDefReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 484);
            this.Controls.Add(this.lblCharge);
            this.Controls.Add(this.txtCharge);
            this.Controls.Add(this.lblDropdownOptions);
            this.Controls.Add(this.txtDropdownOptions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblFieldType);
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
            this.Name = "frmEditCustomFieldDefReg";
            this.Text = "Edit Custom Reg Field Definition";
            this.Load += new System.EventHandler(this.frmEditCustomFieldDefReg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtLocalCaption;
        private System.Windows.Forms.CheckBox chkUseLocal;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.CheckBox chkUseOnline;
        private System.Windows.Forms.TextBox txtSortOrder;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtWebCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblWebCaption;
        private System.Windows.Forms.Label lblFieldType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDropdownOptions;
        private System.Windows.Forms.TextBox txtDropdownOptions;
        private System.Windows.Forms.Label lblCharge;
        private System.Windows.Forms.TextBox txtCharge;
    }
}