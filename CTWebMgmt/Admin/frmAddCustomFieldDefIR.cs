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
    public partial class frmAddCustomFieldDefIR : Form
    {
        public clsCustomFieldIRDef defNewField;

        public frmAddCustomFieldDefIR()
        {
            InitializeComponent();
            defNewField = new clsCustomFieldIRDef();
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

                        //commit addition to local definition
                        /////////////////////////////////////////
                        string strLocalCaption = "";
                        bool blnUseLocal = false;

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
                            if (cboFieldType.SelectedItem.ToString() == "DROPDOWN")
                            {
                                if (txtDropdownOptions.Text == "")
                                {
                                    MessageBox.Show("Please enter options for the dropdown list or select a different field type.");
                                    return;
                                }
                            }

                                //make sure there's no conflict w/ an existing field
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
                                
                            //append definition
                                strSQL = "INSERT INTO tblCustomFieldDefIR " +
                                        "(blnUseLocal, " +
                                            "lngSortOrder, " +
                                            "strLocalCaption, strFieldType ) " +
                                        "VALUES " +
                                        "(@blnUseLocal, " +
                                            "@lngSortOrder, " +
                                            "@strLocalCaption, @strFieldType)";                            

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@blnUseLocal", blnUseLocal);
                            cmdDB.Parameters.AddWithValue("@lngSortOrder", lngSortOrder);
                            cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                            cmdDB.Parameters.AddWithValue("@strFieldType", cboFieldType.SelectedItem.ToString());
                            
                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }

                            strSQL = "SELECT @@IDENTITY;";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            try { defNewField.lngCustomFieldDefIRID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                            catch { defNewField.lngCustomFieldDefIRID = 0; }

                            //update options for dropdowns
                            if (cboFieldType.SelectedItem.ToString() == "DROPDOWN")
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

                defNewField.blnRequired = chkRequired.Checked;
                defNewField.blnUseCamper = chkUseCamper.Checked;
                defNewField.blnUseLocal = chkUseLocal.Checked;
                defNewField.blnUseProfile = chkUseProfile.Checked;
                defNewField.lngSortOrder = lngSortOrder;
                defNewField.mmoFooter = txtFooter.Text;
                defNewField.mmoHeader = txtHeader.Text;
                defNewField.mmoWebCaption = txtWebCaption.Text;
                defNewField.strFieldType = cboFieldType.SelectedItem.ToString();
                defNewField.strLocalCaption = txtLocalCaption.Text;
                defNewField.strDropdownOptions = new List<string>(txtDropdownOptions.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

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

        private void frmAddCustomFieldDefIR_Load(object sender, EventArgs e)
        {
            txtWebCaption.Visible = false;
            txtHeader.Visible = false;
            txtFooter.Visible = false;
            lblWebCaption.Visible = false;
            lblHeader.Visible = false;
            lblFooter.Visible = false;
            chkRequired.Visible = false;

            chkUseLocal.Checked = true;

            cboFieldType.SelectedIndex = 0;
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

                if (cboFieldType.SelectedItem.ToString() != "FLAG")
                    chkRequired.Visible = true;

                if (txtWebCaption.Text == "")
                    txtWebCaption.Text = txtLocalCaption.Text;
            }
        }

        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFieldType.SelectedItem.ToString() == "DROPDOWN")
            {
                this.Width = 698;

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
    }
}
