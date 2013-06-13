using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Donor
{
    public partial class frmAddXCVault : Form
    {
        public frmAddXCVault()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            XCTransaction2.XChargeTransaction objXC = new XCTransaction2.XChargeTransaction();

            try
            {                
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand())
                    {
                        cmdDB.Connection = conDB;

                        string strXChargePath = "";
                        string strXWebID = "";
                        string strAuthKey = "";
                        string strTerminalID = "";

                        clsLiveCharge.subXChargeVars(cmdDB, ref strXChargePath, ref strXWebID, ref strAuthKey, ref strTerminalID);

                        string strAcct="";
                        string strErr="";

                        objXC.XCArchiveVaultAdd((int)this.Handle, strXChargePath, "Creating Vault Entry", true, true, "1518", "", "", "ALLOW", out strAcct, out strErr);

                        txtRes.Text = strErr + strAcct;
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
    }
}
