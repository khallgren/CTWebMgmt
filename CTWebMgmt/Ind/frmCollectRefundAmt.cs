using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.Ind
{
    public partial class frmCollectRefundAmt : Form
    {
        public decimal decAmt = 0;

        public frmCollectRefundAmt(long _lngWebRegistrationID)
        {
            InitializeComponent();

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblWebIndRegistrations.curDeposit, tblWebIndRegistrations.curSpendingMoney, tblWebIndRegistrations.curDonation " +
                        "FROM tblWebIndRegistrations " +
                        "WHERE tblWebIndRegistrations.lngRegistrationWebID=" + _lngWebRegistrationID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drReg = cmdDB.ExecuteReader())
                    {
                        if (drReg.Read())
                        {
                            decimal decDeposit = 0;
                            decimal decSpending = 0;
                            decimal decDonation = 0;

                            try { decDeposit = Convert.ToDecimal(drReg["curDeposit"]); }
                            catch { decDeposit = 0; }

                            try { decSpending = Convert.ToDecimal(drReg["curSpendingMoney"]); }
                            catch { decSpending = 0; }

                            try { decDonation = Convert.ToDecimal(drReg["curDonation"]); }
                            catch { decDonation = 0; }

                            lblDeposit.Text = decDeposit.ToString("C");
                            lblSpending.Text = decSpending.ToString("C");
                            lblDonation.Text = decDonation.ToString("C");

                            txtAmt.Text = (decDeposit + decSpending).ToString();
                        }

                        drReg.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void frmCollectRefundAmt_Load(object sender, EventArgs e)
        {

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
