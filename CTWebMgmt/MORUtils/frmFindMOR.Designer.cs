namespace CTWebMgmt
{
    partial class frmFindMOR
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
            this.grdMOR = new System.Windows.Forms.DataGridView();
            this.colMORID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMORName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCityState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJumpTo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboMORType = new System.Windows.Forms.ComboBox();
            this.txtMORName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddNewMOR = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdMOR)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMOR
            // 
            this.grdMOR.AllowUserToAddRows = false;
            this.grdMOR.AllowUserToDeleteRows = false;
            this.grdMOR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMOR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMOR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMORID,
            this.colMORName,
            this.colCityState,
            this.colJumpTo});
            this.grdMOR.Location = new System.Drawing.Point(12, 68);
            this.grdMOR.Name = "grdMOR";
            this.grdMOR.Size = new System.Drawing.Size(585, 295);
            this.grdMOR.TabIndex = 0;
            this.grdMOR.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMOR_CellClick);
            // 
            // colMORID
            // 
            this.colMORID.DataPropertyName = "lngMORID";
            this.colMORID.HeaderText = "MOR ID";
            this.colMORID.Name = "colMORID";
            this.colMORID.ReadOnly = true;
            // 
            // colMORName
            // 
            this.colMORName.DataPropertyName = "strMORName";
            this.colMORName.HeaderText = "MOR Name";
            this.colMORName.Name = "colMORName";
            this.colMORName.ReadOnly = true;
            // 
            // colCityState
            // 
            this.colCityState.DataPropertyName = "strCityState";
            this.colCityState.HeaderText = "City, State";
            this.colCityState.Name = "colCityState";
            this.colCityState.ReadOnly = true;
            // 
            // colJumpTo
            // 
            this.colJumpTo.DataPropertyName = "lngMORID";
            this.colJumpTo.HeaderText = "Select MOR";
            this.colJumpTo.Name = "colJumpTo";
            this.colJumpTo.ReadOnly = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(522, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cboMORType
            // 
            this.cboMORType.FormattingEnabled = true;
            this.cboMORType.Location = new System.Drawing.Point(340, 41);
            this.cboMORType.Name = "cboMORType";
            this.cboMORType.Size = new System.Drawing.Size(121, 21);
            this.cboMORType.TabIndex = 2;
            // 
            // txtMORName
            // 
            this.txtMORName.Location = new System.Drawing.Point(115, 41);
            this.txtMORName.Name = "txtMORName";
            this.txtMORName.Size = new System.Drawing.Size(136, 20);
            this.txtMORName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "MOR Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "MOR Type:";
            // 
            // btnAddNewMOR
            // 
            this.btnAddNewMOR.Location = new System.Drawing.Point(413, 12);
            this.btnAddNewMOR.Name = "btnAddNewMOR";
            this.btnAddNewMOR.Size = new System.Drawing.Size(103, 23);
            this.btnAddNewMOR.TabIndex = 6;
            this.btnAddNewMOR.Text = "Add New MOR";
            this.btnAddNewMOR.UseVisualStyleBackColor = true;
            this.btnAddNewMOR.Click += new System.EventHandler(this.btnAddNewMOR_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(522, 39);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmFindMOR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(609, 375);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnAddNewMOR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMORName);
            this.Controls.Add(this.cboMORType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdMOR);
            this.Name = "frmFindMOR";
            this.Text = "Find MOR";
            this.Load += new System.EventHandler(this.frmFindMOR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMOR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdMOR;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboMORType;
        private System.Windows.Forms.TextBox txtMORName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddNewMOR;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMORID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMORName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCityState;
        private System.Windows.Forms.DataGridViewButtonColumn colJumpTo;
    }
}