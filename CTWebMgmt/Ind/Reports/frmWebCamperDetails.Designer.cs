namespace CTWebMgmt.Ind.Reports
{
    partial class frmWebCamperDetails
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
            this.rvwWebCamperDetails = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.conCTMain_B = new System.Data.OleDb.OleDbConnection();
            this.cmdWebCamperDetails = new System.Data.OleDb.OleDbCommand();
            this.daWebCamperDetails = new System.Data.OleDb.OleDbDataAdapter();
            this.dsWebCamperDetails = new System.Data.DataSet();
            this.cmdWebCamperDetails_BlockChoices = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.daWebCamperDetails_BlockChoices = new System.Data.OleDb.OleDbDataAdapter();
            this.dsWebCamperDetails_BlockChoices = new System.Data.DataSet();
            this.cmdWebCamperDetails_CustomFieldsCamper = new System.Data.OleDb.OleDbCommand();
            this.daWebCamperDetails_CustomFieldsCamper = new System.Data.OleDb.OleDbDataAdapter();
            this.cmdWebCamperDetails_CustomFieldsProfile = new System.Data.OleDb.OleDbCommand();
            this.daWebCamperDetails_CustomFieldsProfile = new System.Data.OleDb.OleDbDataAdapter();
            this.cmdWebCamperDetails_CustomFieldsReg = new System.Data.OleDb.OleDbCommand();
            this.daWebCamperDetails_CustomFieldsReg = new System.Data.OleDb.OleDbDataAdapter();
            this.dsWebCamperDetails_CustomFieldsCamper = new System.Data.DataSet();
            this.dsWebCamperDetails_CustomFieldsProfile = new System.Data.DataSet();
            this.dsWebCamperDetails_CustomFieldsReg = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_BlockChoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_CustomFieldsCamper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_CustomFieldsProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_CustomFieldsReg)).BeginInit();
            this.SuspendLayout();
            // 
            // rvwWebCamperDetails
            // 
            this.rvwWebCamperDetails.ActiveViewIndex = -1;
            this.rvwWebCamperDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rvwWebCamperDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwWebCamperDetails.Location = new System.Drawing.Point(0, 0);
            this.rvwWebCamperDetails.Name = "rvwWebCamperDetails";
            this.rvwWebCamperDetails.SelectionFormula = "";
            this.rvwWebCamperDetails.Size = new System.Drawing.Size(531, 557);
            this.rvwWebCamperDetails.TabIndex = 0;
            this.rvwWebCamperDetails.ViewTimeSelectionFormula = "";
            // 
            // cmdWebCamperDetails
            // 
            this.cmdWebCamperDetails.Connection = this.conCTMain_B;
            // 
            // daWebCamperDetails
            // 
            this.daWebCamperDetails.SelectCommand = this.cmdWebCamperDetails;
            // 
            // dsWebCamperDetails
            // 
            this.dsWebCamperDetails.DataSetName = "dsWebCamperDetails";
            // 
            // cmdWebCamperDetails_BlockChoices
            // 
            this.cmdWebCamperDetails_BlockChoices.Connection = this.conCTMain_B;
            // 
            // daWebCamperDetails_BlockChoices
            // 
            this.daWebCamperDetails_BlockChoices.SelectCommand = this.cmdWebCamperDetails_BlockChoices;
            this.daWebCamperDetails_BlockChoices.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // dsWebCamperDetails_BlockChoices
            // 
            this.dsWebCamperDetails_BlockChoices.DataSetName = "dsWebCamperDetails_BlockChoices";
            // 
            // cmdWebCamperDetails_CustomFieldsCamper
            // 
            this.cmdWebCamperDetails_CustomFieldsCamper.Connection = this.conCTMain_B;
            // 
            // daWebCamperDetails_CustomFieldsCamper
            // 
            this.daWebCamperDetails_CustomFieldsCamper.SelectCommand = this.cmdWebCamperDetails_CustomFieldsCamper;
            // 
            // cmdWebCamperDetails_CustomFieldsProfile
            // 
            this.cmdWebCamperDetails_CustomFieldsProfile.Connection = this.conCTMain_B;
            // 
            // daWebCamperDetails_CustomFieldsProfile
            // 
            this.daWebCamperDetails_CustomFieldsProfile.ContinueUpdateOnError = true;
            this.daWebCamperDetails_CustomFieldsProfile.SelectCommand = this.cmdWebCamperDetails_CustomFieldsProfile;
            // 
            // cmdWebCamperDetails_CustomFieldsReg
            // 
            this.cmdWebCamperDetails_CustomFieldsReg.Connection = this.conCTMain_B;
            // 
            // daWebCamperDetails_CustomFieldsReg
            // 
            this.daWebCamperDetails_CustomFieldsReg.SelectCommand = this.cmdWebCamperDetails_CustomFieldsReg;
            // 
            // dsWebCamperDetails_CustomFieldsCamper
            // 
            this.dsWebCamperDetails_CustomFieldsCamper.DataSetName = "dsWebCamperDetails_CustomFieldsCamper";
            // 
            // dsWebCamperDetails_CustomFieldsProfile
            // 
            this.dsWebCamperDetails_CustomFieldsProfile.DataSetName = "dsWebCamperDetails_CustomFieldsProfile";
            // 
            // dsWebCamperDetails_CustomFieldsReg
            // 
            this.dsWebCamperDetails_CustomFieldsReg.DataSetName = "dsWebCamperDetails_CustomFieldsReg";
            // 
            // frmWebCamperDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 557);
            this.Controls.Add(this.rvwWebCamperDetails);
            this.Name = "frmWebCamperDetails";
            this.Text = "Web Camper Details";
            this.Load += new System.EventHandler(this.frmWebCamperDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_BlockChoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_CustomFieldsCamper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_CustomFieldsProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsWebCamperDetails_CustomFieldsReg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rvwWebCamperDetails;
        private System.Data.OleDb.OleDbConnection conCTMain_B;
        private System.Data.OleDb.OleDbCommand cmdWebCamperDetails;
        private System.Data.OleDb.OleDbDataAdapter daWebCamperDetails;
        private System.Data.DataSet dsWebCamperDetails;
        private System.Data.OleDb.OleDbCommand cmdWebCamperDetails_BlockChoices;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        private System.Data.OleDb.OleDbDataAdapter daWebCamperDetails_BlockChoices;
        private System.Data.DataSet dsWebCamperDetails_BlockChoices;
        private System.Data.OleDb.OleDbCommand cmdWebCamperDetails_CustomFieldsCamper;
        private System.Data.OleDb.OleDbDataAdapter daWebCamperDetails_CustomFieldsCamper;
        private System.Data.OleDb.OleDbCommand cmdWebCamperDetails_CustomFieldsProfile;
        private System.Data.OleDb.OleDbDataAdapter daWebCamperDetails_CustomFieldsProfile;
        private System.Data.OleDb.OleDbCommand cmdWebCamperDetails_CustomFieldsReg;
        private System.Data.OleDb.OleDbDataAdapter daWebCamperDetails_CustomFieldsReg;
        private System.Data.DataSet dsWebCamperDetails_CustomFieldsCamper;
        private System.Data.DataSet dsWebCamperDetails_CustomFieldsProfile;
        private System.Data.DataSet dsWebCamperDetails_CustomFieldsReg;
    }
}