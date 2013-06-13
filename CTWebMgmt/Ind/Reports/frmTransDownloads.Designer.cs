namespace CTWebMgmt.Ind.Reports
{
    partial class frmTransDownloads
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
            this.conCTMain_B = new System.Data.OleDb.OleDbConnection();
            this.cmdTransDownloads = new System.Data.OleDb.OleDbCommand();
            this.daTransDownloads = new System.Data.OleDb.OleDbDataAdapter();
            this.dsTransDownloads = new System.Data.DataSet();
            this.rvwTransDownloads = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.dsTransDownloads)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdTransDownloads
            // 
            this.cmdTransDownloads.Connection = this.conCTMain_B;
            // 
            // daTransDownloads
            // 
            this.daTransDownloads.SelectCommand = this.cmdTransDownloads;
            // 
            // dsTransDownloads
            // 
            this.dsTransDownloads.DataSetName = "dsTransDownloads";
            // 
            // rvwTransDownloads
            // 
            this.rvwTransDownloads.ActiveViewIndex = -1;
            this.rvwTransDownloads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rvwTransDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwTransDownloads.Location = new System.Drawing.Point(0, 0);
            this.rvwTransDownloads.Name = "rvwTransDownloads";
            this.rvwTransDownloads.SelectionFormula = "";
            this.rvwTransDownloads.Size = new System.Drawing.Size(605, 603);
            this.rvwTransDownloads.TabIndex = 0;
            this.rvwTransDownloads.ViewTimeSelectionFormula = "";
            // 
            // frmTransDownloads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 603);
            this.Controls.Add(this.rvwTransDownloads);
            this.Name = "frmTransDownloads";
            this.Text = "Transaction Downloads";
            this.Load += new System.EventHandler(this.frmTransDownloads_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsTransDownloads)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.OleDb.OleDbConnection conCTMain_B;
        private System.Data.OleDb.OleDbCommand cmdTransDownloads;
        private System.Data.OleDb.OleDbDataAdapter daTransDownloads;
        private System.Data.DataSet dsTransDownloads;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rvwTransDownloads;
    }
}