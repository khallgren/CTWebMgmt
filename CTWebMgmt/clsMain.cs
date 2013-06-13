using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace CTWebMgmt
{
    public class CTWebMgmt
    {
        public const string strUpdateVersion = "KH20130227";
        public static string strCTConn;
        public static string strRegKey = "Software/VB and VBA Program Settings/CampTrak/CTDefaults";

        public static long lngUserID;
        public static long lngWSID;
        public static long lngCTUserID;

        public static bool blnUseSQLServer;

        [STAThread]
        public static void Main()
        {
            //Xceed.DockingWindows.Licenser.LicenseKey = "DWN10-DF7YB-F7UBP-1ABA";
            //Xceed.Grid.Licenser.LicenseKey = "GRD25-G17KB-F7BMP-U4JA";
            //Xceed.Editors.Licenser.LicenseKey = "EDN10-PF7YB-E7BUD-1AAA";
            Xceed.SmartUI.Licenser.LicenseKey = "SUN33-LF7DB-1WBBP-34CA";

            //Telerik.WinControls.Themes.BreezeTheme theme = new Telerik.WinControls.Themes.BreezeTheme();
            //theme.DeserializeTheme();
            //Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Breeze";

            if (!fcnSetWSVars())
            {
                MessageBox.Show("This is the first time you have run this program, please fill in the following setup screens.");

                frmSystemSetup frmSetup = new frmSystemSetup();

                Application.Run(frmSetup);
            }

            bool blnMainGood;

            if (blnUseSQLServer)
                blnMainGood = fcnBuildSQLConnect(strRegKey, ref clsAppSettings.GetAppSettings().strCTConn);
            else
                blnMainGood = fcnRelink("ctmain_b.mdb", strRegKey, "MainPath", ref clsAppSettings.GetAppSettings().strCTConn);

            if (!blnMainGood)
            {
                MessageBox.Show("Cannot open system without locating data files!");

                frmSystemSetup frmSetup = new frmSystemSetup();

                Application.Run(frmSetup);
                MessageBox.Show("Please restart the program");
                return;
            }

            clsAppSettings.GetAppSettings().subInitSettings();

            subStructureUpdateMDB();

            clsNav.objLogin = new frmLogin();

            Application.Run(clsNav.objLogin);

        }

        private static bool fcnSetWSVars()
        {
            //this function tries to set the workstation settings (WSID, use sql server)
            //it returns true if this is the first run of the program

            try
            {

                lngWSID = Settings.Default.lngWSID;

                blnUseSQLServer = Settings.Default.UseSQLServer;

                if (!Settings.Default.FirstSetup)
                {
                    Settings.Default.FirstSetup = true;
                    Settings.Default.Save();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subSetWSVars", ex);
                return false;
            }

        }

        public static bool fcnBuildSQLConnect(string strRegKey, ref string strConn)
        {
            OleDbConnection objConn;

            string strServer = "";
            string strDatabase = "";
            string strUsername = "";
            string strPassword = "";

            strServer = Settings.Default.SQLServer;
            strDatabase = Settings.Default.SQLDatabase;
            strUsername = Settings.Default.SQLUserName;
            strPassword = Settings.Default.SQLPassword;

            strConn = "Provider=sqloledb;Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";User Id=" + strUsername + ";Password=" + strPassword + ";";

            try
            {

                objConn = new OleDbConnection();

                objConn.ConnectionString = strConn;

                objConn.Open();
                objConn.Close();

                return true;
            }
            catch (Exception ex)
            {
                strConn = "failed";
                clsErr.subLogErr("fcnBuildSQLConnect", ex);
                return false;
            }

        }

        public static bool fcnRelink(string strFile, string strRegKey, string strRegistry, ref string strConn)
        {
            OleDbConnection objConn;

            string strMsg = "";

            bool blnRes;

            try
            {
                strFile = Settings.Default[strRegistry].ToString();

                if (strFile == "")
                {
                    return false;
                }

                if (System.IO.File.Exists(strFile))
                {
                    if (strFile.EndsWith("accdb"))
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strFile + "; User Id=admin; Password=";
                    else
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFile + "; User Id=admin; Password=";
                }
                else
                {
                    strFile = fcnFindFile("Find ctMain_b.mdb file:");

                    if (strFile.EndsWith("accdb"))
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strFile + "; User Id=admin; Password=";
                    else
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFile + "; User Id=admin; Password=";
                }

                objConn = new OleDbConnection();

                try
                {
                    objConn.ConnectionString = strConn;

                    objConn.Open();
                    objConn.Close();

                    blnRes = true;
                }
                catch (Exception ex)
                {

                    strMsg = "Error on relink: " + ex.Message + "\r" +
                            "Connection: " + strConn;

                    MessageBox.Show(strMsg);

                    strFile = fcnFindFile("Find ctMain_b.mdb file:");

                    if (strFile.EndsWith("accdb"))
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strFile + "; User Id=admin; Password=";
                    else
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFile + "; User Id=admin; Password=";

                    try
                    {
                        objConn.ConnectionString = strConn;

                        objConn.Open();
                        objConn.Close();

                        blnRes = true;
                    }
                    catch (Exception ex2)
                    {
                        blnRes = false;

                        strMsg = "Error (2) on relink: " + ex2.Message + "\r" +
                                "Connection: " + strConn;

                        MessageBox.Show(strMsg);

                    }

                }

                objConn.Dispose();

                if (blnRes)
                {
                    Settings.Default[strRegistry] = strFile;
                    Settings.Default.Save();
                }

                return blnRes;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("fcnRelink", ex);
                return false;
            }
        }

        private static void subStructureUpdateMDB()
        {
            string strErr = "";

            try
            {
                string strSQL = "";

                bool blnAddFields = false;

                using (OleDbConnection conCT = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conCT.Open();

                    strSQL = "SELECT strInformalSal " +
                            "FROM tblWebRecords";

                    using (OleDbCommand cmdCT = new OleDbCommand(strSQL, conCT))
                    {
                        blnAddFields = false;

                        try
                        {
                            cmdCT.CommandText = strSQL;

                            using (OleDbDataReader drSal = cmdCT.ExecuteReader(System.Data.CommandBehavior.SchemaOnly))
                            {
                                System.Data.DataTable dtSchema = drSal.GetSchemaTable();

                                int intSize = Convert.ToInt32(dtSchema.Rows[0]["ColumnSize"]);

                                if (intSize < 50) blnAddFields = true;

                                drSal.Close();
                            }
                        }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "ALTER TABLE tblWebRecords ALTER COLUMN strInformalSal TEXT(50)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        blnAddFields = false;

                        try
                        {
                            strSQL = "SELECT strHomePhone " +
                                    "FROM tblWebRecords";

                            cmdCT.CommandText = strSQL;

                            using (OleDbDataReader drHomePhone = cmdCT.ExecuteReader(System.Data.CommandBehavior.SchemaOnly))
                            {
                                System.Data.DataTable dtSchema = drHomePhone.GetSchemaTable();

                                int intSize = Convert.ToInt32(dtSchema.Rows[0]["ColumnSize"]);

                                if (intSize < 50) blnAddFields = true;

                                drHomePhone.Close();
                            }
                        }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "ALTER TABLE tblWebRecords ALTER COLUMN strHomePhone TEXT(50)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strEncPassPhrase", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strEncPassPhrase:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebGift", "strAuthNum", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strAuthNum:\n" + strErr);
                        blnAddFields = false;
                        if (!fcnAddField(cmdCT, "tblWebGift", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strPNRef:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebRecords", "blnParent", "YESNO", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.blnParent:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "blnCamper", "YESNO", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.blnCamper:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "intGradeCompleted", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.intGradeCompleted:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "intYearofGraduation", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.intYearofGraduation:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "lngConfMethodID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.lngConfMethodID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "lngProfileWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.lngProfileWebID:\n" + strErr);

                        //tables for ind event registrations
                        blnAddFields = false;

                        strSQL = "SELECT lngRegistrationBlockChoiceID " +
                                "FROM tblWebIndRegBlockChoices";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblWebIndRegBlockChoices " +
                                    "(lngRegistrationBlockChoiceID LONG CONSTRAINT pkRegBlockChoice PRIMARY KEY, " +
                                        "lngCTUserID LONG, " +
                                        "lngRegistrationWebID LONG, " +
                                        "lngBlockID LONG, " +
                                        "lngChoice LONG)";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();

                            string strRegFlags = "";
                            string strRegFields = "";

                            for (int intI = 1; intI <= 30; intI++)
                            {
                                strRegFlags += "blnRegFlag" + intI.ToString() + " YESNO NULL, ";
                                strRegFields += "strRegField" + intI.ToString() + " TEXT(50), ";
                            }

                            strSQL = "CREATE TABLE tblWebIndRegistrations " +
                                    "( blnSpecialNeeds YESNO NULL, " + strRegFlags +
                                        "lngRegistrationWebID INT NOT NULL, lngRecordWebID int NOT NULL, lngConfMethodID int NULL, " +
                                        "curDeposit CURRENCY NULL , curDonation CURRENCY NULL , curSpendingMoney CURRENCY NULL , " +
                                        "dteRegistrationDate datetime NULL , dteCreated datetime NULL , " +
                                        "strBuddy1 TEXT (255)  NULL , strBuddy2 TEXT (255)  NULL , strReleaseTo TEXT (255)  NULL , strConfEmail TEXT (255) NULL , strReferredBy TEXT (50) NULL, strXCTransID TEXT (50)  NULL, strXCAlias TEXT (25) NULL, " + strRegFields +
                                        "strDependNotes MEMO NULL, mmoSpecialNeeds MEMO NULL, mmoRegNotes MEMO NULL )";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();

                            strSQL = "CREATE TABLE tblWebTransactions " +
                                    "(lngTransactionWebID LONG CONSTRAINT pkTransactionWebID PRIMARY KEY, " +
                                    "lngCTUserID LONG, " +
                                    "blnProcessed YESNO, " +
                                    "lngTransactionID LONG, lngRecordID LONG, lngRecordWebID LONG, lngTransTypeID LONG, lngPaymentTypeID LONG, lngRegistrationID LONG, lngRegistrationWebID LONG, lngRegHoldID LONG, lngBillStateID LONG, lngThirdPartyID LONG, lngReversedTransactionID LONG, lngTransSubTypeID LONG, lngGGCCID LONG, lngGGCCRegistrationID LONG, lngThirdPartyTransID LONG, " +
                                    "curPayment CURRENCY, curCharge CURRENCY, " +
                                    "dteDateAdded DATETIME, " +
                                    "strCheckWriter TEXT(255), strCCNumber TEXT(255), strCCExpDate TEXT(255), strBillAddress TEXT(255), strBillCity TEXT(255), strBillName TEXT(255), strBillPhone TEXT(255), strBillZip TEXT(255), strCheckNumber TEXT(255), strTransactionDesc TEXT(255), strCCValCode TEXT(255), strAcctNum TEXT(255), strBankName TEXT(255), strRoutingNum TEXT(255), strAuthNumber TEXT(255), strPNRef TEXT(255))";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();
                        }

                        //overhaul of web ind reg
                        blnAddFields = false;

                        strSQL = "SELECT blnRegFlag30 " +
                                "FROM tblWebIndRegistrations";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "DROP TABLE tblWebIndRegistrations";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            string strRegFlags = "";
                            string strRegFields = "";

                            for (int intI = 1; intI <= 30; intI++)
                            {
                                strRegFlags += "blnRegFlag" + intI.ToString() + " YESNO NULL, ";
                                strRegFields += "strRegField" + intI.ToString() + " TEXT(50), ";
                            }

                            strSQL = "CREATE TABLE tblWebIndRegistrations " +
                                    "( blnSpecialNeeds YESNO NULL, " + strRegFlags +
                                        "lngRegistrationWebID INT NOT NULL, lngRecordWebID int NOT NULL, lngConfMethodID int NULL, " +
                                        "curDeposit CURRENCY NULL , curDonation CURRENCY NULL , curSpendingMoney CURRENCY NULL , " +
                                        "dteRegistrationDate datetime NULL , dteCreated datetime NULL , " +
                                        "strBuddy1 TEXT (255)  NULL , strBuddy2 TEXT (255)  NULL , strReleaseTo TEXT (255)  NULL , strConfEmail TEXT (255) NULL , strReferredBy TEXT (50) NULL, strXCTransID TEXT (50)  NULL , strXCAlias TEXT (25) NULL, " + strRegFields +
                                        "strDependNotes MEMO NULL, mmoSpecialNeeds MEMO NULL, mmoRegNotes MEMO NULL )";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tlkpCountries
                        blnAddFields = false;

                        strSQL = "SELECT lngCountryID " +
                                "FROM tlkpCountry ";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tlkpCountry " +
                                    "( lngCountryID INT NOT NULL, " +
                                        "strCountry TEXT (50) )";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            //populate country table
                            subPopCountries();
                        }

                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strXChargeTerminalID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeTerminalID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strPCIAgreementCode", "TEXT(15)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strPCIAgreementCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strPCIAgreementKey", "TEXT(15)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strPCIAgreementKey:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblGGCCRegistrations", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGGCCRegistrations.strPNRef:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strPNRef:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strPNRef:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strPrevTransType", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strPrevTransType:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strTextClientID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strTextClientID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strMDCustomerID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strMDCustomerID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "blnCapsWebProcessing", "YESNO", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.blnCapsWebProcessing:\n" + strErr);

                        //cert results table for melissa data ncoa
                        blnAddFields = false;

                        strSQL = "SELECT lngRecordID " +
                                "FROM tblRecordCert";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //try drop first (just in case it was created incorrectly)
                            strSQL = "DROP TABLE tblRecordCert";

                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch { }

                            strSQL = "CREATE TABLE tblRecordCert " +
                                        "( lngRecordID LONG CONSTRAINT PK_lngRecordID PRIMARY KEY, " +
                                            "dteProcessed DATETIME, " +
                                            "strErrorCode TEXT(10), " +
                                            "strErrorDesc TEXT(50), " +
                                            "strStatus TEXT(50), " +
                                            "strMoveDate TEXT(50), " +
                                            "strMoveReturnCode TEXT(25), " +
                                            "strMoveReturnDesc TEXT(100), " +
                                            "strMoveTypeCode TEXT(50), " +
                                            "strMoveTypeDesc TEXT(50), " +
                                            "strListName TEXT(25))";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblDiscounts (promo codes)
                        blnAddFields = false;

                        strSQL = "SELECT lngDiscountDefID " +
                                "FROM tblDiscountDefs";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //try drop first (just in case it was created incorrectly)
                            strSQL = "DROP TABLE tblDiscountDefs";

                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch { }

                            strSQL = "CREATE TABLE tblDiscountDefs " +
                                        "( lngDiscountDefID COUNTER CONSTRAINT PK_lngDiscountDefID PRIMARY KEY, " +
                                            "lngDiscountDefWebID INT NULL, " +
                                            "lngDiscountID INT NULL, " +
                                            "strPromoCode TEXT(50))";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //customer ref, pmt method for vanco processing
                        blnAddFields = false;

                        strSQL = "SELECT strVancoCustRef " +
                                "FROM tblWebGGCCRegistrations";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "ALTER TABLE tblWebGGCCRegistrations " +
                                    "ADD COLUMN strVancoCustRef TEXT(50)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            strSQL = "ALTER TABLE tblWebGGCCRegistrations " +
                                    "ADD COLUMN strVancoPmtMethID TEXT(50)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //NCOA IDs to process
                        blnAddFields = false;

                        strSQL = "SELECT lngRecordID " +
                                "FROM tblNCOAIDsToProcess";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblNCOAIDsToProcess " +
                                    "(lngRecordID LONG CONSTRAINT pkRecordID PRIMARY KEY)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //ID holder for chunked upload
                        blnAddFields = false;

                        strSQL = "SELECT lngID " +
                                "FROM tblIDHolder";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblIDHolder " +
                                    "(lngID LONG CONSTRAINT pkID PRIMARY KEY)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }


                        //strAddress
                        //strCity
                        //strState
                        //strZip 
                        blnAddFields = false;

                        strSQL = "SELECT strAddress " +
                                "FROM tblRecordCert";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "ALTER TABLE tblRecordCert " +
                                    "ADD COLUMN strAddress TEXT(255)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            strSQL = "ALTER TABLE tblRecordCert " +
                                    "ADD COLUMN strCity TEXT(255)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            strSQL = "ALTER TABLE tblRecordCert " +
                                    "ADD COLUMN strState TEXT(25)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            strSQL = "ALTER TABLE tblRecordCert " +
                                    "ADD COLUMN strZip TEXT(25)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblMORNCOAAlerts
                        blnAddFields = false;

                        strSQL = "SELECT lngMORID " +
                                "FROM tblMORNCOAAlerts";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblMORNCOAAlerts " +
                                    "(lngMORID LONG CONSTRAINT pkMORID PRIMARY KEY, " +
                                        "blnResolved YESNO, " +
                                        "strListName TEXT(50), " +
                                        "mmoAlertNotes MEMO)";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblRecordCert.blnReconciled
                        blnAddFields = false;

                        strSQL = "SELECT blnReconciled " +
                                "FROM tblRecordCert";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "ALTER TABLE tblRecordCert " +
                                    "ADD COLUMN blnReconciled YESNO";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblCampDefaults.blnUseSSL
                        blnAddFields = false;

                        strSQL = "SELECT blnUseSSL " +
                                "FROM tblCampDefaults";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "ALTER TABLE tblCampDefaults " +
                                    "ADD COLUMN blnUseSSL YESNO";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //transaction dl batches
                        blnAddFields = false;

                        strSQL = "SELECT lngTransactionID " +
                                "FROM tblTransDLBatches";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //try drop first (just in case it was created incorrectly)
                            strSQL = "DROP TABLE tblTransDLBatches";

                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch { }

                            strSQL = "CREATE TABLE tblTransDLBatches " +
                                        "( lngTransactionID LONG CONSTRAINT PK_lngTransactionID PRIMARY KEY, " +
                                            "dteRetrieved DATETIME )";

                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblGoogleCalOptions
                        blnAddFields = false;

                        strSQL = "SELECT blnULGG " +
                                "FROM tblGoogleCalOptions";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblGoogleCalOptions " +
                                    "(blnULGG YESNO, blnULCC YESNO, blnULBlock YESNO, blnFilterByProgramGG YESNO, blnFilterByProgramCC YESNO, blnFilterByProgramBlock YESNO, blnFilterByStartDateGG YESNO, blnFilterByStartDateCC YESNO, blnFilterByStartDateBlock YESNO, blnFilterByStatusGG YESNO, blnFilterByStatusCC YESNO, " +
                                        "dteStartDateGG DATETIME, dteStartDateCC DATETIME, dteStartDateBlock DATETIME, dteEndDateGG DATETIME, dteEndDateCC DATETIME, dteEndDateBlock DATETIME)";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();

                            strSQL = "INSERT INTO tblGoogleCalOptions " +
                                    "(blnULGG, blnULCC, blnULBlock, blnFilterByProgramGG, blnFilterByProgramCC, blnFilterByProgramBlock, blnFilterByStartDateGG, blnFilterByStartDateCC, blnFilterByStartDateBlock, blnFilterByStatusGG, blnFilterByStatusCC, " +
                                        "dteStartDateGG, dteStartDateCC, dteStartDateBlock, dteEndDateGG, dteEndDateCC, dteEndDateBlock) " +
                                    "VALUES " +
                                    "(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " +
                                        "Now(), Now(), Now(), Now(), Now(), Now())";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();

                            strSQL = "CREATE TABLE tblGoogleCalFilteredPrograms " +
                                    "(lngProgramTypeID LONG)";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();

                            strSQL = "CREATE TABLE tblGoogleCalFilteredStatus " +
                                    "(lngStatusID LONG)";

                            cmdCT.CommandText = strSQL;
                            cmdCT.ExecuteNonQuery();
                        }

                        //tblCustomFieldsGiftDef
                        blnAddFields = false;

                        strSQL = "SELECT strFieldName " +
                                "FROM tblCustomFieldsGiftDef";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblCustomFieldsGiftDef " +
                                    "(blnRequired YESNO, " +
                                        "strFieldName TEXT(50), strFieldDesc TEXT(255), strFieldType TEXT(20), strDefaultVal TEXT(255), strValidation TEXT(10))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblCustomFieldsGiftLookupOptions
                        blnAddFields = false;

                        strSQL = "SELECT strFieldName " +
                                "FROM tblCustomFieldsGiftLookupOptions";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblCustomFieldsGiftLookupOptions " +
                                    "(intSortOrder INTEGER, " +
                                        "strFieldName TEXT(50), strLookupOption TEXT(255), " +
                                        "Primary key (strFieldName, strLookupOption))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        //tblDonorExpress
                        blnAddFields = false;

                        strSQL = "SELECT lngDonorExpressID " +
                                "FROM tblDonorExpress";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblDonorExpress " +
                                    "(lngDonorExpressID LONG CONSTRAINT lngDonorExpressID_PK PRIMARY KEY, " +
                                        "blnProcessed YESNO DEFAULT 0, " +
                                        "lngPaymentTypeID LONG, " +
                                        "dteCreated DATETIME, dteSubmitted DATETIME, " +
                                        "curGiftAmt CURRENCY, " +
                                        "strEmail TEXT(255), strFName TEXT(50), strLName TEXT(50), strAddress TEXT(100), strCity TEXT(50), strState TEXT(20), strZip TEXT(50), strHomePhone TEXT(20), strReferredBy TEXT(50), strIMO TEXT(50), strIHO TEXT(50), strCheckNumber TEXT(50), strAcctNum TEXT(255), strBankName TEXT(255), strCCExpDate TEXT(4), strCCNumber TEXT(100), strCCValCode TEXT(10), strRoutingNum TEXT(255), strAuthNum TEXT(50), strPNRef TEXT(50), strXCAlias TEXT(50), strXCTransID TEXT(50), strXCEFTAuthCode TEXT(50), strXCEFTRefID TEXT(50))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            strSQL = "CREATE TABLE tblDonorExpressCustomVals " +
                                    "(lngDonorExpressID LONG, " +
                                        "strFieldName TEXT(50), " +
                                        "strValue MEMO, " +
                                        "PRIMARY KEY (lngDonorExpressID, strFieldName))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();

                            strSQL = "CREATE TABLE tblDXDonorCustomVals " +
                                    "(lngDonorExpressID LONG, " +
                                        "strFieldName TEXT(50), " +
                                        "strValue MEMO, " +
                                        "PRIMARY KEY (lngDonorExpressID, strFieldName))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            cmdCT.ExecuteNonQuery();
                        }

                        blnAddFields = false;

                        strSQL = "SELECT lngCustomFieldValWebIRID " +
                                "FROM tblCustomFieldValWebIR";

                        cmdCT.CommandText = strSQL;
                        cmdCT.Parameters.Clear();

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //tblCustomFieldValWebIR
                            strSQL = "CREATE TABLE tblCustomFieldValWebIR " +
                                    "( lngCustomFieldValWebIRID COUNTER CONSTRAINT lngCustomFieldValWebIRID_PK PRIMARY KEY, " +
                                        "lngRecordWebID LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strValue MEMO )";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("Error adding tblCustomFieldValIR: " + ex.Message); }

                            //append default records to tblCustomFieldDefIR (only used when first creating new structure)
                            strSQL = "SELECT tblCustomFlagDesc.* " +
                                    "FROM tblCustomFlagDesc";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            using (OleDbDataReader drCust = cmdCT.ExecuteReader())
                            {
                                if (drCust.Read())
                                {
                                    bool blnUse = false;
                                    string strVal = "";

                                    using (OleDbConnection conAppendDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                                    {
                                        conAppendDB.Open();

                                        using (OleDbCommand cmdAppend = new OleDbCommand())
                                        {
                                            cmdAppend.Connection = conAppendDB;

                                            int intSort = 0;

                                            for (int intI = 1; intI <= 30; intI++)
                                            {
                                                //flag
                                                try { blnUse = Convert.ToBoolean(drCust["blnFlag" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strFlag" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    strSQL = "DELETE FROM tblCustomFieldValWebIR";

                                                    cmdAppend.CommandText = strSQL;
                                                    cmdAppend.Parameters.Clear();

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }

                                                    //add web records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValWebIR " +
                                                            "( lngRecordWebID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblWebRecords.lngRecordWebID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblWebRecords.blnFlag" + intI.ToString() + " " +
                                                            "FROM tblWebRecords;";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }
                                                }

                                                //field
                                                try { blnUse = Convert.ToBoolean(drCust["blnField" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strField" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    //add web records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValWebIR " +
                                                            "( lngRecordWebID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblWebRecords.lngRecordWebID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblWebRecords.strCustom" + intI.ToString() + " " +
                                                            "FROM tblWebRecords";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }
                                                }
                                            }
                                        }

                                        conAppendDB.Close();
                                    }
                                }

                                drCust.Close();
                            }
                        }

                        //web reg
                        blnAddFields = false;

                        strSQL = "SELECT lngCustomFieldValWebRegID " +
                                "FROM tblCustomFieldValWebReg";

                        cmdCT.CommandText = strSQL;
                        cmdCT.Parameters.Clear();

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //tblCustomFieldValWebReg
                            strSQL = "CREATE TABLE tblCustomFieldValWebReg " +
                                    "( lngCustomFieldValWebRegID COUNTER CONSTRAINT lngCustomFieldValWebRegID_PK PRIMARY KEY, " +
                                        "lngRegistrationWebID LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strValue MEMO )";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("Error adding tblCustomFieldValReg: " + ex.Message); }

                            //append default records to tblCustomFieldDefReg
                            strSQL = "SELECT tblCustomRegFlagDesc.* " +
                                    "FROM tblCustomRegFlagDesc";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            using (OleDbDataReader drCust = cmdCT.ExecuteReader())
                            {
                                if (drCust.Read())
                                {
                                    bool blnUse = false;
                                    string strVal = "";

                                    using (OleDbConnection conAppendDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                                    {
                                        conAppendDB.Open();

                                        using (OleDbCommand cmdAppend = new OleDbCommand())
                                        {
                                            cmdAppend.Connection = conAppendDB;

                                            int intSort = 0;

                                            for (int intI = 1; intI <= 30; intI++)
                                            {
                                                //flag
                                                try { blnUse = Convert.ToBoolean(drCust["blnFlag" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strFlag" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    //add web records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValWebReg " +
                                                            "( lngRegistrationWebID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblWebIndRegistrations.lngRegistrationWebID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblWebIndRegistrations.blnRegFlag" + intI.ToString() + " " +
                                                            "FROM tblWebIndRegistrations " +
                                                            "WHERE @strLocalCaption IN " +
                                                                "(SELECT strLocalCaption " +
                                                                "FROM tblCustomFieldDefReg " +
                                                                "WHERE blnUseOnline=True)";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }
                                                }

                                                //field
                                                try { blnUse = Convert.ToBoolean(drCust["blnField" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strField" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    //add web records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValWebReg " +
                                                            "( lngRegistrationWebID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblWebIndRegistrations.lngRegistrationWebID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblWebIndRegistrations.strRegField" + intI.ToString() + " " +
                                                            "FROM tblWebIndRegistrations " +
                                                            "WHERE @strLocalCaption IN " +
                                                                "(SELECT strLocalCaption " +
                                                                "FROM tblCustomFieldDefReg " +
                                                                "WHERE blnUseOnline=True)";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }
                                                }
                                            }
                                        }

                                        conAppendDB.Close();
                                    }
                                }

                                drCust.Close();
                            }
                        }

                        //tblCustomFieldDefIR
                        blnAddFields = false;

                        strSQL = "SELECT lngCustomFieldDefIRID " +
                                "FROM tblCustomFieldDefIR";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //get web field, flag defs
                            clsCustomFieldIRDef[] cstWebFlags = new clsCustomFieldIRDef[30];
                            clsCustomFieldIRDef[] cstWebFields = new clsCustomFieldIRDef[30];

                            if (!fcnGetWebCustomFieldDefs(cstWebFlags, cstWebFields)) MessageBox.Show("There was an error retrieving web custom field defs");

                            strSQL = "CREATE TABLE tblCustomFieldDefIR " +
                                    "(lngCustomFieldDefIRID COUNTER CONSTRAINT lngCustomFieldDefIRID_PK PRIMARY KEY, " +
                                        "blnRequired YESNO DEFAULT 0, " +
                                        "blnUseLocal YESNO DEFAULT 0, " +
                                        "lngProgramID LONG, " +
                                        "lngSortOrder LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strFieldType TEXT(25))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("Error adding tblCustomFieldDefIR: " + ex.Message); }

                            //tblCustomFieldValIR
                            strSQL = "CREATE TABLE tblCustomFieldValIR " +
                                    "( lngCustomFieldValIRID COUNTER CONSTRAINT lngCustomFieldValIRID_PK PRIMARY KEY, " +
                                        "lngRecordID LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strValue MEMO )";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("Error adding tblCustomFieldValIR: " + ex.Message); }

                            //append default records to tblCustomFieldDefIR (only used when first creating new structure)
                            strSQL = "SELECT tblCustomFlagDesc.* " +
                                    "FROM tblCustomFlagDesc";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            using (OleDbDataReader drCust = cmdCT.ExecuteReader())
                            {
                                if (drCust.Read())
                                {
                                    bool blnUse = false;
                                    string strVal = "";

                                    using (OleDbConnection conAppendDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                                    {
                                        conAppendDB.Open();

                                        using (OleDbCommand cmdAppend = new OleDbCommand())
                                        {
                                            cmdAppend.Connection = conAppendDB;

                                            int intSort = 0;

                                            for (int intI = 1; intI <= 30; intI++)
                                            {
                                                //flag
                                                try { blnUse = Convert.ToBoolean(drCust["blnFlag" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strFlag" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    strSQL = "INSERT INTO tblCustomFieldDefIR " +
                                                            "( blnUseLocal, " +
                                                                "lngSortOrder, " +
                                                                "strLocalCaption, strFieldType ) " +
                                                            "VALUES " +
                                                            "( 1, " +
                                                                intSort.ToString() + ", " +
                                                                "@strLocalCaption, 'FLAG', @mmoWebCaption)";

                                                    cmdAppend.CommandText = strSQL;
                                                    cmdAppend.Parameters.Clear();

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);
                                                    
                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch (Exception ex) { MessageBox.Show("Error converting data into tblCustomFieldDefIR: " + ex.Message); }

                                                    //add records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValIR " +
                                                            "( lngRecordID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblRecords.lngRecordID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblRecords.blnFlag" + intI.ToString() + " " +
                                                            "FROM tblRecords";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch (Exception ex) { MessageBox.Show("There was an error adding custom field values: " + ex.Message); }
                                                }

                                                //field
                                                try { blnUse = Convert.ToBoolean(drCust["blnField" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strField" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    strSQL = "INSERT INTO tblCustomFieldDefIR " +
                                                            "( blnUseLocal, " +
                                                                "lngSortOrder, " +
                                                                "strLocalCaption, strFieldType ) " +
                                                            "VALUES " +
                                                            "( 1, " +
                                                                intSort.ToString() + ", " +
                                                                "@strLocalCaption, 'FIELD')";

                                                    cmdAppend.CommandText = strSQL;
                                                    cmdAppend.Parameters.Clear();

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch (Exception ex) { MessageBox.Show("Error converting data into tblCustomFieldDefIR: " + ex.Message); }

                                                    //add records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValIR " +
                                                            "( lngRecordID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblRecords.lngRecordID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblRecords.strCustom" + intI.ToString() + " " +
                                                            "FROM tblRecords " +
                                                            "WHERE strCustom" + intI.ToString() + "<>''";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }
                                                }
                                            }
                                        }

                                        conAppendDB.Close();
                                    }
                                }

                                drCust.Close();
                            }
                        }

                        ////////////////////////Custom Registration Fields//////////////////////////////////////////////////
                  
                        //tblCustomRegFieldDefIR
                        blnAddFields = false;

                        strSQL = "SELECT lngCustomFieldDefRegID " +
                                "FROM tblCustomFieldDefReg";

                        cmdCT.CommandText = strSQL;

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            //get web field, flag defs
                           clsCustomFieldRegDef[] cstWebRegFlags = new clsCustomFieldRegDef[30];
                           clsCustomFieldRegDef[] cstWebRegFields = new clsCustomFieldRegDef[30];

                            if (!fcnGetWebCustomRegFieldDefs(cstWebRegFlags,cstWebRegFields)) MessageBox.Show("There was an error retrieving web custom reg field defs");

                            strSQL = "CREATE TABLE tblCustomFieldDefReg " +
                                    "(lngCustomFieldDefRegID COUNTER CONSTRAINT lngCustomFieldDefRegID_PK PRIMARY KEY, " +
                                        "blnRequired YESNO DEFAULT 0, " +
                                        "blnUseLocal YESNO DEFAULT 0, " +
                                        "lngProgramID LONG, " +
                                        "lngSortOrder LONG, " +
                                        "decCharge CURRENCY, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strFieldType TEXT(25))";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("Error adding tblCustomFieldDefReg: " + ex.Message); }

                            //tblCustomFieldValReg
                            strSQL = "CREATE TABLE tblCustomFieldValReg " +
                                    "( lngCustomFieldValRegID COUNTER CONSTRAINT lngCustomFieldValRegID_PK PRIMARY KEY, " +
                                        "lngRegistrationID LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strValue MEMO )";

                            cmdCT.Parameters.Clear();
                            cmdCT.CommandText = strSQL;

                            try { cmdCT.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("Error adding tblCustomFieldValReg: " + ex.Message); }

                            //append default records to tblCustomFieldDefReg
                            strSQL = "SELECT tblCustomRegFlagDesc.* " +
                                    "FROM tblCustomRegFlagDesc";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            using (OleDbDataReader drCust = cmdCT.ExecuteReader())
                            {
                                if (drCust.Read())
                                {
                                    bool blnUse = false;
                                    string strVal = "";

                                    using (OleDbConnection conAppendDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                                    {
                                        conAppendDB.Open();

                                        using (OleDbCommand cmdAppend = new OleDbCommand())
                                        {
                                            cmdAppend.Connection = conAppendDB;

                                            int intSort = 0;

                                            for (int intI = 1; intI <= 30; intI++)
                                            {
                                                //flag
                                                try { blnUse = Convert.ToBoolean(drCust["blnFlag" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strFlag" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    strSQL = "INSERT INTO tblCustomFieldDefReg " +
                                                            "( blnUseLocal, " +
                                                                "lngProgramID, lngSortOrder, " +
                                                                "decCharge, "+
                                                                "strLocalCaption, strFieldType ) " +
                                                            "VALUES " +
                                                            "( 1, " +
                                                                "0, " + intSort.ToString() + ", " +
                                                                "@decCharge, "+
                                                                "@strLocalCaption, 'FLAG')";

                                                    cmdAppend.CommandText = strSQL;
                                                    cmdAppend.Parameters.Clear();

                                                    cmdAppend.Parameters.AddWithValue("@decCharge",cstWebRegFlags[intI - 1].decCharge);
                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);
                                                    
                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch (Exception ex) { MessageBox.Show("Error converting data into tblCustomFieldDefReg: " + ex.Message); }

                                                    //add records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValReg " +
                                                            "( lngRegistrationID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblRegistrations.lngRegistrationID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblRegistrations.blnCustomRegFlag" + intI.ToString() + " " +
                                                            "FROM tblRegistrations";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch (Exception ex) { MessageBox.Show("There was an error adding custom reg field values: " + ex.Message); }                                                    
                                                }

                                                //field
                                                try { blnUse = Convert.ToBoolean(drCust["blnField" + intI.ToString()]); }
                                                catch { blnUse = false; }

                                                try { strVal = Convert.ToString(drCust["strField" + intI.ToString() + "Desc"]); }
                                                catch { strVal = ""; }

                                                if (blnUse && strVal != "")
                                                {
                                                    intSort++;

                                                    strSQL = "INSERT INTO tblCustomFieldDefReg " +
                                                            "( blnUseLocal, " +
                                                                "lngProgramID, lngSortOrder, " +
                                                                "strLocalCaption, strFieldType ) " +
                                                            "VALUES " +
                                                            "( 1, " +
                                                                "0, " + intSort.ToString() + ", " +
                                                                "@strLocalCaption, 'FIELD')";

                                                    cmdAppend.CommandText = strSQL;
                                                    cmdAppend.Parameters.Clear();

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);
                                                    
                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch (Exception ex) { MessageBox.Show("Error converting data into tblCustomFieldDefReg: " + ex.Message); }

                                                    //add records to val table
                                                    strSQL = "INSERT INTO tblCustomFieldValReg " +
                                                            "( lngRegistrationID, " +
                                                                "strLocalCaption, strValue ) " +
                                                            "SELECT tblRegistrations.lngRecordID, " +
                                                                "@strLocalCaption AS strLocalCaption, tblRegistrations.strRegField" + intI.ToString() + " " +
                                                            "FROM tblRegistrations " +
                                                            "WHERE strRegField" + intI.ToString() + "<>''";

                                                    cmdAppend.Parameters.Clear();
                                                    cmdAppend.CommandText = strSQL;

                                                    cmdAppend.Parameters.AddWithValue("@strLocalCaption", strVal);

                                                    try { cmdAppend.ExecuteNonQuery(); }
                                                    catch { }
                                                }
                                            }
                                        }

                                        conAppendDB.Close();
                                    }
                                }

                                drCust.Close();
                            }
                        }

                        //field options
                        blnAddFields = false;

                        strSQL = "SELECT lngCustomFieldDefIROptionID " +
                                "FROM tblCustomFieldDefIROptions";

                        cmdCT.CommandText = strSQL;
                        cmdCT.Parameters.Clear();

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblCustomFieldDefIROptions " +
                                    "( lngCustomFieldDefIROptionID COUNTER CONSTRAINT lngCustomFieldDefIROptionID_PK PRIMARY KEY, " +
                                        "intSortOrder LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strValue  TEXT(255) )";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            try { cmdCT.ExecuteNonQuery(); }
                            catch { }

                            strSQL = "CREATE TABLE tblCustomFieldDefRegOptions " +
                                    "( lngCustomFieldDefRegOptionID COUNTER CONSTRAINT lngCustomFieldDefRegOptionID_PK PRIMARY KEY, " +
                                        "intSortOrder LONG, " +
                                        "strLocalCaption TEXT(255), " +
                                        "strValue  TEXT(255) )";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            try { cmdCT.ExecuteNonQuery(); }
                            catch { }
                        }

                        //clean up old defs for custom fields
                        strSQL = "DELETE tblCustomFieldDefIR.* " +
                                "FROM tblCustomFieldDefIR " +
                                "WHERE tblCustomFieldDefIR.lngProgramID<>0";

                        cmdCT.CommandText = strSQL;
                        cmdCT.Parameters.Clear();

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { }

                        //clean up old defs for reg custom fields
                        strSQL = "DELETE tblCustomFieldDefReg.* " +
                                "FROM tblCustomFieldDefReg " +
                                "WHERE tblCustomFieldDefReg.lngProgramID<>0";

                        cmdCT.CommandText = strSQL;
                        cmdCT.Parameters.Clear();

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { }


                        blnAddFields = false;

                        strSQL = "SELECT lngGWTransID " +
                                "FROM tblMerchantGatewayTrans";

                        cmdCT.CommandText = strSQL;
                        cmdCT.Parameters.Clear();

                        try { cmdCT.ExecuteNonQuery(); }
                        catch { blnAddFields = true; }

                        if (blnAddFields)
                        {
                            strSQL = "CREATE TABLE tblMerchantGatewayTrans " +
                                    "(lngGWTransID LONG CONSTRAINT pkGWTransID PRIMARY KEY, " +
                                    "dteTransDate DATETIME, " +
                                    "decAmount CURRENCY, " +
                                    "strFName TEXT(50), " +
                                    "strLName TEXT(100), " +
                                    "strAddress TEXT(100), " +
                                    "strCity TEXT(100), " +
                                    "strState TEXT(50), " +
                                    "strZip TEXT(50), " +
                                    "strCountry TEXT(100), " +
                                    "strEmail TEXT(100), " +
                                    "strCTID TEXT(100), " +
                                    "strStatus TEXT(100), " +
                                    "strLastFour TEXT(4), " +
                                    "strMerchantRef TEXT(50), " +
                                    "strSource TEXT(10))";

                            cmdCT.CommandText = strSQL;
                            cmdCT.Parameters.Clear();

                            cmdCT.ExecuteNonQuery();
                        } 
                                               
                        subManageTlkpTransType(cmdCT);

                        if (!fcnAddField(cmdCT, "tblWebGift", "blnPledgeReminders", "YESNO", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.blnPledgeReminders:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "blnPledgeAutopay", "YESNO", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.blnPledgeAutopay:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "intPledgeFreq", "INTEGER", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.intPledgeFreq:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "intPledgeTerm", "INTEGER", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.intPledgeTerm:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tlkpWeekDesc", "lngWeekWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tlkpWeekDesc.lngWeekWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tlkpAgeGroup", "lngAgeGroupWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tlkpAgeGroup.lngAgeGroupWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tlkpGradeGroup", "lngGradeGroupWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tlkpGradeGroup.lngGradeGroupWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tlkpCampName", "lngCampWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tlkpCampName.lngCampWebID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tlkpCampName", "blnExcludeFromGeneral", "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tlkpCampName.blnExcludeFromGeneral:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tlkpHousingName", "lngHousingWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tlkpHousingName.lngHousingWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblGoogleCalOptions", "strBlockCal", "TEXT(100)", "", ref strErr)) MessageBox.Show("There was an error adding tblGoogleCalOptions.strBlockCal:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGoogleCalOptions", "strGGCal", "TEXT(100)", "", ref strErr)) MessageBox.Show("There was an error adding tblGoogleCalOptions.strGGCal:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGoogleCalOptions", "strCCCal", "TEXT(100)", "", ref strErr)) MessageBox.Show("There was an error adding tblGoogleCalOptions.strCCCal:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tlkpTransType", "lngTransTypeWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tlkpTransType.lngTransTypeWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblMOR", "lngMORWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblMOR.lngMORWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblnkMORIR", "lngMORIRLinkWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblnkMORIR.lngMORIRLinkWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "blnProcessed", "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strGooglePW:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "lngRegSourceID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.lngRegSourceID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strPmtType", "TEXT(10)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strPmtType:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strXCAlias", "TEXT(25)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strRoutingNumber", "TEXT(35)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strRoutingNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strAcctNumber", "TEXT(35)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strAcctNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strPNRef:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strXCAuthCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strXCAuthCode:\n" + strErr);
                        for (int intI = 1; intI <= 10; intI++)
                            if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "blnDiscount" + intI.ToString(), "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.blnDiscount" + intI.ToString() + ":\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strEPSPmtAcctID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strOrgCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strOrgCode:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strGoogleUName", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strGoogleUName:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strGooglePW", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strGooglePW:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strXChargeWebID", "TEXT(30)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeWebID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strXChargeAuthKey", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeAuthKey:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strXChargeTerminalID", "TEXT(30)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeTerminalID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strCashLinqCQUser", "TEXT(30)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeTerminalID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strCashLinqCQPW", "TEXT(30)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeTerminalID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strCashLinqCQMerchantID", "TEXT(30)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strXChargeTerminalID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "lngDefCustRegFlagTransType", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.lngDefCustRegFlagTransType:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "lngDXCampaignID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.lngDXCampaignID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "lngDXCategoryID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.lngDXCategoryID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strEPSAcceptorID", "TEXT(20)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strEPSAcceptorID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strEPSAccountID", "TEXT(20)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strEPSAccountID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strEPSAccountToken", "TEXT(100)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strEPSAccountToken:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblCampDefaults", "strEPSTerminalID", "TEXT(10)", "", ref strErr)) MessageBox.Show("There was an error adding tblCampDefaults.strEPSTerminalID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "lngRegHoldID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.lngRegHoldID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebIndRegistrations", "strCardNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebIndRegistrations.strCardNumber:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebRecords", "mmoSpecialNeeds", "MEMO", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.mmoSpecialNeeds:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "dteBirthdate", "DATETIME", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.dteBirthdate:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "strMotherName", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.strMother:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "strFatherName", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.strFather:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebRecords", "strPassword", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebRecords.strPassword:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblRegistrations", "strReferredBy", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblRegistrations.strReferredBy:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblRegistrations", "lngRegistrationWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblRegistrations.lngRegistrationWebID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblRegistrations", "dteProcessed", "DATETIME", "NOW()", ref strErr)) MessageBox.Show("There was an error adding tblRegistrations.dteProcessed:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblRegistrations", "strOrgCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblRegistrations.strOrgCode:\n" + strErr);
                        
                        if (!fcnAddField(cmdCT, "tblRegHold", "lngRegHoldWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblRegHold.lngRegHoldWebID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblRegHold", "curRegCharge", "CURRENCY", "0", ref strErr)) MessageBox.Show("There was an error adding tblRegHold.curRegCharge:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblRegHold", "blnSharedCostPercent", "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tblRegHold.blnSharedCostPercent:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebGift", "strXCAlias", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strXCTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strXCTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strXCEFTAuthCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strXCEFTAuthCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strXCEFTRefID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strXCEFTRefID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGift", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGift.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebTransactions", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebTransactions.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebTransactions", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebTransactions.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebTransactions", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebTransactions.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebTransactions", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebTransactions.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblGGCCRegistrations", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGGCCRegistrations.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGGCCRegistrations", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGGCCRegistrations.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGGCCRegistrations", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGGCCRegistrations.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGGCCRegistrations", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGGCCRegistrations.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblDonorExpress", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblDonorExpress.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblDonorExpress", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblDonorExpress.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblDonorExpress", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblDonorExpress.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblDonorExpress", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblDonorExpress.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strXCAlias", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strXCTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strXCTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strXCEFTAuthCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strXCEFTAuthCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strXCEFTRefID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strXCEFTRefID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBillingInfo", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblBillingInfo.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblPledge", "blnPledgeAutopay", "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tblPledge.blnPledgeAutopay:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "blnMemorial", "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tblPledge.blnMemorial:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "blnInHonorOf", "YESNO", "0", ref strErr)) MessageBox.Show("There was an error adding tblPledge.blnInHonorOf:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "lngPaymentTypeID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.lngPaymentTypeID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "lngBillStateID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.lngBillStateID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strXCAlias", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strXCTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strXCTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strXCEFTAuthCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strXCEFTAuthCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strXCEFTRefID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strXCEFTRefID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strPNRef:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strMemorialName", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strMemorialName:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strInHonorOf", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strInHonorOf:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strAcctNum", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strAcctNum:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strBankName", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strBankName:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strBillAddress", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strBillAddress:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strBillCity", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strBillCity:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strBillName", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strBillName:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strBillPhone", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strBillPhone:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strBillZip", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strBillZip:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strCCExpDate", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strCCExpDate:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strCCNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strCCNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strCCValCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strCCValCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strRoutingNum", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strRoutingNum:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblPledge", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblPledge.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblBlock", "curMinDep", "CURRENCY", "0", ref strErr)) MessageBox.Show("There was an error adding tblBlock.curMinDep:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblBlock", "lngBlockWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblBlock.lngBlockWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblTransactions", "strXCAlias", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "strXCTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strXCTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "strXCAuthCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strXCAuthCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "lngTransactionWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.lngTransactionWebID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "lngTransBalanceWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.lngTransBalanceWebID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblTransactions", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblTransactions.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblGift", "strXCAlias", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strXCTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strXCTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strPNRef", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strPNRef:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strXCAuthCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strXCAuthCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblGift", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblGift.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblSecurity", "lngUserWebID", "LONG", "", ref strErr)) MessageBox.Show("There was an error adding tblSecurity.lngUserWebID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strXCAlias", "TEXT(25)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strXCAlias:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strXCTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strXCTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strEPSTransID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strEPSTransID:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strEPSApprovalNumber", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strEPSApprovalNumber:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strEPSValidationCode", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strEPSValidationCode:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblWebGGCCRegistrations", "strEPSPmtAcctID", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblWebGGCCRegistrations.strEPSPmtAcctID:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblCustomFieldsGiftDef", "intSortOrder", "INTEGER", "", ref strErr)) MessageBox.Show("There was an error adding tblCustomFieldsGiftDef.intSortOrder:\n" + strErr);

                        if (!fcnAddField(cmdCT, "tblRecords", "blnToSync", "YESNO", "", ref strErr)) MessageBox.Show("There was an error adding tblRecords.blnToSync:\n" + strErr);
                        if (!fcnAddField(cmdCT, "tblRecords", "strPassword", "TEXT(50)", "", ref strErr)) MessageBox.Show("There was an error adding tblRecords.strPassword:\n" + strErr);
                    }

                    conCT.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subStructureUpdateMDB", ex);
            }
        }

        private static void subManageTlkpTransType(OleDbCommand _cmdCT)
        {
            //tlkpTransType
            bool blnAddFields = false;

            string strSQL = "SELECT lngTransTypeID " +
                                "FROM tlkpTransType";

            _cmdCT.CommandText = strSQL;

            try { _cmdCT.ExecuteNonQuery(); }
            catch { blnAddFields = true; }

            if (blnAddFields)
            {
                strSQL = "CREATE TABLE tlkpTransType " +
                             "( lngTransTypeID COUNTER CONSTRAINT PK_lngTransTypeID PRIMARY KEY, " +
                                "blnAccountingEffect YESNO NULL, blnDebitCredit YESNO NULL, blnSpending YESNO NULL, blnAutoOnly YESNO NULL, blnGGType YESNO NULL, blnGiftType YESNO NULL, blnCashEffect YESNO NULL, blnAccrEffect YESNO NULL, blnLockEffects YESNO NULL, blnMoneyTrans YESNO NULL, blnScholarship YESNO NULL, " +
                                "strTransType TEXT(50) NULL, strOldTranType TEXT(50) NULL, strNewTranType TEXT(50) NULL, strCashExplanation TEXT(255) NULL, strCashQuestions TEXT(255) NULL, strCashDate TEXT(10) NULL, strCashDebit TEXT(35) NULL, strCashCredit TEXT(35) NULL, strAccrExplanation TEXT(255) NULL, strAccrQuestions TEXT(255) NULL, strAccrDate1 TEXT(10) NULL, strAccrDebit1 TEXT(35) NULL, strAccrCredit1 TEXT(35) NULL, strAccrDate2 TEXT(10) NULL, strAccrDebit2 TEXT(35) NULL, strAccrCredit2 TEXT(35) NULL )";

                _cmdCT.CommandText = strSQL;
                _cmdCT.ExecuteNonQuery();

                //add records
                subAppendTlkpTransTypeRecord(_cmdCT, 1, -1, -1, 0, 0, 0, 0, -1, -1, 0, -1, 0, "Registration Payment", "Payment for Tuition", "Registration Payment", "Prepayment for registration fees", "", "Trans", "PaymentMethod", "ProgramRevenue", "Prepayment for registration fees", "", "Trans", "PaymentMethod", "ProgramUnearned", "EventEnd", "ProgramUnearned", "ProgramReceivables");
                subAppendTlkpTransTypeRecord(_cmdCT, 2, -1, 0, 0, -1, 0, 0, 0, -1, 0, 0, 0, "Registration Fee", "Tuition", "Registration Fee", "Registration fees chargd automatically to registrants", "", "", "", "", "Registration fees charged automatically to registrants", "", "EventEnd", "ProgramReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 3, 0, -1, 0, 0, 0, 0, 0, -1, 0, -1, 0, "Cancellation Credit", "Cancelation", "Cancellation Credit", "Credits for the refundable fees when a registrant cancels", "Are these credits applied automatically when registration is cancelled?", "Trans", "ProgramRevenue", "ProgramReceivables", "???", "Are these credits applied automatically when registration is cancelled?", "Trans", "ProgramUnearned", "ProgramReceivables", "EventEnd", "ProgramRevenue", "ProgramUnearned");
                subAppendTlkpTransTypeRecord(_cmdCT, 4, -1, 0, 0, 0, 0, 0, -1, -1, 0, -1, 0, "Payment Refund", "Refund", "Payment Refund", "Cash refunds made to registrants", "Revenue effect must be made separately by discount, cancellation, etc.", "Trans", "ProgramRevenue", "PaymentMethod", "Cash refunds made to registrants", "", "Trans", "ProgramUnearned", "PaymentMethod", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 5, -1, 0, -1, 0, 0, 0, 0, -1, 0, 0, 0, "Craft House Charge", "Craft House", "Craft House Charge", "Craft House fees charged manually to registrants", "", "", "", "", "Craft House fees charged manually to registrants", "", "EventEnd", "ProgramReceivables", "ProgramActivityRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 6, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, "Camp Store Charge", "CampTrak General Store", "Camp Store Charge", "", "Where is this used automatically?", "", "", "", "", "Where is this used automatically?", "", "", "", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 7, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, -1, "Scholarship", "Scholarship", "Scholarship", "Credits for internal camperships", "Better handling and tracking as split pay credit!", "Trans", "ScholarshipExpense", "ProgramRevenue", "Credits for internal camperships", "Better handling and tracking as split pay credit!", "EventEnd", "ScholarshipExpense", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 8, -1, -1, 0, 0, 0, 0, -1, -1, 0, -1, 0, "Registration Deposit", "Deposit", "Registration Deposit", "Prepayment for registration fees", "", "Trans", "PaymentMethod", "ProgramRevenue", "Prepayment for registration fees", "", "Trans", "PaymentMethod", "ProgramUnearned", "EventEnd", "ProgramUnearned", "ProgramReceivables");
                subAppendTlkpTransTypeRecord(_cmdCT, 9, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, -1, "Scholarship- Grant", "Grant", "Scholarship- Grant", "Credits for camper grants", "Better handling and tracking as split pay credit!", "Trans", "ScholarshipExpense", "ProgramRevenue", "Credits for camper grants", "Better handling and tracking as split pay credit!", "EventEnd", "ScholarshipExpense", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 10, -1, -1, 0, 0, 0, 0, 0, -1, 0, 0, 0, "Accounts Receivable Credit", "Accounts Receivable Credit", "Accounts Receivable Credit", "Is this a writeoff?", "How is this different from Payment on Accounts Receivable?", "Trans", "ProgramReceivables", "ProgramRevenue", "Payment on Accounts Receivable Balance", "How is this different from Payment on Accounts Receivable?", "EventEnd", "ProgramRevenue", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 11, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, "Other Credit", "Other Credit", "Non Accounting Credit", "", "", "", "", "", "", "", "", "", "", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 12, -1, -1, 0, 0, 0, 0, -1, -1, 0, -1, 0, "Payment for Weekend Camper Fee", "Payment for Weekend Camper Fee", "Payment for Weekend Camper Fee", "Prepayment for weekend fees", "", "Trans", "PaymentMethod", "ProgramRevenue", "Prepayment for weekend fees", "", "Trans", "PaymentMethod", "ProgramUnearned", "EventEnd", "ProgramUnearned", "ProgramReceivables");
                subAppendTlkpTransTypeRecord(_cmdCT, 13, -1, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, "Weekend Camper Fee", "Weekend Camper Fee", "Weekend Camper Fee", "Weekend fees charged manually to registrants", "", "", "", "", "Weekend fees charged manually to registrants", "", "EventEnd", "ProgramReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 14, -1, -1, 0, 0, 0, 0, -1, -1, 0, -1, 0, "Payment for Miscellaneous Charges", "Payment for Misc. Charges", "Payment for Miscellaneous Charges", "Prepayment for miscellaneous registration charges", "", "Trans", "PaymentMethod", "ProgramMiscellaneous", "Prepayment for miscellaneous registration charges", "", "Trans", "PaymentMethod", "ProgramUnearned", "EventEnd", "ProgramUnearned", "ProgramReceivables");
                subAppendTlkpTransTypeRecord(_cmdCT, 15, -1, 0, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Miscellaneous Charge", "Misc. Charges", "Miscellaneous Charge", "Miscellaneous fees charged manually to registrants", "", "Trans", "ProgramReceivables", "ProgramMiscellaneous", "Miscellaneous fees charged manually to registrants", "", "EventEnd", "ProgramReceivables", "ProgramMiscellaneous", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 16, -1, 0, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Cancellation Fee", "Cancellation Charge", "Cancellation Fee", "Cancellation fees charged manually to registrants", "", "Trans", "ProgramReceivables", "ProgramMiscellaneous", "Cancellation fees charged manually to registrants", "", "Trans", "ProgramReceivables", "ProgramMiscellaneous", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 17, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, "Spending Money Charge", "Spending Money Charge", "Spending Money Charge", "", "What happens if users enters store purchases?", "", "", "", "", "What happens if users enters store purchases?", "Trans", "ProgramReceivables", "StoreRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 18, -1, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, "Activity Fee 2", "Both Activities", "Activity 2 Fee", "Activity fees charged manually to registrants", "", "", "", "", "Activity fees charged manually to registrants", "", "EventEnd", "ProgramReceivables", "ProgramActivityRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 19, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, -1, "Scholarship- FIA", "FIA Scholarship", "Scholarship- FIA", "Credits for FIA camperships", "Better handling and tracking as split pay credit!", "Trans", "ScholarshipExpense", "ProgramRevenue", "Credits for FIA camperships", "Better handling and tracking as split pay credit!", "EventEnd", "ScholarshipExpense", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 20, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, -1, "Scholarship- Church", "Church Scholarship", "Scholarship- Church", "Credits for church camperships", "Better handling and tracking as split pay credit!", "Trans", "ScholarshipExpense", "ProgramRevenue", "Credits for church camperships", "Better handling and tracking as split pay credit!", "EventEnd", "ScholarshipExpense", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 21, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "Other Charge", "Other Debit", "Non Accounting Charge", "", "", "", "", "", "", "", "", "", "", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 22, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Discount- Staff 2", "Staff Discount", "Discount- Staff 2", "Discounts for staff applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts for staff applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 24, -1, 0, -1, 0, 0, 0, -1, -1, 0, -1, 0, "Spending Money Refund", "Spending Money Refund", "Spending Money Refund", "Cash refunds of spending money", "", "Trans", "StoreRevenue", "PaymentMethod", "Cash refunds of spending money", "", "Trans", "StoreUnearned", "PaymentMethod", "EventEnd", "StoreRevenue", "StoreUnearned");
                subAppendTlkpTransTypeRecord(_cmdCT, 25, -1, -1, -1, 0, 0, 0, -1, -1, 0, -1, 0, "Spending Money: Initial Prepaid", "Spending Money Initial", "Initial Prepaid Spending Money", "Prepayment of spending money", "", "Trans", "PaymentMethod", "StoreRevenue", "Prepayment of spending money", "", "Trans", "PaymentMethod", "StoreUnearned", "EventEnd", "StoreUnearned", "StoreRevenue");
                subAppendTlkpTransTypeRecord(_cmdCT, 26, -1, 0, -1, 0, 0, 0, -1, -1, 0, -1, 0, "Spending Money Withdrawal", "Spending Money Withdrawal", "Spending Money Withdrawal", "Cash withdrawls of spending money", "", "Trans", "StoreRevenue", "PaymentMethod", "Cash withdrawls of spending money", "", "Trans", "StoreUnearned", "PaymentMethod", "EventEnd", "StoreRevenue", "StoreUnearned");
                subAppendTlkpTransTypeRecord(_cmdCT, 27, -1, -1, -1, 0, 0, 0, -1, -1, 0, -1, 0, "Spending Money: Additional Prepaid", "Spending Money Additional", "Additional Prepaid Spending Money", "Prepayment of spending money", "", "Trans", "PaymentMethod", "StoreRevenue", "Prepayment of spending money", "", "Trans", "PaymentMethod", "StoreUnearned", "EventEnd", "StoreUnearned", "StoreRevenue");
                subAppendTlkpTransTypeRecord(_cmdCT, 33, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Discount- Sibling", "Sibling Discount", "Discount- Sibling", "Discounts for siblings applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts for siblings applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 34, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Camper Exchange Credit", "Camper Exchange Program", "Camper Exchange Credit", "Credits applied to registrant in exchange for tuition at another camp", "Better handling and tracking as split pay credit!", "Trans", "CamperExchangeClearing", "ProgramRevenue", "Credits applied to registrant in exchange for tuition at another camp", "Better handling and tracking as split pay credit!", "Trans", "CamperExchangeClearing", "ProgramUnearned", "EventEnd", "ProgramUnearned", "ProgramReceivables");
                subAppendTlkpTransTypeRecord(_cmdCT, 35, -1, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, "Photo Fee", "Photo", "Photo Fee", "Photo fees charged manually to registrants", "", "", "", "", "Photo fees charged manually to registrants", "", "EventEnd", "ProgramReceivables", "ProgramActivityRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 36, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Discount- Early Registration", "Discount - Early Reg.", "Discount- Early Registration", "Discounts for early registration applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts for early registration applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 37, -1, 0, 0, -1, 0, 0, 0, -1, 0, 0, 0, "Activity Fee", "Activity Charge", "Activity 1 Fee", "Activity fees charged automatically to registrants", "", "", "", "", "Activity fees charged automatically to registrants", "", "EventEnd", "ProgramReceivables", "ProgramActivityRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 38, -1, 0, 0, -1, 0, 0, 0, -1, 0, 0, 0, "Registration Hold Charge", "Split Pay Amt", "Registration Hold Charge", "Charge to third party for a registration", "What is the reference info?", "", "", "", "Automatic charge to third party when using a registration hold", "", "EventEnd", "ProgramReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 39, -1, -1, 0, -1, 0, 0, -1, -1, 0, 0, 0, "Registration Hold Credit", "Split Pay Credit", "Registration Hold Credit", "Credits applied to registrants when third parties assume responsibility for payment", "What is the reference info?  Isn't this a manual trans?", "Trans", "ProgramReceivables", "ProgramRevenue", "Credits applied automatically to registrations using a hold on a block", "", "EventEnd", "ProgramRevenue", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 40, -1, 0, 0, -1, -1, 0, -1, -1, 0, 0, 0, "Group Fees Charge", "Guest Group Tuition", "Group Fees Charge", "Guest type fees charged automatically to group sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "Guest type fees charged automatically to group sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 41, -1, -1, 0, 0, -1, 0, -1, -1, 0, -1, 0, "Group Payment", "Guest Group Payment", "Group Payment", "Payments made by group sponsors after their event", "", "Trans", "PaymentMethod", "RentalReceivables", "Payments made by group sponsors after their event", "", "Trans", "PaymentMethod", "RentalReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 42, -1, -1, 0, 0, -1, 0, -1, -1, 0, -1, 0, "Group Deposit", "Guest Group Deposit", "Group Deposit", "Prepayments made by group sponsors before their event", "", "Trans", "PaymentMethod", "RentalReceivables", "Prepayments made by group sponsors before their event", "", "Trans", "PaymentMethod", "RentalUnearned", "EventEnd", "RentalUnearned", "RentalReceivables");
                subAppendTlkpTransTypeRecord(_cmdCT, 43, -1, 0, 0, -1, -1, 0, -1, -1, 0, 0, 0, "Group Activity Charge", "Guest Group Activity", "Group Activity Charge", "Activity and activity package fees charged automatically to group event sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "Activity and activity package fees charged automatically to group event sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 44, -1, -1, 0, 0, -1, 0, -1, -1, 0, 0, 0, "Group Discount", "Discount - Guest Type", "Group Discount", "Discounts applied to group charges", "", "EventEnd", "RentalDiscounts", "RentalReceivables", "Discounts applied to group charges", "", "EventEnd", "RentalDiscounts", "RentalReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 45, -1, 0, 0, -1, 0, 0, 0, -1, 0, 0, 0, "Activity Package Fee", "Unlimited Activity Package", "Activity Package Fee", "Activity package fees charged automatically to registrants", "", "", "", "", "Activity package fees charged automatically to registrants", "", "EventEnd", "ProgramReceivables", "ProgramActivityRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 46, -1, 0, 0, 0, -1, 0, -1, -1, 0, 0, 0, "Group Rental Housing Charge", "Guest Group Housing", "Group Rental Housing Charge", "Housing fees charged automatically to group sponsors", "Why 2 automatic transactions?", "EventEnd", "RentalReceivables", "ProgramRevenue", "Housing fees charged automatically to group rental sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 47, -1, 0, 0, -1, -1, 0, -1, -1, 0, 0, 0, "Group Resource Charge", "Resource Charge", "Group Resource Charge", "Resource fees charged automatically to group sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "Resource fees charged automatically to group sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 48, -1, 0, 0, -1, -1, 0, -1, -1, 0, 0, 0, "Group Event Housing Charge", "Housing Charge", "Group Event Housing Charge", "Housing fees charged automatically to group sponsors", "Why 2 automatic transactions?", "EventEnd", "RentalReceivables", "ProgramRevenue", "Housing fees charged automatically to group registrations", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 49, -1, 0, 0, -1, -1, 0, 0, -1, 0, 0, 0, "Group Registration Fee", "Event Registration Tuition", "Group Registration Fee", "", "Where is this used automatically?", "", "", "", "Fees charged automatically to group event registrations based on guest type", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 50, -1, 0, 0, -1, 0, 0, 0, -1, 0, 0, 0, "Accounts Receivable Charge", "Accounts Receivable Charge", "Accounts Receivable Charge", "All fees are charged to A/R at EventEnd by accrual conventions", "Is this where the automatic 'move block balances to A/R' goes?", "Trans", "ProgramReceivables", "PaymentMethod", "Miscellaneous Program Charge", "Is this where the automatic 'move block balances to A/R' goes?", "EventEnd", "ProgramReceivables", "ProgramRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 53, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Write-Off", "Write-Off", "Write-Off", "Writeoff of Accounts Receivable", "", "Trans", "ProgramDiscounts", "ProgramRevenue", "Writeoff of Accounts Receivable", "", "Trans", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 54, -1, -1, 0, -1, 0, 0, -1, -1, 0, 0, 0, "Discount- Constituent", "Discount - Const. Church", "Discount- Constituent", "Discounts applied automatically to registrants that are members of constituent congregations", "", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts applied automatically to registrants that are members of constituent congregations", "", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 55, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Rebate", "Rebate", "Rebate", "Rebates applied manually to registrants", "", "Trans", "ProgramDiscounts", "ProgramRevenue", "Rebates applied manually to registrants", "", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 56, -1, 0, 0, -1, 0, -1, -1, -1, -1, 0, 0, "Gift- Cash", "Gift - Cash", "Gift- Cash", "Cash contributions made by donors", "Why isn't this auto?", "Trans", "PaymentMethod", "GiftRevenue", "Cash contributions made by donors", "", "Trans", "PaymentMethod", "GiftRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 57, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, "Gift- Non-Cash", "Gift - Non-Cash", "Gift- Non-Cash", "Needs definition by Bob", "", "", "", "", "Needs definition by Bob", "", "", "", "", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 58, -1, 0, 0, -1, 0, -1, -1, -1, -1, -1, 0, "Gift- Pledge Payment", "Gift - Pledge Payment", "Gift- Pledge Payment", "Cash contributions that satisfy a promise to give", "Why isn't this auto?", "Trans", "PaymentMethod", "PledgeReceivables", "Cash contributions that satisfy a promise to give", "", "Trans", "PaymentMethod", "PledgeReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 59, -1, 0, 0, -1, 0, -1, -1, -1, -1, -1, 0, "Gift- Matching", "Gift - Matching", "Gift- Matching", "Cash contributions made by donors accompanied by a claim for a whole or partial matching amount from an employer, fraternal or other agency", "Could be used to classify the matching gift but more useful as a way to segregate and track gifts made with a match", "Trans", "PaymentMethod", "GiftRevenue", "Cash contributions made by donors accompanied by a claim for a whole or partial matching amount from an employer, fraternal or other agency", "", "Trans", "PaymentMethod", "GiftRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 60, -1, -1, 0, 0, 0, 0, -1, -1, 0, -1, 0, "Accounts Receivable Payment", "Payment for Accounts Receivable", "Accounts Receivable Payment", "Payments on an accounts receivable balance", "", "Trans", "PaymentMethod", "ProgramReceivables", "Payments on an accounts receivable balance", "", "Trans", "PaymentMethod", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 61, -1, 0, 0, 0, -1, 0, -1, -1, 0, 0, 0, "Group Cancellation Charge", "Group Cancellation Charge", "Group Cancellation Charge", "Cancellation fees charged manually to group sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "Cancellation fees charged manually to group sponsors", "", "Trans", "RentalReceivables", "RentalMiscellaneous", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 62, -1, 0, 0, 0, -1, 0, -1, -1, 0, 0, 0, "Group Payment Refund", "Group Refund Charge", "Group Payment Refund", "", "Refund and charge are opposites!", "Tran", "RentalReceivables", "PaymentMethod", "Cash refunds made to groups", "", "Trans", "RentalUnearned", "PaymentMethod", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 63, -1, 0, 0, 0, -1, 0, -1, -1, 0, 0, 0, "Group Miscellaneous Charge", "Misc Group Charge", "Group Miscellaneous Charge", "Miscellaneous fees charged manually to group sponsors", "", "EventEnd", "RentalReceivables", "ProgramRevenue", "Miscellaneous fees charged manually to group sponsors", "", "EventEnd", "RentalReceivables", "RentalMiscellaneous", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 64, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, "Spending Money Complimentary", "Spending Money Complimentary", "Spending Money Complimentary", "Could reduce revenue but that should be done at CGS", "", "", "", "", "Could reduce revenue but that should be done at CGS", "", "", "", "", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 65, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Discount- Volunteer", "Discount - Volunteer", "Discount- Volunteer", "Discounts for volunteers applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts for volunteers applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 66, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Discount- Staff", "Discount - Staff", "Discount- Staff 1", "Discounts for staff applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts for staff applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 67, -1, 0, 0, -1, 0, -1, -1, -1, 0, 0, 0, "Pledge- Scheduled Payment", "Pledge - Scheduled Payment", "Pledge- Scheduled Payment", "Promises to give cash or other assets", "Why isn't this auto?", "Trans", "PledgeReceivables", "GiftRevenue", "Promises to give cash or other assets", "", "Trans", "PledgeReceivables", "GiftRevenue", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 68, -1, 0, 0, -1, 0, -1, -1, -1, 0, 0, 0, "Pledge- Write Off", "Pledge - Write Off", "Pledge- Write Off", "Cancellation of promises to give", "Why isn't this auto?", "Trans", "GiftRevenue", "PledgeReceivables", "Cancellation of promises to give", "", "Trans", "GiftRevenue", "PledgeReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 69, -1, -1, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Discount- Registration", "Discount - Registration", "Discount- Registration", "Discounts for other reasons applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "Trans", "ProgramDiscounts", "ProgramRevenue", "Discounts for other reasons applied manually to registrants", "Which trantype is used for each of the new 10 registration discounts?", "EventEnd", "ProgramDiscounts", "ProgramReceivables", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 70, -1, 0, 0, 0, 0, 0, -1, -1, 0, 0, 0, "Late Fee", "Late Fee", "Late Fee", "Late fees charged manually to registrants", "", "Trans", "ProgramReceivables", "ProgramMiscellaneous", "Late fees charged manually to registrants", "", "EventEnd", "ProgramReceivables", "ProgramMiscellaneous", "", "", "");
                subAppendTlkpTransTypeRecord(_cmdCT, 71, -1, -1, 0, -1, 0, 0, 0, -1, 0, 0, 0, "Cancellation of Registration", "Cancellation of Registration", "Cancellation of Registration", "", "", "", "", "", "", "", "EventEnd", "ProgramRevenue", "ProgramReceivables", "EventEnd", "ProgramReceivables", "ProgramUnearned");
            }
        }

        private static void subAppendTlkpTransTypeRecord(OleDbCommand _cmdDB, long lngTransTypeID, int blnAccountingEffect, int blnDebitCredit, int blnSpending, int blnAutoOnly, int blnGGType, int blnGiftType, int blnCashEffect, int blnAccrEffect, int blnLockEffects, int blnMoneyTrans, int blnScholarship, string strTransType, string strOldTranType, string strNewTranType, string strCashExplanation, string strCashQuestions, string strCashDate, string strCashDebit, string strCashCredit, string strAccrExplanation, string strAccrQuestions, string strAccrDate1, string strAccrDebit1, string strAccrCredit1, string strAccrDate2, string strAccrDebit2, string strAccrCredit2)
        {
            string strSQL = "INSERT INTO tlkpTransType " +
                                "( lngTransTypeID, " +
                                    "blnAccountingEffect, blnDebitCredit, blnSpending, blnAutoOnly, blnGGType, blnGiftType, blnCashEffect, blnAccrEffect, blnLockEffects, blnMoneyTrans, blnScholarship, " +
                                    "strTransType, strOldTranType, strNewTranType, strCashExplanation, strCashQuestions, strCashDate, strCashDebit, strCashCredit, strAccrExplanation, strAccrQuestions, strAccrDate1, strAccrDebit1, strAccrCredit1, strAccrDate2, strAccrDebit2, strAccrCredit2 ) " +
                                "VALUES " +
                                "( " + lngTransTypeID.ToString() + ", " +
                                    blnAccountingEffect + ", " + blnDebitCredit + ", " + blnSpending + ", " + blnAutoOnly + ", " + blnGGType + ", " + blnGiftType + ", " + blnCashEffect + ", " + blnAccrEffect + ", " + blnLockEffects + ", " + blnMoneyTrans + ", " + blnScholarship + ", " +
                                    "\"" + strTransType + "\", \"" + strOldTranType + "\", \"" + strNewTranType + "\", \"" + strCashExplanation + "\", \"" + strCashQuestions + "\", \"" + strCashDate + "\", \"" + strCashDebit + "\", \"" + strCashCredit + "\", \"" + strAccrExplanation + "\", \"" + strAccrQuestions + "\", \"" + strAccrDate1 + "\", \"" + strAccrDebit1 + "\", \"" + strAccrCredit1 + "\", \"" + strAccrDate2 + "\", \"" + strAccrDebit2 + "\", \"" + strAccrCredit2 + "\")";

            _cmdDB.CommandText = strSQL;
            _cmdDB.ExecuteNonQuery();
        }

        private static bool fcnAddField(OleDbCommand _cmdDB, string _strTbl, string _strField, string _strDataType, string _strDefault, ref string _strErr)
        {
            string strSQL = "";

            bool blnRes = false;
            bool blnAddFields = false;

            try
            {
                strSQL = "SELECT " + _strField + " " +
                        "FROM " + _strTbl;

                _cmdDB.CommandText = strSQL;

                try { _cmdDB.ExecuteNonQuery(); }
                catch { blnAddFields = true; }

                if (blnAddFields)
                {
                    string strDefault = "";

                    if (_strDefault != "")
                        strDefault = " DEFAULT " + _strDefault;

                    strSQL = "ALTER TABLE " + _strTbl + " " +
                            "ADD COLUMN " + _strField + " " + _strDataType + strDefault;

                    _cmdDB.CommandText = strSQL;

                    _cmdDB.ExecuteNonQuery();
                }
                blnRes = true;
            }
            catch (Exception ex)
            {
                blnRes = false;
                _strErr = ex.Message;
            }

            return blnRes;
        }

        public static string fcnFindFile(string _strTitle)
        {
            OpenFileDialog dlgFile = new OpenFileDialog();

            string strRes = "";

            try
            {

                dlgFile.Filter = "MSAccess Database Files (*.mdb, *.accdb)|*.mdb;*.accdb";
                dlgFile.Title = _strTitle;

                if (dlgFile.ShowDialog() == DialogResult.OK)
                    strRes = dlgFile.FileName;
                else
                    strRes = "";
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("fcnFindFile", ex);
            }

            return strRes;
        }

        public static bool IsDate(object Expression)
        {
            string strDate = Expression.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string fcnFileToStr(string _strFile)
        {
            string strRes;

            using (StreamReader srdFile = new StreamReader(_strFile))
            {
                strRes = srdFile.ReadToEnd();

                srdFile.Close();
            }

            return strRes;
        }

        private static void subPopCountries()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "INSERT INTO tlkpCountry " +
                        "( lngCountryID, " +
                            "strCountry ) " +
                        "VALUES " +
                        "( @lngCountryID, " +
                            "@strCountry )";

                using (OleDbCommand cmdDB = conDB.CreateCommand())
                {
                    cmdDB.CommandText = strSQL;

                    cmdDB.Parameters.Add(new OleDbParameter("@lngCountryID", 1));
                    cmdDB.Parameters.Add(new OleDbParameter("@strCountry", "USA"));

                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 2;
                    cmdDB.Parameters[1].Value = "Canada";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 3;
                    cmdDB.Parameters[1].Value = "Andorra";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 4;
                    cmdDB.Parameters[1].Value = "United Arab Emirates";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 5;
                    cmdDB.Parameters[1].Value = "Afghanistan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 6;
                    cmdDB.Parameters[1].Value = "Antigua and Barbuda";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 7;
                    cmdDB.Parameters[1].Value = "Anguilla";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 8;
                    cmdDB.Parameters[1].Value = "Albania";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 9;
                    cmdDB.Parameters[1].Value = "Armenia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 10;
                    cmdDB.Parameters[1].Value = "Netherlands Antilles";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 11;
                    cmdDB.Parameters[1].Value = "Angola";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 12;
                    cmdDB.Parameters[1].Value = "Antarctica";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 13;
                    cmdDB.Parameters[1].Value = "Argentina";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 14;
                    cmdDB.Parameters[1].Value = "American Samoa";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 15;
                    cmdDB.Parameters[1].Value = "Austria";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 16;
                    cmdDB.Parameters[1].Value = "Australia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 17;
                    cmdDB.Parameters[1].Value = "Aruba";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 18;
                    cmdDB.Parameters[1].Value = "Azerbaijan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 19;
                    cmdDB.Parameters[1].Value = "Bosnia and Herzegovina";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 20;
                    cmdDB.Parameters[1].Value = "Barbados";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 21;
                    cmdDB.Parameters[1].Value = "Bangladesh";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 22;
                    cmdDB.Parameters[1].Value = "Belgium";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 23;
                    cmdDB.Parameters[1].Value = "Burkina Faso";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 24;
                    cmdDB.Parameters[1].Value = "Bulgaria";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 25;
                    cmdDB.Parameters[1].Value = "Bahrain";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 26;
                    cmdDB.Parameters[1].Value = "Burundi";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 27;
                    cmdDB.Parameters[1].Value = "Benin";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 28;
                    cmdDB.Parameters[1].Value = "Bermuda";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 29;
                    cmdDB.Parameters[1].Value = "Brunei Darussalam";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 30;
                    cmdDB.Parameters[1].Value = "Bolivia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 31;
                    cmdDB.Parameters[1].Value = "Brazil";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 32;
                    cmdDB.Parameters[1].Value = "Bahamas";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 33;
                    cmdDB.Parameters[1].Value = "Bhutan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 34;
                    cmdDB.Parameters[1].Value = "Bouvet Island";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 35;
                    cmdDB.Parameters[1].Value = "Botswana";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 36;
                    cmdDB.Parameters[1].Value = "Belarus";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 37;
                    cmdDB.Parameters[1].Value = "Belize";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 39;
                    cmdDB.Parameters[1].Value = "Cocos (Keeling) Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 40;
                    cmdDB.Parameters[1].Value = "Central African Republic";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 41;
                    cmdDB.Parameters[1].Value = "Congo";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 42;
                    cmdDB.Parameters[1].Value = "Switzerland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 43;
                    cmdDB.Parameters[1].Value = "Cote D'Ivoire (Ivory Coast)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 44;
                    cmdDB.Parameters[1].Value = "Cook Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 45;
                    cmdDB.Parameters[1].Value = "Chile";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 46;
                    cmdDB.Parameters[1].Value = "Cameroon";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 47;
                    cmdDB.Parameters[1].Value = "China";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 48;
                    cmdDB.Parameters[1].Value = "Colombia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 49;
                    cmdDB.Parameters[1].Value = "Costa Rica";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 50;
                    cmdDB.Parameters[1].Value = "Czechoslovakia (former)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 51;
                    cmdDB.Parameters[1].Value = "Cuba";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 52;
                    cmdDB.Parameters[1].Value = "Cape Verde";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 53;
                    cmdDB.Parameters[1].Value = "Christmas Island";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 54;
                    cmdDB.Parameters[1].Value = "Cyprus";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 55;
                    cmdDB.Parameters[1].Value = "Czech Republic";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 56;
                    cmdDB.Parameters[1].Value = "Germany";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 57;
                    cmdDB.Parameters[1].Value = "Djibouti";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 58;
                    cmdDB.Parameters[1].Value = "Denmark";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 59;
                    cmdDB.Parameters[1].Value = "Dominica";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 60;
                    cmdDB.Parameters[1].Value = "Dominican Republic";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 61;
                    cmdDB.Parameters[1].Value = "Algeria";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 62;
                    cmdDB.Parameters[1].Value = "Ecuador";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 63;
                    cmdDB.Parameters[1].Value = "Estonia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 64;
                    cmdDB.Parameters[1].Value = "Egypt";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 65;
                    cmdDB.Parameters[1].Value = "Western Sahara";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 66;
                    cmdDB.Parameters[1].Value = "Eritrea";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 67;
                    cmdDB.Parameters[1].Value = "Spain";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 68;
                    cmdDB.Parameters[1].Value = "Ethiopia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 69;
                    cmdDB.Parameters[1].Value = "Finland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 70;
                    cmdDB.Parameters[1].Value = "Fiji";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 71;
                    cmdDB.Parameters[1].Value = "Falkland Islands (Malvinas)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 72;
                    cmdDB.Parameters[1].Value = "Micronesia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 73;
                    cmdDB.Parameters[1].Value = "Faroe Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 74;
                    cmdDB.Parameters[1].Value = "France";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 76;
                    cmdDB.Parameters[1].Value = "Gabon";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 77;
                    cmdDB.Parameters[1].Value = "Great Britain (UK)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 78;
                    cmdDB.Parameters[1].Value = "Grenada";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 79;
                    cmdDB.Parameters[1].Value = "Georgia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 80;
                    cmdDB.Parameters[1].Value = "French Guiana";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 81;
                    cmdDB.Parameters[1].Value = "Ghana";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 82;
                    cmdDB.Parameters[1].Value = "Gibraltar";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 83;
                    cmdDB.Parameters[1].Value = "Greenland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 84;
                    cmdDB.Parameters[1].Value = "Gambia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 85;
                    cmdDB.Parameters[1].Value = "Guinea";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 86;
                    cmdDB.Parameters[1].Value = "Guadeloupe";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 87;
                    cmdDB.Parameters[1].Value = "Equatorial Guinea";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 88;
                    cmdDB.Parameters[1].Value = "Greece";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 89;
                    cmdDB.Parameters[1].Value = "S. Georgia and S. Sandwich Isls.";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 90;
                    cmdDB.Parameters[1].Value = "Guatemala";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 91;
                    cmdDB.Parameters[1].Value = "Guam";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 92;
                    cmdDB.Parameters[1].Value = "Guinea-Bissau";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 93;
                    cmdDB.Parameters[1].Value = "Guyana";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 94;
                    cmdDB.Parameters[1].Value = "Hong Kong";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 95;
                    cmdDB.Parameters[1].Value = "Heard and McDonald Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 96;
                    cmdDB.Parameters[1].Value = "Honduras";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 97;
                    cmdDB.Parameters[1].Value = "Croatia (Hrvatska)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 98;
                    cmdDB.Parameters[1].Value = "Haiti";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 99;
                    cmdDB.Parameters[1].Value = "Hungary";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 100;
                    cmdDB.Parameters[1].Value = "Indonesia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 101;
                    cmdDB.Parameters[1].Value = "Ireland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 102;
                    cmdDB.Parameters[1].Value = "Israel";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 103;
                    cmdDB.Parameters[1].Value = "India";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 104;
                    cmdDB.Parameters[1].Value = "British Indian Ocean Territory";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 105;
                    cmdDB.Parameters[1].Value = "Iraq";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 106;
                    cmdDB.Parameters[1].Value = "Iran";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 107;
                    cmdDB.Parameters[1].Value = "Iceland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 108;
                    cmdDB.Parameters[1].Value = "Italy";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 109;
                    cmdDB.Parameters[1].Value = "Jamaica";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 110;
                    cmdDB.Parameters[1].Value = "Jordan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 111;
                    cmdDB.Parameters[1].Value = "Japan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 112;
                    cmdDB.Parameters[1].Value = "Kenya";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 113;
                    cmdDB.Parameters[1].Value = "Kyrgyzstan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 114;
                    cmdDB.Parameters[1].Value = "Cambodia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 115;
                    cmdDB.Parameters[1].Value = "Kiribati";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 116;
                    cmdDB.Parameters[1].Value = "Comoros";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 117;
                    cmdDB.Parameters[1].Value = "Saint Kitts and Nevis";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 118;
                    cmdDB.Parameters[1].Value = "Korea (North)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 119;
                    cmdDB.Parameters[1].Value = "Korea (South)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 120;
                    cmdDB.Parameters[1].Value = "Kuwait";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 121;
                    cmdDB.Parameters[1].Value = "Cayman Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 122;
                    cmdDB.Parameters[1].Value = "Kazakhstan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 123;
                    cmdDB.Parameters[1].Value = "Laos";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 124;
                    cmdDB.Parameters[1].Value = "Lebanon";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 125;
                    cmdDB.Parameters[1].Value = "Saint Lucia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 126;
                    cmdDB.Parameters[1].Value = "Liechtenstein";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 127;
                    cmdDB.Parameters[1].Value = "Sri Lanka";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 128;
                    cmdDB.Parameters[1].Value = "Liberia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 129;
                    cmdDB.Parameters[1].Value = "Lesotho";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 130;
                    cmdDB.Parameters[1].Value = "Lithuania";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 131;
                    cmdDB.Parameters[1].Value = "Luxembourg";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 132;
                    cmdDB.Parameters[1].Value = "Latvia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 133;
                    cmdDB.Parameters[1].Value = "Libya";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 134;
                    cmdDB.Parameters[1].Value = "Morocco";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 135;
                    cmdDB.Parameters[1].Value = "Monaco";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 136;
                    cmdDB.Parameters[1].Value = "Moldova";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 137;
                    cmdDB.Parameters[1].Value = "Madagascar";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 138;
                    cmdDB.Parameters[1].Value = "Marshall Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 139;
                    cmdDB.Parameters[1].Value = "Macedonia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 140;
                    cmdDB.Parameters[1].Value = "Mali";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 141;
                    cmdDB.Parameters[1].Value = "Myanmar";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 142;
                    cmdDB.Parameters[1].Value = "Mongolia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 143;
                    cmdDB.Parameters[1].Value = "Macau";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 144;
                    cmdDB.Parameters[1].Value = "Northern Mariana Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 145;
                    cmdDB.Parameters[1].Value = "Martinique";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 146;
                    cmdDB.Parameters[1].Value = "Mauritania";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 147;
                    cmdDB.Parameters[1].Value = "Montserrat";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 148;
                    cmdDB.Parameters[1].Value = "Malta";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 149;
                    cmdDB.Parameters[1].Value = "Mauritius";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 150;
                    cmdDB.Parameters[1].Value = "Maldives";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 151;
                    cmdDB.Parameters[1].Value = "Malawi";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 152;
                    cmdDB.Parameters[1].Value = "Mexico";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 153;
                    cmdDB.Parameters[1].Value = "Malaysia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 154;
                    cmdDB.Parameters[1].Value = "Mozambique";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 155;
                    cmdDB.Parameters[1].Value = "Namibia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 156;
                    cmdDB.Parameters[1].Value = "New Caledonia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 157;
                    cmdDB.Parameters[1].Value = "Niger";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 158;
                    cmdDB.Parameters[1].Value = "Norfolk Island";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 159;
                    cmdDB.Parameters[1].Value = "Nigeria";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 160;
                    cmdDB.Parameters[1].Value = "Nicaragua";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 161;
                    cmdDB.Parameters[1].Value = "Netherlands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 162;
                    cmdDB.Parameters[1].Value = "Norway";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 163;
                    cmdDB.Parameters[1].Value = "Nepal";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 164;
                    cmdDB.Parameters[1].Value = "Nauru";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 165;
                    cmdDB.Parameters[1].Value = "Neutral Zone";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 166;
                    cmdDB.Parameters[1].Value = "Niue";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 167;
                    cmdDB.Parameters[1].Value = "New Zealand (Aotearoa)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 168;
                    cmdDB.Parameters[1].Value = "Oman";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 169;
                    cmdDB.Parameters[1].Value = "Panama";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 170;
                    cmdDB.Parameters[1].Value = "Peru";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 171;
                    cmdDB.Parameters[1].Value = "French Polynesia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 172;
                    cmdDB.Parameters[1].Value = "Papua New Guinea";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 173;
                    cmdDB.Parameters[1].Value = "Philippines";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 174;
                    cmdDB.Parameters[1].Value = "Pakistan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 175;
                    cmdDB.Parameters[1].Value = "Poland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 176;
                    cmdDB.Parameters[1].Value = "St. Pierre and Miquelon";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 177;
                    cmdDB.Parameters[1].Value = "Pitcairn";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 178;
                    cmdDB.Parameters[1].Value = "Puerto Rico";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 179;
                    cmdDB.Parameters[1].Value = "Portugal";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 180;
                    cmdDB.Parameters[1].Value = "Palau";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 181;
                    cmdDB.Parameters[1].Value = "Paraguay";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 182;
                    cmdDB.Parameters[1].Value = "Qatar";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 183;
                    cmdDB.Parameters[1].Value = "Reunion";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 184;
                    cmdDB.Parameters[1].Value = "Romania";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 185;
                    cmdDB.Parameters[1].Value = "Russian Federation";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 186;
                    cmdDB.Parameters[1].Value = "Rwanda";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 187;
                    cmdDB.Parameters[1].Value = "Saudi Arabia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 188;
                    cmdDB.Parameters[1].Value = "Solomon Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 189;
                    cmdDB.Parameters[1].Value = "Seychelles";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 190;
                    cmdDB.Parameters[1].Value = "Sudan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 191;
                    cmdDB.Parameters[1].Value = "Sweden";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 192;
                    cmdDB.Parameters[1].Value = "Singapore";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 193;
                    cmdDB.Parameters[1].Value = "St. Helena";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 194;
                    cmdDB.Parameters[1].Value = "Slovenia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 195;
                    cmdDB.Parameters[1].Value = "Svalbard and Jan Mayen Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 196;
                    cmdDB.Parameters[1].Value = "Slovak Republic";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 197;
                    cmdDB.Parameters[1].Value = "Sierra Leone";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 198;
                    cmdDB.Parameters[1].Value = "San Marino";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 199;
                    cmdDB.Parameters[1].Value = "Senegal";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 200;
                    cmdDB.Parameters[1].Value = "Somalia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 201;
                    cmdDB.Parameters[1].Value = "Suriname";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 202;
                    cmdDB.Parameters[1].Value = "Sao Tome and Principe";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 203;
                    cmdDB.Parameters[1].Value = "USSR (former)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 204;
                    cmdDB.Parameters[1].Value = "El Salvador";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 205;
                    cmdDB.Parameters[1].Value = "Syria";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 206;
                    cmdDB.Parameters[1].Value = "Swaziland";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 207;
                    cmdDB.Parameters[1].Value = "Turks and Caicos Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 208;
                    cmdDB.Parameters[1].Value = "Chad";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 209;
                    cmdDB.Parameters[1].Value = "French Southern Territories";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 210;
                    cmdDB.Parameters[1].Value = "Togo";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 211;
                    cmdDB.Parameters[1].Value = "Thailand";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 212;
                    cmdDB.Parameters[1].Value = "Tajikistan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 213;
                    cmdDB.Parameters[1].Value = "Tokelau";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 214;
                    cmdDB.Parameters[1].Value = "Turkmenistan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 215;
                    cmdDB.Parameters[1].Value = "Tunisia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 216;
                    cmdDB.Parameters[1].Value = "Tonga";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 217;
                    cmdDB.Parameters[1].Value = "East Timor";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 218;
                    cmdDB.Parameters[1].Value = "Turkey";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 219;
                    cmdDB.Parameters[1].Value = "Trinidad and Tobago";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 220;
                    cmdDB.Parameters[1].Value = "Tuvalu";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 221;
                    cmdDB.Parameters[1].Value = "Taiwan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 222;
                    cmdDB.Parameters[1].Value = "Tanzania";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 223;
                    cmdDB.Parameters[1].Value = "Ukraine";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 224;
                    cmdDB.Parameters[1].Value = "Uganda";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 225;
                    cmdDB.Parameters[1].Value = "United Kingdom";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 226;
                    cmdDB.Parameters[1].Value = "US Minor Outlying Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 228;
                    cmdDB.Parameters[1].Value = "Uruguay";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 229;
                    cmdDB.Parameters[1].Value = "Uzbekistan";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 230;
                    cmdDB.Parameters[1].Value = "Vatican City State (Holy See)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 231;
                    cmdDB.Parameters[1].Value = "Saint Vincent and the Grenadines";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 232;
                    cmdDB.Parameters[1].Value = "Venezuela";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 233;
                    cmdDB.Parameters[1].Value = "Virgin Islands (British)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 234;
                    cmdDB.Parameters[1].Value = "Virgin Islands (U.S.)";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 235;
                    cmdDB.Parameters[1].Value = "Viet Nam";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 236;
                    cmdDB.Parameters[1].Value = "Vanuatu";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 237;
                    cmdDB.Parameters[1].Value = "Wallis and Futuna Islands";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 238;
                    cmdDB.Parameters[1].Value = "Samoa";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 239;
                    cmdDB.Parameters[1].Value = "Yemen";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 240;
                    cmdDB.Parameters[1].Value = "Mayotte";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 241;
                    cmdDB.Parameters[1].Value = "Yugoslavia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 242;
                    cmdDB.Parameters[1].Value = "South Africa";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 243;
                    cmdDB.Parameters[1].Value = "Zambia";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 244;
                    cmdDB.Parameters[1].Value = "Zaire";
                    cmdDB.ExecuteNonQuery();

                    cmdDB.Parameters[0].Value = 245;
                    cmdDB.Parameters[1].Value = "Zimbabwe";
                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }
        }

        private static bool fcnGetWebCustomFieldDefs(clsCustomFieldIRDef[] _cstWebFlags, clsCustomFieldIRDef[] _cstWebFields)
        {
            string strSQL = "";

            bool blnRes = false;

            //init
            for (int intI = 0; intI < _cstWebFlags.Length; intI++)
            {
                _cstWebFlags[intI] = new clsCustomFieldIRDef();
                _cstWebFields[intI] = new clsCustomFieldIRDef();
            }

            try
            {
                using (wsXferEventInfo.XferEventInfo wsDLFlags = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                {
                    strSQL = "SELECT strFieldName, strValue " +
                            "FROM tblCustomVals " +
                        "WHERE lngProgramID = 0 AND " +
                            "lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "(strFieldName LIKE 'blnUse%Field%' OR " +
                                "strFieldName LIKE 'blnUse%Flag%' OR " +
                                "strFieldName LIKE 'strFlag%' OR " +
                                "strFieldName LIKE 'strField%') " +
                            "ORDER BY strFieldName";

                    string strWebXML = "";

                    try { strWebXML = wsDLFlags.fcnGetRecords(strSQL, "tblCustomVals", clsWebTalk.strWebConn); }
                    catch (Exception ex) { MessageBox.Show("There was an error retrieving custom field definitions from the web: " + ex.Message); }

                    DataSet dsXML = new DataSet("tblCustomVals");

                    dsXML.ReadXml(new System.IO.StringReader(strWebXML), XmlReadMode.ReadSchema);

                    foreach (DataTable tbl in dsXML.Tables)
                    {
                        foreach (DataRow row in tbl.Rows)
                        {
                            string strFieldName = "";
                            string strFieldVal = "";

                            foreach (DataColumn col in tbl.Columns)
                            {
                                string strColName = col.ColumnName;
                                string strColVal = Convert.ToString(row[col]);

                                if (strColName == "strFieldName") strFieldName = strColVal;
                                else if (strColName == "strValue") strFieldVal = strColVal;
                            }

                            //evaluate field, val, set control value
                            if (strFieldName.IndexOf("blnUseCamperField") >= 0)
                                _cstWebFields[Convert.ToInt32(strFieldName.Substring(17, strFieldName.Length - 17)) - 1].blnUseCamper = Convert.ToBoolean(strFieldVal);
                            else if (strFieldName.IndexOf("blnUseCamperFlag") >= 0)
                                _cstWebFlags[Convert.ToInt32(strFieldName.Substring(16, strFieldName.Length - 16)) - 1].blnUseCamper = Convert.ToBoolean(strFieldVal);
                            else if (strFieldName.IndexOf("blnUseProfileField") >= 0)
                                _cstWebFields[Convert.ToInt32(strFieldName.Substring(18, strFieldName.Length - 18)) - 1].blnUseProfile = Convert.ToBoolean(strFieldVal);
                            else if (strFieldName.IndexOf("blnUseProfileFlag") >= 0)
                                _cstWebFlags[Convert.ToInt32(strFieldName.Substring(17, strFieldName.Length - 17)) - 1].blnUseProfile = Convert.ToBoolean(strFieldVal);
                            else if (strFieldName.IndexOf("strField") >= 0)
                                _cstWebFields[Convert.ToInt32(strFieldName.Substring(8, strFieldName.Length - 8)) - 1].mmoWebCaption = strFieldVal;
                            else if (strFieldName.IndexOf("strFlag") >= 0)
                                _cstWebFlags[Convert.ToInt32(strFieldName.Substring(7, strFieldName.Length - 7)) - 1].mmoWebCaption = strFieldVal;
                        }
                    }
                }

                blnRes = true;
            }
            catch (Exception ex)
            {
                blnRes = false;
            }

            return blnRes;
        }

        private static bool fcnGetWebCustomRegFieldDefs(clsCustomFieldRegDef[] _cstRegWebFlags,clsCustomFieldRegDef[] _cstRegWebFields)
        {
            string strSQL = "";

            bool blnRes = false;

            //init
            for (int intI = 0; intI <_cstRegWebFlags.Length; intI++)
            {
                _cstRegWebFlags[intI] = new clsCustomFieldRegDef();
                _cstRegWebFields[intI] = new clsCustomFieldRegDef();
            }

            try
            {
                using (wsXferEventInfo.XferEventInfo wsDLFlags = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                {
                    strSQL = "SELECT strFieldName, strValue " +
                            "FROM tblCustomVals " +
                        "WHERE lngProgramID = 0 AND " +
                            "lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "(strFieldName LIKE 'blnUse%Field%' OR " +
                                "strFieldName LIKE 'blnUse%Flag%' OR " +
                                "strFieldName LIKE 'strRegFlag%' OR " +
                                "strFieldName LIKE 'curChargeFlag%' OR " +
                                "strFieldName LIKE 'strRegField%') " +
                            "ORDER BY strFieldName";

                    string strWebXML = "";

                    try { strWebXML = wsDLFlags.fcnGetRecords(strSQL, "tblCustomVals", clsWebTalk.strWebConn); }
                    catch (Exception ex) { MessageBox.Show("There was an error retrieving custom field definitions from the web: " + ex.Message); }

                    DataSet dsXML = new DataSet("tblCustomVals");

                    dsXML.ReadXml(new System.IO.StringReader(strWebXML), XmlReadMode.ReadSchema);

                    foreach (DataTable tbl in dsXML.Tables)
                    {
                        foreach (DataRow row in tbl.Rows)
                        {
                            string strFieldName = "";
                            string strFieldVal = "";

                            foreach (DataColumn col in tbl.Columns)
                            {
                                string strColName = col.ColumnName;
                                string strColVal = Convert.ToString(row[col]);

                                if (strColName == "strFieldName") strFieldName = strColVal;
                                else if (strColName == "strValue") strFieldVal = strColVal;
                            }

                            //evaluate field, val, set control value
                            if (strFieldName.IndexOf("blnUseRegField") >= 0)
                                _cstRegWebFields[Convert.ToInt32(strFieldName.Substring(14, strFieldName.Length - 14)) - 1].blnUseOnline = Convert.ToBoolean(strFieldVal);
                            else if (strFieldName.IndexOf("blnUseRegFlag") >= 0)
                                _cstRegWebFlags[Convert.ToInt32(strFieldName.Substring(13, strFieldName.Length - 13)) - 1].blnUseOnline = Convert.ToBoolean(strFieldVal);
                            else if (strFieldName.IndexOf("strRegField") >= 0)
                                _cstRegWebFields[Convert.ToInt32(strFieldName.Substring(11, strFieldName.Length - 11)) - 1].mmoWebCaption = strFieldVal;
                            else if (strFieldName.IndexOf("strRegFlag") >= 0)
                                _cstRegWebFlags[Convert.ToInt32(strFieldName.Substring(10, strFieldName.Length - 10)) - 1].mmoWebCaption = strFieldVal;
                            else if (strFieldName.IndexOf("curChargeFlag") >= 0)
                                _cstRegWebFlags[Convert.ToInt32(strFieldName.Substring(13, strFieldName.Length - 13)) - 1].decCharge = Convert.ToDecimal(strFieldVal);
                        }
                    }
                }

                blnRes = true;
            }
            catch (Exception ex)
            {
                blnRes = false;
            }

            return blnRes;
        }
    }
}