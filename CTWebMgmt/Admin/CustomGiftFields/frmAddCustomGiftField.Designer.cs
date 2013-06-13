namespace CTWebMgmt.Admin
{
    partial class frmAddCustomGiftField
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.txtDefaultVal = new System.Windows.Forms.TextBox();
            this.cboFieldType = new System.Windows.Forms.ComboBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.cboValidation = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtSortOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Field Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Field Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Default Value:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(84, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Validation:";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(164, 83);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(100, 20);
            this.txtFieldName.TabIndex = 5;
            // 
            // txtDefaultVal
            // 
            this.txtDefaultVal.Location = new System.Drawing.Point(164, 159);
            this.txtDefaultVal.Name = "txtDefaultVal";
            this.txtDefaultVal.Size = new System.Drawing.Size(100, 20);
            this.txtDefaultVal.TabIndex = 6;
            // 
            // cboFieldType
            // 
            this.cboFieldType.FormattingEnabled = true;
            this.cboFieldType.Items.AddRange(new object[] {
            "Text",
            "Multi-line Text",
            "Yes/No",
            "Dropdown"});
            this.cboFieldType.Location = new System.Drawing.Point(164, 109);
            this.cboFieldType.Name = "cboFieldType";
            this.cboFieldType.Size = new System.Drawing.Size(121, 21);
            this.cboFieldType.TabIndex = 7;
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(164, 136);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(75, 17);
            this.chkRequired.TabIndex = 8;
            this.chkRequired.Text = "Required?";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // cboValidation
            // 
            this.cboValidation.FormattingEnabled = true;
            this.cboValidation.Items.AddRange(new object[] {
            "None",
            "Numeric",
            "Date"});
            this.cboValidation.Location = new System.Drawing.Point(164, 185);
            this.cboValidation.Name = "cboValidation";
            this.cboValidation.Size = new System.Drawing.Size(121, 21);
            this.cboValidation.TabIndex = 9;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(229, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(293, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtSortOrder
            // 
            this.txtSortOrder.Location = new System.Drawing.Point(164, 212);
            this.txtSortOrder.Name = "txtSortOrder";
            this.txtSortOrder.Size = new System.Drawing.Size(100, 20);
            this.txtSortOrder.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Sort Order:";
            // 
            // frmAddCustomGiftField
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 248);
            this.Controls.Add(this.txtSortOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboValidation);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.cboFieldType);
            this.Controls.Add(this.txtDefaultVal);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmAddCustomGiftField";
            this.Text = "Add Custom Gift Field";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.TextBox txtDefaultVal;
        private System.Windows.Forms.ComboBox cboFieldType;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.ComboBox cboValidation;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSortOrder;
        private System.Windows.Forms.Label label3;
    }
}