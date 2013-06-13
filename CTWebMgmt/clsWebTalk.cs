using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using CTWebMgmt.Properties;
using System.Xml;
using System.IO;

namespace CTWebMgmt
{
    class clsWebTalk
    {
        public static string strWebConn = "Data Source=localhost;Initial Catalog=CTWeb;User Id=EventULDL;Password=652NBnUD;";
        //public static string strWebConn = "Data Source=KINGTUBBY\\SQLEXPRESS;Initial Catalog=CTWeb;User Id=CTWebUser;Password=ctwebp;";
        //public static string strWebConn = "Data Source=BUNNYLEE\\SQLEXPRESS;Initial Catalog=CTWeb;User Id=EventULDL;Password=652NBnUD;";

        public static string strPOSTFileDir = "C:\\inetpub\\wwwroot\\camptrak.com\\fileuls\\";
        //public static string strPOSTFileURI = "http://localhost/XferEventInfo/GetFile.aspx";
        public static string strPOSTFileURI = "https://www.camptrak.com/XferEventInfo/GetFile.aspx";

        public static string fcnBuildSQL(string strSQL, string strGroupBy, RadioButton _radSpecificEvent, ComboBox _cboSpecificEvent, TextBox _txtStartDate, TextBox _txtEndDate, ComboBox _cboProgram)
        {
            try
            {
                strSQL = strSQL + "WHERE tlkpCampName.blnUpload=True AND " +
                                    "tlkpCampName.lngProgramTypeID=" + (int)clsGlobalEnum.conPROGRAMTYPE.GroupEvent + " ";

                if (_radSpecificEvent.Checked) strSQL = strSQL + "AND tblGGCC.lngGGCCID=" + ((clsCboItem)_cboSpecificEvent.SelectedItem).ID + " ";

                if (_txtStartDate.Text != "")
                {
                    DateTime dteStart;
                    DateTime dteEnd;

                    DateTime.TryParse(_txtStartDate.Text, out dteStart);
                    DateTime.TryParse(_txtEndDate.Text, out dteEnd);

                    strSQL = strSQL + "AND DateDiff('d', #" + dteStart.ToString("MM/dd/yyyy") + "#, tblGGCC.dteStartDate)>=0 AND DateDiff('d', tblGGCC.dteStartDate, #" + dteEnd.ToString("MM/dd/yyyy") + "#)>=0 ";
                }

                if (_cboProgram.SelectedIndex >= 0) strSQL = strSQL + "AND tblGGCC.lngProgramTypeID=" + ((clsCboItem)_cboProgram.SelectedItem).ID + " ";

                strSQL = strSQL + strGroupBy;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmUploadEvents.fcnBuildSQL", ex);
            }

            return strSQL;
        }

        public static string fcnUploadData(string strSQL, string strTblName, string strKeyFld, string strFldToUpdate, string strInsertSProc, bool _blnClearAll)
        {
            string strRes = "";

            strRes = fcnUploadData(strSQL, strTblName, strKeyFld, strFldToUpdate, strInsertSProc, _blnClearAll, "long", "records");

            return strRes;
        }

        public static string fcnUploadData(string strSQL, string strTblName, string strKeyFld, string strFldToUpdate, string strInsertSProc, bool _blnClearAll, string _strRecordCaption)
        {
            string strRes = "";

            strRes = fcnUploadData(strSQL, strTblName, strKeyFld, strFldToUpdate, strInsertSProc, _blnClearAll, "long", _strRecordCaption);

            return strRes;
        }

        public static string fcnUploadData(string strSQL, string strTblName, string strKeyFld, string strFldToUpdate, string strInsertSProc, bool _blnClearAll, string _strKeyType, string _strRecordCaption)
        {
            string strULFile;
            string strXML;
            string strWebRes = "";
            string strRes = "";

            //wsULDLEventInfo.Service wsULEventInfo;

            wsXferEventInfo.XferEventInfo wsXferData;

            try
            {
                if (strSQL == "") return "";

                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    objConn.Open();

                    using (OleDbDataAdapter daToUL = new OleDbDataAdapter(strSQL, objConn))
                    {
                        using (DataSet dsToUL = new DataSet(strTblName))
                        {
                            daToUL.Fill(dsToUL);

                            strULFile = Application.StartupPath + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + strTblName + ".xml";

                            if (dsToUL.Tables[0].Rows.Count == 0)
                                strRes = "There are no " + _strRecordCaption + " to upload.\n";
                            else
                            {
                                dsToUL.WriteXml(strULFile);

                                strXML = CTWebMgmt.fcnFileToStr(strULFile);

                                if (strInsertSProc == "spAppendGift") strXML = strXML.Replace("<blnRetrieved>-1</blnRetrieved>", "<blnRetrieved>true</blnRetrieved>");

                                wsXferData = new wsXferEventInfo.XferEventInfo();

                                wsXferData.Timeout = System.Threading.Timeout.Infinite;

                                strWebRes = wsXferData.fcnProcessCTXMLUpload(clsAppSettings.GetAppSettings().lngCTUserID, strXML, strInsertSProc, strKeyFld, strFldToUpdate, _blnClearAll, strWebConn, _strKeyType);
                            }
                        }
                    }

                    objConn.Close();
                }

                if (strFldToUpdate != "" && strWebRes != "" && strFldToUpdate != "lngCTUserIDOut")
                    subLoadWebIDs(strWebRes, strKeyFld, strFldToUpdate, strTblName);

                System.IO.File.Delete(strULFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + DateTime.Now.ToString());
                MessageBox.Show("Debug Message: " + strWebRes);
                clsErr.subLogErr("clsWebTalk.subUploadData", ex);
            }
            return strRes;
        }

        public static string fcnULWithUpdate(string _strSQL, string _strTblName, string _strLocalKeyFld, string _strServerKeyFld, bool _blnClearAll, string _strRecordCaption)
        {
            string strRes = "";

            strRes = fcnULWithUpdate(_strSQL, _strTblName, _strLocalKeyFld, _strServerKeyFld, _blnClearAll, _strRecordCaption, null);

            return strRes;
        }

        public static string fcnULWithUpdate(string _strSQL, string _strTblName, string _strLocalKeyFld, string _strServerKeyFld, bool _blnClearAll, string _strRecordCaption, ListBox _lstStatus)
        {
            string strWebRes = "";
            string strRes = "";

            XmlNode xmlNodeRes;

            try
            {
                if (_strSQL == "") return "";

                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    objConn.Open();

                    using (OleDbDataAdapter daToUL = new OleDbDataAdapter(_strSQL, objConn))
                    {
                        int intTotalRecords = 0;
                        int intStartIndex = 0;
                        int intEndIndex = 1000;

                        int intRemainingRecords = 0;
                        int intCurrentCycle = 1;

                        bool blnFirst = true;

                        while (intRemainingRecords > 0 || blnFirst)
                        {
                            using (DataSet dsToUL = new DataSet(_strTblName))
                            {
                                daToUL.Fill(dsToUL);

                                XmlDataDocument xmlChunk = new XmlDataDocument(dsToUL);

                                if (xmlChunk.ChildNodes.Count > 0)
                                {
                                    if (blnFirst)
                                    {
                                        try { intTotalRecords = xmlChunk.ChildNodes[0].ChildNodes.Count; }
                                        catch { intTotalRecords = 0; }

                                        intRemainingRecords = intTotalRecords;

                                        if (_lstStatus != null)
                                            _lstStatus.Items.Insert(0, "Queueing " + intTotalRecords.ToString() + " " + _strRecordCaption + " for upload");
                                        Application.DoEvents();

                                        if (intEndIndex > intTotalRecords) intEndIndex = intTotalRecords;

                                        if (_lstStatus != null)
                                            _lstStatus.Items.Insert(0, "Uploading " + _strRecordCaption + " " + intStartIndex.ToString() + "-" + intEndIndex.ToString());
                                        Application.DoEvents();

                                        blnFirst = false;
                                    }

                                    //remove all but current k
                                    dsToUL.EnforceConstraints = false;

                                    //clear records after current k
                                    while (xmlChunk.ChildNodes[0].ChildNodes.Count > (1000 * intCurrentCycle))
                                    {
                                        xmlChunk.ChildNodes[0].ChildNodes[(1000 * intCurrentCycle)].ParentNode.RemoveChild(xmlChunk.ChildNodes[0].ChildNodes[(1000 * intCurrentCycle)]);
                                    }

                                    //clear record before current k
                                    while (xmlChunk.ChildNodes[0].ChildNodes.Count > 1000)
                                    {
                                        xmlChunk.ChildNodes[0].ChildNodes[0].ParentNode.RemoveChild(xmlChunk.ChildNodes[0].ChildNodes[0]);
                                    }

                                    using (wsXferEventInfo.XferEventInfo wsXferData = new wsXferEventInfo.XferEventInfo())
                                    {
                                        wsXferData.Timeout = System.Threading.Timeout.Infinite;

                                        //dsToUL.WriteXml("D:\\projects\\camptrak\\covheightsul.xml");

                                        //int intNodeCount = 0;

                                        //for (int intI = 0; intI < xmlDocToSend.ChildNodes[0].ChildNodes.Count; intI++)
                                        //{
                                        //    if (intNodeCount == 0) intNodeCount = xmlDocToSend.ChildNodes[0].ChildNodes[intI].ChildNodes.Count;

                                        //    if (xmlDocToSend.ChildNodes[0].ChildNodes[intI].ChildNodes.Count != intNodeCount)
                                        //    {
                                        //        Console.WriteLine("Count Err");
                                        //    }
                                        //}

                                        int intHourOffset = 0;

                                        intHourOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;

                                        xmlNodeRes = wsXferData.fcnUploadData(xmlChunk, _strTblName, _strServerKeyFld, _strLocalKeyFld, strWebConn, intHourOffset);
                                    }

                                    //load result
                                    if (_strServerKeyFld != "")
                                        subLoadWebIDs(xmlNodeRes, _strTblName);

                                    intRemainingRecords -= 1000;
                                    intCurrentCycle++;

                                    intStartIndex += 1000;
                                    intEndIndex += 1000;

                                    if (intEndIndex > intTotalRecords) intEndIndex = intTotalRecords;

                                    if (intEndIndex > intStartIndex)
                                    {
                                        if (_lstStatus != null)
                                            _lstStatus.Items.Insert(0, "Uploading " + _strRecordCaption + " " + intStartIndex.ToString() + "-" + intEndIndex.ToString());
                                        Application.DoEvents();
                                    }
                                }
                                else
                                    blnFirst = false;
                            }
                        }
                    }

                    objConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + DateTime.Now.ToString());
              //  MessageBox.Show("Debug Message: " + strWebRes);
                clsErr.subLogErr("clsWebTalk.subUploadData", ex);
            }
            return strRes;
        }

        public static string fcnULNoUpdate(string _strSQL, string _strTblName, string _strRecordCaption)
        {
            string strWebRes = "";
            string strRes = "";

            XmlNode xmlNodeRes;

            try
            {
                if (_strSQL == "") return "";

                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    objConn.Open();

                    using (OleDbDataAdapter daToUL = new OleDbDataAdapter(_strSQL, objConn))
                    {
                        int intTotalRecords = 0;
                        int intStartIndex = 0;
                        int intEndIndex = 1000;

                        int intRemainingRecords = 0;
                        int intCurrentCycle = 1;

                        bool blnFirst = true;

                        while (intRemainingRecords > 0 || blnFirst)
                        {
                            using (DataSet dsToUL = new DataSet(_strTblName))
                            {
                                daToUL.Fill(dsToUL);

                                XmlDataDocument xmlChunk = new XmlDataDocument(dsToUL);

                                if (xmlChunk.ChildNodes.Count > 0)
                                {
                                    if (blnFirst)
                                    {
                                        try { intTotalRecords = xmlChunk.ChildNodes[0].ChildNodes.Count; }
                                        catch { intTotalRecords = 0; }

                                        intRemainingRecords = intTotalRecords;

                                        if (intEndIndex > intTotalRecords) intEndIndex = intTotalRecords;

                                        blnFirst = false;
                                    }

                                    //remove all but current k
                                    dsToUL.EnforceConstraints = false;

                                    //clear records after current k
                                    while (xmlChunk.ChildNodes[0].ChildNodes.Count > (1000 * intCurrentCycle))
                                    {
                                        xmlChunk.ChildNodes[0].ChildNodes[(1000 * intCurrentCycle)].ParentNode.RemoveChild(xmlChunk.ChildNodes[0].ChildNodes[(1000 * intCurrentCycle)]);
                                    }

                                    //clear record before current k
                                    while (xmlChunk.ChildNodes[0].ChildNodes.Count > 1000)
                                    {
                                        xmlChunk.ChildNodes[0].ChildNodes[0].ParentNode.RemoveChild(xmlChunk.ChildNodes[0].ChildNodes[0]);
                                    }

                                    using (wsXferEventInfo.XferEventInfo wsXferData = new wsXferEventInfo.XferEventInfo())
                                    {
                                        wsXferData.Timeout = System.Threading.Timeout.Infinite;

                                        int intHourOffset = 0;

                                        intHourOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;

                                        xmlNodeRes = wsXferData.fcnUploadDataNoUpdate(xmlChunk, _strTblName, strWebConn, intHourOffset, clsAppSettings.GetAppSettings().lngCTUserID);
                                    }

                                    intRemainingRecords -= 1000;
                                    intCurrentCycle++;

                                    intStartIndex += 1000;
                                    intEndIndex += 1000;

                                    if (intEndIndex > intTotalRecords) intEndIndex = intTotalRecords;
                                }
                                else
                                    blnFirst = false;
                            }
                        }
                    }

                    objConn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + DateTime.Now.ToString());
                MessageBox.Show("Debug Message: " + strWebRes);
                clsErr.subLogErr("clsWebTalk.subUploadData", ex);
            }
            return strRes;
        }

        private static void subULCachedDS()
        {
        }

        private static void subLoadWebIDs(XmlNode _xmlToUpdate, string _strTbl)
        {
            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        //Loop through the nodes
                        foreach (XmlNode xmlNode in _xmlToUpdate.ChildNodes)
                        {
                            string strServerFld = xmlNode.ChildNodes[0].Name;
                            string strServerVal = xmlNode.ChildNodes[0].InnerText;

                            string strLocalFld = xmlNode.ChildNodes[1].Name;
                            string strLocalVal = xmlNode.ChildNodes[1].InnerText;

                            string strSQL = "";

                            try
                            {
                                strSQL = "UPDATE " + _strTbl + " " +
                                        "SET " + strServerFld + "=@" + strServerFld + " " +
                                        "WHERE " + strLocalFld + "=@" + strLocalFld;

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.Add(new OleDbParameter("@" + strServerFld, strServerVal));
                                cmdDB.Parameters.Add(new OleDbParameter("@" + strLocalFld, strLocalVal));

                                cmdDB.ExecuteNonQuery();
                            }
                            catch { }
                            //execute sql here
                        }
                    }
                }
            }
            catch { }

            return;
        }

        private static void subLoadWebIDs(string strXML, string strKeyFld, string strFldToUpdate, string strTbl)
        {
            OleDbConnection objConn;
            OleDbCommand objCommand;

            XmlDocument xmlIn;
            XmlNodeList xmlNodes;


            if (strKeyFld == "lngAdvRateTypeID")
            {
                Console.WriteLine("AdvRateTypes");
            }
            string strTblName;
            string strSQL = "";
            string strFld;
            string strVal;

            long lngKeyID = 0;
            long lngUpdateID = 0;

            try
            {
                //Create the XML Document
                xmlIn = new XmlDocument();

                //Load the Xml file
                xmlIn.LoadXml(strXML);

                //Get the list of name nodes
                xmlNodes = xmlIn.SelectNodes("//Table");

                objConn = new OleDbConnection();

                objConn.ConnectionString = clsAppSettings.GetAppSettings().strCTConn;

                objConn.Open();

                objCommand = new OleDbCommand();

                objCommand.Connection = objConn;

                strTblName = xmlIn.SelectNodes("//Table").Item(0).ParentNode.Name;

                //Loop through the nodes
                foreach (XmlNode xmlNode in xmlNodes)
                {
                    for (int intI = 0; intI <= xmlNode.ChildNodes.Count - 1; intI++)
                    {
                        strFld = xmlNode.ChildNodes[intI].Name;
                        strVal = xmlNode.ChildNodes[intI].InnerText;

                        if (strFld == strKeyFld)
                            long.TryParse(strVal, out lngKeyID);
                        else if (strFld == strFldToUpdate)
                            long.TryParse(strVal, out lngUpdateID);

                    }

                    try
                    {
                        strSQL = "UPDATE " + strTbl + " " +
                                "SET " + strFldToUpdate + "=" + lngUpdateID + " " +
                                "WHERE " + strKeyFld + "=" + lngKeyID;

                        objCommand.CommandText = strSQL;

                        objCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Debug Message (strSQL): " + strSQL);
                        clsErr.subLogErr("cslWebTalk.subLoadWebIDs", ex);
                    }

                }

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Debug Message (strXML): " + strXML);
                clsErr.subLogErr("cslWebTalk.subLoadWebIDs", ex);
            }

            return;

        }

        public static string fcnWriteXMLToDB(string _strTbl, string _strXML, string _strKeyFld, bool _blnKeyTbl, string _strSuperKey)
        {
            //this function gets a table name and writes the records in the xml to the table
            //it returns xml detailing each id and it's insert status
            DataSet dsXML = new DataSet(_strTbl);

            string strSQL;
            string strDelSQL;
            string strFldNames;
            string strFldVals = "";
            string strXMLOut = "";

            long lngKeyID;
            long lngSuperKeyID;

            string strTimeAdj = "";

            //strTimeAdj = TimeZone.CurrentTimeZone;

            _strXML = _strXML.Replace("-06:00</dte", "</dte");
            _strXML = _strXML.Replace("-05:00</dte", "</dte");

            dsXML.ReadXml(new StringReader(_strXML), XmlReadMode.ReadSchema);

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        strXMLOut = "<?xml version=\"1.0\" standalone=\"yes\"?>\n" +
                                    "<ds" + _strTbl + ">";

                        foreach (DataTable tbl in dsXML.Tables)
                        {
                            //loop through each record and append it
                            foreach (DataRow row in tbl.Rows)
                            {
                                strFldVals = "";
                                strFldNames = "";

                                cmdDB.Parameters.Clear();

                                lngKeyID = 0;
                                lngSuperKeyID = 0;

                                //parse field values for insert sql
                                foreach (DataColumn col in tbl.Columns)
                                {
                                    strFldNames += col.ColumnName + ", ";
                                    strFldVals += "@" + col.ColumnName + ", ";

                                    if (col.ColumnName.Substring(0, 3) == "dte")
                                    {
                                        DateTime dteVal;

                                        try { dteVal = Convert.ToDateTime(row[col]); }
                                        catch { dteVal = DateTime.MinValue; }

                                        cmdDB.Parameters.Add(new OleDbParameter("@" + col.ColumnName, dteVal));

                                        cmdDB.Parameters[cmdDB.Parameters.Count - 1].OleDbType = OleDbType.Date;
                                    }
                                    else
                                        cmdDB.Parameters.Add(new OleDbParameter("@" + col.ColumnName, row[col]));

                                    //pull id fields for writing result xml
                                    if (col.ColumnName == _strKeyFld)
                                    {
                                        long.TryParse(row[col].ToString(), out lngKeyID);

                                        strDelSQL = "DELETE FROM " + _strTbl + " " +
                                                    "WHERE " + _strTbl + "." + _strKeyFld + "=" + lngKeyID;

                                        using (OleDbCommand cmdDel = new OleDbCommand(strDelSQL, conDB))
                                            cmdDel.ExecuteNonQuery();
                                    }

                                    if (col.ColumnName == _strSuperKey)
                                    {
                                        long.TryParse(row[col].ToString(), out lngSuperKeyID);

                                        if (_blnKeyTbl && lngSuperKeyID > 0)
                                            GGCC.clsGGCCCRUD.subDeleteProcessedReg(lngSuperKeyID);
                                    }
                                }

                                strFldVals = strFldVals.Substring(0, strFldVals.Length - 2);
                                strFldNames = strFldNames.Substring(0, strFldNames.Length - 2);

                                strSQL = "INSERT INTO " + _strTbl + " " +
                                        "( " + strFldNames + " ) " +
                                        "VALUES ( " + strFldVals + " ) ";

                                cmdDB.CommandText = strSQL;

                                try
                                {
                                    if (cmdDB.ExecuteNonQuery() > 0)
                                    {
                                        if (_strSuperKey == _strKeyFld)
                                        {
                                            strXMLOut += "<" + _strTbl + ">\n" +
                                                        "<" + _strSuperKey + ">" + lngSuperKeyID + "</" + _strSuperKey + ">\n" +
                                                          "<blnResult>true</blnResult>\n" +
                                                          "<strErr></strErr>\n" +
                                                        "</" + _strTbl + ">";
                                        }
                                        else
                                        {
                                            strXMLOut += "<" + _strTbl + ">\n" +
                                                        "<" + _strSuperKey + ">" + lngSuperKeyID + "</" + _strSuperKey + ">\n" +
                                                          "<" + _strKeyFld + ">" + lngKeyID + "</" + _strKeyFld + ">\n" +
                                                          "<blnResult>true</blnResult>\n" +
                                                          "<strErr></strErr>\n" +
                                                        "</" + _strTbl + ">";
                                        }
                                    }
                                    else
                                    {
                                        if (_strSuperKey == _strKeyFld)
                                        {
                                            strXMLOut += "<" + _strTbl + ">\n" +
                                                          "<" + _strKeyFld + ">" + lngKeyID + "</" + _strKeyFld + ">\n" +
                                                          "<strErr>No Record Added</strErr>\n" +
                                                        "</" + _strTbl + ">";
                                        }
                                        else
                                        {
                                            strXMLOut += "<" + _strTbl + ">\n" +
                                                        "<" + _strSuperKey + ">" + lngSuperKeyID + "</" + _strSuperKey + ">\n" +
                                                          "<" + _strKeyFld + ">" + lngKeyID + "</" + _strKeyFld + ">\n" +
                                                          "<strErr>No Record Added</strErr>\n" +
                                                        "</" + _strTbl + ">";
                                        }
                                    }
                                }
                                catch (Exception exAppend)
                                {
                                    clsErr.subLogErr("fcnWriteXMLToDB_Append", exAppend);
                                    MessageBox.Show("SQL: " + strSQL);
                                    
                                    strXMLOut += "<" + _strTbl + ">\n" +
                                                    "<" + _strSuperKey + ">" + lngSuperKeyID + "</" + _strSuperKey + ">\n" +
                                                                              "<" + _strKeyFld + ">" + lngKeyID + "</" + _strKeyFld + ">\n" +
                                                                              "<blnResult>false</blnResult>\n" +
                                                                              "<strErr>" + exAppend.Message + "</strErr>\n" +
                                                "</" + _strTbl + ">";

                                }

                            }
                        }

                        strXMLOut += "</ds" + _strTbl + ">";
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("fcnWriteXML (strFldVals: " + strFldVals, ex);
            }

            dsXML.Dispose();

            return strXMLOut;

        }

        public static string fcnUploadIRs(string strSQL, bool _blnClearAll)
        {
            OleDbConnection objConn;
            OleDbDataAdapter daToUL;
            DataSet dsToUL;

            string strULFile;
            string strXML;
            string strWebRes = "";
            string strRes = "";

            wsXferEventInfo.XferEventInfo wsXferData;

            try
            {
                if (strSQL == "") return "";

                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                daToUL = new OleDbDataAdapter(strSQL, objConn);

                dsToUL = new DataSet("tblRecords");

                daToUL.Fill(dsToUL);

                strULFile = Application.StartupPath + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_tblRecords.xml";

                if (dsToUL.Tables[0].Rows.Count == 0)
                    strRes = "There are no records to upload.\n";
                else
                {
                    dsToUL.WriteXml(strULFile);

                    strXML = CTWebMgmt.fcnFileToStr(strULFile);

                    wsXferData = new wsXferEventInfo.XferEventInfo();
                    wsXferData.Timeout = System.Threading.Timeout.Infinite;
                    strWebRes = wsXferData.fcnProcessIRUpload(clsAppSettings.GetAppSettings().lngCTUserID, strXML, strWebConn);
                }

                objConn.Close();

                objConn.Dispose();

                subLoadWebIDs(strWebRes, "lngRecordID", "lngRecordWebID", "tblRecords");

                try { System.IO.File.Delete(strULFile); }
                catch { }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsWebTalk.subUploadData", ex);
            }
            return strRes;
        }

        public static void subUpdateDef(string _strFldName, string _strVal, string _strDataType)
        {
            string strRes = "";

            try
            {
                using (wsXferEventInfo.XferEventInfo wsXferData = new wsXferEventInfo.XferEventInfo())
                {
                    strRes = wsXferData.fcnUpdateDefault(clsAppSettings.GetAppSettings().lngCTUserID, _strFldName, _strVal, _strDataType, strWebConn);
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsWebTalk.subUploadData", ex);
            }

            if (strRes != "") MessageBox.Show("Error updating default: " + strRes);

            return;
        }

        public static void subUpdateCustomVal(string _strFldName, string _strVal)
        {
            string strRes = "";

            try
            {
                using (wsXferEventInfo.XferEventInfo wsXferData = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                {
                    strRes = wsXferData.fcnUpdateCustomVal(clsAppSettings.GetAppSettings().lngCTUserID, _strFldName, _strVal, strWebConn);
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsWebTalk.subUpdateCustomVal", ex);
            }

            if (strRes != "") MessageBox.Show("Error updating default: " + strRes);

            return;
        }
    }
}