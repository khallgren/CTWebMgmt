using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;
using System.IO;

namespace CTWebMgmt.GGCC
{
    public partial class frmDLEvents : Form
    {
        public frmDLEvents()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseDLEvents();
        }

        private void btnDL_Click(object sender, EventArgs e)
        {
            //download records, registrations, attendees, and activities that are not marked as 'retrieved'.
            wsXferEventInfo.XferEventInfo wsDLReg;

            string strSQL;
            string[,] strAppendResXML = new string[5, 2];

            long lngCTUserID = clsAppSettings.GetAppSettings().lngCTUserID;

            int intRegCount = 0;

            try
            {
                intRegCount = clsGGCCCRUD.fcnGGCCRegWebCount();

                lstStatus.Items.Add("Downloading Event Registrations...");

                wsDLReg = new wsXferEventInfo.XferEventInfo();

                //clear errors from previous download
                wsDLReg.fcnClearErrs(lngCTUserID, clsWebTalk.strWebConn, "tblGGCCRegistrations");

                //get contacts (tblRecords)
                lstStatus.Items.Add("Getting new contacts");

                Application.DoEvents();

                strSQL = "SELECT tblRecords.blnGender, " +
                            "tblRecords.lngRecordWebID, tblRecords.lngRecordID, tblRecords.lngStateID, tblRecords.lngCountryID, " +
                            "tblRecords.strLastCoName, tblRecords.strFirstName, tblRecords.strCompanyName, tblRecords.strEmail, tblRecords.strAddress, tblRecords.strZip, tblRecords.strWorkExt, tblRecords.strWorkPhone, tblRecords.strCellPhone, tblRecords.strCity, tblRecords.strHomePhone " +
                        "FROM tblGGCCRegistrations " +
                            "INNER JOIN tblRecords ON tblGGCCRegistrations.lngRecordWebID = tblRecords.lngRecordWebID " +
                        "WHERE tblGGCCRegistrations.blnRetrieved = 0 AND " +
                            "tblGGCCRegistrations.lngCTUserID = " + lngCTUserID + " AND " +
                            "tblGGCCRegistrations.dteDateRegistered IS NOT NULL " +
                        "GROUP BY tblRecords.blnGender, " +
                            "tblRecords.lngRecordWebID, tblRecords.lngRecordID, tblRecords.lngStateID, tblRecords.lngCountryID, tblGGCCRegistrations.lngGGCCRegistrationWebID, " +
                            "tblRecords.strLastCoName, tblRecords.strFirstName, tblRecords.strCompanyName, tblRecords.strEmail, tblRecords.strAddress, tblRecords.strZip, tblRecords.strWorkExt, tblRecords.strWorkPhone, tblRecords.strCellPhone, tblRecords.strCity, tblRecords.strHomePhone;";

                strAppendResXML[0, 0] = wsDLReg.fcnGetRecords(strSQL, "tblRecords", clsWebTalk.strWebConn);

                //get registrations (tblGGCCRegistrations)
                lstStatus.Items.Add("Getting new registrations");

                Application.DoEvents();

                strSQL = "SELECT blnCustomGGCCRegFlag1, blnCustomGGCCRegFlag2, blnCustomGGCCRegFlag3, blnCustomGGCCRegFlag4, blnCustomGGCCRegFlag5, blnCustomGGCCRegFlag6, blnCustomGGCCRegFlag7, blnCustomGGCCRegFlag8, blnCustomGGCCRegFlag9, blnCustomGGCCRegFlag10, blnCustomGGCCRegFlag11, blnCustomGGCCRegFlag12, blnCustomGGCCRegFlag13, blnCustomGGCCRegFlag14, blnCustomGGCCRegFlag15, " +
                            "lngGGCCRegistrationWebID, lngGGCCRegistrationID, lngRegPrompt, lngGGCCID, lngRecordWebID, " +
                            "curDeposit, " +
                            "dblDiscount, " +
                            "dteDateRegistered, dteLastModified, " +
                            "strPaymentType, strBankName, strAcctNum, strRoutingNum, strCardType, strCardNum, strCVV2, strCCExp, strPNRef, strVancoCustRef, strVancoPmtMethID, strXCAlias, strXCTransID, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID " +
                        "FROM tblGGCCRegistrations " +
                        "WHERE blnRetrieved = 0 AND " +
                            "lngCTUserID = " + lngCTUserID.ToString() + " AND " +
                            "dteDateRegistered IS NOT NULL";

                strAppendResXML[1, 0] = wsDLReg.fcnGetRecords(strSQL, "tblGGCCRegistrations", clsWebTalk.strWebConn);

                //get attendees
                lstStatus.Items.Add("Getting new attendees");

                Application.DoEvents();

                strSQL = "SELECT tblGGCCRegAttendees.intGender, " +
                            "tblGGCCRegistrations.lngGGCCRegistrationWebID, tblGGCCRegAttendees.lngGGCCRegAttendeeWebID, tblGGCCRegAttendees.lngGGCCRegAttendeeID, tblGGCCRegAttendees.lngGGCCAttendeeStatsID, tblGGCCRegAttendees.lngGuestTypeID, " +
                            "tblGGCCRegAttendees.dteDOB, "+
                            "tblGGCCRegAttendees.curRate, " +
                            "tblGGCCRegAttendees.strFName, tblGGCCRegAttendees.strLName " +
                        "FROM tblGGCCRegistrations " +
                            "INNER JOIN tblGGCCRegAttendees ON tblGGCCRegistrations.lngGGCCRegistrationWebID = tblGGCCRegAttendees.lngGGCCRegistrationWebID " +
                        "WHERE tblGGCCRegistrations.blnRetrieved = 0 AND " +
                            "tblGGCCRegistrations.lngCTUserID = " + lngCTUserID + " AND " +
                            "tblGGCCRegistrations.dteDateRegistered IS NOT NULL";

                strAppendResXML[2, 0] = wsDLReg.fcnGetRecords(strSQL, "tblGGCCRegAttendees", clsWebTalk.strWebConn);

                //get activities
                lstStatus.Items.Add("Getting new activities");

                Application.DoEvents();

                strSQL = "SELECT tblGGCCRegistrations.lngGGCCRegistrationWebID, tblGGCCRegActivities.lngGGCCRegActivityWebID, tblGGCCRegActivities.lngGGCCRegActivityID, tblGGCCRegActivities.intParticipants, tblGGCCRegActivities.lngGGCCActivityID, tblGGCCRegActivities.lngGGCCPackageID " +
                        "FROM tblGGCCRegistrations " +
                            "INNER JOIN tblGGCCRegActivities ON tblGGCCRegistrations.lngGGCCRegistrationWebID = tblGGCCRegActivities.lngGGCCRegistrationWebID " +
                        "WHERE tblGGCCRegistrations.blnRetrieved = 0 AND " +
                            "tblGGCCRegistrations.lngCTUserID = " + lngCTUserID + " AND " +
                            "tblGGCCRegistrations.dteDateRegistered IS NOT NULL";

                strAppendResXML[3, 0] = wsDLReg.fcnGetRecords(strSQL, "tblGGCCRegActivities", clsWebTalk.strWebConn);

                //get housing requests
                lstStatus.Items.Add("Getting new housing requests");

                Application.DoEvents();

                strSQL = "SELECT tblGGCCRegistrations.lngGGCCRegistrationWebID, tblGGCCRegHousingRequests.lngGGCCRegHousingRequestID, tblGGCCRegHousingRequests.lngHousingID, tblGGCCRegHousingRequests.lngCount, " +
                            "tblGGCCRegHousingRequests.curCabinCharge AS curCharge " +
                        "FROM tblGGCCRegHousingRequests " +
                            "INNER JOIN tblGGCCRegistrations ON tblGGCCRegHousingRequests.lngGGCCRegistrationWebID = tblGGCCRegistrations.lngGGCCRegistrationWebID " +
                        "WHERE tblGGCCRegistrations.lngCTUserID = " + lngCTUserID + " AND " +
                            "tblGGCCRegistrations.blnRetrieved = 0 AND " +
                            "tblGGCCRegistrations.dteDateRegistered IS NOT NULL";

                strAppendResXML[4, 0] = wsDLReg.fcnGetRecords(strSQL, "tblGGCCRegHousingRequests", clsWebTalk.strWebConn);

                //add contacts, get result xml

                strAppendResXML[1, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebGGCCRegistrations", strAppendResXML[1, 0], "lngGGCCRegistrationWebID", true, "lngGGCCRegistrationWebID");
                lstStatus.Items.Add("Adding new registrations");

                strAppendResXML[0, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebRecordsGGCCReg", strAppendResXML[0, 0], "lngRecordWebID", false, "lngGGCCRegistrationWebID");
                lstStatus.Items.Add("Adding new contacts");

                strAppendResXML[2, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebGGCCRegAttendees", strAppendResXML[2, 0], "lngGGCCRegAttendeeWebID", false, "lngGGCCRegistrationWebID");
                lstStatus.Items.Add("Adding new attendees");

                strAppendResXML[3, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebGGCCRegActivities", strAppendResXML[3, 0], "lngGGCCRegActivityWebID", false, "lngGGCCRegistrationWebID");
                lstStatus.Items.Add("Adding new activities");

                strAppendResXML[4, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebGGCCRegHousingRequests", strAppendResXML[4, 0], "lngGGCCRegHousingRequestID", false, "lngGGCCRegistrationWebID");
                lstStatus.Items.Add("Adding new housing");

                lstStatus.Items.Add("Posting download results to server");

                //send xml back to server
                if (wsDLReg.fcnDLRes(strAppendResXML[0, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Add("Error posting contact results");
                if (wsDLReg.fcnDLRes(strAppendResXML[1, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Add("Error posting registration results");
                if (wsDLReg.fcnDLRes(strAppendResXML[2, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Add("Error posting attendee results");
                if (wsDLReg.fcnDLRes(strAppendResXML[3, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Add("Error posting activity results");
                if (wsDLReg.fcnDLRes(strAppendResXML[4, 1], clsWebTalk.strWebConn) != "")
                    lstStatus.Items.Add("Error posting housing results");

                //update dl status on server
                //wsDLReg.fcnUpdateDLStatus(lngCTUserID, clsWebTalk.strWebConn);

                wsDLReg.Dispose();

                intRegCount = clsGGCCCRUD.fcnGGCCRegWebCount() - intRegCount;

                lstStatus.Items.Add("Successfully downloaded and added " + intRegCount + " new event registrations");
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("btnDL_Click", ex);
            }

            wsDLReg = null;

        }
    }
}