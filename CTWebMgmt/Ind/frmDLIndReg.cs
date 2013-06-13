using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;
using System.Data.OleDb;

using System.Xml;
using System.IO;

namespace CTWebMgmt.Ind
{
    public partial class frmDLIndReg : Form
    {
        public frmDLIndReg()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseDLIndReg();
        }

        private void btnDLIndReg_Click(object sender, EventArgs e)
        {
            //download records, registrations, and reg choices that are not marked as 'retrieved'.
            wsXferEventInfo.XferEventInfo wsDLReg;

            string strSQL;
            string[,] strAppendResXML = new string[7, 2];

            long lngCTUserID = clsAppSettings.GetAppSettings().lngCTUserID;
            long lngRegCount = 0;

            try
            {
                //get starting count of registrations
                lngRegCount = clsIndCRUD.fcnGetIRRegCount();

                wsDLReg = new wsXferEventInfo.XferEventInfo();

                //get parent records
                lstDLStatus.Items.Add("Getting new parent records");
                Application.DoEvents();

                strSQL = "SELECT tblRecords_Parents.blnGender, " +
                            "tblRecords_Parents.lngRecordWebID, tblRecords_Parents.lngRecordID, tblRecords_Parents.lngStateID, tblRecords_Parents.lngCountryID, " +
                            "tblRecords_Parents.strLastCoName, tblRecords_Parents.strFirstName, tblRecords_Parents.strCompanyName, tblRecords_Parents.strEmail, tblRecords_Parents.strPassword, tblRecords_Parents.strAddress, tblRecords_Parents.strZip, tblRecords_Parents.strWorkExt, tblRecords_Parents.strWorkPhone, tblRecords_Parents.strCellPhone, tblRecords_Parents.strCity, tblRecords_Parents.strMI, tblRecords_Parents.strCounty, tblRecords_Parents.strHomePhone, tblRecords_Parents.strSpouseFName, tblRecords_Parents.strSpouseLName, tblRecords_Parents.strReferredBy, " +
                            "tblRecords_Parents.mmoSpecialNeeds, tblRecords_Parents.mmoNotes " +
                        "FROM tblRecords tblRecords_Campers " +
                            "INNER JOIN tblRegistrations ON tblRecords_Campers.lngRecordWebID = tblRegistrations.lngRecordWebID AND " +
                                "tblRecords_Campers.lngCTUserID = tblRegistrations.lngCTUserID " +
                            "INNER JOIN tblRecords tblRecords_Parents ON tblRecords_Campers.lngCTUserID = tblRecords_Parents.lngCTUserID AND " +
                                "tblRecords_Campers.lngProfileWebID = tblRecords_Parents.lngRecordWebID " +
                        "WHERE tblRegistrations.lngCTUserID = " + lngCTUserID.ToString() + " AND " +
                            "tblRegistrations.blnRetrieved = 0 AND " +
                            "tblRegistrations.blnSubmitted = 1";

                strAppendResXML[0, 0] = wsDLReg.fcnGetRecords(strSQL, "tblRecords", clsWebTalk.strWebConn);

                //get camper records
                lstDLStatus.Items.Add("Getting new camper records");
                Application.DoEvents();

                strSQL = "SELECT tblRecords_Campers.blnGender, " +
                            "tblRecords_Campers.intGradeCompleted, tblRecords_Campers.intYearofGraduation, " +
                            "tblRecords_Campers.lngRecordWebID, tblRecords_Campers.lngRecordID, tblRecords_Campers.lngStateID, tblRecords_Campers.lngCountryID, tblRecords_Campers.lngProfileWebID, " +
                            "tblRecords_Campers.dteBirthDate, " +
                            "tblRecords_Campers.strLastCoName, tblRecords_Campers.strFirstName, tblRecords_Campers.strCompanyName, tblRecords_Campers.strEmail, tblRecords_Campers.strAddress, tblRecords_Campers.strZip, tblRecords_Campers.strWorkExt, tblRecords_Campers.strWorkPhone, tblRecords_Campers.strCellPhone, tblRecords_Campers.strCity, tblRecords_Campers.strMI, tblRecords_Campers.strCounty, tblRecords_Campers.strHomePhone, tblRecords_Campers.strSpouseFName, tblRecords_Campers.strSpouseLName, tblRecords_Campers.strSpousePhone, tblRecords_Campers.strMotherName, tblRecords_Campers.strFatherName, " +
                            "tblRecords_Campers.mmoSpecialNeeds, tblRecords_Campers.mmoNotes " +
                        "FROM tblRecords tblRecords_Campers " +
                            "INNER JOIN tblRegistrations ON tblRecords_Campers.lngRecordWebID = tblRegistrations.lngRecordWebID AND " +
                                "tblRecords_Campers.lngCTUserID = tblRegistrations.lngCTUserID " +
                        "WHERE tblRegistrations.lngCTUserID = " + lngCTUserID.ToString() + " AND " +
                            "tblRegistrations.blnRetrieved = 0 AND " +
                            "tblRegistrations.blnSubmitted = 1";

                strAppendResXML[1, 0] = wsDLReg.fcnGetRecords(strSQL, "tblRecords", clsWebTalk.strWebConn);

                //get registration records
                lstDLStatus.Items.Add("Getting new registrations");
                Application.DoEvents();

                string strDiscount = "";

                for (int intI = 1; intI <= 10; intI++)
                    strDiscount += "blnDiscount" + intI.ToString() + ", ";

                strSQL = "SELECT " + strDiscount +
                            "lngRegistrationWebID, lngRecordWebID, lngRegSourceID, lngConfMethodID, lngRegHoldID, " +
                            "curDeposit, curDonation, curSpendingMoney, " +
                            "dteRegistrationDate, dteCreated, " +
                            "strBuddy1, strBuddy2, strReleaseTo, strXCTransID, strXCAlias, strRoutingNumber, strAcctNumber, strPmtType, strCardNumber, strPNRef, strXCAuthCode, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID, strOrgCode, " +
                            "mmoRegNotes " +
                        "FROM tblRegistrations " +
                        "WHERE lngCTUserID = " + lngCTUserID.ToString() + " AND " +
                            "blnRetrieved = 0 AND " +
                            "blnSubmitted = 1";

                strAppendResXML[2, 0] = wsDLReg.fcnGetRecords(strSQL, "tblRegistrations", clsWebTalk.strWebConn);

                //get block choices
                lstDLStatus.Items.Add("Getting additional registration choices");
                Application.DoEvents();

                strSQL = "SELECT tblRegistrationBlockChoices.lngRegistrationBlockChoiceID, tblRegistrationBlockChoices.lngRegistrationWebID, tblRegistrationBlockChoices.lngBlockID, tblRegistrationBlockChoices.lngChoice " +
                        "FROM tblRegistrations " +
                            "INNER JOIN tblRegistrationBlockChoices ON tblRegistrations.lngRegistrationWebID = tblRegistrationBlockChoices.lngRegistrationWebID " +
                        "WHERE tblRegistrations.blnRetrieved = 0 AND " +
                            "tblRegistrations.lngCTUserID = " + lngCTUserID;

                strAppendResXML[3, 0] = wsDLReg.fcnGetRecords(strSQL, "tblRegistrationBlockChoices", clsWebTalk.strWebConn);

                //add records, get result xml to send to web server and update record ids
                strAppendResXML[0, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebRecords", strAppendResXML[0, 0], "lngRecordWebID", true, "lngRecordWebID");
                lstDLStatus.Items.Add("Adding new parent records");
                Application.DoEvents();

                strAppendResXML[1, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebRecords", strAppendResXML[1, 0], "lngRecordWebID", true, "lngRecordWebID");
                lstDLStatus.Items.Add("Adding new camper records");
                Application.DoEvents();

                //write web record custom field vals to new tbl
                //custom data for profile records
                strSQL = "SELECT tblCustomFieldValIR.lngRecordWebID, " +
                            "tblCustomFieldValIR.strLocalCaption, tblCustomFieldValIR.strValue " +
                        "FROM tblRecords tblRecords_Campers " +
                            "INNER JOIN tblRegistrations ON tblRecords_Campers.lngRecordWebID = tblRegistrations.lngRecordWebID AND " +
                                "tblRecords_Campers.lngCTUserID = tblRegistrations.lngCTUserID " +
                            "INNER JOIN tblRecords tblRecords_Parents ON tblRecords_Campers.lngCTUserID = tblRecords_Parents.lngCTUserID AND " +
                                "tblRecords_Campers.lngProfileWebID = tblRecords_Parents.lngRecordWebID " +
                            "INNER JOIN tblCustomFieldValIR ON tblCustomFieldValIR.lngRecordWebID = tblRecords_Parents.lngRecordWebID " +
                        "WHERE tblRegistrations.lngCTUserID = " + lngCTUserID.ToString() + " AND " +
                            "tblRegistrations.blnRetrieved = 0 AND " +
                            "tblRegistrations.blnSubmitted = 1";

                strAppendResXML[4, 0] = wsDLReg.fcnGetRecords(strSQL, "tblCustomFieldValIR", clsWebTalk.strWebConn);
                strAppendResXML[4, 1] = clsWebTalk.fcnWriteXMLToDB("tblCustomFieldValWebIR", strAppendResXML[4, 0], "", false, "");

                //custom data for camper records
                strSQL = "SELECT tblCustomFieldValIR.lngRecordWebID, " +
                            "tblCustomFieldValIR.strLocalCaption, tblCustomFieldValIR.strValue " +
                        "FROM tblCustomFieldValIR " +
                            "INNER JOIN tblRecords ON tblCustomFieldValIR.lngRecordWebID = tblRecords.lngRecordWebID " +
                            "INNER JOIN tblRegistrations ON tblRecords.lngRecordWebID = tblRegistrations.lngRecordWebID " +
                        "WHERE tblRegistrations.lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "tblRegistrations.blnRetrieved = 0 AND " +
                            "tblRegistrations.blnSubmitted = 1";

                strAppendResXML[5, 0] = wsDLReg.fcnGetRecords(strSQL, "tblCustomFieldValIR", clsWebTalk.strWebConn);
                strAppendResXML[5, 1] = clsWebTalk.fcnWriteXMLToDB("tblCustomFieldValWebIR", strAppendResXML[5, 0], "", false, "");

                strSQL = "SELECT tblCustomFieldValReg.lngRegistrationWebID, " +
                            "tblCustomFieldValReg.strLocalCaption, tblCustomFieldValReg.strValue " +
                        "FROM tblRegistrations " +
                            "INNER JOIN tblCustomFieldValReg ON tblRegistrations.lngRegistrationWebID = tblCustomFieldValReg.lngRegistrationWebID " +
                        "WHERE tblRegistrations.lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "tblRegistrations.blnRetrieved = 0 AND " +
                            "tblRegistrations.blnSubmitted = 1";

                strAppendResXML[6, 0] = wsDLReg.fcnGetRecords(strSQL, "tblCustomFieldValReg", clsWebTalk.strWebConn);
                strAppendResXML[6, 1] = clsWebTalk.fcnWriteXMLToDB("tblCustomFieldValWebReg", strAppendResXML[6, 0], "", false, "");

                strAppendResXML[2, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebIndRegistrations", strAppendResXML[2, 0], "lngRegistrationWebID", true, "lngRegistrationWebID");
                lstDLStatus.Items.Add("Adding new registrations");

                strAppendResXML[3, 1] = clsWebTalk.fcnWriteXMLToDB("tblWebIndRegBlockChoices", strAppendResXML[3, 0], "lngRegistrationBlockChoiceID", false, "lngRegistrationWebID");
                lstDLStatus.Items.Add("Adding new block choices");
                Application.DoEvents();

                subValidateProfiles(lngCTUserID);

                lstDLStatus.Items.Add("Validating custom profile data");
                Application.DoEvents();
                //subValidateCustomProfileData();
                //subCleanCustomData();

                lstDLStatus.Items.Add("Posting download results to server");
                Application.DoEvents();

                string strErr = "";

                //update dl status on server
                strErr = wsDLReg.fcnDLRes(strAppendResXML[2, 1], clsWebTalk.strWebConn);

                wsDLReg.Dispose();

                lngRegCount = clsIndCRUD.fcnGetIRRegCount() - lngRegCount;

                lstDLStatus.Items.Add("Successfully downloaded and added " + lngRegCount + " new registrations");
                Application.DoEvents();

                frmProcessIndReg objProcessIndReg = new frmProcessIndReg();

                objProcessIndReg.MdiParent = this.MdiParent;

                this.Close();

                objProcessIndReg.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("btnDL_Click", ex);
            }

            wsDLReg = null;
        }

        private void subValidateProfiles(long _lngCTUserID)
        {
            //download any missing parents of campers that were submitted through the online registration system.
            string strSQL = "";
            string strWHERE = "";
            long lngMissingProfileCount = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblWebRecords_Camper.lngProfileWebID " +
                        "FROM (tblWebIndRegistrations " +
                            "INNER JOIN tblWebRecords AS tblWebRecords_Camper ON tblWebIndRegistrations.lngRecordWebID = tblWebRecords_Camper.lngRecordWebID) LEFT JOIN tblWebRecords AS tblWebRecords_Parent ON tblWebRecords_Camper.lngProfileWebID = tblWebRecords_Parent.lngRecordWebID " +
                        "GROUP BY tblWebRecords_Camper.lngProfileWebID, tblWebRecords_Parent.lngRecordWebID " +
                        "HAVING tblWebRecords_Camper.lngProfileWebID Is Not Null AND " +
                            "tblWebRecords_Parent.lngRecordWebID Is Null";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drProfile = cmdDB.ExecuteReader())
                    {
                        while (drProfile.Read())
                        {
                            lngMissingProfileCount++;

                            long lngProfileWebID = 0;

                            try { lngProfileWebID = Convert.ToInt32(drProfile["lngProfileWebID"]); }
                            catch { lngProfileWebID = 0; }

                            if (strWHERE == "")
                                strWHERE = "WHERE tblRecords.lngRecordWebID=" + lngProfileWebID.ToString();
                            else
                                strWHERE += " OR tblRecords.lngRecordWebID=" + lngProfileWebID.ToString();
                        }
                    }
                }

                conDB.Close();
            }

            if (lngMissingProfileCount > 0)
            {
                //get parent records
                lstDLStatus.Items.Add("Getting missing parent records");
                Application.DoEvents();

                strSQL = "SELECT tblRecords.blnGender, " +
                            "tblRecords.lngRecordWebID, tblRecords.lngRecordID, tblRecords.lngStateID, tblRecords.lngCountryID, " +
                            "tblRecords.strLastCoName, tblRecords.strFirstName, tblRecords.strCompanyName, tblRecords.strEmail, tblRecords.strAddress, tblRecords.strZip, tblRecords.strWorkExt, tblRecords.strWorkPhone, tblRecords.strCellPhone, tblRecords.strCity, tblRecords.strMI, tblRecords.strCounty, tblRecords.strHomePhone, tblRecords.strSpouseFName, tblRecords.strSpouseLName, tblRecords.strReferredBy, " +
                            "tblRecords.mmoSpecialNeeds, tblRecords.mmoNotes " +
                        "FROM tblRecords " +
                        strWHERE;

                using (wsXferEventInfo.XferEventInfo wsDLProfile = new wsXferEventInfo.XferEventInfo())
                {
                    string strAppendResXML = "";

                    strAppendResXML = wsDLProfile.fcnGetRecords(strSQL, "tblRecords", clsWebTalk.strWebConn);

                    string strRes = clsWebTalk.fcnWriteXMLToDB("tblWebRecords", strAppendResXML, "lngRecordWebID", true, "lngRecordWebID");
                    lstDLStatus.Items.Add("Adding new parent records");
                    Application.DoEvents();
                }
            }
        }

        private void subValidateCustomProfileData()
        {
            //download custom data for parents of campers that were submitted through the online registration system.

            string strFile = DateTime.Now.ToString("yyyyMMdd_hhmmss") + "_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + "_ProfileIDs.xml";
            string strFileName = System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strFile;
            string strWebFile = "C:\\inetpub\\wwwroot\\camptrak.com\\fileuls\\" + strFile;
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblWebRecords.lngProfileWebID " +
                        "FROM tblWebRecords " +
                        "WHERE tblWebRecords.lngProfileWebID Is Not Null";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataAdapter daProfile = new OleDbDataAdapter(cmdDB))
                    {
                        DataSet dsProfile = new DataSet();

                        daProfile.Fill(dsProfile);

                        dsProfile.WriteXml(strFileName);
                    }
                }

                conDB.Close();
            }

            //put file            
            bool blnULRes = false;
            string strULRes = "";

            try
            {
                System.Net.WebClient cliUL = new System.Net.WebClient();

                byte[] bytULRes = cliUL.UploadFile(clsAppSettings.GetAppSettings().strPOSTFileURI, strFileName);

                // Decode and display the response.

                try { strULRes = System.Text.Encoding.ASCII.GetString(bytULRes); }
                catch { strULRes = "ERR"; }

                blnULRes = true;
            }
            catch (Exception ex)
            {
                lstDLStatus.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ": Error uploading XML file: " + ex.Message);
                Application.DoEvents();
                blnULRes = false;
            }

            if (blnULRes)
            {
                //tell web service to generate data
                using (wsXferEventInfo.XferEventInfo svc = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                {
                    strULRes = svc.fcnGetCustomData(strWebFile, clsAppSettings.GetAppSettings().strWebDBConn, clsAppSettings.GetAppSettings().lngCTUserID);
                }

                if (strULRes.EndsWith(".xml"))
                {
                    //retrieve results
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                        webClient.DownloadFile("https://www.camptrak.com/fileuls/CustomData/" + strULRes, System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strULRes);
                    
                    //insert or edit data in table
                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        using (OleDbCommand cmdDB = new OleDbCommand())
                        {
                            cmdDB.Connection = conDB;

                            Type[] dataTypes = { typeof(Record), typeof(CustomField) };

                            //serialize objres to file
                            System.Xml.Serialization.XmlSerializer xsrRes = new System.Xml.Serialization.XmlSerializer(typeof(CustomRecordData), dataTypes);

                            using (Stream reader = new FileStream(System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strULRes, FileMode.Open))
                            {
                                CustomRecordData crdFromServer;

                                crdFromServer = (CustomRecordData)xsrRes.Deserialize(reader);

                                for (int intRecord = 0; intRecord < crdFromServer.Records.Count; intRecord++)
                                {
                                    long lngRecordWebID = 0;

                                    try { lngRecordWebID = crdFromServer.Records[intRecord].RecordWebID; }
                                    catch { lngRecordWebID = 0; }

                                    if (lngRecordWebID > 0)
                                    {
                                        for (int intCustomField = 0; intCustomField < crdFromServer.Records[intRecord].CustomFields.Count; intCustomField++)
                                        {
                                            string strLocalCaption = "";
                                            string strValue = "";

                                            try { strLocalCaption = crdFromServer.Records[intRecord].CustomFields[intCustomField].LocalCaption; }
                                            catch { strLocalCaption = ""; }

                                            try { strValue = crdFromServer.Records[intRecord].CustomFields[intCustomField].CustomValue; }
                                            catch { strValue = ""; }

                                            if (strLocalCaption != "")
                                            {
                                                //delete and add value
                                                strSQL = "DELETE tblCustomFieldValWebIR.* " +
                                                        "FROM tblCustomFieldValWebIR " +
                                                        "WHERE lngRecordWebID=@lngRecordWebID AND " +
                                                            "strLocalCaption=@strLocalCaption";

                                                cmdDB.CommandText = strSQL;
                                                cmdDB.Parameters.Clear();

                                                cmdDB.Parameters.AddWithValue("@lngRecordWebID", lngRecordWebID);
                                                cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                                                try { cmdDB.ExecuteNonQuery(); }
                                                catch { }

                                                //add value
                                                strSQL = "INSERT INTO tblCustomFieldValWebIR " +
                                                        "(lngRecordWebID, " +
                                                            "strLocalCaption, strValue) " +
                                                        "VALUES " +
                                                        "(@lngRecordWebID, " +
                                                            "@strLocalCaption, @strValue)";

                                                cmdDB.CommandText = strSQL;
                                                cmdDB.Parameters.Clear();

                                                cmdDB.Parameters.AddWithValue("@lngRecordWebID", lngRecordWebID);
                                                cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                                                cmdDB.Parameters.AddWithValue("@strValue", strValue);

                                                try { cmdDB.ExecuteNonQuery(); }
                                                catch { }
                                            }
                                        }
                                    }
                                }

                                reader.Close();
                            }
                        }

                        conDB.Close();
                    }
                }
                else
                {
                    string strWebResShort = "";

                    if (strULRes.Length > 300)
                        strWebResShort = strULRes.Substring(0, 300);
                    else
                        strWebResShort = strULRes;

                    lstDLStatus.Items.Insert(0, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ": There was an error validating profile custom fields: " + strWebResShort);
                    Application.DoEvents();
                }

                if (File.Exists(System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strULRes)) File.Delete(System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strULRes);
            }

            if (File.Exists(strFileName)) File.Delete(strFileName);            
        }

        private void subCleanCustomData()
        {
            //delete duplicate custom field data
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldValWebIR.lngRecordWebID, " +
                            "tblCustomFieldValWebIR.strLocalCaption " +
                        "FROM tblCustomFieldValWebIR " +
                        "GROUP BY tblCustomFieldValWebIR.lngRecordWebID, " +
                            "tblCustomFieldValWebIR.strLocalCaption " +
                        "HAVING Count(tblCustomFieldValWebIR.lngCustomFieldValWebIRID)>1";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCust = cmdDB.ExecuteReader())
                    {
                        while (drCust.Read())
                        {
                            long lngRecordWebID = 0;
                            string strLocalCaption = "";

                            try { lngRecordWebID = Convert.ToInt32(drCust["lngRecordWebID"]); }
                            catch { lngRecordWebID = 0; }

                            try { strLocalCaption = Convert.ToString(drCust["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }

                            if (lngRecordWebID > 0)
                            {
                                strSQL = "DELETE tblCustomFieldValWebIR.* " +
                                        "FROM tblCustomFieldValWebIR " +
                                        "WHERE tblCustomFieldValWebIR.lngRecordWebID=@lngRecordWebID1 AND " +
                                            "tblCustomFieldValWebIR.strLocalCaption=@strLocalCaption1 AND " +
                                            "tblCustomFieldValWebIR.lngCustomFieldValWebIRID Not In " +
                                                "(SELECT Max(tblCustomFieldValWebIR.lngCustomFieldValWebIRID) AS MaxOflngCustomFieldValWebIRID " +
                                                "FROM tblCustomFieldValWebIR " +
                                                "GROUP BY tblCustomFieldValWebIR.lngRecordWebID, " +
                                                    "tblCustomFieldValWebIR.strLocalCaption " +
                                                "HAVING tblCustomFieldValWebIR.lngRecordWebID=@lngRecordWebID2 AND " +
                                                    "tblCustomFieldValWebIR.strLocalCaption=@strLocalCaption2)";

                                using (OleDbCommand cmdDelete = new OleDbCommand(strSQL, conDB))
                                {
                                    cmdDelete.Parameters.AddWithValue("@lngRecordWebID1", lngRecordWebID);
                                    cmdDelete.Parameters.AddWithValue("@strLocalCaption1", strLocalCaption);
                                    cmdDelete.Parameters.AddWithValue("@lngRecordWebID2", lngRecordWebID);
                                    cmdDelete.Parameters.AddWithValue("@strLocalCaption2", strLocalCaption);

                                    try { cmdDelete.ExecuteNonQuery(); }
                                    catch (Exception ex) { }
                                }
                            }
                        }

                        drCust.Close();
                    }
                }

                conDB.Close();
            }
        }

        private string fcnGetUpdateCriter(string _strOriginalXML)
        {
            string strRes = "";

            return strRes;
        }

        private void frmDLIndReg_Load(object sender, EventArgs e)
        {

        }
    }
}