using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;

namespace CTWebMgmt.Ind.Reports
{
    public partial class frmWebCamperDetails : Form
    {
        private rptWebCamperDetails wcdToPrint;

        private List<long> lngRegWebID = new List<long>();

        public frmWebCamperDetails(List<long> _lngRegWebID)
        {
            InitializeComponent();

            lngRegWebID = _lngRegWebID;
        }

        private void frmWebCamperDetails_Load(object sender, EventArgs e)
        {
            subConfigureCrystalReports();
        }

        private void subConfigureCrystalReports()
        {
            string strSQL = "";

            string strHAVING = "";

            for (int intI = 0; intI < lngRegWebID.Count; intI++)
                strHAVING += "(tblWebIndRegistrations.lngRegistrationWebID=" + lngRegWebID[intI].ToString() + ") OR ";

            strHAVING = "HAVING " + strHAVING.Substring(0, strHAVING.Length - 3);

            strSQL = "SELECT tblWebRecords_Camper.blnGender, tblWebIndRegistrations.blnProcessed, " +
                        "tblWebIndRegistrations.lngRegistrationWebID, tblWebRecords_Camper.intGradeCompleted, tblWebRecords_Camper.lngProfileWebID, tblWebIndRegistrations.lngRecordWebID, " +
                        "tblWebIndRegistrations.curDeposit, tblWebIndRegistrations.curSpendingMoney, tblWebIndRegistrations.curDonation, IIf(IsNull([tblBlock].[curCharge]),0,[tblBlock].[curCharge])-IIf(IsNull([tblWebIndRegistrations].[curDeposit]),0,[tblWebIndRegistrations].[curDeposit]) AS decBalance, tblBlock.curCharge, " +
                        "tblWebRecords_Camper.dteBirthDate, tblWebIndRegistrations.dteRegistrationDate, " +
                        "tblWebRecords_Camper.strFirstName AS strCamperFName, tblWebRecords_Camper.strLastCoName AS strCamperLName, tblWebRecords_Camper.strAddress AS strCamperAddress, tblWebRecords_Camper.strCity AS strCamperCity, tlkpStates_Camper.strState AS strCamperState, tblWebRecords_Camper.strZip AS strCamperZip, tblWebRecords_Camper.strHomePhone AS strCamperHomePhone, tblWebRecords_Camper.strCellPhone AS strCamperCellPhone, tblWebRecords_Camper.strEmail AS strCamperEmail, tblWebIndRegistrations.strBuddy1, tblWebIndRegistrations.strBuddy2, tblWebIndRegistrations.strReleaseTo, tblWebRecords_Camper.mmoNotes, tblWebIndRegistrations.strCardNumber AS strCCNumber, tblWebTransactions.strCCExpDate, tblWebRecords_Camper.mmoSpecialNeeds, tblWebRecords_Parent.strFirstName AS strParentFName, tblWebRecords_Parent.strLastCoName AS strParentLName, tblWebRecords_Parent.strAddress AS strParentAddress, tblWebRecords_Parent.strCity AS strParentCity, tlkpStates_Parent.strState AS strParentState, tblWebRecords_Parent.strZip AS strParentZip, tblWebRecords_Parent.strHomePhone AS strParentHomePhone, tblWebRecords_Parent.strWorkPhone AS strParentWorkPhone, tblWebRecords_Parent.strWorkExt AS strParentWorkExt, tblWebRecords_Parent.strCellPhone AS strParentCellPhone, tblWebIndRegistrations.strDependNotes, tblWebRecords_Parent.strEmail AS strParentEmail, tblWebIndRegistrations.strReferredBy, tblWebRecords_Parent.strSpouseFName, tblWebRecords_Parent.strSpouseLName, tblWebRecords_Parent.strSpousePhone, tblWebRecords_Camper.strFatherName, tblWebRecords_Camper.strMotherName, tblWebTransactions.strBillAddress, tblWebTransactions.strBillCity, tblWebTransactions.strBillName, tblWebTransactions.strBillPhone, tblWebTransactions.strBillZip, tlkpStates_Trans.strState AS strBillState, tblWebTransactions.strBankName, tblWebTransactions.strAcctNum, tblWebTransactions.strRoutingNum, tblWebIndRegistrations.strPNRef, tblWebIndRegistrations.strXCAuthCode, tblWebIndRegistrations.strXCTransID, tblWebIndRegistrations.strXCAlias, tblWebIndRegistrations.strEPSPmtAcctID, tblWebIndRegistrations.strOrgCode, tblWebIndRegistrations.mmoRegNotes " +
                    "FROM (((((((tblWebIndRegistrations " +
                        "INNER JOIN tblWebRecords AS tblWebRecords_Camper ON tblWebIndRegistrations.lngRecordWebID = tblWebRecords_Camper.lngRecordWebID) " +
                        "LEFT JOIN tlkpStates AS tlkpStates_Camper ON tblWebRecords_Camper.lngStateID = tlkpStates_Camper.lngStateID) " +
                        "LEFT JOIN tblWebTransactions ON (tblWebIndRegistrations.lngRecordWebID = tblWebTransactions.lngRecordWebID) AND " +
                            "(tblWebIndRegistrations.lngRegistrationWebID = tblWebTransactions.lngRegistrationWebID)) " +
                        "LEFT JOIN tlkpStates AS tlkpStates_Trans ON tblWebTransactions.lngBillStateID = tlkpStates_Trans.lngStateID) " +
                        "INNER JOIN tblWebRecords AS tblWebRecords_Parent ON tblWebRecords_Camper.lngProfileWebID = tblWebRecords_Parent.lngRecordWebID) " +
                        "LEFT JOIN tlkpStates AS tlkpStates_Parent ON tblWebRecords_Parent.lngStateID = tlkpStates_Parent.lngStateID) " +
                        "INNER JOIN tblWebIndRegBlockChoices ON tblWebIndRegistrations.lngRegistrationWebID = tblWebIndRegBlockChoices.lngRegistrationWebID) " +
                        "INNER JOIN tblBlock ON tblWebIndRegBlockChoices.lngBlockID = tblBlock.lngBlockID " +
                    "WHERE tblWebIndRegBlockChoices.lngChoice=1 Or tblWebIndRegBlockChoices.lngChoice Is Null " +
                    "GROUP BY tblWebRecords_Camper.blnGender, tblWebIndRegistrations.blnProcessed, " +
                        "tblWebIndRegistrations.lngRegistrationWebID, tblWebRecords_Camper.intGradeCompleted, tblWebRecords_Camper.lngProfileWebID, tblWebIndRegistrations.lngRecordWebID, " +
                        "tblWebIndRegistrations.curDeposit, tblWebIndRegistrations.curSpendingMoney, tblWebIndRegistrations.curDonation, IIf(IsNull([tblBlock].[curCharge]),0,[tblBlock].[curCharge])-IIf(IsNull([tblWebIndRegistrations].[curDeposit]),0,[tblWebIndRegistrations].[curDeposit]), tblBlock.curCharge, " +
                        "tblWebRecords_Camper.dteBirthDate, tblWebIndRegistrations.dteRegistrationDate, " +
                        "tblWebRecords_Camper.strFirstName, tblWebRecords_Camper.strLastCoName, tblWebRecords_Camper.strAddress, tblWebRecords_Camper.strCity, tlkpStates_Camper.strState, tblWebRecords_Camper.strZip, tblWebRecords_Camper.strHomePhone, tblWebRecords_Camper.strCellPhone, tblWebRecords_Camper.strEmail, tblWebIndRegistrations.strBuddy1, tblWebIndRegistrations.strBuddy2, tblWebIndRegistrations.strReleaseTo, tblWebRecords_Camper.mmoNotes, tblWebIndRegistrations.strCardNumber, tblWebTransactions.strCCExpDate, tblWebRecords_Camper.mmoSpecialNeeds, tblWebRecords_Parent.strFirstName, tblWebRecords_Parent.strLastCoName, tblWebRecords_Parent.strAddress, tblWebRecords_Parent.strCity, tlkpStates_Parent.strState, tblWebRecords_Parent.strZip, tblWebRecords_Parent.strHomePhone, tblWebRecords_Parent.strWorkPhone, tblWebRecords_Parent.strWorkExt, tblWebRecords_Parent.strCellPhone, tblWebIndRegistrations.strDependNotes, tblWebRecords_Parent.strEmail, tblWebIndRegistrations.strReferredBy, tblWebRecords_Parent.strSpouseFName, tblWebRecords_Parent.strSpouseLName, tblWebRecords_Parent.strSpousePhone, tblWebRecords_Camper.strFatherName, tblWebRecords_Camper.strMotherName, tblWebTransactions.strBillAddress, tblWebTransactions.strBillCity, tblWebTransactions.strBillName, tblWebTransactions.strBillPhone, tblWebTransactions.strBillZip, tlkpStates_Trans.strState, tblWebTransactions.strBankName, tblWebTransactions.strAcctNum, tblWebTransactions.strRoutingNum, tblWebIndRegistrations.strPNRef, tblWebIndRegistrations.strXCAuthCode, tblWebIndRegistrations.strXCTransID, tblWebIndRegistrations.strXCAlias, tblWebIndRegistrations.strEPSPmtAcctID, tblWebIndRegistrations.strOrgCode, tblWebIndRegistrations.mmoRegNotes " +
                    strHAVING + " " +
                    "ORDER BY tblWebIndRegistrations.dteRegistrationDate";


            conCTMain_B.ConnectionString = clsAppSettings.GetAppSettings().strCTConn;

            daWebCamperDetails.SelectCommand.CommandText = strSQL;

            daWebCamperDetails.Fill(dsWebCamperDetails, "qrptWebCamperDetails");

            //set data sources for sub-reports
            //Block choices
            string strSQL_BlockChoices = "";

            strSQL_BlockChoices = "SELECT tblWebIndRegBlockChoices.lngRegistrationWebID, tblWebIndRegBlockChoices.lngChoice, " +
                                    "tblBlock.strBlockCode " +
                                "FROM tblWebIndRegBlockChoices " +
                                    "INNER JOIN tblBlock ON tblWebIndRegBlockChoices.lngBlockID = tblBlock.lngBlockID " +
                                "ORDER BY tblWebIndRegBlockChoices.lngChoice";

            daWebCamperDetails_BlockChoices.SelectCommand.CommandText = strSQL_BlockChoices;
            daWebCamperDetails_BlockChoices.Fill(dsWebCamperDetails_BlockChoices, "sqryWebCamperDetails_BlockChoices");

            //Custom fields, camper
            string strSQL_CustomFieldsCamper = "";

            strSQL_CustomFieldsCamper = "SELECT tblCustomFieldValWebIR.lngCustomFieldValWebIRID, tblCustomFieldValWebIR.lngRecordWebID, " +
                                            "tblCustomFieldValWebIR.strLocalCaption, IIf([tblCustomFieldDefIR].[strFieldType]='FLAG',IIf([tblCustomFieldValWebIR].[strValue]='-1' OR [tblCustomFieldValWebIR].[strValue]='True','X','0'),[tblCustomFieldValWebIR].[strValue]) AS strValue " +
                                        "FROM tblCustomFieldDefIR " +
                                            "RIGHT JOIN tblCustomFieldValWebIR ON tblCustomFieldDefIR.strLocalCaption = tblCustomFieldValWebIR.strLocalCaption " +
                                        "WHERE tblCustomFieldDefIR.blnUseLocal=True " +
                                        "GROUP BY tblCustomFieldValWebIR.lngCustomFieldValWebIRID, tblCustomFieldValWebIR.lngRecordWebID, tblCustomFieldDefIR.lngSortOrder, " +
                                            "tblCustomFieldValWebIR.strLocalCaption, IIf([tblCustomFieldDefIR].[strFieldType]='FLAG',IIf([tblCustomFieldValWebIR].[strValue]='-1' OR [tblCustomFieldValWebIR].[strValue]='True','X','0'),[tblCustomFieldValWebIR].[strValue]) " +
                                        "ORDER BY tblCustomFieldDefIR.lngSortOrder";

            daWebCamperDetails_CustomFieldsCamper.SelectCommand.CommandText = strSQL_CustomFieldsCamper;
            daWebCamperDetails_CustomFieldsCamper.Fill(dsWebCamperDetails_CustomFieldsCamper, "sqryWebCamperDetails_CustomFieldsCamper");

            //Custom fields, profile
            string strSQL_CustomFieldsProfile = "";

            strSQL_CustomFieldsProfile = "SELECT tblCustomFieldValWebIR.lngCustomFieldValWebIRID, tblCustomFieldValWebIR.lngRecordWebID, " +
                                            "tblCustomFieldValWebIR.strLocalCaption, IIf([tblCustomFieldDefIR].[strFieldType]='FLAG',IIf([tblCustomFieldValWebIR].[strValue]='-1' OR [tblCustomFieldValWebIR].[strValue]='True','X','0'),[tblCustomFieldValWebIR].[strValue]) AS strValue " +
                                        "FROM tblCustomFieldDefIR " +
                                            "RIGHT JOIN tblCustomFieldValWebIR ON tblCustomFieldDefIR.strLocalCaption = tblCustomFieldValWebIR.strLocalCaption " +
                                        "WHERE tblCustomFieldDefIR.blnUseLocal=True " +
                                        "GROUP BY tblCustomFieldValWebIR.lngCustomFieldValWebIRID, tblCustomFieldValWebIR.lngRecordWebID, tblCustomFieldDefIR.lngSortOrder, " +
                                            "tblCustomFieldValWebIR.strLocalCaption, IIf([tblCustomFieldDefIR].[strFieldType]='FLAG',IIf([tblCustomFieldValWebIR].[strValue]='-1' OR [tblCustomFieldValWebIR].[strValue]='True', 'X','0'),[tblCustomFieldValWebIR].[strValue]) " +
                                        "ORDER BY tblCustomFieldDefIR.lngSortOrder";

            daWebCamperDetails_CustomFieldsProfile.SelectCommand.CommandText = strSQL_CustomFieldsProfile;
            daWebCamperDetails_CustomFieldsProfile.Fill(dsWebCamperDetails_CustomFieldsProfile, "sqryWebCamperDetails_CustomFieldsProfile");

            //Custom fields, reg
            string strSQL_CustomFieldsReg = "";

            strSQL_CustomFieldsReg = "SELECT tblCustomFieldValWebReg.lngCustomFieldValWebRegID, tblCustomFieldValWebReg.lngRegistrationWebID, " +
                                        "tblCustomFieldValWebReg.strLocalCaption, IIf([tblCustomFieldDefReg].[strFieldType]='FLAG',IIf([tblCustomFieldValWebReg].[strValue]='-1' Or [tblCustomFieldValWebReg].[strValue]='True','X','0'),[tblCustomFieldValWebReg].[strValue]) AS strValue " +
                                    "FROM tblCustomFieldValWebReg " +
                                        "LEFT JOIN tblCustomFieldDefReg ON tblCustomFieldValWebReg.strLocalCaption = tblCustomFieldDefReg.strLocalCaption " +
                                    "WHERE tblCustomFieldDefReg.blnUseLocal=True " +
                                    "GROUP BY tblCustomFieldValWebReg.lngCustomFieldValWebRegID, tblCustomFieldValWebReg.lngRegistrationWebID, tblCustomFieldDefReg.lngSortOrder, " +
                                        "tblCustomFieldValWebReg.strLocalCaption, IIf([tblCustomFieldDefReg].[strFieldType]='FLAG',IIf([tblCustomFieldValWebReg].[strValue]='-1' Or [tblCustomFieldValWebReg].[strValue]='True','X','0'),[tblCustomFieldValWebReg].[strValue]) " +
                                    "ORDER BY tblCustomFieldDefReg.lngSortOrder";

            daWebCamperDetails_CustomFieldsReg.SelectCommand.CommandText = strSQL_CustomFieldsReg;
            daWebCamperDetails_CustomFieldsReg.Fill(dsWebCamperDetails_CustomFieldsReg, "sqryWebCamperDetails_CustomFieldsReg");

            wcdToPrint = new rptWebCamperDetails();

            wcdToPrint.SetDataSource(dsWebCamperDetails);
            wcdToPrint.Subreports[0].SetDataSource(dsWebCamperDetails_BlockChoices);
            wcdToPrint.Subreports[1].SetDataSource(dsWebCamperDetails_CustomFieldsCamper);
            wcdToPrint.Subreports[2].SetDataSource(dsWebCamperDetails_CustomFieldsProfile);
            wcdToPrint.Subreports[3].SetDataSource(dsWebCamperDetails_CustomFieldsReg);

            rvwWebCamperDetails.ReportSource = wcdToPrint;

            //determine which live charge fields to display
            //initially hide all live charge controls
            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Text = "";
            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Width = 0;
            ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Width = 0;

            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCAuthCode"]).Text = "";
            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCAuthCode"]).Width = 0;
            ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtXCAuthCode"]).Width = 0;

            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Text = "";
            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Width = 0;
            ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Width = 0;

            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Text = "";
            ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Width = 0;
            ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtEPSPmtAcctID"]).Width = 0;

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
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Text = "PN Ref:";
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblPNRef"]).Width = 1560;
                        ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtPNRef"]).Width = 1800;
                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.XCharge:
                    {
                        //show xc
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCAuthCode"]).Text = "XC Auth Code:";
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCAuthCode"]).Width = 1560;
                        ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtXCAuthCode"]).Width = 1800;

                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Text = "XC Trans ID:";
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblXCTransID"]).Width = 1560;
                        ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtXCTransID"]).Width = 1800;
                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.EPS:
                    {
                        //show eps
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Text = "Pmt Acct ID:";
                        ((TextObject)wcdToPrint.ReportDefinition.ReportObjects["lblEPSPmtAcctID"]).Width = 1560;
                        ((FieldObject)wcdToPrint.ReportDefinition.ReportObjects["txtEPSPmtAcctID"]).Width = 3720;
                        break;
                    }
            }
        }
    }
}