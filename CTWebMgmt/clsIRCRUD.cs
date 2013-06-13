using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CTWebMgmt
{
    class clsIRCRUD
    {
        public static long fcnAddIRFromWebRecord(long _lngRecordWebID)
        {
            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;

            string strSQL;

            long lngRes = 0;

            conDB.Open();

            strSQL = "INSERT INTO tblRecords " +
                    "( blnFlag1, blnFlag10, blnFlag2, blnFlag3, blnFlag4, blnFlag5, blnFlag6, blnFlag7, blnFlag8, blnFlag9, " +
                        "lngStateID, lngRecordWebID, " +
                        "strAddress, strCity, strEmail, strWorkExt, strCustom1, strCustom10, strCustom2, strCustom3, strCustom4, strCustom5, strCustom6, strCustom7, strCustom8, strCustom9, strFirstName, strHomePhone, strLastCoName, strSpouseName, strWorkPhone, strZip, strMI ) " +
                    "SELECT tblWebRecords.blnFlag1, tblWebRecords.blnFlag10, tblWebRecords.blnFlag2, tblWebRecords.blnFlag3, tblWebRecords.blnFlag4, tblWebRecords.blnFlag5, tblWebRecords.blnFlag6, tblWebRecords.blnFlag7, tblWebRecords.blnFlag8, tblWebRecords.blnFlag9, " +
                        "tblWebRecords.lngStateID, lngRecordWebID, " +
                        "tblWebRecords.strAddress, tblWebRecords.strCity, tblWebRecords.strEMail, tblWebRecords.strWorkExt, tblWebRecords.strCustom1, tblWebRecords.strCustom10, tblWebRecords.strCustom2, tblWebRecords.strCustom3, tblWebRecords.strCustom4, tblWebRecords.strCustom5, tblWebRecords.strCustom6, tblWebRecords.strCustom7, tblWebRecords.strCustom8, tblWebRecords.strCustom9, tblWebRecords.strFirstName, tblWebRecords.strHomePhone, tblWebRecords.strLastCoName, tblWebRecords.strSpouseFName, tblWebRecords.strWorkPhone, tblWebRecords.strZip, tblWebRecords.strMI " +
                    "FROM tblWebRecords " +
                    "WHERE tblWebRecords.lngRecordWebID=" + _lngRecordWebID;

            cmdDB = new OleDbCommand(strSQL, conDB);

            cmdDB.ExecuteNonQuery();

            strSQL = "SELECT @@IDENTITY;";

            cmdDB.CommandText = strSQL;

            try
            {
                long.TryParse(cmdDB.ExecuteScalar().ToString(), out lngRes);
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsIRCRUD.fcnAddIRFromWebProfile", ex);
            }

            conDB.Close();

            cmdDB.Dispose();
            conDB.Dispose();

            return lngRes;
        }

        public static long fcnAddIRFromDonorExpress(long _lngDonorExpressID)
        {
            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;

            string strSQL;

            long lngRes = 0;

            conDB.Open();

            strSQL = "INSERT INTO tblRecords " +
                    "( blnFlag1, blnFlag10, blnFlag2, blnFlag3, blnFlag4, blnFlag5, blnFlag6, blnFlag7, blnFlag8, blnFlag9, " +
                        "lngStateID, lngRecordWebID, " +
                        "strAddress, strCity, strEmail, strWorkExt, strCustom1, strCustom10, strCustom2, strCustom3, strCustom4, strCustom5, strCustom6, strCustom7, strCustom8, strCustom9, strFirstName, strHomePhone, strLastCoName, strSpouseName, strWorkPhone, strZip, strMI ) " +
                    "SELECT tblWebRecords.blnFlag1, tblWebRecords.blnFlag10, tblWebRecords.blnFlag2, tblWebRecords.blnFlag3, tblWebRecords.blnFlag4, tblWebRecords.blnFlag5, tblWebRecords.blnFlag6, tblWebRecords.blnFlag7, tblWebRecords.blnFlag8, tblWebRecords.blnFlag9, " +
                        "tblWebRecords.lngStateID, lngRecordWebID, " +
                        "tblWebRecords.strAddress, tblWebRecords.strCity, tblWebRecords.strEMail, tblWebRecords.strWorkExt, tblWebRecords.strCustom1, tblWebRecords.strCustom10, tblWebRecords.strCustom2, tblWebRecords.strCustom3, tblWebRecords.strCustom4, tblWebRecords.strCustom5, tblWebRecords.strCustom6, tblWebRecords.strCustom7, tblWebRecords.strCustom8, tblWebRecords.strCustom9, tblWebRecords.strFirstName, tblWebRecords.strHomePhone, tblWebRecords.strLastCoName, tblWebRecords.strSpouseFName, tblWebRecords.strWorkPhone, tblWebRecords.strZip, tblWebRecords.strMI " +
                    "FROM tblWebRecords " +
                    "WHERE tblWebRecords.lngRecordWebID=" + _lngDonorExpressID.ToString();

            cmdDB = new OleDbCommand(strSQL, conDB);

            cmdDB.ExecuteNonQuery();

            strSQL = "SELECT @@IDENTITY;";

            cmdDB.CommandText = strSQL;

            try
            {
                long.TryParse(cmdDB.ExecuteScalar().ToString(), out lngRes);
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsIRCRUD.fcnAddIRFromWebProfile", ex);
            }

            conDB.Close();

            cmdDB.Dispose();
            conDB.Dispose();

            return lngRes;
        }

        public static void subUpdateWebID(long _lngRecordID, long _lngRecordWebID)
        {
            string strSQL = "UPDATE tblRecords " +
                            "SET lngRecordWebID=" + _lngRecordWebID + " " +
                            "WHERE lngRecordID=" + _lngRecordID;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }
            return;
        }

        public static void subUpdateBillingInfoCC(long _lngRecordID, string _strPNRef, string _strCardNumber)
        {
            string strXCAlias = "";

            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT strXCAlias " +
                            "FROM tblBillingInfo " +
                            "WHERE lngRecordID=" + _lngRecordID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { strXCAlias = Convert.ToString(cmdDB.ExecuteScalar()); }
                        catch { strXCAlias = ""; }
                    }

                    conDB.Close();
                }
            }
            catch { }

            subUpdateBillingInfoCC(_lngRecordID, _strPNRef, _strCardNumber, strXCAlias);
        }

        public static void subUpdateBillingInfoCC(long _lngRecordID, string _strPNRef, string _strCardNumber, string _strXCAlias, string _strEPSPmtAcctID)
        {
            string strSQL = "";

            bool blnExists = false;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngRecordID " +
                        "FROM tblBillingInfo " +
                        "WHERE lngRecordID=" + _lngRecordID;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drBillingInfo = cmdDB.ExecuteReader())
                    {
                        if (drBillingInfo.Read())
                            blnExists = true;

                        drBillingInfo.Close();
                    }

                    if (blnExists)
                        strSQL = "UPDATE tblBillingInfo " +
                                "SET lngPaymentTypeID=2, " +
                                    "strCCNumber='" + _strCardNumber + "', strPNRef = '" + _strPNRef + "', strXCAlias='" + _strXCAlias + "', strEPSPmtAcctID = @strEPSPmtAcctID " +
                                "WHERE lngRecordID = " + _lngRecordID;
                    else
                        strSQL = "INSERT INTO tblBillingInfo " +
                                "(lngRecordID, lngPaymentTypeID, " +
                                    "strCCNumber, strPNRef, strXCAlias, strEPSPmtAcctID )" +
                                "VALUES " +
                                "(" + _lngRecordID + ", 2, " +
                                    "'" + _strCardNumber + "', '" + _strPNRef + "', '" + _strXCAlias + "', @strEPSPmtAcctID )";

                    cmdDB.CommandText = strSQL;

                    cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", _strEPSPmtAcctID));

                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }

            return;
        }

        public static void subUpdateBillingInfoCC(long _lngRecordID, string _strPNRef, string _strCardNumber, string _strXCAlias)
        {
            string strEPSPmtAcctID = "";

            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT strEPSPmtAcctID " +
                            "FROM tblBillingInfo " +
                            "WHERE lngRecordID=" + _lngRecordID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { strEPSPmtAcctID = Convert.ToString(cmdDB.ExecuteScalar()); }
                        catch { strEPSPmtAcctID = ""; }
                    }

                    conDB.Close();
                }
            }
            catch { }

            subUpdateBillingInfoCC(_lngRecordID, _strPNRef, _strCardNumber, _strXCAlias, strEPSPmtAcctID);
        }

        public static void subUpdateIRAddress(long _lngRecordID, string _strAddress, string _strCity, string _strState, string _strZip)
        {
            string strSQL = "";

            long lngStateID=0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //get state id
                strSQL = "SELECT tlkpStates.lngStateID " +
                        "FROM tlkpStates " +
                        "WHERE tlkpStates.strState=\"" + _strState + "\"";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    lngStateID = Convert.ToInt32(cmdDB.ExecuteScalar());

                    strSQL = "UPDATE tblRecords " +
                            "SET tblRecords.lngStateID = " + lngStateID + ", " +
                                "tblRecords.strAddress = \"" + _strAddress + "\", tblRecords.strCity = \"" + _strCity + "\", tblRecords.strZip = \"" + _strZip + "\" " +
                            "WHERE tblRecords.lngRecordID=" + _lngRecordID;

                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }
        }

        public static long fcnGetStateIDFromAbbr(string _strState)
        {
            string strSQL = "";

            long lngStateID = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //get state id
                strSQL = "SELECT tlkpStates.lngStateID " +
                        "FROM tlkpStates " +
                        "WHERE tlkpStates.strState=\"" + _strState + "\"";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { lngStateID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { lngStateID = 0; }
                }

                conDB.Close();
            }

            return lngStateID;
        }

        public static void subSetToSync(long _lngRecordID)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblRecords " +
                        "SET blnToSync=@blnToSync " +
                        "WHERE lngRecordID=@lngRecordID";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@blnToSync", true);
                    cmdDB.Parameters.AddWithValue("@lngRecordID", _lngRecordID);

                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }
                }

                conDB.Close();
            }
        }
    }
}