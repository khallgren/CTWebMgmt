using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.GGCC
{
    public partial class frmCollectRefundAmt : Form
    {
        public decimal decAmt = 0;

        public frmCollectRefundAmt(long _lngGGCCRegistrationWebID)
        {
            InitializeComponent();

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT curDeposit " +
                        "FROM tblWebGGCCRegistrations " +
                        "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegistrationWebID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    decimal decDeposit = 0;

                    try { decDeposit = Convert.ToDecimal(cmdDB.ExecuteScalar()); }
                    catch { decDeposit = 0; }

                    lblDeposit.Text = decDeposit.ToString("C");

                    txtAmt.Text = decDeposit.ToString();
                }

                conDB.Close();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try { decAmt = Convert.ToDecimal(txtAmt.Text); }
            catch { decAmt = 0; }

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
