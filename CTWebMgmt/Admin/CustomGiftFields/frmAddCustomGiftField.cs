using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt.Admin
{
    public partial class frmAddCustomGiftField : Form
    {
        public string strFieldName = "";
        public string strFieldType = "";
        public bool blnRequired = false;
        public string strDefaultVal = "";
        public string strValidation = "";
        public string strFieldDesc = "";
        public int intSortOrder = 0;

        public frmAddCustomGiftField()
        {
            InitializeComponent();
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
    }
}
