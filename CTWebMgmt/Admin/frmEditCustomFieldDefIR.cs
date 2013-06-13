using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Admin
{
    public partial class frmEditCustomFieldDefIR : Form
    {
        public clsCustomFieldIRDef defCustomField;

        public frmEditCustomFieldDefIR(clsCustomFieldIRDef _defCustomField)
        {
            InitializeComponent();
            defCustomField = _defCustomField;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                long lngSortOrder = 0;

                try { lngSortOrder = Convert.ToInt32(txtSortOrder.Text); }
                catch { lngSortOrder = 0; }

                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        //commit changes to local definition
                        /////////////////////////////////////////
                        string strOldCaption = "";
                        string strLocalCaption = "";
                        bool blnUseLocal = false;

                        strOldCaption = defCustomField.strLocalCaption;

                        strLocalCaption = txtLocalCaption.Text;
                        blnUseLocal = chkUseLocal.Checked;

                        if (strLocalCaption == "")
                        {
                            MessageBox.Show("Please enter a caption.");
                            txtLocalCaption.Focus();
                            return;
                        }
                        else
                        {
                            if (lblFieldType.Text == "DROPDOWN")
                            {
                                if (txtDropdownOptions.Text == "")
                                {
                                    MessageBox.Show("Please enter options for the dropdown list or select a different field type.");
                                    return;
                                }
                            }

                            if (strLocalCaption != strOldCaption)
                            {
                                //local caption has been changed: make sure there's no conflict w/ an existing field
                                strSQL = "SELECT COUNT(lngCustomFieldDefIRID) AS intMatches " +
                                        "FROM tblCustomFieldDefIR " +
                                        "WHERE strLocalCaption=@strLocalCaption";

                                cmdDB.CommandText = strSQL;

                                cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);

                                int intMatches = 0;

                                try { intMatches = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                catch { intMatches = 0; }

                                if (intMatches > 0)
                                {
                                    MessageBox.Show("The name '" + strLocalCaption + "' conflicts with an existing custom field.");
                                    return;
                                }

                                //update values to match new definition
                                strSQL = "UPDATE tblCustomFieldValIR " +
                                        "SET strLocalCaption=@strLocalCaption " +
                                        "WHERE strLocalCaption=@strOldCaption";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                                cmdDB.Parameters.AddWithValue("@strOldCaption", strOldCaption);

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }
                            }

                            //update definition
                            strSQL = "UPDATE tblCustomFieldDefIR " +
                                    "SET blnUseLocal=@blnUseLocal, " +
                                        "strLocalCaption=@strLocalCaption " +
                                    "WHERE strLocalCaption=@strOldCaption";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@blnUseLocal", blnUseLocal);
                            cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                            cmdDB.Parameters.AddWithValue("@strOldCaption", strOldCaption);

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }

                            //update options for dropdowns
                            if (lblFieldType.Text == "DROPDOWN")
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

                                //add each option to dropdown definition
                                List<string> strOptions = new List<string>(txtDropdownOptions.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

                                for (int intI = 0; intI < strOptions.Count; intI++)
                                {
                                    strSQL = "INSERT INTO tblCustomFieldDefIROptions " +
                                            "( intSortOrder, " +
                                                "strLocalCaption, strValue ) " +
                                            "SELECT 0 AS intSortOrder, " +
                                                "@strLocalCaption AS strLocalCaption, @strValue AS strValue";

                                    cmdDB.CommandText = strSQL;
                                    cmdDB.Parameters.Clear();

                                    cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                                    cmdDB.Parameters.AddWithValue("@strValue", strOptions[intI]);

                                    try { cmdDB.ExecuteNonQuery(); }
                                    catch { }
                                }
                            }
                        }
                    }

                    conDB.Close();
                }

                defCustomField.blnRequired = chkRequired.Checked;
                defCustomField.blnUseCamper = chkUseCamper.Checked;
                defCustomField.blnUseLocal = chkUseLocal.Checked;
                defCustomField.blnUseProfile = chkUseProfile.Checked;
                defCustomField.lngSortOrder = lngSortOrder;
                defCustomField.mmoFooter = txtFooter.Text;
                defCustomField.mmoHeader = txtHeader.Text;
                defCustomField.mmoWebCaption = txtWebCaption.Text;
                defCustomField.strFieldType = lblFieldType.Text;
                defCustomField.strLocalCaption = txtLocalCaption.Text;
                defCustomField.strDropdownOptions = new List<string>(txtDropdownOptions.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error updating the custom field definition: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmEditCustomFieldDefIR_Load(object sender, EventArgs e)
        {
            txtWebCaption.Visible = false;
            txtHeader.Visible = false;
            txtFooter.Visible = false;
            lblWebCaption.Visible = false;
            lblHeader.Visible = false;
            lblFooter.Visible = false;
            chkRequired.Visible = false;

            lblFieldType.Text = defCustomField.strFieldType;
            chkRequired.Checked = defCustomField.blnRequired;
            chkUseCamper.Checked = defCustomField.blnUseCamper;
            chkUseLocal.Checked = defCustomField.blnUseLocal;
            chkUseProfile.Checked = defCustomField.blnUseProfile;
            txtSortOrder.Text = defCustomField.lngSortOrder.ToString();
            txtFooter.Text = defCustomField.mmoFooter;
            txtHeader.Text = defCustomField.mmoHeader;
            txtWebCaption.Text = defCustomField.mmoWebCaption;
            txtLocalCaption.Text = defCustomField.strLocalCaption;

            if (defCustomField.strFieldType == "DROPDOWN")
            {
                this.Width = 698;

                for (int intI = 0; intI < defCustomField.strDropdownOptions.Count; intI++)
                    txtDropdownOptions.Text += defCustomField.strDropdownOptions[intI] + "\r\n";

                //load dropdown options
                lblDropdownOptions.Visible = true;
                txtDropdownOptions.Visible = true;
            }
            else
            {
                this.Width = 449;
                lblDropdownOptions.Visible = false;
                txtDropdownOptions.Visible = false;
            }
        }

        private void chkUseProfileCamper_CheckedChanged(object sender, EventArgs e)
        {
            txtWebCaption.Visible = false;
            txtHeader.Visible = false;
            txtFooter.Visible = false;
            lblWebCaption.Visible = false;
            lblHeader.Visible = false;
            lblFooter.Visible = false;
            chkRequired.Visible = false;

            if (chkUseCamper.Checked || chkUseProfile.Checked)
            {
                txtWebCaption.Visible = true;
                txtHeader.Visible = true;
                txtFooter.Visible = true;
                lblWebCaption.Visible = true;
                lblHeader.Visible = true;
                lblFooter.Visible = true;

                if (lblFieldType.Text != "FLAG")
                    chkRequired.Visible = true;
            }
        }
    }
}