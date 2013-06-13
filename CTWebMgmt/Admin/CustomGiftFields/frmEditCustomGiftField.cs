using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Admin.CustomGiftFields
{
    public partial class frmEditCustomGiftField : Form
    {
        public string strFieldName = "";
        public string strFieldType = "";
        public bool blnRequired = false;
        public string strDefaultVal = "";
        public string strValidation = "";
        public string strFieldDesc = "";
        public int intSortOrder = 0;

        public frmEditCustomGiftField(string _strFieldName)
        {
            InitializeComponent();

            strFieldName = _strFieldName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //validate input
            if (txtFieldName.Text == "")
            {
                MessageBox.Show("Please enter a field name or click 'Cancel'.");
                txtFieldName.Focus();
                return;
            }
            
            if (cboFieldType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a field type or click 'Cancel'.");
                cboFieldType.Focus();
                return;
            }

            if (cboValidation.SelectedIndex < 0) cboValidation.SelectedIndex = 0;

            if (txtSortOrder.Text == "") txtSortOrder.Text = "0";

            if (txtSortOrder.Text != "" && txtSortOrder.Text != "0")
            {
                try { intSortOrder = Convert.ToInt32(txtSortOrder.Text); }
                catch { intSortOrder = 0; }

                if (intSortOrder <= 0)
                {
                    MessageBox.Show("Please enter a number for sort order.");
                    txtSortOrder.Focus();
                    return;
                }
            }

            strFieldName = txtFieldName.Text;
            strFieldType = cboFieldType.SelectedItem.ToString();
            blnRequired = chkRequired.Checked;
            strDefaultVal = txtDefaultVal.Text;
            strValidation = cboValidation.SelectedItem.ToString();
            strFieldDesc = "";
            intSortOrder = Convert.ToInt32(txtSortOrder.Text);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmEditCustomGiftField_Load(object sender, EventArgs e)
        {
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strSQL = "";

                strSQL = "SELECT strFieldName, strFieldType, blnRequired, strDefaultVal, strValidation, intSortOrder " +
                        "FROM tblCustomFieldsGiftDef " +
                        "WHERE strFieldName=@strFieldName";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.Add(new OleDbParameter("@strFieldName", strFieldName));

                    using (OleDbDataReader drFlds = cmdDB.ExecuteReader())
                    {
                        if (drFlds.Read())
                        {
                            txtFieldName.Text = Convert.ToString(drFlds["strFieldName"]);
                            cboFieldType.SelectedIndex = cboFieldType.FindString(Convert.ToString(drFlds["strFieldType"]));
                            chkRequired.Checked = Convert.ToBoolean(drFlds["blnRequired"]);
                            txtDefaultVal.Text = Convert.ToString(drFlds["strDefaultVal"]);
                            cboValidation.SelectedIndex = cboValidation.FindString(Convert.ToString(drFlds["strValidation"]));
                            txtSortOrder.Text = Convert.ToString(drFlds["intSortOrder"]);
                        }

                        drFlds.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnLookupOptions_Click(object sender, EventArgs e)
        {
            using (frmCustomGiftLookupOptions objLookupOptions = new frmCustomGiftLookupOptions(strFieldName))
            {
                objLookupOptions.ShowDialog();
            }
        }

        private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFieldType.SelectedItem.ToString() == "Dropdown")
                btnLookupOptions.Visible = true;
            else
                btnLookupOptions.Visible = false;
        }
    }
}