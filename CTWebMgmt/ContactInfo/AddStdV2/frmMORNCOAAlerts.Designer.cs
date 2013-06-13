namespace CTWebMgmt.ContactInfo.AddStdV2
{
    partial class frmMORNCOAAlerts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdMORNCOAAlerts = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.colMORID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colListName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlertNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResolved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkShowResolved = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdMORNCOAAlerts)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMORNCOAAlerts
            // 
            this.grdMORNCOAAlerts.AllowUserToAddRows = false;
            this.grdMORNCOAAlerts.AllowUserToOrderColumns = true;
            this.grdMORNCOAAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMORNCOAAlerts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdMORNCOAAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMORNCOAAlerts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMORID,
            this.colListName,
            this.colAlertNotes,
            this.colResolved});
            this.grdMORNCOAAlerts.Location = new System.Drawing.Point(12, 41);
            this.grdMORNCOAAlerts.Name = "grdMORNCOAAlerts";
            this.grdMORNCOAAlerts.Size = new System.Drawing.Size(939, 408);
            this.grdMORNCOAAlerts.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(876, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colMORID
            // 
            this.colMORID.DataPropertyName = "lngMORID";
            this.colMORID.HeaderText = "MOR ID";
            this.colMORID.Name = "colMORID";
            this.colMORID.ReadOnly = true;
            this.colMORID.Width = 77;
            // 
            // colListName
            // 
            this.colListName.DataPropertyName = "strListName";
            this.colListName.HeaderText = "List Name";
            this.colListName.Name = "colListName";
            this.colListName.ReadOnly = true;
            this.colListName.Width = 118;
            // 
            // colAlertNotes
            // 
            this.colAlertNotes.DataPropertyName = "mmoAlertNotes";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colAlertNotes.DefaultCellStyle = dataGridViewCellStyle1;
            this.colAlertNotes.HeaderText = "Alert Notes";
            this.colAlertNotes.Name = "colAlertNotes";
            this.colAlertNotes.Width = 619;
            // 
            // colResolved
            // 
            this.colResolved.DataPropertyName = "blnResolved";
            this.colResolved.HeaderText = "Resolved?";
            this.colResolved.Name = "colResolved";
            this.colResolved.Width = 64;
            // 
            // chkShowResolved
            // 
            this.chkShowResolved.AutoSize = true;
            this.chkShowResolved.Location = new System.Drawing.Point(598, 14);
            this.chkShowResolved.Name = "chkShowResolved";
            this.chkShowResolved.Size = new System.Drawing.Size(150, 17);
            this.chkShowResolved.TabIndex = 3;
            this.chkShowResolved.Text = "Show Resolved Conflicts?";
            this.chkShowResolved.UseVisualStyleBackColor = true;
            this.chkShowResolved.CheckedChanged += new System.EventHandler(this.chkShowResolved_CheckedChanged);
            // 
            // frmMORNCOAAlerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 461);
            this.Controls.Add(this.chkShowResolved);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grdMORNCOAAlerts);
            this.Name = "frmMORNCOAAlerts";
            this.Text = "MOR/NCOA Conflicts";
            this.Load += new System.EventHandler(this.frmMORNCOAAlerts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdMORNCOAAlerts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdMORNCOAAlerts;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMORID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colListName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlertNotes;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colResolved;
        private System.Windows.Forms.CheckBox chkShowResolved;
    }
}