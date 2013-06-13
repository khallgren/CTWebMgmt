using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace CTWebMgmt.Ind.Reports
{
    public partial class frmTransDownloads : Form
    {
        private rptTransDownloads tdlToPrint;

        private DateTime dteCriter;

        public frmTransDownloads()
        {
            InitializeComponent();

            dteCriter = DateTime.MinValue;
        }

        public frmTransDownloads(DateTime _dteCriter)
        {
            InitializeComponent();

            dteCriter = _dteCriter;
        }

        private void frmTransDownloads_Load(object sender, EventArgs e)
        {
            subConfigureCrystalReports();
        }

        private void subConfigureCrystalReports()
        {
            string strSQL = "";

            string strWhere = "";

            if (dteCriter != DateTime.MinValue)
                strWhere = "WHERE DateDiff(\"n\", [tblTransDLBatches].[dteRetrieved], #" + dteCriter.ToString() + "#)=0 ";

            strSQL = "SELECT tblTransDLBatches.lngTransactionID, tblRecords.lngRecordID, " +
                        "tblTransDLBatches.dteRetrieved, " +
                        "tblRecords.strFirstName, tblRecords.strLastCoName, tblBlock.strBlockCode, tblTransactions.strCCNumber, tblTransactions.strCCExpDate, tblTransactions.strAuthNumber, tblTransactions.strXCAlias, tblTransactions.strXCTransID, tblTransactions.strEPSTransID, tblTransactions.strPNRef " +
                    "FROM (((tblTransDLBatches " +
                        "INNER JOIN tblTransactions ON tblTransDLBatches.lngTransactionID = tblTransactions.lngTransactionID) " +
                        "INNER JOIN tblRecords ON tblTransactions.lngRecordID = tblRecords.lngRecordID) " +
                        "LEFT JOIN tblRegistrations ON (tblTransactions.lngRegistrationID = tblRegistrations.lngRegistrationID) AND " +
                            "(tblTransactions.lngRecordID = tblRegistrations.lngRecordID)) " +
                        "LEFT JOIN tblBlock ON tblRegistrations.lngBlockID = tblBlock.lngBlockID " +
                    strWhere +
                    "ORDER BY tblTransDLBatches.dteRetrieved, tblRecords.strLastCoName, tblRecords.strFirstName";

            conCTMain_B.ConnectionString = clsAppSettings.GetAppSettings().strCTConn;

            daTransDownloads.SelectCommand.CommandText = strSQL;

            daTransDownloads.Fill(dsTransDownloads, "qryTransDownloads");

            tdlToPrint = new rptTransDownloads();

            tdlToPrint.SetDataSource(dsTransDownloads);

            rvwTransDownloads.ReportSource = tdlToPrint;

            //determine which live charge fields to display
            //initially hide all live charge controls
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Width = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Width = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Width = 0;

            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Height = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Height = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Height = 0;

            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Left = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Left = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Left = 0;

            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Top = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Top = 0;
            ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Top = 0;

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
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Width = 1590;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Height = 240;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Left = 9810;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Top = 0;
                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.XCharge:
                    {
                        //show xc
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Width = 1590;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Height = 240;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Left = 9810;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Top = 0;
                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.EPS:
                    {
                        //show eps
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Width = 1590;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Height = 240;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Left = 9810;
                        ((FieldObject)tdlToPrint.ReportDefinition.ReportObjects["txtEPSTransID"]).Top = 0;
                        break;
                    }
            }
        }
    }
}