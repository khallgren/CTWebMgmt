using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Ind.Reports
{
    public partial class frmTransDownloadsSetup : Form
    {
        DateTime dteDefault;

        public frmTransDownloadsSetup(DateTime _dteDefault)
        {
            InitializeComponent();

            dteDefault = _dteDefault;
        }

        public frmTransDownloadsSetup()
        {
            InitializeComponent();

            dteDefault = DateTime.MinValue;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmTransDownloadsSetup_Load(object sender, EventArgs e)
        {
            //populate cbo
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT COUNT(tblTransDLBatches.lngTransactionID) AS intTransCount, " +
                            "tblTransDLBatches.dteRetrieved " +
                        "FROM tblTransDLBatches " +
                            "INNER JOIN tblTransactions ON tblTransDLBatches.lngTransactionID = tblTransactions.lngTransactionID " +
                        "GROUP BY tblTransDLBatches.dteRetrieved";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drBatch = cmdDB.ExecuteReader())
                    {
                        int intCurrentIndex = 1;

                        cboBatch.Items.Add("");

                        while (drBatch.Read())
                        {
                            DateTime dteRetrieved = DateTime.MinValue;

                            try { dteRetrieved = Convert.ToDateTime(drBatch["dteRetrieved"]); }
                            catch { dteRetrieved = DateTime.MinValue; }

                            cboBatch.Items.Add(dteRetrieved.ToString());

                            if (dteRetrieved == dteDefault) cboBatch.SelectedIndex = intCurrentIndex;

                            intCurrentIndex++;
                        }

                        drBatch.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            DateTime dteCriter = DateTime.MinValue;

            try { dteCriter = Convert.ToDateTime(cboBatch.SelectedItem); }
            catch { dteCriter = DateTime.MinValue; }

            using (frmTransDownloads objTransDownloads = new frmTransDownloads(dteCriter))
            {
                objTransDownloads.ShowDialog();
            }

            this.Close();
        }
    }
}