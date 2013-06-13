namespace CTWebMgmt.Admin.CustomGiftFields
{
    partial class frmCustomGiftLookupOptions
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.grdLookupOptions = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.colSortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLookupOption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdLookupOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(97, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(102, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Commit C&hanges";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // grdLookupOptions
            // 
            this.grdLookupOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLookupOptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSortOrder,
            this.colLookupOption,
            this.colFieldName});
            this.grdLookupOptions.Location = new System.Drawing.Point(12, 41);
            this.grdLookupOptions.Name = "grdLookupOptions";
            this.grdLookupOptions.Size = new System.Drawing.Size(268, 213);
            this.grdLookupOptions.TabIndex = 4;
            this.grdLookupOptions.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdLookupOptions_UserAddedRow);
            this.grdLookupOptions.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdLookupOptions_UserDeletedRow);
            this.grdLookupOptions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdLookupOptions_CellEndEdit);
            this.grdLookupOptions.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdLookupOptions_DefaultValuesNeeded);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(205, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // colSortOrder
            // 
            this.colSortOrder.DataPropertyName = "intSortOrder";
            this.colSortOrder.HeaderText = "Sort Order";
            this.colSortOrder.Name = "colSortOrder";
            // 
            // colLookupOption
            // 
            this.colLookupOption.DataPropertyName = "strLookupOption";
            this.colLookupOption.HeaderText = "Lookup Value";
            this.colLookupOption.Name = "colLookupOption";
            // 
            // colFieldName
            // 
            this.colFieldName.DataPropertyName = "strFieldName";
            this.colFieldName.HeaderText = "Field Name";
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.Visible = false;
            // 
            // frmCustomGiftLookupOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grdLookupOptions);
            this.Controls.Add(this.btnClose);
            this.Name = "frmCustomGiftLookupOptions";
            this.Text = "Lookup Options";
            this.Load += new System.EventHandler(this.frmCustomGiftLookupOptions_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCustomGiftLookupOptions_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.grdLookupOptions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView grdLookupOptions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSortOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLookupOption;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
    }
}