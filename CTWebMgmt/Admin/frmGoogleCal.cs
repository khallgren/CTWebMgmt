using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Util;

namespace CTWebMgmt
{
    public partial class frmGoogleCal : Form
    {
        CalendarService svcCTCal;

        public frmGoogleCal()
        {
            InitializeComponent();
            
            // Register the authenticator.
            var cliCTCal = new NativeApplicationClient(GoogleAuthenticationServer.Description);

            cliCTCal.ClientIdentifier = "529290214236.apps.googleusercontent.com";
            cliCTCal.ClientSecret = "4mdTMEEKuSCwGhPPxV9HfbtN";

            var athCTCal = new OAuth2Authenticator<NativeApplicationClient>(cliCTCal, GetAuthorization);

            // Create the service.
            svcCTCal = new CalendarService(athCTCal);
        }

        private IAuthorizationState GetAuthorization(NativeApplicationClient arg)
        {
            // Get the auth URL:
            IAuthorizationState authState = new AuthorizationState(new[] { CalendarService.Scopes.Calendar.GetStringValue() });
            authState.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);
            Uri uriAuth = arg.RequestUserAuthorization(authState);

            string strAuthCode = "";

            // Request authorization from the user (by opening a browser window):
            using ( Admin.frmOAuth objOAuth = new global::CTWebMgmt.Admin.frmOAuth(uriAuth.ToString()))
            {
                Cursor.Current = Cursors.Default;
                objOAuth.ShowDialog();
                strAuthCode = objOAuth.strAuthCode;
            }

            // Retrieve the access token by using the authorization code:
            return arg.ProcessUserAuthorization(strAuthCode, authState);
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (chkGroupEvents.Checked)
            {             
                if (cboCCCalendar.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a calendar for group events.");
                    return;
                }
                
                subClearCal(cboCCCalendar, lblCCStatus);
            }

            if (chkGroupRentals.Checked)
            {
                if (cboGGCalendar.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a calendar for group rentals.");
                    return;
                }

                subClearCal(cboGGCalendar, lblGGStatus);
            }

            if (chkIndEvents.Checked)
            {
                if (cboBlockCalendar.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a calendar for individual events.");
                    return;
                }

                subClearCal(cboBlockCalendar, lblBlockStatus);
            }

            if (chkGroupEvents.Checked)
                subULCC();

            if (chkGroupRentals.Checked)
                subULGG();

            if (chkIndEvents.Checked)
                subULBlocks();

            MessageBox.Show("Upload process complete");
        }

        private void subClearCal(ComboBox _cboToClear,Label _lblStatus)
        {
            try
            {
                string strCalID = "";

                strCalID = ((clsCboItem)_cboToClear.SelectedItem).STRID;

                _lblStatus.Text = "Clearing Calendars...";

                subClearEvents(strCalID, _lblStatus);
               
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);
            }

        }

        private void subClearEvents(string _strCalID, Label _lblStatus)
        {
            int intNumberDeleted = 0;

            EventsResource.ListRequest EventsList = svcCTCal.Events.List(_strCalID);

            Events svcEventsToDel = EventsList.Fetch();

            bool blnFirst = true;

            while (EventsList.PageToken != null || blnFirst)
            {
                blnFirst = false;

                if (svcEventsToDel.Items != null)
                {
                    for (int intI = 0; intI < svcEventsToDel.Items.Count; intI++)
                    {
                        string strIDToDel = svcEventsToDel.Items[intI].Id;

                        string strDelRes = svcCTCal.Events.Delete(_strCalID, strIDToDel).Fetch();

                        if (strDelRes != "")
                            lstOutput.Items.Insert(0, "Delete result: " + strDelRes);

                        intNumberDeleted++;

                        if (_lblStatus.Text == "Clearing Calendars...")
                            _lblStatus.Text = "Clearing Calendars";
                        else if (_lblStatus.Text == "Clearing Calendars")
                            _lblStatus.Text = "Clearing Calendars.";
                        else if (_lblStatus.Text == "Clearing Calendars.")
                            _lblStatus.Text = "Clearing Calendars..";
                        else
                            _lblStatus.Text = "Clearing Calendars...";

                        _lblStatus.Text += " (" + intNumberDeleted.ToString() + ")";

                        Application.DoEvents();
                    }
                }

                EventsList.PageToken = svcEventsToDel.NextPageToken;
                svcEventsToDel = EventsList.Fetch();
            }
        }

        private string fcnBuildGGCriter()
        {
            string strWhere = "";
            string strSQL = "";

            bool blnProgram = false;
            bool blnStatus = false;
            bool blnStartDate = false;
            
            strWhere = "WHERE tblGGCC.lngGroupTypeID=1 ";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblGoogleCalOptions.blnFilterByProgramGG, tblGoogleCalOptions.blnFilterByStartDateGG, tblGoogleCalOptions.blnFilterByStatusGG, " +
                            "tblGoogleCalOptions.dteStartDateGG, tblGoogleCalOptions.dteEndDateGG " +
                        "FROM tblGoogleCalOptions";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPref = cmdDB.ExecuteReader())
                    {
                        if (drPref.Read())
                        {
                            try { blnProgram = Convert.ToBoolean(drPref["blnFilterByProgramGG"]); }
                            catch { blnProgram = false; }

                            try { blnStartDate = Convert.ToBoolean(drPref["blnFilterByStartDateGG"]); }
                            catch { blnStartDate = false; }

                            try { blnStatus = Convert.ToBoolean(drPref["blnFilterByStatusGG"]); }
                            catch { blnStatus = false; }

                            if (blnStartDate)
                            {
                                DateTime dteStart;
                                DateTime dteEnd;

                                try { dteStart = Convert.ToDateTime(drPref["dteStartDateGG"]); }
                                catch { dteStart = DateTime.Now; }

                                try { dteEnd = Convert.ToDateTime(drPref["dteEndDateGG"]); }
                                catch { dteEnd = DateTime.Now.AddYears(1); }

                                strWhere += "AND (DateDiff(\"d\",#" + dteStart.ToString("MM/dd/yyyy") + "#, [tblGGCC].[dteStartDate])>=0 AND " +
                                        "DateDiff(\"d\",[tblGGCC].[dteStartDate],#" + dteEnd.ToString("MM/dd/yyyy") + "#)>=0) ";

                            }
                        }

                        drPref.Close();
                    }

                    if (blnProgram)
                    {
                        strSQL = "SELECT tblGoogleCalFilteredPrograms.lngProgramTypeID " +
                                "FROM tblGoogleCalFilteredPrograms " +
                                    "INNER JOIN tlkpCampName ON tblGoogleCalFilteredPrograms.lngProgramTypeID = tlkpCampName.lngCampID " +
                                "WHERE tlkpCampName.lngProgramTypeID=3";
                        //1=Block
                        //2=CC
                        //3=GG

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        strWhere += "AND tblGGCC.lngProgramTypeID In (0, ";

                        using (OleDbDataReader drProg = cmdDB.ExecuteReader())
                        {
                            while (drProg.Read())
                            {
                                try { strWhere += Convert.ToInt32(drProg["lngProgramTypeID"]).ToString() + ", "; }
                                catch { }
                            }

                            drProg.Close();
                        }

                        strWhere = strWhere.Substring(0, strWhere.Length - 2) + ") ";
                    }

                    if (blnStatus)
                    {
                        strSQL = "SELECT tblGoogleCalFilteredStatus.lngStatusID " +
                                "FROM tblGoogleCalFilteredStatus";

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        strWhere += "AND tblGGCC.lngGroupStatusID In (0, ";

                        using (OleDbDataReader drProg = cmdDB.ExecuteReader())
                        {
                            while (drProg.Read())
                            {
                                try { strWhere += Convert.ToInt32(drProg["lngStatusID"]).ToString() + ", "; }
                                catch { }
                            }

                            drProg.Close();
                        }

                        strWhere = strWhere.Substring(0, strWhere.Length - 2) + ") ";
                    }
                }

                conDB.Close();
            }

            return strWhere;
        }

        private void subULGG()
        {
            string strSQL = "";
            string strWhere = fcnBuildGGCriter();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT COUNT(tblGGCC.lngGGCCID) AS intCount " +
                        "FROM tblGGCC " +
                        strWhere;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    int intCount = 0;

                    try { intCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intCount = 0; }

                    if (intCount == 0) lblGGStatus.Text = "No group rentals to upload";

                    strSQL = "SELECT tblGGCC.lngGGCCID, " +
                                "tblGGCC.dteStartDate, tblGGCC.dteStartTime, tblGGCC.dteEndDate, tblGGCC.dteEndTime, " +
                                "tblGGCC.strGGCCName, tblSites.strSiteName, tblGGCC.mmoGGCCNotes " +
                            "FROM tblGGCC " +
                                "LEFT JOIN tblSites ON tblGGCC.lngSiteID = tblSites.lngSiteID " +
                            strWhere;

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drGG = cmdDB.ExecuteReader())
                    {
                        int intCurrent = 0;

                        while (drGG.Read())
                        {
                            intCurrent++;

                            lblGGStatus.Text = "Uploading rental " + intCurrent.ToString() + " of " + intCount.ToString();
                            Application.DoEvents();

                            string strTitle = "";
                            string strDesc = "";
                            string strLocation = "";

                            DateTime dteStart;
                            DateTime dteEnd;

                            long lngGGCCID=0;

                            try { lngGGCCID = Convert.ToInt32(drGG["lngGGCCID"]); }
                            catch { lngGGCCID = 0; }

                            try { subPopGGDetails(lngGGCCID, ref strTitle, ref strDesc); }
                            catch { strTitle = ""; strDesc = ""; }

                            try { strLocation = Convert.ToString(drGG["strSiteName"]); }
                            catch { strLocation = "Undefined Location"; }

                            string strHousing = "";

                            try { strHousing = fcnGetGGCCHousingAreas(lngGGCCID); }
                            catch { strHousing = ""; }

                            if (strHousing != "") strLocation += ": " + strHousing;

                            try { dteStart = new DateTime(Convert.ToDateTime(drGG["dteStartDate"]).Year, Convert.ToDateTime(drGG["dteStartDate"]).Month, Convert.ToDateTime(drGG["dteStartDate"]).Day, Convert.ToDateTime(drGG["dteStartTime"]).Hour, Convert.ToDateTime(drGG["dteStartTime"]).Minute, Convert.ToDateTime(drGG["dteStartTime"]).Second); }
                            catch { dteStart = DateTime.Now; }

                            try { dteEnd = new DateTime(Convert.ToDateTime(drGG["dteEndDate"]).Year, Convert.ToDateTime(drGG["dteEndDate"]).Month, Convert.ToDateTime(drGG["dteEndDate"]).Day, Convert.ToDateTime(drGG["dteEndTime"]).Hour, Convert.ToDateTime(drGG["dteEndTime"]).Minute, Convert.ToDateTime(drGG["dteEndTime"]).Second); }
                            catch { dteEnd = DateTime.Now; }

                            string strCalendarID = "";

                            try { strCalendarID = ((clsCboItem)cboGGCalendar.SelectedItem).STRID; }
                            catch { strCalendarID = ""; }

                            lstOutput.Items.Insert(0, "Uploading rental (" + strTitle + ", " + dteStart.ToString() + "-" + dteEnd.ToString());
                            Application.DoEvents();

                            subULEvent(strTitle, strDesc, strLocation, strCalendarID, dteStart, dteEnd,lstOutput);
                        }

                        drGG.Close();
                    }
                }

                conDB.Close();
            }
        }

        private string fcnBuildCCCriter()
        {
            string strWhere = "";
            string strSQL = "";

            bool blnProgram = false;
            bool blnStatus = false;
            bool blnStartDate = false; 

            strWhere = "WHERE tblGGCC.lngGroupTypeID=0 ";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblGoogleCalOptions.blnFilterByProgramCC, tblGoogleCalOptions.blnFilterByStartDateCC, tblGoogleCalOptions.blnFilterByStatusCC, " +
                            "tblGoogleCalOptions.dteStartDateCC, tblGoogleCalOptions.dteEndDateCC " +
                        "FROM tblGoogleCalOptions";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPref = cmdDB.ExecuteReader())
                    {
                        if (drPref.Read())
                        {
                            try { blnProgram = Convert.ToBoolean(drPref["blnFilterByProgramCC"]); }
                            catch{blnProgram=false;}

                            try { blnStartDate = Convert.ToBoolean(drPref["blnFilterByStartDateCC"]); }
                            catch { blnStartDate = false; }

                            try { blnStatus = Convert.ToBoolean(drPref["blnFilterByStatusCC"]); }
                            catch { blnStatus = false; }

                            if (blnStartDate)
                            {
                                DateTime dteStart;
                                DateTime dteEnd;

                                try { dteStart = Convert.ToDateTime(drPref["dteStartDateCC"]); }
                                catch { dteStart = DateTime.Now; }

                                try { dteEnd = Convert.ToDateTime(drPref["dteEndDateCC"]); }
                                catch { dteEnd = DateTime.Now.AddYears(1); }

                                strWhere += "AND (DateDiff(\"d\",#" + dteStart.ToString("MM/dd/yyyy") + "#, [tblGGCC].[dteStartDate])>=0 AND " +
                                        "DateDiff(\"d\",[tblGGCC].[dteStartDate],#" + dteEnd.ToString("MM/dd/yyyy") + "#)>=0) ";

                            }
                        }

                        drPref.Close();
                    }

                    if (blnProgram)
                    {
                        strSQL = "SELECT tblGoogleCalFilteredPrograms.lngProgramTypeID " +
                                "FROM tblGoogleCalFilteredPrograms " +
                                    "INNER JOIN tlkpCampName ON tblGoogleCalFilteredPrograms.lngProgramTypeID = tlkpCampName.lngCampID " +
                                "WHERE tlkpCampName.lngProgramTypeID=2";
                        //1=Block
                        //2=CC
                        //3=GG

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        strWhere += "AND tblGGCC.lngProgramTypeID In (0, ";

                        using (OleDbDataReader drProg = cmdDB.ExecuteReader())
                        {
                            while (drProg.Read())
                            {
                                try { strWhere += Convert.ToInt32(drProg["lngProgramTypeID"]).ToString() + ", "; }
                                catch { }
                            }

                            drProg.Close();
                        }

                        strWhere = strWhere.Substring(0, strWhere.Length - 2) + ") ";
                    }

                    if (blnStatus)
                    {
                        strSQL = "SELECT tblGoogleCalFilteredStatus.lngStatusID " +
                                "FROM tblGoogleCalFilteredStatus";

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        strWhere += "AND tblGGCC.lngGroupStatusID In (0, ";

                        using (OleDbDataReader drProg = cmdDB.ExecuteReader())
                        {
                            while (drProg.Read())
                            {
                                try { strWhere += Convert.ToInt32(drProg["lngStatusID"]).ToString() + ", "; }
                                catch { }
                            }

                            drProg.Close();
                        }

                        strWhere = strWhere.Substring(0, strWhere.Length - 2) + ") ";
                    }                    
                }

                conDB.Close();
            }

            return strWhere;
        }

        private void subULCC()
        {
            string strSQL = "";
            string strWhere = "";

            strWhere = fcnBuildCCCriter();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT Count(tblGGCC.lngGGCCID) AS intCount " +
                        "FROM tblGGCC " +
                        strWhere;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    int intCount = 0;

                    try { intCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intCount = 0; }

                    if (intCount == 0) lblCCStatus.Text = "No group events to upload";

                    strSQL = "SELECT tblGGCC.lngGGCCID, " +
                                "tblGGCC.dteStartDate, tblGGCC.dteStartTime, tblGGCC.dteEndDate, tblGGCC.dteEndTime, " +
                                "tblGGCC.strGGCCName, tblSites.strSiteName, tblGGCC.mmoGGCCNotes " +
                            "FROM tblGGCC " +
                                "LEFT JOIN tblSites ON tblGGCC.lngSiteID = tblSites.lngSiteID " +
                            strWhere;

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drCC = cmdDB.ExecuteReader())
                    {
                        int intCurrent = 0;

                        while (drCC.Read())
                        {
                            intCurrent++;

                            lblCCStatus.Text = "Uploading event " + intCurrent.ToString() + " of " + intCount.ToString();
                            Application.DoEvents();

                            string strTitle = "";
                            string strDesc = "";
                            string strLocation = "";

                            DateTime dteStart;
                            DateTime dteEnd;

                            long lngGGCCID = 0;

                            try { lngGGCCID = Convert.ToInt32(drCC["lngGGCCID"]); }
                            catch { lngGGCCID = 0; }

                            try { subPopCCDetails(lngGGCCID, ref strTitle, ref strDesc); }
                            catch { strTitle = ""; strDesc = ""; }

                            try { strLocation = Convert.ToString(drCC["strSiteName"]); }
                            catch { strLocation = "Undefined Location"; }

                            string strHousing = "";

                            try { strHousing = fcnGetGGCCHousingAreas(lngGGCCID); }
                            catch { strHousing = ""; }

                            if (strHousing != "") strLocation += ": " + strHousing;

                            try { dteStart = new DateTime(Convert.ToDateTime(drCC["dteStartDate"]).Year, Convert.ToDateTime(drCC["dteStartDate"]).Month, Convert.ToDateTime(drCC["dteStartDate"]).Day, Convert.ToDateTime(drCC["dteStartTime"]).Hour, Convert.ToDateTime(drCC["dteStartTime"]).Minute, Convert.ToDateTime(drCC["dteStartTime"]).Second); }
                            catch { dteStart = DateTime.Now; }

                            try { dteEnd = new DateTime(Convert.ToDateTime(drCC["dteEndDate"]).Year, Convert.ToDateTime(drCC["dteEndDate"]).Month, Convert.ToDateTime(drCC["dteEndDate"]).Day, Convert.ToDateTime(drCC["dteEndTime"]).Hour, Convert.ToDateTime(drCC["dteEndTime"]).Minute, Convert.ToDateTime(drCC["dteEndTime"]).Second); }
                            catch { dteEnd = DateTime.Now; }

                            string strCalendarID = "";

                            try { strCalendarID = ((clsCboItem)cboCCCalendar.SelectedItem).STRID; }
                            catch { strCalendarID = ""; }

                            subULEvent(strTitle, strDesc, strLocation, strCalendarID, dteStart, dteEnd, lstOutput);
                        }

                        drCC.Close();
                    }
                }

                conDB.Close();
            }
        }

        private string fcnGetBlockHousingAreas(long _lngBlockID)
        {
            string strSQL = "";
            string strRes = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tlkpHousingName.strHousingName " +
                        "FROM (tlkpCabinNames " +
                            "INNER JOIN tlkpHousingName ON tlkpCabinNames.lngHousingID = tlkpHousingName.lngHousingID) " +
                            "INNER JOIN tblnkBlockCabins ON tlkpCabinNames.lngCabinID = tblnkBlockCabins.lngCabinID " +
                        "GROUP BY tlkpHousingName.strHousingName, tblnkBlockCabins.lngBlockID " +
                        "HAVING tblnkBlockCabins.lngBlockID=" + _lngBlockID;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drBlock = cmdDB.ExecuteReader())
                    {
                        while (drBlock.Read())
                        {
                            try { strRes += Convert.ToString(drBlock["strHousingName"]) + ", "; }
                            catch { }
                        }

                        if (strRes.Length > 2) strRes = strRes.Substring(0, strRes.Length - 2);

                        drBlock.Close();
                    }
                }

                conDB.Close();
            }

            return strRes;
        }
        
        private string fcnGetGGCCHousingAreas(long _lngGGCCID)
        {
            string strSQL = "";
            string strRes = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tlkpHousingName.strHousingName " +
                        "FROM (tblGGCCHousing " +
                            "INNER JOIN tlkpCabinNames ON tblGGCCHousing.lngCabinID = tlkpCabinNames.lngCabinID) " +
                            "INNER JOIN tlkpHousingName ON tlkpCabinNames.lngHousingID = tlkpHousingName.lngHousingID " +
                        "GROUP BY tlkpHousingName.strHousingName, tblGGCCHousing.lngGGCCID " +
                        "HAVING tblGGCCHousing.lngGGCCID=" + _lngGGCCID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drGGCC = cmdDB.ExecuteReader())
                    {
                        while (drGGCC.Read())
                        {
                            try { strRes += Convert.ToString(drGGCC["strHousingName"]) + ", "; }
                            catch { }
                        }

                        if (strRes.Length > 2) strRes = strRes.Substring(0, strRes.Length - 2);

                        drGGCC.Close();
                    }
                }

                conDB.Close();
            }

            return strRes;
        }

        private string fcnBuildBlockCriter()
        {
            string strWhere = "";
            string strSQL = "";

            bool blnProgram = false;
            bool blnStartDate = false; 

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblGoogleCalOptions.blnFilterByProgramBlock, tblGoogleCalOptions.blnFilterByStartDateBlock, " +
                            "tblGoogleCalOptions.dteStartDateBlock, tblGoogleCalOptions.dteEndDateBlock " +
                        "FROM tblGoogleCalOptions";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPref = cmdDB.ExecuteReader())
                    {
                        if (drPref.Read())
                        {
                            try { blnProgram = Convert.ToBoolean(drPref["blnFilterByProgramBlock"]); }
                            catch { blnProgram = false; }

                            try { blnStartDate = Convert.ToBoolean(drPref["blnFilterByStartDateBlock"]); }
                            catch { blnStartDate = false; }

                            if (blnStartDate)
                            {
                                DateTime dteStart;
                                DateTime dteEnd;

                                try { dteStart = Convert.ToDateTime(drPref["dteStartDateBlock"]); }
                                catch { dteStart = DateTime.Now; }

                                try { dteEnd = Convert.ToDateTime(drPref["dteEndDateBlock"]); }
                                catch { dteEnd = DateTime.Now.AddYears(1); }

                                if (strWhere == "")
                                    strWhere = "WHERE (DateDiff(\"d\",#" + dteStart.ToString("MM/dd/yyyy") + "#, [tlkpWeekDesc].[dteStartDate])>=0 AND " +
                                        "DateDiff(\"d\",[tlkpWeekDesc].[dteStartDate],#" + dteEnd.ToString("MM/dd/yyyy") + "#)>=0) ";
                                else
                                    strWhere += "AND (DateDiff(\"d\",#" + dteStart.ToString("MM/dd/yyyy") + "#, [tlkpWeekDesc].[dteStartDate])>=0 AND " +
                                            "DateDiff(\"d\",[tlkpWeekDesc].[dteStartDate],#" + dteEnd.ToString("MM/dd/yyyy") + "#)>=0) ";                                
                            }
                        }

                        drPref.Close();
                    }

                    if (blnProgram)
                    {
                        strSQL = "SELECT tblGoogleCalFilteredPrograms.lngProgramTypeID " +
                                "FROM tblGoogleCalFilteredPrograms " +
                                    "INNER JOIN tlkpCampName ON tblGoogleCalFilteredPrograms.lngProgramTypeID = tlkpCampName.lngCampID " +
                                "WHERE tlkpCampName.lngProgramTypeID=1";
                        //1=Block
                        //2=CC
                        //3=GG

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        if (strWhere == "")
                            strWhere = "WHERE tblBlock.lngCampID In (0, ";
                        else
                            strWhere += "AND tblBlock.lngCampID In (0, ";

                        using (OleDbDataReader drProg = cmdDB.ExecuteReader())
                        {
                            while (drProg.Read())
                            {
                                try { strWhere += Convert.ToInt32(drProg["lngProgramTypeID"]).ToString() + ", "; }
                                catch { }
                            }

                            drProg.Close();
                        }

                        strWhere = strWhere.Substring(0, strWhere.Length - 2) + ") ";
                    }                                  
                }

                conDB.Close();
            }

            return strWhere;
        }

        private void subULBlocks()
        {
            string strSQL = "";
            string strWhere = fcnBuildBlockCriter();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT COUNT(tblBlock.lngBlockID) AS intCount " +
                        "FROM tblBlock " +
                            "INNER JOIN tlkpWeekDesc ON tblBlock.lngWeekID = tlkpWeekDesc.lngWeekID " +
                        strWhere;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    int intCount = 0;

                    try { intCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intCount = 0; }

                    if (intCount == 0) lblBlockStatus.Text = "No blocks to upload";

                    strSQL = "SELECT tblBlock.lngBlockID, " +
                                "tlkpWeekDesc.dteStartDate, tlkpWeekDesc.dteStartTime, tlkpWeekDesc.dteEndDate, tlkpWeekDesc.dteEndTime, " +
                                "tblBlock.strBlockCode, tblSites.strSiteName " +
                            "FROM (tblBlock " +
                                "LEFT JOIN tblSites ON tblBlock.lngSiteID = tblSites.lngSiteID) " +
                                "LEFT JOIN tlkpWeekDesc ON tblBlock.lngWeekID = tlkpWeekDesc.lngWeekID " +
                            strWhere;

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drBlock = cmdDB.ExecuteReader())
                    {
                        int intCurrent = 0;

                        while (drBlock.Read())
                        {
                            intCurrent++;

                            lblBlockStatus.Text = "Uploading block " + intCurrent.ToString() + " of " + intCount.ToString();
                            Application.DoEvents();

                            string strTitle = "";
                            string strDesc = "";
                            string strLocation = "";

                            DateTime dteStart;
                            DateTime dteEnd;

                            long lngBlockID = 0;

                            try { lngBlockID = Convert.ToInt32(drBlock["lngBlockID"]); }
                            catch { lngBlockID = 0; }

                            try { subPopBlockDetails(lngBlockID, ref strTitle, ref strDesc); }
                            catch { strTitle = ""; strDesc = ""; }

                            try { strLocation = Convert.ToString(drBlock["strSiteName"]); }
                            catch { strLocation = "Undefined Location"; }

                            string strHousing = "";

                            try { strHousing = fcnGetBlockHousingAreas(lngBlockID); }
                            catch { strHousing = ""; }

                            if (strHousing != "") strLocation += ": " + strHousing;

                            try { dteStart = new DateTime(Convert.ToDateTime(drBlock["dteStartDate"]).Year, Convert.ToDateTime(drBlock["dteStartDate"]).Month, Convert.ToDateTime(drBlock["dteStartDate"]).Day, Convert.ToDateTime(drBlock["dteStartTime"]).Hour, Convert.ToDateTime(drBlock["dteStartTime"]).Minute, Convert.ToDateTime(drBlock["dteStartTime"]).Second); }
                            catch { dteStart = DateTime.Now; }

                            try { dteEnd = new DateTime(Convert.ToDateTime(drBlock["dteEndDate"]).Year, Convert.ToDateTime(drBlock["dteEndDate"]).Month, Convert.ToDateTime(drBlock["dteEndDate"]).Day, Convert.ToDateTime(drBlock["dteEndTime"]).Hour, Convert.ToDateTime(drBlock["dteEndTime"]).Minute, Convert.ToDateTime(drBlock["dteEndTime"]).Second); }
                            catch { dteEnd = DateTime.Now; }

                            string strCalendarID = "";

                            try { strCalendarID = ((clsCboItem)cboBlockCalendar.SelectedItem).STRID; }
                            catch { strCalendarID = ""; }

                            subULEvent(strTitle, strDesc, strLocation, strCalendarID, dteStart, dteEnd, lstOutput);
                        }

                        drBlock.Close();
                    }
                }

                conDB.Close();
            }
        }
      
        private void subULEvent(string _strTitle, string _strDesc, string _strLocation, string _strCalendarID, DateTime _dteStart, DateTime _dteEnd)
        {
            subULEvent(_strTitle, _strDesc, _strLocation, _strCalendarID, _dteStart, _dteEnd, null);
        }
            
        private void subULEvent(string _strTitle, string _strDesc, string _strLocation, string _strCalendarID, DateTime _dteStart, DateTime _dteEnd, ListBox _lstStatus)
        {
            try
            {
                Event evtNew = new Event();

                // Set the title and content of the entry.
                evtNew.Summary = _strTitle;
                evtNew.Description = _strDesc;

                // Set a location for the event.
                evtNew.Location = _strLocation;

                EventDateTime dteStart = new EventDateTime();
                EventDateTime dteEnd = new EventDateTime();

                dteStart.DateTime = XmlConvert.ToString(_dteStart);
                dteEnd.DateTime = XmlConvert.ToString(_dteEnd);

                evtNew.Start = dteStart;
                evtNew.End = dteEnd;
                
                // Send the request and receive the response:
                Event evtInsertedEvent = svcCTCal.Events.Insert(evtNew, _strCalendarID).Fetch();

                if (_lstStatus != null)
                    _lstStatus.Items.Insert(0, "Added calendar event: " + evtInsertedEvent.Id);
                
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Console.Write("Error: " + ex.Message);

                if (_lstStatus != null)
                    _lstStatus.Items.Insert(0, "Error adding calendar event: " + ex.Message);
                Application.DoEvents();
            }
        }

        private void btnRefreshCals_Click(object sender, EventArgs e)
        {
            subRefreshCalendars(cboBlockCalendar);
            subRefreshCalendars(cboGGCalendar);
            subRefreshCalendars(cboCCCalendar);

            if (cboBlockCalendar.Items.Count > 0)
                MessageBox.Show("Calendars retrieved successfully!");
        }

        private void subRefreshCalendars(ComboBox _cboToRefresh)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                _cboToRefresh.Items.Clear();

                CalendarList lstCals = svcCTCal.CalendarList.List().Fetch();
                
                foreach (CalendarListEntry cntCalendar in lstCals.Items)
                {
                    clsCboItem itmNew = new clsCboItem(cntCalendar.Id,cntCalendar.Summary);

                    _cboToRefresh.Items.Add(itmNew);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error refreshing the calendar list: " + ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        private void frmGoogleCal_Load(object sender, EventArgs e)
        {
            //get auth for calendar

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblGoogleCalOptions.blnULGG, tblGoogleCalOptions.blnULCC, tblGoogleCalOptions.blnULBlock, " +
                            "tblCampDefaults.strGoogleUName, tblCampDefaults.strGooglePW, tblGoogleCalOptions.strBlockCal, tblGoogleCalOptions.strGGCal, tblGoogleCalOptions.strCCCal " +
                        "FROM tblCampDefaults, tblGoogleCalOptions";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                    {
                        if (drDef.Read())
                        {
                            subRefreshCalendars(cboBlockCalendar);
                            subRefreshCalendars(cboGGCalendar);
                            subRefreshCalendars(cboCCCalendar);

                            string strBlockCal = "";
                            string strGGCal = "";
                            string strCCCal = "";

                            try { strBlockCal = Convert.ToString(drDef["strBlockCal"]); }
                            catch { strBlockCal = ""; }

                            try { strGGCal = Convert.ToString(drDef["strGGCal"]); }
                            catch { strGGCal = ""; }

                            try { strCCCal = Convert.ToString(drDef["strCCCal"]); }
                            catch { strCCCal = ""; }

                            subSetCals(strBlockCal, strGGCal, strCCCal);

                            try { chkIndEvents.Checked = Convert.ToBoolean(drDef["blnULBlock"]); }
                            catch { chkIndEvents.Checked = false; }

                            try { chkGroupRentals.Checked = Convert.ToBoolean(drDef["blnULGG"]); }
                            catch { chkGroupRentals.Checked = false; }

                            try { chkGroupEvents.Checked = Convert.ToBoolean(drDef["blnULCC"]); }
                            catch { chkGroupEvents.Checked = false; }
                        }

                        drDef.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subSetCals(string _strBlockCal, string _strGGCal, string _strCCCal)
        {
            for (int intI = 0; intI < cboBlockCalendar.Items.Count; intI++)
            {
                if (cboBlockCalendar.Items[intI].ToString() == _strBlockCal)
                    cboBlockCalendar.SelectedIndex = intI;
            }

            for (int intI = 0; intI < cboGGCalendar.Items.Count; intI++)
            {
                if (cboGGCalendar.Items[intI].ToString() == _strGGCal)
                    cboGGCalendar.SelectedIndex = intI;
            }

            for (int intI = 0; intI < cboCCCalendar.Items.Count; intI++)
            {
                if (cboCCCalendar.Items[intI].ToString() == _strCCCal)
                    cboCCCalendar.SelectedIndex = intI;
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            subSaveSettings();
        }

        private void subSaveSettings()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblGoogleCalOptions " +
                            "SET blnULGG=@blnULGG, blnULCC=@blnULCC, blnULBlock=@blnULBlock, " +
                            "strBlockCal=@strBlockCal, strGGCal=@strGGCal, strCCCal=@strCCCal";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.Add(new OleDbParameter("@blnULGG", chkGroupRentals.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnULCC", chkGroupEvents.Checked));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnULBlock", chkIndEvents.Checked));

                    string strBlockCal = "";
                    string strGGCal = "";
                    string strCCCal = "";

                    try { strBlockCal = cboBlockCalendar.SelectedItem.ToString(); }
                    catch { strBlockCal = ""; }

                    try { strGGCal = cboGGCalendar.SelectedItem.ToString(); }
                    catch { strGGCal = ""; }

                    try { strCCCal = cboCCCalendar.SelectedItem.ToString(); }
                    catch { strCCCal = ""; }

                    if (strBlockCal == null) strBlockCal = "";
                    if (strGGCal == null) strGGCal = "";
                    if (strCCCal == null) strCCCal = "";

                    cmdDB.Parameters.Add(new OleDbParameter("@strBlockCal", strBlockCal));
                    cmdDB.Parameters.Add(new OleDbParameter("@strGGCal", strGGCal));
                    cmdDB.Parameters.Add(new OleDbParameter("@strCCCal", strCCCal));

                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }
        }        

        private void subPopGGDetails (long _lngGGCCID, ref string _strTitle, ref string _strDesc)
        {
            string strSQL = "";
            string strCCName = "";
            string strCabinList = "";
            string strFirstMeal = "";
            string strLastMeal = "";
            string strNotes = "";
            string strStatus = "";

            int intTotAttCount = 0;
            int intExpAttCount = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT Sum(tblnkGGCCAttendeeStats.intCount) AS intTotalAttendees, Sum(tblnkGGCCAttendeeStats.intCountEst) AS intExpAttCount, " +
                            "tblGGCC.strGGCCName, tblGGCC.mmoGGCCNotes " +
                        "FROM tblGGCC " +
                            "LEFT JOIN tblnkGGCCAttendeeStats ON tblGGCC.lngGGCCID = tblnkGGCCAttendeeStats.lngGGCCID " +
                        "WHERE tblGGCC.lngGGCCID=" + _lngGGCCID + " " +
                        "GROUP BY tblGGCC.strGGCCName, tblGGCC.mmoGGCCNotes";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCC = cmdDB.ExecuteReader())
                    {
                        if (drCC.Read())
                        {
                            try { strCCName = Convert.ToString(drCC["strGGCCName"]); }
                            catch { strCCName = ""; }

                            try { strNotes = Convert.ToString(drCC["mmoGGCCNotes"]); }
                            catch { strNotes = ""; }

                            try { intTotAttCount = Convert.ToInt32(drCC["intTotalAttendees"]); }
                            catch { intTotAttCount = 0; }

                            try { intExpAttCount = Convert.ToInt32(drCC["intExpAttCount"]); }
                            catch { intExpAttCount = 0; }
                        }

                        drCC.Close();
                    }

                    //cabin list
                    strSQL = "SELECT SUM(tblGGCCBeds.intNumBeds) AS intBedCount, " +
                                "tlkpCabinNames.strCabinName " +
                            "FROM ((tblGGCC " +
                                "INNER JOIN tblGGCCHousing ON tblGGCC.lngGGCCID = tblGGCCHousing.lngGGCCID) " +
                                "INNER JOIN tlkpCabinNames ON tblGGCCHousing.lngCabinID = tlkpCabinNames.lngCabinID) " +
                                "LEFT JOIN tblGGCCBeds ON tblGGCCHousing.lngGGCCHousingID = tblGGCCBeds.lngGGCCHousingID " +
                            "WHERE tblGGCC.lngGGCCID=" + _lngGGCCID + " " +
                            "GROUP BY tlkpCabinNames.strCabinName";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drCabins = cmdDB.ExecuteReader())
                    {
                        while (drCabins.Read())
                        {
                            try { strCabinList += Convert.ToString(drCabins["strCabinName"]) + " (" + Convert.ToInt32(drCabins["intBedCount"]) + "), "; }
                            catch { }
                        }

                        if (strCabinList.Length > 2) strCabinList = strCabinList.Substring(0, strCabinList.Length - 2);

                        drCabins.Close();
                    }

                    //status
                    strSQL = "SELECT tlkpGroupStatus.strGroupStatus " +
                            "FROM tblGGCC " +
                                "LEFT JOIN tlkpGroupStatus ON tblGGCC.lngGroupStatusID = tlkpGroupStatus.lngGroupStatusID " +
                            "WHERE tblGGCC.lngGGCCID=" + _lngGGCCID.ToString();

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    try { strStatus = Convert.ToString(cmdDB.ExecuteScalar()); }
                    catch { strStatus = ""; }

                    //meals
                    strSQL = "SELECT tblGGCCActivities.lngGGCCActivityID, " +
                                "tblGGCCActivities.dteActivityDate, tblGGCCActivities.dteActivityTime, " +
                                "tlkpGGCCActivities.strActivityName " +
                            "FROM tblGGCCActivities " +
                                "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID " +
                            "WHERE tblGGCCActivities.lngGGCCID=" + _lngGGCCID + " AND " +
                                "tlkpGGCCActivities.lngActivityTypeID=2 " +
                            "ORDER BY tblGGCCActivities.dteActivityDate, tblGGCCActivities.dteActivityTime";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drMeals = cmdDB.ExecuteReader())
                    {
                        bool blnFirst = true;

                        while (drMeals.Read())
                        {
                            if (blnFirst)
                            {
                                try { strFirstMeal = Convert.ToString(drMeals["strActivityName"]) + " " + Convert.ToDateTime(drMeals["dteActivityDate"]).ToString("M/d/yyyy"); }
                                catch { strFirstMeal = ""; }
                                blnFirst = false;
                            }

                            try { strLastMeal = Convert.ToString(drMeals["strActivityName"]) + " " + Convert.ToDateTime(drMeals["dteActivityDate"]).ToString("M/d/yyyy"); ; }
                            catch { strLastMeal = ""; }
                        }

                        drMeals.Close();
                    }
                }

                conDB.Close();
            }

            _strTitle = strCCName + ": " + strStatus + ", " + intTotAttCount.ToString() + " Attendees (" + intExpAttCount.ToString() + " expected)";

            _strDesc = strCabinList + "\n" +
                "First Meal: " + strFirstMeal + "\n" +
                "Last Meal: " + strLastMeal + "\n" +
                strNotes;

            return;
        }

        private void subPopCCDetails(long _lngGGCCID, ref string _strTitle, ref string _strDesc)
        {
            string strSQL = "";
            string strCCName = "";
            string strCabinList = "";
            string strFirstMeal = "";
            string strLastMeal = "";
            string strNotes = "";
            string strStatus = "";

            int intTotAttCount = 0;
            int intTotReg = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT Count(tblGGCCRegAttendees.lngGGCCRegAttendeeID) AS intTotalAttendees, " +
                            "tblGGCC.strGGCCName, tblGGCC.mmoGGCCNotes " +
                        "FROM (tblGGCC " +
                            "LEFT JOIN tblGGCCRegistrations ON tblGGCC.lngGGCCID = tblGGCCRegistrations.lngGGCCID) " +
                            "LEFT JOIN tblGGCCRegAttendees ON tblGGCCRegistrations.lngGGCCRegistrationID = tblGGCCRegAttendees.lngGGCCRegistrationID " +
                        "WHERE tblGGCC.lngGGCCID=" + _lngGGCCID + " " +
                        "GROUP BY tblGGCC.strGGCCName, tblGGCC.mmoGGCCNotes;";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCC = cmdDB.ExecuteReader())
                    {
                        if (drCC.Read())
                        {
                            try { strCCName = Convert.ToString(drCC["strGGCCName"]); }
                            catch { strCCName = ""; }

                            try { strNotes = Convert.ToString(drCC["mmoGGCCNotes"]); }
                            catch { strNotes = ""; }

                            try { intTotAttCount = Convert.ToInt32(drCC["intTotalAttendees"]); }
                            catch { intTotAttCount = 0; }
                        }

                        drCC.Close();
                    }

                    //reg count
                    strSQL = "SELECT Count(tblGGCCRegistrations.lngGGCCRegistrationID) AS intRegCount " +
                            "FROM tblGGCCRegistrations " +
                            "WHERE tblGGCCRegistrations.lngGGCCID=" + _lngGGCCID.ToString();

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drRegCount = cmdDB.ExecuteReader())
                    {
                        while (drRegCount.Read())
                        {
                            try { intTotReg = Convert.ToInt32(drRegCount["intRegCount"]); }
                            catch { intTotReg = 0; }
                        }

                        drRegCount.Close();
                    }

                    //cabin list
                    strSQL = "SELECT SUM(tblGGCCBeds.intNumBeds) AS intBedCount, " +
                                "tlkpCabinNames.strCabinName " +
                            "FROM ((tblGGCC " +
                                "INNER JOIN tblGGCCHousing ON tblGGCC.lngGGCCID = tblGGCCHousing.lngGGCCID) " +
                                "INNER JOIN tlkpCabinNames ON tblGGCCHousing.lngCabinID = tlkpCabinNames.lngCabinID) " +
                                "LEFT JOIN tblGGCCBeds ON tblGGCCHousing.lngGGCCHousingID = tblGGCCBeds.lngGGCCHousingID " +
                            "WHERE tblGGCC.lngGGCCID=" + _lngGGCCID + " " +
                            "GROUP BY tlkpCabinNames.strCabinName";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drCabins = cmdDB.ExecuteReader())
                    {
                        while (drCabins.Read())
                        {
                            try { strCabinList += Convert.ToString(drCabins["strCabinName"]) + " (" + Convert.ToInt32(drCabins["intBedCount"]) + "), "; }
                            catch { }
                        }

                        if (strCabinList.Length > 2) strCabinList = strCabinList.Substring(0, strCabinList.Length - 2);

                        drCabins.Close();
                    }

                    //status
                    strSQL = "SELECT tlkpGroupStatus.strGroupStatus " +
                            "FROM tblGGCC " +
                                "LEFT JOIN tlkpGroupStatus ON tblGGCC.lngGroupStatusID = tlkpGroupStatus.lngGroupStatusID " +
                            "WHERE tblGGCC.lngGGCCID=" + _lngGGCCID.ToString();

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    try { strStatus = Convert.ToString(cmdDB.ExecuteScalar()); }
                    catch { strStatus = ""; }

                    //meals
                    strSQL = "SELECT tblGGCCActivities.lngGGCCActivityID, " +
                                "tblGGCCActivities.dteActivityDate, tblGGCCActivities.dteActivityTime, " +
                                "tlkpGGCCActivities.strActivityName " +
                            "FROM tblGGCCActivities " +
                                "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID " +
                            "WHERE tblGGCCActivities.lngGGCCID=" + _lngGGCCID + " AND " +
                                "tlkpGGCCActivities.lngActivityTypeID=2 " +
                            "ORDER BY tblGGCCActivities.dteActivityDate, tblGGCCActivities.dteActivityTime";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drMeals = cmdDB.ExecuteReader())
                    {
                        bool blnFirst = true;

                        while (drMeals.Read())
                        {
                            if (blnFirst)
                            {
                                try { strFirstMeal = Convert.ToString(drMeals["strActivityName"]) + " " + Convert.ToDateTime(drMeals["dteActivityDate"]).ToString("M/d/yyyy"); }
                                catch { strFirstMeal = ""; }
                                blnFirst = false;
                            }

                            try { strLastMeal = Convert.ToString(drMeals["strActivityName"]) + " " + Convert.ToDateTime(drMeals["dteActivityDate"]).ToString("M/d/yyyy"); ; }
                            catch { strLastMeal = ""; }
                        }

                        drMeals.Close();
                    }
                }

                conDB.Close();
            }

            if (intTotReg == 1)
                _strTitle = strCCName + ": " + strStatus + ", " + intTotAttCount.ToString() + " Attendees (" + intTotReg.ToString() + " registration)";
            else
                _strTitle = strCCName + ": " + strStatus + ", " + intTotAttCount.ToString() + " Attendees (" + intTotReg.ToString() + " registrations)";

            _strDesc = strCabinList + "\n" +
                "First Meal: " + strFirstMeal + "\n" +
                "Last Meal: " + strLastMeal + "\n" +
                strNotes;

            return;
        }

        private void subPopBlockDetails(long _lngBlockID, ref string _strTitle, ref string _strDesc)
        {
            string strBlockCode = "";
            string strProgram = "";
            string strAgeGroup = "";
            string strGradeGroup = "";
            string strCabinList = "";
            string strSQL = "";

            int intCur = 0;
            int intCap = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT Count(tblRegistrations.lngRegistrationID) AS intCur, tblBlock.intCapacity, " +
                            "tblBlock.strBlockCode, tlkpAgeGroup.strAgeGroup, tlkpGradeGroup.strGradeRange, tlkpCampName.strCampName " +
                        "FROM (((tblBlock " +
                            "LEFT JOIN tlkpAgeGroup ON tblBlock.lngAgeGroupID = tlkpAgeGroup.lngAgeGroupID) " +
                            "LEFT JOIN tlkpGradeGroup ON tblBlock.lngGradeGroupID = tlkpGradeGroup.lngGradeGroupID) " +
                            "LEFT JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID) " +
                            "LEFT JOIN tblRegistrations ON tblBlock.lngBlockID = tblRegistrations.lngBlockID " +
                        "WHERE tblBlock.lngBlockID=" + _lngBlockID.ToString() + " " +
                        "GROUP BY tblBlock.intCapacity, " +
                            "tblBlock.strBlockCode, tlkpAgeGroup.strAgeGroup, tlkpGradeGroup.strGradeRange, tlkpCampName.strCampName";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drBlock = cmdDB.ExecuteReader())
                    {
                        if (drBlock.Read())
                        {
                            try { intCur = Convert.ToInt32(drBlock["intCur"]); }
                            catch { intCur = 0; }

                            try { intCap = Convert.ToInt32(drBlock["intCapacity"]); }
                            catch { intCap = 0; }

                            try { strBlockCode = Convert.ToString(drBlock["strBlockCode"]); }
                            catch { strBlockCode = ""; }

                            try { strAgeGroup = Convert.ToString(drBlock["strAgeGroup"]); }
                            catch { strAgeGroup = ""; }

                            try { strGradeGroup = Convert.ToString(drBlock["strGradeRange"]); }
                            catch { strGradeGroup = ""; }

                            try { strProgram = Convert.ToString(drBlock["strCampName"]); }
                            catch { strProgram = ""; }
                        }

                        drBlock.Close();
                    }

                    //cabin list
                    strSQL = "SELECT tblnkBlockCabins.intBedsUsed AS intBedCount, " +
                                "tlkpCabinNames.strCabinName " +
                            "FROM (tblBlock " +
                                "LEFT JOIN tblnkBlockCabins ON tblBlock.lngBlockID = tblnkBlockCabins.lngBlockID) " +
                                "LEFT JOIN tlkpCabinNames ON tblnkBlockCabins.lngCabinID = tlkpCabinNames.lngCabinID " +
                            "WHERE tblBlock.lngBlockID=" + _lngBlockID.ToString();

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drCabins = cmdDB.ExecuteReader())
                    {
                        while (drCabins.Read())
                        {
                            try { strCabinList += Convert.ToString(drCabins["strCabinName"]) + " (" + Convert.ToInt32(drCabins["intBedCount"]) + "), "; }
                            catch { }
                        }

                        if (strCabinList.Length > 2) strCabinList = strCabinList.Substring(0, strCabinList.Length - 2);

                        drCabins.Close();
                    }

                }

                conDB.Close();
            }

            _strTitle = strBlockCode + " (" + strProgram + "): " + intCur.ToString() + "/" + intCap.ToString();
            _strDesc = strAgeGroup + "/" + strGradeGroup + "\n" +
                    strCabinList;

            return;
        }

        //1=Block
        //2=CC
        //3=GG
        private void btnGGOptions_Click(object sender, EventArgs e)
        {
            using (Admin.frmGoogleCalULOptions objOptions = new global::CTWebMgmt.Admin.frmGoogleCalULOptions(3))
            {
                objOptions.ShowDialog();
            }
        }

        private void btnCCOptions_Click(object sender, EventArgs e)
        {
            using (Admin.frmGoogleCalULOptions objOptions = new global::CTWebMgmt.Admin.frmGoogleCalULOptions(2))
            {
                objOptions.ShowDialog();
            }
        }

        private void btnBlockOptions_Click(object sender, EventArgs e)
        {
            using (Admin.frmGoogleCalULOptions objOptions = new global::CTWebMgmt.Admin.frmGoogleCalULOptions(1))
            {
                objOptions.ShowDialog();
            }
        }

        private void btnClearCals_Click(object sender, EventArgs e)
        {
            subClearCal(cboCCCalendar, lblCCStatus);
            subClearCal(cboGGCalendar, lblGGStatus);
            subClearCal(cboBlockCalendar, lblBlockStatus);

            MessageBox.Show("Calendars cleared!");
        }
    }
}