using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CTWebMgmt
{
    public partial class frmAddIR : Form
    {
        public long lngRecordID = 0;

        private clsIR irToAdd;

        public frmAddIR(clsIR _irToAdd)
        {
            InitializeComponent();

            irToAdd = _irToAdd;

            clsCboSources.subFillStateCbo(ref cboState, _irToAdd.lngStateID);
            clsCboSources.subFillStateCbo(ref cboBillState, _irToAdd.lngBillStateID);

            chkParent.Checked = _irToAdd.blnParent;
            chkCamper.Checked = _irToAdd.blnCamper;

            radF.Checked = false;
            radM.Checked = false;

            if (_irToAdd.blnGender)
                radM.Checked = true;
            else
                radF.Checked = true;

            txtMI.Text = _irToAdd.strMI;
            txtGrade.Text = _irToAdd.intGrade.ToString();

            if (_irToAdd.dteBDate == DateTime.MinValue)
                txtBDate.Text = "";
            else
                txtBDate.Text = _irToAdd.dteBDate.ToString();

            txtRecordWebID.Text = _irToAdd.lngRecordWebID.ToString();

            txtEMail.Text = _irToAdd.strEmail;
            txtConfEmail.Text = _irToAdd.strConfEmail;
            txtWorkPhone.Text = _irToAdd.strWorkPhone;
            txtHomePhone.Text = _irToAdd.strHomePhone;
            txtCellPhone.Text = _irToAdd.strCellPhone;
            txtZip.Text = _irToAdd.strZip;
            txtCity.Text = _irToAdd.strCity;
            txtAddress.Text = _irToAdd.strAddress;
            txtCompany.Text = _irToAdd.strCompany;
            txtLName.Text = _irToAdd.strLName;
            txtFName.Text = _irToAdd.strFName;
            txtPmtType.Text = _irToAdd.strPmtType;

            txtSpecNeeds.Text = _irToAdd.strSpecialNeeds;

            if (_irToAdd.strSpecialNeeds.Length > 0)
                btnSpecialNeeds.Visible = true;
            else
                btnSpecialNeeds.Visible = false;

            txtNotes.Text = _irToAdd.strNotes;
            txtPmtRef.Text = _irToAdd.strPmtRef;
            txtBankName.Text = _irToAdd.strBankName;
            txtFather.Text = _irToAdd.strFather;
            txtMother.Text = _irToAdd.strMother;
            txtBillName.Text = _irToAdd.strBillName;
            txtBillAddress.Text = _irToAdd.strBillAddress;
            txtBillCity.Text = _irToAdd.strBillCity;
            txtBillZip.Text = _irToAdd.strBillZip;
            txtBillPhone.Text = _irToAdd.strBillPhone;

            //check caps and convert if necessary
            bool blnCapsWebProcessing = false;

            string strSQL = "SELECT blnCapsWebProcessing " +
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

            if (blnCapsWebProcessing)
            {
                txtMI.Text = txtMI.Text.ToUpper();
                txtCity.Text = txtCity.Text.ToUpper();
                txtAddress.Text = txtAddress.Text.ToUpper();
                txtCompany.Text = txtCompany.Text.ToUpper();
                txtLName.Text = txtLName.Text.ToUpper();
                txtFName.Text = txtFName.Text.ToUpper();

                txtSpecNeeds.Text = txtSpecNeeds.Text.ToUpper();

                txtNotes.Text = txtNotes.Text.ToUpper();
                txtBankName.Text = txtBankName.Text.ToUpper();
                txtFather.Text = txtFather.Text.ToUpper();
                txtMother.Text = txtMother.Text.ToUpper();
                txtBillName.Text = txtBillName.Text.ToUpper();
                txtBillAddress.Text = txtBillAddress.Text.ToUpper();
                txtBillCity.Text = txtBillCity.Text.ToUpper();
            }

            //custom field defs and vals

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldDefIR.lngCustomFieldDefIRID, " +
                               "tblCustomFieldDefIR.strFieldType, tblCustomFieldDefIR.strLocalCaption " +
                           "FROM tblCustomFieldDefIR " +
                           "WHERE tblCustomFieldDefIR.blnUseLocal=True "+
                           "ORDER BY tblCustomFieldDefIR.lngSortOrder, " +
                               "tblCustomFieldDefIR.strLocalCaption";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCustDef = cmdDB.ExecuteReader())
                    {
                        bool blnCustIRRightCol = false;
                        int intCustIRTop = 6;

                        while (drCustDef.Read())
                        {
                            long lngCustomFieldDefIRID = 0;
                            string strFieldType = "";
                            string strLocalCaption = "";

                            try { lngCustomFieldDefIRID = Convert.ToInt32(drCustDef["lngCustomFieldDefIRID"]); }
                            catch { lngCustomFieldDefIRID = 0; }
                            try { strFieldType = Convert.ToString(drCustDef["strFieldType"]); }
                            catch { strFieldType = ""; }
                            try { strLocalCaption = Convert.ToString(drCustDef["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }

                            //add field def
                            Panel panCustIR = new Panel();
                            panCustIR.Name = "panCustomIR_" + lngCustomFieldDefIRID.ToString();
                            panCustIR.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustIR.Width = 355;
                            panCustIR.Top = intCustIRTop;

                            switch (strFieldType)
                            {
                                case "FIELD":
                                    Label lblCustFieldIR = new Label();
                                    lblCustFieldIR.Name = "lblCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    lblCustFieldIR.Text = strLocalCaption;
                                    lblCustFieldIR.Top = 6;
                                    lblCustFieldIR.Left = 6;
                                    lblCustFieldIR.Width = 175;

                                    TextBox txtCustFieldIR = new TextBox();
                                    txtCustFieldIR.Name = "txtCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    txtCustFieldIR.Left = 187;
                                    txtCustFieldIR.Top = 6;
                                    txtCustFieldIR.Width = 156;
                                    txtCustFieldIR.Height = 20;

                                    for (int intI = 0; intI < irToAdd.strCustom.Count; intI++)
                                        if (irToAdd.strCustom[intI][0] == strLocalCaption) txtCustFieldIR.Text = irToAdd.strCustom[intI][1];

                                    if (blnCapsWebProcessing) txtCustFieldIR.Text = txtCustFieldIR.Text.ToUpper();

                                    panCustIR.Height = 32;
                                    panCustIR.Controls.Add(lblCustFieldIR);
                                    panCustIR.Controls.Add(txtCustFieldIR);

                                    break;

                                case "MULTI-LINE TEXT":
                                    Label lblCustMLIR = new Label();
                                    lblCustMLIR.Name = "lblCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    lblCustMLIR.Text = strLocalCaption;
                                    lblCustMLIR.Top = 6;
                                    lblCustMLIR.Left = 6;
                                    lblCustMLIR.Width = 175;

                                    TextBox txtCustMLIR = new TextBox();
                                    txtCustMLIR.Name = "txtCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    txtCustMLIR.Left = 187;
                                    txtCustMLIR.Top = 6;
                                    txtCustMLIR.Width = 156;
                                    txtCustMLIR.Height = 40;

                                    for (int intI = 0; intI < irToAdd.strCustom.Count; intI++)
                                        if (irToAdd.strCustom[intI][0] == strLocalCaption) txtCustMLIR.Text = irToAdd.strCustom[intI][1];

                                    if (blnCapsWebProcessing) txtCustMLIR.Text = txtCustMLIR.Text.ToUpper();

                                    panCustIR.Height = 52;
                                    panCustIR.Controls.Add(lblCustMLIR);
                                    panCustIR.Controls.Add(txtCustMLIR);

                                    break;

                                case "FLAG":
                                    CheckBox chkCustIR = new CheckBox();
                                    chkCustIR.Name = "chkCustIR_" + lngCustomFieldDefIRID.ToString();
                                    chkCustIR.Text = strLocalCaption;
                                    chkCustIR.Top = 6;
                                    chkCustIR.Left = 6;
                                    chkCustIR.Width = 226;

                                    panCustIR.Height = 32;
                                    panCustIR.Controls.Add(chkCustIR);

                                    for (int intI = 0; intI < irToAdd.strCustom.Count; intI++)
                                    {
                                        if (irToAdd.strCustom[intI][0] == strLocalCaption)
                                        {
                                            string strVal = irToAdd.strCustom[intI][1];
                                            bool blnChecked = false;

                                            if (strVal.ToLower() == "true")
                                                blnChecked = true;
                                            else if (strVal.ToLower() == "false")
                                                blnChecked = false;
                                            else if (strVal.ToLower() == "-1")
                                                blnChecked = true;
                                            else
                                                blnChecked = false;

                                            chkCustIR.Checked = blnChecked;
                                        }
                                    }

                                    break;

                                case "DROPDOWN":
                                    Label lblCustCboIR = new Label();
                                    lblCustCboIR.Name = "lblCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    lblCustCboIR.Text = strLocalCaption;
                                    lblCustCboIR.Top = 6;
                                    lblCustCboIR.Left = 6;
                                    lblCustCboIR.Width = 175;

                                    ComboBox cboCustFieldIR = new ComboBox();
                                    cboCustFieldIR.Name = "cboCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    cboCustFieldIR.Left = 187;
                                    cboCustFieldIR.Top = 6;
                                    cboCustFieldIR.Width = 156;
                                    cboCustFieldIR.Height = 20;
                                    //add options
                                    subSetCboItems(cboCustFieldIR, strLocalCaption);

                                    for (int intI = 0; intI < irToAdd.strCustom.Count; intI++)
                                        if (irToAdd.strCustom[intI][0] == strLocalCaption) cboCustFieldIR.SelectedItem = irToAdd.strCustom[intI][1];

                                    panCustIR.Height = 32;
                                    panCustIR.Controls.Add(lblCustCboIR);
                                    panCustIR.Controls.Add(cboCustFieldIR);

                                    break;
                            }

                            if (blnCustIRRightCol)
                            {
                                panCustIR.Left = 387;
                                intCustIRTop += panCustIR.Height + 6;
                            }
                            else
                                panCustIR.Left = 6;

                            pagCustom.Controls.Add(panCustIR);

                            blnCustIRRightCol = !blnCustIRRightCol;
                        }

                        drCustDef.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lngRecordID = 0;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            //add record, get id, close form            
            try
            {
                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    objConn.Open();

                    strSQL = "INSERT INTO tblRecords " +
                            "( blnCamper, blnParent, blnGender, " +
                                "intGradeCompleted, " +
                                "lngStateID, lngRecordWebID, " +
                                "dteBirthDate, " +
                                "strFirstName, strLastCoName, strCompanyName, strAddress, strCity, strZip, strHomePhone, strWorkPhone, strCellPhone, strEmail, strConfEmail, strMI, mmoSpecialNeeds, mmoNotes, strFatherName, strMotherName ) " +
                            "VALUES " +
                            "( @blnCamper, @blnParent, @blnGender, " +
                                "@intGradeCompleted, " +
                                "@lngStateID, @lngRecordWebID, " +
                                "@dteBirthDate, " +
                                "@strFirstName, @strLastCoName, @strCompanyName, @strAddress, @strCity, @strZip, @strHomePhone, @strWorkPhone, @strCellPhone, @strEmail, @strConfEmail, @strMI, @mmoSpecialNeeds, @mmoNotes, @strFatherName, @strMotherName )";

                    using (OleDbCommand objCommand = new OleDbCommand(strSQL, objConn))
                    {
                        objCommand.Parameters.AddWithValue("@blnCamper", chkCamper.Checked);
                        objCommand.Parameters.AddWithValue("@blnParent", chkParent.Checked);

                        bool blnGender = true;

                        blnGender = radM.Checked;

                        objCommand.Parameters.AddWithValue("@blnGender", blnGender);

                        int intGrade = 0;

                        try { intGrade = Convert.ToInt32(txtGrade.Text); }
                        catch { intGrade = 0; }

                        objCommand.Parameters.AddWithValue("@intGradeCompleted", intGrade);

                        objCommand.Parameters.AddWithValue("@lngStateID", ((clsCboItem)cboState.SelectedItem).ID);

                        long lngRecordWebID = 0;

                        try { lngRecordWebID = Convert.ToInt32(txtRecordWebID.Text); }
                        catch { lngRecordWebID = 0; }

                        objCommand.Parameters.AddWithValue("@lngRecordWebID", lngRecordWebID);

                        DateTime dteBDate;

                        try { dteBDate = Convert.ToDateTime(txtBDate.Text); }
                        catch { dteBDate = DateTime.MinValue; }

                        if (dteBDate == DateTime.MinValue)
                            objCommand.Parameters.AddWithValue("@dteBDate", DBNull.Value);
                        else
                            objCommand.Parameters.AddWithValue("@dteBDate", dteBDate);

                        objCommand.Parameters.AddWithValue("@strFirstName", txtFName.Text);
                        objCommand.Parameters.AddWithValue("@strLastCoName", txtLName.Text);
                        objCommand.Parameters.AddWithValue("@strCompanyName", txtCompany.Text);
                        objCommand.Parameters.AddWithValue("@strAddress", txtAddress.Text);
                        objCommand.Parameters.AddWithValue("@strCity", txtCity.Text);
                        objCommand.Parameters.AddWithValue("@strZip", txtZip.Text);
                        objCommand.Parameters.AddWithValue("@strHomePhone", txtHomePhone.Text);
                        objCommand.Parameters.AddWithValue("@strWorkPhone", txtWorkPhone.Text);
                        objCommand.Parameters.AddWithValue("@strCellPhone", txtCellPhone.Text);
                        objCommand.Parameters.AddWithValue("@strEmail", txtEMail.Text);
                        objCommand.Parameters.AddWithValue("@strConfEmail", txtConfEmail.Text);

                        objCommand.Parameters.AddWithValue("@strMI", txtMI.Text);
                        objCommand.Parameters.AddWithValue("@mmoSpecialNeeds", txtSpecNeeds.Text);
                        objCommand.Parameters.AddWithValue("@mmoNotes", txtNotes.Text);
                        objCommand.Parameters.AddWithValue("@strFatherName", txtFather.Text);
                        objCommand.Parameters.AddWithValue("@strMotherName", txtMother.Text);

                        if (objCommand.ExecuteNonQuery() > 0)
                        {
                            objCommand.CommandText = "SELECT @@IDENTITY;";

                            lngRecordID = Convert.ToInt32(objCommand.ExecuteScalar().ToString());

                            //append custom vals
                            subAppendCustomIRVals(lngRecordID);
                        }
                        else
                            lngRecordID = 0;
                    }

                    objConn.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmAddIR.btnContinue", ex);
                lngRecordID = 0;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void subAppendCustomIRVals(long _lngRecordID)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL="DELETE tblCustomFieldValIR.* "+
                        "FROM tblCustomFieldValIR "+
                        "WHERE tblCustomFieldValIR.lngRecordID=@lngRecordID";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@lngRecordID", _lngRecordID);

                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }

                    for (int intI = 0; intI < pagCustom.Controls.Count; intI++)
                    {
                        if (pagCustom.Controls[intI].HasChildren)
                        {
                            if (pagCustom.Controls[intI].Name.StartsWith("panCustomIR_"))
                            {
                                long lngCustomFieldDefIRID = 0;
                                string strID = pagCustom.Controls[intI].Name;

                                lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                                Panel panCustom = (Panel)pagCustom.Controls[intI];

                                string strLocalCaption = "";
                                string strValue = "";

                                //assume textbox or cbo for caption
                                try { strLocalCaption = ((Label)panCustom.Controls["lblCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                                catch { strLocalCaption = ""; }

                                //either a flag or err
                                if (strLocalCaption == "")
                                {
                                    try { strLocalCaption = ((CheckBox)panCustom.Controls["chkCustIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                                    catch { strLocalCaption = ""; }
                                }

                                if (strLocalCaption != "")
                                {
                                    try { strValue = ((TextBox)panCustom.Controls["txtCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                                    catch { strValue = ""; }

                                    if (strValue == "")
                                    {
                                        try { strValue = ((CheckBox)panCustom.Controls["chkCustIR_" + lngCustomFieldDefIRID.ToString()]).Checked.ToString(); }
                                        catch { strValue = ""; }
                                    }

                                    if (strValue == "")
                                    {
                                        try { strValue = ((ComboBox)panCustom.Controls["cboCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).SelectedItem.ToString(); }
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
    }
}