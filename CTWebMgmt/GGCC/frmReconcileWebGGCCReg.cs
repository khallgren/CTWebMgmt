using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CTWebMgmt.GGCC
{
    public partial class frmReconcileWebGGCCReg : Form
    {
        public clsGlobalEnum.conCheckExistingRegRes lngRes;
        private long lngGGCCRegWebID;

        public frmReconcileWebGGCCReg(long _lngGGCCRegWebID)
        {
            InitializeComponent();

            lngGGCCRegWebID = _lngGGCCRegWebID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lngRes = clsGlobalEnum.conCheckExistingRegRes.Cancel;
            this.Close();
        }

        private void frmReconcileWebGGCCReg_Load(object sender, EventArgs e)
        {
            //fill controls
            try
            {
                OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
                OleDbCommand objCommand;
                OleDbDataAdapter daGGCCReg;
                OleDbDataReader drGGCCReg;

                string strSQL;

                objConn.Open();
                
                //open recordset of existing, new details
                strSQL = "SELECT tblWebGGCCRegistrations.lngGGCCRegistrationWebID, tblWebRecordsGGCCReg.lngStateID AS lngStateIDNew, " +
                            "tblGGCC.dteStartDate, tblGGCCRegistrations.dteDateRegistered AS dteDateRegisteredOld, tblWebGGCCRegistrations.dteLastModified AS dteLastModifiedNew, " +
                            "tblGGCC.strGGCCName, tblRecords.strFirstName AS strFirstNameOld, tblRecords.strLastCoName AS strLastCoNameOld, tblRecords.strCompanyName AS strCompanyNameOld, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORAddress1,tblRecords.strAddress) AS AddressOld, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORCity,tblRecords.strCity) AS CityOld, IIf(tblRecords.blnUseMORAddress=True,tlkpStates_1.strState,tlkpStates.strState) AS StateOld, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORZip,tblRecords.strZip) AS ZipOld, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORPhone,tblRecords.strHomePhone) AS HomePhoneOld, tblRecords.strWorkPhone AS strWorkPhoneOld, tblRecords.strCellPhone AS strCellPhoneOld, tblRecords.strEmail AS strEmailOld, tlkpPaymentType.strPaymentType AS strPaymentTypeOld, tblTransactions.strBankName AS strBankNameOld, tblTransactions.strAcctNum AS strAcctNumOld, tblTransactions.strRoutingNum AS strRoutingNumOld, tblTransactions.strCCNumber AS strCCNumberOld, tblTransactions.strCCValCode AS strCCValCodeOld, tblWebRecordsGGCCReg.strCompanyName AS strCompanyNameNew, tblWebRecordsGGCCReg.strFirstName AS strFirstNameNew, tblWebRecordsGGCCReg.strLastCoName AS strLastCoNameNew, tblWebRecordsGGCCReg.strAddress AS strAddressNew, tblWebRecordsGGCCReg.strCity AS strCityNew, tblWebRecordsGGCCReg.strZip AS strZipNew, tblWebRecordsGGCCReg.strHomePhone AS strHomePhoneNew, tblWebRecordsGGCCReg.strWorkPhone AS strWorkPhoneNew, tblWebRecordsGGCCReg.strCellPhone AS strCellPhoneNew, tblWebRecordsGGCCReg.strEmail AS strEmailNew, tblWebGGCCRegistrations.strPaymentType AS strPaymentTypeNew, tblWebGGCCRegistrations.strBankName AS strBankNameNew, tblWebGGCCRegistrations.strAcctNum AS strAcctNumNew, tblWebGGCCRegistrations.strRoutingNum AS strRoutingNumNew, tblWebGGCCRegistrations.strCardType AS strCardTypeNew, tblWebGGCCRegistrations.strCardNum AS strCardNumNew, tblWebGGCCRegistrations.strCVV2 AS strCVV2New " +
                        "FROM ((((((((tblGGCCRegistrations " +
                            "INNER JOIN tblWebGGCCRegistrations ON tblGGCCRegistrations.lngGGCCRegistrationWebID = tblWebGGCCRegistrations.lngGGCCRegistrationWebID) " +
                            "INNER JOIN tblWebRecordsGGCCReg ON tblWebGGCCRegistrations.lngRecordWebID = tblWebRecordsGGCCReg.lngRecordWebID) " +
                            "INNER JOIN tblRecords ON tblGGCCRegistrations.lngRecordID = tblRecords.lngRecordID) " +
                            "INNER JOIN tblGGCC ON tblWebGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID) " +
                            "LEFT JOIN tblTransactions ON tblGGCCRegistrations.lngGGCCRegistrationID = tblTransactions.lngGGCCRegistrationID) " +
                            "LEFT JOIN tlkpPaymentType ON tblTransactions.lngPaymentTypeID = tlkpPaymentType.lngPaymentTypeID) " +
                            "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                            "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                            "LEFT JOIN tlkpStates AS tlkpStates_1 ON tblMOR.lngMORStateID = tlkpStates_1.lngStateID " +
                        "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " AND " +
                            "(tblTransactions.lngTransTypeID=8 OR " +
                                "tblTransactions.lngTransTypeID Is Null);";

                objCommand = new OleDbCommand(strSQL, objConn);

                drGGCCReg = objCommand.ExecuteReader();

                if (drGGCCReg.Read())
                {
                    txtGGCCRegistrationWebID.Text = drGGCCReg["lngGGCCRegistrationWebID"].ToString();
                    txtStateNew.Text= drGGCCReg["lngStateIDNew"].ToString();
                    txtStartDate.Text= drGGCCReg["dteStartDate"].ToString();
                    txtRegDateOld.Text = drGGCCReg["dteDateRegisteredOld"].ToString();
                    txtRegDateNew.Text = drGGCCReg["dteLastModifiedNew"].ToString();
                    txtGGCCName.Text = drGGCCReg["strGGCCName"].ToString();
                    txtFNameOld.Text = drGGCCReg["strFirstNameOld"].ToString();
                    txtLNameOld.Text = drGGCCReg["strLastCoNameOld"].ToString();
                    txtCompanyNameOld.Text = drGGCCReg["strCompanyNameOld"].ToString();
                    txtAddressOld.Text = drGGCCReg["AddressOld"].ToString();
                    txtCityOld.Text = drGGCCReg["CityOld"].ToString();
                    txtStateOld.Text = drGGCCReg["StateOld"].ToString();
                    txtZipOld.Text = drGGCCReg["ZipOld"].ToString();
                    txtHomePhoneOld.Text = drGGCCReg["HomePhoneOld"].ToString();
                    txtWorkPhoneOld.Text = drGGCCReg["strWorkPhoneOld"].ToString();
                    txtCellPhoneOld.Text = drGGCCReg["strCellPhoneOld"].ToString();
                    txtEMailOld.Text = drGGCCReg["strEmailOld"].ToString();
                    txtPmtTypeOld.Text = drGGCCReg["strPaymentTypeOld"].ToString();
                    txtBankNameOld.Text = drGGCCReg["strBankNameOld"].ToString();
                    txtAcctNumOld.Text = drGGCCReg["strAcctNumOld"].ToString();
                    txtRoutingNumOld.Text = drGGCCReg["strRoutingNumOld"].ToString();
                    txtCardNumOld.Text = drGGCCReg["strCCNumberOld"].ToString();
                    txtCVV2Old.Text = drGGCCReg["strCCValCodeOld"].ToString();
                    txtCompanyNameNew.Text = drGGCCReg["strCompanyNameNew"].ToString();
                    txtFNameNew.Text = drGGCCReg["strFirstNameNew"].ToString();
                    txtLNameNew.Text = drGGCCReg["strLastCoNameNew"].ToString();
                    txtAddressNew.Text = drGGCCReg["strAddressNew"].ToString();
                    txtCityNew.Text = drGGCCReg["strCityNew"].ToString();
                    txtZipNew.Text = drGGCCReg["strZipNew"].ToString();
                    txtHomePhoneNew.Text = drGGCCReg["strHomePhoneNew"].ToString();
                    txtWorkPhoneNew.Text = drGGCCReg["strWorkPhoneNew"].ToString();
                    txtCellPhoneNew.Text = drGGCCReg["strCellPhoneNew"].ToString();
                    txtEMailNew.Text = drGGCCReg["strEmailNew"].ToString();
                    txtPmtTypeNew.Text = drGGCCReg["strPaymentTypeNew"].ToString();
                    txtBankNameNew.Text = drGGCCReg["strBankNameNew"].ToString();
                    txtAcctNumNew.Text = drGGCCReg["strAcctNumNew"].ToString();
                    txtRoutingNumNew.Text = drGGCCReg["strRoutingNumNew"].ToString();
                    txtCardTypeNew.Text = drGGCCReg["strCardTypeNew"].ToString();
                    txtCardNumNew.Text = drGGCCReg["strCardNumNew"].ToString();
                    txtCVV2New.Text = drGGCCReg["strCVV2New"].ToString();
                }

                drGGCCReg.Close();

                //fill 4 grids
                daGGCCReg = new OleDbDataAdapter(objCommand);
                
                //existing activities
                strSQL = "SELECT tblGGCCRegActivities.lngGGCCActivityID, " +
                            "tlkpGGCCActivities.strActivityName, Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\") AS dteActivityDateTime, Sum(tblGGCCRegActivities.intParticipants) AS intParticipants " +
                        "FROM ((tblGGCCRegistrations " +
                            "INNER JOIN tblGGCCRegActivities ON tblGGCCRegistrations.lngGGCCRegistrationID = tblGGCCRegActivities.lngGGCCRegistrationID) " +
                            "INNER JOIN tblGGCCActivities ON tblGGCCRegActivities.lngGGCCActivityID = tblGGCCActivities.lngGGCCActivityID) " +
                            "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID " +
                        "WHERE tblGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                        "GROUP BY tblGGCCRegActivities.lngGGCCActivityID, " +
                            "tlkpGGCCActivities.strActivityName, Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\") " +
                        "ORDER BY Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\");";

                BindingSource srcActivitiesOld = new BindingSource();

                grdActivitiesOld.DataSource = srcActivitiesOld;

                objCommand.CommandText = strSQL;

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblActivitiesOld = new DataTable();

                tblActivitiesOld.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daGGCCReg.Fill(tblActivitiesOld);

                srcActivitiesOld.DataSource = tblActivitiesOld;

                grdActivitiesOld.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                //new activities
                strSQL = "SELECT tblWebGGCCRegActivities.lngGGCCActivityID, tlkpGGCCActivities.strActivityName, Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\") AS dteActivityDateTime, Sum(tblWebGGCCRegActivities.intParticipants) AS intParticipants " +
                        "FROM (tblWebGGCCRegActivities " +
                            "INNER JOIN tblGGCCActivities ON tblWebGGCCRegActivities.lngGGCCActivityID = tblGGCCActivities.lngGGCCActivityID) " +
                            "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID " +
                        "WHERE tblWebGGCCRegActivities.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                        "GROUP BY tblWebGGCCRegActivities.lngGGCCActivityID, " +
                            "tlkpGGCCActivities.strActivityName, Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\") " +
                        "ORDER BY Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\")";

                BindingSource srcActivitiesNew = new BindingSource();

                grdActivitiesNew.DataSource = srcActivitiesNew;

                objCommand.CommandText = strSQL;

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblActivitiesNew = new DataTable();

                tblActivitiesNew.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daGGCCReg.Fill(tblActivitiesNew);

                srcActivitiesNew.DataSource = tblActivitiesNew;

                grdActivitiesNew.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                //existing attendees
                strSQL = "SELECT " +
                            "(SELECT Count(tblGGCCRegAttendees.lngGGCCRegAttendeeID) AS intMCount " +
                            "FROM tblGGCCRegAttendees " +
                            "WHERE tblGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                "tblGGCCRegAttendees.intGender=-1 AND " +
                                "tblGGCCRegAttendees.lngGGCCRegistrationID=tblGGCCRegistrations.lngGGCCRegistrationID " +
                            "GROUP BY tblGGCCRegAttendees.lngGGCCAttendeeStatsID, tblGGCCRegAttendees.lngGGCCRegistrationID) AS intMCount, " +
                            "(SELECT Count(tblGGCCRegAttendees.lngGGCCRegAttendeeID) AS intFCount " +
                            "FROM tblGGCCRegAttendees " +
                            "WHERE tblGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                "tblGGCCRegAttendees.intGender=0 AND " +
                                "tblGGCCRegAttendees.lngGGCCRegistrationID=tblGGCCRegistrations.lngGGCCRegistrationID " +
                            "GROUP BY tblGGCCRegAttendees.lngGGCCAttendeeStatsID, tblGGCCRegAttendees.lngGGCCRegistrationID) AS intFCount, " +
                            "(SELECT Count(tblGGCCRegAttendees.lngGGCCRegAttendeeID) AS intNACount " +
                            "FROM tblGGCCRegAttendees " +
                            "WHERE tblGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                "tblGGCCRegAttendees.intGender=3 AND " +
                                "tblGGCCRegAttendees.lngGGCCRegistrationID=tblGGCCRegistrations.lngGGCCRegistrationID " +
                            "GROUP BY tblGGCCRegAttendees.lngGGCCAttendeeStatsID, tblGGCCRegAttendees.lngGGCCRegistrationID) AS intNACount, " +
                            "tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID, " +
                            "[tlkpGuestTypes].[strGuestType] & \", \" & [tblCabinCategories].[strCabinCategory] AS strGuestType " +
                            "FROM ((((tblGGCC " +
                                "INNER JOIN tblnkGGCCAttendeeStats ON tblGGCC.lngGGCCID = tblnkGGCCAttendeeStats.lngGGCCID) " +
                                "INNER JOIN tblAdvRateTypes ON tblnkGGCCAttendeeStats.lngAdvRateTypeID = tblAdvRateTypes.lngAdvRateTypeID) " +
                                "INNER JOIN tblCabinCategories ON tblAdvRateTypes.lngCabinCategoryID = tblCabinCategories.lngCabinCategoryID) " +
                                "INNER JOIN tlkpGuestTypes ON tblAdvRateTypes.lngGuestTypeID = tlkpGuestTypes.lngGuestTypeID) " +
                                "INNER JOIN tblGGCCRegistrations ON tblGGCC.lngGGCCID = tblGGCCRegistrations.lngGGCCID " +
                            "WHERE tblGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                            "ORDER BY tblCabinCategories.strCabinCategory, tlkpGuestTypes.strGuestType;";
                
                BindingSource srcAttOld = new BindingSource();

                grdAttendeesOld.DataSource = srcAttOld;

                objCommand.CommandText = strSQL;

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblAttOld = new DataTable();

                tblAttOld.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daGGCCReg.Fill(tblAttOld);

                srcAttOld.DataSource = tblAttOld;

                grdAttendeesOld.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                //new attendees
                strSQL = "SELECT " +
                            "(SELECT Count(tblWebGGCCRegAttendees.lngGGCCRegAttendeeWebID) AS intMCount " +
                            "FROM tblWebGGCCRegAttendees " +
                            "WHERE tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                "tblWebGGCCRegAttendees.intGender=-1 AND " +
                                "tblWebGGCCRegAttendees.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                            "GROUP BY tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID, tblWebGGCCRegAttendees.lngGGCCRegistrationWebID " +
                            ") AS intMCount, " +
                            "(SELECT Count(tblWebGGCCRegAttendees.lngGGCCRegAttendeeWebID) AS intFCount " +
                            "FROM tblWebGGCCRegAttendees " +
                            "WHERE tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                "tblWebGGCCRegAttendees.intGender=0 AND " +
                                "tblWebGGCCRegAttendees.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                            "GROUP BY tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID, tblWebGGCCRegAttendees.lngGGCCRegistrationWebID " +
                            ") AS intFCount, " +
                            "(SELECT Count(tblWebGGCCRegAttendees.lngGGCCRegAttendeeWebID) AS intNACount " +
                            "FROM tblWebGGCCRegAttendees " +
                            "WHERE tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                "tblWebGGCCRegAttendees.intGender=3 AND " +
                                "tblWebGGCCRegAttendees.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                            "GROUP BY tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID, tblWebGGCCRegAttendees.lngGGCCRegistrationWebID " +
                            ") AS intNACount, " +
                            "tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID, " +
                            "[tlkpGuestTypes].[strGuestType] & \", \" & [tblCabinCategories].[strCabinCategory] AS strGuestType " +
                        "FROM ((((tblWebGGCCRegistrations " +
                            "INNER JOIN tblGGCC ON tblWebGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID) " +
                            "INNER JOIN tblnkGGCCAttendeeStats ON tblGGCC.lngGGCCID = tblnkGGCCAttendeeStats.lngGGCCID) " +
                            "INNER JOIN tblAdvRateTypes ON tblnkGGCCAttendeeStats.lngAdvRateTypeID = tblAdvRateTypes.lngAdvRateTypeID) " +
                            "INNER JOIN tblCabinCategories ON tblAdvRateTypes.lngCabinCategoryID = tblCabinCategories.lngCabinCategoryID) " +
                            "INNER JOIN tlkpGuestTypes ON tblAdvRateTypes.lngGuestTypeID = tlkpGuestTypes.lngGuestTypeID " +
                        "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                        "ORDER BY tblCabinCategories.strCabinCategory, tlkpGuestTypes.strGuestType;";

                BindingSource srcAttNew = new BindingSource();

                grdAttendeesNew.DataSource = srcAttNew;

                objCommand.CommandText = strSQL;

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblAttNew = new DataTable();

                tblAttNew.Locale = System.Globalization.CultureInfo.InvariantCulture;

                daGGCCReg.Fill(tblAttNew);

                srcAttNew.DataSource = tblAttNew;

                grdAttendeesNew.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                objConn.Close();

                drGGCCReg.Dispose();
                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmReconcileWebGGCCReg.Load", ex);
            }
        }
    }
}