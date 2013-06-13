using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml;

namespace CTWebMgmt.Admin
{
    public partial class frmSyncCustomFlags : Form
    {
        private long lngDirtyProgramID = 0;
        private string strDirtyProgram = "";

        private int intNextIRTop = 0;
        private int intNextRegTop = 0;

        public frmSyncCustomFlags()
        {
            InitializeComponent();
        }

        private void frmSyncCustomFlags_Load(object sender, EventArgs e)
        {
            subLoadCustomFieldDefIRs(0);
            subLoadCustomFieldDefRegs(0);

            string strSQL = "";

            StringBuilder stbBln = new StringBuilder();
            StringBuilder stbStr = new StringBuilder();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //load program specific options
                strSQL = "SELECT tlkpCampName.lngCampID, " +
                            "tlkpCampName.strCampName " +
                        "FROM tlkpCampName " +
                        "ORDER BY tlkpCampName.strCampName;";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCamp = cmdDB.ExecuteReader())
                    {
                        cboProgramSpecificDef.Items.Add(new clsCboItem(0, "Default, no program specified"));

                        while (drCamp.Read())
                            cboProgramSpecificDef.Items.Add(new clsCboItem(Convert.ToInt32(drCamp["lngCampID"]), Convert.ToString(drCamp["strCampName"])));

                        drCamp.Close();

                        cboProgramSpecificDef.SelectedIndex = 0;
                    }
                }

                conDB.Close();
            }

            subFillGrid();
        }

        private void subLoadCustomFieldDefIRs(long _lngProgramID)
        {
            //loop through custom field defs and add controls

            //first clear any existing panels
            bool blnContinueClearing = true;

            while (blnContinueClearing)
            {
                for (int intI = 0; intI < pagIRCustom.Controls.Count; intI++)
                {
                    blnContinueClearing = false;

                    if (pagIRCustom.Controls[intI].Name.Contains("panCustomIR_"))
                    {
                        blnContinueClearing = true;
                        pagIRCustom.Controls.RemoveAt(intI);
                        break;
                    }
                }
            }

            string strSQL = "";

            //get web defs from web service, get local defs from db
            //local defs
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldDefIR.blnUseLocal, " +
                            "tblCustomFieldDefIR.lngCustomFieldDefIRID, tblCustomFieldDefIR.lngSortOrder, " +
                            "tblCustomFieldDefIR.strLocalCaption, tblCustomFieldDefIR.strFieldType " +
                        "FROM tblCustomFieldDefIR " +
                        "ORDER BY tblCustomFieldDefIR.lngSortOrder";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCust = cmdDB.ExecuteReader())
                    {
                        intNextIRTop = 55;

                        while (drCust.Read())
                        {
                            //local defs
                            bool blnUseLocal = false;
                            long lngCustomFieldDefIRID = 0;
                            long lngSortOrder = 0;
                            string strLocalCaption = "";
                            string strFieldType = "";

                            try { blnUseLocal = Convert.ToBoolean(drCust["blnUseLocal"]); }
                            catch { blnUseLocal = false; }
                            try { lngCustomFieldDefIRID = Convert.ToInt32(drCust["lngCustomFieldDefIRID"]); }
                            catch { lngCustomFieldDefIRID = 0; }
                            try { lngSortOrder = Convert.ToInt32(drCust["lngSortOrder"]); }
                            catch { lngSortOrder = 0; }
                            try { strLocalCaption = Convert.ToString(drCust["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }
                            try { strFieldType = Convert.ToString(drCust["strFieldType"]); }
                            catch { strFieldType = ""; }

                            CheckBox chkUseLocal = new CheckBox();
                            chkUseLocal.Name = "chkUseLocalIR_" + lngCustomFieldDefIRID.ToString();
                            chkUseLocal.Checked = blnUseLocal;
                            chkUseLocal.Top = 9;
                            chkUseLocal.Left = lblUseLocalHead.Left;
                            chkUseLocal.Width = 15;

                            CheckBox chkUseProfile = new CheckBox();
                            chkUseProfile.Name = "chkUseProfileIR_" + lngCustomFieldDefIRID.ToString();
                            chkUseProfile.Top = 9;
                            chkUseProfile.Left = lblUseProfileHead.Left;
                            chkUseProfile.Width = 15;
                            chkUseProfile.CheckedChanged += new EventHandler(chkUseCamperProfile_CheckedChanged);

                            CheckBox chkUseCamper = new CheckBox();
                            chkUseCamper.Name = "chkUseCamperIR_" + lngCustomFieldDefIRID.ToString();
                            chkUseCamper.Top = 9;
                            chkUseCamper.Left = lblUseCamperHead.Left;
                            chkUseCamper.Width = 15;
                            chkUseCamper.CheckedChanged += new EventHandler(chkUseCamperProfile_CheckedChanged);

                            CheckBox chkRequired = new CheckBox();
                            chkRequired.Name = "chkRequiredIR_" + lngCustomFieldDefIRID.ToString();
                            chkRequired.Top = 9;
                            chkRequired.Left = lblRequiredHead.Left;
                            chkRequired.Width = 15;
                            chkRequired.Visible = false;

                            TextBox txtSortOrder = new TextBox();
                            txtSortOrder.Name = "txtSortOrderIR_" + lngCustomFieldDefIRID.ToString();
                            txtSortOrder.Text = lngSortOrder.ToString();
                            txtSortOrder.Top = 9;
                            txtSortOrder.Left = lblSortOrderHead.Left;
                            txtSortOrder.Width = 55;

                            TextBox txtLocalCaption = new TextBox();
                            txtLocalCaption.Name = "txtLocalCaptionIR_" + lngCustomFieldDefIRID.ToString();
                            txtLocalCaption.Text = strLocalCaption;
                            txtLocalCaption.Top = 9;
                            txtLocalCaption.Left = lblLocalLabelHead.Left;
                            txtLocalCaption.Width = 165;
                            txtLocalCaption.Enabled = false;
                            txtLocalCaption.ReadOnly = true;

                            TextBox txtWebCaption = new TextBox();
                            txtWebCaption.Name = "txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString();
                            txtWebCaption.Top = 9;
                            txtWebCaption.Left = lblOnlineLabelHead.Left;
                            txtWebCaption.Width = 165;
                            txtWebCaption.Visible = false;

                            Label lblFieldType = new Label();
                            lblFieldType.Name = "lblFieldTypeIR_" + lngCustomFieldDefIRID.ToString();
                            lblFieldType.Top = 9;
                            lblFieldType.Text = strFieldType;
                            lblFieldType.Left = lblFieldTypeHead.Left;

                            Button btnDetails = new Button();
                            btnDetails.Name = "btnDetailsIR_" + lngCustomFieldDefIRID.ToString();
                            btnDetails.Click += new EventHandler(btnDetailsIR_Click);
                            btnDetails.Top = 9;
                            btnDetails.Text = "Details";
                            btnDetails.Width = 57;
                            btnDetails.Left = txtSortOrder.Left + txtSortOrder.Width + 6;

                            Button btnDelete = new Button();
                            btnDelete.Name = "btnDeleteIR_" + lngCustomFieldDefIRID.ToString();
                            btnDelete.Click += new EventHandler(btnDeleteIR_Click);
                            btnDelete.Top = 9;
                            btnDelete.Text = "Delete";
                            btnDelete.Width = 55;
                            btnDelete.Left = btnDetails.Left + btnDetails.Width + 6;

                            //holders for header/footer
                            TextBox txtHeader = new TextBox();
                            txtHeader.Name = "txtHeaderIR_" + lngCustomFieldDefIRID.ToString();
                            txtHeader.Top = 24;
                            txtHeader.Width = 55;
                            txtHeader.Left = 0;
                            txtHeader.Visible = false;

                            TextBox txtFooter = new TextBox();
                            txtFooter.Name = "txtFooterIR_" + lngCustomFieldDefIRID.ToString();
                            txtFooter.Top = 24;
                            txtFooter.Width = 55;
                            txtFooter.Left = 61;
                            txtFooter.Visible = false;

                            TextBox txtDropdownOptions = new TextBox();
                            txtDropdownOptions.Name = "txtDropdownOptionsIR_" + lngCustomFieldDefIRID.ToString();
                            txtDropdownOptions.Top = 24;
                            txtDropdownOptions.Width = 55;
                            txtDropdownOptions.Left = 122;
                            if (strFieldType == "DROPDOWN") subLoadDropdownOptionsIR(txtDropdownOptions, lngCustomFieldDefIRID, strLocalCaption);
                            txtDropdownOptions.Visible = false;

                            Panel panCustomIR = new Panel();

                            panCustomIR.Name = "panCustomIR_" + lngCustomFieldDefIRID.ToString();
                            panCustomIR.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustomIR.Width = btnDelete.Left + btnDelete.Width + 6;
                            panCustomIR.Height = 38;
                            panCustomIR.Left = 3;
                            panCustomIR.Top = intNextIRTop;

                            panCustomIR.Controls.Add(chkUseLocal);
                            panCustomIR.Controls.Add(chkUseProfile);
                            panCustomIR.Controls.Add(chkUseCamper);
                            panCustomIR.Controls.Add(chkRequired);
                            panCustomIR.Controls.Add(txtSortOrder);
                            panCustomIR.Controls.Add(txtLocalCaption);
                            panCustomIR.Controls.Add(lblFieldType);
                            panCustomIR.Controls.Add(txtWebCaption);
                            panCustomIR.Controls.Add(btnDetails);
                            panCustomIR.Controls.Add(btnDelete);
                            panCustomIR.Controls.Add(txtHeader);
                            panCustomIR.Controls.Add(txtFooter);
                            panCustomIR.Controls.Add(txtDropdownOptions);

                            pagIRCustom.Controls.Add(panCustomIR);

                            intNextIRTop += 44;
                        }

                        drCust.Close();
                    }
                }

                conDB.Close();
            }

            //web defs
            //load web ir definitions
            //load web reg definitions
            using (wsXferEventInfo.XferEventInfo wsDLFlags = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                strSQL = "SELECT lngCustomFieldDefIRWebID, ISNULL(lngCustomFieldDefIRID, 0) AS lngCustomFieldDefIRID, lngCTUserID, " +
                            "blnUseCamper, blnUseProfile, blnRequired, " +
                            "lngProgramID, " +
                            "strLocalCaption, strWebCaption, strHeader, strFooter " +
                        "FROM tblCustomFieldDefIR " +
                        "WHERE lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "lngProgramID = " + _lngProgramID.ToString();

                string strWebXML = "";

                try { strWebXML = wsDLFlags.fcnGetRecords(strSQL, "tblCustomVals", clsWebTalk.strWebConn); }
                catch (Exception ex) { }

                DataSet dsXML = new DataSet("tblCustomVals");

                dsXML.ReadXml(new System.IO.StringReader(strWebXML), XmlReadMode.ReadSchema);

                foreach (DataTable tbl in dsXML.Tables)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        string strNameOnWS = "";
                        long lngIDOnWS = 0;

                        bool blnUseProfile = false;
                        bool blnUseCamper = false;
                        bool blnRequired = false;
                        string strWebCaption = "";
                        string strHeader = "";
                        string strFooter = "";

                        try { strNameOnWS = Convert.ToString(row["strLocalCaption"]); }
                        catch { strNameOnWS = ""; }

                        try { lngIDOnWS = Convert.ToInt32(row["lngCustomFieldDefIRID"]); }
                        catch { lngIDOnWS = 0; }

                        try { strWebCaption = Convert.ToString(row["strWebCaption"]); }
                        catch { strWebCaption = ""; }

                        if (lngIDOnWS == 0)
                        {
                            //field/flag hasn't been matched with a local id:  look up caption from old table to match it to web data
                            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                            {
                                conDB.Open();

                                //join in old structure to match fields that haven't been used yet.
                                strSQL = "SELECT tblCustomFieldDefIR.lngCustomFieldDefIRID " +
                                        "FROM tblCustomFlagDesc " +
                                            "INNER JOIN tblCustomFieldDefIR ON tblCustomFlagDesc." + strNameOnWS + " = tblCustomFieldDefIR.strLocalCaption";

                                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                                {
                                    try { lngIDOnWS = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                    catch { lngIDOnWS = 0; }
                                }

                                conDB.Close();
                            }
                        }

                        if (lngIDOnWS > 0)
                        {
                            try
                            {
                                try { blnUseProfile = Convert.ToBoolean(row["blnUseProfile"]); }
                                catch { blnUseProfile = false; }

                                try { blnUseCamper = Convert.ToBoolean(row["blnUseCamper"]); }
                                catch { blnUseCamper = false; }

                                try { blnRequired = Convert.ToBoolean(row["blnRequired"]); }
                                catch { blnRequired = false; }

                                try { strHeader = Convert.ToString(row["strHeader"]); }
                                catch { strHeader = ""; }

                                try { strFooter = Convert.ToString(row["strFooter"]); }
                                catch { strFooter = ""; }

                                Panel panCustomIR = (Panel)pagIRCustom.Controls["panCustomIR_" + lngIDOnWS.ToString()];

                                if (blnUseCamper) ((CheckBox)panCustomIR.Controls["chkUseCamperIR_" + lngIDOnWS.ToString()]).Checked = true;
                                if (blnUseProfile) ((CheckBox)panCustomIR.Controls["chkUseProfileIR_" + lngIDOnWS.ToString()]).Checked = true;
                                if (blnRequired) ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngIDOnWS.ToString()]).Checked = true;
                                ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngIDOnWS.ToString()]).Text = strWebCaption;
                                ((TextBox)panCustomIR.Controls["txtHeaderIR_" + lngIDOnWS.ToString()]).Text = strHeader;
                                ((TextBox)panCustomIR.Controls["txtFooterIR_" + lngIDOnWS.ToString()]).Text = strFooter;

                                if (blnUseCamper || blnUseProfile)
                                {
                                    if (((Label)panCustomIR.Controls["lblFieldTypeIR_" + lngIDOnWS.ToString()]).Text != "FLAG")
                                        ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngIDOnWS.ToString()]).Visible = true;

                                    ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngIDOnWS.ToString()]).Visible = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error retrieving custom field definitions: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void subLoadDropdownOptionsIR(TextBox _txtDropdownOptions, long _lngCustomFieldDefIRID, string _strLocalCaption)
        {             
            //add each option to dropdown definition
            string strSQL="";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT strValue " +
                        "FROM tblCustomFieldDefIROptions " +
                        "WHERE strLocalCaption=@strLocalCaption";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@strLocalCaption", _strLocalCaption);

                    using (OleDbDataReader drOptions = cmdDB.ExecuteReader())
                    {
                        string strValue = "";
                        string strItems = "";

                        while (drOptions.Read())
                        {
                            try { strValue = Convert.ToString(drOptions["strValue"]); }
                            catch { strValue = ""; }

                            if (strItems == "")
                                strItems = strValue;
                            else
                                strItems += "\r\n" + strValue;
                        }

                        _txtDropdownOptions.Text = strItems;
                    }
                }

                conDB.Close();
            }
        }

        private void subLoadDropdownOptionsReg(TextBox _txtDropdownOptions, long _lngCustomFieldDefRegID, string _strLocalCaption)
        {
            //add each option to dropdown definition
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT strValue " +
                        "FROM tblCustomFieldDefRegOptions " +
                        "WHERE strLocalCaption=@strLocalCaption";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@strLocalCaption", _strLocalCaption);

                    using (OleDbDataReader drOptions = cmdDB.ExecuteReader())
                    {
                        string strValue = "";
                        string strItems = "";

                        while (drOptions.Read())
                        {
                            try { strValue = Convert.ToString(drOptions["strValue"]); }
                            catch { strValue = ""; }

                            if (strItems == "")
                                strItems = strValue;
                            else
                                strItems += "\r\n" + strValue;
                        }

                        _txtDropdownOptions.Text = strItems;
                    }
                }

                conDB.Close();
            }
        }

        private void subLoadCustomFieldDefRegs(long _lngProgramID)
        {
            //loop through custom field defs and add controls

            //first clear any existing panels
            bool blnContinueClearing = true;

            while (blnContinueClearing)
            {
                for (int intI = 0; intI < pagRegCustom.Controls.Count; intI++)
                {
                    blnContinueClearing = false;

                    if (pagRegCustom.Controls[intI].Name.Contains("panCustomReg_"))
                    {
                        blnContinueClearing = true;
                        pagRegCustom.Controls.RemoveAt(intI);
                        break;
                    }
                }
            }

            string strSQL = "";

            //get web defs from web service, get local defs from db
            //local defs
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldDefReg.blnUseLocal, " +
                            "tblCustomFieldDefReg.lngCustomFieldDefRegID, tblCustomFieldDefReg.lngSortOrder, " +
                            "tblCustomFieldDefReg.decCharge, " +
                            "tblCustomFieldDefReg.strLocalCaption, tblCustomFieldDefReg.strFieldType " +
                        "FROM tblCustomFieldDefReg " +
                        "ORDER BY tblCustomFieldDefReg.lngSortOrder";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCust = cmdDB.ExecuteReader())
                    {
                        intNextRegTop = 55;

                        while (drCust.Read())
                        {
                            //local defs
                            bool blnUseLocal = false;
                            long lngCustomFieldDefRegID = 0;
                            long lngSortOrder = 0;
                            decimal decCharge = 0;
                            string strLocalCaption = "";
                            string strFieldType = "";

                            try { blnUseLocal = Convert.ToBoolean(drCust["blnUseLocal"]); }
                            catch { blnUseLocal = false; }
                            try { lngCustomFieldDefRegID = Convert.ToInt32(drCust["lngCustomFieldDefRegID"]); }
                            catch { lngCustomFieldDefRegID = 0; }
                            try { lngSortOrder = Convert.ToInt32(drCust["lngSortOrder"]); }
                            catch { lngSortOrder = 0; }
                            try { decCharge = Convert.ToDecimal(drCust["decCharge"]); }
                            catch { decCharge = 0; }
                            try { strLocalCaption = Convert.ToString(drCust["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }
                            try { strFieldType = Convert.ToString(drCust["strFieldType"]); }
                            catch { strFieldType = ""; }

                            CheckBox chkUseLocalReg = new CheckBox();
                            chkUseLocalReg.Name = "chkUseLocalReg_" +lngCustomFieldDefRegID.ToString();
                            chkUseLocalReg.Checked = blnUseLocal;
                            chkUseLocalReg.Top = 9;
                            chkUseLocalReg.Left =lblUseLocalRegHead.Left;
                            chkUseLocalReg.Width = 15;

                            CheckBox chkUseOnlineReg = new CheckBox();
                            chkUseOnlineReg.Name = "chkUseOnlineReg_" +lngCustomFieldDefRegID.ToString();
                            chkUseOnlineReg.Top = 9;
                            chkUseOnlineReg.Left = lblUseOnlineRegHead.Left;
                            chkUseOnlineReg.Width = 15;
                            chkUseOnlineReg.CheckedChanged += new EventHandler(chkUseOnlineReg_CheckedChanged);

                            CheckBox chkRequiredReg = new CheckBox();
                            chkRequiredReg.Name = "chkRequiredReg_" +lngCustomFieldDefRegID.ToString();
                            chkRequiredReg.Top = 9;
                            chkRequiredReg.Left = lblRequiredRegHead.Left;
                            chkRequiredReg.Width = 15;
                            chkRequiredReg.Visible = false;

                            TextBox txtChargeReg = new TextBox();
                            txtChargeReg.Name = "txtChargeReg_" + lngCustomFieldDefRegID.ToString();
                            txtChargeReg.Top = 9;
                            txtChargeReg.Left = lblChargeRegHead.Left;
                            txtChargeReg.Width = 55;
                            txtChargeReg.Visible = false;

                            TextBox txtSortOrderReg = new TextBox();
                            txtSortOrderReg.Name = "txtSortOrderReg_" +lngCustomFieldDefRegID.ToString();
                            txtSortOrderReg.Text = lngSortOrder.ToString();
                            txtSortOrderReg.Top = 9;
                            txtSortOrderReg.Left = lblSortOrderRegHead.Left;
                            txtSortOrderReg.Width = 55;

                            TextBox txtLocalCaptionReg = new TextBox();
                            txtLocalCaptionReg.Name = "txtLocalCaptionReg_" +lngCustomFieldDefRegID.ToString();
                            txtLocalCaptionReg.Text = strLocalCaption;
                            txtLocalCaptionReg.Top = 9;
                            txtLocalCaptionReg.Left =lblLocalCaptionRegHead.Left;
                            txtLocalCaptionReg.Width = 165;
                            txtLocalCaptionReg.Enabled = false;
                            txtLocalCaptionReg.ReadOnly = true;

                            TextBox txtWebCaptionReg = new TextBox();
                            txtWebCaptionReg.Name = "txtWebCaptionReg_" +lngCustomFieldDefRegID.ToString();
                            txtWebCaptionReg.Top = 9;
                            txtWebCaptionReg.Left =lblWebCaptionRegHead.Left;
                            txtWebCaptionReg.Width = 165;
                            txtWebCaptionReg.Visible = false;

                            Label lblFieldTypeReg = new Label();
                            lblFieldTypeReg.Name = "lblFieldTypeReg_" +lngCustomFieldDefRegID.ToString();
                            lblFieldTypeReg.Top = 9;
                            lblFieldTypeReg.Text = strFieldType;
                            lblFieldTypeReg.Left = lblFieldTypeRegHead.Left;

                            Button btnDetailsReg = new Button();
                            btnDetailsReg.Name = "btnDetailsReg_" +lngCustomFieldDefRegID.ToString();
                            btnDetailsReg.Click += new EventHandler(btnDetailsReg_Click);
                            btnDetailsReg.Top = 9;
                            btnDetailsReg.Text = "Details";
                            btnDetailsReg.Width = 57;
                            btnDetailsReg.Left = txtSortOrderReg.Left + txtSortOrderReg.Width + 6;

                            Button btnDeleteReg = new Button();
                            btnDeleteReg.Name = "btnDeleteReg_" +lngCustomFieldDefRegID.ToString();
                            btnDeleteReg.Click += new EventHandler(btnDeleteReg_Click);
                            btnDeleteReg.Top = 9;
                            btnDeleteReg.Text = "Delete";
                            btnDeleteReg.Width = 55;
                            btnDeleteReg.Left = btnDetailsReg.Left + btnDetailsReg.Width + 6;

                            //holders for header/footer
                            TextBox txtHeader = new TextBox();
                            txtHeader.Name = "txtHeaderReg_" +lngCustomFieldDefRegID.ToString();
                            txtHeader.Top = 24;
                            txtHeader.Width = 55;
                            txtHeader.Left = 0;
                            txtHeader.Visible = false;

                            TextBox txtFooter = new TextBox();
                            txtFooter.Name = "txtFooterReg_" +lngCustomFieldDefRegID.ToString();
                            txtFooter.Top = 24;
                            txtFooter.Width = 55;
                            txtFooter.Left = 61;
                            txtFooter.Visible = false;

                            TextBox txtDropdownOptions = new TextBox();
                            txtDropdownOptions.Name = "txtDropdownOptionsReg_" +lngCustomFieldDefRegID.ToString();
                            txtDropdownOptions.Top = 24;
                            txtDropdownOptions.Width = 55;
                            txtDropdownOptions.Left = 122;
                            if (strFieldType == "DROPDOWN") subLoadDropdownOptionsReg(txtDropdownOptions, lngCustomFieldDefRegID, strLocalCaption);
                            txtDropdownOptions.Visible = false;

                            Panel panCustomReg = new Panel();

                            panCustomReg.Name = "panCustomReg_" +lngCustomFieldDefRegID.ToString();
                            panCustomReg.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustomReg.Width = btnDeleteReg.Left + btnDeleteReg.Width + 6;
                            panCustomReg.Height = 38;
                            panCustomReg.Left = 3;
                            panCustomReg.Top = intNextRegTop;

                            panCustomReg.Controls.Add(chkUseLocalReg);
                            panCustomReg.Controls.Add(chkUseOnlineReg);
                            panCustomReg.Controls.Add(chkRequiredReg);
                            panCustomReg.Controls.Add(txtChargeReg);
                            panCustomReg.Controls.Add(txtSortOrderReg);
                            panCustomReg.Controls.Add(txtLocalCaptionReg);
                            panCustomReg.Controls.Add(lblFieldTypeReg);
                            panCustomReg.Controls.Add(txtWebCaptionReg);
                            panCustomReg.Controls.Add(btnDetailsReg);
                            panCustomReg.Controls.Add(btnDeleteReg);
                            panCustomReg.Controls.Add(txtHeader);
                            panCustomReg.Controls.Add(txtFooter);
                            panCustomReg.Controls.Add(txtDropdownOptions);

                            pagRegCustom.Controls.Add(panCustomReg);

                            intNextRegTop += 44;
                        }

                        drCust.Close();
                    }
                }

                conDB.Close();
            }
         
            //web defs
            
            //load web reg definitions
            using (wsXferEventInfo.XferEventInfo wsDLFlags = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                strSQL = "SELECT blnRequired, " +
                            "lngCustomFieldDefRegID, " +
                            "decCharge, " +
                            "strLocalCaption, strWebCaption, strHeader, strFooter " +
                        "FROM tblCustomFieldDefReg " +
                        "WHERE lngCTUserID = " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AND " +
                            "lngProgramID = " + _lngProgramID.ToString();

                string strWebXML = "";

                try { strWebXML = wsDLFlags.fcnGetRecords(strSQL, "tblCustomVals", clsWebTalk.strWebConn); }
                catch (Exception ex) { }

                DataSet dsXML = new DataSet("tblCustomVals");

                dsXML.ReadXml(new System.IO.StringReader(strWebXML), XmlReadMode.ReadSchema);

                foreach (DataTable tbl in dsXML.Tables)
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        string strNameOnWS = "";
                        long lngIDOnWS = 0;

                        bool blnRequired = false;
                        decimal decCharge = 0;
                        string strWebCaption = "";
                        string strHeader = "";
                        string strFooter = "";

                        try { strNameOnWS = Convert.ToString(row["strLocalCaption"]); }
                        catch { strNameOnWS = ""; }

                        try { lngIDOnWS = Convert.ToInt32(row["lngCustomFieldDefRegID"]); }
                        catch { lngIDOnWS = 0; }

                        try { strWebCaption = Convert.ToString(row["strWebCaption"]); }
                        catch { strWebCaption = ""; }

                        if (lngIDOnWS == 0)
                        {
                            //field/flag hasn't been matched with a local id:  look up caption from old table to match it to web data
                            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                            {
                                conDB.Open();

                                strSQL = "SELECT tblCustomFieldDefReg.lngCustomFieldDefRegID " +
                                        "FROM tblCustomFieldDefReg " +
                                            "INNER JOIN tblCustomRegFlagDesc ON tblCustomFieldDefReg.strLocalCaption = tblCustomRegFlagDesc." + strNameOnWS;

                                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                                {
                                    try { lngIDOnWS = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                    catch { lngIDOnWS = 0; }
                                }

                                conDB.Close();
                            }
                        }

                        if (lngIDOnWS > 0)
                        {
                            try
                            {
                                try { blnRequired = Convert.ToBoolean(row["blnRequired"]); }
                                catch { blnRequired = false; }

                                try { decCharge = Convert.ToDecimal(row["decCharge"]); }
                                catch { decCharge = 0; }

                                try { strHeader = Convert.ToString(row["strHeader"]); }
                                catch { strHeader = ""; }

                                try { strFooter = Convert.ToString(row["strFooter"]); }
                                catch { strFooter = ""; }

                                Panel panCustomReg = (Panel)pagRegCustom.Controls["panCustomReg_" + lngIDOnWS.ToString()];

                                ((CheckBox)panCustomReg.Controls["chkUseOnlineReg_" + lngIDOnWS.ToString()]).Checked = true;

                                if (blnRequired) ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngIDOnWS.ToString()]).Checked = true;
                                ((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngIDOnWS.ToString()]).Text = strWebCaption;
                                ((TextBox)panCustomReg.Controls["txtChargeReg_" + lngIDOnWS.ToString()]).Text = decCharge.ToString("C");
                                ((TextBox)panCustomReg.Controls["txtHeaderReg_" + lngIDOnWS.ToString()]).Text = strHeader;
                                ((TextBox)panCustomReg.Controls["txtFooterReg_" + lngIDOnWS.ToString()]).Text = strFooter;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error retrieving custom field definitions: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        void btnDeleteReg_Click(object sender, EventArgs e)
        {
            string strMsg = "";

            strMsg = "Deleting a field cannot be undone, and any custom data will be removed with the definition.\nAre you sure you wish to delete this custom element?";

            if (MessageBox.Show(strMsg, "Delete Custom Field?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string strSQL = "";
                string strLocalCaption = "";

                string strID = ((Button)sender).Name;
                long lngCustomFieldDefRegID = 0;

                lngCustomFieldDefRegID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                Panel panCustomReg = (Panel)pagRegCustom.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()];

                strLocalCaption = ((TextBox)panCustomReg.Controls["txtLocalCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text;

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        if (((Label)panCustomReg.Controls["lblFieldTypeReg_" + lngCustomFieldDefRegID.ToString()]).Text == "DROPDOWN")
                        {
                            //clear existing options
                            strSQL = "DELETE tblCustomFieldDefRegOptions.* " +
                                    "FROM tblCustomFieldDefRegOptions " +
                                    "WHERE strLocalCaption=@strLocalCaption";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }
                        }

                        strSQL = "DELETE tblCustomFieldDefReg.* " +
                                "FROM tblCustomFieldDefReg " +
                                "WHERE strLocalCaption=@strLocalCaption";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "DELETE tblCustomFieldValReg.* " +
                                "FROM tblCustomFieldValReg " +
                                "WHERE strLocalCaption=@strLocalCaption";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }

                    conDB.Close();
                }

                subSaveCustomFieldDefReg(lngDirtyProgramID);
                subLoadCustomFieldDefRegs(lngDirtyProgramID);
            }
        }

        void btnDetailsReg_Click(object sender, EventArgs e)
        {
            string strID = ((Button)sender).Name;
            long lngCustomFieldDefRegID = 0;

            lngCustomFieldDefRegID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

            Panel panCustomReg = (Panel)pagRegCustom.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()];

            clsCustomFieldRegDef defCustomField = new clsCustomFieldRegDef();

            decimal decCharge = 0;

            try { decCharge = Convert.ToDecimal(((TextBox)panCustomReg.Controls["txtChargeReg_" + lngCustomFieldDefRegID.ToString()]).Text.Replace("$", "").Replace(",", "")); }
            catch { decCharge = 0; }
            
            defCustomField.blnRequired = ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngCustomFieldDefRegID.ToString()]).Checked;
            defCustomField.blnUseOnline = ((CheckBox)panCustomReg.Controls["chkUseOnlineReg_" + lngCustomFieldDefRegID.ToString()]).Checked;
            defCustomField.blnUseLocal = ((CheckBox)panCustomReg.Controls["chkUseLocalReg_" + lngCustomFieldDefRegID.ToString()]).Checked;
            defCustomField.decCharge = decCharge;
            defCustomField.lngSortOrder = Convert.ToInt32(((TextBox)panCustomReg.Controls["txtSortOrderReg_" + lngCustomFieldDefRegID.ToString()]).Text);
            defCustomField.mmoFooter = ((TextBox)panCustomReg.Controls["txtFooterReg_" + lngCustomFieldDefRegID.ToString()]).Text;
            defCustomField.mmoHeader = ((TextBox)panCustomReg.Controls["txtHeaderReg_" + lngCustomFieldDefRegID.ToString()]).Text;
            defCustomField.mmoWebCaption = ((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text;
            defCustomField.strFieldType = ((Label)panCustomReg.Controls["lblFieldTypeReg_" + lngCustomFieldDefRegID.ToString()]).Text;
            defCustomField.strLocalCaption = ((TextBox)panCustomReg.Controls["txtLocalCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text;
            defCustomField.strDropdownOptions = new List<string>(((TextBox)panCustomReg.Controls["txtDropdownOptionsReg_" + lngCustomFieldDefRegID.ToString()]).Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
            
            frmEditCustomFieldDefReg objEdit = new frmEditCustomFieldDefReg(defCustomField);

            if (objEdit.ShowDialog() == DialogResult.OK)
            {
                //update controls
                ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngCustomFieldDefRegID.ToString()]).Checked = objEdit.defCustomField.blnRequired;
                ((CheckBox)panCustomReg.Controls["chkUseOnlineReg_" + lngCustomFieldDefRegID.ToString()]).Checked = objEdit.defCustomField.blnUseOnline;
                ((CheckBox)panCustomReg.Controls["chkUseLocalReg_" + lngCustomFieldDefRegID.ToString()]).Checked = objEdit.defCustomField.blnUseLocal;
                ((TextBox)panCustomReg.Controls["txtChargeReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.decCharge.ToString();
                ((TextBox)panCustomReg.Controls["txtSortOrderReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.lngSortOrder.ToString();
                ((TextBox)panCustomReg.Controls["txtFooterReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.mmoFooter;
                ((TextBox)panCustomReg.Controls["txtHeaderReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.mmoHeader;
                ((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.mmoWebCaption;
                ((Label)panCustomReg.Controls["lblFieldTypeReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.strFieldType;
                ((TextBox)panCustomReg.Controls["txtLocalCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text = objEdit.defCustomField.strLocalCaption;
                ((TextBox)panCustomReg.Controls["txtDropdownOptionsReg_" + lngCustomFieldDefRegID.ToString()]).Text = "";

                for (int intI = 0; intI < objEdit.defCustomField.strDropdownOptions.Count; intI++)
                    ((TextBox)panCustomReg.Controls["txtDropdownOptionsReg_" + lngCustomFieldDefRegID.ToString()]).Text += objEdit.defCustomField.strDropdownOptions[intI] + "\r\n";
            }
        }        

        void chkUseOnlineReg_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string strID = ((CheckBox)sender).Name;
                long lngCustomFieldDefRegID = 0;
                bool blnShowWebCtls = false;

                lngCustomFieldDefRegID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                Panel panCustomReg = (Panel)pagRegCustom.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()];

                blnShowWebCtls = ((CheckBox)panCustomReg.Controls["chkUseOnlineReg_" + lngCustomFieldDefRegID.ToString()]).Checked;

                if (blnShowWebCtls)
                {
                    ((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Visible = true;

                    //only show charge field for flags, required for non-flags
                    string strFieldType = ((Label)panCustomReg.Controls["lblFieldTypeReg_" + lngCustomFieldDefRegID.ToString()]).Text;

                    if (strFieldType == "FLAG")
                    {
                        ((TextBox)panCustomReg.Controls["txtChargeReg_" + lngCustomFieldDefRegID.ToString()]).Visible = true;
                        ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngCustomFieldDefRegID.ToString()]).Visible = false;
                    }
                    else
                    {
                        ((TextBox)panCustomReg.Controls["txtChargeReg_" + lngCustomFieldDefRegID.ToString()]).Visible = false;
                        ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngCustomFieldDefRegID.ToString()]).Visible = true;
                    }

                    //update web caption to local caption if it's blank
                    if (((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text == "") ((TextBox)pagRegCustom.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text = ((TextBox)pagRegCustom.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["txtLocalCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text;
                }
                else
                {
                    ((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Visible = false;
                    ((TextBox)panCustomReg.Controls["txtChargeReg_" + lngCustomFieldDefRegID.ToString()]).Visible = false;
                    ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngCustomFieldDefRegID.ToString()]).Visible = false;
                }
            }
            catch { }
        }

        void chkUseCamperProfile_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string strID = ((CheckBox)sender).Name;
                long lngCustomFieldDefIRID = 0;
                bool blnShowWebCtls = false;

                lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                Panel panCustomIR = (Panel)pagIRCustom.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()];

                if (((CheckBox)panCustomIR.Controls["chkUseProfileIR_" + lngCustomFieldDefIRID.ToString()]).Checked)
                    blnShowWebCtls = true;

                if (((CheckBox)panCustomIR.Controls["chkUseCamperIR_" + lngCustomFieldDefIRID.ToString()]).Checked)
                    blnShowWebCtls = true;

                if (((Label)panCustomIR.Controls["lblFieldTypeIR_" +lngCustomFieldDefIRID.ToString()]).Text == "FLAG")
                    ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngCustomFieldDefIRID.ToString()]).Visible = false;
                else
                    ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngCustomFieldDefIRID.ToString()]).Visible = blnShowWebCtls;

                ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Visible = blnShowWebCtls;

                if (blnShowWebCtls)
                    if (((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text == "") ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text = ((TextBox)panCustomIR.Controls["txtLocalCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text;

            }
            catch { }
        }

        void btnDeleteIR_Click(object sender, EventArgs e)
        {
            string strMsg = "";

            strMsg = "Deleting a field cannot be undone, and any custom data will be removed with the definition.\nAre you sure you wish to delete this custom element?";

            if (MessageBox.Show(strMsg, "Delete Custom Field?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string strSQL = "";
                string strLocalCaption = "";

                string strID = ((Button)sender).Name;
                long lngCustomFieldDefIRID = 0;

                lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                Panel panCustomIR = (Panel)pagIRCustom.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()];

                strLocalCaption = ((TextBox)panCustomIR.Controls["txtLocalCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text;

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        if (((Label)panCustomIR.Controls["lblFieldTypeIR_" + lngCustomFieldDefIRID.ToString()]).Text == "DROPDOWN")
                        {
                            //clear existing options
                            strSQL = "DELETE tblCustomFieldDefIROptions.* " +
                                    "FROM tblCustomFieldDefIROptions " +
                                    "WHERE strLocalCaption=@strLocalCaption";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }
                        }

                        strSQL = "DELETE tblCustomFieldDefIR.* " +
                                "FROM tblCustomFieldDefIR " +
                                "WHERE strLocalCaption=@strLocalCaption";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "DELETE tblCustomFieldValIR.* " +
                                "FROM tblCustomFieldValIR " +
                                "WHERE strLocalCaption=@strLocalCaption";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }

                    conDB.Close();
                }

                subSaveCustomFieldDefIR(lngDirtyProgramID);
                subLoadCustomFieldDefIRs(lngDirtyProgramID);
            }
        }

        void btnDetailsIR_Click(object sender, EventArgs e)
        {
            string strID = ((Button)sender).Name;
            long lngCustomFieldDefIRID = 0;

            lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

            Panel panCustomIR = (Panel)pagIRCustom.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()];

            clsCustomFieldIRDef defCustomField = new clsCustomFieldIRDef();

            defCustomField.blnRequired = ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngCustomFieldDefIRID.ToString()]).Checked;
            defCustomField.blnUseCamper = ((CheckBox)panCustomIR.Controls["chkUseCamperIR_" + lngCustomFieldDefIRID.ToString()]).Checked;
            defCustomField.blnUseLocal = ((CheckBox)panCustomIR.Controls["chkUseLocalIR_" + lngCustomFieldDefIRID.ToString()]).Checked;
            defCustomField.blnUseProfile = ((CheckBox)panCustomIR.Controls["chkUseProfileIR_" + lngCustomFieldDefIRID.ToString()]).Checked;
            defCustomField.lngSortOrder = Convert.ToInt32(((TextBox)panCustomIR.Controls["txtSortOrderIR_" + lngCustomFieldDefIRID.ToString()]).Text);
            defCustomField.mmoFooter = ((TextBox)panCustomIR.Controls["txtFooterIR_" + lngCustomFieldDefIRID.ToString()]).Text;
            defCustomField.mmoHeader = ((TextBox)panCustomIR.Controls["txtHeaderIR_" + lngCustomFieldDefIRID.ToString()]).Text;
            defCustomField.mmoWebCaption = ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text;
            defCustomField.strFieldType = ((Label)panCustomIR.Controls["lblFieldTypeIR_" + lngCustomFieldDefIRID.ToString()]).Text;
            defCustomField.strLocalCaption = ((TextBox)panCustomIR.Controls["txtLocalCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text;
            defCustomField.strDropdownOptions = new List<string>(((TextBox)panCustomIR.Controls["txtDropdownOptionsIR_" + lngCustomFieldDefIRID.ToString()]).Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

            frmEditCustomFieldDefIR objEdit = new frmEditCustomFieldDefIR(defCustomField);

            if (objEdit.ShowDialog() == DialogResult.OK)
            {
                //update controls
                 ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngCustomFieldDefIRID.ToString()]).Checked=objEdit.defCustomField.blnRequired;
                 ((CheckBox)panCustomIR.Controls["chkUseCamperIR_" + lngCustomFieldDefIRID.ToString()]).Checked=objEdit.defCustomField.blnUseCamper;
                 ((CheckBox)panCustomIR.Controls["chkUseLocalIR_" + lngCustomFieldDefIRID.ToString()]).Checked=objEdit.defCustomField.blnUseLocal;
                 ((CheckBox)panCustomIR.Controls["chkUseProfileIR_" + lngCustomFieldDefIRID.ToString()]).Checked=objEdit.defCustomField.blnUseProfile;
                 ((TextBox)panCustomIR.Controls["txtSortOrderIR_" + lngCustomFieldDefIRID.ToString()]).Text=objEdit.defCustomField.lngSortOrder.ToString();
                 ((TextBox)panCustomIR.Controls["txtFooterIR_" + lngCustomFieldDefIRID.ToString()]).Text=objEdit.defCustomField.mmoFooter;
                 ((TextBox)panCustomIR.Controls["txtHeaderIR_" + lngCustomFieldDefIRID.ToString()]).Text=objEdit.defCustomField.mmoHeader;
                 ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text=objEdit.defCustomField.mmoWebCaption;
                 ((Label)panCustomIR.Controls["lblFieldTypeIR_" + lngCustomFieldDefIRID.ToString()]).Text=objEdit.defCustomField.strFieldType;
                 ((TextBox)panCustomIR.Controls["txtLocalCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text=objEdit.defCustomField.strLocalCaption;

                 ((TextBox)panCustomIR.Controls["txtDropdownOptionsIR_" + lngCustomFieldDefIRID.ToString()]).Text = "";

                 for (int intI = 0; intI < objEdit.defCustomField.strDropdownOptions.Count; intI++)
                     ((TextBox)panCustomIR.Controls["txtDropdownOptionsIR_" + lngCustomFieldDefIRID.ToString()]).Text += objEdit.defCustomField.strDropdownOptions[intI] + "\r\n";
            }
        }

        private void subFillGrid()
        {
            try
            {
                string strSQL = "";

                strSQL = "SELECT tblCustomFieldsGiftDef.intSortOrder, " +
                            "tblCustomFieldsGiftDef.strFieldName, tblCustomFieldsGiftDef.strFieldType, tblCustomFieldsGiftDef.strValidation " +
                        "FROM tblCustomFieldsGiftDef " +
                        "ORDER BY tblCustomFieldsGiftDef.intSortOrder, " +
                            "tblCustomFieldsGiftDef.strFieldName";

                using (OleDbDataAdapter daGiftFieldsLocal = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn))
                {
                    using (OleDbCommandBuilder cmdGiftFieldsLocal = new OleDbCommandBuilder(daGiftFieldsLocal))
                    {
                        // Populate a new data table and bind it to the BindingSource.
                        using (DataTable tblGiftFieldsLocal = new DataTable())
                        {
                            tblGiftFieldsLocal.Locale = System.Globalization.CultureInfo.InvariantCulture;

                            daGiftFieldsLocal.Fill(tblGiftFieldsLocal);

                            grdCustomGiftFieldsLocal.DataSource = tblGiftFieldsLocal;
                        }
                    }
                }

                grdCustomGiftFieldsLocal.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmSyncCustomFlags.subFillGrid", ex);
            }
        }

        private void btnAddCustomGiftField_Click(object sender, EventArgs e)
        {
            using (frmAddCustomGiftField objAddCustomGiftField = new frmAddCustomGiftField())
            {
                if (objAddCustomGiftField.ShowDialog() == DialogResult.OK)
                {
                    //add field
                    string strSQL = "";

                    strSQL = "INSERT INTO tblCustomFieldsGiftDef " +
                            "( blnRequired, " +
                                "intSortOrder, " +
                                "strFieldName, strFieldType, strDefaultVal, strValidation ) " +
                            "VALUES " +
                            "(@blnRequired, " +
                                "@intSortOrder, " +
                                "@strFieldName, @strFieldType, @strDefaultVal, @strValidation )";

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            cmdDB.Parameters.Add(new OleDbParameter("@blnRequired", objAddCustomGiftField.blnRequired));
                            cmdDB.Parameters.Add(new OleDbParameter("@intSortOrder", objAddCustomGiftField.intSortOrder));
                            cmdDB.Parameters.Add(new OleDbParameter("@strFieldName", objAddCustomGiftField.strFieldName));
                            cmdDB.Parameters.Add(new OleDbParameter("@strFieldType", objAddCustomGiftField.strFieldType));
                            cmdDB.Parameters.Add(new OleDbParameter("@strDefaultVal", objAddCustomGiftField.strDefaultVal));
                            cmdDB.Parameters.Add(new OleDbParameter("@strValidation", objAddCustomGiftField.strValidation));

                            try { cmdDB.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("There was an error adding the custom field: " + ex.Message); }
                        }

                        conDB.Close();

                        //refresh grid
                        subFillGrid();
                    }
                }
            }
        }

        private void grdCustomGiftFieldsLocal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string strFieldName = "";

            try
            {
                if (grdCustomGiftFieldsLocal.Columns[e.ColumnIndex].Name == "colEdit")
                {
                    if (e.RowIndex >= 0)
                    {
                        strFieldName = grdCustomGiftFieldsLocal.Rows[e.RowIndex].Cells["colFieldName"].Value.ToString();

                        using (CustomGiftFields.frmEditCustomGiftField objEditCustomGiftField = new global::CTWebMgmt.Admin.CustomGiftFields.frmEditCustomGiftField(strFieldName))
                        {
                            if (objEditCustomGiftField.ShowDialog() == DialogResult.OK)
                            {
                                //update field
                                string strSQL = "";

                                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                                {
                                    conDB.Open();

                                    strSQL = "UPDATE tblCustomFieldsGiftDef " +
                                            "SET tblCustomFieldsGiftDef.blnRequired = @blnRequired, " +
                                                "tblCustomFieldsGiftDef.intSortOrder = @intSortOrder, " +
                                                "tblCustomFieldsGiftDef.strFieldName = @strFieldName, tblCustomFieldsGiftDef.strFieldType = @strFieldType, tblCustomFieldsGiftDef.strDefaultVal = @strDefaultVal, tblCustomFieldsGiftDef.strValidation = @strValidation " +
                                            "WHERE tblCustomFieldsGiftDef.strFieldName = @strFieldNameCriter";

                                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                                    {
                                        cmdDB.Parameters.Add(new OleDbParameter("@blnRequired", objEditCustomGiftField.blnRequired));
                                        cmdDB.Parameters.Add(new OleDbParameter("@intSortOrder", objEditCustomGiftField.intSortOrder));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strFieldName", objEditCustomGiftField.strFieldName));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strFieldType", objEditCustomGiftField.strFieldType));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strDefaultVal", objEditCustomGiftField.strDefaultVal));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strValidation", objEditCustomGiftField.strValidation));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strFieldNameCriter", strFieldName));

                                        try { cmdDB.ExecuteNonQuery(); }
                                        catch (Exception ex) { MessageBox.Show("There was an error editing the gift: " + ex.Message); }
                                    }

                                    conDB.Close();
                                }

                                subFillGrid();
                            }
                        }
                    }
                }
                else if (grdCustomGiftFieldsLocal.Columns[e.ColumnIndex].Name == "colDelete")
                {
                    if (e.RowIndex >= 0)
                    {
                        strFieldName = grdCustomGiftFieldsLocal.Rows[e.RowIndex].Cells["colFieldName"].Value.ToString();

                        if (MessageBox.Show("Are you sure you wish to delete the custom field '" + strFieldName + "'?", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                            {
                                conDB.Open();

                                string strSQL = "";

                                strSQL = "DELETE tblCustomFieldsGiftDef.* " +
                                        "FROM tblCustomFieldsGiftDef " +
                                        "WHERE tblCustomFieldsGiftDef.strFieldName = @strFieldName";

                                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                                {
                                    cmdDB.Parameters.Add(new OleDbParameter("@strFieldName", strFieldName));

                                    try { cmdDB.ExecuteNonQuery(); }
                                    catch { }
                                }

                                conDB.Close();
                            }

                            subFillGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr(this.Name + ".grdCustomGiftFieldsLocal_CellClick", ex);
            }
        }

        private void subSaveAll()
        {
            if (MessageBox.Show("Custom field definitions on the web server will be over-written.\n\nAre you sure you wish to continue?", "CampTrak", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                subSaveCustomFieldDefIR(lngDirtyProgramID);
                subSaveCustomFieldDefReg(lngDirtyProgramID);

                subSaveGiftFields();

                MessageBox.Show("Save Complete");
            }
        }

        public string fcnDev(string _strFileName)
        {
            string strRes = "";

            XmlDocument docIn = new XmlDocument();

            docIn.Load(_strFileName);

            XmlNode xmlDef = docIn.ChildNodes[1];

            long lngProgramID = 0;
            long lngCTUserID = 0;

            try { lngProgramID = Convert.ToInt32(xmlDef.Attributes["ProgramID"].Value); }
            catch { lngProgramID = 0; }

            try { lngCTUserID = Convert.ToInt32(xmlDef.Attributes["CTUserID"].Value); }
            catch { lngCTUserID = 0; }

            string strSQL = "";

            try
            {
                {

                    //first clear current defs
                    strSQL = "DELETE " +
                            "FROM tblCustomFieldDefIR " +
                            "WHERE lngProgramID=@lngProgramID AND " +
                                "lngCTUserID=@lngCTUserID";

                    {

                        for (int intField = 0; intField < xmlDef.ChildNodes.Count; intField++)
                        {
                            XmlNode xmlField = xmlDef.ChildNodes[intField];

                            long lngCustomFieldDefIRID = 0;
                            bool blnUseCamper = false;
                            bool blnUseProfile = false;
                            bool blnRequired = false;
                            long lngSortOrder = 0;
                            string strLocalCaption = "";
                            string strWebCaption = "";
                            string strHeader = "";
                            string strFooter = "";
                            string strFieldType = "";

                            try { lngCustomFieldDefIRID = Convert.ToInt32(xmlField["lngCustomFieldDefIRID"].InnerText); }
                            catch { lngCustomFieldDefIRID = 0; }

                            try { blnUseCamper = Convert.ToBoolean(xmlField["blnUseCamper"].InnerText); }
                            catch { blnUseCamper = false; }

                            try { blnUseProfile = Convert.ToBoolean(xmlField["blnUseProfile"].InnerText); }
                            catch { blnUseProfile = false; }

                            try { blnRequired = Convert.ToBoolean(xmlField["blnRequired"].InnerText); }
                            catch { blnRequired = false; }

                            try { lngSortOrder = Convert.ToInt32(xmlField["lngSortOrder"].InnerText); }
                            catch { lngSortOrder = 0; }

                            try { strLocalCaption = xmlField["strLocalCaption"].InnerText; }
                            catch { strLocalCaption = ""; }

                            try { strWebCaption = xmlField["strWebCaption"].InnerText; }
                            catch { strWebCaption = ""; }

                            try { strHeader = xmlField["strHeader"].InnerText; }
                            catch { strHeader = ""; }

                            try { strFooter = xmlField["strFooter"].InnerText; }
                            catch { strFooter = ""; }

                            try { strFieldType = xmlField["strFieldType"].InnerText; }
                            catch { strFieldType = "FIELD"; }

                            strSQL = "INSERT INTO tblCustomFieldDefIR " +
                                    "(blnUseCamper, blnUseProfile, blnRequired, " +
                                        "lngCTUserID, lngCustomFieldDefIRID, lngProgramID, lngSortOrder, " +
                                        "strLocalCaption, strWebCaption, strHeader, strFooter, strFieldType) " +
                                    "VALUES " +
                                    "(@blnUseCamper, @blnUseProfile, @blnRequired, " +
                                        "@lngCTUserID, @lngCustomFieldDefIRID, @lngProgramID, @lngSortOrder, " +
                                        "@strLocalCaption, @strWebCaption, @strHeader, @strFooter, @strFieldType)";


                            if (strFieldType == "DROPDOWN")
                            {
                                //clear existing options
                                strSQL = "DELETE FROM tblCustomFieldDefIROptions " +
                                        "WHERE strLocalCaption=@strLocalCaption AND " +
                                            "lngCTUserID=@lngCTUserID";


                                //parse and add option values
                                XmlNode xmlOptions = xmlField["strDropdownOptions"];

                                for (int intOption = 0; intOption < xmlOptions.ChildNodes.Count; intOption++)
                                {
                                    string strOption = "";

                                    try { strOption = xmlOptions.ChildNodes[intOption].InnerText; }
                                    catch { strOption = ""; }

                                    if (strOption != "")
                                    {
                                        strSQL = "INSERT INTO tblCustomFieldDefIROptions " +
                                                "(lngCTUserID, " +
                                                    "strLocalCaption, strValue) " +
                                                "VALUES " +
                                                "(@lngCTUserID, " +
                                                    "@strLocalCaption, @strValue)";

                                    }
                                }
                            }
                        }
                    }
                }

                //try to delete file

            }
            catch (Exception ex) { strRes = ex.Message; }

            return strRes;
        }

        private void subSetExcludeFromGeneral(long _lngProgramID)
        {
            //commit 'chkExcludeFromGeneral'
            using (wsXferEventInfo.XferEventInfo svcCTWeb = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                if (!svcCTWeb.fcnSetExcludeFromGeneral(clsAppSettings.GetAppSettings().lngCTUserID, _lngProgramID, chkExcludeFromGeneral.Checked, clsWebTalk.strWebConn)) MessageBox.Show("There was an error committing 'blnExcludeFromGeneral'!");

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tlkpCampName " +
                    "SET blnExcludeFromGeneral = @blnExcludeFromGeneral " +
                    "WHERE lngCampID=@lngCampID";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@blnExcludeFromGeneral", chkExcludeFromGeneral.Checked);
                    cmdDB.Parameters.AddWithValue("@lngCampID", _lngProgramID);

                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }
                }

                conDB.Close();
            }
        }

        private void subSaveCustomFieldDefIR(long _lngProgramID)
        {
            //commit 'chkExcludeFromGeneral'
            subSetExcludeFromGeneral(_lngProgramID);

            //open set of local defs to save
            string strSQL = "";            

            string strFile = DateTime.Now.ToString("yyyyMMdd") + "_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + "CustomFieldDefIR.xml";
            string strFileName = System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strFile;
            string strWebFile = "C:\\inetpub\\wwwroot\\camptrak.com\\fileuls\\" + strFile;

            using (XmlWriter xmlOut = XmlWriter.Create(strFileName))
            {
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("CustomFieldDefIRs");
                xmlOut.WriteAttributeString("ProgramID", _lngProgramID.ToString());
                xmlOut.WriteAttributeString("CTUserID", clsAppSettings.GetAppSettings().lngCTUserID.ToString());

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblCustomFieldDefIR.lngCustomFieldDefIRID " +
                            "FROM tblCustomFieldDefIR";

                    List<long> lngCustomFieldDefIRIDs = new List<long>();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drCust = cmdDB.ExecuteReader())
                        {
                            while (drCust.Read())
                            {
                                long lngCustomFieldDefIRID = 0;

                                try { lngCustomFieldDefIRID = Convert.ToInt32(drCust["lngCustomFieldDefIRID"]); }
                                catch { lngCustomFieldDefIRID = 0; }

                                lngCustomFieldDefIRIDs.Add(lngCustomFieldDefIRID);
                            }

                            drCust.Close();
                        }

                        for (int intI = 0; intI < lngCustomFieldDefIRIDs.Count; intI++)
                        {
                            long lngCustomFieldDefIRID = lngCustomFieldDefIRIDs[intI];

                            Panel panCustomIR = (Panel)pagIRCustom.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()];

                            bool blnUseLocal = false;
                            long lngSortOrder = 0;
                            string strLocalCaption = "";
                            string strFieldType = "";

                            try { blnUseLocal = ((CheckBox)panCustomIR.Controls["chkUseLocalIR_" + lngCustomFieldDefIRID.ToString()]).Checked; }
                            catch { blnUseLocal = false; }

                            try { lngSortOrder = Convert.ToInt32(((TextBox)panCustomIR.Controls["txtSortOrderIR_" + lngCustomFieldDefIRID.ToString()]).Text); }
                            catch { lngSortOrder = 0; }

                            try { strLocalCaption = ((TextBox)panCustomIR.Controls["txtLocalCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strLocalCaption = ""; }

                            try { strFieldType = ((Label)panCustomIR.Controls["lblFieldTypeIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strFieldType = "FIELD"; }

                            strSQL = "UPDATE tblCustomFieldDefIR " +
                                    "SET tblCustomFieldDefIR.blnUseLocal = @blnUseLocal, " +
                                        "tblCustomFieldDefIR.lngSortOrder = @lngSortOrder, " +
                                        "tblCustomFieldDefIR.strLocalCaption = @strLocalCaption " +
                                    "WHERE tblCustomFieldDefIR.lngCustomFieldDefIRID=@lngCustomFieldDefIRID";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@blnUseLocal", blnUseLocal);
                            cmdDB.Parameters.AddWithValue("@lngSortOrder", lngSortOrder);
                            cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                            cmdDB.Parameters.AddWithValue("@lngCustomFieldDefIRID", lngCustomFieldDefIRID);

                            cmdDB.ExecuteNonQuery();

                            //build xml of web data to commit
                            bool blnUseProfile = false;
                            bool blnUseCamper = false;
                            bool blnRequired = false;
                            string strWebCaption = "";
                            string strHeader = "";
                            string strFooter = "";

                            try { blnUseProfile = ((CheckBox)panCustomIR.Controls["chkUseProfileIR_" + lngCustomFieldDefIRID.ToString()]).Checked; }
                            catch { blnUseProfile = false; }

                            try { blnUseCamper = ((CheckBox)panCustomIR.Controls["chkUseCamperIR_" + lngCustomFieldDefIRID.ToString()]).Checked; }
                            catch { blnUseCamper = false; }

                            try { blnRequired = ((CheckBox)panCustomIR.Controls["chkRequiredIR_" + lngCustomFieldDefIRID.ToString()]).Checked; }
                            catch { blnRequired = false; }

                            try { strWebCaption = ((TextBox)panCustomIR.Controls["txtWebCaptionIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strWebCaption = ""; }

                            try { strHeader = ((TextBox)panCustomIR.Controls["txtHeaderIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strHeader = ""; }

                            try { strFooter = ((TextBox)panCustomIR.Controls["txtFooterIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strFooter = ""; }

                            if ((blnUseCamper || blnUseProfile) && strWebCaption != "")
                            {
                                //generate xml of definition
                                xmlOut.WriteStartElement("CustomFieldDefIR");

                                xmlOut.WriteElementString("lngCTUserID", clsAppSettings.GetAppSettings().lngCTUserID.ToString());
                                xmlOut.WriteElementString("lngCustomFieldDefIRID", lngCustomFieldDefIRID.ToString());
                                xmlOut.WriteElementString("blnUseCamper", blnUseCamper.ToString());
                                xmlOut.WriteElementString("blnUseProfile", blnUseProfile.ToString());
                                xmlOut.WriteElementString("blnRequired", blnRequired.ToString());
                                xmlOut.WriteElementString("lngProgramID", _lngProgramID.ToString());
                                xmlOut.WriteElementString("lngSortOrder", lngSortOrder.ToString());
                                xmlOut.WriteElementString("strLocalCaption", strLocalCaption);
                                xmlOut.WriteElementString("strWebCaption", strWebCaption);
                                xmlOut.WriteElementString("strHeader", strHeader);
                                xmlOut.WriteElementString("strFooter", strFooter);
                                xmlOut.WriteElementString("strFieldType", strFieldType);

                                //write dd options
                                xmlOut.WriteStartElement("strDropdownOptions");
                                
                                List<string> strDropdownOptions = new List<string>(((TextBox)panCustomIR.Controls["txtDropdownOptionsIR_" + lngCustomFieldDefIRID.ToString()]).Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

                                for (int intOption = 0; intOption < strDropdownOptions.Count; intOption++)
                                    xmlOut.WriteElementString("strValue", strDropdownOptions[intOption]);

                                xmlOut.WriteEndElement();

                                xmlOut.WriteEndElement();
                            }
                        }

                        xmlOut.WriteEndElement();
                        xmlOut.WriteEndDocument();
                    }

                    conDB.Close();
                }
            }

            //fcnDev(strFileName);

            //upload xml doc of web field defs
            using (wsXferEventInfo.XferEventInfo xferEventInfo = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                string strWebRes = "";

                System.Net.WebClient myWebClient = new System.Net.WebClient();

                string fileName = strFileName;

                try
                {
                    byte[] responseArray = myWebClient.UploadFile(clsWebTalk.strPOSTFileURI, fileName);

                    // Decode and display the response.
                    string strULRes = "";

                    try { strULRes = System.Text.Encoding.ASCII.GetString(responseArray); }
                    catch { strULRes = "ERR"; }

                    //tell server to process uploaded file
                    strWebRes = xferEventInfo.fcnCommitCustomFieldDefIR(strWebFile, clsWebTalk.strWebConn);
                }
                catch (System.Net.WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (strWebRes != "")
                    MessageBox.Show("There was an error uploading the custom flag and field definitions to the web server.");
            }

            //delete temp local file
            System.IO.File.Delete(strFileName);
        }

        private void subSaveCustomFieldDefReg(long _lngProgramID)
        {
            //open set of local defs to save
            string strSQL = "";

            string strFile = DateTime.Now.ToString("yyyyMMdd") + "_" + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + "CustomFieldDefReg.xml";
            string strFileName = System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath) + "\\" + strFile;
            string strWebFile = "C:\\inetpub\\wwwroot\\camptrak.com\\fileuls\\" + strFile;

            using (XmlWriter xmlOut = XmlWriter.Create(strFileName))
            {
                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("CustomFieldDefRegs");
                xmlOut.WriteAttributeString("ProgramID", _lngProgramID.ToString());
                xmlOut.WriteAttributeString("CTUserID", clsAppSettings.GetAppSettings().lngCTUserID.ToString());

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblCustomFieldDefReg.lngCustomFieldDefRegID " +
                            "FROM tblCustomFieldDefReg";

                    List<long> lngCustomFieldDefRegIDs = new List<long>();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drCust = cmdDB.ExecuteReader())
                        {
                            while (drCust.Read())
                            {
                                long lngCustomFieldDefRegID = 0;

                                try { lngCustomFieldDefRegID = Convert.ToInt32(drCust["lngCustomFieldDefRegID"]); }
                                catch { lngCustomFieldDefRegID = 0; }

                                lngCustomFieldDefRegIDs.Add(lngCustomFieldDefRegID);
                            }

                            drCust.Close();
                        }

                        for (int intI = 0; intI < lngCustomFieldDefRegIDs.Count; intI++)
                        {
                            long lngCustomFieldDefRegID = lngCustomFieldDefRegIDs[intI];
                            Panel panCustomReg = (Panel)pagRegCustom.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()];

                            bool blnUseLocal = false;
                            long lngSortOrder = 0;
                            decimal decCharge = 0;
                            string strLocalCaption = "";
                            string strFieldType="";

                            try { blnUseLocal = ((CheckBox)panCustomReg.Controls["chkUseLocalReg_" + lngCustomFieldDefRegID.ToString()]).Checked; }
                            catch { blnUseLocal = false; }

                            try { lngSortOrder = Convert.ToInt32(((TextBox)panCustomReg.Controls["txtSortOrderReg_" + lngCustomFieldDefRegID.ToString()]).Text); }
                            catch { lngSortOrder = 0; }

                            try { decCharge = Convert.ToDecimal(panCustomReg.Controls["txtChargeReg_" + lngCustomFieldDefRegID.ToString()].Text.Replace("$", "").Replace(",", "")); }
                            catch { decCharge = 0; }

                            try { strLocalCaption = ((TextBox)panCustomReg.Controls["txtLocalCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text; }
                            catch { strLocalCaption = ""; }

                            try { strFieldType = ((Label)panCustomReg.Controls["lblFieldTypeReg_" +lngCustomFieldDefRegID.ToString()]).Text; }
                            catch { strFieldType = "FIELD"; }

                            strSQL = "UPDATE tblCustomFieldDefReg " +
                                    "SET tblCustomFieldDefReg.blnUseLocal = @blnUseLocal, " +
                                        "tblCustomFieldDefReg.lngSortOrder = @lngSortOrder, " +
                                        "tblCustomFieldDefReg.decCharge = @decCharge, " +
                                        "tblCustomFieldDefReg.strLocalCaption = @strLocalCaption " +
                                    "WHERE tblCustomFieldDefReg.lngCustomFieldDefRegID=@lngCustomFieldDefRegID";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@blnUseLocal", blnUseLocal);
                            cmdDB.Parameters.AddWithValue("@lngSortOrder", lngSortOrder);
                            cmdDB.Parameters.AddWithValue("@decCharge", decCharge);
                            cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                            cmdDB.Parameters.AddWithValue("@lngCustomFieldDefRegID", lngCustomFieldDefRegID);

                            cmdDB.ExecuteNonQuery();

                            //build xml of web data to commit
                            bool blnRequired = false;
                            string strWebCaption = "";
                            string strHeader = "";
                            string strFooter = "";

                            try { blnRequired = ((CheckBox)panCustomReg.Controls["chkRequiredReg_" + lngCustomFieldDefRegID.ToString()]).Checked; }
                            catch { blnRequired = false; }

                            try { strWebCaption = ((TextBox)panCustomReg.Controls["txtWebCaptionReg_" + lngCustomFieldDefRegID.ToString()]).Text; }
                            catch { strWebCaption = ""; }

                            try { strHeader = ((TextBox)panCustomReg.Controls["txtHeaderReg_" + lngCustomFieldDefRegID.ToString()]).Text; }
                            catch { strHeader = ""; }

                            try { strFooter = ((TextBox)panCustomReg.Controls["txtFooterReg_" + lngCustomFieldDefRegID.ToString()]).Text; }
                            catch { strFooter = ""; }

                            if (strWebCaption != "" && ((CheckBox)panCustomReg.Controls["chkUseOnlineReg_"+lngCustomFieldDefRegID.ToString()]).Checked)
                            {
                                //generate xml of definition
                                xmlOut.WriteStartElement("CustomFieldDefReg");

                                xmlOut.WriteElementString("lngCTUserID", clsAppSettings.GetAppSettings().lngCTUserID.ToString());
                                xmlOut.WriteElementString("lngCustomFieldDefRegID", lngCustomFieldDefRegID.ToString());
                                xmlOut.WriteElementString("blnRequired", blnRequired.ToString());
                                xmlOut.WriteElementString("lngProgramID", _lngProgramID.ToString());
                                xmlOut.WriteElementString("lngSortOrder", lngSortOrder.ToString());
                                xmlOut.WriteElementString("decCharge", decCharge.ToString());
                                xmlOut.WriteElementString("strLocalCaption", strLocalCaption);
                                xmlOut.WriteElementString("strWebCaption", strWebCaption);
                                xmlOut.WriteElementString("strHeader", strHeader);
                                xmlOut.WriteElementString("strFooter", strFooter);
                                xmlOut.WriteElementString("strFieldType", strFieldType);

                                //write dd options
                                xmlOut.WriteStartElement("strDropdownOptions");

                                List<string> strDropdownOptions = new List<string>(((TextBox)panCustomReg.Controls["txtDropdownOptionsReg_" + lngCustomFieldDefRegID.ToString()]).Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

                                for (int intOption = 0; intOption < strDropdownOptions.Count; intOption++)
                                    xmlOut.WriteElementString("strValue", strDropdownOptions[intOption]);

                                xmlOut.WriteEndElement();

                                xmlOut.WriteEndElement();
                            }
                        }

                        xmlOut.WriteEndElement();
                        xmlOut.WriteEndDocument();
                    }

                    conDB.Close();
                }
            }

            //upload xml doc of web field defs
            using (wsXferEventInfo.XferEventInfo xferEventInfo = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                string strWebRes = "";

                System.Net.WebClient myWebClient = new System.Net.WebClient();

                string fileName = strFileName;

                try
                {
                    byte[] responseArray = myWebClient.UploadFile(clsWebTalk.strPOSTFileURI, fileName);

                    // Decode and display the response.
                    string strULRes = "";

                    try { strULRes = System.Text.Encoding.ASCII.GetString(responseArray); }
                    catch { strULRes = "ERR"; }

                    //tell server to process uploaded file
                    strWebRes = xferEventInfo.fcnCommitCustomFieldDefReg(strWebFile, clsWebTalk.strWebConn);
                }
                catch (System.Net.WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (strWebRes != "")
                    MessageBox.Show("There was an error uploading the custom flag and field definitions to the web server.");
            }

            //delete temp local file
            System.IO.File.Delete(strFileName);
        }

        private void subSaveGiftFields()
        {
            string strSQL;
            string strULRes = "";

            this.Cursor = Cursors.WaitCursor;

            //upload 
            strSQL = "SELECT blnRequired, " +
                        "intSortOrder, " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AS lngCTUserID, " +
                        "strFieldName, strFieldDesc, strFieldType, strDefaultVal, strValidation " +
                    "FROM tblCustomFieldsGiftDef";

            strULRes = clsWebTalk.fcnULNoUpdate(strSQL, "tblCustomFieldsGiftDef", "custom gift field");

            strSQL = "SELECT intSortOrder, " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AS lngCTUserID, " +
                        "strFieldName, strLookupOption " +
                    "FROM tblCustomFieldsGiftLookupOptions";

            strULRes = clsWebTalk.fcnULNoUpdate(strSQL, "tblCustomFieldsGiftLookupOptions", "custom gift lookup options");

            this.Cursor = Cursors.Default;
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            subSaveAll();
        }

        private void cboProgramSpecificDef_MouseDown(object sender, MouseEventArgs e)
        {
            cboProgramSpecificDef.DroppedDown = true;
        }

        private void cboProgramSpecificDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lngProgramID = 0;

            try { lngProgramID = ((clsCboItem)cboProgramSpecificDef.SelectedItem).ID; }
            catch { lngProgramID = 0; }

            if (lngProgramID != lngDirtyProgramID)
            {
                string strMsg = "Would you like to commit the " + strDirtyProgram + " program before editing a different one?";

                if (MessageBox.Show(strMsg, "CampTrak Web Manager", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //commit
                    subSaveCustomFieldDefIR(lngDirtyProgramID);
                    subSaveCustomFieldDefReg(lngDirtyProgramID);
                }

                lngDirtyProgramID = lngProgramID;
                strDirtyProgram = ((clsCboItem)cboProgramSpecificDef.SelectedItem).Item;

                //load new program
                subLoadCustomFieldDefIRs(lngProgramID);
                subLoadCustomFieldDefRegs(lngProgramID);

                if (lngProgramID > 0)
                {
                    chkExcludeFromGeneral.Visible = true;

                    chkExcludeFromGeneral.Checked = fcnGetExcludeFromGeneral(lngProgramID);
                }
                else
                    chkExcludeFromGeneral.Visible = false;
            }
        }

        private bool fcnGetExcludeFromGeneral(long _lngProgramID)
        {
            string strSQL = "";
            bool blnRes = false;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT blnExcludeFromGeneral " +
                        "FROM tlkpCampName " +
                        "WHERE lngCampID=" + _lngProgramID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { blnRes = Convert.ToBoolean(cmdDB.ExecuteScalar()); }
                    catch { blnRes = false; }
                }

                conDB.Close();
            }

            return blnRes;
        }

        private void btnAddFieldIR_Click(object sender, EventArgs e)
        {
            frmAddCustomFieldDefIR objAddField = new frmAddCustomFieldDefIR();

            if (objAddField.ShowDialog() == DialogResult.OK)
            {
                //add new panel
                CheckBox chkUseLocal = new CheckBox();
                chkUseLocal.Name = "chkUseLocalIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                chkUseLocal.Checked = objAddField.defNewField.blnUseLocal;
                chkUseLocal.Top = 9;
                chkUseLocal.Left = lblUseLocalHead.Left;
                chkUseLocal.Width = 15;

                CheckBox chkUseProfile = new CheckBox();
                chkUseProfile.Name = "chkUseProfileIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                chkUseProfile.Checked = objAddField.defNewField.blnUseProfile;
                chkUseProfile.Top = 9;
                chkUseProfile.Left = lblUseProfileHead.Left;
                chkUseProfile.Width = 15;
                chkUseProfile.CheckedChanged += new EventHandler(chkUseCamperProfile_CheckedChanged);

                CheckBox chkUseCamper = new CheckBox();
                chkUseCamper.Name = "chkUseCamperIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                chkUseCamper.Checked = objAddField.defNewField.blnUseCamper;
                chkUseCamper.Top = 9;
                chkUseCamper.Left = lblUseCamperHead.Left;
                chkUseCamper.Width = 15;
                chkUseCamper.CheckedChanged += new EventHandler(chkUseCamperProfile_CheckedChanged);

                CheckBox chkRequired = new CheckBox();
                chkRequired.Name = "chkRequiredIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                chkRequired.Top = 9;
                chkRequired.Left = lblRequiredHead.Left;
                chkRequired.Checked = objAddField.defNewField.blnRequired;
                chkRequired.Width = 15;
                chkRequired.Visible = false;

                TextBox txtSortOrder = new TextBox();
                txtSortOrder.Name = "txtSortOrderIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                txtSortOrder.Text = objAddField.defNewField.lngSortOrder.ToString();
                txtSortOrder.Top = 9;
                txtSortOrder.Left = lblSortOrderHead.Left;
                txtSortOrder.Width = 55;

                TextBox txtLocalCaption = new TextBox();
                txtLocalCaption.Name = "txtLocalCaptionIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                txtLocalCaption.Text = objAddField.defNewField.strLocalCaption;
                txtLocalCaption.Top = 9;
                txtLocalCaption.Left = lblLocalLabelHead.Left;
                txtLocalCaption.Width = 165;
                txtLocalCaption.Enabled = false;
                txtLocalCaption.ReadOnly = true;

                TextBox txtWebCaption = new TextBox();
                txtWebCaption.Name = "txtWebCaptionIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                txtWebCaption.Top = 9;
                txtWebCaption.Left = lblOnlineLabelHead.Left;
                txtWebCaption.Text = objAddField.defNewField.mmoWebCaption;
                txtWebCaption.Width = 165;
                txtWebCaption.Visible = false;

                Label lblFieldType = new Label();
                lblFieldType.Name = "lblFieldTypeIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                lblFieldType.Top = 9;
                lblFieldType.Text = objAddField.defNewField.strFieldType;
                lblFieldType.Left = lblFieldTypeHead.Left;

                Button btnDetails = new Button();
                btnDetails.Name = "btnDetailsIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                btnDetails.Click += new EventHandler(btnDetailsIR_Click);
                btnDetails.Top = 9;
                btnDetails.Text = "Details";
                btnDetails.Width = 57;
                btnDetails.Left = txtSortOrder.Left + txtSortOrder.Width + 6;

                Button btnDelete = new Button();
                btnDelete.Name = "btnDeleteIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                btnDelete.Click += new EventHandler(btnDeleteIR_Click);
                btnDelete.Top = 9;
                btnDelete.Text = "Delete";
                btnDelete.Width = 55;
                btnDelete.Left = btnDetails.Left + btnDetails.Width + 6;

                //holders for header/footer
                TextBox txtHeader = new TextBox();
                txtHeader.Name = "txtHeaderIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                txtHeader.Top = 24;
                txtHeader.Text = objAddField.defNewField.mmoHeader;
                txtHeader.Width = 55;
                txtHeader.Left = 0;
                txtHeader.Visible = false;

                TextBox txtFooter = new TextBox();
                txtFooter.Name = "txtFooterIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                txtFooter.Top = 24;
                txtFooter.Text = objAddField.defNewField.mmoFooter;
                txtFooter.Width = 55;
                txtFooter.Left = 61;
                txtFooter.Visible = false;

                TextBox txtDropdownOptions = new TextBox();
                txtDropdownOptions.Name = "txtDropdownOptionsIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                txtDropdownOptions.Top = 24;
                txtDropdownOptions.Width = 55;
                txtDropdownOptions.Left = 122;
                if (objAddField.defNewField.strFieldType == "DROPDOWN")
                {
                    for (int intI = 0; intI < objAddField.defNewField.strDropdownOptions.Count; intI++)
                        txtDropdownOptions.Text += objAddField.defNewField.strDropdownOptions[intI] + "\r\n";
                }
                txtDropdownOptions.Visible = false;

                Panel panCustomIR = new Panel();

                panCustomIR.Name = "panCustomIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString();
                panCustomIR.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                panCustomIR.Width = btnDelete.Left + btnDelete.Width + 6;
                panCustomIR.Height = 38;
                panCustomIR.Left = 3;
                panCustomIR.Top = intNextIRTop;
                intNextIRTop += 44;

                panCustomIR.Controls.Add(chkUseLocal);
                panCustomIR.Controls.Add(chkUseProfile);
                panCustomIR.Controls.Add(chkUseCamper);
                panCustomIR.Controls.Add(chkRequired);
                panCustomIR.Controls.Add(txtSortOrder);
                panCustomIR.Controls.Add(txtLocalCaption);
                panCustomIR.Controls.Add(lblFieldType);
                panCustomIR.Controls.Add(txtWebCaption);
                panCustomIR.Controls.Add(btnDetails);
                panCustomIR.Controls.Add(btnDelete);
                panCustomIR.Controls.Add(txtHeader);
                panCustomIR.Controls.Add(txtFooter);
                panCustomIR.Controls.Add(txtDropdownOptions);

                pagIRCustom.Controls.Add(panCustomIR);

                chkUseCamperProfile_CheckedChanged(panCustomIR.Controls["chkUseProfileIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString()], null);
                chkUseCamperProfile_CheckedChanged(panCustomIR.Controls["chkUseCamperIR_" + objAddField.defNewField.lngCustomFieldDefIRID.ToString()], null);
            }
        }

        private void btnAddFieldReg_Click(object sender, EventArgs e)
        {
            frmAddCustomFieldDefReg objAddField = new frmAddCustomFieldDefReg();

            if (objAddField.ShowDialog() == DialogResult.OK)
            {
                //add new panel
                CheckBox chkUseLocal = new CheckBox();
                chkUseLocal.Name = "chkUseLocalReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                chkUseLocal.Checked = objAddField.defNewField.blnUseLocal;
                chkUseLocal.Top = 9;
                chkUseLocal.Left = lblUseLocalRegHead.Left;
                chkUseLocal.Width = 15;

                TextBox txtSortOrder = new TextBox();
                txtSortOrder.Name = "txtSortOrderReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtSortOrder.Text = objAddField.defNewField.lngSortOrder.ToString();
                txtSortOrder.Top = 9;
                txtSortOrder.Left =lblSortOrderRegHead.Left;
                txtSortOrder.Width = 55;

                CheckBox chkUseOnlineReg = new CheckBox();
                chkUseOnlineReg.Name = "chkUseOnlineReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                chkUseOnlineReg.Top = 9;
                chkUseOnlineReg.Left = lblUseOnlineRegHead.Left;
                chkUseOnlineReg.Width = 15;
                chkUseOnlineReg.Checked = objAddField.defNewField.blnUseOnline;
                chkUseOnlineReg.CheckedChanged += new EventHandler(chkUseOnlineReg_CheckedChanged);

                CheckBox chkRequiredReg = new CheckBox();
                chkRequiredReg.Name = "chkRequiredReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                chkRequiredReg.Top = 9;
                chkRequiredReg.Left = lblRequiredRegHead.Left;
                chkRequiredReg.Width = 15;
                chkRequiredReg.Checked = objAddField.defNewField.blnRequired;
                chkRequiredReg.Visible = false;

                TextBox txtChargeReg = new TextBox();
                txtChargeReg.Name = "txtChargeReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtChargeReg.Top = 9;
                txtChargeReg.Left = lblChargeRegHead.Left;
                txtChargeReg.Width = 55;
                txtChargeReg.Text = objAddField.defNewField.decCharge.ToString();
                txtChargeReg.Visible = false;

                TextBox txtLocalCaption = new TextBox();
                txtLocalCaption.Name = "txtLocalCaptionReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtLocalCaption.Text = objAddField.defNewField.strLocalCaption;
                txtLocalCaption.Top = 9;
                txtLocalCaption.Left =lblLocalCaptionRegHead.Left;
                txtLocalCaption.Width = 165;
                txtLocalCaption.Enabled = false;
                txtLocalCaption.ReadOnly = true;

                TextBox txtWebCaption = new TextBox();
                txtWebCaption.Name = "txtWebCaptionReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtWebCaption.Top = 9;
                txtWebCaption.Left =lblWebCaptionRegHead.Left;
                txtWebCaption.Text = objAddField.defNewField.mmoWebCaption;
                txtWebCaption.Width = 165;
                txtWebCaption.Visible = false;

                Label lblFieldType = new Label();
                lblFieldType.Name = "lblFieldTypeReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                lblFieldType.Top = 9;
                lblFieldType.Text = objAddField.defNewField.strFieldType;
                lblFieldType.Left =lblFieldTypeRegHead.Left;

                Button btnDetails = new Button();
                btnDetails.Name = "btnDetailsReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                btnDetails.Click += new EventHandler(btnDetailsReg_Click);
                btnDetails.Top = 9;
                btnDetails.Text = "Details";
                btnDetails.Width = 57;
                btnDetails.Left = txtSortOrder.Left + txtSortOrder.Width + 6;

                Button btnDelete = new Button();
                btnDelete.Name = "btnDeleteReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                btnDelete.Click += new EventHandler(btnDeleteReg_Click);
                btnDelete.Top = 9;
                btnDelete.Text = "Delete";
                btnDelete.Width = 55;
                btnDelete.Left = btnDetails.Left + btnDetails.Width + 6;

                //holders for header/footer
                TextBox txtHeader = new TextBox();
                txtHeader.Name = "txtHeaderReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtHeader.Top = 24;
                txtHeader.Text = objAddField.defNewField.mmoHeader;
                txtHeader.Width = 55;
                txtHeader.Left = 0;
                txtHeader.Visible = false;

                TextBox txtFooter = new TextBox();
                txtFooter.Name = "txtFooterReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtFooter.Top = 24;
                txtFooter.Text = objAddField.defNewField.mmoFooter;
                txtFooter.Width = 55;
                txtFooter.Left = 61;
                txtFooter.Visible = false;

                TextBox txtDropdownOptions = new TextBox();
                txtDropdownOptions.Name = "txtDropdownOptionsReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                txtDropdownOptions.Top = 24;
                txtDropdownOptions.Width = 55;
                txtDropdownOptions.Left = 122;
                if (objAddField.defNewField.strFieldType == "DROPDOWN")
                {
                    for (int intI = 0; intI < objAddField.defNewField.strDropdownOptions.Count; intI++)
                        txtDropdownOptions.Text += objAddField.defNewField.strDropdownOptions[intI] + "\r\n";
                }
                txtDropdownOptions.Visible = false;

                Panel panCustomReg = new Panel();

                panCustomReg.Name = "panCustomReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString();
                panCustomReg.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                panCustomReg.Width = btnDelete.Left + btnDelete.Width + 6;
                panCustomReg.Height = 38;
                panCustomReg.Left = 3;
                panCustomReg.Top = intNextRegTop;
                intNextRegTop += 44;

                panCustomReg.Controls.Add(chkUseLocal);
                panCustomReg.Controls.Add(chkUseOnlineReg);
                panCustomReg.Controls.Add(chkRequiredReg);
                panCustomReg.Controls.Add(txtChargeReg);
                panCustomReg.Controls.Add(txtSortOrder);
                panCustomReg.Controls.Add(txtLocalCaption);
                panCustomReg.Controls.Add(lblFieldType);
                panCustomReg.Controls.Add(txtWebCaption);
                panCustomReg.Controls.Add(btnDetails);
                panCustomReg.Controls.Add(btnDelete);
                panCustomReg.Controls.Add(txtHeader);
                panCustomReg.Controls.Add(txtFooter);
                panCustomReg.Controls.Add(txtDropdownOptions);

                pagRegCustom.Controls.Add(panCustomReg);

                chkUseOnlineReg_CheckedChanged(panCustomReg.Controls["chkUseOnlineReg_" + objAddField.defNewField.lngCustomFieldDefRegID.ToString()], null);
            }
        }
    }
}