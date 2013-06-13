using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.ContactInfo.AddressStandardization.Reports
{
    public partial class frmNCOASummaryReport : Form
    {
        public frmNCOASummaryReport()
        {
            InitializeComponent();
        }

        private void btnGenerateRpt_Click(object sender, EventArgs e)
        {
            //get md user id, ticket id
            string strSQL = "";

            int intMDCustomerID = 0;
            int intTicketID = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT strMDCustomerID " +
                            "FROM tblCampDefaults";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { intMDCustomerID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch (Exception ex) { intMDCustomerID = 0; }
                    }

                    conDB.Close();
                }

                try { intTicketID = Convert.ToInt32(((clsCboItem)cboCert.SelectedItem).ID); }
                catch { intTicketID = 0; }

                wsMDSmartMoverV2B.SmartMover wsSM = new global::CTWebMgmt.wsMDSmartMoverV2B.SmartMover();

                wsMDSmartMoverV2B.RespNCOALinkReportLink wsRpt = wsSM.GetSummaryReportLink(intMDCustomerID, "", "");

                string strURL = "";

                try { strURL = wsRpt.SummaryReport.NCOALink; }
                catch { strURL = ""; }

                if (strURL != "")
                {
                    MessageBox.Show("Report generated successfully.\nA new browser window should open to display the report.\nIf the browser fails to open you can copy the link below and paste into a new browser window.");
                    System.Diagnostics.Process.Start(strURL);
                    txtURL.Text = wsRpt.SummaryReport.NCOALink;
                }
                else
                    MessageBox.Show("There was an error retrieving the report.");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNCOASummaryReport_Load(object sender, EventArgs e)
        {
            //load processing dates
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strSQL = "";

                strSQL = "SELECT First(Format([tblRecordCert].[dteProcessed],\"m/d/yyyy h:mm ampm\")) AS dteProcessed, tblRecordCert.strTicketID " +
                        "FROM tblRecordCert " +
                        "GROUP BY tblRecordCert.strTicketID, Format([tblRecordCert].[dteProcessed],\"yyyymmdd\") " +
                        "ORDER BY Format([tblRecordCert].[dteProcessed],\"yyyymmdd\")";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCert = cmdDB.ExecuteReader())
                    {
                        while (drCert.Read())
                        {
                            string strCert = "";

                            int intTicketID=0;

                            try { strCert = Convert.ToString(drCert["dteProcessed"]); }
                            catch { strCert = "Err"; }

                            try { intTicketID = Convert.ToInt32(drCert["strTicketID"]); }
                            catch { intTicketID = 0; }

                            clsCboItem cboNew=new clsCboItem((long)intTicketID,strCert);

                            cboCert.Items.Add(cboNew);
                        }

                        drCert.Close();
                    }
                }

                conDB.Close();
            }
        }
    }
}