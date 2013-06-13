using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
using System.Xml;
using System.IO;

namespace CTWebMgmt.Admin.Reports
{
    public partial class frmReconcileMerchantGatewaySetup : Form
    {
        public frmReconcileMerchantGatewaySetup()
        {
            InitializeComponent();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            DateTime dteStart = DateTime.MinValue;
            DateTime dteEnd = DateTime.MinValue;

            try { dteStart = dtpStart.Value; }
            catch { dteStart = DateTime.MinValue; }

            try { dteEnd = dtpEnd.Value; }
            catch { dteEnd = DateTime.MinValue; }

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand("DELETE * FROM tblMerchantGatewayTrans", conDB))
                {
                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }
                }

                conDB.Close();
            }

            //select which cc val codes to display
            switch (clsLiveCharge.fcnGetLiveChargeMethod())
            {
                case clsGlobalEnum.conLIVECHARGE.CashLinq:
                    {
                        if (!fcnGetCashLinqTrans(dteStart, dteEnd))
                            MessageBox.Show("There was an error connecting to the CashLinq gateway.\n\nTry again later or email support@camptrak.com for additional help.");

                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.XCharge:
                    {
                        if (DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) > dtpStart.Value)
                        {
                            MessageBox.Show("X-Charge allows reporting on the last 90 days only.\nPlease modify the start date.");
                            dtpStart.Focus();
                            return;
                        }

                        if (!fcnGetXCTrans(dteStart, dteEnd))
                            MessageBox.Show("There was an error connecting to the X-Charge gateway.\n\nTry again later or email support@camptrak.com for additional help.");

                        break;
                    }

                case clsGlobalEnum.conLIVECHARGE.EPS:
                    {
                        if (!fcnGetEPSTrans(dteStart, dteEnd))
                            MessageBox.Show("There was an error connecting to the Element gateway.\n\nTry again later or email support@camptrak.com for additional help.");

                        break;
                    }
            }

            using (frmReconcileMerchantGateway objReconcileMerchantGateway = new frmReconcileMerchantGateway())
            {
                objReconcileMerchantGateway.ShowDialog();
            }
        }

        private bool fcnGetCashLinqTrans(DateTime _dteStart, DateTime _dteEnd)
        {
            bool blnRes = false;

            try
            {
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

                        try { dsLoginRes.ReadXml(new System.IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(strResXML))); }
                        catch { }

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

                    string strPageName = "";
                    string strRes = "";

                    if (radAllCTTrans.Checked || radUnmatched.Checked || radCTWeb.Checked)
                    {
                        if (clsAppSettings.GetAppSettings().lngCTUserID == 876299671)
                            strPageName = "CTPmtPageAlt_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString();
                        else
                            strPageName = "CTPmtPage_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString();                        

                        strRes = wsCLCQ.GetReconciled(Convert.ToInt64(strCQUserID), Convert.ToByte(strUserType), strUserKey, strPageName, _dteStart, _dteEnd);

                        if (strRes == "Invalid Login")
                        {
                            MessageBox.Show("Invalid CashLinq credentials.\nIn CampTrak select Administration-->Set Defaults to update your CashLinq CustomQ user name and password.");
                            return false;
                        }

                        blnRes = fcnAddCashLinqTrans(strRes, "Web Module");
                    }

                    if (blnRes || radCTLocal.Checked)
                    {
                        if (clsAppSettings.GetAppSettings().lngCTUserID == 876299671)
                            strPageName = "LocalTranAlt_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString();
                        else
                            strPageName = "LocalTran_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString();

                        strRes = wsCLCQ.GetReconciled(Convert.ToInt64(strCQUserID), Convert.ToByte(strUserType), strUserKey, strPageName, _dteStart, _dteEnd);

                        blnRes = fcnAddCashLinqTrans(strRes, "Local App");

                    }

                    //eliminate matched transactions from data set if needed
                    if (radUnmatched.Checked)
                    {
                        string strSQL = "";

                        using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                        {
                            conDB.Open();

                            strSQL = "DROP VIEW qryMerchantTransSummary";

                            using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                            {
                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "DROP VIEW qryMerchantGiftSummary";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "CREATE VIEW qryMerchantTransSummary AS " +
                                            "SELECT Count(tblTransactions.lngTransactionID) AS CountOflngTransactionID, " +
                                                "Sum(tblTransactions.curPayment) AS curTotAmt, " +
                                                "Min(tblTransactions.dteDateAdded) AS dteDateAdded, " +
                                                "tblTransactions.strPNRef " +
                                            "FROM tblTransactions " +
                                            "WHERE IIf([tblTransactions].[strPNRef] Is Null, '', [tblTransactions].[strPNRef]) <>'' AND " +
                                                "DateDiff('d', #" + _dteStart.ToShortDateString() + "#, [tblTransactions].[dteDateAdded]) >= 0 AND " +
                                                "DateDiff('d', [tblTransactions].[dteDateAdded], #" + _dteEnd.ToShortDateString() + "#) >=0 " +
                                            "GROUP BY tblTransactions.strPNRef";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "CREATE VIEW qryMerchantGiftSummary AS " +
                                            "SELECT Count(tblGift.lngGiftID) AS CountOflngGiftID, " +
                                                "Sum(tblGift.curAmount) AS curTotAmt, " +
                                                "Min(tblGift.dteGiftDate) AS dteGiftDate, " +
                                                "tblGift.strPNRef " +
                                            "FROM tblGift " +
                                            "WHERE DateDiff('d', #" + _dteStart.ToShortDateString() + "#, [tblGift].[dteGiftDate]) >=0 AND " +
                                                "DateDiff('d', [tblGift].[dteGiftDate], #" + _dteEnd.ToShortDateString() + "#) >= 0 AND " +
                                                "IIf([tblGift].[strPNRef] Is Null, '', [tblGift].[strPNRef]) <> '' " +
                                            "GROUP BY tblGift.strPNRef";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "DELETE tblMerchantGatewayTrans.* " +
                                        "FROM tblMerchantGatewayTrans " +
                                        "WHERE tblMerchantGatewayTrans.lngGWTransID In " +
                                            "(" +
                                                "SELECT tblMerchantGatewayTrans.lngGWTransID " +
                                                "FROM (tblMerchantGatewayTrans " +
                                                    "LEFT JOIN qryMerchantGiftSummary ON (tblMerchantGatewayTrans.strMerchantRef = qryMerchantGiftSummary.strPNRef) AND " +
                                                        "(tblMerchantGatewayTrans.decAmount = qryMerchantGiftSummary.curTotAmt)) " +
                                                    "LEFT JOIN qryMerchantTransSummary ON (tblMerchantGatewayTrans.decAmount = qryMerchantTransSummary.curTotAmt) AND " +
                                                        "(tblMerchantGatewayTrans.strMerchantRef = qryMerchantTransSummary.strPNRef) " +
                                                "WHERE qryMerchantGiftSummary.CountOflngGiftID Is Not Null OR " +
                                                    "qryMerchantTransSummary.CountOflngTransactionID Is Not Null " +
                                            ")";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }
                            }

                            conDB.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                blnRes = false;
            }

            return blnRes;
        }

        private bool fcnAddCashLinqTrans(string _strXML, string _strSource)
        {
            bool blnRes = false;

            using (DataSet dsRes = new DataSet())
            {
                dsRes.ReadXml(new System.IO.StringReader(_strXML));

                using (DataTable dtRes = dsRes.Tables[0])
                {

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        string strSQL = "";

                        strSQL = "INSERT INTO tblMerchantGatewayTrans " +
                                "( lngGWTransID, " +
                                    "dteTransDate, " +
                                    "decAmount, " +
                                    "strFName, strLName, strAddress, strCity, strState, strZip, strCountry, strEmail, strCTID, strStatus, strLastFour, strMerchantRef, strSource ) " +
                                "VALUES " +
                                "(@lngGWTransID, " +
                                    "@dteTransDate, " +
                                    "@decAmount, " +
                                    "@strFName, @strLName, @strAddress, @strCity, @strState, @strZip, @strCountry, @strEmail, @strCTID, @strStatus, @strLastFour, @strMerchantRef, @strSource )";

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            long lngGWTransID = 0;

                            DateTime dteTransDate = DateTime.MinValue;
                            decimal decAmount = 0;

                            string strFName = "";
                            string strLName = "";
                            string strAddress = "";
                            string strCity = "";
                            string strState = "";
                            string strZip = "";
                            string strCountry = "";
                            string strEmail = "";
                            string strCTID = "";
                            string strStatus = "";
                            string strLastFour = "";
                            string strMerchantRef = "";

                            for (int intRow = 0; intRow < dtRes.Rows.Count; intRow++)
                            {
                                DataRow rowCurrent = dtRes.Rows[intRow];

                                lngGWTransID = Convert.ToInt64(rowCurrent["TransID"]);
                                dteTransDate = Convert.ToDateTime(rowCurrent["TransTime"]);
                                decAmount = Convert.ToDecimal(rowCurrent["Amount"]);
                                strFName = Convert.ToString(rowCurrent["FirstName"]);
                                strLName = Convert.ToString(rowCurrent["LastName"]);
                                strAddress = Convert.ToString(rowCurrent["Address"]);
                                strCity = Convert.ToString(rowCurrent["City"]);
                                strState = Convert.ToString(rowCurrent["State"]);
                                strZip = Convert.ToString(rowCurrent["Zip"]);
                                strCountry = Convert.ToString(rowCurrent["CountryName"]);
                                strEmail = Convert.ToString(rowCurrent["Email"]);
                                strCTID = Convert.ToString(rowCurrent["Custom1"]);
                                strStatus = Convert.ToString(rowCurrent["Status"]);
                                strLastFour = Convert.ToString(rowCurrent["AcctInfo1"]);
                                strMerchantRef = Convert.ToString(rowCurrent["TransID"]);

                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.AddWithValue("@lngGWTransID", lngGWTransID);
                                cmdDB.Parameters.AddWithValue("@dteTransDate", dteTransDate);
                                cmdDB.Parameters[1].OleDbType = OleDbType.DBDate;
                                cmdDB.Parameters.AddWithValue("@decAmount", decAmount);
                                cmdDB.Parameters.AddWithValue("@strFName", strFName);
                                cmdDB.Parameters.AddWithValue("@strLName", strLName);
                                cmdDB.Parameters.AddWithValue("@strAddress", strAddress);
                                cmdDB.Parameters.AddWithValue("@strCity", strCity);
                                cmdDB.Parameters.AddWithValue("@strState", strState);
                                cmdDB.Parameters.AddWithValue("@strZip", strZip);
                                cmdDB.Parameters.AddWithValue("@strCountry", strCountry);
                                cmdDB.Parameters.AddWithValue("@strEmail", strEmail);
                                cmdDB.Parameters.AddWithValue("@strCTID", strCTID);
                                cmdDB.Parameters.AddWithValue("@strStatus", strStatus);
                                cmdDB.Parameters.AddWithValue("@strLastFour", strLastFour);
                                cmdDB.Parameters.AddWithValue("@strMerchantRef", strMerchantRef);
                                cmdDB.Parameters.AddWithValue("@strSource", _strSource);

                                cmdDB.ExecuteNonQuery();
                            }

                            blnRes = true;
                        }

                        conDB.Close();
                    }
                }
            }

            return blnRes;
        }

        private bool fcnGetXCTrans(DateTime _dteStart, DateTime _dteEnd)
        {
            bool blnRes = false;

            string strXMLToPost = "";
            string strXWebID = "";
            string strXWebAuthKey = "";
            string strXWebTerminalID = "";
            string strSQL = "";

            //get xcharge user settings
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

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

            int intCurrentPage = 1;
            bool blnContinueToNextPage = true;

            while (blnContinueToNextPage)
            {
                strXMLToPost = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                    "<GatewayRequest>" +
                                        "<SpecVersion>XWeb3.3</SpecVersion>" +
                                        "<XWebID>" + strXWebID + "</XWebID>" +
                                        "<AuthKey>" + strXWebAuthKey + "</AuthKey>" +
                                        "<TerminalID>" + strXWebTerminalID + "</TerminalID>" +
                                        "<Industry>ECOMMERCE</Industry>" +
                                        "<TransactionType>BatchRequestTransaction</TransactionType>" +
                                        "<BatchTransactionType>REPORT</BatchTransactionType>" +
                                        "<PageNumber>" + intCurrentPage.ToString() + "</PageNumber>" +
                                        "<RecordsPerPage>100</RecordsPerPage>" +
                                        "<StartDate>" + _dteStart.Month.ToString("D2") + _dteStart.Day.ToString("D2") + _dteStart.Year.ToString().Substring(2, 2) + "</StartDate>" +
                                        "<EndDate>" + _dteEnd.Month.ToString("D2") + _dteEnd.Day.ToString("D2") + _dteEnd.Year.ToString().Substring(2, 2) + "</EndDate>" +
                                        "<Details>TRUE</Details>" +
                                        "<POSType>PC</POSType>" +
                                        "<PinCapabilities>FALSE</PinCapabilities>" +
                                        "<TrackCapabilities>NONE</TrackCapabilities>" +
                                        "<BatchNum>ALL</BatchNum>" +
                                    "</GatewayRequest>";

                string strResultXML = "";

                WebClient objRequest = new WebClient();

                objRequest.Encoding = System.Text.Encoding.ASCII;

                //convert xml string to byte array
                System.Byte[] bytToSend = System.Text.Encoding.ASCII.GetBytes(strXMLToPost);

                strResultXML = System.Text.Encoding.ASCII.GetString(objRequest.UploadData(clsLiveCharge.strXChargeRptURI, "POST", bytToSend));

                XmlDocument xmlResponse = new XmlDocument();

                xmlResponse.Load(new StringReader(strResultXML));

                string strTransRpt = "";

                try { strTransRpt = xmlResponse["GatewayResponse"]["BatchDetailsResponse"].InnerText; }
                catch { strTransRpt = ""; }

                try
                {
                    if (Convert.ToInt32(xmlResponse["GatewayResponse"]["TotalPages"].InnerText) > intCurrentPage)
                        blnContinueToNextPage = true;
                    else
                        blnContinueToNextPage = false;
                }
                catch { blnContinueToNextPage = false; }

                intCurrentPage++;

                if (!strTransRpt.Contains("\n"))
                {
                    MessageBox.Show("There were no transactions returned by the request.\nPlease expand your criteria and try again.");
                    return false;
                }

                using (DataTable dtRes = new DataTable())
                {
                    string strColRow = strTransRpt.Substring(0, strTransRpt.IndexOf("\n"));

                    string[] strCols = strColRow.Split('\t');

                    for (int intI = 0; intI < strCols.Length; intI++)
                        dtRes.Columns.Add(new DataColumn(strCols[intI], typeof(string)));

                    dtRes.Columns.Add(new DataColumn("HOLDER", typeof(string)));

                    string[] strLines = strTransRpt.Split('\n');

                    for (int intI = 1; intI < strLines.Length; intI++)
                    {
                        dtRes.Rows.Add(strLines[intI].Split('\t'));

                        if (dtRes.Rows[dtRes.Rows.Count - 1]["Transaction Type"].ToString() != "CreditSaleTransaction")
                            dtRes.Rows[dtRes.Rows.Count - 1].Delete();
                    }

                    blnRes = fcnAddXCTrans(dtRes);
                }
            }

            //remove specific matched/category transactions if needed
            if (radCTLocal.Checked)
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "DELETE tblMerchantGatewayTrans.* " +
                            "FROM tblMerchantGatewayTrans " +
                            "WHERE tblMerchantGatewayTrans.strSource=@strSource";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.Parameters.AddWithValue("@strSource", "WEB MODULE");

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }

                    conDB.Close();
                }
            }
            else if (radCTWeb.Checked)
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "DELETE tblMerchantGatewayTrans.* " +
                            "FROM tblMerchantGatewayTrans " +
                            "WHERE tblMerchantGatewayTrans.strSource=@strSource";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.Parameters.AddWithValue("@strSource", "LOCAL APP");

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }

                    conDB.Close();
                }
            }
            else if (radUnmatched.Checked)
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "DROP VIEW qryMerchantTransSummary";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "DROP VIEW qryMerchantGiftSummary";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "CREATE VIEW qryMerchantTransSummary AS " +
                                    "SELECT Count(tblTransactions.lngTransactionID) AS CountOflngTransactionID, " +
                                        "Sum(tblTransactions.curPayment) AS curTotAmt, " +
                                        "Min(tblTransactions.dteDateAdded) AS dteDateAdded, " +
                                        "tblTransactions.strXCTransID, tblTransactions.strXCAuthCode " +
                                    "FROM tblTransactions " +
                                    "WHERE (IIf([tblTransactions].[strXCTransID] Is Null, '', [tblTransactions].[strXCTransID]) <> '' OR " +
                                        "IIf([tblTransactions].[strXCAuthCode] Is Null, '', [tblTransactions].[strXCAuthCode]) <>'') AND " +
                                        "DateDiff('d', #" + _dteStart.ToShortDateString() + "#, [tblTransactions].[dteDateAdded]) >= 0 AND " +
                                        "DateDiff('d', [tblTransactions].[dteDateAdded], #" + _dteEnd.ToShortDateString() + "#) >=0 " +
                                    "GROUP BY tblTransactions.strXCTransID, tblTransactions.strXCAuthCode";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "CREATE VIEW qryMerchantGiftSummary AS " +
                                    "SELECT Count(tblGift.lngGiftID) AS CountOflngGiftID, " +
                                        "Sum(tblGift.curAmount) AS curTotAmt, " +
                                        "Min(tblGift.dteGiftDate) AS dteGiftDate, " +
                                        "tblGift.strXCTransID, tblGift.strXCAuthCode " +
                                    "FROM tblGift " +
                                    "WHERE (IIf([tblGift].[strXCTransID] Is Null, '', [tblGift].[strXCTransID]) <> '' OR " +
                                        "IIf([tblGift].[strXCAuthCode] Is Null, '', [tblGift].[strXCAuthCode]) <> '') AND " +
                                        "DateDiff('d', #" + _dteStart.ToShortDateString() + "#, [tblGift].[dteGiftDate]) >=0 AND " +
                                        "DateDiff('d', [tblGift].[dteGiftDate], #" + _dteEnd.ToShortDateString() + "#) >= 0 " +
                                    "GROUP BY tblGift.strXCTransID, tblGift.strXCAuthCode";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "DELETE tblMerchantGatewayTrans.* " +
                                "FROM tblMerchantGatewayTrans " +
                                "WHERE tblMerchantGatewayTrans.lngGWTransID IN " +
                                    "(" +
                                        "SELECT tblMerchantGatewayTrans.lngGWTransID " +
                                        "FROM ((((tblMerchantGatewayTrans " +
                                            "LEFT JOIN qryMerchantGiftSummary AS qryMerchantGiftSummary_Local ON (tblMerchantGatewayTrans.strMerchantRef = qryMerchantGiftSummary_Local.strXCAuthCode) AND " +
                                                "(tblMerchantGatewayTrans.decAmount = qryMerchantGiftSummary_Local.curTotAmt)) " +
                                            "LEFT JOIN qryMerchantTransSummary AS qryMerchantTransSummary_Local ON (tblMerchantGatewayTrans.strMerchantRef = qryMerchantTransSummary_Local.strXCAuthCode) AND " +
                                                "(tblMerchantGatewayTrans.decAmount = qryMerchantTransSummary_Local.curTotAmt)) " +
                                            "LEFT JOIN qryMerchantGiftSummary AS qryMerchantGiftSummary_Web ON (tblMerchantGatewayTrans.strMerchantRef = qryMerchantGiftSummary_Web.strXCTransID) AND " +
                                                "(tblMerchantGatewayTrans.decAmount = qryMerchantGiftSummary_Web.curTotAmt)) " +
                                            "LEFT JOIN qryMerchantTransSummary AS qryMerchantTransSummary_Web ON (tblMerchantGatewayTrans.strMerchantRef = qryMerchantTransSummary_Web.strXCTransID) AND " +
                                                "(tblMerchantGatewayTrans.decAmount = qryMerchantTransSummary_Web.curTotAmt)) " +
                                            "LEFT JOIN (SELECT IIf(IsNull([qryMerchantTransSummary].[CountOflngTransactionID]), 0, [qryMerchantTransSummary].[CountOflngTransactionID]) + IIf(IsNull([qryMerchantGiftSummary].[CountOflngGiftID]), 0, [qryMerchantGiftSummary].[CountOflngGiftID]) AS TransCount, IIf(IsNull([qryMerchantTransSummary].[curTotAmt]), 0, [qryMerchantTransSummary].[curTotAmt]) + IIf(IsNull([qryMerchantGiftSummary].[curTotAmt]), 0, [qryMerchantGiftSummary].[curTotAmt]) AS curTotalAmt, qryMerchantGiftSummary.strXCTransID " +
                                                "FROM qryMerchantGiftSummary " +
                                                    "INNER JOIN qryMerchantTransSummary ON qryMerchantGiftSummary.strXCTransID = qryMerchantTransSummary.strXCTransID) AS sqryCombinedTrans ON (tblMerchantGatewayTrans.strMerchantRef = sqryCombinedTrans.strXCTransID) AND " +
                                                "(tblMerchantGatewayTrans.decAmount = sqryCombinedTrans.curTotalAmt) " +
                                        "WHERE (((qryMerchantTransSummary_Local.CountOflngTransactionID) Is Not Null)) OR " +
                                            "(((qryMerchantTransSummary_Web.CountOflngTransactionID) Is Not Null)) OR " +
                                            "(((qryMerchantGiftSummary_Local.CountOflngGiftID) Is Not Null)) OR " +
                                            "(((qryMerchantGiftSummary_Web.CountOflngGiftID) Is Not Null)) OR " +
                                            "(((IIf(IsNull(sqryCombinedTrans.TransCount), 0, sqryCombinedTrans.TransCount)) > 0)) " +
                                    ")";
                        
                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch (Exception ex) { }
                    }

                    conDB.Close();
                }
            }

            return blnRes;
        }

        private bool fcnAddXCTrans(DataTable _dtRes)
        {
            bool blnRes = false;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strSQL = "";

                strSQL = "INSERT INTO tblMerchantGatewayTrans " +
                        "( lngGWTransID, " +
                            "dteTransDate, " +
                            "decAmount, " +
                            "strFName, strLName, strAddress, strCity, strState, strZip, strCountry, strEmail, strCTID, strStatus, strLastFour, strMerchantRef, strSource ) " +
                        "VALUES " +
                        "(@lngGWTransID, " +
                            "@dteTransDate, " +
                            "@decAmount, " +
                            "@strFName, @strLName, @strAddress, @strCity, @strState, @strZip, @strCountry, @strEmail, @strCTID, @strStatus, @strLastFour, @strMerchantRef, @strSource )";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    long lngGWTransID = 0;

                    DateTime dteTransDate = DateTime.MinValue;
                    decimal decAmount = 0;

                    string strFName = "";
                    string strLName = "";
                    string strAddress = "";
                    string strCity = "";
                    string strState = "";
                    string strZip = "";
                    string strCountry = "";
                    string strEmail = "";
                    string strCTID = "";
                    string strStatus = "";
                    string strLastFour = "";
                    string strMerchantRef = "";
                    string strSource = "";

                    for (int intRow = 0; intRow < _dtRes.Rows.Count; intRow++)
                    {
                        DataRow rowCurrent = _dtRes.Rows[intRow];

                        lngGWTransID = Convert.ToInt64(rowCurrent["Transaction ID"]);
                        dteTransDate = Convert.ToDateTime(rowCurrent["Date"] + " " + rowCurrent["Time"]);
                        decAmount = Convert.ToDecimal(rowCurrent["Amount"]);
                        strFName = "NA";
                        strLName = "";
                        strAddress = "NA";
                        strCity = "NA";
                        strState = "";
                        strZip = "";
                        strCountry = "NA";
                        strEmail = "NA";
                        strCTID = Convert.ToString(rowCurrent["Invoice Number"]);
                        strStatus = Convert.ToString(rowCurrent["Response Description"]);
                        strLastFour = Convert.ToString(rowCurrent["Account Number"]);

                        if (strLastFour.Length > 4) strLastFour = strLastFour.Substring(strLastFour.Length - 4, 4);

                        if (Convert.ToString(rowCurrent["User ID"]).Length > 0)
                        {
                            strSource = "Local App";
                            strMerchantRef = Convert.ToString(rowCurrent["Approval Code"]);
                        }
                        else
                        {
                            strMerchantRef = Convert.ToString(rowCurrent["Transaction ID"]);
                            strSource = "Web Module";
                        }

                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@lngGWTransID", lngGWTransID);
                        cmdDB.Parameters.AddWithValue("@dteTransDate", dteTransDate);
                        cmdDB.Parameters[1].OleDbType = OleDbType.DBDate;
                        cmdDB.Parameters.AddWithValue("@decAmount", decAmount);
                        cmdDB.Parameters.AddWithValue("@strFName", strFName);
                        cmdDB.Parameters.AddWithValue("@strLName", strLName);
                        cmdDB.Parameters.AddWithValue("@strAddress", strAddress);
                        cmdDB.Parameters.AddWithValue("@strCity", strCity);
                        cmdDB.Parameters.AddWithValue("@strState", strState);
                        cmdDB.Parameters.AddWithValue("@strZip", strZip);
                        cmdDB.Parameters.AddWithValue("@strCountry", strCountry);
                        cmdDB.Parameters.AddWithValue("@strEmail", strEmail);
                        cmdDB.Parameters.AddWithValue("@strCTID", strCTID);
                        cmdDB.Parameters.AddWithValue("@strStatus", strStatus);
                        cmdDB.Parameters.AddWithValue("@strLastFour", strLastFour);
                        cmdDB.Parameters.AddWithValue("@strMerchantRef", strMerchantRef);

                        cmdDB.Parameters.AddWithValue("@strSource", strSource);

                        cmdDB.ExecuteNonQuery();
                    }

                    blnRes = true;
                }

                conDB.Close();
            }

            return blnRes;
        }

        private bool fcnGetEPSTrans(DateTime _dteStart, DateTime _dteEnd)
        {
            bool blnRes = false;

            //Get TransactionSetupID from EPS
            try
            {
                wsEPSReporting.Express EPSExpress = new global::CTWebMgmt.wsEPSReporting.Express();

                wsEPSReporting.Credentials EPSCred = new global::CTWebMgmt.wsEPSReporting.Credentials();
                wsEPSReporting.Application EPSApp = new global::CTWebMgmt.wsEPSReporting.Application();
                wsEPSReporting.Parameters EPSParams = new global::CTWebMgmt.wsEPSReporting.Parameters();

                EPSParams.TransactionDateTimeBegin = _dteStart.ToString("yyyy-MM-dd HH:mm:ss.fff");
                EPSParams.TransactionDateTimeEnd = _dteEnd.ToString("yyyy-MM-dd HH:mm:ss.fff");

  //              [yyyy-MM-dd HH:mm:ss.fff
//
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

                wsEPSReporting.Response EPSResponse = EPSExpress.TransactionQuery(EPSCred, EPSApp, EPSParams, null);

                //evaluate response...
                if (EPSResponse.ExpressResponseCode == "0")
                {
                    string strRes = "";

                    strRes = EPSResponse.ReportingData;

                    blnRes = fcnAddEPSTrans(strRes);

                    //eliminate matched transactions from data set if needed
                    if (radUnmatched.Checked)
                    {
                        string strSQL = "";

                        using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                        {
                            conDB.Open();

                            strSQL = "DROP VIEW qryMerchantTransSummary";

                            using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                            {
                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "DROP VIEW qryMerchantGiftSummary";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "CREATE VIEW qryMerchantTransSummary AS " +
                                            "SELECT Count(tblTransactions.lngTransactionID) AS CountOflngTransactionID, " +
                                                "Sum(tblTransactions.curPayment) AS curTotAmt, " +
                                                "Min(tblTransactions.dteDateAdded) AS dteDateAdded, " +
                                                "tblTransactions.strEPSTransID " +
                                            "FROM tblTransactions " +
                                            "WHERE IIf([tblTransactions].[strEPSTransID] Is Null, '', [tblTransactions].[strEPSTransID]) <>'' AND " +
                                                "DateDiff('d', #" + _dteStart.ToShortDateString() + "#, [tblTransactions].[dteDateAdded]) >= 0 AND " +
                                                "DateDiff('d', [tblTransactions].[dteDateAdded], #" + _dteEnd.ToShortDateString() + "#) >=0 " +
                                            "GROUP BY tblTransactions.strEPSTransID";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "CREATE VIEW qryMerchantGiftSummary AS " +
                                            "SELECT Count(tblGift.lngGiftID) AS CountOflngGiftID, " +
                                                "Sum(tblGift.curAmount) AS curTotAmt, " +
                                                "Min(tblGift.dteGiftDate) AS dteGiftDate, " +
                                                "tblGift.strEPSTransID " +
                                            "FROM tblGift " +
                                            "WHERE DateDiff('d', #" + _dteStart.ToShortDateString() + "#, [tblGift].[dteGiftDate]) >=0 AND " +
                                                "DateDiff('d', [tblGift].[dteGiftDate], #" + _dteEnd.ToShortDateString() + "#) >= 0 AND " +
                                                "IIf([tblGift].[strEPSTransID] Is Null, '', [tblGift].[strEPSTransID]) <> '' " +
                                            "GROUP BY tblGift.strEPSTransID";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                strSQL = "DELETE tblMerchantGatewayTrans.* " +
                                        "FROM tblMerchantGatewayTrans " +
                                        "WHERE tblMerchantGatewayTrans.lngGWTransID In " +
                                            "(" +
                                                "SELECT tblMerchantGatewayTrans.lngGWTransID " +
                                                "FROM (tblMerchantGatewayTrans " +
                                                    "LEFT JOIN qryMerchantGiftSummary ON (tblMerchantGatewayTrans.strMerchantRef = qryMerchantGiftSummary.strEPSTransID) AND " +
                                                        "(tblMerchantGatewayTrans.decAmount = qryMerchantGiftSummary.curTotAmt)) " +
                                                    "LEFT JOIN qryMerchantTransSummary ON (tblMerchantGatewayTrans.decAmount = qryMerchantTransSummary.curTotAmt) AND " +
                                                        "(tblMerchantGatewayTrans.strMerchantRef = qryMerchantTransSummary.strEPSTransID) " +
                                                "WHERE qryMerchantGiftSummary.CountOflngGiftID Is Not Null OR " +
                                                    "qryMerchantTransSummary.CountOflngTransactionID Is Not Null " +
                                            ")";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }
                            }

                            conDB.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("EPS Response: " + EPSResponse.ExpressResponseMessage);
                }
            }
            catch (Exception ex)
            {
                blnRes = false;
            }

            return blnRes;
        }

        private bool fcnAddEPSTrans(string _strXML)
        {
            bool blnRes = false;

            using (DataSet dsRes = new DataSet())
            {
                dsRes.ReadXml(new System.IO.StringReader(_strXML));

                using (DataTable dtRes = dsRes.Tables[0])
                {
                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        string strSQL = "";

                        strSQL = "INSERT INTO tblMerchantGatewayTrans " +
                                "( lngGWTransID, " +
                                    "dteTransDate, " +
                                    "decAmount, " +
                                    "strFName, strLName, strAddress, strCity, strState, strZip, strCountry, strEmail, strCTID, strStatus, strLastFour, strMerchantRef, strSource ) " +
                                "VALUES " +
                                "(@lngGWTransID, " +
                                    "@dteTransDate, " +
                                    "@decAmount, " +
                                    "@strFName, @strLName, @strAddress, @strCity, @strState, @strZip, @strCountry, @strEmail, @strCTID, @strStatus, @strLastFour, @strMerchantRef, @strSource )";

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            long lngGWTransID = 0;

                            DateTime dteTransDate = DateTime.MinValue;
                            decimal decAmount = 0;

                            string strFName = "";
                            string strLName = "";
                            string strAddress = "";
                            string strCity = "";
                            string strState = "";
                            string strZip = "";
                            string strCountry = "";
                            string strEmail = "";
                            string strCTID = "";
                            string strStatus = "";
                            string strLastFour = "";
                            string strMerchantRef = "";
                            string strTerminalID = "";
                            string strSource = "";

                            string strResponseCode = "";

                            for (int intRow = 0; intRow < dtRes.Rows.Count; intRow++)
                            {
                                DataRow rowCurrent = dtRes.Rows[intRow];

                                try { strResponseCode = Convert.ToString(rowCurrent["ExpressResponseCode"]); }
                                catch { }

                                if (strResponseCode == "0")
                                {
                                    lngGWTransID = Convert.ToInt64(rowCurrent["TransactionID"]);
                                    dteTransDate = Convert.ToDateTime(rowCurrent["TimeStamp"]);
                                    decAmount = Convert.ToDecimal(rowCurrent["TransactionAmount"]);
                                    strFName = "NA";
                                    strLName = "";
                                    strAddress = "NA";
                                    strCity = "NA";
                                    strState = "";
                                    strZip = "";
                                    strCountry = "";
                                    strEmail = "";
                                    strCTID = Convert.ToString(rowCurrent["ReferenceNumber"]);
                                    strStatus = Convert.ToString(rowCurrent["TransactionStatus"]);

                                    try { strLastFour = Convert.ToString(rowCurrent["CardNumberMasked"]).Substring(Convert.ToString(rowCurrent["CardNumberMasked"]).Length - 4, 4); }
                                    catch { strLastFour = ""; }

                                    strMerchantRef = Convert.ToString(rowCurrent["TransactionID"]);

                                    strTerminalID = Convert.ToString(rowCurrent["TerminalID"]);

                                    if (strTerminalID == "0001")
                                        strSource = "Web Module";
                                    else
                                        strSource = "Local App";

                                    cmdDB.Parameters.Clear();

                                    cmdDB.Parameters.AddWithValue("@lngGWTransID", lngGWTransID);
                                    cmdDB.Parameters.AddWithValue("@dteTransDate", dteTransDate);
                                    cmdDB.Parameters[1].OleDbType = OleDbType.DBDate;
                                    cmdDB.Parameters.AddWithValue("@decAmount", decAmount);
                                    cmdDB.Parameters.AddWithValue("@strFName", strFName);
                                    cmdDB.Parameters.AddWithValue("@strLName", strLName);
                                    cmdDB.Parameters.AddWithValue("@strAddress", strAddress);
                                    cmdDB.Parameters.AddWithValue("@strCity", strCity);
                                    cmdDB.Parameters.AddWithValue("@strState", strState);
                                    cmdDB.Parameters.AddWithValue("@strZip", strZip);
                                    cmdDB.Parameters.AddWithValue("@strCountry", strCountry);
                                    cmdDB.Parameters.AddWithValue("@strEmail", strEmail);
                                    cmdDB.Parameters.AddWithValue("@strCTID", strCTID);
                                    cmdDB.Parameters.AddWithValue("@strStatus", strStatus);
                                    cmdDB.Parameters.AddWithValue("@strLastFour", strLastFour);
                                    cmdDB.Parameters.AddWithValue("@strMerchantRef", strMerchantRef);
                                    cmdDB.Parameters.AddWithValue("@strSource", strSource);

                                    cmdDB.ExecuteNonQuery();
                                }
                            }

                            blnRes = true;
                        }

                        conDB.Close();
                    }
                }
            }

            return blnRes;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmReconcileMerchantGatewaySetup_Load(object sender, EventArgs e)
        {
            DateTime dteStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dteEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).Subtract(new TimeSpan(1, 0, 0, 0));

            dtpStart.Value = dteStart;
            dtpEnd.Value = dteEnd;

            radAllCTTrans.Checked = true;
        }
    }
}