namespace CTWebMgmt
{
    partial class frmReferredBy
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
            this.grdReferredBy = new System.Windows.Forms.DataGridView();
            this.strReferredBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdReferredBy)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(205, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grdReferredBy
            // 
            this.grdReferredBy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReferredBy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.strReferredBy});
            this.grdReferredBy.Location = new System.Drawing.Point(12, 41);
            this.grdReferredBy.Name = "grdReferredBy";
            this.grdReferredBy.Size = new System.Drawing.Size(268, 213);
            this.grdReferredBy.TabIndex = 1;
            this.grdReferredBy.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdReferredBy_UserAddedRow);
            this.grdReferredBy.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdReferredBy_UserDeletedRow);
            this.grdReferredBy.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReferredBy_CellEndEdit);
            // 
            // strReferredBy
            // 
            this.strReferredBy.DataPropertyName = "strReferredBy";
            this.strReferredBy.HeaderText = "Referred By";
            this.strReferredBy.Name = "strReferredBy";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(97, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(102, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Commit C&hanges";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(16, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmReferredBy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grdReferredBy);
            this.Controls.Add(this.btnClose);
            this.Name = "frmReferredBy";
            this.Text = "Referred By";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmReferredBy_FormClosed);
            this.Load += new System.EventHandler(this.frmReferredBy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdReferredBy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView grdReferredBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn strReferredBy;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnUpload;
    }
}