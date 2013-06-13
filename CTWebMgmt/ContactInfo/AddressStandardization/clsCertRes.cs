using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CTWebMgmt.ContactInfo.AddressStandardization
{
    class clsCertRes
    {
        public static void subProcessCertRes(string _strListName)
        {
            string strSQL = "";

            bool blnMORNCOAAlert = false;

            //update addresses
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //start with result set
                strSQL = "SELECT tblRecordCert.lngRecordID, " +
                            "tblRecordCert.dteProcessed, " +
                            "tblRecordCert.strStatus, tblRecordCert.strAddress, tblRecordCert.strCity, tblRecordCert.strState, tblRecordCert.strZip, tblRecordCert.strMoveTypeCode " +
                        "FROM tblRecordCert " +
                        "WHERE tblRecordCert.strListName=\"" + _strListName + "\"";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drRes = cmdDB.ExecuteReader())
                    {
                        while (drRes.Read())
                        {
                            string strAddress = Convert.ToString(drRes["strAddress"]);
                            string strCity = Convert.ToString(drRes["strCity"]);
                            string strState = Convert.ToString(drRes["strState"]);
                            string strZip = Convert.ToString(drRes["strZip"]);

                            long lngRecordID = Convert.ToInt32(drRes["lngRecordID"]);
                            long lngPrimaryMORID=0;

                            bool blnUseMORAddress = false;
                            bool blnReconciled = false;

                            //get fields to be used regardless of status
                            //using mor address
                            //tblRecordCert.blnReconciled
                            strSQL = "SELECT tblRecords.blnUseMORAddress, tblRecordCert.blnReconciled, " +
                                        "tblMOR.lngMORID " +
                                    "FROM ((tblRecordCert " +
                                        "INNER JOIN tblRecords ON tblRecordCert.lngRecordID = tblRecords.lngRecordID) " +
                                        "LEFT JOIN tblnkMORIR ON (tblRecords.lngRecordID = tblnkMORIR.lngRecordID) AND " +
                                            "(tblRecords.lngPrimaryMORID = tblnkMORIR.lngMORID)) LEFT JOIN tblMOR ON tblnkMORIR.lngMORID = tblMOR.lngMORID " +
                                    "WHERE tblRecords.lngRecordID=" + Convert.ToInt32(drRes["lngRecordID"]);

                            using (OleDbCommand cmdIR = new OleDbCommand(strSQL, conDB))
                            {
                                using (OleDbDataReader drIR = cmdIR.ExecuteReader())
                                {
                                    if (drIR.Read())
                                    {
                                        blnReconciled = Convert.ToBoolean(drIR["blnReconciled"]);

                                        if (drIR["lngMORID"] != DBNull.Value)
                                        {
                                            blnUseMORAddress = Convert.ToBoolean(drIR["blnUseMORAddress"]);
                                            lngPrimaryMORID=Convert.ToInt32(drIR["lngMORID"]);
                                        }
                                        else
                                        {
                                            blnUseMORAddress = false;
                                            lngPrimaryMORID=0;
                                        }
                                    }
                                    else
                                    {
                                        blnReconciled = false;
                                        blnUseMORAddress = false;
                                    }

                                    drIR.Close();
                                }
                            }

                            if (!blnReconciled)
                            {
                                if (!blnUseMORAddress)
                                {
                                    //evaluate return code from melissa data
                                    switch (Convert.ToString(drRes["strStatus"]))
                                    {
                                        case "Standardized":
                                        case "Moved":
                                            //update ir address
                                            clsIRCRUD.subUpdateIRAddress(lngRecordID, strAddress, strCity, strState, strZip);

                                            break;

                                        case "Error":
                                        default:
                                            //treat unmatched status as error
                                            break;
                                    }

                                    //mark ir as reconciled
                                    subUpdateIRToReconciled(lngRecordID);
                                }
                                else
                                {
                                    //using mor address
                                    string strMORNote = "";

                                    bool blnFlagMOR = false;

                                    //check other mor members for move data
                                    strSQL = "SELECT tblRecordCert.lngRecordID, " +
                                                "Format([tblRecordCert].[dteProcessed],\"m/d/yyyy h:mm ampm\") AS dteProcessed, " +
                                                "tblRecordCert.strStatus, tblRecordCert.strAddress, tblRecordCert.strCity, tblRecordCert.strState, tblRecordCert.strZip, tblRecordCert.strMoveTypeCode, tblRecords_MORMembers.strFirstName AS strFName, tblRecords_MORMembers.strLastCoName AS strLName, tblRecords_MORMembers.blnUseMORAddress " +
                                            "FROM ((tblRecords " +
                                                "INNER JOIN tblnkMORIR ON tblRecords.lngPrimaryMORID = tblnkMORIR.lngMORID) " +
                                                "INNER JOIN tblRecordCert ON tblnkMORIR.lngRecordID = tblRecordCert.lngRecordID) " +
                                                "INNER JOIN tblRecords AS tblRecords_MORMembers ON (tblnkMORIR.lngMORID = tblRecords_MORMembers.lngPrimaryMORID) AND " +
                                                    "(tblnkMORIR.lngRecordID = tblRecords_MORMembers.lngRecordID) " +
                                            "WHERE tblRecords_MORMembers.blnUseMORAddress=True AND " +
                                                "tblRecords.lngRecordID=" + lngRecordID;

                                    using (OleDbCommand cmdMOR = new OleDbCommand(strSQL, conDB))
                                    {
                                        using (OleDbDataReader drMOR = cmdMOR.ExecuteReader())
                                        {
                                            string strCurAddress = "";
                                            string strCurCity = "";
                                            string strCurState = "";
                                            string strCurZip = "";
                                            string strCurStatus = "";
                                            string strCurMoveType = "";

                                            long lngCurRecordID = 0;

                                            bool blnMultAddress = false;

                                            while (drMOR.Read())
                                            {
                                                lngCurRecordID = Convert.ToInt32(drMOR["lngRecordID"]);
                                                strCurAddress = Convert.ToString(drMOR["strAddress"]);
                                                strCurCity = Convert.ToString(drMOR["strCity"]);
                                                strCurState = Convert.ToString(drMOR["strState"]);
                                                strCurZip = Convert.ToString(drMOR["strZip"]);
                                                strCurStatus = Convert.ToString(drMOR["strStatus"]);
                                                strCurMoveType = Convert.ToString(drMOR["strMoveTypeCode"]);

                                                if (strMORNote != "") strMORNote += "\r\n";

                                                strMORNote += "ID " + lngCurRecordID + ": " + strCurAddress + " " + strCurCity + ", " + strCurState + " " + strCurZip + " (" + strCurStatus + " " + strCurMoveType + ")";

                                                if (strCurAddress != "")
                                                {
                                                    if (strCurAddress != strAddress || strCurCity != strCity || strCurState != strState || strCurZip != strZip) blnMultAddress = true;
                                                }
                                            }

                                            drMOR.Close();

                                            if (blnMultAddress)
                                            {
                                                blnFlagMOR = true;
                                                blnMORNCOAAlert = true;
                                            }
                                        }

                                        //update mor flag or mor address
                                        if (blnFlagMOR)
                                        {
                                            int intAlertID = 0;

                                            strSQL = "SELECT lngMORID " +
                                                    "FROM tblMORNCOAAlerts " +
                                                    "WHERE lngMORID=" + lngPrimaryMORID.ToString();

                                            cmdMOR.CommandText = strSQL;
                                            cmdMOR.Parameters.Clear();

                                            try { intAlertID = Convert.ToInt32(cmdMOR.ExecuteScalar()); }
                                            catch { intAlertID = 0; }

                                            if (intAlertID > 0)
                                                strSQL = "UPDATE tblMORNCOAAlerts " +
                                                        "SET blnResolved=0, " +
                                                            "lngMORID=" + lngPrimaryMORID.ToString() + ", " +
                                                            "strListName=\"" + _strListName + "\", " +
                                                            "mmoAlertNotes=\"" + strMORNote + "\" " +
                                                        "WHERE lngMORID=" + lngPrimaryMORID.ToString();
                                            else
                                                strSQL = "INSERT INTO tblMORNCOAAlerts " +
                                                        "( blnResolved, " +
                                                            "lngMORID, " +
                                                            "strListName, " +
                                                            "mmoAlertNotes ) " +
                                                        "SELECT 0, " +
                                                            lngPrimaryMORID + ", \"" + _strListName + "\", \"" + strMORNote + "\"";
                                        }
                                        else
                                            strSQL = "UPDATE tblMOR " +
                                                    "SET tblMOR.lngMORStateID = " + clsIRCRUD.fcnGetStateIDFromAbbr(strState) + ", " +
                                                        "tblMOR.strMORAddress1 = \"" + strAddress + "\", tblMOR.strMORCity = \"" + strCity + "\", tblMOR.strMORZip = \"" + strZip + "\" " +
                                                    "WHERE tblMOR.lngMORID=" + lngPrimaryMORID;

                                        cmdMOR.CommandText = strSQL;

                                        try { cmdMOR.ExecuteNonQuery(); }
                                        catch { }

                                        //mark all members of mor as reconciled
                                        strSQL = "UPDATE ((tblRecords " +
                                                    "INNER JOIN tblnkMORIR ON tblRecords.lngPrimaryMORID = tblnkMORIR.lngMORID) " +
                                                    "INNER JOIN tblRecordCert ON tblnkMORIR.lngRecordID = tblRecordCert.lngRecordID) " +
                                                    "INNER JOIN tblRecords AS tblRecords_MORMembers ON (tblnkMORIR.lngRecordID = tblRecords_MORMembers.lngRecordID) AND " +
                                                        "(tblnkMORIR.lngMORID = tblRecords_MORMembers.lngPrimaryMORID) " +
                                                "SET tblRecordCert.blnReconciled = True " +
                                                "WHERE tblRecords_MORMembers.blnUseMORAddress=True AND " +
                                                    "tblRecords.lngRecordID=" + lngRecordID;

                                        cmdMOR.CommandText = strSQL;

                                        cmdMOR.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        drRes.Close();
                    }
                }

                conDB.Close();
            }

            if (blnMORNCOAAlert) System.Windows.Forms.MessageBox.Show("Some conflicting results were returned for one or more MORs.\nThese will need to be processed manually.");
        }

        private static void subUpdateIRToReconciled(long _lngRecordID)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblRecordCert " +
                        "SET tblRecordCert.blnReconciled = True " +
                        "WHERE tblRecordCert.lngRecordID=" + _lngRecordID;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }
        }
    }
}
