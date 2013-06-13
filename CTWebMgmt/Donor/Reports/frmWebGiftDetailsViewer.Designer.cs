namespace CTWebMgmt.Donor.Reports
{
    partial class frmWebGiftDetailsViewer
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
            this.rvwWebGiftDetails = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.conCTMain_B = new System.Data.OleDb.OleDbConnection();
            this.cmdWebGiftDetails = new System.Data.OleDb.OleDbCommand();
            this.daWebGiftDetails = new System.Data.OleDb.OleDbDataAdapter();
            this.dsWebGiftDetails = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebGiftDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // rvwWebGiftDetails
            // 
            this.rvwWebGiftDetails.ActiveViewIndex = -1;
            this.rvwWebGiftDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rvwWebGiftDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwWebGiftDetails.Location = new System.Drawing.Point(0, 0);
            this.rvwWebGiftDetails.Name = "rvwWebGiftDetails";
            this.rvwWebGiftDetails.SelectionFormula = "";
            this.rvwWebGiftDetails.Size = new System.Drawing.Size(536, 444);
            this.rvwWebGiftDetails.TabIndex = 0;
            this.rvwWebGiftDetails.ViewTimeSelectionFormula = "";
            // 
            // cmdWebGiftDetails
            // 
            this.cmdWebGiftDetails.Connection = this.conCTMain_B;
            // 
            // daWebGiftDetails
            // 
            this.daWebGiftDetails.SelectCommand = this.cmdWebGiftDetails;
            // 
            // dsWebGiftDetails
            // 
            this.dsWebGiftDetails.DataSetName = "dsWebGiftDetails";
            // 
            // frmWebGiftDetailsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 444);
            this.Controls.Add(this.rvwWebGiftDetails);
            this.Name = "frmWebGiftDetailsViewer";
            this.Text = "Web Gift Details Viewer";
            this.Load += new System.EventHandler(this.frmWebGiftDetailsViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsWebGiftDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rvwWebGiftDetails;
        private System.Data.OleDb.OleDbConnection conCTMain_B;
        private System.Data.OleDb.OleDbCommand cmdWebGiftDetails;
        private System.Data.OleDb.OleDbDataAdapter daWebGiftDetails;
        private System.Data.DataSet dsWebGiftDetails;
    }
}