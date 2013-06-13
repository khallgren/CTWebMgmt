namespace CTWebMgmt.Donor.Reports
{
    partial class frmDXDetailsViewer
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
            this.rvwDXDetails = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.conCTMain_B = new System.Data.OleDb.OleDbConnection();
            this.cmdDXDetails = new System.Data.OleDb.OleDbCommand();
            this.daDXDetails = new System.Data.OleDb.OleDbDataAdapter();
            this.dsDXDetails = new System.Data.DataSet();
            this.cmdDXDetails_CustomFields = new System.Data.OleDb.OleDbCommand();
            this.daDXDetails_CustomFields = new System.Data.OleDb.OleDbDataAdapter();
            this.dsDXDetails_CustomFields = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dsDXDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDXDetails_CustomFields)).BeginInit();
            this.SuspendLayout();
            // 
            // rvwDXDetails
            // 
            this.rvwDXDetails.ActiveViewIndex = -1;
            this.rvwDXDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rvwDXDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwDXDetails.Location = new System.Drawing.Point(0, 0);
            this.rvwDXDetails.Name = "rvwDXDetails";
            this.rvwDXDetails.SelectionFormula = "";
            this.rvwDXDetails.Size = new System.Drawing.Size(433, 433);
            this.rvwDXDetails.TabIndex = 0;
            this.rvwDXDetails.ViewTimeSelectionFormula = "";
            // 
            // cmdDXDetails
            // 
            this.cmdDXDetails.Connection = this.conCTMain_B;
            // 
            // daDXDetails
            // 
            this.daDXDetails.SelectCommand = this.cmdDXDetails;
            // 
            // dsDXDetails
            // 
            this.dsDXDetails.DataSetName = "dsDXDetails";
            // 
            // cmdDXDetails_CustomFields
            // 
            this.cmdDXDetails_CustomFields.Connection = this.conCTMain_B;
            // 
            // daDXDetails_CustomFields
            // 
            this.daDXDetails_CustomFields.SelectCommand = this.cmdDXDetails_CustomFields;
            // 
            // dsDXDetails_CustomFields
            // 
            this.dsDXDetails_CustomFields.DataSetName = "dsDXDetails_CustomFields";
            // 
            // frmDXDetailsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 433);
            this.Controls.Add(this.rvwDXDetails);
            this.Name = "frmDXDetailsViewer";
            this.Text = "Donor Express Detail Report";
            this.Load += new System.EventHandler(this.frmDXDetailsViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsDXDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDXDetails_CustomFields)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rvwDXDetails;
        private System.Data.OleDb.OleDbConnection conCTMain_B;
        private System.Data.OleDb.OleDbCommand cmdDXDetails;
        private System.Data.OleDb.OleDbDataAdapter daDXDetails;
        private System.Data.DataSet dsDXDetails;
        private System.Data.OleDb.OleDbCommand cmdDXDetails_CustomFields;
        private System.Data.OleDb.OleDbDataAdapter daDXDetails_CustomFields;
        private System.Data.DataSet dsDXDetails_CustomFields;
    }
}