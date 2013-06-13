using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;


namespace CTWebMgmt.Donor
{
    public partial class frmDLGifts : Form
    {
        public frmDLGifts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseDLGifts();
        }

        private void btnDLGifts_Click(object sender, EventArgs e)
        {
            //download profile, gifts, pledges, pledge pmts, and donor express records

            wsXferEventInfo.XferEventInfo wsDLGift;

            string strSQL;
            string[,] strAppendResXML = new string[5, 2];

            long lngCTUserID = clsAppSettings.GetAppSettings().lngCTUserID;

            int intGiftCount = 0;
            int intDXCount = 0;

            try
            {
                intGiftCount = clsDonorCRUD.fcnGiftWebCount();
                intDXCount = clsDonorCRUD.fcnDXCount();

                lstStatus.Items.Insert(0, "Downloading Gifts...");
                Application.DoEvents();

                wsDLGift = new wsXferEventInfo.XferEventInfo();

                //clear errors from previous download
                wsDLGift.fcnClearErrs(lngCTUserID, clsWebTalk.strWebConn, "tblGift");

                //get contacts (tblProfiles)
                lstStatus.Items.Insert(0, "Getting new donors");
                Application.DoEvents();

                strSQL = "SELECT tblRecords.blnFlag1, tblRecords.blnFlag2, tblRecords.blnFlag3, tblRecords.blnFlag4, tblRecords.blnFlag5, tblRecords.blnFlag6, tblRecords.blnFlag7, tblRecords.blnFlag8, tblRecords.blnFlag9, tblRecords.blnFlag10, " +
                            "tblRecords.dteCreationDate, " +
                            "tblRecords.lngRecordWebID, tblRecords.lngRecordID, tblRecords.lngStateID, " +
                            "tblRecords.strFirstName, tblRecords.strLastCoName, tblRecords.strAddress, tblRecords.strCity, tblRecords.strZip, tblRecords.strHomePhone, tblRecords.strWorkPhone, tblRecords.strWorkExt, tblRecords.strCellPhone, tblRecords.strEmail, tblRecords.strSpouseFName, tblRecords.strSpouseLName, tblRecords.strSpousePhone, tblRecords.strReferredBy, tblRecords.strCustom1, tblRecords.strCustom2, tblRecords.strCustom3, tblRecords.strCustom4, tblRecords.strCustom5, tblRecords.strCustom6, tblRecords.strCustom7, tblRecords.strCustom8, tblRecords.strCustom9, tblRecords.strCustom10, tblRecords.strMI, tblRecords.strTitle, tblRecords.strInformalSal " +
                        "FROM tblGift " +
                            "INNER JOIN tblRecords ON tblGift.lngRecordWebID = tblRecords.lngRecordWebID AND " +
                                "tblGift.lngCTUserID = tblRecords.lngCTUserID " +
                        "WHERE ISNULL(tblGift.lngGiftID, 0) = 0 AND " +
                            "tblGift.blnRetrieved = 0 AND " +
                            "tblRecords.lngCTUserID = " + lngCTUserID;

                strAppendResXML[0, 0] = wsDLGift.fcnGetRecords(strSQL, "tblWebRecords", clsWebTalk.strWebConn);

                //get gifts (tblGifts)
                lstStatus.Items.Insert(0, "Getting new gifts");
                Application.DoEvents();
                
                strSQL = "SELECT blnMemorial, blnInHonorOf, blnPledgeReminders, blnPledgeAutopay, " +
                            "intPledgeFreq, intPledgeTerm, lngGiftWebID, lngRecordWebID, lngRecordID, lngGiftCategoryID, lngPledgeID, lngCampaignID, lngPaymentTypeID, lngBillStateID, " +
                            "dteGiftDate, " +
                            "curAmount, " +
                            "strMemorialName, strInHonorOf, strAcctNum, strBankName, strBillAddress, strBillCity, strBillName, strBillPhone, strBillZip, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum, strAuthNum, strPNRef, strXCAlias, strXCTransID, strXCEFTAuthCode, strXCEFTRefID, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID " +
                        "FROM tblGift " +
                        "WHERE ISNULL(tblGift.lngGiftID, 0) = 0 AND " +
                            "(blnRetrieved = 0) AND " +
                            "(lngCTUserID = " + lngCTUserID + " )";
                
                strAppendResXML[1, 0] = wsDLGift.fcnGetRecords(strSQL, "tblWebGift", clsWebTalk.strWebConn);

                //get donor express gifts
                lstStatus.Items.Insert(0, "Getting new donor express gifts");
                Application.DoEvents();

                strSQL = "SELECT lngDonorExpressID, lngPaymentTypeID, " +
                            "dteCreated, dteSubmitted, " +
                            "curGiftAmt, " +
                            "strEmail, strFName, strLName, strAddress, strCity, strState, strZip, strHomePhone, strReferredBy, strIMO, strIHO, strCheckNumber, strAcctNum, strBankName, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum, strAuthNum, strPNRef, strXCAlias, strXCTransID, strXCEFTAuthCode, strXCEFTRefID, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID " +
                        "FROM tblDonorExpress " +
                        "WHERE blnRetrieved = 0 AND " +
                            "lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "(NOT (dteSubmitted IS NULL))";

                strAppendResXML[2, 0] = wsDLGift.fcnGetRecords(strSQL, "tblDonorExpress", clsWebTalk.strWebConn);

                lstStatus.Items.Insert(0, "Getting new donor express custom values");
                Application.DoEvents();

                strSQL = "SELECT tblDonorExpressCustomVals.lngDonorExpressID, " +
                            "tblDonorExpressCustomVals.strFieldName, tblDonorExpressCustomVals.strValue " +
                        "FROM tblDonorExpressCustomVals " +
                            "INNER JOIN tblDonorExpress ON tblDonorExpressCustomVals.lngDonorExpressID = tblDonorExpress.lngDonorExpressID " +
                        "WHERE tblDonorExpressCustomVals.lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "tblDonorExpress.blnRetrieved = 0 AND " +
                            "(NOT (tblDonorExpress.dteSubmitted IS NULL))";

                strAppendResXML[3, 0] = wsDLGift.fcnGetRecords(strSQL, "tblDonorExpressCustomVals", clsWebTalk.strWebConn);

                strSQL = "SELECT tblDXDonorCustomVals.lngDonorExpressID, " +
                            "tblDXDonorCustomVals.strFieldName, tblDXDonorCustomVals.strValue " +
                        "FROM tblDXDonorCustomVals " +
                            "INNER JOIN tblDonorExpress ON tblDXDonorCustomVals.lngDonorExpressID = tblDonorExpress.lngDonorExpressID " +
                        "WHERE tblDonorExpress.blnRetrieved = 0 AND " +
                            "(NOT (tblDonorExpress.dteSubmitted IS NULL)) AND " +
                            "tblDXDonorCustomVals.lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString();

                strAppendResXML[4, 0] = wsDLGift.fcnGetRecords(strSQL, "tblDXDonorCustomVals", clsWebTalk.strWebConn);
                
                //add contacts, get result xml
                strAppendResXML[0, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebRecords", strAppendResXML[0, 0], "lngRecordWebID", false, "lngGiftWebID");

                lstStatus.Items.Insert(0, "Adding new donors");
                Application.DoEvents();

                strAppendResXML[1, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebGift", strAppendResXML[1, 0], "lngGiftWebID", false, "lngGiftWebID");

                lstStatus.Items.Insert(0, "Adding new gifts");
                Application.DoEvents();

                //append downloaded donor express records to db
                strAppendResXML[2, 1] = clsWebTalk.fcnWriteXMLToDB("tblDonorExpress", strAppendResXML[2, 0], "lngDonorExpressID", false, "lngDonorExpressID");

                //append downloaded donor express custom vals to db
                strAppendResXML[3, 1] = clsWebTalk.fcnWriteXMLToDB("tblDonorExpressCustomVals", strAppendResXML[3, 0], "", false, "lngDonorExpressID");
                strAppendResXML[4, 1] = clsWebTalk.fcnWriteXMLToDB("tblDXDonorCustomVals", strAppendResXML[4, 0], "", false, "lngDonorExpressID");

                lstStatus.Items.Insert(0, "Posting download results to server");
                Application.DoEvents();

                //send xml back to server
                if (wsDLGift.fcnDLRes(strAppendResXML[0, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Insert(0, "Error posting donor results");
                Application.DoEvents();

                string strRes = wsDLGift.fcnDLRes(strAppendResXML[1, 1], clsWebTalk.strWebConn);
                if (strRes != "")
                    lstStatus.Items.Insert(0, "Error posting gift results");
                Application.DoEvents();

                if (wsDLGift.fcnDLRes(strAppendResXML[2, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Insert(0, "Error posting donor express results");
                Application.DoEvents();

                wsDLGift.Dispose();

                intGiftCount = clsDonorCRUD.fcnGiftWebCount() - intGiftCount;
                intDXCount = clsDonorCRUD.fcnDXCount() - intDXCount;

                lstStatus.Items.Insert(0, "Successfully downloaded and added " + intGiftCount.ToString() + " new gifts and " + intDXCount.ToString() + " donor express gifts");
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("btnDL_Click", ex);
            }

            wsDLGift = null;

            DialogResult resNav;

            resNav = MessageBox.Show("Process gifts now?", "Process Gifts?", MessageBoxButtons.YesNo);

            if (resNav == DialogResult.Yes)
            {
                clsNav.subShowProcessGifts(clsNav.objSwitchboard);
                clsNav.subCloseDLGifts();
            }
        }
    }
}