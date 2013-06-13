namespace CTWebMgmt.Admin.Reports
{
    partial class frmReconcileMerchantGateway
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
            this.rvwReconcileMerchantGateway = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.conCTMain_B = new System.Data.OleDb.OleDbConnection();
            this.daMerchantGatewayTrans = new System.Data.OleDb.OleDbDataAdapter();
            this.cmdMerchantGatewayTrans = new System.Data.OleDb.OleDbCommand();
            this.dsMerchantGatewayTrans = new System.Data.DataSet();
            this.mnuReconcileMerchGW = new System.Windows.Forms.MenuStrip();
            this.mnuHowToUse = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dsMerchantGatewayTrans)).BeginInit();
            this.mnuReconcileMerchGW.SuspendLayout();
            this.SuspendLayout();
            // 
            // rvwReconcileMerchantGateway
            // 
            this.rvwReconcileMerchantGateway.ActiveViewIndex = -1;
            this.rvwReconcileMerchantGateway.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rvwReconcileMerchantGateway.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvwReconcileMerchantGateway.Location = new System.Drawing.Point(0, 24);
            this.rvwReconcileMerchantGateway.Name = "rvwReconcileMerchantGateway";
            this.rvwReconcileMerchantGateway.SelectionFormula = "";
            this.rvwReconcileMerchantGateway.Size = new System.Drawing.Size(496, 477);
            this.rvwReconcileMerchantGateway.TabIndex = 0;
            this.rvwReconcileMerchantGateway.ViewTimeSelectionFormula = "";
            // 
            // daMerchantGatewayTrans
            // 
            this.daMerchantGatewayTrans.SelectCommand = this.cmdMerchantGatewayTrans;
            // 
            // cmdMerchantGatewayTrans
            // 
            this.cmdMerchantGatewayTrans.Connection = this.conCTMain_B;
            // 
            // dsMerchantGatewayTrans
            // 
            this.dsMerchantGatewayTrans.DataSetName = "dsMerchantGatewayTrans";
            // 
            // mnuReconcileMerchGW
            // 
            this.mnuReconcileMerchGW.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHowToUse});
            this.mnuReconcileMerchGW.Location = new System.Drawing.Point(0, 0);
            this.mnuReconcileMerchGW.Name = "mnuReconcileMerchGW";
            this.mnuReconcileMerchGW.Size = new System.Drawing.Size(496, 24);
            this.mnuReconcileMerchGW.TabIndex = 1;
            this.mnuReconcileMerchGW.Text = "How to use this report";
            // 
            // mnuHowToUse
            // 
            this.mnuHowToUse.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuHowToUse.Name = "mnuHowToUse";
            this.mnuHowToUse.Size = new System.Drawing.Size(136, 20);
            this.mnuHowToUse.Text = "How to use this report";
            this.mnuHowToUse.Click += new System.EventHandler(this.mnuHowToUse_Click);
            // 
            // frmReconcileMerchantGateway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 501);
            this.Controls.Add(this.rvwReconcileMerchantGateway);
            this.Controls.Add(this.mnuReconcileMerchGW);
            this.MainMenuStrip = this.mnuReconcileMerchGW;
            this.Name = "frmReconcileMerchantGateway";
            this.Text = "Reconcile Merchant Gateway Transactions";
            this.Load += new System.EventHandler(this.frmReconcileMerchantGateway_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsMerchantGatewayTrans)).EndInit();
            this.mnuReconcileMerchGW.ResumeLayout(false);
            this.mnuReconcileMerchGW.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rvwReconcileMerchantGateway;
        private System.Data.OleDb.OleDbConnection conCTMain_B;
        private System.Data.OleDb.OleDbDataAdapter daMerchantGatewayTrans;
        private System.Data.OleDb.OleDbCommand cmdMerchantGatewayTrans;
        private System.Data.DataSet dsMerchantGatewayTrans;
        private System.Windows.Forms.MenuStrip mnuReconcileMerchGW;
        private System.Windows.Forms.ToolStripMenuItem mnuHowToUse;
    }
}