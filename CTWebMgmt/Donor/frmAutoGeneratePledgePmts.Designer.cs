namespace CTWebMgmt.Donor
{
    partial class frmAutoGeneratePledgePmts
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
            this.btnGeneratePmts = new System.Windows.Forms.Button();
            this.grdPmtPreview = new System.Windows.Forms.DataGridView();
            this.colPledgeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNextPmtDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScheduledAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditBilling = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdPmtPreview)).BeginInit();
            
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(685, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGeneratePmts
            // 
            this.btnGeneratePmts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePmts.Location = new System.Drawing.Point(556, 12);
            this.btnGeneratePmts.Name = "btnGeneratePmts";
            this.btnGeneratePmts.Size = new System.Drawing.Size(123, 23);
            this.btnGeneratePmts.TabIndex = 1;
            this.btnGeneratePmts.Text = "&Generate Payments";
            this.btnGeneratePmts.UseVisualStyleBackColor = true;
            this.btnGeneratePmts.Click += new System.EventHandler(this.btnGeneratePmts_Click);
            // 
            // grdPmtPreview
            // 
            this.grdPmtPreview.AllowUserToAddRows = false;
            this.grdPmtPreview.AllowUserToDeleteRows = false;
            this.grdPmtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPmtPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPmtPreview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPledgeID,
            this.colRecordID,
            this.colName,
            this.colCompanyName,
            this.colNextPmtDate,
            this.colScheduledAmt,
            this.colEditBilling});
            this.grdPmtPreview.Location = new System.Drawing.Point(12, 65);
            this.grdPmtPreview.Name = "grdPmtPreview";
            this.grdPmtPreview.Size = new System.Drawing.Size(748, 271);
            this.grdPmtPreview.TabIndex = 2;
            this.grdPmtPreview.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPmtPreview_CellClick);
            // 
            // colPledgeID
            // 
            this.colPledgeID.DataPropertyName = "lngPledgeID";
            this.colPledgeID.HeaderText = "Pledge ID";
            this.colPledgeID.Name = "colPledgeID";
            this.colPledgeID.ReadOnly = true;
            // 
            // colRecordID
            // 
            this.colRecordID.DataPropertyName = "lngRecordID";
            this.colRecordID.HeaderText = "Record ID";
            this.colRecordID.Name = "colRecordID";
            this.colRecordID.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "strName";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCompanyName
            // 
            this.colCompanyName.DataPropertyName = "strCompanyName";
            this.colCompanyName.HeaderText = "Company";
            this.colCompanyName.Name = "colCompanyName";
            this.colCompanyName.ReadOnly = true;
            // 
            // colNextPmtDate
            // 
            this.colNextPmtDate.DataPropertyName = "dteNextPmtDate";
            this.colNextPmtDate.HeaderText = "Next Payment Date";
            this.colNextPmtDate.Name = "colNextPmtDate";
            this.colNextPmtDate.ReadOnly = true;
            // 
            // colScheduledAmt
            // 
            this.colScheduledAmt.DataPropertyName = "curScheduledAmt";
            this.colScheduledAmt.HeaderText = "Scheduled Amount";
            this.colScheduledAmt.Name = "colScheduledAmt";
            this.colScheduledAmt.ReadOnly = true;
            // 
            // colEditBilling
            // 
            this.colEditBilling.HeaderText = "Edit Billing Info";
            this.colEditBilling.Name = "colEditBilling";
            this.colEditBilling.Text = "Edit Billing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Preview of auto payments:";
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 342);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(748, 212);
            this.lstStatus.TabIndex = 4;
            // 
            // frmAutoGeneratePledgePmts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 562);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grdPmtPreview);
            this.Controls.Add(this.btnGeneratePmts);
            this.Controls.Add(this.btnClose);
            this.Name = "frmAutoGeneratePledgePmts";
            // 
            // 
            // 
            
            this.Text = "Auto-Generate Pledge Payments";
            this.Load += new System.EventHandler(this.frmAutoGeneratePledgePmts_Load);
            this.Activated += new System.EventHandler(this.frmAutoGeneratePledgePmts_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.grdPmtPreview)).EndInit();
            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGeneratePmts;
        private System.Windows.Forms.DataGridView grdPmtPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPledgeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextPmtDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScheduledAmt;
        private System.Windows.Forms.DataGridViewButtonColumn colEditBilling;
        private System.Windows.Forms.ListBox lstStatus;
    }
}