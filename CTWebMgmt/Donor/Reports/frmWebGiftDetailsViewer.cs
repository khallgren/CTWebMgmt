using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OleDb;

namespace CTWebMgmt.Donor.Reports
{
    public partial class frmWebGiftDetailsViewer : Form
    {
        private rptWebGiftDetails wgdWebGiftDetails;
        private long lngGiftWebID ;

        public frmWebGiftDetailsViewer(long _lngGiftWebID)
        {
            InitializeComponent();

            lngGiftWebID = _lngGiftWebID;
        }

        private void frmWebGiftDetailsViewer_Load(object sender, EventArgs e)
        {
            subConfigureCrystalReports();
        }

        private void subConfigureCrystalReports()
        {
            string strSQL = "";

            string strWHERE = "";

            strWHERE="WHERE tblWebGift.lngGiftWebID="+lngGiftWebID.ToString();

            strSQL = "SELECT tblWebGift.blnPledgeReminders, tblWebGift.blnPledgeAutopay, " +
                        "tblWebGift.intPledgeFreq, tblWebGift.intPledgeTerm, " +
                        "tblWebGift.lngGiftWebID, tblWebGift.lngRecordWebID, " +
                        "tblWebGift.curAmount, " +
                        "tblWebGift.dteGiftDate, " +
                        "tlkpCampaignCodes.strCampaignCode, tblGiftCategory.strOLDesc, tlkpPaymentType.strPaymentType, tblWebGift.strInHonorOf, tblWebGift.strAcctNum, tblWebGift.strBankName, tblWebGift.strCCNumber, tblWebGift.strRoutingNum, tblWebGift.strMemorialName, tblWebGift.strPNRef, tblWebGift.strXCAlias, tblWebGift.strXCTransID, tblWebGift.strEPSTransID, tblWebGift.strEPSApprovalNumber, tblWebGift.strEPSValidationCode, tblWebGift.strEPSPmtAcctID, tblWebRecords.strFirstName, tblWebRecords.strLastCoName, tblWebRecords.strAddress, tblWebRecords.strCity, tlkpStates.strState, tblWebRecords.strZip, tblWebRecords.strEmail, tblWebRecords.strHomePhone, tblWebRecords.strCellPhone " +
                    "FROM ((((tblWebGift " +
                        "LEFT JOIN tlkpCampaignCodes ON tblWebGift.lngCampaignID = tlkpCampaignCodes.lngCampaignID) " +
                        "LEFT JOIN tblGiftCategory ON tblWebGift.lngGiftCategoryID = tblGiftCategory.lngGiftCategoryID) " +
                        "INNER JOIN tblWebRecords ON tblWebGift.lngRecordWebID = tblWebRecords.lngRecordWebID) " +
                        "LEFT JOIN tlkpStates ON tblWebRecords.lngStateID = tlkpStates.lngStateID) " +
                        "LEFT JOIN tlkpPaymentType ON tblWebGift.lngPaymentTypeID = tlkpPaymentType.lngPaymentTypeID " +
                    strWHERE;

            conCTMain_B.ConnectionString = clsAppSettings.GetAppSettings().strCTConn;

            daWebGiftDetails.SelectCommand.CommandText = strSQL;

            daWebGiftDetails.Fill(dsWebGiftDetails, "qrptWebGiftDetails");

            wgdWebGiftDetails = new rptWebGiftDetails();

            wgdWebGiftDetails.SetDataSource(dsWebGiftDetails);

            rvwWebGiftDetails.ReportSource = wgdWebGiftDetails;
            
                    int intPledgeFreq=0;
            string strPledgeFreq = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT intPledgeFreq " +
                        "FROM tblWebGift " +
                        "WHERE lngGiftWebID=" + lngGiftWebID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { intPledgeFreq = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intPledgeFreq = 0; }
                }

                conDB.Close();
            }

            switch (intPledgeFreq)
            {
                case 0:
                    strPledgeFreq = "";
                    break;

                case 1:
                    strPledgeFreq = "Weekly";
                    break;

                case 2:
                    strPledgeFreq = "Monthly";
                    break;

                case 3:
                    strPledgeFreq = "Quarterly";
                    break;

                case 4:
                    strPledgeFreq = "Semi-Annually";
                    break;

                case 5:
                    strPledgeFreq = "Annually";
                    break;

                case 6:
                    strPledgeFreq = "Bi-Monthly";
                    break;

                case 7:
                    strPledgeFreq = "Bi-Weekly";
                    break;
            }

            //determine applicable pledge frequency
            ((TextObject)wgdWebGiftDetails.ReportDefinition.ReportObjects["txtPledgeFreq"]).Text = strPledgeFreq;

            //determine which live charge fields to display
            //initially hide all live charge controls

            //cashlinq
/*            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Text = "";
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Width = 0;
            ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Width = 0;

            //xcharge
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Text = "";
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Width = 0;
            ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Width = 0;

            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCAlias"]).Text = "";
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCAlias"]).Width = 0;
            ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtXCAlias"]).Width = 0;

            //eps
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSTransID"]).Text = "";
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSTransID"]).Width = 0;
            ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Width = 0;

            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Text = "";
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Width = 0;
            ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtEPSPmtAcctID"]).Width = 0;

            //select which cc val codes to display
            switch (clsLiveCharge.fcnGetLiveChargeMethod())
            {
                //lblPNRef
                //txtPNRef
                //lblXCAuthCode
                //txtXCAuthCode
                case clsGlobalEnum.conLIVECHARGE.CashLinq:
                    {
                        //show pnref
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Text = "PN Ref:";
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Width = 1560;
                        ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Width = 1800;
                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.XCharge:
                    {
                        //show xc
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Text = "XC Trans ID:";
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Width = 1560;
                        ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Width = 1800;

                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCAlias"]).Text = "XC Alias:";
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblXCAlias"]).Width = 1560;
                        ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtXCAlias"]).Width = 1800;
                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.EPS:
                    {
                        //show eps
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSTransID"]).Text = "EPS Trans ID:";
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSTransID"]).Width = 1560;
                        ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Width = 1800;

                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Text = "EPS Pmt Acct ID:";
                        ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Width = 1560;
                        ((FieldObject)dxdToPrint.ReportDefinition.ReportObjects["txtEPSPmtAcctID"]).Width = 3600;
                        break;
                    }
            }*/
        }
    }
}
