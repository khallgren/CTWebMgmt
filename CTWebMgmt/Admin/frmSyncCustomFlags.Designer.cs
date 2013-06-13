namespace CTWebMgmt.Admin
{
    partial class frmSyncCustomFlags
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
            this.tabCustomFlagsFields = new System.Windows.Forms.TabControl();
            this.pagIRCustom = new System.Windows.Forms.TabPage();
            this.btnAddFieldIR = new System.Windows.Forms.Button();
            this.lblSortOrderHead = new System.Windows.Forms.Label();
            this.lblFieldTypeHead = new System.Windows.Forms.Label();
            this.lblRequiredHead = new System.Windows.Forms.Label();
            this.lblOnlineLabelHead = new System.Windows.Forms.Label();
            this.lblUseProfileHead = new System.Windows.Forms.Label();
            this.lblUseCamperHead = new System.Windows.Forms.Label();
            this.lblLocalLabelHead = new System.Windows.Forms.Label();
            this.lblUseLocalHead = new System.Windows.Forms.Label();
            this.pagRegCustom = new System.Windows.Forms.TabPage();
            this.btnAddFieldReg = new System.Windows.Forms.Button();
            this.lblChargeRegHead = new System.Windows.Forms.Label();
            this.lblSortOrderRegHead = new System.Windows.Forms.Label();
            this.lblFieldTypeRegHead = new System.Windows.Forms.Label();
            this.lblRequiredRegHead = new System.Windows.Forms.Label();
            this.lblWebCaptionRegHead = new System.Windows.Forms.Label();
            this.lblUseOnlineRegHead = new System.Windows.Forms.Label();
            this.lblLocalCaptionRegHead = new System.Windows.Forms.Label();
            this.lblUseLocalRegHead = new System.Windows.Forms.Label();
            this.pagGiftFlagsFields = new System.Windows.Forms.TabPage();
            this.btnAddCustomGiftField = new System.Windows.Forms.Button();
            this.grdCustomGiftFieldsLocal = new System.Windows.Forms.DataGridView();
            this.colSortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colValidation = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnULCustomGiftFields = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboProgramSpecificDef = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkExcludeFromGeneral = new System.Windows.Forms.CheckBox();
            this.tabCustomFlagsFields.SuspendLayout();
            this.pagIRCustom.SuspendLayout();
            this.pagRegCustom.SuspendLayout();
            this.pagGiftFlagsFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomGiftFieldsLocal)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCustomFlagsFields
            // 
            this.tabCustomFlagsFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCustomFlagsFields.Controls.Add(this.pagIRCustom);
            this.tabCustomFlagsFields.Controls.Add(this.pagRegCustom);
            this.tabCustomFlagsFields.Controls.Add(this.pagGiftFlagsFields);
            this.tabCustomFlagsFields.Location = new System.Drawing.Point(0, 31);
            this.tabCustomFlagsFields.Name = "tabCustomFlagsFields";
            this.tabCustomFlagsFields.SelectedIndex = 0;
            this.tabCustomFlagsFields.Size = new System.Drawing.Size(1023, 757);
            this.tabCustomFlagsFields.TabIndex = 281;
            // 
            // pagIRCustom
            // 
            this.pagIRCustom.AutoScroll = true;
            this.pagIRCustom.Controls.Add(this.btnAddFieldIR);
            this.pagIRCustom.Controls.Add(this.lblSortOrderHead);
            this.pagIRCustom.Controls.Add(this.lblFieldTypeHead);
            this.pagIRCustom.Controls.Add(this.lblRequiredHead);
            this.pagIRCustom.Controls.Add(this.lblOnlineLabelHead);
            this.pagIRCustom.Controls.Add(this.lblUseProfileHead);
            this.pagIRCustom.Controls.Add(this.lblUseCamperHead);
            this.pagIRCustom.Controls.Add(this.lblLocalLabelHead);
            this.pagIRCustom.Controls.Add(this.lblUseLocalHead);
            this.pagIRCustom.Location = new System.Drawing.Point(4, 22);
            this.pagIRCustom.Name = "pagIRCustom";
            this.pagIRCustom.Padding = new System.Windows.Forms.Padding(3);
            this.pagIRCustom.Size = new System.Drawing.Size(1015, 731);
            this.pagIRCustom.TabIndex = 7;
            this.pagIRCustom.Text = "Contact Fields";
            this.pagIRCustom.UseVisualStyleBackColor = true;
            // 
            // btnAddFieldIR
            // 
            this.btnAddFieldIR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFieldIR.Location = new System.Drawing.Point(909, 6);
            this.btnAddFieldIR.Name = "btnAddFieldIR";
            this.btnAddFieldIR.Size = new System.Drawing.Size(100, 26);
            this.btnAddFieldIR.TabIndex = 864;
            this.btnAddFieldIR.Text = "Add Field";
            this.btnAddFieldIR.UseVisualStyleBackColor = true;
            this.btnAddFieldIR.Click += new System.EventHandler(this.btnAddFieldIR_Click);
            // 
            // lblSortOrderHead
            // 
            this.lblSortOrderHead.AutoSize = true;
            this.lblSortOrderHead.Location = new System.Drawing.Point(764, 39);
            this.lblSortOrderHead.Name = "lblSortOrderHead";
            this.lblSortOrderHead.Size = new System.Drawing.Size(55, 13);
            this.lblSortOrderHead.TabIndex = 863;
            this.lblSortOrderHead.Text = "Sort Order";
            // 
            // lblFieldTypeHead
            // 
            this.lblFieldTypeHead.AutoSize = true;
            this.lblFieldTypeHead.Location = new System.Drawing.Point(22, 39);
            this.lblFieldTypeHead.Name = "lblFieldTypeHead";
            this.lblFieldTypeHead.Size = new System.Drawing.Size(56, 13);
            this.lblFieldTypeHead.TabIndex = 861;
            this.lblFieldTypeHead.Text = "Field Type";
            // 
            // lblRequiredHead
            // 
            this.lblRequiredHead.AutoSize = true;
            this.lblRequiredHead.Location = new System.Drawing.Point(697, 39);
            this.lblRequiredHead.Name = "lblRequiredHead";
            this.lblRequiredHead.Size = new System.Drawing.Size(56, 13);
            this.lblRequiredHead.TabIndex = 854;
            this.lblRequiredHead.Text = "Required?";
            // 
            // lblOnlineLabelHead
            // 
            this.lblOnlineLabelHead.AutoSize = true;
            this.lblOnlineLabelHead.Location = new System.Drawing.Point(526, 39);
            this.lblOnlineLabelHead.Name = "lblOnlineLabelHead";
            this.lblOnlineLabelHead.Size = new System.Drawing.Size(66, 13);
            this.lblOnlineLabelHead.TabIndex = 847;
            this.lblOnlineLabelHead.Text = "Online Label";
            // 
            // lblUseProfileHead
            // 
            this.lblUseProfileHead.AutoSize = true;
            this.lblUseProfileHead.Location = new System.Drawing.Point(390, 26);
            this.lblUseProfileHead.Name = "lblUseProfileHead";
            this.lblUseProfileHead.Size = new System.Drawing.Size(62, 26);
            this.lblUseProfileHead.TabIndex = 846;
            this.lblUseProfileHead.Text = "Use Online,\r\nProfile";
            // 
            // lblUseCamperHead
            // 
            this.lblUseCamperHead.AutoSize = true;
            this.lblUseCamperHead.Location = new System.Drawing.Point(458, 26);
            this.lblUseCamperHead.Name = "lblUseCamperHead";
            this.lblUseCamperHead.Size = new System.Drawing.Size(62, 26);
            this.lblUseCamperHead.TabIndex = 845;
            this.lblUseCamperHead.Text = "Use Online,\r\nCamper";
            // 
            // lblLocalLabelHead
            // 
            this.lblLocalLabelHead.AutoSize = true;
            this.lblLocalLabelHead.Location = new System.Drawing.Point(215, 39);
            this.lblLocalLabelHead.Name = "lblLocalLabelHead";
            this.lblLocalLabelHead.Size = new System.Drawing.Size(62, 13);
            this.lblLocalLabelHead.TabIndex = 844;
            this.lblLocalLabelHead.Text = "Local Label";
            // 
            // lblUseLocalHead
            // 
            this.lblUseLocalHead.AutoSize = true;
            this.lblUseLocalHead.Location = new System.Drawing.Point(136, 39);
            this.lblUseLocalHead.Name = "lblUseLocalHead";
            this.lblUseLocalHead.Size = new System.Drawing.Size(62, 13);
            this.lblUseLocalHead.TabIndex = 843;
            this.lblUseLocalHead.Text = "Use Locally";
            // 
            // pagRegCustom
            // 
            this.pagRegCustom.AutoScroll = true;
            this.pagRegCustom.Controls.Add(this.btnAddFieldReg);
            this.pagRegCustom.Controls.Add(this.lblChargeRegHead);
            this.pagRegCustom.Controls.Add(this.lblSortOrderRegHead);
            this.pagRegCustom.Controls.Add(this.lblFieldTypeRegHead);
            this.pagRegCustom.Controls.Add(this.lblRequiredRegHead);
            this.pagRegCustom.Controls.Add(this.lblWebCaptionRegHead);
            this.pagRegCustom.Controls.Add(this.lblUseOnlineRegHead);
            this.pagRegCustom.Controls.Add(this.lblLocalCaptionRegHead);
            this.pagRegCustom.Controls.Add(this.lblUseLocalRegHead);
            this.pagRegCustom.Location = new System.Drawing.Point(4, 22);
            this.pagRegCustom.Name = "pagRegCustom";
            this.pagRegCustom.Padding = new System.Windows.Forms.Padding(3);
            this.pagRegCustom.Size = new System.Drawing.Size(1015, 731);
            this.pagRegCustom.TabIndex = 8;
            this.pagRegCustom.Text = "Registration Fields";
            this.pagRegCustom.UseVisualStyleBackColor = true;
            // 
            // btnAddFieldReg
            // 
            this.btnAddFieldReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFieldReg.Location = new System.Drawing.Point(909, 6);
            this.btnAddFieldReg.Name = "btnAddFieldReg";
            this.btnAddFieldReg.Size = new System.Drawing.Size(100, 26);
            this.btnAddFieldReg.TabIndex = 873;
            this.btnAddFieldReg.Text = "Add Field";
            this.btnAddFieldReg.UseVisualStyleBackColor = true;
            this.btnAddFieldReg.Click += new System.EventHandler(this.btnAddFieldReg_Click);
            // 
            // lblChargeRegHead
            // 
            this.lblChargeRegHead.AutoSize = true;
            this.lblChargeRegHead.Location = new System.Drawing.Point(711, 39);
            this.lblChargeRegHead.Name = "lblChargeRegHead";
            this.lblChargeRegHead.Size = new System.Drawing.Size(41, 13);
            this.lblChargeRegHead.TabIndex = 872;
            this.lblChargeRegHead.Text = "Charge";
            // 
            // lblSortOrderRegHead
            // 
            this.lblSortOrderRegHead.AutoSize = true;
            this.lblSortOrderRegHead.Location = new System.Drawing.Point(776, 39);
            this.lblSortOrderRegHead.Name = "lblSortOrderRegHead";
            this.lblSortOrderRegHead.Size = new System.Drawing.Size(55, 13);
            this.lblSortOrderRegHead.TabIndex = 871;
            this.lblSortOrderRegHead.Text = "Sort Order";
            // 
            // lblFieldTypeRegHead
            // 
            this.lblFieldTypeRegHead.AutoSize = true;
            this.lblFieldTypeRegHead.Location = new System.Drawing.Point(22, 39);
            this.lblFieldTypeRegHead.Name = "lblFieldTypeRegHead";
            this.lblFieldTypeRegHead.Size = new System.Drawing.Size(56, 13);
            this.lblFieldTypeRegHead.TabIndex = 870;
            this.lblFieldTypeRegHead.Text = "Field Type";
            // 
            // lblRequiredRegHead
            // 
            this.lblRequiredRegHead.AutoSize = true;
            this.lblRequiredRegHead.Location = new System.Drawing.Point(633, 39);
            this.lblRequiredRegHead.Name = "lblRequiredRegHead";
            this.lblRequiredRegHead.Size = new System.Drawing.Size(56, 13);
            this.lblRequiredRegHead.TabIndex = 869;
            this.lblRequiredRegHead.Text = "Required?";
            // 
            // lblWebCaptionRegHead
            // 
            this.lblWebCaptionRegHead.AutoSize = true;
            this.lblWebCaptionRegHead.Location = new System.Drawing.Point(465, 39);
            this.lblWebCaptionRegHead.Name = "lblWebCaptionRegHead";
            this.lblWebCaptionRegHead.Size = new System.Drawing.Size(66, 13);
            this.lblWebCaptionRegHead.TabIndex = 868;
            this.lblWebCaptionRegHead.Text = "Online Label";
            // 
            // lblUseOnlineRegHead
            // 
            this.lblUseOnlineRegHead.AutoSize = true;
            this.lblUseOnlineRegHead.Location = new System.Drawing.Point(390, 39);
            this.lblUseOnlineRegHead.Name = "lblUseOnlineRegHead";
            this.lblUseOnlineRegHead.Size = new System.Drawing.Size(59, 13);
            this.lblUseOnlineRegHead.TabIndex = 867;
            this.lblUseOnlineRegHead.Text = "Use Online";
            // 
            // lblLocalCaptionRegHead
            // 
            this.lblLocalCaptionRegHead.AutoSize = true;
            this.lblLocalCaptionRegHead.Location = new System.Drawing.Point(215, 39);
            this.lblLocalCaptionRegHead.Name = "lblLocalCaptionRegHead";
            this.lblLocalCaptionRegHead.Size = new System.Drawing.Size(62, 13);
            this.lblLocalCaptionRegHead.TabIndex = 865;
            this.lblLocalCaptionRegHead.Text = "Local Label";
            // 
            // lblUseLocalRegHead
            // 
            this.lblUseLocalRegHead.AutoSize = true;
            this.lblUseLocalRegHead.Location = new System.Drawing.Point(136, 39);
            this.lblUseLocalRegHead.Name = "lblUseLocalRegHead";
            this.lblUseLocalRegHead.Size = new System.Drawing.Size(62, 13);
            this.lblUseLocalRegHead.TabIndex = 864;
            this.lblUseLocalRegHead.Text = "Use Locally";
            // 
            // pagGiftFlagsFields
            // 
            this.pagGiftFlagsFields.Controls.Add(this.btnAddCustomGiftField);
            this.pagGiftFlagsFields.Controls.Add(this.grdCustomGiftFieldsLocal);
            this.pagGiftFlagsFields.Controls.Add(this.btnULCustomGiftFields);
            this.pagGiftFlagsFields.Location = new System.Drawing.Point(4, 22);
            this.pagGiftFlagsFields.Name = "pagGiftFlagsFields";
            this.pagGiftFlagsFields.Padding = new System.Windows.Forms.Padding(3);
            this.pagGiftFlagsFields.Size = new System.Drawing.Size(1015, 731);
            this.pagGiftFlagsFields.TabIndex = 2;
            this.pagGiftFlagsFields.Text = "Gift Flags/Fields";
            this.pagGiftFlagsFields.UseVisualStyleBackColor = true;
            // 
            // btnAddCustomGiftField
            // 
            this.btnAddCustomGiftField.Location = new System.Drawing.Point(8, 21);
            this.btnAddCustomGiftField.Name = "btnAddCustomGiftField";
            this.btnAddCustomGiftField.Size = new System.Drawing.Size(136, 23);
            this.btnAddCustomGiftField.TabIndex = 298;
            this.btnAddCustomGiftField.Text = "Add Custom Gift Field";
            this.btnAddCustomGiftField.UseVisualStyleBackColor = true;
            this.btnAddCustomGiftField.Click += new System.EventHandler(this.btnAddCustomGiftField_Click);
            // 
            // grdCustomGiftFieldsLocal
            // 
            this.grdCustomGiftFieldsLocal.AllowUserToAddRows = false;
            this.grdCustomGiftFieldsLocal.AllowUserToDeleteRows = false;
            this.grdCustomGiftFieldsLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCustomGiftFieldsLocal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSortOrder,
            this.colFieldName,
            this.colFieldType,
            this.colValidation,
            this.colEdit,
            this.colDelete});
            this.grdCustomGiftFieldsLocal.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdCustomGiftFieldsLocal.Location = new System.Drawing.Point(8, 50);
            this.grdCustomGiftFieldsLocal.Name = "grdCustomGiftFieldsLocal";
            this.grdCustomGiftFieldsLocal.ReadOnly = true;
            this.grdCustomGiftFieldsLocal.RowHeadersVisible = false;
            this.grdCustomGiftFieldsLocal.Size = new System.Drawing.Size(610, 694);
            this.grdCustomGiftFieldsLocal.TabIndex = 297;
            this.grdCustomGiftFieldsLocal.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCustomGiftFieldsLocal_CellClick);
            // 
            // colSortOrder
            // 
            this.colSortOrder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSortOrder.DataPropertyName = "intSortOrder";
            this.colSortOrder.HeaderText = "Sort Order";
            this.colSortOrder.Name = "colSortOrder";
            this.colSortOrder.ReadOnly = true;
            this.colSortOrder.Width = 80;
            // 
            // colFieldName
            // 
            this.colFieldName.DataPropertyName = "strFieldName";
            this.colFieldName.HeaderText = "Field Name";
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.ReadOnly = true;
            // 
            // colFieldType
            // 
            this.colFieldType.DataPropertyName = "strFieldType";
            this.colFieldType.HeaderText = "Field Type";
            this.colFieldType.Items.AddRange(new object[] {
            "Text",
            "Multi-line Text",
            "Yes/No",
            "Dropdown"});
            this.colFieldType.Name = "colFieldType";
            this.colFieldType.ReadOnly = true;
            // 
            // colValidation
            // 
            this.colValidation.DataPropertyName = "strValidation";
            this.colValidation.HeaderText = "Validation";
            this.colValidation.Items.AddRange(new object[] {
            "None",
            "Numeric",
            "Date"});
            this.colValidation.Name = "colValidation";
            this.colValidation.ReadOnly = true;
            // 
            // colEdit
            // 
            this.colEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.colEdit.DataPropertyName = "strFieldName";
            this.colEdit.HeaderText = "";
            this.colEdit.Name = "colEdit";
            this.colEdit.ReadOnly = true;
            this.colEdit.Text = "...";
            this.colEdit.UseColumnTextForButtonValue = true;
            this.colEdit.Width = 5;
            // 
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.colDelete.DataPropertyName = "strFieldName";
            this.colDelete.HeaderText = "";
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Text = "X";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 5;
            // 
            // btnULCustomGiftFields
            // 
            this.btnULCustomGiftFields.Location = new System.Drawing.Point(624, 50);
            this.btnULCustomGiftFields.Name = "btnULCustomGiftFields";
            this.btnULCustomGiftFields.Size = new System.Drawing.Size(91, 74);
            this.btnULCustomGiftFields.TabIndex = 296;
            this.btnULCustomGiftFields.Text = "^\r\n^\r\nUpload";
            this.btnULCustomGiftFields.UseVisualStyleBackColor = true;
            this.btnULCustomGiftFields.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(797, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 26);
            this.btnSave.TabIndex = 782;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(927, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 26);
            this.button1.TabIndex = 286;
            this.button1.Text = "Save Changes";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(480, 345);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 285;
            this.button2.Text = "<<";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(480, 316);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 284;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(576, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 20);
            this.label7.TabIndex = 283;
            this.label7.Text = "Web Definitions";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 20);
            this.label8.TabIndex = 282;
            this.label8.Text = "Local Definitions";
            // 
            // cboProgramSpecificDef
            // 
            this.cboProgramSpecificDef.FormattingEnabled = true;
            this.cboProgramSpecificDef.Location = new System.Drawing.Point(240, 6);
            this.cboProgramSpecificDef.Name = "cboProgramSpecificDef";
            this.cboProgramSpecificDef.Size = new System.Drawing.Size(216, 21);
            this.cboProgramSpecificDef.TabIndex = 783;
            this.cboProgramSpecificDef.SelectedIndexChanged += new System.EventHandler(this.cboProgramSpecificDef_SelectedIndexChanged);
            this.cboProgramSpecificDef.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cboProgramSpecificDef_MouseDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 13);
            this.label9.TabIndex = 784;
            this.label9.Text = "Program Specific Flag and Field Definition:";
            // 
            // chkExcludeFromGeneral
            // 
            this.chkExcludeFromGeneral.AutoSize = true;
            this.chkExcludeFromGeneral.Location = new System.Drawing.Point(462, 8);
            this.chkExcludeFromGeneral.Name = "chkExcludeFromGeneral";
            this.chkExcludeFromGeneral.Size = new System.Drawing.Size(212, 17);
            this.chkExcludeFromGeneral.TabIndex = 785;
            this.chkExcludeFromGeneral.Text = "Only include in program-specific options";
            this.chkExcludeFromGeneral.UseVisualStyleBackColor = true;
            this.chkExcludeFromGeneral.Visible = false;
            // 
            // frmSyncCustomFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 787);
            this.Controls.Add(this.chkExcludeFromGeneral);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboProgramSpecificDef);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabCustomFlagsFields);
            this.Name = "frmSyncCustomFlags";
            this.Text = "Synchronize Custom Flag/Field Descriptions";
            this.Load += new System.EventHandler(this.frmSyncCustomFlags_Load);
            this.tabCustomFlagsFields.ResumeLayout(false);
            this.pagIRCustom.ResumeLayout(false);
            this.pagIRCustom.PerformLayout();
            this.pagRegCustom.ResumeLayout(false);
            this.pagRegCustom.PerformLayout();
            this.pagGiftFlagsFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomGiftFieldsLocal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabCustomFlagsFields;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage pagGiftFlagsFields;
        private System.Windows.Forms.Button btnULCustomGiftFields;
        private System.Windows.Forms.Button btnAddCustomGiftField;
        private System.Windows.Forms.DataGridView grdCustomGiftFieldsLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSortOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colFieldType;
        private System.Windows.Forms.DataGridViewComboBoxColumn colValidation;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cboProgramSpecificDef;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage pagIRCustom;
        private System.Windows.Forms.Label lblRequiredHead;
        private System.Windows.Forms.Label lblOnlineLabelHead;
        private System.Windows.Forms.Label lblUseProfileHead;
        private System.Windows.Forms.Label lblUseCamperHead;
        private System.Windows.Forms.Label lblLocalLabelHead;
        private System.Windows.Forms.Label lblUseLocalHead;
        private System.Windows.Forms.Label lblFieldTypeHead;
        private System.Windows.Forms.Label lblSortOrderHead;
        private System.Windows.Forms.TabPage pagRegCustom;
        private System.Windows.Forms.Label lblSortOrderRegHead;
        private System.Windows.Forms.Label lblFieldTypeRegHead;
        private System.Windows.Forms.Label lblRequiredRegHead;
        private System.Windows.Forms.Label lblWebCaptionRegHead;
        private System.Windows.Forms.Label lblUseOnlineRegHead;
        private System.Windows.Forms.Label lblLocalCaptionRegHead;
        private System.Windows.Forms.Label lblUseLocalRegHead;
        private System.Windows.Forms.Label lblChargeRegHead;
        private System.Windows.Forms.Button btnAddFieldIR;
        private System.Windows.Forms.Button btnAddFieldReg;
        private System.Windows.Forms.CheckBox chkExcludeFromGeneral;

    }
}