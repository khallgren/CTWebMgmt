namespace CTWebMgmt.Ind.Setup
{
    partial class frmMinDep
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
            this.grdBlocks = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtMinDep = new System.Windows.Forms.TextBox();
            this.fraMethod = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radPercent = new System.Windows.Forms.RadioButton();
            this.radDollars = new System.Windows.Forms.RadioButton();
            this.btnApplyMethod = new System.Windows.Forms.Button();
            this.colBlockID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBlockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCharge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinDep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdBlocks)).BeginInit();
            this.fraMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdBlocks
            // 
            this.grdBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBlocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBlocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBlockID,
            this.colBlockCode,
            this.colCharge,
            this.colMinDep});
            this.grdBlocks.Location = new System.Drawing.Point(12, 48);
            this.grdBlocks.Name = "grdBlocks";
            this.grdBlocks.Size = new System.Drawing.Size(726, 435);
            this.grdBlocks.TabIndex = 0;
            this.grdBlocks.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdBlocks_UserAddedRow);
            this.grdBlocks.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdBlocks_UserDeletedRow);
            this.grdBlocks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBlocks_CellEndEdit);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(663, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(501, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Location = new System.Drawing.Point(582, 12);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 3;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtMinDep
            // 
            this.txtMinDep.Location = new System.Drawing.Point(158, 14);
            this.txtMinDep.Name = "txtMinDep";
            this.txtMinDep.Size = new System.Drawing.Size(100, 20);
            this.txtMinDep.TabIndex = 4;
            // 
            // fraMethod
            // 
            this.fraMethod.Controls.Add(this.radDollars);
            this.fraMethod.Controls.Add(this.radPercent);
            this.fraMethod.Location = new System.Drawing.Point(273, 0);
            this.fraMethod.Name = "fraMethod";
            this.fraMethod.Size = new System.Drawing.Size(86, 42);
            this.fraMethod.TabIndex = 5;
            this.fraMethod.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Calculate Minimum Deposit:";
            // 
            // radPercent
            // 
            this.radPercent.AutoSize = true;
            this.radPercent.Location = new System.Drawing.Point(9, 7);
            this.radPercent.Name = "radPercent";
            this.radPercent.Size = new System.Drawing.Size(62, 17);
            this.radPercent.TabIndex = 0;
            this.radPercent.TabStop = true;
            this.radPercent.Text = "Percent";
            this.radPercent.UseVisualStyleBackColor = true;
            // 
            // radDollars
            // 
            this.radDollars.AutoSize = true;
            this.radDollars.Location = new System.Drawing.Point(9, 22);
            this.radDollars.Name = "radDollars";
            this.radDollars.Size = new System.Drawing.Size(57, 17);
            this.radDollars.TabIndex = 1;
            this.radDollars.TabStop = true;
            this.radDollars.Text = "Dollars";
            this.radDollars.UseVisualStyleBackColor = true;
            // 
            // btnApplyMethod
            // 
            this.btnApplyMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyMethod.Location = new System.Drawing.Point(365, 12);
            this.btnApplyMethod.Name = "btnApplyMethod";
            this.btnApplyMethod.Size = new System.Drawing.Size(75, 23);
            this.btnApplyMethod.TabIndex = 7;
            this.btnApplyMethod.Text = "Apply";
            this.btnApplyMethod.UseVisualStyleBackColor = true;
            this.btnApplyMethod.Click += new System.EventHandler(this.btnApplyMethod_Click);
            // 
            // colBlockID
            // 
            this.colBlockID.DataPropertyName = "lngBlockID";
            this.colBlockID.HeaderText = "Block ID";
            this.colBlockID.Name = "colBlockID";
            this.colBlockID.ReadOnly = true;
            // 
            // colBlockCode
            // 
            this.colBlockCode.DataPropertyName = "strBlockCode";
            this.colBlockCode.HeaderText = "Block Code";
            this.colBlockCode.Name = "colBlockCode";
            // 
            // colCharge
            // 
            this.colCharge.DataPropertyName = "curCharge";
            this.colCharge.HeaderText = "Charge";
            this.colCharge.Name = "colCharge";
            // 
            // colMinDep
            // 
            this.colMinDep.DataPropertyName = "curMinDep";
            this.colMinDep.HeaderText = "Minimum Deposit";
            this.colMinDep.Name = "colMinDep";
            // 
            // frmMinDep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 495);
            this.Controls.Add(this.btnApplyMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fraMethod);
            this.Controls.Add(this.txtMinDep);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grdBlocks);
            this.Name = "frmMinDep";
            this.Text = "Block Definitions";
            this.Load += new System.EventHandler(this.frmMinDep_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMinDep_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.grdBlocks)).EndInit();
            this.fraMethod.ResumeLayout(false);
            this.fraMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdBlocks;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtMinDep;
        private System.Windows.Forms.GroupBox fraMethod;
        private System.Windows.Forms.RadioButton radDollars;
        private System.Windows.Forms.RadioButton radPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApplyMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBlockID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBlockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCharge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinDep;
    }
}