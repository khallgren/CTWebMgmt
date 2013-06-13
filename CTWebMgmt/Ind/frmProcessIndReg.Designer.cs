namespace CTWebMgmt.Ind
{
    partial class frmProcessIndReg
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
            this.grdRegistrations = new System.Windows.Forms.DataGridView();
            this.colRegWebID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCamperName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBlockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radAllCampers = new System.Windows.Forms.RadioButton();
            this.radUnprocessedCampers = new System.Windows.Forms.RadioButton();
            this.btnClearProcessedReg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegistrations)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(557, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grdRegistrations
            // 
            this.grdRegistrations.AllowUserToAddRows = false;
            this.grdRegistrations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRegistrations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRegistrations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRegWebID,
            this.colCamperName,
            this.colBlockCode,
            this.colRegDate,
            this.colDetails});
            this.grdRegistrations.Location = new System.Drawing.Point(12, 41);
            this.grdRegistrations.Name = "grdRegistrations";
            this.grdRegistrations.Size = new System.Drawing.Size(620, 384);
            this.grdRegistrations.TabIndex = 2;
            this.grdRegistrations.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdRegistrations_CellClick);
            // 
            // colRegWebID
            // 
            this.colRegWebID.DataPropertyName = "lngRegistrationWebID";
            this.colRegWebID.HeaderText = "";
            this.colRegWebID.Name = "colRegWebID";
            this.colRegWebID.ReadOnly = true;
            this.colRegWebID.Visible = false;
            // 
            // colCamperName
            // 
            this.colCamperName.DataPropertyName = "strName";
            this.colCamperName.HeaderText = "Camper Name";
            this.colCamperName.Name = "colCamperName";
            this.colCamperName.ReadOnly = true;
            // 
            // colBlockCode
            // 
            this.colBlockCode.DataPropertyName = "strBlockCode";
            this.colBlockCode.HeaderText = "Block Code";
            this.colBlockCode.Name = "colBlockCode";
            this.colBlockCode.ReadOnly = true;
            // 
            // colRegDate
            // 
            this.colRegDate.DataPropertyName = "dteRegistrationDate";
            this.colRegDate.HeaderText = "Registration Date";
            this.colRegDate.Name = "colRegDate";
            this.colRegDate.ReadOnly = true;
            // 
            // colDetails
            // 
            this.colDetails.DataPropertyName = "lngRegistrationWebID";
            this.colDetails.HeaderText = "";
            this.colDetails.Name = "colDetails";
            this.colDetails.Text = "Details";
            this.colDetails.UseColumnTextForButtonValue = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radAllCampers);
            this.groupBox1.Controls.Add(this.radUnprocessedCampers);
            this.groupBox1.Location = new System.Drawing.Point(262, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 37);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // radAllCampers
            // 
            this.radAllCampers.AutoSize = true;
            this.radAllCampers.Location = new System.Drawing.Point(181, 14);
            this.radAllCampers.Name = "radAllCampers";
            this.radAllCampers.Size = new System.Drawing.Size(100, 17);
            this.radAllCampers.TabIndex = 1;
            this.radAllCampers.TabStop = true;
            this.radAllCampers.Text = "All Registrations";
            this.radAllCampers.UseVisualStyleBackColor = true;
            this.radAllCampers.CheckedChanged += new System.EventHandler(this.radUnprocessedCampers_CheckedChanged);
            // 
            // radUnprocessedCampers
            // 
            this.radUnprocessedCampers.AutoSize = true;
            this.radUnprocessedCampers.Location = new System.Drawing.Point(6, 14);
            this.radUnprocessedCampers.Name = "radUnprocessedCampers";
            this.radUnprocessedCampers.Size = new System.Drawing.Size(156, 17);
            this.radUnprocessedCampers.TabIndex = 0;
            this.radUnprocessedCampers.TabStop = true;
            this.radUnprocessedCampers.Text = "Un-Processed Registrations";
            this.radUnprocessedCampers.UseVisualStyleBackColor = true;
            this.radUnprocessedCampers.CheckedChanged += new System.EventHandler(this.radUnprocessedCampers_CheckedChanged);
            // 
            // btnClearProcessedReg
            // 
            this.btnClearProcessedReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearProcessedReg.Location = new System.Drawing.Point(67, 12);
            this.btnClearProcessedReg.Name = "btnClearProcessedReg";
            this.btnClearProcessedReg.Size = new System.Drawing.Size(189, 23);
            this.btnClearProcessedReg.TabIndex = 4;
            this.btnClearProcessedReg.Text = "Clear Processed Registrations";
            this.btnClearProcessedReg.UseVisualStyleBackColor = true;
            this.btnClearProcessedReg.Visible = false;
            this.btnClearProcessedReg.Click += new System.EventHandler(this.btnClearProcessedReg_Click);
            // 
            // frmProcessIndReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 437);
            this.Controls.Add(this.btnClearProcessedReg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdRegistrations);
            this.Controls.Add(this.btnClose);
            this.Name = "frmProcessIndReg";
            this.Text = "Process Registrations";
            this.Load += new System.EventHandler(this.frmProcessIndReg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdRegistrations)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView grdRegistrations;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegWebID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCamperName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBlockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegDate;
        private System.Windows.Forms.DataGridViewButtonColumn colDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radAllCampers;
        private System.Windows.Forms.RadioButton radUnprocessedCampers;
        private System.Windows.Forms.Button btnClearProcessedReg;
    }
}