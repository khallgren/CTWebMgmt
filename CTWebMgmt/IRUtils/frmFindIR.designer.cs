namespace CTWebMgmt
{
    partial class frmFindIR
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.grdIRs = new System.Windows.Forms.DataGridView();
            this.colRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecordID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFullTextFilter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdIRs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(628, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.Location = new System.Drawing.Point(466, 12);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(80, 84);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(100, 20);
            this.txtCompany.TabIndex = 2;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(270, 84);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(100, 20);
            this.txtFName.TabIndex = 3;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(453, 84);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(100, 20);
            this.txtLName.TabIndex = 4;
            // 
            // grdIRs
            // 
            this.grdIRs.AllowUserToAddRows = false;
            this.grdIRs.AllowUserToDeleteRows = false;
            this.grdIRs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdIRs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdIRs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordID,
            this.colCompany,
            this.colFName,
            this.colLName,
            this.colCity,
            this.colState});
            this.grdIRs.Location = new System.Drawing.Point(12, 111);
            this.grdIRs.Name = "grdIRs";
            this.grdIRs.Size = new System.Drawing.Size(691, 255);
            this.grdIRs.TabIndex = 5;
            // 
            // colRecordID
            // 
            this.colRecordID.DataPropertyName = "lngRecordID";
            this.colRecordID.HeaderText = "Record ID";
            this.colRecordID.Name = "colRecordID";
            this.colRecordID.ReadOnly = true;
            // 
            // colCompany
            // 
            this.colCompany.DataPropertyName = "strCompany";
            this.colCompany.HeaderText = "Company";
            this.colCompany.Name = "colCompany";
            this.colCompany.ReadOnly = true;
            // 
            // colFName
            // 
            this.colFName.DataPropertyName = "strFName";
            this.colFName.HeaderText = "First Name";
            this.colFName.Name = "colFName";
            this.colFName.ReadOnly = true;
            // 
            // colLName
            // 
            this.colLName.DataPropertyName = "strLName";
            this.colLName.HeaderText = "Last Name";
            this.colLName.Name = "colLName";
            this.colLName.ReadOnly = true;
            // 
            // colCity
            // 
            this.colCity.DataPropertyName = "strCity";
            this.colCity.HeaderText = "City";
            this.colCity.Name = "colCity";
            this.colCity.ReadOnly = true;
            // 
            // colState
            // 
            this.colState.DataPropertyName = "strState";
            this.colState.HeaderText = "State";
            this.colState.Name = "colState";
            this.colState.ReadOnly = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(558, 82);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "First Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Company:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Last Name:";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(547, 12);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 23);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.Text = "Add &New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Record ID:";
            // 
            // txtRecordID
            // 
            this.txtRecordID.AcceptsReturn = true;
            this.txtRecordID.Location = new System.Drawing.Point(80, 58);
            this.txtRecordID.Name = "txtRecordID";
            this.txtRecordID.Size = new System.Drawing.Size(100, 20);
            this.txtRecordID.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Full Text:";
            // 
            // txtFullTextFilter
            // 
            this.txtFullTextFilter.AcceptsReturn = true;
            this.txtFullTextFilter.Location = new System.Drawing.Point(80, 32);
            this.txtFullTextFilter.Name = "txtFullTextFilter";
            this.txtFullTextFilter.Size = new System.Drawing.Size(100, 20);
            this.txtFullTextFilter.TabIndex = 13;
            this.txtFullTextFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFullTextFilter_KeyPress);
            // 
            // frmFindIR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 378);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFullTextFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRecordID);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grdIRs);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmFindIR";
            this.Text = "Find Record";
            this.Load += new System.EventHandler(this.frmFindIR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdIRs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.DataGridView grdIRs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRecordID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFullTextFilter;
    }
}