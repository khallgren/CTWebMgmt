using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.IRUtils
{
    public partial class frmReconcileIR : Form
    {
        string strWebTbl = "";

        public long lngDBID = 0;
        long lngWebID = 0;

        public frmReconcileIR(string _strWebTbl, long _lngDBID, long _lngWebID)
        {
            InitializeComponent();

            strWebTbl = _strWebTbl;
            lngDBID = _lngDBID;
            lngWebID = _lngWebID;
        }

        private void frmReconcileIR_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            subLoadCbo();

            //check caps and convert if necessary
            bool blnCapsWebProcessing = false;

            strSQL = "SELECT blnCapsWebProcessing " +
                    "FROM tblCampDefaults";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { blnCapsWebProcessing = Convert.ToBoolean(cmdDB.ExecuteScalar()); }
                    catch { blnCapsWebProcessing = false; }
                }

                conDB.Close();
            }

            if (strWebTbl == "tblWebRecords")
                subLoadCustomDefs(lngDBID, blnCapsWebProcessing);
            else
                pagCustom.Hide();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //local flds
                strSQL = "SELECT tblRecords.intGradeCompleted, " +
                            "IIf([tblRecords].[blnUseMORAddress]=True,[tlkpStates_1].[lngStateid],[tlkpStates].[lngStateid]) AS lngStateID, " +
                            "tblRecords.dteBirthDate, " +
                            "tblRecords.strFirstName, tblRecords.strMI, tblRecords.strLastCoName, tblRecords.strCompanyName, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORAddress1],[tblRecords].[strAddress]) AS strAddress, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORCity],[tblRecords].[strCity]) AS strCity, IIf([tblRecords].[blnUseMORAddress]=True,[tlkpStates_1].[strState],[tlkpStates].[strState]) AS strState, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORZip],[tblRecords].[strZip]) AS strZip, IIf([tblRecords].[blnUseMORAddress]=True,[tblMOR].[strMORPhone],[tblRecords].[strHomePhone]) AS strHomePhone, tblRecords.strWorkPhone, tblRecords.strCellPhone, tblRecords.strEmail, tblRecords.strFormalGiftSal, tblRecords.strInformalGiftSal, tblRecords.strSpouseName, tblRecords.strConfEmail, tblRecords.strMotherName, tblRecords.strFatherName  " +
                        "FROM ((tblRecords " +
                            "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                            "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                            "LEFT JOIN tlkpStates AS tlkpStates_1 ON tblMOR.lngMORStateID = tlkpStates_1.lngStateID " +
                        "WHERE tblRecords.lngRecordID=" + lngDBID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCT = cmdDB.ExecuteReader())
                    {
                        if (drCT.Read())
                        {
                            long lngStateID = 0;

                            try { lngStateID = Convert.ToInt32(drCT["lngStateID"]); }
                            catch { lngStateID = 0; }

                            //fill local flds
                            for (int intI = 0; intI < cboStateDB.Items.Count; intI++)
                            {
                                if (((clsCboItem)cboStateDB.Items[intI]).ID == lngStateID)
                                {
                                    cboStateDB.SelectedIndex = intI;
                                    break;
                                }
                            }

                            txtFNameDB.Text = Convert.ToString(drCT["strFirstName"]);
                            txtMIDB.Text = Convert.ToString(drCT["strMI"]);
                            txtLNameDB.Text = Convert.ToString(drCT["strLastCoName"]);
                            txtCompanyNameDB.Text = Convert.ToString(drCT["strCompanyName"]);
                            txtGradeDB.Text = Convert.ToString(drCT["intGradeCompleted"]);

                            DateTime dteDOB = DateTime.MinValue;

                            try { dteDOB = Convert.ToDateTime(drCT["dteBirthDate"]); }
                            catch { dteDOB = DateTime.MinValue; }

                            if (dteDOB != DateTime.MinValue) txtDOBDB.Text = dteDOB.ToString("d");
                            
                            txtAddressDB.Text = Convert.ToString(drCT["strAddress"]);
                            txtCityDB.Text = Convert.ToString(drCT["strCity"]);
                            txtZipDB.Text = Convert.ToString(drCT["strZip"]);
                            txtHomePhoneDB.Text = Convert.ToString(drCT["strHomePhone"]);
                            txtWorkPhoneDB.Text = Convert.ToString(drCT["strWorkPhone"]);
                            txtCellPhoneDB.Text = Convert.ToString(drCT["strCellPhone"]);
                            txtEMailDB.Text = Convert.ToString(drCT["strEmail"]);
                            
                            try { txtRegConfEmailDB.Text = Convert.ToString(drCT["strConfEmail"]); }
                            catch { txtRegConfEmailDB.Text = ""; }
                            
                            txtFormalSalDB.Text = Convert.ToString(drCT["strFormalGiftSal"]);
                            txtInformalSalDB.Text = Convert.ToString(drCT["strInformalGiftSal"]);
                            txtSpouseNameDB.Text = Convert.ToString(drCT["strSpouseName"]);
                            txtMotherDB.Text = Convert.ToString(drCT["strMotherName"]);
                            txtFatherDB.Text = Convert.ToString(drCT["strFatherName"]);
                        }

                        drCT.Close();
                    }

                    if (strWebTbl == "tblWebRecords")
                    {
                        //set web src
                        strSQL = "SELECT tblWebRecords.intGradeCompleted, " +
                                    "tblWebRecords.lngStateID, " +
                                    "tblWebRecords.dteBirthDate, " +
                                    "tblWebRecords.strFirstName, tblWebRecords.strMI, tblWebRecords.strLastCoName, tblWebRecords.strCompanyName, tblWebRecords.strAddress, tblWebRecords.strCity, tblWebRecords.strTitle, tblWebRecords.strZip, tblWebRecords.strHomePhone, tblWebRecords.strWorkPhone, tblWebRecords.strCellPhone, tblWebRecords.strEmail, tblWebRecords.strInformalSal, [tblWebRecords].[strSpouseFName], [tblWebRecords].[strSpouseLName], tblWebRecords_Profile.strEmail AS strConfEmail, tblWebRecords.strMotherName, tblWebRecords.strFatherName " +
                                "FROM tblWebRecords " +
                                    "LEFT JOIN tblWebRecords AS tblWebRecords_Profile ON tblWebRecords.lngProfileWebID = tblWebRecords_Profile.lngRecordWebID " +
                                "WHERE tblWebRecords.lngRecordWebID=" + lngWebID;

                    }
                    else if (strWebTbl == "tblDonorExpress")
                    {
                        strSQL = "SELECT 0 AS intGradeCompleted, " +
                                    "tlkpStates.lngStateID, " +
                                    "tblDonorExpress.strFName AS strFirstName, \"\" AS strMI, tblDonorExpress.strLName AS strLastCoName, \"\" AS strCompanyName, tblDonorExpress.strAddress, tblDonorExpress.strCity, \"\" AS strTitle, tblDonorExpress.strZip, tblDonorExpress.strHomePhone, \"\" AS strWorkPhone, \"\" AS strCellPhone, tblDonorExpress.strEmail, tblDonorExpress.strEmail AS strConfEmail, \"\" AS strInformalSal, \"\" AS strSpouseFName, \"\" AS strSpouseLName, \"\" AS strMotherName, \"\" AS strFatherName " +
                                "FROM tblDonorExpress " +
                                    "LEFT JOIN tlkpStates ON tblDonorExpress.strState = tlkpStates.strState " +
                                "WHERE tblDonorExpress.lngDonorExpressID=" + lngWebID.ToString();
                    }
                    else if (strWebTbl == "tblWebRecordsGGCCReg")
                    {
                        //set web src
                        strSQL = "SELECT 0 AS intGradeCompleted, tblWebRecordsGGCCReg.lngStateID, " +
                                    "tblWebRecordsGGCCReg.dteBirthDate, " +
                                    "tblWebRecordsGGCCReg.strFirstName, tblWebRecordsGGCCReg.strMI, tblWebRecordsGGCCReg.strLastCoName, tblWebRecordsGGCCReg.strCompanyName, tblWebRecordsGGCCReg.strAddress, tblWebRecordsGGCCReg.strCity, \"\" AS strTitle, tblWebRecordsGGCCReg.strZip, tblWebRecordsGGCCReg.strHomePhone, tblWebRecordsGGCCReg.strWorkPhone, tblWebRecordsGGCCReg.strCellPhone, tblWebRecordsGGCCReg.strEmail, tblWebRecordsGGCCReg.strEmail AS strConfEmail, \"\" AS strInformalSal, \"\" AS strSpouseFName, \"\" AS strSpouseLName, \"\" AS strMotherName, \"\" AS strFatherName " +
                                "FROM tblWebRecordsGGCCReg " +
                                "WHERE tblWebRecordsGGCCReg.lngRecordWebID=" + lngWebID.ToString();
                    }
                    else
                        MessageBox.Show("Load proper source");

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drWeb = cmdDB.ExecuteReader())
                    {
                        if (drWeb.Read())
                        {
                            //fill web flds
                            int intGrade = 0;

                            try { intGrade = Convert.ToInt32(drWeb["intGradeCompleted"]); }
                            catch { intGrade = 0; }

                            DateTime dteDOB = DateTime.MinValue;

                            try { dteDOB = Convert.ToDateTime(drWeb["dteBirthDate"]); }
                            catch { dteDOB = DateTime.MinValue; }

                            if (dteDOB != DateTime.MinValue) txtDOBWeb.Text = dteDOB.ToString("d");

                            txtFormalSalWeb.Text = (((string)(Convert.ToString(drWeb["strTitle"]) + " " + Convert.ToString(drWeb["strFirstName"]) + " " + Convert.ToString(drWeb["strMI"]) + " " + Convert.ToString(drWeb["strLastCoName"]))).Replace("  ", " ")).Trim();

                            try { txtMotherWeb.Text = Convert.ToString(drWeb["strMotherName"]); }
                            catch { }

                            try { txtFatherWeb.Text = Convert.ToString(drWeb["strFatherName"]); }
                            catch { }

                            for (int intI = 0; intI < cboStateWeb.Items.Count; intI++)
                            {
                                if (((clsCboItem)cboStateWeb.Items[intI]).ID == Convert.ToInt32(drWeb["lngStateID"]))
                                {
                                    cboStateWeb.SelectedIndex = intI;
                                    break;
                                }
                            }

                            txtFNameWeb.Text = Convert.ToString(drWeb["strFirstName"]);
                            txtMIWeb.Text = Convert.ToString(drWeb["strMI"]);
                            txtLNameWeb.Text = Convert.ToString(drWeb["strLastCoName"]);
                            txtCompanyNameWeb.Text = Convert.ToString(drWeb["strCompanyName"]);
                            txtGradeWeb.Text = intGrade.ToString();
                            txtAddressWeb.Text = Convert.ToString(drWeb["strAddress"]);
                            txtCityWeb.Text = Convert.ToString(drWeb["strCity"]);

                            for (int intI = 0; intI < cboTitleWeb.Items.Count; intI++)
                            {
                                if (cboTitleWeb.Items[intI].ToString() == Convert.ToString(drWeb["strTitle"]))
                                {
                                    cboTitleWeb.SelectedIndex = intI;
                                    break;
                                }
                            }

                            txtZipWeb.Text = Convert.ToString(drWeb["strZip"]);
                            txtHomePhoneWeb.Text = Convert.ToString(drWeb["strHomePhone"]);
                            txtWorkPhoneWeb.Text = Convert.ToString(drWeb["strWorkPhone"]);
                            txtCellPhoneWeb.Text = Convert.ToString(drWeb["strCellPhone"]);
                            txtEMailWeb.Text = Convert.ToString(drWeb["strEmail"]);
                            
                            try { txtRegConfEmailWeb.Text = Convert.ToString(drWeb["strConfEmail"]); }
                            catch { txtRegConfEmailWeb.Text = ""; }

                            txtInformalSalWeb.Text = Convert.ToString(drWeb["strInformalSal"]);
                            txtSpouseNameWeb.Text = Convert.ToString(drWeb["strSpouseFName"]) + " " + Convert.ToString(drWeb["strSpouseLName"]);
                        }

                        drWeb.Close();
                    }

                    if (blnCapsWebProcessing)
                    {
                        txtFormalSalWeb.Text = txtFormalSalWeb.Text.ToUpper();
                        txtMotherWeb.Text =txtMotherWeb.Text.ToUpper();
                        txtFatherWeb.Text=txtFatherWeb.Text.ToUpper();
                        txtFNameWeb.Text = txtFNameWeb.Text.ToUpper();
                        txtMIWeb.Text = txtMIWeb.Text.ToUpper();
                        txtLNameWeb.Text = txtLNameWeb.Text.ToUpper();
                        txtCompanyNameWeb.Text = txtCompanyNameWeb.Text.ToUpper();
                        txtAddressWeb.Text = txtAddressWeb.Text.ToUpper();
                        txtCityWeb.Text = txtCityWeb.Text.ToUpper();
                        txtInformalSalWeb.Text = txtInformalSalWeb.Text.ToUpper();
                        txtSpouseNameWeb.Text = txtSpouseNameWeb.Text.ToUpper();
                    }
                }
            }

            subHighlightDiffs();
        }

        private void subLoadCustomDefs(long _lngRecordID, bool _blnCapsWebProcessing)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldValIR.strLocalCaption, tblCustomFieldValIR.strValue " +
                        "FROM tblCustomFieldValIR " +
                        "WHERE tblCustomFieldValIR.lngRecordID=@lngRecordID";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@lngRecordID", _lngRecordID);

                    List<string[]> strCustomValsLocal = new List<string[]>();
                    List<string[]> strCustomValsWeb = new List<string[]>();

                    using (OleDbDataReader drCustomValsLocal = cmdDB.ExecuteReader())
                    {
                        while (drCustomValsLocal.Read())
                        {
                            string[] strCustomVal = new string[2];
                            string strLocalCaption = "";
                            string strValue = "";

                            try { strLocalCaption = Convert.ToString(drCustomValsLocal["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }
                            try { strValue = Convert.ToString(drCustomValsLocal["strValue"]); }
                            catch { strValue = ""; }

                            strCustomVal[0] = strLocalCaption;
                            strCustomVal[1] = strValue;

                            strCustomValsLocal.Add(strCustomVal);
                        }

                        drCustomValsLocal.Close();
                    }

                    strSQL = "SELECT tblCustomFieldValWebIR.strLocalCaption, tblCustomFieldValWebIR.strValue " +
                        "FROM tblCustomFieldValWebIR " +
                        "WHERE tblCustomFieldValWebIR.lngRecordWebID=@lngRecordWebID";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    cmdDB.Parameters.AddWithValue("@lngRecordWebID", lngWebID);

                    using (OleDbDataReader drCustomValsWeb = cmdDB.ExecuteReader())
                    {
                        while (drCustomValsWeb.Read())
                        {
                            string[] strCustomVal = new string[2];
                            string strLocalCaption = "";
                            string strValue = "";

                            try { strLocalCaption = Convert.ToString(drCustomValsWeb["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }
                            try { strValue = Convert.ToString(drCustomValsWeb["strValue"]); }
                            catch { strValue = ""; }

                            if (_blnCapsWebProcessing) strValue = strValue.ToUpper();

                            strCustomVal[0] = strLocalCaption;
                            strCustomVal[1] = strValue;

                           strCustomValsWeb.Add(strCustomVal);
                        }

                        drCustomValsWeb.Close();
                    }

                strSQL = "SELECT tblCustomFieldDefIR.lngCustomFieldDefIRID, " +
                            "tblCustomFieldDefIR.strLocalCaption, tblCustomFieldDefIR.strFieldType " +
                        "FROM tblCustomFieldDefIR " +
                        "WHERE tblCustomFieldDefIR.blnUseLocal=True " +
                        "ORDER BY tblCustomFieldDefIR.lngSortOrder";

                    cmdDB.CommandText=strSQL;
                    cmdDB.Parameters.Clear();

                    using (OleDbDataReader drCust = cmdDB.ExecuteReader())
                    {
                        int intCustIRTop = 6;
                        bool blnCustIRRightCol = false;

                        while (drCust.Read())
                        {
                            long lngCustomFieldDefIRID = 0;
                            string strFieldType = "";
                            string strLocalCaption = "";

                            try { lngCustomFieldDefIRID = Convert.ToInt32(drCust["lngCustomFieldDefIRID"]); }
                            catch { lngCustomFieldDefIRID = 0; }
                            try { strFieldType = Convert.ToString(drCust["strFieldType"]); }
                            catch { strFieldType = ""; }
                            try { strLocalCaption = Convert.ToString(drCust["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }

                            //add field def
                            Panel panCustIRLocal = new Panel();
                            panCustIRLocal.Name = "panCustomIRLocal_" + lngCustomFieldDefIRID.ToString();
                            panCustIRLocal.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustIRLocal.Width = 214;
                            panCustIRLocal.Top = intCustIRTop;

                            Panel panCustIRWeb = new Panel();
                            panCustIRWeb.Name = "panCustomIRWeb_" + lngCustomFieldDefIRID.ToString();
                            panCustIRWeb.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustIRWeb.Width = 214;
                            panCustIRWeb.Top = intCustIRTop;

                            switch (strFieldType)
                            {
                                case "FIELD":
                                    Label lblCustFieldIRLocal = new Label();
                                    lblCustFieldIRLocal.Name = "lblCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    lblCustFieldIRLocal.Text = strLocalCaption;
                                    lblCustFieldIRLocal.Top = 6;
                                    lblCustFieldIRLocal.Left = 6;
                                    lblCustFieldIRLocal.Width = 101;

                                    TextBox txtCustFieldIRLocal = new TextBox();
                                    txtCustFieldIRLocal.Name = "txtCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    txtCustFieldIRLocal.Left = 113;
                                    txtCustFieldIRLocal.Top = 6;
                                    txtCustFieldIRLocal.Width = 101;
                                    txtCustFieldIRLocal.Height = 20;

                                    for (int intI = 0; intI < strCustomValsLocal.Count; intI++)
                                        if (strCustomValsLocal[intI][0] == strLocalCaption) txtCustFieldIRLocal.Text = strCustomValsLocal[intI][1];

                                    panCustIRLocal.Height = 32;
                                    panCustIRLocal.Controls.Add(lblCustFieldIRLocal);
                                    panCustIRLocal.Controls.Add(txtCustFieldIRLocal);

                                    //web
                                    Label lblCustFieldIRWeb = new Label();
                                    lblCustFieldIRWeb.Name = "lblCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    lblCustFieldIRWeb.Text = strLocalCaption;
                                    lblCustFieldIRWeb.Top = 6;
                                    lblCustFieldIRWeb.Left = 6;
                                    lblCustFieldIRWeb.Width = 101;

                                    TextBox txtCustFieldIRWeb = new TextBox();
                                    txtCustFieldIRWeb.Name = "txtCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    txtCustFieldIRWeb.Left = 113;
                                    txtCustFieldIRWeb.Top = 6;
                                    txtCustFieldIRWeb.Width = 101;
                                    txtCustFieldIRWeb.Height = 20;

                                    for (int intI = 0; intI < strCustomValsWeb.Count; intI++)
                                        if (strCustomValsWeb[intI][0] == strLocalCaption)txtCustFieldIRWeb.Text = strCustomValsWeb[intI][1];

                                    panCustIRWeb.Height = 32;
                                    panCustIRWeb.Controls.Add(lblCustFieldIRWeb);
                                    panCustIRWeb.Controls.Add(txtCustFieldIRWeb);

                                    break;

                                case "MULTI-LINE TEXT":
                                    Label lblCustMLIRLocal = new Label();
                                    lblCustMLIRLocal.Name = "lblCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    lblCustMLIRLocal.Text = strLocalCaption;
                                    lblCustMLIRLocal.Top = 6;
                                    lblCustMLIRLocal.Left = 6;
                                    lblCustMLIRLocal.Width = 101;

                                    TextBox txtCustMLIRLocal = new TextBox();
                                    txtCustMLIRLocal.Name = "txtCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    txtCustMLIRLocal.Left = 113;
                                    txtCustMLIRLocal.Top = 6;
                                    txtCustMLIRLocal.Width = 101;
                                    txtCustMLIRLocal.Height = 40;

                                    for (int intI = 0; intI < strCustomValsLocal.Count; intI++)
                                        if (strCustomValsLocal[intI][0] == strLocalCaption) txtCustMLIRLocal.Text = strCustomValsLocal[intI][1];

                                    panCustIRLocal.Height = 52;
                                    panCustIRLocal.Controls.Add(lblCustMLIRLocal);
                                    panCustIRLocal.Controls.Add(txtCustMLIRLocal);
                                   
                                    //web
                                    Label lblCustMLIRWeb = new Label();
                                    lblCustMLIRWeb.Name = "lblCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    lblCustMLIRWeb.Text = strLocalCaption;
                                    lblCustMLIRWeb.Top = 6;
                                    lblCustMLIRWeb.Left = 6;
                                    lblCustMLIRWeb.Width = 101;

                                    TextBox txtCustMLIRWeb = new TextBox();
                                    txtCustMLIRWeb.Name = "txtCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    txtCustMLIRWeb.Left = 113;
                                    txtCustMLIRWeb.Top = 6;
                                    txtCustMLIRWeb.Width = 101;
                                    txtCustMLIRWeb.Height = 40;

                                    for (int intI = 0; intI < strCustomValsWeb.Count; intI++)
                                        if (strCustomValsWeb[intI][0] == strLocalCaption) txtCustMLIRWeb.Text = strCustomValsWeb[intI][1];

                                    panCustIRWeb.Height = 52;
                                    panCustIRWeb.Controls.Add(lblCustMLIRWeb);
                                    panCustIRWeb.Controls.Add(txtCustMLIRWeb);

                                    break;

                                case "FLAG":
                                    CheckBox chkCustIRLocal = new CheckBox();
                                    chkCustIRLocal.Name = "chkCustIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    chkCustIRLocal.Text = strLocalCaption;
                                    chkCustIRLocal.Top = 6;
                                    chkCustIRLocal.Left = 6;
                                    chkCustIRLocal.Width = 226;

                                    panCustIRLocal.Height = 32;
                                    panCustIRLocal.Controls.Add(chkCustIRLocal);

                                    for (int intI = 0; intI < strCustomValsLocal.Count; intI++)
                                    {
                                        if (strCustomValsLocal[intI][0] == strLocalCaption)
                                        {
                                            string strVal = strCustomValsLocal[intI][1];
                                            bool blnChecked = false;

                                            if (strVal.ToLower() == "true")
                                                blnChecked = true;
                                            else if (strVal.ToLower() == "false")
                                                blnChecked = false;
                                            else if (strVal.ToLower() == "-1")
                                                blnChecked = true;
                                            else
                                                blnChecked = false;

                                            chkCustIRLocal.Checked = blnChecked;
                                        }
                                    }

                                    //web
                                    CheckBox chkCustIRWeb = new CheckBox();
                                    chkCustIRWeb.Name = "chkCustIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    chkCustIRWeb.Text = strLocalCaption;
                                    chkCustIRWeb.Top = 6;
                                    chkCustIRWeb.Left = 6;
                                    chkCustIRWeb.Width = 226;

                                    for (int intI = 0; intI < strCustomValsWeb.Count; intI++)
                                    {
                                        if (strCustomValsWeb[intI][0] == strLocalCaption)
                                        {
                                            string strVal = strCustomValsWeb[intI][1];
                                            bool blnChecked = false;

                                            if (strVal.ToLower() == "true")
                                                blnChecked = true;
                                            else if (strVal.ToLower() == "false")
                                                blnChecked = false;
                                            else if (strVal.ToLower() == "-1")
                                                blnChecked = true;
                                            else
                                                blnChecked = false;

                                            chkCustIRWeb.Checked = blnChecked;
                                        }
                                    }

                                    panCustIRWeb.Height = 32;
                                    panCustIRWeb.Controls.Add(chkCustIRWeb);

                                    break;

                                case "DROPDOWN":
                                    Label lblCustCboIRLocal = new Label();
                                    lblCustCboIRLocal.Name = "lblCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    lblCustCboIRLocal.Text = strLocalCaption;
                                    lblCustCboIRLocal.Top = 6;
                                    lblCustCboIRLocal.Left = 6;
                                    lblCustCboIRLocal.Width = 101;

                                    ComboBox cboCustFieldIRLocal = new ComboBox();
                                    cboCustFieldIRLocal.Name = "cboCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString();
                                    cboCustFieldIRLocal.Left = 113;
                                    cboCustFieldIRLocal.Top = 6;
                                    cboCustFieldIRLocal.Width = 101;
                                    cboCustFieldIRLocal.Height = 20;
                                    //add options
                                    subSetCboItems(cboCustFieldIRLocal, strLocalCaption);

                                    for (int intI = 0; intI < strCustomValsLocal.Count; intI++)
                                        if (strCustomValsLocal[intI][0] == strLocalCaption) cboCustFieldIRLocal.SelectedItem = strCustomValsLocal[intI][1];

                                    panCustIRLocal.Height = 32;
                                    panCustIRLocal.Controls.Add(lblCustCboIRLocal);
                                    panCustIRLocal.Controls.Add(cboCustFieldIRLocal);

                                    //web
                                    Label lblCustCboIRWeb = new Label();
                                    lblCustCboIRWeb.Name = "lblCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    lblCustCboIRWeb.Text = strLocalCaption;
                                    lblCustCboIRWeb.Top = 6;
                                    lblCustCboIRWeb.Left = 6;
                                    lblCustCboIRWeb.Width = 101;

                                    ComboBox cboCustFieldIRWeb = new ComboBox();
                                    cboCustFieldIRWeb.Name = "cboCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString();
                                    cboCustFieldIRWeb.Left = 113;
                                    cboCustFieldIRWeb.Top = 6;
                                    cboCustFieldIRWeb.Width = 101;
                                    cboCustFieldIRWeb.Height = 20;
                                    //add options
                                    subSetCboItems(cboCustFieldIRWeb, strLocalCaption);

                                    for (int intI = 0; intI < strCustomValsWeb.Count; intI++)
                                        if (strCustomValsWeb[intI][0] == strLocalCaption) cboCustFieldIRWeb.SelectedItem = strCustomValsWeb[intI][1];

                                    panCustIRWeb.Height = 32;
                                    panCustIRWeb.Controls.Add(lblCustCboIRWeb);
                                    panCustIRWeb.Controls.Add(cboCustFieldIRWeb);

                                    break;
                            }

                            if (blnCustIRRightCol)
                            {
                                panCustIRLocal.Left = 220;
                                panCustIRWeb.Left = 220;
                                intCustIRTop += panCustIRLocal.Height + 6;
                            }
                            else
                            {
                                panCustIRLocal.Left = 0;
                                panCustIRWeb.Left = 0;
                            }

                            panCustomLocal.Controls.Add(panCustIRLocal);
                            panCustomWeb.Controls.Add(panCustIRWeb);

                            blnCustIRRightCol = !blnCustIRRightCol;
                        }

                        drCust.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subSetCboItems(ComboBox _cboCustFieldIR, string _strLocalCaption)
        {
            string strSQL = "";

            _cboCustFieldIR.Items.Add("");

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldDefIROptions.strValue " +
                        "FROM tblCustomFieldDefIROptions " +
                        "WHERE tblCustomFieldDefIROptions.strLocalCaption=@strLocalCaption " +
                        "ORDER BY tblCustomFieldDefIROptions.intSortOrder";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@strLocalCaption", _strLocalCaption);

                    using (OleDbDataReader drOptions = cmdDB.ExecuteReader())
                    {
                        while (drOptions.Read())
                        {
                            string strValue = "";

                            try { strValue = Convert.ToString(drOptions["strValue"]); }
                            catch { strValue = ""; }

                            if (strValue != "") _cboCustFieldIR.Items.Add(strValue);
                        }

                        drOptions.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subHighlightDiffs()
        {
            txtFormalSalDB.BackColor = System.Drawing.SystemColors.Window;
            cboStateDB.BackColor = System.Drawing.SystemColors.Window;
            txtFNameDB.BackColor = System.Drawing.SystemColors.Window;
            txtMIDB.BackColor = System.Drawing.SystemColors.Window;
            txtLNameDB.BackColor = System.Drawing.SystemColors.Window;
            txtCompanyNameDB.BackColor = System.Drawing.SystemColors.Window;
            txtGradeDB.BackColor = System.Drawing.SystemColors.Window;
            txtDOBDB.BackColor = System.Drawing.SystemColors.Window;
            txtAddressDB.BackColor = System.Drawing.SystemColors.Window;
            txtCityDB.BackColor = System.Drawing.SystemColors.Window;
            cboTitleDB.BackColor = System.Drawing.SystemColors.Window;
            txtZipDB.BackColor = System.Drawing.SystemColors.Window;
            txtHomePhoneDB.BackColor = System.Drawing.SystemColors.Window;
            txtWorkPhoneDB.BackColor = System.Drawing.SystemColors.Window;
            txtCellPhoneDB.BackColor = System.Drawing.SystemColors.Window;
            txtEMailDB.BackColor = System.Drawing.SystemColors.Window;
            txtRegConfEmailDB.BackColor = System.Drawing.SystemColors.Window;
            txtInformalSalDB.BackColor = System.Drawing.SystemColors.Window;
            txtSpouseNameDB.BackColor = System.Drawing.SystemColors.Window;
            txtMotherDB.BackColor = System.Drawing.SystemColors.Window;
            txtFatherDB.BackColor = System.Drawing.SystemColors.Window;

            if (txtFormalSalWeb.Text != txtFormalSalDB.Text) txtFormalSalDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (cboStateWeb.SelectedIndex != cboStateDB.SelectedIndex) cboStateDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtFNameWeb.Text != txtFNameDB.Text) txtFNameDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtMIWeb.Text != txtMIDB.Text) txtMIDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtLNameWeb.Text != txtLNameDB.Text) txtLNameDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtCompanyNameWeb.Text != txtCompanyNameDB.Text) txtCompanyNameDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtGradeWeb.Text != txtGradeDB.Text) txtGradeDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtDOBWeb.Text != txtDOBDB.Text) txtDOBDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtAddressWeb.Text != txtAddressDB.Text) txtAddressDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtCityWeb.Text != txtCityDB.Text) txtCityDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (cboTitleWeb.SelectedIndex != cboTitleDB.SelectedIndex) cboTitleDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtZipWeb.Text != txtZipDB.Text) txtZipDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtHomePhoneWeb.Text != txtHomePhoneDB.Text) txtHomePhoneDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtWorkPhoneWeb.Text != txtWorkPhoneDB.Text) txtWorkPhoneDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtCellPhoneWeb.Text != txtCellPhoneDB.Text) txtCellPhoneDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtEMailWeb.Text != txtEMailDB.Text) txtEMailDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtRegConfEmailWeb.Text != txtRegConfEmailDB.Text) txtRegConfEmailDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtInformalSalWeb.Text != txtInformalSalDB.Text) txtInformalSalDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtSpouseNameWeb.Text != txtSpouseNameDB.Text) txtSpouseNameDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtMotherWeb.Text != txtMotherDB.Text) txtMotherDB.BackColor = System.Drawing.Color.LemonChiffon;
            if (txtFatherWeb.Text != txtFatherDB.Text) txtFatherDB.BackColor = System.Drawing.Color.LemonChiffon;
        }

        private void subLoadCbo()
        {
            cboTitleWeb.Items.Add("Mr.");
            cboTitleWeb.Items.Add("Mrs.");
            cboTitleWeb.Items.Add("Ms.");
            cboTitleWeb.Items.Add("Dr.");

            cboTitleDB.Items.Add("Mr.");
            cboTitleDB.Items.Add("Mrs.");
            cboTitleDB.Items.Add("Ms.");
            cboTitleDB.Items.Add("Dr.");

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngStateID, " +
                            "strState " +
                        "FROM tlkpStates " +
                        "ORDER BY strState";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drStates = cmdDB.ExecuteReader())
                    {
                        while (drStates.Read())
                        {
                            cboStateDB.Items.Add(new clsCboItem(Convert.ToInt32(drStates["lngStateID"]), Convert.ToString(drStates["strState"])));
                            cboStateWeb.Items.Add(new clsCboItem(Convert.ToInt32(drStates["lngStateID"]), Convert.ToString(drStates["strState"])));
                        }

                        drStates.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            long lngMORID=0;

            bool blnUseMOR=false;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblRecords.blnUseMORAddress, " +
                            "tblRecords.lngPrimaryMORID " +
                        "FROM tblRecords " +
                        "WHERE tblRecords.lngRecordID=" + lngDBID;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drIR = cmdDB.ExecuteReader())
                    {
                        if (drIR.Read())
                        {
                            try
                            {
                                lngMORID = Convert.ToInt32(drIR["lngPrimaryMORID"]);
                                blnUseMOR = Convert.ToBoolean(drIR["blnUseMORAddress"]);
                            }
                            catch { }
                        }

                        drIR.Close();
                    }

                    DateTime dteBirthDate = DateTime.MinValue;

                    try { dteBirthDate = Convert.ToDateTime(txtDOBDB.Text); }
                    catch { dteBirthDate = DateTime.MinValue; }

                    string strDOB = "";

                    if (dteBirthDate != DateTime.MinValue) { strDOB = "tblRecords.dteBirthDate = @dteBirthDate, "; }

                    if (blnUseMOR)
                    {
                        strSQL = "UPDATE tblMOR " +
                                "SET tblMOR.lngMORStateID = " + ((clsCboItem)cboStateDB.SelectedItem).ID + ", tblMOR.lngContactLastModifiedUser = " + ((clsCboItem)clsNav.objLogin.cboUsers.SelectedItem).ID + ", " +
                                    "tblMOR.dteContactLastModified = Now(), " +
                                    "tblMOR.strMORCity = @strMORCity, tblMOR.strMORZip = @strMORZip, tblMOR.strMORPhone = @strMORPhone, tblMOR.strMORAddress1 = @strMORAddress1 " +
                                "WHERE tblMOR.lngMORID=" + lngMORID;

                        cmdDB.CommandText = strSQL;

                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@strMORCity", txtCityDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMORZip", txtZipDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMORPhone", txtHomePhoneDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMORAddress1", txtAddressDB.Text);

                        cmdDB.ExecuteNonQuery();

                        strSQL = "UPDATE tblRecords " +
                                "SET tblRecords.intGradeCompleted = @intGradeCompleted, " +
                                    "tblRecords.lngContactLastModifiedUser = " + ((clsCboItem)clsNav.objLogin.cboUsers.SelectedItem).ID + ", " +
                                    "tblRecords.dteContactLastModified = Now(), tblRecords.dteModificationDate = Now(), " + strDOB +
                                    "tblRecords.strFirstName = @strFirstName, tblRecords.strLastCoName = @strLastCoName, tblRecords.strCompanyName = @strCompanyName, tblRecords.strEmail = @strEmail, tblRecords.strConfEmail = @strConfEmail, tblRecords.strWorkExt = @strWorkExt, tblRecords.strSpouseName = @strSpouseName, tblRecords.strWorkPhone = @strWorkPhone, tblRecords.strCellPhone = @strCellPhone, tblRecords.strMI = @strMI, tblRecords.strFormalGiftSal = @strFormalGiftSal, tblRecords.strInformalGiftSal = @strInformalGiftSal, tblRecords.strMotherName=@strMotherName, tblRecords.strFatherName=@strFatherName " +
                                "WHERE tblRecords.lngRecordID=" + lngDBID;

                        cmdDB.CommandText = strSQL;

                        cmdDB.Parameters.Clear();

                        int intGrade = 0;

                        try { intGrade = Convert.ToInt32(txtGradeDB.Text); }
                        catch { intGrade = 0; }

                        cmdDB.Parameters.AddWithValue("@intGradeCompleted", intGrade);

                        if (dteBirthDate != DateTime.MinValue) { cmdDB.Parameters.AddWithValue("@dteBirthDate", dteBirthDate); }

                        cmdDB.Parameters.AddWithValue("@strFirstName", txtFNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strLastCoName", txtLNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strCompanyName", txtCompanyNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strEmail", txtEMailDB.Text);
                        cmdDB.Parameters.AddWithValue("@strConfEmail", txtRegConfEmailDB.Text);
                        cmdDB.Parameters.AddWithValue("@strWorkExt", txtExtDB.Text);
                        cmdDB.Parameters.AddWithValue("@strSpouseName", txtSpouseNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strWorkPhone", txtWorkPhoneDB.Text);
                        cmdDB.Parameters.AddWithValue("@strCellPhone", txtCellPhoneDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMI", txtMIDB.Text);
                        cmdDB.Parameters.AddWithValue("@strFormalGiftSal", txtFormalSalDB.Text);
                        cmdDB.Parameters.AddWithValue("@strInformalGiftSal", txtInformalSalDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMotherName", txtMotherDB.Text);
                        cmdDB.Parameters.AddWithValue("@strFatherName", txtFatherDB.Text);

                        cmdDB.ExecuteNonQuery();
                    }
                    else
                    {
                        long lngStateID = 0;

                        try { lngStateID = ((clsCboItem)cboStateDB.SelectedItem).ID; }
                        catch { lngStateID = 0; }

                        strSQL = "UPDATE tblRecords " +
                                "SET tblRecords.intGradeCompleted=@intGradeCompleted, " +
                                    "tblRecords.lngStateID = " + lngStateID.ToString() + ", tblRecords.lngContactLastModifiedUser = " + ((clsCboItem)clsNav.objLogin.cboUsers.SelectedItem).ID + ", " +
                                    "tblRecords.dteContactLastModified = Now(), tblRecords.dteModificationDate = Now(), " + strDOB +
                                    "tblRecords.strFirstName = @strFirstName, tblRecords.strLastCoName = @strLastCoName, tblRecords.strCompanyName = @strCompanyName, tblRecords.strEmail = @strEmail, tblRecords.strConfEmail = @strConfEmail, tblRecords.strAddress = @strAddress, tblRecords.strZip = @strZip, tblRecords.strWorkExt = @strWorkExt, tblRecords.strSpouseName = @strSpouseName, tblRecords.strWorkPhone = @strWorkPhone, tblRecords.strCellPhone=@strCellPhone, tblRecords.strCity = @strCity, tblRecords.strMI = @strMI, tblRecords.strHomePhone = @strHomePhone, tblRecords.strFormalGiftSal = @strFormalGiftSal, tblRecords.strInformalGiftSal = @strInformalGiftSal, tblRecords.strMotherName=@strMotherName, tblRecords.strFatherName=@strFatherName " +
                                "WHERE tblRecords.lngRecordID=" + lngDBID.ToString();

                        cmdDB.CommandText = strSQL;

                        cmdDB.Parameters.Clear();

                        int intGrade = 0;

                        try { intGrade = Convert.ToInt32(txtGradeDB.Text); }
                        catch { intGrade = 0; }

                        cmdDB.Parameters.AddWithValue("@intGradeCompleted", intGrade);

                        if (dteBirthDate != DateTime.MinValue) { cmdDB.Parameters.AddWithValue("@dteBirthDate", dteBirthDate); }

                        cmdDB.Parameters.AddWithValue("@strFirstName", txtFNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strLastCoName", txtLNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strCompanyName", txtCompanyNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strEmail", txtEMailDB.Text);
                        cmdDB.Parameters.AddWithValue("@strConfEmail", txtRegConfEmailDB.Text);
                        cmdDB.Parameters.AddWithValue("@strAddress", txtAddressDB.Text);
                        cmdDB.Parameters.AddWithValue("@strZip", txtZipDB.Text);
                        cmdDB.Parameters.AddWithValue("@strWorkExt", txtExtDB.Text);
                        cmdDB.Parameters.AddWithValue("@strSpouseName", txtSpouseNameDB.Text);
                        cmdDB.Parameters.AddWithValue("@strWorkPhone", txtWorkPhoneDB.Text);
                        cmdDB.Parameters.AddWithValue("@strCellPhone", txtCellPhoneDB.Text);
                        cmdDB.Parameters.AddWithValue("@strCity", txtCityDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMI", txtMIDB.Text);
                        cmdDB.Parameters.AddWithValue("@strHomePhone", txtHomePhoneDB.Text);
                        cmdDB.Parameters.AddWithValue("@strFormalGiftSal", txtFormalSalDB.Text);
                        cmdDB.Parameters.AddWithValue("@strInformalGiftSal", txtInformalSalDB.Text);
                        cmdDB.Parameters.AddWithValue("@strMotherName", txtMotherDB.Text);
                        cmdDB.Parameters.AddWithValue("@strFatherName", txtFatherDB.Text);

                        cmdDB.ExecuteNonQuery();
                    }
                }

                conDB.Close();
            }

            //append custom vals
            subAppendCustomIRVals(lngDBID);

            DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void subAppendCustomIRVals(long _lngRecordID)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "DELETE tblCustomFieldValIR.* " +
                        "FROM tblCustomFieldValIR " +
                        "WHERE tblCustomFieldValIR.lngRecordID=@lngRecordID";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@lngRecordID", _lngRecordID);

                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }

                    for (int intI = 0; intI <panCustomLocal.Controls.Count; intI++)
                    {
                        if (panCustomLocal.Controls[intI].HasChildren)
                        {
                            if (panCustomLocal.Controls[intI].Name.StartsWith("panCustomIRLocal_"))
                            {
                                long lngCustomFieldDefIRID = 0;
                                string strID = panCustomLocal.Controls[intI].Name;

                                lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                                Panel panCustom = (Panel)panCustomLocal.Controls[intI];

                                string strLocalCaption = "";
                                string strValue = "";

                                //assume textbox or cbo for caption
                                try { strLocalCaption = ((Label)panCustom.Controls["lblCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString()]).Text; }
                                catch { strLocalCaption = ""; }

                                //either a flag or err
                                if (strLocalCaption == "")
                                {
                                    try { strLocalCaption = ((CheckBox)panCustom.Controls["chkCustIRLocal_" + lngCustomFieldDefIRID.ToString()]).Text; }
                                    catch { strLocalCaption = ""; }
                                }

                                if (strLocalCaption != "")
                                {
                                    try { strValue = ((TextBox)panCustom.Controls["txtCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString()]).Text; }
                                    catch { strValue = ""; }

                                    if (strValue == "")
                                    {
                                        try { strValue = ((CheckBox)panCustom.Controls["chkCustIRLocal_" + lngCustomFieldDefIRID.ToString()]).Checked.ToString(); }
                                        catch { strValue = ""; }
                                    }

                                    if (strValue == "")
                                    {
                                        try { strValue = ((ComboBox)panCustom.Controls["cboCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString()]).SelectedItem.ToString(); }
                                        catch { strValue = ""; }
                                    }
                                }

                                strSQL = "INSERT INTO tblCustomFieldValIR " +
                                        "( lngRecordID, " +
                                            "strLocalCaption, strValue ) " +
                                        "VALUES " +
                                        "( @lngRecordID, " +
                                            "@strLocalCaption, @strValue )";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.AddWithValue("@lngRecordID", _lngRecordID);
                                cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                                cmdDB.Parameters.AddWithValue("@strValue", strValue);

                                try { cmdDB.ExecuteNonQuery(); }
                                catch (Exception ex) { }
                            }
                        }
                    }
                }

                conDB.Close();
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            lngDBID = 0;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            txtFormalSalDB.Text = txtFormalSalWeb.Text;
            cboStateDB.SelectedIndex = cboStateWeb.SelectedIndex;
            txtFNameDB.Text = txtFNameWeb.Text;
            txtMIDB.Text = txtMIWeb.Text;
            txtLNameDB.Text = txtLNameWeb.Text;
            txtCompanyNameDB.Text = txtCompanyNameWeb.Text;
            txtGradeDB.Text = txtGradeWeb.Text;
            txtDOBDB.Text = txtDOBWeb.Text;
            txtAddressDB.Text = txtAddressWeb.Text;
            txtCityDB.Text = txtCityWeb.Text;
            cboTitleDB.SelectedIndex = cboTitleWeb.SelectedIndex;
            txtZipDB.Text = txtZipWeb.Text;
            txtHomePhoneDB.Text = txtHomePhoneWeb.Text;
            txtWorkPhoneDB.Text = txtWorkPhoneWeb.Text;
            txtCellPhoneDB.Text = txtCellPhoneWeb.Text;
            txtEMailDB.Text = txtEMailWeb.Text;
            txtRegConfEmailDB.Text = txtRegConfEmailWeb.Text;
            txtInformalSalDB.Text = txtInformalSalWeb.Text;
            txtSpouseNameDB.Text = txtSpouseNameWeb.Text;
            txtMotherDB.Text = txtMotherWeb.Text;
            txtFatherDB.Text = txtFatherWeb.Text;

            subHighlightDiffs();
        }

        private void btnCustomWebToCTAll_Click(object sender, EventArgs e)
        {
            for (int intI = 0; intI < panCustomLocal.Controls.Count; intI++)
            {
                if (panCustomLocal.Controls[intI].HasChildren)
                {
                    if (panCustomLocal.Controls[intI].Name.StartsWith("panCustomIRLocal_"))
                    {
                        long lngCustomFieldDefIRID = 0;
                        string strID = panCustomLocal.Controls[intI].Name;

                        lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                        Panel panCustomLocalVal = (Panel)panCustomLocal.Controls[intI];
                        Panel panCustomWebVal = (Panel)panCustomWeb.Controls[intI];

                        try
                        {
                            ((TextBox)panCustomLocalVal.Controls["txtCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString()]).Text = ((TextBox)panCustomWebVal.Controls["txtCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString()]).Text;
                        }
                        catch
                        {
                            //try checkbox
                            try
                            {
                                ((CheckBox)panCustomLocalVal.Controls["chkCustIRLocal_" + lngCustomFieldDefIRID.ToString()]).Checked = ((CheckBox)panCustomWebVal.Controls["chkCustIRWeb_" + lngCustomFieldDefIRID.ToString()]).Checked;
                            }
                            catch
                            {
                                //try dropdown
                                try { ((ComboBox)panCustomLocalVal.Controls["cboCustFieldIRLocal_" + lngCustomFieldDefIRID.ToString()]).SelectedItem = ((ComboBox)panCustomWebVal.Controls["cboCustFieldIRWeb_" + lngCustomFieldDefIRID.ToString()]).SelectedItem; }
                                catch { }
                            }
                        }
                    }
                }
            }
        }

        private void btnXferTitle_Click(object sender, EventArgs e)
        {
            cboTitleDB.SelectedIndex = cboTitleWeb.SelectedIndex;
        }

        private void btnXferGeneric_Click(object sender, EventArgs e)
        {
            string strField = ((Button)sender).Name.Substring(7);

            try { pagContactInfo.Controls["txt" + strField + "DB"].Text = pagContactInfo.Controls["txt" + strField + "Web"].Text; }
            catch (Exception ex) { }
        }

        private void btnXferCityState_Click(object sender, EventArgs e)
        {
            txtCityDB.Text = txtCityWeb.Text;
            cboStateDB.SelectedIndex = cboStateWeb.SelectedIndex;
        }

        private void btnXferWorkPhoneExt_Click(object sender, EventArgs e)
        {
            txtWorkPhoneDB.Text = txtWorkPhoneWeb.Text;
            txtExtDB.Text = txtExtWeb.Text;
        }
    }
}