using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CTWebMgmt.Donor
{
    class clsDonorCRUD
    {
        public static int fcnGiftWebCount()
        {
            //get current count of web gifts

            OleDbConnection conDB;
            OleDbCommand cmdDB;

            string strSQL;

            int intRes = 0;

            try
            {
                conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                conDB.Open();

                strSQL = "SELECT Count(lngGiftWebID) AS intGiftCount " +
                        "FROM tblWebGift;";

                cmdDB = new OleDbCommand(strSQL, conDB);

                int.TryParse(cmdDB.ExecuteScalar().ToString(), out intRes);

                conDB.Close();

                cmdDB.Dispose();
                conDB.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsDonorCRUD.fcnGiftWebCount()", ex);
            }

            return intRes;
        }

        public static int fcnDXCount()
        {
            //get current count of dx gifts

            string strSQL;

            int intRes = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT Count(tblDonorExpress.lngDonorExpressID) AS intDXCount " +
                            "FROM tblDonorExpress";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { intRes = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { intRes = 0; }
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsDonorCRUD.fcnGiftWebCount()", ex);
            }

            return intRes;
        }

        public static void subFillGiftCatCbo(ref ComboBox _cboCat)
        {
            //fill the passed combo box with gift categories

            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;
            OleDbDataReader drCat;

            string strSQL;

            conDB.Open();

            strSQL = "SELECT tblGiftCategory.lngGiftCategoryID, " +
                        "[tlkpGiftCategoryChoice].[strGiftCategoryChoice]+'--'+IIf(IsNull([tlkpGiftAllocation].[strGiftAllocation]),\"\",[tlkpGiftAllocation].[strGiftAllocation])+'--'+IIf(IsNull([tlkpGiftSubAllocation].[strGiftSubAllocation]),\"\",[tlkpGiftSubAllocation].[strGiftSubAllocation])+\" (\"+IIf(IsNull([tblGiftCategory].[strOLDesc]),\"\",[tblGiftCategory].[strOLDesc])+\")\" AS strGiftCategory " +
                    "FROM ((tblGiftCategory " +
                        "INNER JOIN tlkpGiftCategoryChoice ON tblGiftCategory.lngGiftCategoryChoiceID = tlkpGiftCategoryChoice.lngGiftCategoryChoiceID) " +
                        "LEFT JOIN tlkpGiftAllocation ON tblGiftCategory.lngGiftAllocation = tlkpGiftAllocation.lngGiftAllocation) " +
                        "LEFT JOIN tlkpGiftSubAllocation ON tblGiftCategory.lngGiftSubAllocation = tlkpGiftSubAllocation.lngGiftSubAllocation " +
                    "WHERE tblGiftCategory.blnUseOnline=True " +
                    "ORDER BY [tlkpGiftCategoryChoice].[strGiftCategoryChoice]+'--'+IIf(IsNull([tlkpGiftAllocation].[strGiftAllocation]),\"\",[tlkpGiftAllocation].[strGiftAllocation])+'--'+IIf(IsNull([tlkpGiftSubAllocation].[strGiftSubAllocation]),\"\",[tlkpGiftSubAllocation].[strGiftSubAllocation])+\" (\"+IIf(IsNull([tblGiftCategory].[strOLDesc]),\"\",[tblGiftCategory].[strOLDesc])+\")\";";

            cmdDB = new OleDbCommand(strSQL, conDB);

            drCat = cmdDB.ExecuteReader();

            _cboCat.Items.Clear();

            while (drCat.Read())
            {
                clsCboItem cboNew = new clsCboItem(long.Parse(drCat["lngGiftCategoryID"].ToString()), drCat["strGiftCategory"].ToString());

                _cboCat.Items.Add(cboNew);
            }

            drCat.Close();

            conDB.Close();

            drCat.Dispose();
            cmdDB.Dispose();
            conDB.Dispose();

        }

        public static void subFillCampaignCbo(ref ComboBox _cboCampaign)
        {
            //fill the passed combo box with campaign codes

            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;
            OleDbDataReader drCampaign;

            string strSQL;

            conDB.Open();

            strSQL = "SELECT tlkpCampaignCodes.lngCampaignID, " +
                        "tlkpCampaignCodes.strCampaignCode " +
                    "FROM tlkpCampaignCodes " +
                    "ORDER BY tlkpCampaignCodes.strCampaignCode;";

            cmdDB = new OleDbCommand(strSQL, conDB);

            drCampaign = cmdDB.ExecuteReader();

            _cboCampaign.Items.Clear();

            while (drCampaign.Read())
            {
                clsCboItem cboNew = new clsCboItem(long.Parse(drCampaign["lngCampaignID"].ToString()), drCampaign["strCampaignCode"].ToString());

                _cboCampaign.Items.Add(cboNew);
            }

            drCampaign.Close();

            conDB.Close();

            drCampaign.Dispose();
            cmdDB.Dispose();
            conDB.Dispose();

        }

        public static long fcnGetDefaultCampaignID()
        {
            string strSQL;

            long lngRes = 0;

            try
            {
                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    using (OleDbCommand objCommand = new OleDbCommand())
                    {
                        objConn.Open();

                        objCommand.Connection = objConn;

                        strSQL = "SELECT lngOLGiftCampaign " +
                                     "FROM tblCampDefaults;";

                        objCommand.CommandText = strSQL;

                        long.TryParse(objCommand.ExecuteScalar().ToString(), out lngRes);

                        objConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsDonorCRUD.fcnGetDefaultCampaignID", ex);
            }

            return lngRes;
        }

        public static long fcnAddGift(bool _blnMemorial, bool _blnInHonorOf, long _lngGiftCategory, long _lngRecordID, long _lngCampaignID, long _lngPaymentTypeID, long _lngBillStateID, long _lngPledgeID, long _lngGiftWebID, decimal _curAmount, DateTime _dteGiftDate, string _strMemorialName, string _strInHonorOf, string _strAcctNum, string _strBankName, string _strBillAddress, string _strBillCity, string _strBillName, string _strBillPhone, string _strBillZip, string _strCCExpDate, string _strCCNumber, string _strCCValCode, string _strRoutingNum)
        {
            long lngRes = 0;

            lngRes = fcnAddGift(_blnMemorial, _blnInHonorOf, _lngGiftCategory, _lngRecordID, _lngCampaignID, _lngPaymentTypeID, _lngBillStateID, _lngPledgeID, _lngGiftWebID, _curAmount, _dteGiftDate, _strMemorialName, _strInHonorOf, _strAcctNum, _strBankName, _strBillAddress, _strBillCity, _strBillName, _strBillPhone, _strBillZip, _strCCExpDate, _strCCNumber, _strCCValCode, _strRoutingNum, "", "", "", "");

            return lngRes;
        }

        public static long fcnAddGift(bool _blnMemorial, bool _blnInHonorOf, long _lngGiftCategory, long _lngRecordID, long _lngCampaignID, long _lngPaymentTypeID, long _lngBillStateID, long _lngPledgeID, long _lngGiftWebID, decimal _curAmount, DateTime _dteGiftDate, string _strMemorialName, string _strInHonorOf, string _strAcctNum, string _strBankName, string _strBillAddress, string _strBillCity, string _strBillName, string _strBillPhone, string _strBillZip, string _strCCExpDate, string _strCCNumber, string _strCCValCode, string _strRoutingNum, string _strAuthNum, string _strPNRef, string _strXCAlias, string _strXCTransID)
        {
            long lngRes = 0;

            lngRes = fcnAddGift(_blnMemorial, _blnInHonorOf, _lngGiftCategory, _lngRecordID, _lngCampaignID, _lngPaymentTypeID, _lngBillStateID, _lngPledgeID, _lngGiftWebID, _curAmount, _dteGiftDate, _strMemorialName, _strInHonorOf, _strAcctNum, _strBankName, _strBillAddress, _strBillCity, _strBillName, _strBillPhone, _strBillZip, _strCCExpDate, _strCCNumber, _strCCValCode, _strRoutingNum, _strAuthNum, _strPNRef, _strXCAlias, _strXCTransID, "", "", "", "");

            return lngRes;
        }

        public static long fcnAddGift(bool _blnMemorial, bool _blnInHonorOf, long _lngGiftCategory, long _lngRecordID, long _lngCampaignID, long _lngPaymentTypeID, long _lngBillStateID, long _lngPledgeID, long _lngGiftWebID, decimal _curAmount, DateTime _dteGiftDate, string _strMemorialName, string _strInHonorOf, string _strAcctNum, string _strBankName, string _strBillAddress, string _strBillCity, string _strBillName, string _strBillPhone, string _strBillZip, string _strCCExpDate, string _strCCNumber, string _strCCValCode, string _strRoutingNum, string _strAuthNum, string _strPNRef, string _strXCAlias, string _strXCTransID, string _strEPSTransID, string _strEPSApprovalNumber, string _strEPSValidationCode, string _strEPSPmtAcctID)
        {
            long lngGiftID = 0;

            string strSQL;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "INSERT INTO tblGift " +
                        "( blnMarkedForCC, blnMemorial, blnInHonorOf, " +
                            "lngGiftCategoryID, lngRecordID, lngCampaignID, lngGiftTypeID, lngPaymentTypeID, lngBillStateID, lngPledgeID, lngGiftWebID, " +
                            "curAmount, " +
                            "dteGiftDate, dteDateEntered, " +
                            "strMemorialName, strInHonorOf, strAcctNum, strBankName, strBillAddress, strBillCity, strBillName, strBillPhone, strBillZip, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum, strCCAuthorizationCode, strPNRef, strXCAlias, strXCTransID, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID ) " +
                        "VALUES " +
                        "(@blnMarkedForCC, @blnMemorial, @blnInHonorOf, " +
                            "@lngGiftCategoryID, @lngRecordID, @lngCampaignID, @lngGiftTypeID, @lngPaymentTypeID, @lngBillStateID, @lngPledgeID, @lngGiftWebID, " +
                            "@curAmount, " +
                            "@dteGiftDate, Now(), " +
                            "@strMemorialName, @strInHonorOf, @strAcctNum, @strBankName, @strBillAddress, @strBillCity, @strBillName, @strBillPhone, @strBillZip, @strCCExpDate, @strCCNumber, @strCCValCode, @strRoutingNum, @strCCAuthorizationCode, @strPNRef, @strXCAlias, @strXCTransID, @strEPSTransID, @strEPSApprovalNumber, @strEPSValidationCode, @strEPSPmtAcctID )";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    //create parameters
                    if (_strAuthNum == "")
                        cmdDB.Parameters.Add(new OleDbParameter("@blnMarkedForCC", false));
                    else
                        cmdDB.Parameters.Add(new OleDbParameter("@blnMarkedForCC", true));

                    cmdDB.Parameters.Add(new OleDbParameter("@blnMemorial", _blnMemorial));
                    cmdDB.Parameters.Add(new OleDbParameter("@blnInHonorOf", _blnInHonorOf));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngGiftCategoryID", _lngGiftCategory));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", _lngRecordID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngCampaignID", _lngCampaignID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngGiftTypeID", 1));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", _lngPaymentTypeID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", _lngBillStateID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngPledgeID", _lngPledgeID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngGiftWebID", _lngGiftWebID));
                    cmdDB.Parameters.Add(new OleDbParameter("@curAmount", _curAmount));
                    cmdDB.Parameters.Add(new OleDbParameter("@dteGiftDate", _dteGiftDate));
                    cmdDB.Parameters.Add(new OleDbParameter("@strMemorialName", _strMemorialName));
                    cmdDB.Parameters.Add(new OleDbParameter("@strInHonorOf", _strInHonorOf));
                    cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", _strAcctNum));
                    cmdDB.Parameters.Add(new OleDbParameter("@strBankName", _strBankName));
                    cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", _strBillAddress));
                    cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", _strBillCity));
                    cmdDB.Parameters.Add(new OleDbParameter("@strBillName", _strBillName));
                    cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", _strBillPhone));
                    cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", _strBillZip));
                    cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", _strCCExpDate));

                    if (_strAuthNum != "" && _strCCNumber.Length > 12)
                        cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", _strCCNumber.Substring(12, _strCCNumber.Length - 12)));
                    else
                        cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", _strCCNumber));

                    cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", _strCCValCode));
                    cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", _strRoutingNum));
                    cmdDB.Parameters.Add(new OleDbParameter("@strCCAuthorizationCode", _strAuthNum));
                    cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", _strPNRef));
                    cmdDB.Parameters.Add(new OleDbParameter("@strXCAlias", _strXCAlias));
                    cmdDB.Parameters.Add(new OleDbParameter("@strXCTransID", _strXCTransID));
                    
                    cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", _strEPSTransID));
                    cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", _strEPSApprovalNumber));
                    cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", _strEPSValidationCode));
                    cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", _strEPSPmtAcctID));

                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }

                    //get gift id
                    strSQL = "SELECT @@IDENTITY";

                    cmdDB.CommandText = strSQL;

                    try { long.TryParse(cmdDB.ExecuteScalar().ToString(), out lngGiftID); }
                    catch { }
                }

                conDB.Close();
            }

            return lngGiftID;
        }

        public static long fcnAddPledge(bool _blnPledgeReminder, bool _blnPledgeAutopay, bool _blnMemorial, bool _blnInHonorOf, long _lngRecordID, long _lngCampaignID, long _lngCategoryID, long _lngFrequencyID, long _lngTerm, long _lngPaymentTypeID, long _lngBillStateID, decimal _curPeriodAmt, string _strMemorialName, string _strInHonorOf, string _strAcctNum, string _strBankName, string _strBillAddress, string _strBillCity, string _strBillName, string _strBillPhone, string _strBillZip, string _strCCExpDate, string _strCCNumber, string _strCCValCode, string _strRoutingNum, string _strPNRef, string _strXCAlias, string _strEPSPmtAcctID)
        {
            string strSQL;

            long lngPledgeID = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "INSERT INTO tblPledge " +
                        "(blnMoreAllowed, blnPledgeReminder, blnPledgeAutopay, blnMemorial, blnInHonorOf, " +
                            "lngRecordID, lngCampaignID, lngCategoryID, lngFrequencyID, lngTerm, lngRepID, lngPaymentTypeID, lngBillStateID, " +
                            "dtePledgeDate, " +
                            "curPeriodAmt, " +
                            "strMemorialName, strInHonorOf, strAcctNum, strBankName, strBillAddress, strBillCity, strBillName, strBillPhone, strBillZip, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum, strPNRef, strXCAlias, strEPSPmtAcctID, " +
                            "mmoPledgeNotes) " +
                        "VALUES " +
                        "(0, " + _blnPledgeReminder + ", " + _blnPledgeAutopay + ", " + _blnMemorial + ", " + _blnInHonorOf + ", " +
                            _lngRecordID + ", " + _lngCampaignID + ", " + _lngCategoryID + ", " + _lngFrequencyID + ", " + _lngTerm + ", 0, " + _lngPaymentTypeID + ", " + _lngBillStateID + ", " +
                            "Now(), " +
                            _curPeriodAmt + ", " +
                            "\"" + _strMemorialName + "\", \"" + _strInHonorOf + "\", \"" + _strAcctNum + "\", \"" + _strBankName + "\", \"" + _strBillAddress + "\", \"" + _strBillCity + "\", \"" + _strBillName + "\", \"" + _strBillPhone + "\", \"" + _strBillZip + "\", \"" + _strCCExpDate + "\", \"" + _strCCNumber + "\", \"" + _strCCValCode + "\", \"" + _strRoutingNum + "\", \"" + _strPNRef + "\", \"" + _strXCAlias + "\", \"" + _strEPSPmtAcctID + "\", " +
                            "\"Online Pledge\")";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.ExecuteNonQuery();

                    strSQL = "SELECT @@IDENTITY";

                    cmdDB.CommandText = strSQL;

                    try { long.TryParse(cmdDB.ExecuteScalar().ToString(), out lngPledgeID); }
                    catch { }

                    //schedule pmts
                    for (int intI = 0; intI < int.Parse(_lngTerm.ToString()); intI++)
                    {
                        strSQL = "INSERT INTO tblPledgePayments " +
                                "(lngPledgeID, lngTransTypeID, " +
                                    "curScheduledAmt, " +
                                    "dteScheduledDate) " +
                                "VALUES " +
                                "(" + lngPledgeID + ", 67, " +
                                    _curPeriodAmt + ", " +
                                    "#" + fcnCalcDate((int)_lngFrequencyID, intI) + "#)";

                        cmdDB.CommandText = strSQL;

                        cmdDB.ExecuteNonQuery();
                    }
                }
            }

            return lngPledgeID;
        }

        public static long fcnAddPledgePmt(long _lngPledgeID, long _lngGiftID, decimal _curAmount, DateTime _dtePaidDate)
        {
            string strSQL;

            long lngPledgePmtID = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //get next scheduled pledge pmt id, ordered by scheduled recvd dte.                                
                strSQL = "SELECT lngPledgePaymentID " +
                           "FROM tblPledgePayments " +
                           "WHERE lngPledgeID = " + _lngPledgeID + " AND " +
                                   "curPaidAmt=0 " +
                           "ORDER BY dteScheduledDate;";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.CommandText = strSQL;

                    try { long.TryParse(cmdDB.ExecuteScalar().ToString(), out lngPledgePmtID); }
                    catch { }

                    strSQL = "UPDATE tblPledgePayments " +
                            "SET lngGiftID = " + _lngGiftID + ", " +
                                "curPaidAmt = " + _curAmount + ", " +
                                "dtePaidDate = #" + _dtePaidDate + "# " +
                            "WHERE lngPledgePaymentID=" + lngPledgePmtID;

                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }

            return lngPledgePmtID;
        }

        private static DateTime fcnCalcDate(int _intFreq, int _intIndex)
        {
            DateTime dteRes = DateTime.Now;

            switch (_intFreq)
            {
                case 1:
                    dteRes = DateTime.Now.AddDays(7 * _intIndex);
                    break;

                case 2:
                    dteRes = DateTime.Now.AddMonths(_intIndex);
                    break;

                case 3:
                    dteRes = DateTime.Now.AddMonths(3 * _intIndex);
                    break;

                case 4:
                    dteRes = DateTime.Now.AddMonths(6 * _intIndex);
                    break;

                case 5:
                    dteRes = DateTime.Now.AddYears(_intIndex);
                    break;

                case 6:
                    dteRes = DateTime.Now.AddMonths(2 * _intIndex);
                    break;
            }

            return dteRes;
        }
    }
}