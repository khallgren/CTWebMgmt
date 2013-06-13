namespace CTWebMgmt.GGCC
{
    partial class frmPickMatchGGCCWebRegRecord
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
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grdMatches = new System.Windows.Forms.DataGridView();
            this.lngRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strHomePhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdMatches)).BeginInit();
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(310, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(472, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(391, 12);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Text = "&Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select matching record and click \'Continue\':";
            // 
            // grdMatches
            // 
            this.grdMatches.AllowUserToAddRows = false;
            this.grdMatches.AllowUserToDeleteRows = false;
            this.grdMatches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lngRecordID,
            this.strName,
            this.strAddress,
            this.strCity,
            this.strState,
            this.strZip,
            this.strHomePhone});
            this.grdMatches.Location = new System.Drawing.Point(15, 41);
            this.grdMatches.Name = "grdMatches";
            this.grdMatches.ReadOnly = true;
            this.grdMatches.Size = new System.Drawing.Size(532, 250);
            this.grdMatches.TabIndex = 4;
            // 
            // lngRecordID
            // 
            this.lngRecordID.DataPropertyName = "lngRecordID";
            this.lngRecordID.HeaderText = "ID";
            this.lngRecordID.Name = "lngRecordID";
            this.lngRecordID.ReadOnly = true;
            this.lngRecordID.Visible = false;
            // 
            // strName
            // 
            this.strName.DataPropertyName = "strName";
            this.strName.HeaderText = "Name";
            this.strName.Name = "strName";
            this.strName.ReadOnly = true;
            // 
            // strAddress
            // 
            this.strAddress.DataPropertyName = "strAddress";
            this.strAddress.HeaderText = "Address";
            this.strAddress.Name = "strAddress";
            this.strAddress.ReadOnly = true;
            // 
            // strCity
            // 
            this.strCity.DataPropertyName = "strCity";
            this.strCity.HeaderText = "City";
            this.strCity.Name = "strCity";
            this.strCity.ReadOnly = true;
            // 
            // strState
            // 
            this.strState.DataPropertyName = "strState";
            this.strState.HeaderText = "State";
            this.strState.Name = "strState";
            this.strState.ReadOnly = true;
            // 
            // strZip
            // 
            this.strZip.DataPropertyName = "strZip";
            this.strZip.HeaderText = "Zip";
            this.strZip.Name = "strZip";
            this.strZip.ReadOnly = true;
            // 
            // strHomePhone
            // 
            this.strHomePhone.DataPropertyName = "strHomePhone";
            this.strHomePhone.HeaderText = "Home Phone";
            this.strHomePhone.Name = "strHomePhone";
            this.strHomePhone.ReadOnly = true;
            // 
            // frmPickMatchGGCCWebRegRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 303);
            this.Controls.Add(this.grdMatches);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Name = "frmPickMatchGGCCWebRegRecord";
            this.Text = "Select Matching Record";
            this.Load += new System.EventHandler(this.frmPickMatchGGCCWebRegRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMatches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdMatches;
        private System.Windows.Forms.DataGridViewTextBoxColumn lngRecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn strName;
        private System.Windows.Forms.DataGridViewTextBoxColumn strAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn strCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn strState;
        private System.Windows.Forms.DataGridViewTextBoxColumn strZip;
        private System.Windows.Forms.DataGridViewTextBoxColumn strHomePhone;
    }
}