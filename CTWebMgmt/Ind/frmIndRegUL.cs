using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;

namespace CTWebMgmt.Ind
{
    public partial class frmIndRegUL : Form
    {
        public frmIndRegUL()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUL_Click(object sender, EventArgs e)
        {
            string strSQL;
            string strULRes = "";

            try
            {
                this.Cursor = Cursors.WaitCursor;

                //weeks
                strSQL = "SELECT tlkpWeekDesc.intSortOrder, " +
                            clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, 0 AS lngWeekWebID, tlkpWeekDesc.lngWeekID, " +
                            "tlkpWeekDesc.dteStartDate, tlkpWeekDesc.dteStartTime, tlkpWeekDesc.dteEndDate, tlkpWeekDesc.dteEndTime, " +
                            "tlkpWeekDesc.strWeekDesc " +
                        "FROM (tblBlock " +
                            "INNER JOIN tlkpWeekDesc ON tblBlock.lngWeekID = tlkpWeekDesc.lngWeekID) " +
                            "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID " +
                        "WHERE tlkpCampName.blnUpload=True " +
                        "GROUP BY tlkpWeekDesc.intSortOrder, " +
                            "tlkpWeekDesc.lngWeekID, " +
                            "tlkpWeekDesc.dteStartDate, tlkpWeekDesc.dteStartTime, tlkpWeekDesc.dteEndDate, tlkpWeekDesc.dteEndTime, " +
                            "tlkpWeekDesc.strWeekDesc";

                lstStatus.Items.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Begin Uploading Program Blocks");
                lstStatus.Items.Add("Uploading Weeks...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tlkpWeekDesc", "lngWeekID", "lngWeekWebID", "spAppendWeek", true, "weeks");

                //age groups
                strSQL = "SELECT tlkpAgeGroup.intSortOrder, tlkpAgeGroup.intMinAge, tlkpAgeGroup.intMaxAge, " +
                            clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, 0 AS lngAgeGroupWebID, tlkpAgeGroup.lngAgeGroupID, " +
                            "tlkpAgeGroup.strAgeGroup " +
                        "FROM (tblBlock " +
                            "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tlkpAgeGroup ON tblBlock.lngAgeGroupID = tlkpAgeGroup.lngAgeGroupID " +
                        "WHERE tlkpCampName.blnUpload=True " +
                        "GROUP BY tlkpAgeGroup.intSortOrder, tlkpAgeGroup.intMinAge, tlkpAgeGroup.intMaxAge, " +
                            "tlkpAgeGroup.lngAgeGroupID, " +
                            "tlkpAgeGroup.strAgeGroup";

                lstStatus.Items.Add("Uploading Age Groups...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tlkpAgeGroup", "lngAgeGroupID", "lngAgeGroupWebID", "spAppendAgeGroup", true, "age groups");

                //grade groups
                strSQL = "SELECT tlkpGradeGroup.intMinGrade, tlkpGradeGroup.intMaxGrade, tlkpGradeGroup.intSortOrder, " +
                            clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, 0 AS lngGradeGroupWebID, tlkpGradeGroup.lngGradeGroupID, " +
                            "tlkpGradeGroup.strGradeRange " +
                        "FROM (tblBlock " +
                            "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tlkpGradeGroup ON tblBlock.lngGradeGroupID = tlkpGradeGroup.lngGradeGroupID " +
                        "WHERE tlkpCampName.blnUpload=True " +
                        "GROUP BY tlkpGradeGroup.intMinGrade, tlkpGradeGroup.intMaxGrade, tlkpGradeGroup.intSortOrder, " +
                            "tlkpGradeGroup.lngGradeGroupID, " +
                            "tlkpGradeGroup.strGradeRange";

                lstStatus.Items.Add("Uploading Grade Groups...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tlkpGradeGroup", "lngGradeGroupID", "lngGradeGroupWebID", "spAppendGradeGroup", true, "grade groups");

                //programs
                strSQL = "SELECT tlkpCampName.blnExcludeFromGeneral, " +
                             clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, tlkpCampName.lngCampID, 0 AS lngCampWebID, tlkpCampName.lngProgramTypeID, " +
                            "tlkpCampName.strCampName " +
                        "FROM tblBlock " +
                            "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID " +
                        "WHERE tlkpCampName.blnUpload=True " +
                        "GROUP BY tlkpCampName.blnExcludeFromGeneral, " +
                            "tlkpCampName.lngCampID, tlkpCampName.lngProgramTypeID, " +
                            "tlkpCampName.strCampName " +
                        "HAVING tlkpCampName.lngProgramTypeID=1";

                lstStatus.Items.Add("Uploading Program Types...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tlkpCampName", "lngCampID", "lngCampWebID", "spAppendCampName", true, "program types");

                //housing
                strSQL = "SELECT " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, 0 AS lngHousingWebID, tlkpHousingName.lngHousingID, tlkpHousingName.lngSiteID, tlkpHousingName.lngCampCenterID, " +
                            "tlkpHousingName.strHousingName, IIf(IsNull(tlkpHousingName.strHousingDesc), '', tlkpHousingName.strHousingDesc) AS strHousingDesc " +
                        "FROM (((tblBlock " +
                            "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID) " +
                            "INNER JOIN tblnkBlockCabins ON tblBlock.lngBlockID = tblnkBlockCabins.lngBlockID) " +
                            "INNER JOIN tlkpCabinNames ON tblnkBlockCabins.lngCabinID = tlkpCabinNames.lngCabinID) " +
                            "INNER JOIN tlkpHousingName ON tlkpCabinNames.lngHousingID = tlkpHousingName.lngHousingID " +
                        "WHERE tlkpCampName.blnUpload=True " +
                        "GROUP BY tlkpHousingName.lngHousingID, tlkpHousingName.lngSiteID, tlkpHousingName.lngCampCenterID, " +
                            "tlkpHousingName.strHousingName, tlkpHousingName.strHousingDesc;";

                lstStatus.Items.Add("Uploading Housing...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tlkpHousingName", "lngHousingID", "lngHousingWebID", "spAppendHousing", true, "housing areas");

                //refresh current enrollment counts
                subRefreshBlockStats();

                //blocks
                strSQL = "SELECT tblBlock.intCapacity, tblBlock.intCurrEnrollment, tblBlock.intWaitingList, " +
                            clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, 0 AS lngBlockWebID, tblBlock.lngBlockID, tblBlock.lngWeekID, tblBlock.lngAgeGroupID, tblBlock.lngCampID,  First(IIf(IsNull([tlkpCabinNames].[lngHousingID]),0,[tlkpCabinNames].[lngHousingID])) AS lngHousingID, tblBlock.lngGenderID, tblBlock.lngGradeGroupID, " +
                            "tblBlock.curCharge, tblBlock.curMinDep, " +
                            "tblBlock.strBlockCode " +
                        "FROM ((tblBlock " +
                            "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID) " +
                            "LEFT JOIN tblnkBlockCabins ON tblBlock.lngBlockID = tblnkBlockCabins.lngBlockID) " +
                            "LEFT JOIN tlkpCabinNames ON tblnkBlockCabins.lngCabinID = tlkpCabinNames.lngCabinID " +
                        "WHERE tlkpCampName.blnUpload=True " +
                        "GROUP BY tblBlock.intCapacity, tblBlock.intCurrEnrollment, tblBlock.intWaitingList, " +
                            "tblBlock.lngBlockID, tblBlock.lngWeekID, tblBlock.lngAgeGroupID, tblBlock.lngCampID, tblBlock.lngGenderID, tblBlock.lngGradeGroupID, tblBlock.lngRecordID, tblBlock.lngSiteID, tblBlock.lngBlockAliasID, tblBlock.lngCampCenterID, " +
                            "tblBlock.curCharge, tblBlock.curMinDep, " +
                            "tblBlock.strBlockCode;";

                lstStatus.Items.Add("Uploading Blocks...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tblBlock", "lngBlockID", "lngBlockWebID", "spAppendBlock", true, "blocks");

                //reg holds
                strSQL = "SELECT tblRegHold.blnSharedCostPercent, " +
                            "IIf(IsNull(tblRegHold.lngRegHoldWebID), 0, tblRegHold.lngRegHoldWebID) AS lngRegHoldWebID, " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AS lngCTUserID, tblRegHold.lngRegHoldID, tblRegHold.intHoldQty, tblRegHold.lngBlockID, tblRegHold.lngRecordID, " +
                            "tblRegHold.curCostShare, tblRegHold.curRegCharge, " +
                            "tblRegHold.dteDeadline, tblRegHold.dteDateCreated, " +
                            "tblRegHold.strOrgCode " +
                        "FROM tblRegHold";

                lstStatus.Items.Add("Uploading Registration Holds...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnULWithUpdate(strSQL, "tblRegHold", "lngRegHoldID", "lngRegHoldWebID", true, "registration holds");

                //referred by
                strSQL = "SELECT " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                            "tblReferredBy.strReferredBy " +
                        "FROM tblReferredBy " +
                        "ORDER BY tblReferredBy.strReferredBy;";

                lstStatus.Items.Add("Referred By Options...");
                Application.DoEvents();

                strULRes += clsWebTalk.fcnUploadData(strSQL, "tblReferredBy", "strReferredBy", "", "spAppendReferredBy", true, "string", "referred by options");

                lstStatus.Items.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Complete");

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
                clsErr.subLogErr("frmIndRegUL.btnUL", ex);
            }

            this.Cursor = Cursors.Default;
        }

        private void subRefreshBlockStats()
        {
            List<long> lngBlockIDs = new List<long>();

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblBlock.lngBlockID " +
                      "FROM tblBlock " +
                          "INNER JOIN tlkpCampName ON tblBlock.lngCampID = tlkpCampName.lngCampID " +
                      "WHERE tlkpCampName.blnUpload=True";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drBlocks = cmdDB.ExecuteReader())
                    {
                        while (drBlocks.Read())
                        {
                            long lngBlockID = 0;

                            try { lngBlockID = Convert.ToInt32(drBlocks["lngBlockID"]); }
                            catch { lngBlockID = 0; }

                            lngBlockIDs.Add(lngBlockID);
                        }

                        drBlocks.Close();
                    }

                    for (int intI = 0; intI < lngBlockIDs.Count; intI++)
                    {
                        long lngBlockID = lngBlockIDs[intI];

                        //recalc vals for block
                        int intRemainingHolds = fcnRemainingHolds(cmdDB, lngBlockID);
                        int intRegCount = fcnRegCount(cmdDB, lngBlockID);
                        int intPending1stChoice = clsIndCRUD.fcnPending1stChoice(cmdDB, lngBlockID);
                        int intCurrentEnrollment = 0;
                        int intWaitingListCount = fcnWaitingListCount(cmdDB, lngBlockID);

                        intCurrentEnrollment = intRemainingHolds + intRegCount + intPending1stChoice;

                        //update values in block
                        strSQL = "UPDATE tblBlock " +
                                "SET tblBlock.intCurrEnrollment = " + intCurrentEnrollment.ToString() + ", tblBlock.intWaitingList = " + intWaitingListCount.ToString() + " " +
                                "WHERE tblBlock.lngBlockID=" + lngBlockID.ToString();

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }
                }

                conDB.Close();
            }
        }

        private int fcnRemainingHolds(OleDbCommand _cmdDB, long _lngBlockID)
        {
            int intRes = 0;

            int intHoldQty = 0;
            int intUsedHolds = 0;

            string strSQL = "";

            strSQL = "SELECT Sum(tblRegHold.intHoldQty) AS intHoldQty " +
                    "FROM tblRegHold " +
                    "WHERE tblRegHold.lngBlockID=" + _lngBlockID.ToString() + " AND " +
                        "DateDiff('d', Now(), tblRegHold.dteDeadline) > 0 AND " +
                        "tblRegHold.lngRegHoldID Is Not Null";

            _cmdDB.Parameters.Clear();
            _cmdDB.CommandText = strSQL;

            try { intHoldQty = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
            catch { intHoldQty = 0; }

            if (intHoldQty > 0)
            {
                strSQL = "SELECT Count(tblRegistrations.lngRegistrationID) AS intUsedHolds " +
                        "FROM tblRegHold " +
                            "LEFT JOIN tblRegistrations ON tblRegHold.lngRegHoldID = tblRegistrations.lngRegHoldID " +
                        "WHERE tblRegHold.lngBlockID=" + _lngBlockID.ToString() + " AND " +
                            "DateDiff('d', Now(), tblRegHold.dteDeadline) >= 0 AND " +
                            "tblRegHold.lngRegHoldID Is Not Null";

                _cmdDB.Parameters.Clear();
                _cmdDB.CommandText = strSQL;

                try { intUsedHolds = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
                catch { intUsedHolds = 0; }
            }
            else
            {
                intHoldQty = 0;
                intUsedHolds = 0;
            }

            intRes = intHoldQty - intUsedHolds;

            return intRes;
        }

        private int fcnRegCount(OleDbCommand _cmdDB, long _lngBlockID)
        {
            int intRes = 0;

            string strSQL = "";

            strSQL = "SELECT Count(tblRegistrations.lngRegistrationID) AS intRegCount " +
                    "FROM tblRegistrations " +
                    "WHERE tblRegistrations.lngBlockID=" + _lngBlockID.ToString();

            _cmdDB.Parameters.Clear();
            _cmdDB.CommandText = strSQL;

            try { intRes = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
            catch { intRes = 0; }

            return intRes;
        }

        private int fcnWaitingListCount(OleDbCommand _cmdDB, long _lngBlockID)
        {
            int intRes = 0;

            string strSQL = "";

            strSQL = "SELECT Count(tblWaitingList.lngWaitID) AS intWaitCount " +
                    "FROM tblWaitingList " +
                    "WHERE tblWaitingList.lngBlockID=" + _lngBlockID.ToString();

            _cmdDB.Parameters.Clear();
            _cmdDB.CommandText = strSQL;

            try { intRes = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
            catch { intRes = 0; }

            return intRes;
        }
    }
}