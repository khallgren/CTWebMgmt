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
    public partial class frmBatchWebCamperDetailSetup : Form
    {
        public frmBatchWebCamperDetailSetup()
        {
            InitializeComponent();
        }

        private void frmBatchWebCamperDetailSetup_Load(object sender, EventArgs e)
        {
            radAllDates.Checked = true;
            radUnprocessedReg.Checked = true;

            subConfig();
        }

        private void subConfig()
        {
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            dtpStartDate.Visible = false;
            dtpEndDate.Visible = false;

            if (radSpecificDate.Checked)
            {
                lblStartDate.Text = "Registration Date:";
                lblStartDate.Visible = true;
                dtpStartDate.Visible = true;
            }
            else if (radDateRange.Checked)
            {
                lblStartDate.Text = "Start Date:";
                lblStartDate.Visible = true;
                dtpStartDate.Visible = true;

                lblEndDate.Visible = true;
                dtpEndDate.Visible = true;
            }
        }

        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            subConfig();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> lngRegWebIDs = new List<long>();
                
                //get list of id's
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        string strSQL = "";

                        string strWHERE = "";

                        if (radUnprocessedReg.Checked)
                        {
                            if (strWHERE == "")
                                strWHERE = "WHERE (tblWebIndRegistrations.blnProcessed=False) ";
                            else
                                strWHERE += "AND (tblWebIndRegistrations.blnProcessed=False) ";
                        }
                        else if (radProcessedReg.Checked)
                        {
                            if (strWHERE == "")
                                strWHERE = "WHERE (tblWebIndRegistrations.blnProcessed=True) ";
                            else
                                strWHERE += "AND (tblWebIndRegistrations.blnProcessed=True) ";
                        }

                        if (radSpecificDate.Checked)
                        {
                            if (strWHERE == "")
                                strWHERE = "WHERE (DateDiff('d', [tblWebIndRegistrations].[dteRegistrationDate], #"+dtpStartDate.Value.ToString("MM/dd/yyyy")+"#) = 0) ";
                            else
                                strWHERE += "AND (DateDiff('d', [tblWebIndRegistrations].[dteRegistrationDate], #" + dtpStartDate.Value.ToString("MM/dd/yyyy") + "#) = 0) ";
                        }
                        else if (radDateRange.Checked)
                        {
                            if (strWHERE == "")
                                strWHERE = "WHERE (DateDiff('d', #" + dtpStartDate.Value.ToString("MM/dd/yyyy") + "#, [tblWebIndRegistrations].[dteRegistrationDate]) >= 0 AND " +
                                                "DateDiff('d', [tblWebIndRegistrations].[dteRegistrationDate], #" +dtpEndDate.Value.ToString("MM/dd/yyyy") + "#) >= 0) ";
                            else
                                strWHERE += "AND (DateDiff('d', #" + dtpStartDate.Value.ToString("MM/dd/yyyy") + "#, [tblWebIndRegistrations].[dteRegistrationDate]) >= 0 AND " +
                                                "DateDiff('d', [tblWebIndRegistrations].[dteRegistrationDate], #" + dtpEndDate.Value.ToString("MM/dd/yyyy") + "#) >= 0) ";
                        }

                        strSQL = "SELECT lngRegistrationWebID " +
                                "FROM tblWebIndRegistrations " +
                                strWHERE;

                        cmdDB.CommandText = strSQL;

                        using (OleDbDataReader drReg = cmdDB.ExecuteReader())
                        {
                            while (drReg.Read())
                            {
                                long lngRegWebID = 0;

                                try { lngRegWebID = Convert.ToInt32(drReg["lngRegistrationWebID"]); }
                                catch { lngRegWebID = 0; }

                                lngRegWebIDs.Add(lngRegWebID);
                            }

                            drReg.Close();
                        }
                    }

                    conDB.Close();
                }

                //open rpt
                using (frmWebCamperDetails objWebCamperDetails = new frmWebCamperDetails(lngRegWebIDs))
                {
                    objWebCamperDetails.WindowState = FormWindowState.Maximized;
                    objWebCamperDetails.ShowDialog();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error displaying the report: " + ex.Message);
            }
        }
    }
}
