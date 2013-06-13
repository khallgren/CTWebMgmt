using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt.Admin.Reports
{
    public partial class frmReconcileMerchantGateway : Form
    {
        private rptReconcileMerchantGateway rptToPrint;

        public frmReconcileMerchantGateway()
        {
            InitializeComponent();
        }

        private void frmReconcileMerchantGateway_Load(object sender, EventArgs e)
        {
            subConfigureCrystalReports();
        }

        private void subConfigureCrystalReports()
        {
            string strSQL = "";

            strSQL = "SELECT tblMerchantGatewayTrans.* " +
                    "FROM tblMerchantGatewayTrans " +
                    "ORDER BY tblMerchantGatewayTrans.dteTransDate";

            conCTMain_B.ConnectionString = clsAppSettings.GetAppSettings().strCTConn;

            daMerchantGatewayTrans.SelectCommand.CommandText = strSQL;
            daMerchantGatewayTrans.Fill(dsMerchantGatewayTrans, "tblMerchantGatewayTrans");

            rptToPrint = new rptReconcileMerchantGateway();

            rptToPrint.SetDataSource(dsMerchantGatewayTrans);

            rvwReconcileMerchantGateway.ReportSource = rptToPrint;
        }

        private void mnuHowToUse_Click(object sender, EventArgs e)
        {
            using (frmReconileMerchantGatewayInst objInst = new frmReconileMerchantGatewayInst())
                objInst.ShowDialog();
        }
    }
}
