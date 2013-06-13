using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace CTWebMgmt.Donor.Reports
{
    public partial class frmDXDetailsViewer : Form
    {
        private rptDXDetails dxdToPrint;
        private long lngDonorExpressID;

        public frmDXDetailsViewer(long _lngDonorExpressID)
        {
            InitializeComponent();

            lngDonorExpressID = _lngDonorExpressID;
        }

        private void frmDXDetailsViewer_Load(object sender, EventArgs e)
        {
            subConfigureCrystalReports();
        }

        private void subConfigureCrystalReports()
        {
            string strSQL = "";

            string strWHERE = "";

            strWHERE = "WHERE tblDonorExpress.lngDonorExpressID=" + lngDonorExpressID.ToString();

            strSQL = "SELECT tblDonorExpress.blnProcessed, " +
                        "tblDonorExpress.lngDonorExpressID, tblDonorExpress.lngPaymentTypeID, " +
                        "tblDonorExpress.dteCreated, tblDonorExpress.dteSubmitted, " +
                        "tblDonorExpress.curGiftAmt, " +
                        "tblDonorExpress.strEmail, tblDonorExpress.strFName, tblDonorExpress.strLName, tblDonorExpress.strAddress, tblDonorExpress.strCity, tblDonorExpress.strState, tblDonorExpress.strZip, tblDonorExpress.strHomePhone, tblDonorExpress.strReferredBy, tblDonorExpress.strIMO, tblDonorExpress.strIHO, tblDonorExpress.strCheckNumber, tblDonorExpress.strAcctNum, tblDonorExpress.strBankName, tblDonorExpress.strCCExpDate, tblDonorExpress.strCCNumber, tblDonorExpress.strCCValCode, tblDonorExpress.strRoutingNum, tblDonorExpress.strAuthNum, tblDonorExpress.strPNRef, tblDonorExpress.strXCAlias, tblDonorExpress.strXCTransID, tblDonorExpress.strXCEFTAuthCode, tblDonorExpress.strXCEFTRefID, tblDonorExpress.strEPSTransID, tblDonorExpress.strEPSApprovalNumber, tblDonorExpress.strEPSValidationCode, tblDonorExpress.strEPSPmtAcctID " +
                    "FROM tblDonorExpress " +
                    strWHERE;

            conCTMain_B.ConnectionString = clsAppSettings.GetAppSettings().strCTConn;

            daDXDetails.SelectCommand.CommandText = strSQL;

            daDXDetails.Fill(dsDXDetails, "qrptDonorExpress");

            //set data sources for sub-reports
            //custom gift fields
            string strSQL_CustomFields = "";

            strSQL_CustomFields = "SELECT tblDonorExpressCustomVals.lngDonorExpressID, " +
                                    "tblDonorExpressCustomVals.strFieldName, tblDonorExpressCustomVals.strValue " +
                                "FROM tblCustomFieldsGiftDef " +
                                    "LEFT JOIN tblDonorExpressCustomVals ON tblCustomFieldsGiftDef.strFieldName = tblDonorExpressCustomVals.strFieldName " +
                                "ORDER BY tblCustomFieldsGiftDef.intSortOrder";

            daDXDetails_CustomFields.SelectCommand.CommandText = strSQL_CustomFields;
            daDXDetails_CustomFields.Fill(dsDXDetails_CustomFields, "sqryDonorExpres_CustomFields");

            dxdToPrint = new rptDXDetails();

            dxdToPrint.SetDataSource(dsDXDetails);
            dxdToPrint.Subreports[0].SetDataSource(dsDXDetails_CustomFields);

            rvwDXDetails.ReportSource = dxdToPrint;

            //determine which live charge fields to display
            //initially hide all live charge controls

            //cashlinq
            ((TextObject)dxdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Text = "";
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
            }
        }
    }
}