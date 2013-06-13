using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;


namespace CTWebMgmt
{
    public partial class frmSystemSetup : Form
    {
        public frmSystemSetup()
        {
            InitializeComponent();
        }

        private void subSetCtlVisibility()
        {
            lblSQLDatabase.Visible = radSQLServer.Checked;
            lblSQLPassword.Visible = radSQLServer.Checked;
            lblSQLServer.Visible = radSQLServer.Checked;
            lblSQLUsername.Visible = radSQLServer.Checked;

            txtSQLDatabase.Visible = radSQLServer.Checked;
            txtSQLPassword.Visible = radSQLServer.Checked;
            txtSQLServer.Visible = radSQLServer.Checked;
            txtSQLUsername.Visible = radSQLServer.Checked;
            txtSQLServer.Visible = radSQLServer.Checked;

            lblPOSConnection.Visible = radMSAccess.Checked;
            txtCTConnection.Visible = radMSAccess.Checked;
            btnFindCTMain_B.Visible = radMSAccess.Checked;
        }

        private void radMSAccess_CheckedChanged(object sender, EventArgs e)
        {
            subSetCtlVisibility();
        }

        private void frmSystemSetup_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            clsCboItem cboCCMethodID;

            //-------------------settings tab init --------------------
            txtStationID.Text = Settings.Default.lngWSID.ToString();

            //----------------Connect Tab init --------------------------------
            txtCTConnection.Text = Settings.Default.MainPath;

            txtSQLServer.Text = Settings.Default.SQLServer;
            txtSQLDatabase.Text = Settings.Default.SQLDatabase;
            txtSQLUsername.Text = Settings.Default.SQLUserName;
            txtSQLPassword.Text = Settings.Default.SQLPassword;

            if (CTWebMgmt.blnUseSQLServer)
            {
                radSQLServer.Checked = true;
                radMSAccess.Checked = false;
            }
            else
            {
                radSQLServer.Checked = false;
                radMSAccess.Checked = true;
            }

            subSetCtlVisibility();

            if (txtCTConnection.Text == "D:\\projects\\CampTrak\\Data\\ctmain_b.mdb")
                btnDev.Visible = true;
            else
                btnDev.Visible = false;

            //------------------cc setup----------------------
            foreach (int lngCCMethod in Enum.GetValues(typeof(clsGlobalEnum.conLIVECHARGE)))
            {

                cboCCMethodID = new clsCboItem(lngCCMethod, Enum.GetName(typeof(clsGlobalEnum.conLIVECHARGE), lngCCMethod));

                cboCCMethod.Items.Add(cboCCMethodID);
            }

            txtXChargePath.Text = clsAppSettings.GetAppSettings().strXChargePath;
            txtCashLinqUser.Text = clsAppSettings.GetAppSettings().strCashLinqUName;
            txtCashLinqPW.Text = clsAppSettings.GetAppSettings().strCashLinqPW;
            txtCashLinqCQUser.Text = clsAppSettings.GetAppSettings().strCashLinqCQUser;
            txtCashLinqCQPW.Text = clsAppSettings.GetAppSettings().strCashLinqCQPW;
            txtCashLinqCQMerchantID.Text = clsAppSettings.GetAppSettings().strCashLinqCQMerchantID;

            lblXChargePath.Visible = false;
            txtXChargePath.Visible = false;
            btnSetXChargePath.Visible = false;
            lblCashLinqUser.Visible = false;
            txtCashLinqUser.Visible = false;
            lblCashLinqPW.Visible = false;
            txtCashLinqPW.Visible = false;

            lblCashLinqCQUser.Visible = false;
            lblCashLinqCQPW.Visible = false;
            lblCashLinqCQMerchantID.Visible = false;
            txtCashLinqCQUser.Visible = false;
            txtCashLinqCQPW.Visible = false;
            txtCashLinqCQMerchantID.Visible = false;

            if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
            {
                txtXChargePath.Visible = true;
                btnSetXChargePath.Visible = true;
                lblXChargePath.Visible = true;
            }
            else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
            {
                txtCashLinqUser.Visible = true;
                txtCashLinqPW.Visible = true;
                lblCashLinqUser.Visible = true;
                lblCashLinqPW.Visible = true;

                lblCashLinqCQUser.Visible = true;
                lblCashLinqCQPW.Visible = true;
                lblCashLinqCQMerchantID.Visible = true;
                txtCashLinqCQUser.Visible = true;
                txtCashLinqCQPW.Visible = true;
                txtCashLinqCQMerchantID.Visible = true;
            }
            else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.XChargeXML)
            {
                lblXChargeXWebID.Visible = true;
                txtXChargeXWebID.Visible = true;
                lblXChargeAuthKey.Visible = true;
                lblXChargeXWebID.Visible = true;
            }

            try
            {
                //campaign codes
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tlkpCampaignCodes.lngCampaignID, " +
                                "tlkpCampaignCodes.strCampaignCode " +
                            "FROM tlkpCampaignCodes " +
                            "WHERE tlkpCampaignCodes.blnActive=True " +
                            "ORDER BY tlkpCampaignCodes.strCampaignCode";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drCampaigns = cmdDB.ExecuteReader())
                        {
                            while (drCampaigns.Read())
                            {
                                clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drCampaigns["lngCampaignID"]), Convert.ToString(drCampaigns["strCampaignCode"]));

                                cboDefaultCampaign.Items.Add(cboNew);
                                cboDXCampaign.Items.Add(new clsCboItem(cboNew.ID, cboNew.Item));
                            }

                            drCampaigns.Close();
                        }

                        strSQL = "SELECT tblGiftCategory.lngGiftCategoryID, " +
                                    "[strGiftCategoryChoice] & \", \" & [strGiftAllocation] & IIf(Not ([strGiftSubAllocation]=\"None\"),\", \" & [strGiftSubAllocation]) AS strGiftCat " +
                                "FROM ((tblGiftCategory " +
                                    "LEFT JOIN tlkpGiftCategoryChoice ON tblGiftCategory.lngGiftCategoryChoiceID = tlkpGiftCategoryChoice.lngGiftCategoryChoiceID) " +
                                    "LEFT JOIN tlkpGiftAllocation ON tblGiftCategory.lngGiftAllocation = tlkpGiftAllocation.lngGiftAllocation) " +
                                    "LEFT JOIN tlkpGiftSubAllocation ON tblGiftCategory.lngGiftSubAllocation = tlkpGiftSubAllocation.lngGiftSubAllocation " +
                                "ORDER BY tlkpGiftCategoryChoice.strGiftCategoryChoice, tlkpGiftAllocation.strGiftAllocation, tlkpGiftSubAllocation.strGiftSubAllocation";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        using (OleDbDataReader drGiftCat = cmdDB.ExecuteReader())
                        {
                            while (drGiftCat.Read())
                            {
                                clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drGiftCat["lngGiftCategoryID"]), Convert.ToString(drGiftCat["strGiftCat"]));

                                cboIndRegCategory.Items.Add(cboNew);
                                cboDXCategory.Items.Add(new clsCboItem(cboNew.ID, cboNew.Item));
                            }

                            drGiftCat.Close();
                        }

                        strSQL = "SELECT lngLiveCharge, lngOLGiftCampaign, lngOLGiftCategoryID, lngDXCampaignID, lngDXCategoryID, " +
                                    "strXChargeWebID, strXChargeAuthKey, strEncPassPhrase, strTextClientID, strMDCustomerID " +
                                "FROM tblCampDefaults";

                        cmdDB.CommandText = strSQL;

                        using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                        {
                            if (drDef.Read())
                            {
                                try
                                {
                                    txtClientID.Text = Convert.ToString(drDef["strTextClientID"]);

                                    for (int intI = 0; intI < cboCCMethod.Items.Count; intI++)
                                    {
                                        if (((clsCboItem)cboCCMethod.Items[intI]).ID == Convert.ToInt32(drDef["lngLiveCharge"]))
                                            cboCCMethod.SelectedIndex = intI;
                                    }

                                    txtXChargeXWebID.Text = Convert.ToString(drDef["strXChargeWebID"]);
                                    txtXChargeAuthKey.Text = Convert.ToString(drDef["strXChargeAuthKey"]);

                                    txtMDCustomerID.Text = Convert.ToString(drDef["strMDCustomerID"]);

                                    long lngOLGiftCampaign = 0;
                                    long lngOLGiftCategoryID = 0;
                                    long lngDXCampaignID = 0;
                                    long lngDXCategoryID = 0;

                                    try { lngOLGiftCampaign = Convert.ToInt32(drDef["lngOLGiftCampaign"]); }
                                    catch { lngOLGiftCampaign = 0; }

                                    try { lngOLGiftCategoryID = Convert.ToInt32(drDef["lngOLGiftCategoryID"]); }
                                    catch { lngOLGiftCategoryID = 0; }

                                    try { lngDXCampaignID = Convert.ToInt32(drDef["lngDXCampaignID"]); }
                                    catch { lngDXCampaignID = 0; }

                                    try { lngDXCategoryID = Convert.ToInt32(drDef["lngDXCategoryID"]); }
                                    catch { lngDXCategoryID = 0; }

                                    clsCboItem.subSetSelectedIndex(ref cboDefaultCampaign, lngOLGiftCampaign);
                                    clsCboItem.subSetSelectedIndex(ref cboIndRegCategory, lngOLGiftCategoryID);
                                    clsCboItem.subSetSelectedIndex(ref cboDXCampaign, lngDXCampaignID);
                                    clsCboItem.subSetSelectedIndex(ref cboDXCategory, lngDXCategoryID);
                                }
                                catch (Exception ex) { clsErr.subLogErr("frmSystemSetup.Load", ex); }
                            }

                            drDef.Close();
                        }

                        try
                        {
                            //load trans types
                            strSQL = "SELECT tlkpTransType.lngTransTypeID, " +
                                        "IIf(IsNull([tblCustomTransType].[strCustomTransTypeLabel]),[tlkpTransType].[strTransType],[tblCustomTransType].[strCustomTransTypeLabel]) AS strTransType " +
                                    "FROM tlkpTransType " +
                                        "LEFT JOIN tblCustomTransType ON tlkpTransType.lngTransTypeID = tblCustomTransType.lngCustomTransTypeID " +
                                    "WHERE tlkpTransType.blnDebitCredit = 0";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            using (OleDbDataReader drTrans = cmdDB.ExecuteReader())
                            {
                                cboTransTypeForCustomRegFlagCharge.Items.Add(new clsCboItem(0, ""));

                                while (drTrans.Read())
                                {
                                    clsCboItem itmNew = new clsCboItem(Convert.ToInt32(drTrans["lngTransTypeID"]), Convert.ToString(drTrans["strTransType"]));

                                    cboTransTypeForCustomRegFlagCharge.Items.Add(itmNew);
                                }

                                drTrans.Close();
                            }

                            //get current trans type
                            bool blnCapsWebProcessing = false;
                            long lngDefCustRegFlagTransType = 0;

                            strSQL = "SELECT blnCapsWebProcessing, " +
                                        "lngDefCustRegFlagTransType " +
                                    "FROM tblCampDefaults";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                            {
                                if (drDef.Read())
                                {
                                    try { blnCapsWebProcessing = Convert.ToBoolean(drDef["blnCapsWebProcessing"]); }
                                    catch { blnCapsWebProcessing = false; }

                                    try { lngDefCustRegFlagTransType = Convert.ToInt32(drDef["lngDefCustRegFlagTransType"]); }
                                    catch { lngDefCustRegFlagTransType = 0; }
                                }

                                drDef.Close();
                            }

                            for (int intI = 0; intI < cboTransTypeForCustomRegFlagCharge.Items.Count; intI++)
                            {
                                if (lngDefCustRegFlagTransType == ((clsCboItem)cboTransTypeForCustomRegFlagCharge.Items[intI]).ID)
                                    cboTransTypeForCustomRegFlagCharge.SelectedIndex = intI;
                            }
                        }
                        catch { }

                    }

                    conDB.Close();

                    subLiveChargeVisibility();
                }
            }
            catch { }
        }

        private void radSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            subSetCtlVisibility();
        }

        private void btnSetConnection_Click(object sender, EventArgs e)
        {
            Settings.Default.UseSQLServer = radSQLServer.Checked;

            if (radMSAccess.Checked)
                Settings.Default.MainPath = txtCTConnection.Text;
            else if (radSQLServer.Checked)
            {
                Settings.Default.SQLServer = txtSQLServer.Text;
                Settings.Default.SQLDatabase = txtSQLDatabase.Text;
                Settings.Default.SQLUserName = txtSQLUsername.Text;
                Settings.Default.SQLPassword = txtSQLPassword.Text;
            }

            Settings.Default.Save();
        }

        private void cmdSaveSettings_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            long lngNewWSID = 0;

            long.TryParse(txtStationID.Text, out lngNewWSID);

            if (lngNewWSID != 0)
                Settings.Default.lngWSID = lngNewWSID;
            else
            {
                MessageBox.Show("Station ID must be numeric");
                return;
            }

            if (cboCCMethod.SelectedIndex <= 0)
                clsAppSettings.GetAppSettings().lngLiveCharge = clsGlobalEnum.conLIVECHARGE.None;
            else
                clsAppSettings.GetAppSettings().lngLiveCharge = (clsGlobalEnum.conLIVECHARGE)((clsCboItem)cboCCMethod.SelectedItem).ID;

            clsAppSettings.GetAppSettings().strXChargePath = txtXChargePath.Text;
            clsAppSettings.GetAppSettings().strCashLinqUName = txtCashLinqUser.Text;
            clsAppSettings.GetAppSettings().strCashLinqPW = txtCashLinqPW.Text;

            clsAppSettings.GetAppSettings().strCashLinqCQUser = txtCashLinqCQUser.Text;
            clsAppSettings.GetAppSettings().strCashLinqCQPW = txtCashLinqCQPW.Text;
            clsAppSettings.GetAppSettings().strCashLinqCQMerchantID = txtCashLinqCQMerchantID.Text;

            Settings.Default.Save();

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "UPDATE tblCampDefaults " +
                            "SET lngLiveCharge=@lngLiveCharge, " +
                                "strXChargeWebID=@strXChargeWebID, strXChargeAuthKey=@strXChargeAuthKey, strTextClientID=@strTextClientID, strMDCustomerID=@strMDCustomerID, strXChargePath=@strXChargePath, strCashLinqUName=@strCashLinqUName, strCashLinqPW=@strCashLinqPW, strCashLinqCQUser=@strCashLinqCQUser, strCashLinqCQPW=@strCashLinqCQPW, strCashLinqCQMerchantID=@strCashLinqCQMerchantID";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.Parameters.Add(new OleDbParameter("@lngLiveCharge", ((clsCboItem)cboCCMethod.SelectedItem).ID));
                        cmdDB.Parameters.Add(new OleDbParameter("@strXChargeWebID", txtXChargeXWebID.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strXChargeAuthKey", txtXChargeAuthKey.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strTextClientID", txtClientID.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strMDCustomerID", txtMDCustomerID.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strXChargePath", txtXChargePath.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCashLinqUName", txtCashLinqUser.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCashLinqPW", txtCashLinqPW.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCashLinqCQUser", txtCashLinqCQUser.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCashLinqCQPW", txtCashLinqCQPW.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCashLinqCQMerchantID", txtCashLinqCQMerchantID.Text));

                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }
            }
            catch { }
        }

        private void cboCCMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            subLiveChargeVisibility();
        }

        private void subLiveChargeVisibility()
        {
            lblCashLinqUser.Visible = false;
            lblCashLinqPW.Visible = false;
            lblXChargePath.Visible = false;
            lblXChargeAuthKey.Visible = false;
            lblXChargeXWebID.Visible = false;

            txtXChargePath.Visible = false;
            txtXChargeAuthKey.Visible = false;
            txtXChargeXWebID.Visible = false;
            txtCashLinqUser.Visible = false;
            txtCashLinqPW.Visible = false;

            lblCashLinqCQUser.Visible = false;
            lblCashLinqCQPW.Visible = false;
            lblCashLinqCQMerchantID.Visible = false;
            txtCashLinqCQUser.Visible = false;
            txtCashLinqCQPW.Visible = false;
            txtCashLinqCQMerchantID.Visible = false;

            btnSetXChargePath.Visible = false;

            if (((clsCboItem)cboCCMethod.SelectedItem).ID == (long)clsGlobalEnum.conLIVECHARGE.XCharge)
            {
                txtXChargePath.Visible = true;
                btnSetXChargePath.Visible = true;
                lblXChargePath.Visible = true;
            }
            else if (((clsCboItem)cboCCMethod.SelectedItem).ID == (long)clsGlobalEnum.conLIVECHARGE.CashLinq)
            {
                txtCashLinqUser.Visible = true;
                txtCashLinqPW.Visible = true;
                lblCashLinqUser.Visible = true;
                lblCashLinqPW.Visible = true;

                lblCashLinqCQUser.Visible = true;
                lblCashLinqCQPW.Visible = true;
                lblCashLinqCQMerchantID.Visible = true;
                txtCashLinqCQUser.Visible = true;
                txtCashLinqCQPW.Visible = true;
                txtCashLinqCQMerchantID.Visible = true;
            }
            else if (((clsCboItem)cboCCMethod.SelectedItem).ID == (long)clsGlobalEnum.conLIVECHARGE.XChargeXML)
            {
                lblXChargeXWebID.Visible = true;
                lblXChargeAuthKey.Visible = true;
                txtXChargeXWebID.Visible = true;
                txtXChargeAuthKey.Visible = true;
            }
        }

        private void btnSetXChargePath_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dlgXCharge;

            string strFolder;

            dlgXCharge = new FolderBrowserDialog();

            if (dlgXCharge.ShowDialog() == DialogResult.OK)
                strFolder = dlgXCharge.SelectedPath;
            else
                strFolder = "";

            txtXChargePath.Text = strFolder;

        }

        private void btnFindCTMain_B_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgCTMain;

            string strFileName;

            dlgCTMain = new OpenFileDialog();

            dlgCTMain.Filter = "MSAccess Databases (*.mdb)|*.mdb|All Files|*.*";
            dlgCTMain.Title = "Find CTMain_B.mdb";

            if (dlgCTMain.ShowDialog() == DialogResult.OK)
                strFileName = dlgCTMain.FileName;
            else
                strFileName = "";

            txtCTConnection.Text = strFileName;
        }

        private void btnTestCTMain_Click(object sender, EventArgs e)
        {
            OleDbConnection objConn;

            string strConn = "";

            objConn = new OleDbConnection();

            try
            {
                if (txtCTConnection.Text.EndsWith("accdb"))
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + txtCTConnection.Text + "; User Id=admin; Password=";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + txtCTConnection.Text + "; User Id=admin; Password=";
            }
            catch { }

            objConn.ConnectionString = strConn;

            try
            {
                objConn.Open();
                objConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No connection possible (" + ex.Message + ")");
                return;
            }
            MessageBox.Show("Connection OK");
        }

        private void btnSaveGiftTab_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            long lngDefaultCampaignID = 0;
            long lngOLGiftCategoryID = 0;
            long lngDXCampaignID = 0;
            long lngDXCategoryID = 0;

            lngDefaultCampaignID = ((clsCboItem)cboDefaultCampaign.SelectedItem).ID;
            lngOLGiftCategoryID = ((clsCboItem)cboIndRegCategory.SelectedItem).ID;
            lngDXCampaignID = ((clsCboItem)cboDXCampaign.SelectedItem).ID;
            lngDXCategoryID=((clsCboItem)cboDXCategory.SelectedItem).ID;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "UPDATE tblCampDefaults " +
                            "SET lngOLGiftCampaign=" + lngDefaultCampaignID + ", lngOLGiftCategoryID=" + lngOLGiftCategoryID.ToString() + ", lngDXCampaignID=" + lngDXCampaignID.ToString() + ", lngDXCategoryID=" + lngDXCategoryID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB)) { cmdDB.ExecuteNonQuery(); }

                    conDB.Close();
                }
            }
            catch { }
        }

        private void btnDev_Click(object sender, EventArgs e)
        {
            XCTransaction2.XChargeTransaction objXC = new XCTransaction2.XChargeTransaction();

            string strErr = "";
            string strRpt = "";

            if (!objXC.XCReportEx((int)this.Handle, "IP|KINGTUBBY|26", "CTDEV", true, true, "TRANDETAILS", "8/7/2011", "9/1/2011", "CTDEV", "", "", out strErr, out strRpt))
                MessageBox.Show("ERROR: " + strErr);
            else
                MessageBox.Show("SUCCESS: " + strRpt);
        }

        private void btnSaveWebSettings_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            bool blnCapsWebProcessing = false;
            long lngDefCustRegFlagTransType = 0;

            blnCapsWebProcessing = chkCapsWebProcessing.Checked;
            lngDefCustRegFlagTransType = ((clsCboItem)cboTransTypeForCustomRegFlagCharge.SelectedItem).ID;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "UPDATE tblCampDefaults " +
                            "SET blnCapsWebProcessing=@blnCapsWebProcessing, " +
                                "lngDefCustRegFlagTransType = @lngDefCustRegFlagTransType";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.Parameters.AddWithValue("@blnCapsWebProcessing", blnCapsWebProcessing);
                        cmdDB.Parameters.AddWithValue("@lngDefCustRegFlagTransType", lngDefCustRegFlagTransType);

                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }
            }
            catch { }
        }
    }
}