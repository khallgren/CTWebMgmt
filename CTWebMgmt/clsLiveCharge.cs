using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data;

namespace CTWebMgmt
{
    class clsLiveCharge
    {
        //public static string strVancoURI = "https://www.vancodev.com/cgi-bin/wstest2.vps?xml=";
        public static string strVancoURI = "https://www.vancoservices.com/cgi-bin/ws2.vps?xml=";

        //public static string strXChargeURI = "https://test.t3secure.net/x-chargeweb.dll";
        public static string strXChargeURI = "https://gw.t3secure.net/x-chargeweb.dll";
        public static string strXChargeRptURI = "https://gw.t3secure.net:29716/x-chargeweb.dll";

        public static bool fcnSendGiftTrans(int _intHandle, long _lngGiftID, long _lngRecordID, System.Windows.Forms.ListBox _lstStatus)
        {
            string strSQL;
            string strCCNum = "";
            string strExpDate = "";
            string strCVV2 = "";
            string strBillName = "";
            string strZip = "";
            string strAuth = "";
            string strPNRef = "";
            string strXCAlias = "";
            string strEPSPmtAcctID = "";
            string strBillAddress = "";
            string strBillZip = "";
            string strAcctNum = "";
            string strRoutingNum = "";

            decimal decAmt = 0;

            long lngPledgeID = 0;
            long lngPaymentTypeID = 0;

            //get live charge setting from db
            clsGlobalEnum.conLIVECHARGE intLiveChargeMethod = fcnLiveChargeMethod();

            bool blnChargeRes = false;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //gather gift, payment details (cc number, amount, etc)
                    strSQL = "SELECT tblGift.lngPledgeID, tblGift.lngPaymentTypeID, " +
                                "tblGift.curAmount, " +
                                "tblBillingInfo.strCCNumber, tblBillingInfo.strCCExpDate, tblGift.strCCAuthorizationCode, tblGift.strCCValCode, tblBillingInfo.strBillZip, tblBillingInfo.strBillName, tblBillingInfo.strXCAlias, tblBillingInfo.strPNRef, tblBillingInfo.strEPSPmtAcctID, tblGift.strBillAddress, tblGift.strBillZip, tblGift.strAcctNum, tblGift.strRoutingNum " +
                            "FROM tblGift " +
                                "INNER JOIN tblBillingInfo ON tblGift.lngRecordID = tblBillingInfo.lngRecordID " +
                            "WHERE tblGift.lngGiftID=" + _lngGiftID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drGift = cmdDB.ExecuteReader())
                        {
                            if (drGift.Read())
                            {
                                strCCNum = drGift["strCCNumber"].ToString();
                                strExpDate = drGift["strCCExpDate"].ToString();

                                try { strZip = drGift["strBillZip"].ToString(); }
                                catch { strZip = ""; }

                                strCVV2 = drGift["strCCValCode"].ToString();
                                decAmt = Convert.ToDecimal(drGift["curAmount"]);
                                strBillName = Convert.ToString(drGift["strBillName"]);

                                //pnref and xcalias are pulled from billing info (not gift!)
                                strPNRef = Convert.ToString(drGift["strPNRef"]);
                                strXCAlias = Convert.ToString(drGift["strXCAlias"]);
                                strEPSPmtAcctID = Convert.ToString(drGift["strEPSPmtAcctID"]);

                                try { lngPledgeID = Convert.ToInt32(drGift["lngPledgeID"]); }
                                catch { lngPledgeID = 0; }

                                try { lngPaymentTypeID = Convert.ToInt32(drGift["lngPaymentTypeID"]); }
                                catch { lngPaymentTypeID = 0; }

                                try { strBillAddress = Convert.ToString(drGift["strBillAddress"]); }
                                catch { strBillAddress = ""; }

                                try { strBillZip = Convert.ToString(drGift["strBillZip"]); }
                                catch { strBillZip = ""; }

                                try { strAcctNum = Convert.ToString(drGift["strAcctNum"]); }
                                catch { strAcctNum = ""; }

                                try { strRoutingNum = Convert.ToString(drGift["strRoutingNum"]); }
                                catch { strRoutingNum = ""; }
                            }

                            drGift.Close();
                        }

                        switch (intLiveChargeMethod)
                        {
                            case clsGlobalEnum.conLIVECHARGE.None:
                                blnChargeRes = true;
                                break;

                            case clsGlobalEnum.conLIVECHARGE.XCharge:

                                if (strXCAlias != "")
                                {
                                    string strXCTransIDRes = "";
                                    decimal decApprovedAmt = 0;

                                    blnChargeRes = fcnLiveXCharge(_intHandle, ref strCCNum, decAmt, _lngGiftID, _lngRecordID, "Sale", ref strXCAlias, ref strXCTransIDRes, ref decApprovedAmt);

                                    if (blnChargeRes)
                                    {
                                        if (decApprovedAmt < decAmt)
                                        {
                                            System.Windows.Forms.MessageBox.Show("WARNING:  PARTIAL APPROVAL ONLY; PLEDGE MUST BE MODIFIED MANUALLY, XCAlias: " + strXCAlias + ", GIFT ID " + _lngGiftID.ToString());
                                            _lstStatus.Items.Insert(0, "WARNING:  PARTIAL APPROVAL; PLEDGE MUST BE MODIFIED MANUALLY, XCAlias: " + strXCAlias + ", GIFT ID: " + _lngGiftID.ToString());
                                        }
                                        else
                                            _lstStatus.Items.Insert(0, "Successfully processed payment, XCAlias: " + strXCAlias);

                                        System.Windows.Forms.Application.DoEvents();

                                        if (lngPledgeID > 0)
                                        {
                                            //update pledge info
                                            strSQL = "UPDATE tblPledge " +
                                                    "SET lngPaymentTypeID = 2, " +
                                                        "strXCAlias = \"" + strXCAlias + "\", strCCNumber = \"" + strCCNum + "\" " +
                                                    "WHERE lngPledgeID=" + lngPledgeID.ToString();

                                            cmdDB.Parameters.Clear();
                                            cmdDB.CommandText = strSQL;
                                            cmdDB.ExecuteNonQuery();
                                        }

                                        //update gift info
                                        strSQL = "UPDATE tblGift " +
                                                "SET blnMarkedForCC=-1, " +
                                                    "lngPaymentTypeID = 2, " +
                                                    "strXCAlias = \"" + strXCAlias + "\", strCCAuthorizationCode = \"" + strXCAlias + "\", strCCNumber = \"" + strCCNum + "\", strXCTransID=\"" + strXCTransIDRes + "\" " +
                                                "WHERE tblGift.lngGiftID=" + _lngGiftID.ToString();

                                        cmdDB.Parameters.Clear();
                                        cmdDB.CommandText = strSQL;
                                        cmdDB.ExecuteNonQuery();

                                        clsIRCRUD.subUpdateBillingInfoCC(_lngRecordID, "", strCCNum, strXCAlias);
                                    }
                                }
                                else
                                {
                                    if (lngPaymentTypeID == 11)
                                    {
                                        //////////////////////////////
                                        string strXCTransIDRes = "";
                                        decimal decApprovedAmt = 0;

                                        blnChargeRes = fcnLiveXChargeEFT(_intHandle, strAcctNum, strRoutingNum, decAmt, _lngGiftID, _lngRecordID, "Sale", strBillName, strBillAddress, strBillZip, ref strXCTransIDRes, ref decApprovedAmt);

                                        if (blnChargeRes)
                                        {
                                            _lstStatus.Items.Insert(0, "Successfully processed payment, XC Trans ID: " + strXCTransIDRes);

                                            System.Windows.Forms.Application.DoEvents();

                                            //update gift info
                                            strSQL = "UPDATE tblGift " +
                                                    "SET blnMarkedForCC=-1, " +
                                                        "lngPaymentTypeID = 11, " +
                                                        "strXCTransID=\"" + strXCTransIDRes + "\" " +
                                                    "WHERE tblGift.lngGiftID=" + _lngGiftID.ToString();

                                            cmdDB.Parameters.Clear();
                                            cmdDB.CommandText = strSQL;
                                            cmdDB.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        _lstStatus.Items.Insert(0, "X-Charge alias is required to auto-process credit cards for pledge payments.");
                                        blnChargeRes = false;
                                    }
                                }

                                break;

                            case clsGlobalEnum.conLIVECHARGE.CashLinq:
                                //TODO: CL CustomQ gift processing
                                string strPNRefRes = "";
                                string strAuthNumber = "";

                                blnChargeRes = fcnCashLinqRepeatSale(_lngGiftID, _lngRecordID, decAmt, strPNRef, ref strPNRefRes, ref strAuthNumber);

                                if (blnChargeRes)
                                {
                                    _lstStatus.Items.Insert(0, "Successfully processed payment, PNRef: " + strPNRefRes);
                                    System.Windows.Forms.Application.DoEvents();

                                    strAuthNumber = strPNRefRes;
                                    strAuth = strAuthNumber;

                                    if (lngPledgeID > 0)
                                    {
                                        //update pledge info
                                        strSQL = "UPDATE tblPledge " +
                                                "SET lngPaymentTypeID = " + (int)clsGlobalEnum.conPMTTYPE.CC + ", " +
                                                    "strPNRef = \"" + strPNRefRes + "\", strCCNumber = \"" + strCCNum + "\", strBillName = \"" + strBillName + "\", strBillZip = \"" + strZip + "\" " +
                                                "WHERE lngPledgeID=" + lngPledgeID.ToString();

                                        cmdDB.Parameters.Clear();
                                        cmdDB.CommandText = strSQL;
                                        cmdDB.ExecuteNonQuery();
                                    }

                                    //update gift info
                                    strSQL = "UPDATE tblGift " +
                                            "SET tblGift.blnMarkedForCC=-1, " +
                                                "tblGift.lngPaymentTypeID = " + (int)clsGlobalEnum.conPMTTYPE.CC + ", " +
                                                "tblGift.strPNRef = \"" + strPNRefRes + "\", tblGift.strCCAuthorizationCode = \"" + strAuthNumber + "\", tblGift.strCCNumber = \"" + strCCNum + "\", tblGift.strCCExpDate = \"" + strExpDate + "\", tblGift.strCCValCode = \"" + strCVV2 + "\", tblGift.strBillName = \"" + strBillName + "\", tblGift.strBillZip = \"" + strZip + "\" " +
                                            "WHERE tblGift.lngGiftID=" + _lngGiftID.ToString();

                                    cmdDB.Parameters.Clear();
                                    cmdDB.CommandText = strSQL;
                                    cmdDB.ExecuteNonQuery();

                                    //update billing info for ir
                                    clsIRCRUD.subUpdateBillingInfoCC(_lngRecordID, strPNRefRes, strCCNum);
                                }
                                break;

                            case clsGlobalEnum.conLIVECHARGE.EPS:

                                strAuthNumber = "";

                                string strEPSTransIDResult = "";
                                string strEPSApprovalNumber = "";
                                string strEPSValidationCode = "";

                                blnChargeRes = fcnEPSRepeatSale(_lngGiftID, _lngRecordID, decAmt, strEPSPmtAcctID, ref strEPSTransIDResult, ref strAuthNumber, ref strEPSApprovalNumber, ref strEPSValidationCode);

                                if (blnChargeRes)
                                {
                                    _lstStatus.Items.Insert(0, "Successfully processed payment, EPS Trans ID: " + strEPSTransIDResult);
                                    System.Windows.Forms.Application.DoEvents();

                                    strAuthNumber = strEPSTransIDResult;
                                    strAuth = strAuthNumber;

                                    if (lngPledgeID > 0)
                                    {
                                        //update pledge info
                                        strSQL = "UPDATE tblPledge " +
                                                "SET lngPaymentTypeID = " + (int)clsGlobalEnum.conPMTTYPE.CC + ", " +
                                                    "strEPSPmtAcctID = \"" + strEPSPmtAcctID + "\", strCCNumber = \"" + strCCNum + "\", strBillName = \"" + strBillName + "\", strBillZip = \"" + strZip + "\" " +
                                                "WHERE lngPledgeID=" + lngPledgeID.ToString();

                                        cmdDB.Parameters.Clear();
                                        cmdDB.CommandText = strSQL;
                                        cmdDB.ExecuteNonQuery();
                                    }

                                    //update gift info
                                    strSQL = "UPDATE tblGift " +
                                            "SET tblGift.blnMarkedForCC=1, " +
                                                "tblGift.lngPaymentTypeID = " + (int)clsGlobalEnum.conPMTTYPE.CC + ", " +
                                                "tblGift.strEPSPmtAcctID = \"" + strEPSPmtAcctID + "\", tblGift.strCCAuthorizationCode = \"" + strAuthNumber + "\", tblGift.strCCNumber = \"" + strCCNum + "\", tblGift.strCCExpDate = \"" + strExpDate + "\", tblGift.strCCValCode = \"" + strCVV2 + "\", tblGift.strBillName = \"" + strBillName + "\", tblGift.strBillZip = \"" + strZip + "\", strEPSTransID = \"" + strEPSTransIDResult + "\", strEPSApprovalNumber = \"" + strEPSApprovalNumber + "\", strEPSValidationCode = \"" + strEPSValidationCode + "\" " +
                                            "WHERE tblGift.lngGiftID=" + _lngGiftID.ToString();

                                    cmdDB.Parameters.Clear();
                                    cmdDB.CommandText = strSQL;
                                    cmdDB.ExecuteNonQuery();

                                    //update billing info for ir                                  
                                    clsIRCRUD.subUpdateBillingInfoCC(_lngRecordID, "", strCCNum, "", strEPSPmtAcctID);
                                }

                                break;

                            case clsGlobalEnum.conLIVECHARGE.XChargeXML:

                                System.Windows.Forms.MessageBox.Show("The X-Charge XML method of processing credit cards is no longer supported due to PCI compliance regulations.\n\nPlease contact CampTrak support at (248) 937-8605 for further assistance.");
                                blnChargeRes = false;
                                break;
                        }
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error processing gift: " + ex.Message);
            }

            return blnChargeRes;
        }

        public static bool fcnLiveXCharge(long _lngHandle, ref string _strCardNum, decimal _decAmt, long _lngSaleID, long _lngRecordID, string _strSaleType, ref string _strXCAlias, ref string _strXCTransID, ref decimal _curApprovedAmt)
        {
            XCTransaction2.XChargeTransaction objXC = new XCTransaction2.XChargeTransaction();

            string strErr = "";
            string strRes = "";
            string strCVVRes = "";

            string strXChargePath = "";
            string strRawSwipe = "";
            string strTrack1 = "";
            string strTrack2 = "";
            string strCCType = "";
            string strExpMonth = "";
            string strExpYear = "";
            string strName = "";

            string strAppAmtRes = "";
            string strBalAmtRes = "";

            bool blnRes = false;

            bool blnProcessLocal = true;

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT strXChargePath " +
                        "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { strXChargePath = Convert.ToString(cmdDB.ExecuteScalar()); }
                    catch { strXChargePath = ""; }
                }

                conDB.Close();
            }

            if (_strXCAlias == "")
            {

                string strZip = "";
                string strAddress = "";
                string strCVV2 = "";

                //prompt for card entry
                //If objXC.PromptCreditCardEntry(lngHandle, "Enter Transaction Details", False, False, False, strRawSwipe, strTrack1, strTrack2, str_CardNum, strCCType, strExpMonth, strExpYear, strName, str_Zip, str_Address, str_CVV) Then
                if (objXC.PromptCreditCardEntry((int)_lngHandle, "Enter Transaction Details", false, false, false, out strRawSwipe, out strTrack1, out strTrack2, out _strCardNum, out strCCType, out strExpMonth, out strExpYear, out strName, out strZip, out strAddress, out strCVV2))
                {
                    //add to vault, get alias
                    if (objXC.XCArchiveVaultAdd((int)_lngHandle, strXChargePath, "CampTrak Transaction", true, false, strTrack1, "", "", "USEEXISTING", out _strXCAlias, out strErr))
                        blnRes = true;
                    else
                        blnRes = false;
                }
                else
                    blnRes = false;
            }
            else
            {
                //alias exists
                //if alias came from web then repeat trans must be processed through web service...try this first
                if (fcnLiveXChargeWeb(_decAmt, _strXCAlias, ref _strXCTransID, _lngSaleID.ToString(), ref _curApprovedAmt))
                {
                    blnProcessLocal = false;
                    blnRes = true;
                    strAppAmtRes = _curApprovedAmt.ToString();
                }
                else
                    blnProcessLocal = true;
            }

            if (blnProcessLocal && _strXCAlias != "")
            {
                string strXCAuthCode = "";

                //the allow dup flag is throwing an error on first transaction...first try not allowing.
                //if dupe error is returned then try again with flag switched
                if (objXC.XCPurchaseEx2((int)_lngHandle, strXChargePath, "CampTrak Transactions", true, false, clsNav.objLogin.cboUsers.Text, _lngSaleID.ToString(), _strXCAlias, "", strRawSwipe, _decAmt.ToString(), "", "", "", "", "", false, false, true, out strErr, out strXCAuthCode, out strRes, out strCVVRes, out _strXCTransID, out strAppAmtRes, out strBalAmtRes))
                    blnRes = true;
                else
                    if (strErr == "094 AP DUPE")
                    {
                        if (objXC.XCPurchaseEx2((int)_lngHandle, strXChargePath, "CampTrak Transactions", true, false, clsNav.objLogin.cboUsers.Text, _lngSaleID.ToString(), _strXCAlias, "", strRawSwipe, _decAmt.ToString(), "", "", "", "", "", false, true, true, out strErr, out strXCAuthCode, out strRes, out strCVVRes, out _strXCTransID, out strAppAmtRes, out strBalAmtRes))
                            blnRes = true;
                        else
                            blnRes = false;
                    }
            }

            if (blnRes)
            {
                try
                {
                    _curApprovedAmt = Convert.ToDecimal(strAppAmtRes);

                    if (_curApprovedAmt < _decAmt)
                        System.Windows.Forms.MessageBox.Show("Partial Approval\n\nRequested Amount: " + _decAmt + "\nApproved Amount: " + strAppAmtRes + "\nAmount Remaining: " + (_decAmt - _curApprovedAmt).ToString());
                }
                catch { }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error processing card: " + strErr);
            }

            return blnRes;
        }

        public static bool fcnLiveXChargeEFT(long _lngHandle, string _strAcctNum, string _strRoutingNum, decimal _decAmt, long _lngSaleID, long _lngRecordID, string _strSaleType, string _strBillName, string _strBillAddress, string _strBillZip, ref string _strXCTransID, ref decimal _curApprovedAmt)
        {
            XCTransaction2.XChargeTransaction objXC = new XCTransaction2.XChargeTransaction();

            string strErr = "";
            string strApp = "";
            string strXChargePath = "";

            bool blnRes = false;

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT strXChargePath " +
                        "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { strXChargePath = Convert.ToString(cmdDB.ExecuteScalar()); }
                    catch { strXChargePath = ""; }
                }

                conDB.Close();
            }

            if (_strSaleType == "Refund") _decAmt = 0 - _decAmt;

            string strXCAuthCode = "";

            blnRes = objXC.XCACHDebit((int)_lngHandle, strXChargePath, "CampTrak POS Transaction", true, true, _decAmt.ToString(), _strRoutingNum, _strAcctNum, "Checking", "Personal", _strBillName, _strBillAddress, _strBillZip, clsNav.objLogin.cboUsers.Text, _lngSaleID.ToString(), out strXCAuthCode, out strErr, out _strXCTransID);

            if (blnRes) _curApprovedAmt = _decAmt;

            try { objXC = null; }
            catch { }

            return blnRes;
        }

        private static void subAppendXMLElement(string _strElementName, string _strElementValue, ref XmlDocument _xmlDoc, ref XmlElement _xmlParent)
        {
            XmlElement xmlNewElement = _xmlDoc.CreateElement(_strElementName);

            _xmlParent.AppendChild(xmlNewElement);

            xmlNewElement.InnerText = _strElementValue;
        }

        public static string fcnGetXMLElement(string _strElementName, XmlDocument _xmlDocument)
        {
            XmlNode nodElement;

            string strRes = "";

            nodElement = _xmlDocument.SelectSingleNode(_strElementName);

            if (nodElement != null)
                strRes = nodElement.InnerText;

            return strRes;
        }

        public static clsGlobalEnum.conLIVECHARGE fcnGetLiveChargeMethod()
        {
            clsGlobalEnum.conLIVECHARGE intRes = clsGlobalEnum.conLIVECHARGE.None;

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngLiveCharge " +
                        "FROM tblCampDefaults;";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    intRes = (clsGlobalEnum.conLIVECHARGE)cmdDB.ExecuteScalar();
                }

                conDB.Close();
            }

            return intRes;
        }

        public static string fcnGetAlphaVal(int _intIn)
        {
            string strRes = "";

            switch (_intIn)
            {
                case 1:
                    strRes = "a";
                    break;
                case 2:
                    strRes = "b";
                    break;
                case 3:
                    strRes = "c";
                    break;
                case 4:
                    strRes = "d";
                    break;
                case 5:
                    strRes = "e";
                    break;
                case 6:
                    strRes = "f";
                    break;
                case 7:
                    strRes = "g";
                    break;
                case 8:
                    strRes = "h";
                    break;
                case 9:
                    strRes = "i";
                    break;
                case 10:
                    strRes = "j";
                    break;
                case 11:
                    strRes = "k";
                    break;
                case 12:
                    strRes = "l";
                    break;
                case 13:
                    strRes = "m";
                    break;
                case 14:
                    strRes = "n";
                    break;
                case 15:
                    strRes = "o";
                    break;
                case 16:
                    strRes = "p";
                    break;
                case 17:
                    strRes = "q";
                    break;
                case 18:
                    strRes = "r";
                    break;
                case 19:
                    strRes = "s";
                    break;
                case 20:
                    strRes = "t";
                    break;
                case 21:
                    strRes = "u";
                    break;
                case 22:
                    strRes = "v";
                    break;
                case 23:
                    strRes = "w";
                    break;
                case 24:
                    strRes = "x";
                    break;
                case 25:
                    strRes = "y";
                    break;
                case 26:
                    strRes = "z";
                    break;
            }

            return strRes;
        }

        public static int fcnGetNumAlpha(string _strIn)
        {
            int intRes = 0;

            switch (_strIn.ToLower())
            {
                case "a":
                    intRes = 1;
                    break;
                case "b":
                    intRes = 2;
                    break;
                case "c":
                    intRes = 3;
                    break;
                case "d":
                    intRes = 4;
                    break;
                case "e":
                    intRes = 5;
                    break;
                case "f":
                    intRes = 6;
                    break;
                case "g":
                    intRes = 7;
                    break;
                case "h":
                    intRes = 8;
                    break;
                case "i":
                    intRes = 9;
                    break;
                case "j":
                    intRes = 0;
                    break;
                case "k":
                    intRes = 11;
                    break;
                case "l":
                    intRes = 12;
                    break;
                case "m":
                    intRes = 13;
                    break;
                case "n":
                    intRes = 14;
                    break;
                case "o":
                    intRes = 15;
                    break;
                case "p":
                    intRes = 16;
                    break;
                case "q":
                    intRes = 17;
                    break;
                case "r":
                    intRes = 18;
                    break;
                case "s":
                    intRes = 19;
                    break;
                case "t":
                    intRes = 20;
                    break;
                case "u":
                    intRes = 21;
                    break;
                case "v":
                    intRes = 22;
                    break;
                case "w":
                    intRes = 23;
                    break;
                case "x":
                    intRes = 24;
                    break;
                case "y":
                    intRes = 25;
                    break;
                case "z":
                    intRes = 26;
                    break;
            }

            return intRes;
        }

        public static clsGlobalEnum.conLIVECHARGE fcnLiveChargeMethod()
        {
            clsGlobalEnum.conLIVECHARGE intRes = clsGlobalEnum.conLIVECHARGE.None;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand())
                {
                    cmdDB.Connection = conDB;

                    intRes = fcnLiveChargeMethod(cmdDB);
                }

                conDB.Close();
            }

            return intRes;
        }

        public static clsGlobalEnum.conLIVECHARGE fcnLiveChargeMethod(OleDbCommand _cmdDB)
        {
            clsGlobalEnum.conLIVECHARGE intRes = clsGlobalEnum.conLIVECHARGE.None;

            string strSQL = "SELECT lngLiveCharge " +
                            "FROM tblCampDefaults;";

            _cmdDB.CommandText = strSQL;
            _cmdDB.Parameters.Clear();

            try { intRes = (clsGlobalEnum.conLIVECHARGE)_cmdDB.ExecuteScalar(); }
            catch { intRes = clsGlobalEnum.conLIVECHARGE.None; }

            return intRes;
        }

        public static void subXChargeVars(OleDbCommand _cmdDB, ref string _strXChargePath, ref string _strXWebID, ref string _strAuthKey, ref string _strTerminalID)
        {
            string strSQL = "SELECT strXChargePath, strXChargeWebID, strXChargeAuthKey, strXChargeTerminalID " +
                            "FROM tblCampDefaults;";

            _cmdDB.CommandText = strSQL;
            _cmdDB.Parameters.Clear();

            using (OleDbDataReader drXCharge = _cmdDB.ExecuteReader())
            {
                if (drXCharge.Read())
                {
                    try
                    {
                        _strXChargePath = Convert.ToString(drXCharge["strXChargePath"]);
                        _strXWebID = Convert.ToString(drXCharge["strXChargeWebID"]);
                        _strAuthKey = Convert.ToString(drXCharge["strXChargeAuthKey"]);
                        _strTerminalID = Convert.ToString(drXCharge["strXChargeTerminalID"]);
                    }
                    catch { }
                }

                drXCharge.Close();
            }
        }

        public static void subProcessRefundXCCC(decimal _decAmt, string _strXCTransID, string _strXCAlias)
        {
            string strXMLToPost = "";
            string strXWebID = "";
            string strXWebAuthKey = "";
            string strXWebTerminalID = "";

            //get xcharge user settings
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strSQL = "";

                strSQL = "SELECT strXChargeWebID, strXChargeAuthKey, strXChargeTerminalID " +
                        "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drXCharge = cmdDB.ExecuteReader())
                    {
                        if (drXCharge.Read())
                        {
                            strXWebID = Convert.ToString(drXCharge["strXChargeWebID"]);
                            strXWebAuthKey = Convert.ToString(drXCharge["strXChargeAuthKey"]);
                            strXWebTerminalID = Convert.ToString(drXCharge["strXChargeTerminalID"]);
                        }

                        drXCharge.Close();
                    }
                }

                conDB.Close();
            }

            strXMLToPost = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                "<GatewayRequest>" +
                                    "<SpecVersion>XWeb3.0</SpecVersion>" +
                                    "<XWebID>" + strXWebID + "</XWebID>" +
                                    "<AuthKey>" + strXWebAuthKey + "</AuthKey>" +
                                    "<TerminalID>" + strXWebTerminalID + "</TerminalID>" +
                                    "<Industry>ECOMMERCE</Industry>" +
                                    "<TransactionType>CreditReturnTransaction</TransactionType>" +
                                    "<DuplicateMode>CHECKING_OFF</DuplicateMode>" +
                                    "<CustomerPresent>FALSE</CustomerPresent>" +
                                    "<CardPresent>FALSE</CardPresent>" +
                                    "<Amount>" + _decAmt.ToString("F") + "</Amount>" +
                                    "<TransactionID>" + _strXCTransID + "</TransactionID>" +
                                    "<Alias>" + _strXCAlias + "</Alias>" +
                                    "<PinCapabilities>FALSE</PinCapabilities>" +
                                    "<TrackCapabilities>NONE</TrackCapabilities>" +
                                    "<POSType>PC</POSType>" +
                                "</GatewayRequest>";

            string strResultXML = "";

            WebClient objRequest = new WebClient();

            objRequest.Encoding = System.Text.Encoding.ASCII;

            //convert xml string to byte array
            System.Byte[] bytToSend = System.Text.Encoding.ASCII.GetBytes(strXMLToPost);

            strResultXML = System.Text.Encoding.ASCII.GetString(objRequest.UploadData(strXChargeURI, "POST", bytToSend));

            XmlDocument xmlResponse = new XmlDocument();

            xmlResponse.Load(new StringReader(strResultXML));

            string strTransOutID = "";

            try { strTransOutID = xmlResponse["GatewayResponse"]["TransactionID"].InnerText; }
            catch { strTransOutID = ""; }

            string strResponseCode = "";

            try { strResponseCode = xmlResponse["GatewayResponse"]["ResponseCode"].InnerText; }
            catch { strResponseCode = "ERR"; }

            if (strResponseCode != "000")
                System.Windows.Forms.MessageBox.Show("There was an error processing the refund:\n\n" + strResultXML);
        }

        public static void subProcessRefundXCEFT(decimal _decAmt, long _lngRegWebID, string _strAcctNumber, string _strRoutingNumber, string _strBillName, string _strBillAddress, string _strBillZip, int _intHandle, string _strXChargePath)
        {
            XCTransaction2.XChargeTransaction objXC = new XCTransaction2.XChargeTransaction();

            string strErr = "";
            string strApp = "";
            string strRes = "";

            bool blnRes;

            blnRes = objXC.XCACHDebit(_intHandle, _strXChargePath, "Processing EFT Refund", true, true, _decAmt.ToString(), _strRoutingNumber, _strAcctNumber, "CHECKING", "PERSONAL", _strBillName, _strBillAddress, _strBillZip, clsNav.objLogin.cboUsers.Text, _lngRegWebID.ToString(), out strApp, out strErr, out strRes);

            if (!blnRes)
                System.Windows.Forms.MessageBox.Show("There was an error processing the refund.");
        }

        public static void subProcessRefundEPSEFT(decimal _decAmt)
        {
            System.Windows.Forms.MessageBox.Show("EFT Refunds for EPS transactions are not yet implemented.\n\nPlease process manually.");
        }

        public static void subProcessRefundEPSCC(decimal _decAmt, long _lngCampTrakID, string _strOriginalEPSTransID)
        {
            //Get TransactionSetupID from EPS
            try
            {
                wsEPSTrans.Express EPSExpress = new wsEPSTrans.Express();

                wsEPSTrans.Credentials EPSCred = new wsEPSTrans.Credentials();
                wsEPSTrans.Application EPSApp = new wsEPSTrans.Application();
                wsEPSTrans.Terminal EPSTerminal = new wsEPSTrans.Terminal();
                wsEPSTrans.Transaction EPSTrans = new wsEPSTrans.Transaction();
                wsEPSTrans.ExtendedParameters[] EPSExtParam = new wsEPSTrans.ExtendedParameters[0];

                string strEPSAcceptorID = "";
                string strEPSAccountID = "";
                string strEPSAccountToken = "";
                string strEPSTerminalID = "";

                try
                {
                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        string strSQL = "SELECT strEPSAcceptorID, strEPSAccountID, strEPSAccountToken, strEPSTerminalID " +
                                        "FROM tblCampDefaults";

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                            {
                                if (drDef.Read())
                                {
                                    strEPSAcceptorID = Convert.ToString(drDef["strEPSAcceptorID"]);
                                    strEPSAccountID = Convert.ToString(drDef["strEPSAccountID"]);
                                    strEPSAccountToken = Convert.ToString(drDef["strEPSAccountToken"]);
                                    strEPSTerminalID = Convert.ToString(drDef["strEPSTerminalID"]);
                                }

                                drDef.Close();
                            }
                        }

                        conDB.Close();
                    }
                }
                catch { }

                EPSCred.AcceptorID = strEPSAcceptorID;
                EPSCred.AccountID = strEPSAccountID;
                EPSCred.AccountToken = strEPSAccountToken;

                EPSApp.ApplicationID = "1190";
                EPSApp.ApplicationName = "CampTrak Software";
                EPSApp.ApplicationVersion = "1.0.0";

                EPSTrans.ReferenceNumber = _lngCampTrakID.ToString();

                EPSTerminal.TerminalID = strEPSTerminalID;

                EPSTrans.TransactionAmount = _decAmt.ToString("C").Replace("$", "");
                EPSTrans.TransactionID = _strOriginalEPSTransID;

                wsEPSTrans.Response EPSResponse = EPSExpress.CreditCardReturn(EPSCred, EPSApp, EPSTerminal, EPSTrans, EPSExtParam);

                System.Windows.Forms.MessageBox.Show("Response: " + EPSResponse.ExpressResponseMessage);
            }
            catch (Exception ex)
            {
            }
        }

        public static void subProcessRefundCashLinqCC(decimal _decAmt, string _strPNRef)
        {
            try
            {
                Int64 lngTransID = Convert.ToInt64(_strPNRef);
                decimal decAmt = _decAmt;
                string strReason = "Reg Cancelled";

                string strCQUserID = "";
                string strUserType = "";
                string strUserKey = "";

                //need to be looked up from db
                string strCustomQWSUserName = clsAppSettings.GetAppSettings().strCashLinqCQUser;
                string strCustomQWSPW = clsAppSettings.GetAppSettings().strCashLinqCQPW;
                string strCustomQMerchantID = clsAppSettings.GetAppSettings().strCashLinqCQMerchantID;

                string strResXML = "";

                using (wsCashLinq.CQ wsCLCQ = new global::CTWebMgmt.wsCashLinq.CQ())
                {
                    //login to get cquserid
                    using (DataSet dsLoginRes = new DataSet())
                    {
                        strResXML = wsCLCQ.MerchLogin(strCustomQWSUserName, strCustomQWSPW, strCustomQMerchantID);

                        dsLoginRes.ReadXml(new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(strResXML)));

                        try
                        {
                            strCQUserID = dsLoginRes.Tables[0].Rows[0]["UID"].ToString();
                            strUserType = dsLoginRes.Tables[0].Rows[0]["UType"].ToString();
                            strUserKey = dsLoginRes.Tables[0].Rows[0]["UserKey"].ToString();
                        }
                        catch
                        {
                            strCQUserID = "";
                            strUserType = "";
                            strUserKey = "";
                        }
                    }

                    wsCashLinq.Response wsCLResp = new global::CTWebMgmt.wsCashLinq.Response();

                    wsCLResp = wsCLCQ.Refund(Convert.ToInt64(strCQUserID), Convert.ToByte(strUserType), strUserKey, lngTransID, decAmt, strReason);

                    if (wsCLResp.RespMSG.ToUpper() != "Approved".ToUpper())
                    {
                        System.Windows.Forms.MessageBox.Show("There was an error processing the refund: " + wsCLResp.RespMSG);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            }

            return;
        }

        public static void subProcessRefundCashLinqEFT(decimal _decAmt, long _lngRegWebID, string _strAcctNumber, string _strRoutingNumber, string _strBillName, string _strBillAddress, string _strBillZip, int _intHandle)
        {
            System.Windows.Forms.MessageBox.Show("CashLinq Refunds are pending");
        }

        public static string fcnGetXChargePath()
        {
            string strSQL = "";
            string strRes = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCampDefaults.strXChargePath " +
                        "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { strRes = Convert.ToString(cmdDB.ExecuteScalar()); }
                    catch { strRes = ""; }
                }

                conDB.Close();
            }

            return strRes;
        }

        public static string fcnGetSavedXChargeAcct(int _intHandle, string _strXCAcctID, string _strXCPath)
        {
            XCTransaction2.XChargeTransaction Tran = new XCTransaction2.XChargeTransaction();

            string Acct = "";
            string ExpDate = "";
            string AcctType = "";
            string PurgeDate = "";
            string ErrMsg = "";

            string strRes = "";
            if (Tran.XCArchiveVaultQuery(_intHandle, _strXCPath, "DAV Query", true, true, _strXCAcctID, "", ref _strXCAcctID, out Acct, out ExpDate, out AcctType, out PurgeDate, out ErrMsg))
                strRes = ErrMsg + "\n" +
                    "XCAcctID = " + _strXCAcctID + "\n" +
                    "Acct = " + Acct + "\n" +
                    "ExpDate = " + ExpDate + "\n" +
                    "AcctType = " + AcctType + "\n" +
                    "PurgeDate = " + PurgeDate;

            return strRes;
        }

        public static bool fcnCashLinqRepeatSale(long _lngCTID, long _lngRecordID, decimal _decAmt, string _strPNRefIn, ref string _strReturnedPNRef, ref string _strAuthNumber)
        {
            string strCQUserID = "";
            string strUserType = "";
            string strUserKey = "";

            bool blnRes = false;

            string strCustomQWSUserName = "";
            string strCustomQWSPW = "";
            string strCustomQMerchantID = "";

            strCustomQWSUserName = clsAppSettings.GetAppSettings().strCashLinqCQUser;
            strCustomQWSPW = clsAppSettings.GetAppSettings().strCashLinqCQPW;
            strCustomQMerchantID = clsAppSettings.GetAppSettings().strCashLinqCQMerchantID;

            string strResXML = "";

            using (wsCashLinq.CQ wsCLCQ = new global::CTWebMgmt.wsCashLinq.CQ())
            {
                //login to service
                using (DataSet dsLoginRes = new DataSet())
                {
                    strResXML = wsCLCQ.MerchLogin(strCustomQWSUserName, strCustomQWSPW, strCustomQMerchantID);

                    dsLoginRes.ReadXml(new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(strResXML)));

                    try
                    {
                        strCQUserID = dsLoginRes.Tables[0].Rows[0]["UID"].ToString();
                        strUserType = dsLoginRes.Tables[0].Rows[0]["UType"].ToString();
                        strUserKey = dsLoginRes.Tables[0].Rows[0]["UserKey"].ToString();
                    }
                    catch
                    {
                        strCQUserID = "";
                        strUserType = "";
                        strUserKey = "";
                    }
                }

                wsCashLinq.Response wsCLResponse = new global::CTWebMgmt.wsCashLinq.Response();

                wsCLResponse = wsCLCQ.RepeatSale(Convert.ToInt64(strCQUserID), Convert.ToByte(strUserType), strUserKey, Convert.ToInt32(_strPNRefIn), _decAmt, DateTime.Now);

                string strRespMsg = "";

                strRespMsg = wsCLResponse.RespMSG;
                _strReturnedPNRef = wsCLResponse.PNRef;
                _strAuthNumber = wsCLResponse.AuthCode;

                if (strRespMsg.ToUpper() != "Approved".ToUpper())
                    blnRes = false;
                else
                    blnRes = true;
            }

            return blnRes;

        }

        public static bool fcnEPSRepeatSale(long _lngCTID, long _lngRecordID, decimal _decAmt, string _strEPSPmtAcctID, ref string _strEPSTransIDResult, ref string _strAuthNumber, ref string _strEPSApprovalNumber, ref string _strEPSValidationCode)
        {
            bool blnRes = false;

            //Get TransactionSetupID from EPS
            try
            {
                wsEPSTrans.Express EPSExpress = new wsEPSTrans.Express();

                wsEPSTrans.Credentials EPSCred = new wsEPSTrans.Credentials();
                wsEPSTrans.Application EPSApp = new wsEPSTrans.Application();
                wsEPSTrans.Terminal EPSTerminal = new wsEPSTrans.Terminal();
                wsEPSTrans.Transaction EPSTrans = new wsEPSTrans.Transaction();
                wsEPSTrans.ExtendedParameters[] EPSExtParam = new wsEPSTrans.ExtendedParameters[1];
                wsEPSTrans.ExtendedParameters EPSParam = new global::CTWebMgmt.wsEPSTrans.ExtendedParameters();
                wsEPSTrans.PaymentAccount EPSPmtAcct = new global::CTWebMgmt.wsEPSTrans.PaymentAccount();

                string strEPSAcceptorID = "";
                string strEPSAccountID = "";
                string strEPSAccountToken = "";
                string strEPSTerminalID = "";

                try
                {
                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        string strSQL = "SELECT strEPSAcceptorID, strEPSAccountID, strEPSAccountToken, strEPSTerminalID " +
                                        "FROM tblCampDefaults";

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                            {
                                if (drDef.Read())
                                {
                                    strEPSAcceptorID = Convert.ToString(drDef["strEPSAcceptorID"]);
                                    strEPSAccountID = Convert.ToString(drDef["strEPSAccountID"]);
                                    strEPSAccountToken = Convert.ToString(drDef["strEPSAccountToken"]);
                                    strEPSTerminalID = Convert.ToString(drDef["strEPSTerminalID"]);
                                }

                                drDef.Close();
                            }
                        }

                        conDB.Close();
                    }
                }
                catch { }

                EPSCred.AcceptorID = strEPSAcceptorID;
                EPSCred.AccountID = strEPSAccountID;
                EPSCred.AccountToken = strEPSAccountToken;

                EPSApp.ApplicationID = "1190";
                EPSApp.ApplicationName = "CampTrak Software";
                EPSApp.ApplicationVersion = "1.0.0";

                EPSTrans.ReferenceNumber = _lngCTID.ToString();

                EPSTerminal.TerminalID = strEPSTerminalID;

                EPSTrans.TransactionAmount = _decAmt.ToString("C").Replace("$", "");

                EPSPmtAcct.PaymentAccountID = _strEPSPmtAcctID;
                EPSParam.Key = "PaymentAccount";
                EPSParam.Value = EPSPmtAcct;
                EPSExtParam[0] = EPSParam;

                wsEPSTrans.Response EPSResponse = EPSExpress.CreditCardSale(EPSCred, EPSApp, EPSTerminal, null, EPSTrans, null, EPSExtParam);

                //evaluate response...
                //set return values
                _strEPSTransIDResult = EPSResponse.Transaction.TransactionID;
                _strAuthNumber = EPSResponse.Transaction.ApprovalNumber;
                _strEPSApprovalNumber = EPSResponse.Transaction.ApprovalNumber;
                _strEPSValidationCode = "";

                if (EPSResponse.Transaction.TransactionStatusCode == "1") blnRes = true;
            }
            catch (Exception ex)
            {
            }

            return blnRes;

        }

        public static bool fcnLiveXChargeWeb(decimal _decAmt, string _strXCAlias, ref string _strXCTransIDOut, string strInvoiceNumber, ref decimal _decApprovedAmt)
        {

            bool blnRes = false;

            string strXMLToPost = "";
            string strXWebID = "";
            string strXWebAuthKey = "";
            string strXWebTerminalID = "";
            string strXChargePath = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand())
                {
                    cmdDB.Connection = conDB;

                    subXChargeVars(cmdDB, ref strXChargePath, ref strXWebID, ref strXWebAuthKey, ref strXWebTerminalID);
                }

                conDB.Close();
            }

            strXMLToPost = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                            "<GatewayRequest>" +
                                "<SpecVersion>XWeb3.0</SpecVersion>" +
                                "<XWebID>" + strXWebID + "</XWebID>" +
                                "<AuthKey>" + strXWebAuthKey + "</AuthKey>" +
                                "<TerminalID>" + strXWebTerminalID + "</TerminalID>" +
                                "<Industry>ECOMMERCE</Industry>" +
                                "<TransactionType>CreditSaleTransaction</TransactionType>" +
                                "<DuplicateMode>CHECKING_OFF</DuplicateMode>" +
                                "<Amount>" + _decAmt.ToString("F") + "</Amount>" +
                                "<Alias>" + _strXCAlias + "</Alias>" +
                                "<UpdateAlias>FALSE</UpdateAlias>" +
                                "<InvoiceNumber>" + strInvoiceNumber + "</InvoiceNumber>" +
                                "<POSType>PC</POSType>" +
                                "<PinCapabilities>FALSE</PinCapabilities>" +
                                "<TrackCapabilities>NONE</TrackCapabilities>" +
                                "<CustomerPresent>FALSE</CustomerPresent>" +
                                "<CardPresent>FALSE</CardPresent>" +
                                "<ECI>7</ECI>" +
                            "</GatewayRequest>";

            string strResultXML = "";

            WebClient objRequest = new WebClient();

            objRequest.Encoding = System.Text.Encoding.ASCII;

            //convert xml string to byte array
            System.Byte[] bytToSend = System.Text.Encoding.ASCII.GetBytes(strXMLToPost);

            strResultXML = System.Text.Encoding.ASCII.GetString(objRequest.UploadData(strXChargeURI, "POST", bytToSend));

            _strXCTransIDOut = "";

            XmlDocument xmlResponse = new XmlDocument();

            xmlResponse.Load(new StringReader(strResultXML));

            try { _strXCTransIDOut = xmlResponse["GatewayResponse"]["TransactionID"].InnerText; }
            catch { _strXCTransIDOut = ""; }

            string strResponseCode = "";

            try { strResponseCode = xmlResponse["GatewayResponse"]["ResponseCode"].InnerText; }
            catch { strResponseCode = "ERR"; }

            if (strResponseCode != "000")
            {
                //xweb refund failed...try local method
                blnRes = false;
            }
            else
            {
                blnRes = true;
                _decApprovedAmt = _decAmt;
            }

            return blnRes;
        }
    }
}