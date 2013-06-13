using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml;
using CTWebMgmt.Properties;


namespace CTWebMgmt.GGCC
{
    public partial class frmUploadEvents : Form
    {
        public frmUploadEvents()
        {
            InitializeComponent();
        }

        private void btnClose_Click(Object sender, EventArgs e)
        {
            clsNav.subCloseUploadEvents();
        }

        private void subChooseEvent(Object sender, EventArgs e)
        {
            cboSpecificEvent.Visible = false;

            if (radSpecificEvent.Checked) cboSpecificEvent.Visible = true;

        }

        private void btnUpload_Click(Object sender, EventArgs e)
        {
            string strSQL;
            string strGroupBy;
            string strULRes = "";

            try
            {
                //validate form
                if (radSpecificEvent.Checked)
                {
                    if (cboSpecificEvent.SelectedIndex < 0)
                    {
                        MessageBox.Show("Please select a specific event.");
                        cboSpecificEvent.Focus();
                        return;
                    }

                }

                if (txtStartDate.Text != "")
                {

                    if (!CTWebMgmt.IsDate(txtStartDate.Text))
                    {
                        MessageBox.Show("Please enter a valid start date.");
                        txtStartDate.Focus();
                        return;
                    }
                    else if (!CTWebMgmt.IsDate(txtEndDate.Text))
                    {
                        MessageBox.Show("Please enter a valid end date.");
                        txtEndDate.Focus();
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;

                StringBuilder stbCustom = new StringBuilder();

                for (int intI = 1; intI <= 15; intI++)
                {
                    stbCustom.Append("IIf(IsNull(tblGGCC.strCustomGGCC" + intI.ToString() + "), \"\", tblGGCC.strCustomGGCC" + intI.ToString() + ") AS strCustomGGCC" + intI.ToString() + ", ");
                }

                //events
                strSQL = "SELECT tblGGCC.blnConvBedUsage, tblGGCC.blnDoNotRate, tblGGCC.blnGGCCFlag1, tblGGCC.blnGGCCFlag10, tblGGCC.blnGGCCFlag11, tblGGCC.blnGGCCFlag12, tblGGCC.blnGGCCFlag13, tblGGCC.blnGGCCFlag14, tblGGCC.blnGGCCFlag15, tblGGCC.blnGGCCFlag2, tblGGCC.blnGGCCFlag3, tblGGCC.blnGGCCFlag4, tblGGCC.blnGGCCFlag5, tblGGCC.blnGGCCFlag6, tblGGCC.blnGGCCFlag7, tblGGCC.blnGGCCFlag8, tblGGCC.blnGGCCFlag9, tblGGCC.blnLinens, tblGGCC.blnChargeRegHousing, tblGGCC.blnLockEvent, tblGGCC.blnMinDepPercent, " +
                            "tblGGCC.curDepositAmt, " +
                            "tblGGCC.dblDiscount, " +
                            "CStr(IIf(IsNull(tblGGCC.dteDateCreated), \"\", tblGGCC.dteDateCreated)) AS dteDateCreated, CStr(IIf(IsNull(tblGGCC.dteDateModified), \"\", tblGGCC.dteDateModified)) AS dteDateModified, CStr(IIf(IsNull(tblGGCC.dteCheckInTime), \"\", tblGGCC.dteCheckInTime)) AS dteCheckInTime, CStr(IIf(IsNull(tblGGCC.dteCheckOutTime), \"\", tblGGCC.dteCheckOutTime)) AS dteCheckOutTime, CStr(IIf(IsNull(tblGGCC.dteContractDue), \"\", tblGGCC.dteContractDue)) AS dteContractDue, CStr(IIf(IsNull(tblGGCC.dteContractRcvd), \"\", tblGGCC.dteContractRcvd)) AS dteContractRcvd, CStr(IIf(IsNull(tblGGCC.dteContractSent), \"\", tblGGCC.dteContractSent)) AS dteContractSent, CStr(IIf(IsNull(tblGGCC.dteEndDate), \"\", tblGGCC.dteEndDate)) AS dteEndDate, CStr(IIf(IsNull(tblGGCC.dteEndTime), \"\", tblGGCC.dteEndTime)) AS dteEndTime, CStr(IIf(IsNull(tblGGCC.dtePacketRcvd), \"\", tblGGCC.dtePacketRcvd)) AS dtePacketRcvd, CStr(IIf(IsNull(tblGGCC.dtePacketSent), \"\", tblGGCC.dtePacketSent)) AS dtePacketSent, CStr(IIf(IsNull(tblGGCC.dteStartDate), \"\", tblGGCC.dteStartDate)) AS dteStartDate, CStr(IIf(IsNull(tblGGCC.dteStartTime), \"\", tblGGCC.dteStartTime)) AS dteStartTime, CStr(IIf(IsNull(tblGGCC.dteCountDueDate), \"\", tblGGCC.dteCountDueDate)) AS dteCountDueDate, " +
                            "tblGGCC.intHousingValidation, tblGGCC.intConfHousingDisplay, tblGGCC.intCapacity, tblGGCC.intMaxRoomOcc, tblGGCC.intMultOccBedUsage, tblGGCC.intNumResPerRoom, " +
                            "0 AS lngGGCCWebID, tblGGCC.lngGGCCID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGGCC.lngBathTypeID, tblGGCC.lngUserCreated, tblGGCC.lngUserModified, tblGGCC.lngCCTypeID, tblGGCC.lngGroupStatusID, tblGGCC.lngGroupTypeID, tblGGCC.lngProgramTypeID, tblGGCC.lngRateStyleID, IIf(ISNULL(tblGGCC.lngRateTypeID), 0, tblGGCC.lngRateTypeID) AS lngRateTypeID, tblGGCC.lngSiteID, tblGGCC.lngTransactionID, tblGGCC.lngDepCalcMethod, " +
                            stbCustom.ToString() + "tblGGCC.strGGCCName, " +
                            "IIf(IsNull(tblGGCC.mmoGGCCNotes), \"\", tblGGCC.mmoGGCCNotes) AS mmoGGCCNotes, IIf(IsNull(tblGGCC.mmoRegConfComments), \"\", tblGGCC.mmoRegConfComments) AS mmoRegConfComments " +
                        "FROM tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "";

                lstStatus.Items.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Events...");
                lstStatus.Items.Insert(0, "Uploading Events...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblGGCC", "lngGGCCID", "lngGGCCWebID", "spAppendGGCC", chkClearExisting.Checked, "group events");

                //camp names
                strSQL = "SELECT tlkpCampName.blnLOOKUPTHISFIELDNAME, tlkpCampName.blnUpload, " +
                            "0 AS lngCampWebID, tlkpCampName.lngCampID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tlkpCampName.lngProgramTypeID, " +
                            "tlkpCampName.strCampName " +
                        "FROM tlkpCampName " +
                            "INNER JOIN tblGGCC ON tlkpCampName.lngCampID = tblGGCC.lngProgramTypeID ";

                strGroupBy = "GROUP BY tlkpCampName.blnLOOKUPTHISFIELDNAME, tlkpCampName.blnUpload, " +
                                "tlkpCampName.lngCampID, tlkpCampName.lngProgramTypeID, " +
                                "tlkpCampName.strCampName;";

                lstStatus.Items.Insert(0, "Uploading Camp Names...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tlkpCampName", "lngCampID", "lngCampWebID", "spAppendCampName", chkClearExisting.Checked, "camps");

                //sites
                strSQL = "SELECT 0 AS lngSiteWebID, tblSites.lngSiteID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tblSites.strSiteName " +
                        "FROM (tblGGCC " +
                            "INNER JOIN tblSites ON tblGGCC.lngSiteID = tblSites.lngSiteID) " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "GROUP BY tblSites.lngSiteID, " +
                                "tblSites.strSiteName;";

                lstStatus.Items.Insert(0, "Uploading Sites...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblSites", "lngSiteID", "lngSiteWebID", "spAppendSite", chkClearExisting.Checked, "sites");

                //rate types
                strSQL = "SELECT 0 AS lngRateTypeWebID, tlkpRateTypes.lngRateTypeID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tlkpRateTypes.strRateType " +
                        "FROM (((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblnkGGCCAttendeeStats ON tblGGCC.lngGGCCID = tblnkGGCCAttendeeStats.lngGGCCID) " +
                            "INNER JOIN tblAdvRateTypes ON tblnkGGCCAttendeeStats.lngAdvRateTypeID = tblAdvRateTypes.lngAdvRateTypeID) " +
                            "INNER JOIN tlkpRateTypes ON tblAdvRateTypes.lngRateTypeID = tlkpRateTypes.lngRateTypeID ";

                strGroupBy = "GROUP BY tlkpRateTypes.lngRateTypeID, " +
                                "tlkpRateTypes.strRateType;";

                lstStatus.Items.Insert(0, "Uploading Rate Types...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tlkpRateTypes", "lngRateTypeID", "lngRateTypeWebID", "spAppendRateType", chkClearExisting.Checked, "rate types");

                //cabin categories
                strSQL = "SELECT 0 AS lngCabinCategoryWebID, tblCabinCategories.lngCabinCategoryID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tblCabinCategories.strCabinCategory, tblCabinCategories.strCategoryDesc " +
                        "FROM (((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tlkpRateTypes ON tblGGCC.lngRateTypeID = tlkpRateTypes.lngRateTypeID) " +
                            "INNER JOIN tblAdvRateTypes ON tlkpRateTypes.lngRateTypeID = tblAdvRateTypes.lngRateTypeID) " +
                            "INNER JOIN tblCabinCategories ON tblCabinCategories.lngCabinCategoryID = tblAdvRateTypes.lngCabinCategoryID ";

                strGroupBy = "GROUP BY tblCabinCategories.lngCabinCategoryID, tblCabinCategories.lngCabinCategoryWebID, " +
                                    "tblCabinCategories.strCabinCategory, tblCabinCategories.strCategoryDesc;";

                lstStatus.Items.Insert(0, "Uploading Cabin Categories...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblCabinCategories", "lngCabinCategoryID", "lngCabinCategoryWebID", "spAppendCabinCategory", chkClearExisting.Checked, "cabin categories");

                //adv rate categories
                strSQL = "SELECT tblAdvRateCat.lngAdvRateCatID, 0 AS lngAdvRateCatWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tblAdvRateCat.strAdvRateCat " +
                        "FROM (((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tlkpRateTypes ON tblGGCC.lngRateTypeID = tlkpRateTypes.lngRateTypeID) " +
                            "INNER JOIN tblAdvRateTypes ON tlkpRateTypes.lngRateTypeID = tblAdvRateTypes.lngRateTypeID) " +
                            "INNER JOIN tblAdvRateCat ON tblAdvRateTypes.lngAdvRateCatID = tblAdvRateCat.lngAdvRateCatID ";

                strGroupBy = "GROUP BY tblAdvRateCat.lngAdvRateCatID, tblAdvRateCat.lngAdvRateCatWebID, " +
                                    "tblAdvRateCat.strAdvRateCat;";

                lstStatus.Items.Insert(0, "Uploading Advanced Rate Categories...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblAdvRateCat", "lngAdvRateCatID", "lngAdvRateCatWebID", "spAppendAdvRateCat", chkClearExisting.Checked, "advanced rate categories");

                //guest types
                strSQL = "SELECT tlkpGuestTypes.lngGuestTypeID, 0 AS lngGuestTypeWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tlkpGuestTypes.curPricePerHead, " +
                            "tlkpGuestTypes.strGuestType " +
                        "FROM (((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tlkpRateTypes ON tblGGCC.lngRateTypeID = tlkpRateTypes.lngRateTypeID) " +
                            "INNER JOIN tblAdvRateTypes ON tlkpRateTypes.lngRateTypeID = tblAdvRateTypes.lngRateTypeID) " +
                            "INNER JOIN tlkpGuestTypes ON tblAdvRateTypes.lngGuestTypeID = tlkpGuestTypes.lngGuestTypeID ";

                strGroupBy = "GROUP BY tlkpGuestTypes.lngGuestTypeID, tlkpGuestTypes.lngGuestTypeWebID, " +
                                    "tlkpGuestTypes.curPricePerHead, " +
                                    "tlkpGuestTypes.strGuestType;";

                lstStatus.Items.Insert(0, "Uploading Guest Types...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tlkpGuestTypes", "lngGuestTypeID", "lngGuestTypeWebID", "spAppendGuestType", chkClearExisting.Checked, "guest types");

                //adv rate types
                strSQL = "SELECT tblAdvRateTypes.blnDaily, " +
                            "tblAdvRateTypes.lngAdvRateTypeID, 0 AS lngAdvRateTypeWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblAdvRateTypes.lngAdvRateCatID, tblAdvRateTypes.lngCabinCategoryID, tblAdvRateTypes.lngGuestTypeID, tblAdvRateTypes.lngProgramTypeID, tblAdvRateTypes.lngRateTypeID, " +
                            "tblAdvRateTypes.curRate, " +
                            "IIf(ISNULL(tblAdvRateTypes.strDesc), \"\", tblAdvRateTypes.strDesc) AS strDesc " +
                        "FROM ((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tlkpRateTypes ON tblGGCC.lngRateTypeID = tlkpRateTypes.lngRateTypeID) " +
                            "INNER JOIN tblAdvRateTypes ON tlkpRateTypes.lngRateTypeID = tblAdvRateTypes.lngRateTypeID ";

                strGroupBy = "GROUP BY tblAdvRateTypes.blnDaily, " +
                                    "tblAdvRateTypes.lngAdvRateTypeID, tblAdvRateTypes.lngAdvRateTypeWebID, tblAdvRateTypes.lngAdvRateCatID, tblAdvRateTypes.lngCabinCategoryID, tblAdvRateTypes.lngGuestTypeID, tblAdvRateTypes.lngProgramTypeID, tblAdvRateTypes.lngRateTypeID, tblAdvRateTypes.curRate, " +
                                    "IIf(ISNULL(tblAdvRateTypes.strDesc), \"\", tblAdvRateTypes.strDesc);";

                lstStatus.Items.Insert(0, "Uploading Advanced Rate Types...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblAdvRateTypes", "lngAdvRateTypeID", "lngAdvRateTypeWebID", "spAppendAdvRateType", chkClearExisting.Checked, "advanced rate types");

                //attendee stats
                strSQL = "SELECT tblnkGGCCAttendeeStats.intCount, tblnkGGCCAttendeeStats.intCountEst, " +
                            "tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID, 0 AS lngGGCCAttendeeStatsWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblnkGGCCAttendeeStats.lngAdvRateTypeID, tblnkGGCCAttendeeStats.lngGGCCID, tblnkGGCCAttendeeStats.lngGuestTypeID, tblnkGGCCAttendeeStats.lngRateUsageID, " +
                            "tblnkGGCCAttendeeStats.curRate, " +
                            "IIf(ISNULL(tblnkGGCCAttendeeStats.strDesc), \"\", tblnkGGCCAttendeeStats.strDesc) AS strDesc " +
                        "FROM (tblGGCC " +
                            "INNER JOIN tblnkGGCCAttendeeStats ON tblGGCC.lngGGCCID = tblnkGGCCAttendeeStats.lngGGCCID) " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "GROUP BY tblnkGGCCAttendeeStats.intCount, tblnkGGCCAttendeeStats.intCountEst, " +
                            "tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID, tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsWebID, tblnkGGCCAttendeeStats.lngAdvRateTypeID, tblnkGGCCAttendeeStats.lngGGCCID, tblnkGGCCAttendeeStats.lngGuestTypeID, tblnkGGCCAttendeeStats.lngRateUsageID, " +
                            "tblnkGGCCAttendeeStats.curRate, " +
                            "IIf(ISNULL(tblnkGGCCAttendeeStats.strDesc), \"\", tblnkGGCCAttendeeStats.strDesc);";

                lstStatus.Items.Insert(0, "Uploading Attendee Stats...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblnkGGCCAttendeeStats", "lngGGCCAttendeeStatsID", "lngGGCCAttendeeStatsWebID", "spAppendGGCCAttStat", chkClearExisting.Checked, "attendee stats");

                //housing areas
                strSQL = "SELECT tlkpHousingName.lngHousingID, 0 AS lngHousingWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tlkpHousingName.lngSiteID, tlkpHousingName.lngCampCenterID, " +
                            "tlkpHousingName.strHousingName, tlkpHousingName.strHousingDesc " +
                        "FROM (((tlkpHousingName " +
                            "INNER JOIN tlkpCabinNames ON tlkpHousingName.lngHousingID = tlkpCabinNames.lngHousingID) " +
                            "INNER JOIN tblGGCCHousing ON tlkpCabinNames.lngCabinID = tblGGCCHousing.lngCabinID) " +
                            "INNER JOIN tblGGCC ON tblGGCCHousing.lngGGCCID = tblGGCC.lngGGCCID) " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "GROUP BY tlkpHousingName.lngHousingID, tlkpHousingName.lngSiteID, tlkpHousingName.lngCampCenterID, " +
                                "tlkpHousingName.strHousingName, tlkpHousingName.strHousingDesc;";

                lstStatus.Items.Insert(0, "Uploading Housing Areas...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tlkpHousingName", "lngHousingID", "lngHousingWebID", "spAppendHousing", chkClearExisting.Checked, "housing areas");

                //cabin names
                strSQL = "SELECT tlkpCabinNames.blnLinens, tlkpCabinNames.blnFlag1, tlkpCabinNames.blnFlag10, tlkpCabinNames.blnFlag11, tlkpCabinNames.blnFlag12, tlkpCabinNames.blnFlag13, tlkpCabinNames.blnFlag14, tlkpCabinNames.blnFlag15, tlkpCabinNames.blnFlag16, tlkpCabinNames.blnFlag17, tlkpCabinNames.blnFlag18, tlkpCabinNames.blnFlag19, tlkpCabinNames.blnFlag2, tlkpCabinNames.blnFlag20, tlkpCabinNames.blnFlag3, tlkpCabinNames.blnFlag4, tlkpCabinNames.blnFlag5, tlkpCabinNames.blnFlag6, tlkpCabinNames.blnFlag7, tlkpCabinNames.blnFlag8, tlkpCabinNames.blnFlag9, " +
                            "tlkpCabinNames.intNumofBeds, " +
                            "tlkpCabinNames.lngCabinID, 0 AS lngCabinWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tlkpCabinNames.lngHousingID, " +
                            "tlkpCabinNames.curCostPerBed, tlkpCabinNames.curCostPerNight, " +
                            "tlkpCabinNames.strCabinName, tlkpCabinNames.strField1, tlkpCabinNames.strField10, tlkpCabinNames.strField11, tlkpCabinNames.strField12, tlkpCabinNames.strField13, tlkpCabinNames.strField14, tlkpCabinNames.strField15, tlkpCabinNames.strField16, tlkpCabinNames.strField17, tlkpCabinNames.strField18, tlkpCabinNames.strField19, tlkpCabinNames.strField2, tlkpCabinNames.strField20, tlkpCabinNames.strField3, tlkpCabinNames.strField4, tlkpCabinNames.strField5, tlkpCabinNames.strField6, tlkpCabinNames.strField7, tlkpCabinNames.strField8, tlkpCabinNames.strField9, IIf(IsNull(tlkpCabinNames.strPhoneExt), \"\", tlkpCabinNames.strPhoneExt) AS strPhoneExt, tlkpCabinNames.strQualityCode, " +
                            "IIf(ISNULL(tlkpCabinNames.mmoCabinNotes), \"\", tlkpCabinNames.mmoCabinNotes) AS mmoCabinNotes " +
                        "FROM ((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCHousing ON tblGGCC.lngGGCCID = tblGGCCHousing.lngGGCCID) " +
                            "INNER JOIN tlkpCabinNames ON tblGGCCHousing.lngCabinID = tlkpCabinNames.lngCabinID ";

                strGroupBy = "GROUP BY tlkpCabinNames.blnLinens, tlkpCabinNames.blnFlag1, tlkpCabinNames.blnFlag10, tlkpCabinNames.blnFlag11, tlkpCabinNames.blnFlag12, tlkpCabinNames.blnFlag13, tlkpCabinNames.blnFlag14, tlkpCabinNames.blnFlag15, tlkpCabinNames.blnFlag16, tlkpCabinNames.blnFlag17, tlkpCabinNames.blnFlag18, tlkpCabinNames.blnFlag19, tlkpCabinNames.blnFlag2, tlkpCabinNames.blnFlag20, tlkpCabinNames.blnFlag3, tlkpCabinNames.blnFlag4, tlkpCabinNames.blnFlag5, tlkpCabinNames.blnFlag6, tlkpCabinNames.blnFlag7, tlkpCabinNames.blnFlag8, tlkpCabinNames.blnFlag9, " +
                                "tlkpCabinNames.intNumofBeds, " +
                                "tlkpCabinNames.lngCabinID, tlkpCabinNames.lngHousingID, " +
                                "tlkpCabinNames.curCostPerBed, tlkpCabinNames.curCostPerNight, " +
                                "tlkpCabinNames.strCabinName, tlkpCabinNames.strField1, tlkpCabinNames.strField10, tlkpCabinNames.strField11, tlkpCabinNames.strField12, tlkpCabinNames.strField13, tlkpCabinNames.strField14, tlkpCabinNames.strField15, tlkpCabinNames.strField16, tlkpCabinNames.strField17, tlkpCabinNames.strField18, tlkpCabinNames.strField19, tlkpCabinNames.strField2, tlkpCabinNames.strField20, tlkpCabinNames.strField3, tlkpCabinNames.strField4, tlkpCabinNames.strField5, tlkpCabinNames.strField6, tlkpCabinNames.strField7, tlkpCabinNames.strField8, tlkpCabinNames.strField9, IIf(IsNull(tlkpCabinNames.strPhoneExt), \"\", tlkpCabinNames.strPhoneExt), tlkpCabinNames.strQualityCode, " +
                                "IIf(ISNULL(tlkpCabinNames.mmoCabinNotes), \"\", tlkpCabinNames.mmoCabinNotes);";

                lstStatus.Items.Insert(0, "Uploading Cabin Names...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tlkpCabinNames", "lngCabinID", "lngCabinWebID", "spAppendCabin", chkClearExisting.Checked, "cabin names");

                //bath types
                strSQL = "SELECT tblBathTypes.lngBathTypeID, 0 AS lngBathTypeWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tblBathTypes.strBathType " +
                        "FROM (((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCHousing ON tblGGCC.lngGGCCID = tblGGCCHousing.lngGGCCID) " +
                            "INNER JOIN tblCabinConfigurations ON tblGGCCHousing.lngCabinConfigurationID = tblCabinConfigurations.lngCabinConfigurationID) " +
                            "INNER JOIN tblBathTypes ON tblCabinConfigurations.lngBathTypeID = tblBathTypes.lngBathTypeID ";

                strGroupBy = "GROUP BY tblBathTypes.lngBathTypeID, tblBathTypes.lngBathTypeWebID, " +
                                "tblBathTypes.strBathType;";

                lstStatus.Items.Insert(0, "Uploading Bath Types...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblBathTypes", "lngBathTypeID", "lngBathTypeWebID", "spAppendBathType", chkClearExisting.Checked, "bath types");

                //reserved housing
                strSQL = "SELECT tblGGCCHousing.intMaxRoomOcc, " +
                            "tblGGCCHousing.lngGGCCHousingID, 0 AS lngGGCCHousingWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGGCCHousing.lngCabinConfigurationID, tblGGCCHousing.lngCabinID, tblGGCCHousing.lngGenderID, tblGGCCHousing.lngGGCCID, tblGGCCHousing.lngTransactionID, " +
                            "tblGGCCHousing.curCabinCharge, tblGGCCHousing.curChargePerBed " +
                        "FROM (tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCHousing ON tblGGCC.lngGGCCID = tblGGCCHousing.lngGGCCID ";

                strGroupBy = "GROUP BY tblGGCCHousing.intMaxRoomOcc, " +
                                "tblGGCCHousing.lngGGCCHousingID, tblGGCCHousing.lngGGCCHousingWebID, tblGGCCHousing.lngCabinConfigurationID, tblGGCCHousing.lngCabinID, tblGGCCHousing.lngGenderID, tblGGCCHousing.lngGGCCID, tblGGCCHousing.lngTransactionID, " +
                                "tblGGCCHousing.curCabinCharge, tblGGCCHousing.curChargePerBed;";

                strSQL = "SELECT tblGGCCHousing.intMaxRoomOcc, sqrySpotsTot.intSpotsTot, IIf(IsNull(sqrySpotsUsed.intSpotsUsed), 0, sqrySpotsUsed.intSpotsUsed) AS intSpotsUsed, IIf(IsNull(sqryRegCount.intRegCount), 0, sqryRegCount.intRegCount) AS intRegCount, " +
                            "tblGGCCHousing.lngGGCCHousingID, 0 AS lngGGCCHousingWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGGCCHousing.lngCabinConfigurationID, tblGGCCHousing.lngCabinID, tblGGCCHousing.lngGenderID, tblGGCCHousing.lngGGCCID, tblGGCCHousing.lngTransactionID, " +
                            "tblGGCCHousing.curCabinCharge, tblGGCCHousing.curChargePerBed " +
                        "FROM ((((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCHousing ON tblGGCC.lngGGCCID = tblGGCCHousing.lngGGCCID) " +
                            "INNER JOIN " +
                                "(SELECT tblGGCCHousing.lngGGCCHousingID, Sum(IIf([tblBedTypes].[blnMultOcc],[tblGGCC].[intMultOccBedUsage]*[tblGGCCBeds].[intNumBeds],[tblGGCCBeds].[intNumBeds])) AS intSpotsTot " +
                                "FROM ((tblGGCCHousing " +
                                    "INNER JOIN tblGGCCBeds ON tblGGCCHousing.lngGGCCHousingID = tblGGCCBeds.lngGGCCHousingID) " +
                                    "INNER JOIN tblBedTypes ON tblGGCCBeds.lngBedTypeID = tblBedTypes.lngBedTypeID) " +
                                    "INNER JOIN tblGGCC ON tblGGCCHousing.lngGGCCID = tblGGCC.lngGGCCID " +
                                "GROUP BY tblGGCCHousing.lngGGCCHousingID) AS sqrySpotsTot ON tblGGCCHousing.lngGGCCHousingID=sqrySpotsTot.lngGGCCHousingID) " +
                            "LEFT JOIN " +
                                "(SELECT tblGGCCRegHousing.lngGGCCHousingID, Sum(IIf([tblBedTypes].[blnMultOcc],[tblGGCC].[intMultOccBedUsage]*[tblGGCCRegBeds].[intNumBeds],[tblGGCCRegBeds].[intNumBeds])) AS intSpotsUsed " +
                                "FROM (((tblGGCCRegHousing " +
                                    "INNER JOIN tblGGCCRegBeds ON tblGGCCRegHousing.lngGGCCRegHousingID = tblGGCCRegBeds.lngGGCCRegHousingID) " +
                                    "INNER JOIN tblGGCCRegistrations ON tblGGCCRegHousing.lngGGCCRegistrationID = tblGGCCRegistrations.lngGGCCRegistrationID) " +
                                    "INNER JOIN tblGGCC ON tblGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID) " +
                                    "INNER JOIN tblBedTypes ON tblGGCCRegBeds.lngBedTypeID = tblBedTypes.lngBedTypeID " +
                                "GROUP BY tblGGCCRegHousing.lngGGCCHousingID) AS sqrySpotsUsed ON tblGGCCHousing.lngGGCCHousingID = sqrySpotsUsed.lngGGCCHousingID) " +
                            "LEFT JOIN " +
                                "(SELECT tblGGCCRegHousing.lngGGCCHousingID, Count(tblGGCCRegHousing.lngGGCCRegistrationID) AS intRegCount " +
                                "FROM tblGGCCRegHousing " +
                                "GROUP BY tblGGCCRegHousing.lngGGCCHousingID) AS sqryRegCount ON tblGGCCHousing.lngGGCCHousingID = sqryRegCount.lngGGCCHousingID ";

                strGroupBy = "GROUP BY tblGGCCHousing.intMaxRoomOcc, sqrySpotsTot.intSpotsTot, IIf(IsNull(sqrySpotsUsed.intSpotsUsed), 0, sqrySpotsUsed.intSpotsUsed), IIf(IsNull(sqryRegCount.intRegCount), 0, sqryRegCount.intRegCount), " +
                                "tblGGCCHousing.lngGGCCHousingID, tblGGCCHousing.lngGGCCHousingWebID, tblGGCCHousing.lngCabinConfigurationID, tblGGCCHousing.lngCabinID, tblGGCCHousing.lngGenderID, tblGGCCHousing.lngGGCCID, tblGGCCHousing.lngTransactionID, " +
                                "tblGGCCHousing.curCabinCharge, tblGGCCHousing.curChargePerBed;";

                lstStatus.Items.Insert(0, "Uploading Reserved Housing...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblGGCCHousing", "lngGGCCHousingID", "lngGGCCHousingWebID", "spAppendGGCCHousing", chkClearExisting.Checked, "reserved housing areas");

                //activity lookup
                strSQL = "SELECT tlkpGGCCActivities.blnRestrictToLocation, " +
                            "tlkpGGCCActivities.intCapacity, " +
                            "tlkpGGCCActivities.lngActivityID, 0 AS lngActivityWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tlkpGGCCActivities.lngActivityTypeID, tlkpGGCCActivities.lngDefaultLocation, " +
                            "tlkpGGCCActivities.curDefaultChargePerPerson, " +
                            "tlkpGGCCActivities.strActivityName " +
                        "FROM ((tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCActivities ON tblGGCC.lngGGCCID = tblGGCCActivities.lngGGCCID) " +
                            "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID ";

                strGroupBy = "GROUP BY tlkpGGCCActivities.blnRestrictToLocation, " +
                                "tlkpGGCCActivities.intCapacity, " +
                                "tlkpGGCCActivities.lngActivityID, tlkpGGCCActivities.lngActivityWebID, tlkpGGCCActivities.lngActivityTypeID, tlkpGGCCActivities.lngDefaultLocation, " +
                                "tlkpGGCCActivities.curDefaultChargePerPerson, " +
                                "tlkpGGCCActivities.strActivityName;";

                lstStatus.Items.Insert(0, "Uploading Group Activities...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tlkpGGCCActivities", "lngActivityID", "lngActivityWebID", "spAppendActivity", chkClearExisting.Checked, "available activities");

                //reserved activities
                strSQL = "SELECT tblGGCCActivities.blnAvailableForReg, tblGGCCActivities.blnExclusive, tblGGCCActivities.blnAvailableForOLReg, " +
                            "tblGGCCActivities.intDuration, tblGGCCActivities.intParticipants, " +
                            "tblGGCCActivities.lngGGCCActivityID, 0 AS lngGGCCActivityWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGGCCActivities.lngActivityID, tblGGCCActivities.lngGGCCID, tblGGCCActivities.lngLocationID, tblGGCCActivities.lngTransactionID, tblGGCCActivities.lngOldGGCCActivityID, " +
                            "tblGGCCActivities.curChargePerPerson, " +
                            "CStr(IIf(IsNull(tblGGCCActivities.dteActivityDate), \"\", tblGGCCActivities.dteActivityDate)) AS dteActivityDate, CStr(IIf(IsNull(tblGGCCActivities.dteActivityTime), \"\", tblGGCCActivities.dteActivityTime)) AS dteActivityTime " +
                        "FROM (tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCActivities ON tblGGCC.lngGGCCID = tblGGCCActivities.lngGGCCID ";

                strGroupBy = "GROUP BY tblGGCCActivities.blnAvailableForReg, tblGGCCActivities.blnExclusive, tblGGCCActivities.blnAvailableForOLReg, " +
                                "tblGGCCActivities.intDuration, tblGGCCActivities.intParticipants, " +
                                "tblGGCCActivities.lngGGCCActivityID, tblGGCCActivities.lngGGCCActivityWebID, tblGGCCActivities.lngActivityID, tblGGCCActivities.lngGGCCID, tblGGCCActivities.lngLocationID, tblGGCCActivities.lngTransactionID, tblGGCCActivities.lngOldGGCCActivityID, " +
                                "tblGGCCActivities.curChargePerPerson, " +
                                "tblGGCCActivities.dteActivityDate, tblGGCCActivities.dteActivityTime;";

                lstStatus.Items.Insert(0, "Uploading Reserved Activity Definitions...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblGGCCActivities", "lngGGCCActivityID", "lngGGCCActivityWebID", "spAppendGGCCActivity", chkClearExisting.Checked, "reserved activities");

                //activity packages
                strSQL = "SELECT tblGGCCPackages.lngGGCCPackageID, 0 AS lngGGCCPackageWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGGCCPackages.lngGGCCID, " +
                            "tblGGCCPackages.curPackageCost, " +
                            "tblGGCCPackages.strPackageName, IIf(IsNull(tblGGCCPackages.strPackageDesc), '', tblGGCCPackages.strPackageDesc) AS strPackageDesc " +
                        "FROM (tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCPackages ON tblGGCC.lngGGCCID = tblGGCCPackages.lngGGCCID ";

                strGroupBy = "GROUP BY tblGGCCPackages.lngGGCCPackageID, tblGGCCPackages.lngGGCCPackageWebID, tblGGCCPackages.lngGGCCID, " +
                                "tblGGCCPackages.curPackageCost, " +
                                "tblGGCCPackages.strPackageName, tblGGCCPackages.strPackageDesc;";

                lstStatus.Items.Insert(0, "Uploading Activity Packages...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblGGCCPackages", "lngGGCCPackageID", "lngGGCCPackageWebID", "spAppendGGCCPackage", chkClearExisting.Checked, "activity packages");

                //activities in packages
                strSQL = "SELECT tblGGCCPackages.lngGGCCPackageID, 0 AS lngGGCCPackageWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblGGCCPackages.lngGGCCID, " +
                            "tblGGCCPackages.curPackageCost, " +
                            "tblGGCCPackages.strPackageName, tblGGCCPackages.strPackageDesc " +
                        "FROM (tblGGCC " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblGGCCPackages ON tblGGCC.lngGGCCID = tblGGCCPackages.lngGGCCID ";

                strSQL = "SELECT tblnkGGCCActivityPackages.lngGGCCActivityPackageID, 0 AS lngGGCCActivityPackageWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblnkGGCCActivityPackages.lngGGCCActivityID, tblnkGGCCActivityPackages.lngGGCCPackageID " +
                        "FROM ((tblnkGGCCActivityPackages " +
                            "INNER JOIN tblGGCCPackages ON tblnkGGCCActivityPackages.lngGGCCPackageID = tblGGCCPackages.lngGGCCPackageID) " +
                            "INNER JOIN tblGGCC ON tblGGCCPackages.lngGGCCID  = tblGGCC.lngGGCCID) " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "";

                lstStatus.Items.Insert(0, "Uploading Package Definitions...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblnkGGCCActivityPackages", "lngGGCCActivityPackageID", "lngGGCCActivityPackageWebID", "spAppendGGCCActivityPackage", chkClearExisting.Checked, "activity package definitions");

                //cabin category capacities
                strSQL = "SELECT tblCabinCategoryCapacity.lngCabinCategoryCapacityID, 0 AS lngCabinCategoryCapacityWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblCabinCategoryCapacity.lngCabinCategoryID, " +
                            "tblCabinCategoryCapacity.strCabinCategoryCapacity " +
                        "FROM (((tblCabinCategoryCapacity " +
                            "INNER JOIN tblCabinConfigurations ON tblCabinCategoryCapacity.lngCabinCategoryCapacityID = tblCabinConfigurations.lngCabinCatCapID) " +
                            "INNER JOIN tblGGCCHousing ON tblGGCCHousing.lngCabinConfigurationID = tblCabinConfigurations.lngCabinConfigurationID) " +
                            "INNER JOIN tblGGCC ON tblGGCCHousing.lngGGCCID = tblGGCC.lngGGCCID) " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "GROUP BY tblCabinCategoryCapacity.lngCabinCategoryCapacityID, tblCabinCategoryCapacity.lngCabinCategoryID, " +
                            "tblCabinCategoryCapacity.strCabinCategoryCapacity, tlkpCampName.blnUpload;";

                lstStatus.Items.Insert(0, "Uploading Cabin Category Capacities...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblCabinCategoryCapacity", "lngCabinCategoryCapacityID", "lngCabinCategoryCapacityWebID", "spAppendCabinCategoryCapacity", chkClearExisting.Checked, "cabin category capacities");

                //cabin configurations
                strSQL = "SELECT tblCabinConfigurations.lngCabinConfigurationID, 0 AS lngCabinConfigurationWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tblCabinConfigurations.lngBathTypeID, tblCabinConfigurations.lngCabinCatCapID, tblCabinConfigurations.lngCabinID, tblCabinConfigurations.lngUsageID, " +
                            "tblCabinConfigurations.dteEndDate, tblCabinConfigurations.dteStartDate, " +
                            "tblCabinConfigurations.curCostPerNight, " +
                            "tblCabinConfigurations.strQualityCode " +
                        "FROM ((tblCabinConfigurations " +
                            "INNER JOIN tblGGCCHousing ON tblCabinConfigurations.lngCabinConfigurationID = tblGGCCHousing.lngCabinConfigurationID ) " +
                            "INNER JOIN tblGGCC ON tblGGCCHousing.lngGGCCID = tblGGCC.lngGGCCID) " +
                            "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID ";

                strGroupBy = "GROUP BY tblCabinConfigurations.lngCabinConfigurationID, tblCabinConfigurations.lngBathTypeID, tblCabinConfigurations.lngCabinCatCapID, tblCabinConfigurations.lngCabinID, tblCabinConfigurations.lngUsageID, " +
                                        "tblCabinConfigurations.dteEndDate, tblCabinConfigurations.dteStartDate, " +
                                        "tblCabinConfigurations.curCostPerNight, " +
                                        "tblCabinConfigurations.strQualityCode;";

                lstStatus.Items.Insert(0, "Uploading Cabin Configurations...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(clsWebTalk.fcnBuildSQL(strSQL, strGroupBy, radSpecificEvent, cboSpecificEvent, txtStartDate, txtEndDate, cboProgram), "tblCabinConfigurations", "lngCabinConfigurationID", "lngCabinConfigurationWebID", "spAppendCabinConfiguration", chkClearExisting.Checked, "cabin configurations");

                lstStatus.Items.Insert(0, DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Upload Complete.");
                Application.DoEvents();

                if (strULRes != "")
                {
                    lstStatus.Items.Insert(0, "Warning--missing definitions!");
                    Application.DoEvents();
                    MessageBox.Show("There are some warnings about missing data.\nThis could be because of events not being fully defined.\nPlease review the missing information in the following dialog.");
                    MessageBox.Show(strULRes);
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmUploadEvents.btnUpload", ex);
            }

            this.Cursor = Cursors.Default;
        }

        private void frmUploadEvents_Load(object sender, EventArgs e)
        {

            OleDbConnection objConn;
            OleDbCommand objCommand;
            OleDbDataReader dr;

            clsCboItem cboItem;

            string strSQL;

            objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

            objConn.Open();

            objCommand = new OleDbCommand();

            strSQL = "SELECT tlkpCampName.lngCampID, " +
                        "tlkpCampName.strCampName " +
                    "FROM tlkpCampName " +
                    "WHERE tlkpCampName.blnUpload = True AND " +
                        "tlkpCampName.lngProgramTypeID = " + ((int)clsGlobalEnum.conPROGRAMTYPE.GroupEvent).ToString() + " " +
                    "ORDER BY tlkpCampName.strCampName;";

            objCommand.CommandText = strSQL;

            objCommand.Connection = objConn;

            dr = objCommand.ExecuteReader();

            while (dr.Read())
            {
                long lngNewCampID;

                long.TryParse(dr["lngCampID"].ToString(), out lngNewCampID);

                cboItem = new clsCboItem(lngNewCampID, dr["strCampName"].ToString());

                cboProgram.Items.Add(cboItem);

            }

            dr.Close();

            strSQL = "SELECT tblGGCC.lngGGCCID, " +
                        "[tblGGCC].[strGGCCName] & \" (\" & [tblGGCC].[dteStartDate] & \")\" AS strEventName " +
                    "FROM tblGGCC " +
                        "INNER JOIN tlkpCampName ON tblGGCC.lngProgramTypeID = tlkpCampName.lngCampID " +
                    "WHERE tlkpCampName.blnUpload = True AND " +
                        "tlkpCampName.lngProgramTypeID = " + ((int)clsGlobalEnum.conPROGRAMTYPE.GroupEvent).ToString() + " " +
                    "ORDER BY tblGGCC.dteStartDate;";

            objCommand.CommandText = strSQL;

            dr = objCommand.ExecuteReader();

            while (dr.Read())
            {
                long lngNewGGCCID;

                long.TryParse(dr["lngGGCCID"].ToString(), out lngNewGGCCID);

                cboItem = new clsCboItem(lngNewGGCCID, dr["strEventName"].ToString());

                cboSpecificEvent.Items.Add(cboItem);

            }

            dr.Close();

            objConn.Close();

            objCommand.Dispose();
            objConn.Dispose();

            radAllEvents.Checked = true;
        }

        private void chkClearExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClearExisting.Checked)
                lblClearExisting.Visible = true;
            else
                lblClearExisting.Visible = false;
        }

        private void chkClearExisting_MouseEnter(object sender, EventArgs e)
        {
            subShowTooltip("Check this box to clear previously uploaded events from the web server before uploading the new event definitions.");
        }

        private void chkClearExisting_MouseLeave(object sender, EventArgs e)
        {
            subHideTooltip();
        }

        private void subShowTooltip(string _strDesc)
        {
            lblTooltip.Text = _strDesc;

            lblTooltip.Visible = true;
        }

        private void subHideTooltip()
        {
            lblTooltip.Visible = false;
        }

        private void subValDate(TextBox _txtToVal)
        {            
            string strOut="";

            try
            {
                string strIn = "";

                DateTime dteIn;

                strIn = _txtToVal.Text;

                dteIn = Convert.ToDateTime(strIn);

                strOut = dteIn.ToString("M/d/yyyy");
            }
            catch
            {
                strOut = "";
            }

            _txtToVal.Text = strOut;
        }

        private void txtStartDate_Leave(object sender, EventArgs e)
        {
            subValDate(txtStartDate);
        }

        private void txtEndDate_Leave(object sender, EventArgs e)
        {
            subValDate(txtEndDate);
        }

        private void lblEventStartDates_MouseEnter(object sender, EventArgs e)
        {
            subShowTooltip("Define start and end dates.  Any event over-lapping the specified date range will be uploaded.");
        }

        private void lblEventStartDates_MouseLeave(object sender, EventArgs e)
        {
            subHideTooltip();
        }            
    }
}