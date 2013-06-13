using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml;

namespace CTWebMgmt.Ind.Setup
{
    public partial class frmDiscounts : Form
    {
        public frmDiscounts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!fcnSave())
                MessageBox.Show("There was an error saving discount definitions.");

            Close();
        }

        private bool fcnSave()
        {
            string strSQL = "";

            bool blnRes = false;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strCriter = "";

                for (int intI = 1; intI <= 10; intI++)
                    strCriter += "tblCampDefaults.curDiscAmt" + intI.ToString() + " = @curDiscAmt" + intI.ToString() + ", ";

                for (int intI = 1; intI <= 10; intI++)
                    strCriter += "tblCampDefaults.strDiscDesc" + intI.ToString() + " = @strDiscDesc" + intI.ToString() + ", ";

                strCriter = strCriter.Substring(0, strCriter.Length - 2);

                strSQL = "UPDATE tblCampDefaults " +
                        "SET " + strCriter;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    for (int intI = 1; intI <= 10; intI++)
                    {
                        decimal decAmt = 0;

                        try { decAmt = decimal.Parse(((TextBox)Controls["txtDiscountAmt" + intI.ToString()]).Text, System.Globalization.NumberStyles.Currency); }
                        catch { decAmt = 0; }

                        //cmdDB.Parameters.Add(new OleDbParameter("@curDiscAmt" + intI.ToString(), OleDbType.Currency));
                        //cmdDB.Parameters["@curDiscAmt" + intI.ToString()].Value = decAmt;
                        cmdDB.Parameters.Add(new OleDbParameter("@curDiscAmt" + intI.ToString(), decAmt));
                    }

                    for (int intI = 1; intI <= 10; intI++)
                        cmdDB.Parameters.Add(new OleDbParameter("@strDiscDesc" + intI.ToString(), ((TextBox)Controls["txtDiscount" + intI.ToString()]).Text));

                    cmdDB.ExecuteNonQuery();

                    for (int intI = 1; intI <= 10; intI++)
                    {
                        strSQL = "UPDATE tblDiscountDefs " +
                                "SET tblDiscountDefs.strPromoCode = @strPromoCode " +
                                "WHERE tblDiscountDefs.lngDiscountID=" + intI.ToString();

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.Add(new OleDbParameter("@strPromoCode", ((TextBox)Controls["txtPromoCode" + intI.ToString()]).Text));

                        cmdDB.ExecuteNonQuery();
                    }
                }

                conDB.Close();
            }

            //commit discounts to web server
            blnRes = fcnCommitDiscountsToWeb();

            return blnRes;
        }

        private bool fcnCommitDiscountsToWeb()
        {
            bool blnRes = false;

            string strFile = DateTime.Now.ToString("yyyyMMdd") + "_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + "DiscountDef.xml";
            string strFileName = System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strFile;

            string strWebFile = clsWebTalk.strPOSTFileDir + strFile;

            using (XmlWriter xmlOut = XmlWriter.Create(strFileName))
            {
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("DiscountDef");
                xmlOut.WriteAttributeString("CTUserID", clsAppSettings.GetAppSettings().lngCTUserID.ToString());

                //generate xml of definition
                for (int intI = 1; intI <= 10; intI++)
                {
                    xmlOut.WriteStartElement("Discount");
                    xmlOut.WriteElementString("DiscountID", intI.ToString());
                    xmlOut.WriteElementString("Description", ((TextBox)Controls["txtDiscount" + intI.ToString()]).Text);
                    xmlOut.WriteElementString("PromoCode", ((TextBox)Controls["txtPromoCode" + intI.ToString()]).Text);
                    xmlOut.WriteElementString("Amount", ((TextBox)Controls["txtDiscountAmt" + intI.ToString()]).Text.Replace("$", ""));
                    xmlOut.WriteElementString("Display", ((CheckBox)Controls["chkDisplayOnline" + intI.ToString()]).Checked.ToString());
                    xmlOut.WriteEndElement();
                }

                xmlOut.WriteEndElement();
                xmlOut.WriteEndDocument();
            }

            using (wsXferEventInfo.XferEventInfo xferEventInfo = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                string strWebRes = "";

                //upload file

                // Create a new WebClient instance.
                System.Net.WebClient myWebClient = new System.Net.WebClient();

                // Upload the file to the URI.
                // The 'UploadFile(uriString,fileName)' method implicitly uses HTTP POST method.

                try
                {
                    byte[] responseArray = myWebClient.UploadFile(clsWebTalk.strPOSTFileURI, strFileName);

                    // Decode and display the response.
                    string strULRes = "";

                    try { strULRes = System.Text.Encoding.ASCII.GetString(responseArray); }
                    catch { strULRes = "ERR"; }

                    strWebRes = xferEventInfo.fcnCommitDiscountDefs(strWebFile, clsWebTalk.strWebConn);
                }
                catch (System.Net.WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //upload xml definition file...web server will process it and return result.
                if (strWebRes != "")
                    MessageBox.Show("There was an error uploading the discount definitions to the web server.");
                else
                    blnRes = true;
            }

            //delete temp file
            System.IO.File.Delete(strFileName);

            return blnRes;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (fcnSave())
                MessageBox.Show("Discounts saved successfully");
            else
                MessageBox.Show("A problem was encountered while saving the discount definitions");
        }

        private void frmDiscounts_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCampDefaults.curDiscAmt1, tblCampDefaults.curDiscAmt2, tblCampDefaults.curDiscAmt3, tblCampDefaults.curDiscAmt4, tblCampDefaults.curDiscAmt5, tblCampDefaults.curDiscAmt6, tblCampDefaults.curDiscAmt7, tblCampDefaults.curDiscAmt8, tblCampDefaults.curDiscAmt9, tblCampDefaults.curDiscAmt10, " +
                            "tblCampDefaults.strDiscDesc1, tblCampDefaults.strDiscDesc2, tblCampDefaults.strDiscDesc3, tblCampDefaults.strDiscDesc4, tblCampDefaults.strDiscDesc5, tblCampDefaults.strDiscDesc6, tblCampDefaults.strDiscDesc7, tblCampDefaults.strDiscDesc8, tblCampDefaults.strDiscDesc9, tblCampDefaults.strDiscDesc10 " +
                        "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drDisc = cmdDB.ExecuteReader())
                    {
                        if (drDisc.Read())
                        {
                            for (int intI = 1; intI <= 10; intI++)
                            {
                                decimal decDiscAmt = 0;

                                try { decDiscAmt = Convert.ToDecimal(drDisc["curDiscAmt" + intI.ToString()]); }
                                catch { decDiscAmt = 0; }

                                ((TextBox)Controls["txtDiscountAmt" + intI.ToString()]).Text = decDiscAmt.ToString("C");

                                ((TextBox)Controls["txtDiscount" + intI.ToString()]).Text = Convert.ToString(drDisc["strDiscDesc" + intI.ToString()]);
                            }
                        }

                        drDisc.Close();
                    }

                    for (int intI = 1; intI <= 10; intI++)
                    {
                        strSQL = "SELECT tblDiscountDefs.lngDiscountDefID " +
                                "FROM tblDiscountDefs " +
                                "WHERE tblDiscountDefs.lngDiscountID=" + intI.ToString();

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        long lngID = 0;

                        try { lngID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { lngID = 0; }

                        if (lngID <= 0)
                        {
                            strSQL = "INSERT INTO tblDiscountDefs " +
                                    "( lngDiscountID ) " +
                                    "VALUES " +
                                    "(" + intI.ToString() + ")";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.ExecuteNonQuery();
                        }
                    }

                    strSQL = "SELECT tblDiscountDefs.lngDiscountID, " +
                                "tblDiscountDefs.strPromoCode " +
                            "FROM tblDiscountDefs";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    using (OleDbDataReader drPromo = cmdDB.ExecuteReader())
                    {
                        while (drPromo.Read())
                        {
                            int intChoice = 0;

                            try { intChoice = Convert.ToInt32(drPromo["lngDiscountID"]); }
                            catch { intChoice = 0; }

                            try { ((TextBox)Controls["txtPromoCode" + intChoice.ToString()]).Text = Convert.ToString(drPromo["strPromoCode"]); }
                            catch { }
                        }

                        drPromo.Close();
                    }
                }

                conDB.Close();
            }

            subLoadOnlineSettings();
        }

        private void subLoadOnlineSettings()
        {
            string strSQL = "";

            using (wsXferEventInfo.XferEventInfo wsDLDiscount = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                strSQL = "SELECT blnShowUsers, " +
                            "lngDiscountID " +
                        "FROM tblDiscountDefs " +
                        "WHERE lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString();

                string strWebXML = "";

                try { strWebXML = wsDLDiscount.fcnGetRecords(strSQL, "tblDiscountDefs", clsWebTalk.strWebConn); }
                catch (Exception ex) { }

                DataSet dsXML = new DataSet("tblDiscountDefs");

                dsXML.ReadXml(new System.IO.StringReader(strWebXML), XmlReadMode.ReadSchema);

                foreach (DataTable tbl in dsXML.Tables)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        bool blnShowUsers = false;
                        long lngDiscountID = 0;

                        foreach (DataColumn col in tbl.Columns)
                        {
                            string strColName = col.ColumnName;
                            string strColVal = Convert.ToString(row[col]);

                            if (strColName == "blnShowUsers")
                            {
                                try { blnShowUsers = Convert.ToBoolean(strColVal); }
                                catch { blnShowUsers = false; }
                            }
                            else if (strColName == "lngDiscountID")
                            {
                                try { lngDiscountID = Convert.ToInt32(strColVal); }
                                catch { lngDiscountID = 0; }
                            }
                        }

                        ((CheckBox)Controls["chkDisplayOnline" + lngDiscountID.ToString()]).Checked = blnShowUsers;
                    }
                }
            }
        }
    }
}