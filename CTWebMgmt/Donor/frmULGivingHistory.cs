using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;
using System.Data.OleDb;


namespace CTWebMgmt.Donor
{
    public partial class frmULGivingHistory : Form
    {
        public frmULGivingHistory()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseULGivingHistory();
        }

        private string fcnULDonors()
        {
            //add ids to tbl to upload
            //get count
            //loop through and upload 50 at a time

            string strSQL = "";
            string strRes = "";

            strSQL = "DELETE * FROM tblIDHolder";

            int intRecordCount = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();

                        strSQL = "INSERT INTO tblIDHolder " +
                                "(lngID) " +
                                "SELECT tblRecords.lngRecordID " +
                                "FROM tblRecords " +
                                "WHERE tblRecords.blnDonor=True AND " +
                                    "tblRecords.strEmail<>\"\" AND " +
                                    "tblRecords.strEmail Is Not Null";

                        cmdDB.CommandText = strSQL;

                        cmdDB.ExecuteNonQuery();

                        strSQL = "SELECT COUNT(lngID) " +
                                "FROM tblIDHolder";

                        cmdDB.CommandText = strSQL;

                        try { intRecordCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { intRecordCount = 0; }

                        for (int intCurrentPass = 1; (intCurrentPass - 1) * 50 <= intRecordCount; intCurrentPass++)
                        {
                            //upload chunk
                            strSQL = "SELECT TOP 50 tblRecords.blnTick, tblRecords.blnGender, tblRecords.blnMailingListInclude, tblRecords.blnParent, tblRecords.blnCamper, tblRecords.blnGroup, tblRecords.blnChurch, tblRecords.blnDonor, tblRecords.blnStaff, tblRecords.blnBoardMember, tblRecords.blnVolunteer, tblRecords.blnSpecialNeeds, tblRecords.blnFormerCamper, tblRecords.blnConstChurch, tblRecords.blnUseMORAddress, tblRecords.blnActiveRecord, tblRecords.blnEventCoordinator, tblRecords.blnSchool, tblRecords.blnRecruit, tblRecords.blnApprovalNeeded, " +
                                        "tblRecords.intGradeCompleted, tblRecords.intYearofGraduation, " +
                                        "tblRecords.lngRecordID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[lngMORStateID],[tblRecords].[lngStateID]) AS lngStateID, tblRecords.lngStateID2, tblRecords.lngChurchId, tblRecords.lngPrimaryMORID, tblRecords.lngPrimaryChurchID, tblRecords.lngCountryID, tblRecords.lngRecruitStatusID, tblRecords.lngContactLastModifiedUser, tblRecords.lngWebCamperID, tblRecords.lngRecordWebID, " +
                                        "tblRecords.curIncome, " +
                                        "CStr(IIf(IsNull(tblRecords.dteBirthDate), \"\", tblRecords.dteBirthDate)) AS dteBirthDate, CStr(IIf(IsNull(tblRecords.dteCreationDate), \"\", tblRecords.dteCreationDate)) AS dteCreationDate, CStr(IIf(IsNull(tblRecords.dteModificationDate), \"\", tblRecords.dteModificationDate)) AS dteModificationDate, CStr(IIf(IsNull(tblRecords.dteContactLastModified), \"\", tblRecords.dteContactLastModified)) AS dteContactLastModified, " +
                                        "tblRecords.strLastCoName, tblRecords.strFirstName, tblRecords.strCompanyName, tblRecords.strEmail, tblRecords.strAddressDesc, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORAddress1],[tblRecords].[strAddress]) AS strAddress, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORZip],[tblRecords].[strZip]) AS strZip, tblRecords.strWorkExt, tblRecords.strFaxPhone, tblRecords.strAddressDesc2, tblRecords.strAddress2, tblRecords.strCity2, tblRecords.strZip2, tblRecords.strHomePhone2, tblRecords.strWorkPhone2, tblRecords.strFaxPhone2, tblRecords.strSpouseName, tblRecords.strFatherName, tblRecords.strMotherName, tblRecords.strKidsNames, tblRecords.strSiblingNames, tblRecords.txtParentSalutation, tblRecords.strBarCode, tblRecords.strCarrierRoute, tblRecords.strDPCode, tblRecords.strFormalGiftSal, tblRecords.strInformalGiftSal, tblRecords.strReturnCode, tblRecords.strSSN, tblRecords.strWorkPhone, tblRecords.strCellPhone, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORCity],[tblRecords].[strCity]) AS strCity, tblRecords.strConfEmail, tblRecords.strMI, tblRecords.strCounty, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORPhone],[tblRecords].[strHomePhone]) AS strHomePhone, " +
                                        "tblRecords.mmoSpecialNeeds, tblRecords.mmoNotes, tblRecords.Notes " +
                                    "FROM (tblRecords " +
                                        "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                                        "INNER JOIN tblIDHolder ON tblRecords.lngRecordID = tblIDHolder.lngID " +
                                    "ORDER BY tblIDHolder.lngID";

                            if (intCurrentPass * 50 > intRecordCount)
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Donors " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + intRecordCount.ToString() + " of " + intRecordCount.ToString()));
                            else
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Donors " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + (intCurrentPass * 50).ToString() + " of " + intRecordCount.ToString()));
                            Application.DoEvents();

                            strRes += clsWebTalk.fcnUploadIRs(strSQL, false);

                            strSQL = "DELETE * " +
                                    "FROM tblIDHolder " +
                                    "WHERE tblIDHolder.lngID IN " +
                                        "(SELECT TOP 50 tblRecords.lngRecordID " +
                                        "FROM (tblRecords " +
                                            "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID ) " +
                                            "INNER JOIN tblIDHolder ON tblRecords.lngRecordID = tblIDHolder.lngID " +
                                        "ORDER BY tblIDHolder.lngID)";

                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }

            return strRes;
        }

        private string fcnULGifts()
        {
            //add ids to tbl to upload
            //get count
            //loop through and upload 50 at a time            

            string strSQL = "";
            string strRes = "";

            strSQL = "DELETE * FROM tblIDHolder";

            int intRecordCount = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();

                        strSQL = "INSERT INTO tblIDHolder " +
                                "(lngID) " +
                                "SELECT tblGift.lngGiftID " +
                                "FROM tblRecords " +
                                    "INNER JOIN tblGift ON tblRecords.lngRecordID = tblGift.lngRecordID " +
                                "WHERE tblRecords.blnDonor=True AND " +
                                    "tblRecords.strEmail<>\"\" AND " +
                                    "tblRecords.strEmail Is Not Null";

                        cmdDB.CommandText = strSQL;

                        cmdDB.ExecuteNonQuery();

                        strSQL = "SELECT COUNT(lngID) " +
                                "FROM tblIDHolder";

                        cmdDB.CommandText = strSQL;

                        try { intRecordCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { intRecordCount = 0; }

                        for (int intCurrentPass = 1; (intCurrentPass - 1) * 50 <= intRecordCount; intCurrentPass++)
                        {
                            //upload chunk
                            strSQL = "SELECT TOP 50 tblGift.blnMemorial, tblGift.blnInHonorOf, tblGift.blnAnonymous, CBool(-1) AS blnRetrieved, " +
                                        "tblGift.lngGiftID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGift.lngGiftCategoryID, tblGift.lngRecordID, IIf(IsNull(tblGift.lngPledgeID), 0, tblGift.lngPledgeID) AS lngPledgeID, tblGift.lngCampaignID, tblGift.lngGiftTypeID, tblGift.lngNonCashID, tblGift.lngRepID, IIf(IsNull(tblGift.lngTriggerID), 0, tblGift.lngTriggerID) AS lngTriggerID, tblGift.lngPaymentTypeID, tblGift.lngBillStateID, IIf(IsNull(tblGift.lngGiftWebID), 0, tblGift.lngGiftWebID) AS lngGiftWebID, tblRecords.lngRecordWebID, " +
                                        "CStr(IIf(IsNull(tblGift.dteGiftDate), \"\", tblGift.dteGiftDate)) AS dteGiftDate, CStr(IIf(IsNull(tblGift.dteDateEntered), \"\", tblGift.dteDateEntered)) AS dteDateEntered, " +
                                        "tblGift.curAmount, tblGift.curDeclaredValue, " +
                                        "IIf(IsNull(tblGift.strCheckNumber), \"\", tblGift.strCheckNumber) AS strCheckNumber, IIf(IsNull(tblGift.strMemorialName), \"\", tblGift.strMemorialName) AS strMemorialName, IIf(IsNull(tblGift.strInHonorOf), \"\", tblGift.strInHonorOf) AS strInHonorOf, IIf(IsNull(tblGift.strAcctNum), \"\", tblGift.strAcctNum) AS strAcctNum, IIf(IsNull(tblGift.strBankName), \"\", tblGift.strBankName) AS strBankName, IIf(IsNull(tblGift.strBillAddress), \"\", tblGift.strBillAddress) AS strBillAddress, IIf(IsNull(tblGift.strBillCity), \"\", tblGift.strBillCity) AS strBillCity, IIf(IsNull(tblGift.strBillName), \"\", tblGift.strBillName) AS strBillName, IIf(IsNull(tblGift.strBillPhone), \"\", tblGift.strBillPhone) AS strBillPhone, IIf(IsNull(tblGift.strBillZip), \"\", tblGift.strBillZip) AS strBillZip, IIf(IsNull(tblGift.strCCExpDate), \"\", tblGift.strCCExpDate) AS strCCExpDate, IIf(IsNull(tblGift.strCCNumber), \"\", tblGift.strCCNumber) AS strCCNumber, IIf(IsNull(tblGift.strCCValCode), \"\", tblGift.strCCValCode) AS strCCValCode, tblGift.strEFTFile, IIf(IsNull(tblGift.strRoutingNum), \"\", tblGift.strRoutingNum) AS strRoutingNum, " +
                                        "IIf(IsNull(tblGift.mmoNotes), \"\", tblGift.mmoNotes) AS mmoNotes, IIf(IsNull(tblGift.mmoExternalNotes), \"\", tblGift.mmoExternalNotes) AS mmoExternalNotes " +
                                    "FROM (tblRecords " +
                                        "INNER JOIN tblGift ON tblRecords.lngRecordID = tblGift.lngRecordID) " +
                                        "INNER JOIN tblIDHolder ON tblGift.lngGiftID = tblIDHolder.lngID " +
                                    "ORDER BY tblIDHolder.lngID";

                            if (intCurrentPass * 50 > intRecordCount)
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Gifts " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + intRecordCount.ToString() + " of " + intRecordCount.ToString()));
                            else
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Gifts " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + (intCurrentPass * 50).ToString() + " of " + intRecordCount.ToString()));
                            Application.DoEvents();

                            strRes += clsWebTalk.fcnUploadData(strSQL, "tblGift", "lngGiftID", "lngGiftWebID", "spAppendGift", false);

                            strSQL = "DELETE * " +
                                    "FROM tblIDHolder " +
                                    "WHERE tblIDHolder.lngID IN " +
                                        "(SELECT TOP 50 tblGift.lngGiftID " +
                                        "FROM (tblRecords " +
                                            "INNER JOIN tblGift ON tblRecords.lngRecordID = tblGift.lngRecordID) " +
                                            "INNER JOIN tblIDHolder ON tblGift.lngGiftID = tblIDHolder.lngID " +
                                        "ORDER BY tblIDHolder.lngID)";

                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }

            return strRes;
        }

        private string fcnULPledges()
        {
            //add ids to tbl to upload
            //get count
            //loop through and upload 50 at a time            

            string strSQL = "";
            string strRes = "";
           
            strSQL = "DELETE * FROM tblIDHolder";

            int intRecordCount = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();

                        strSQL = "INSERT INTO tblIDHolder " +
                                "(lngID) " +
                                "SELECT lngPledgeID " +
                                "FROM tblPledge " +
                                    "INNER JOIN tblRecords ON tblPledge.lngRecordID = tblRecords.lngRecordID " +
                                "WHERE tblRecords.blnDonor=True AND " +
                                    "tblRecords.strEmail<>\"\" AND " +
                                    "tblRecords.strEmail Is Not Null";

                        cmdDB.CommandText = strSQL;

                        cmdDB.ExecuteNonQuery();

                        strSQL = "SELECT COUNT(lngID) " +
                                "FROM tblIDHolder";

                        cmdDB.CommandText = strSQL;

                        try { intRecordCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { intRecordCount = 0; }

                        for (int intCurrentPass = 1; (intCurrentPass - 1) * 50 <= intRecordCount; intCurrentPass++)
                        {
                            //upload chunk
                            strSQL = "SELECT TOP 50 blnMoreAllowed, blnPledgeReminder, blnMemorial, blnInHonorOf, blnPledgeAutopay, " +
                                        "lngPledgeID, IIf(IsNull(lngPledgeWebID), 0, lngPledgeWebID) AS lngPledgeWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblPledge.lngRecordID, lngCampaignID, lngCategoryID, lngFrequencyID, lngTerm, IIf(IsNull(lngRepID), 0, lngRepID) AS lngRepID, IIf(IsNull(lngPaymentTypeID), 0, lngPaymentTypeID) AS lngPaymentTypeID, IIf(IsNull(lngBillStateID), 0, lngBillStateID) AS lngBillStateID, " +
                                        "dtePledgeDate, " +
                                        "curPeriodAmt, curPledgeAmount, " +
                                        "IIf(IsNull(strMemorialName), \"\", strMemorialName) AS strMemorialName, IIf(IsNull(strInHonorOf), \"\", strInHonorOf) AS strInHonorOf, IIf(IsNull(strAcctNum), \"\", strAcctNum) AS strAcctNum, IIf(IsNull(strBankName), \"\", strBankName) AS strBankName, IIf(IsNull(strBillAddress), \"\", strBillAddress) AS strBillAddress, IIf(IsNull(strBillCity), \"\", strBillCity) AS strBillCity, IIf(IsNull(strBillName), \"\", strBillName) AS strBillName, IIf(IsNull(strBillPhone), \"\", strBillPhone) AS strBillPhone, IIf(IsNull(strBillZip), \"\", strBillZip) AS strBillZip, IIf(IsNull(strCCExpDate), \"\", strCCExpDate) AS strCCExpDate, IIf(IsNull(strCCNumber), \"\", strCCNumber) AS strCCNumber, IIf(IsNull(strCCValCode), \"\", strCCValCode) AS strCCValCode, IIf(IsNull(strRoutingNum), \"\", strRoutingNum) AS strRoutingNum, " +
                                        "IIf(IsNull(mmoPledgeNotes), \"\", mmoPledgeNotes) AS mmoPledgeNotes " +
                                    "FROM (tblPledge " +
                                        "INNER JOIN tblRecords ON tblPledge.lngRecordID = tblRecords.lngRecordID) " +
                                        "INNER JOIN tblIDHolder ON tblPledge.lngPledgeID = tblIDHolder.lngID " +
                                    "ORDER BY tblIDHolder.lngID";

                            if (intCurrentPass * 50 > intRecordCount)
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Pledges " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + intRecordCount.ToString() + " of " + intRecordCount.ToString()));
                            else
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Pledges " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + (intCurrentPass * 50).ToString() + " of " + intRecordCount.ToString()));
                            Application.DoEvents();

                            strRes += clsWebTalk.fcnUploadData(strSQL, "tblPledge", "lngPledgeID", "lngPledgeWebID", "spAppendPledge", false);

                            strSQL = "DELETE * " +
                                    "FROM tblIDHolder " +
                                    "WHERE tblIDHolder.lngID IN " +
                                        "(SELECT TOP 50 lngPledgeID " +
                                        "FROM (tblPledge " +
                                            "INNER JOIN tblRecords ON tblPledge.lngRecordID = tblRecords.lngRecordID) " +
                                            "INNER JOIN tblIDHolder ON tblPledge.lngPledgeID = tblIDHolder.lngID " +
                                        "ORDER BY tblIDHolder.lngID)";

                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }

            return strRes;
        }



        private string fcnULPledgePayments()
        {
            //add ids to tbl to upload
            //get count
            //loop through and upload 50 at a time            

            string strSQL = "";
            string strRes = "";

            strSQL = "DELETE * FROM tblIDHolder";

            int intRecordCount = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();

                        strSQL = "INSERT INTO tblIDHolder " +
                                "(lngID) " +
                                "SELECT lngPledgePaymentID " +
                                "FROM (tblRecords " +
                                    "INNER JOIN tblPledge ON tblRecords.lngRecordID = tblPledge.lngRecordID) " +
                                    "INNER JOIN tblPledgePayments ON tblPledge.lngPledgeID = tblPledgePayments.lngPledgeID " +
                                "WHERE tblRecords.blnDonor = True AND " +
                                    "tblRecords.strEmail <> \"\" AND " +
                                    "tblRecords.strEmail Is Not Null";

                        cmdDB.CommandText = strSQL;

                        cmdDB.ExecuteNonQuery();

                        strSQL = "SELECT COUNT(lngID) " +
                                "FROM tblIDHolder";

                        cmdDB.CommandText = strSQL;

                        try { intRecordCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { intRecordCount = 0; }

                        for (int intCurrentPass = 1; (intCurrentPass - 1) * 50 <= intRecordCount; intCurrentPass++)
                        {
                            //upload chunk
                            strSQL = "SELECT TOP 50 IIf(IsNull(lngPledgePaymentWebID), 0, lngPledgePaymentWebID) AS lngPledgePaymentWebID, lngPledgePaymentID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblPledgePayments.lngPledgeID, lngGiftID, " +
                                        "curScheduledAmt, curPaidAmt, " +
                                        "dteScheduledDate, IIf(IsNull(tblPledgePayments.dtePaidDate), #9/29/76#, tblPledgePayments.dtePaidDate) AS dtePaidDate " +
                                    "FROM ((tblRecords " +
                                        "INNER JOIN tblPledge ON tblRecords.lngRecordID = tblPledge.lngRecordID) " +
                                        "INNER JOIN tblPledgePayments ON tblPledge.lngPledgeID = tblPledgePayments.lngPledgeID) " +
                                        "INNER JOIN tblIDHolder ON tblPledgePayments.lngPledgePaymentID = tblIDHolder.lngID " +
                                    "ORDER BY tblIDHolder.lngID";

                            if (intCurrentPass * 50 > intRecordCount)
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Pledge Payments " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + intRecordCount.ToString() + " of " + intRecordCount.ToString()));
                            else
                                lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Pledge Payments " + (((intCurrentPass - 1) * 50) + 1).ToString() + " to " + (intCurrentPass * 50).ToString() + " of " + intRecordCount.ToString()));
                            Application.DoEvents();

                            strRes += clsWebTalk.fcnUploadData(strSQL, "tblPledgePayments", "lngPledgePaymentID", "lngPledgePaymentWebID", "spAppendPledgePmt", false);

                            strSQL = "DELETE * " +
                                    "FROM tblIDHolder " +
                                    "WHERE tblIDHolder.lngID IN " +
                                        "(SELECT TOP 50 lngPledgePaymentID " +
                                        "FROM ((tblRecords " +
                                            "INNER JOIN tblPledge ON tblRecords.lngRecordID = tblPledge.lngRecordID) " +
                                            "INNER JOIN tblPledgePayments ON tblPledge.lngPledgeID = tblPledgePayments.lngPledgeID) " +
                                            "INNER JOIN tblIDHolder ON tblPledgePayments.lngPledgePaymentID = tblIDHolder.lngID " +
                                        "ORDER BY tblIDHolder.lngID)";

                            cmdDB.CommandText = strSQL;

                            cmdDB.ExecuteNonQuery();
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }

            return strRes;
        }

        private void btnUL_Click(object sender, EventArgs e)
        {
            string strULRes = "";
            string strMsg = "";

            strMsg = "Uploading giving history will update each donor's online profile with the local CampTrak data.\n" +
                    "Please make sure there are no online donations or event registrations waiting to be downloaded before continuing.\n" +
                    "\n" +
                    "Do you wish to continue?";

            if (MessageBox.Show(strMsg, "Continue?", System.Windows.Forms.MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            //ul donors
            strULRes += fcnULDonors();

            //ul gifts
            strULRes += fcnULGifts();

            //ul pledges
            strULRes += fcnULPledges();

            //ul pmts
            strULRes += fcnULPledgePayments();

            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Finished uploading donors and their giving history."));
            Application.DoEvents();

            if (MessageBox.Show("Completed uploading giving history.\n\n" + "Would you like to set up a batch e-mail invite?", "Send Invite?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clsNav.subCloseULGivingHistory();
                clsNav.subShowBatchDonorInvite(clsNav.objSwitchboard);
            }
        }
    }
}