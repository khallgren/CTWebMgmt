namespace CTWebMgmt.ContactInfo.AddStdV2
{
    partial class frmAddStdV2Results
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddStdV2Results));
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCertRes = new System.Windows.Forms.DataGridView();
            this.colResults = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdCertRes)).BeginInit();
            this.SuspendLayout();
            // 
            // colName
            // 
            this.colName.DataPropertyName = "strName";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colRecordID
            // 
            this.colRecordID.DataPropertyName = "lngRecordID";
            this.colRecordID.HeaderText = "Record ID";
            this.colRecordID.Name = "colRecordID";
            this.colRecordID.ReadOnly = true;
            // 
            // grdCertRes
            // 
            this.grdCertRes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCertRes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCertRes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordID,
            this.colName,
            this.colResults});
            this.grdCertRes.Location = new System.Drawing.Point(20, 94);
            this.grdCertRes.Name = "grdCertRes";
            this.grdCertRes.Size = new System.Drawing.Size(771, 464);
            this.grdCertRes.TabIndex = 8;
            // 
            // colResults
            // 
            this.colResults.DataPropertyName = "strStatus";
            this.colResults.HeaderText = "Result";
            this.colResults.Name = "colResults";
            this.colResults.ReadOnly = true;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(17, 14);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(566, 65);
            this.lblMsg.TabIndex = 7;
            this.lblMsg.Text = resources.GetString("lblMsg.Text");
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(716, 564);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Finish";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmAddStdV2Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 601);
            this.Controls.Add(this.grdCertRes);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnClose);
            this.Name = "frmAddStdV2Results";
            this.Text = "Address Standardization - Results";
            this.Load += new System.EventHandler(this.frmAddStdV2Results_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCertRes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordID;
        private System.Windows.Forms.DataGridView grdCertRes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResults;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnClose;
    }
}