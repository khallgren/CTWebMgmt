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
    public partial class frmEditPledgeBillingInfo : Form
    {
        private long lngPledgeID;
        private long lngRecordID;

        public long lngPaymentTypeID;
        public long lngBillStateID;

        public string strAcctNum;
        public string strBankName;
        public string strBillAddress;
        public string strBillCity;
        public string strBillName;
        public string strBillPhone;
        public string strBillZip;
        public string strCCExpDate;
        public string strCCNumber;
        public string strCCValCode;
        public string strRoutingNum;
        public string strXCAlias;
        public string strPNRef;
        public string strEPSPmtAcctID;

        public frmEditPledgeBillingInfo(long _lngPledgeID, long _lngRecordID)
        {
            InitializeComponent();

            lngPledgeID = _lngPledgeID;
            lngRecordID = _lngRecordID;
        }

        private void frmEditPledgeBillingInfo_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            subFillCbos();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblPledge.lngPaymentTypeID, tblPledge.lngBillStateID, " +
                            "tblPledge.strAcctNum, tblPledge.strBankName, tblPledge.strBillAddress, tblPledge.strBillCity, tblPledge.strBillName, tblPledge.strBillPhone, tblPledge.strBillZip, tblPledge.strCCExpDate, tblPledge.strCCNumber, tblPledge.strCCValCode, tblPledge.strRoutingNum, tblBillingInfo.strPNRef, tblBillingInfo.strXCAlias, tblBillingInfo.strEPSPmtAcctID " +
                        "FROM tblPledge " +
                            "INNER JOIN tblBillingInfo ON tblPledge.lngRecordID = tblBillingInfo.lngRecordID " +
                        "WHERE lngPledgeID=" + lngPledgeID.ToString();

                using (OleDbCommand cmdPledge = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPledge = cmdPledge.ExecuteReader())
                    {
                        if (drPledge.Read())
                        {
                            long lngPaymentTypeID = 0;

                            try { lngPaymentTypeID = Convert.ToInt32(drPledge["lngPaymentTypeID"]); }
                            catch { lngPaymentTypeID = 0; }

                            for (int intI = 0; intI < cboPaymentType.Items.Count; intI++)
                            {
                                if (((clsCboItem)cboPaymentType.Items[intI]).ID == lngPaymentTypeID)
                                {
                                    cboPaymentType.SelectedIndex = intI;
                                    break;
                                }
                            }

                            for (int intI = 0; intI < cboBillState.Items.Count; intI++)
                            {
                                long lngBillStateID = 0;

                                try { lngBillStateID = Convert.ToInt32(drPledge["lngBillStateID"]); }
                                catch { lngBillStateID = 0; }

                                if (((clsCboItem)cboBillState.Items[intI]).ID == lngBillStateID)
                                {
                                    cboBillState.SelectedIndex = intI;
                                    break;
                                }
                            }

                            try { txtAcctNumber.Text = Convert.ToString(drPledge["strAcctNum"]); }
                            catch { txtAcctNumber.Text = ""; }

                            txtAcctNumber.Text = clsEncryption.fcnDecrypt(txtAcctNumber.Text);

                            try { txtBankName.Text = Convert.ToString(drPledge["strBankName"]); }
                            catch { txtBankName.Text = ""; }

                            try { txtBillAddress.Text = Convert.ToString(drPledge["strBillAddress"]); }
                            catch { }

                            try { txtBillCity.Text = Convert.ToString(drPledge["strBillCity"]); }
                            catch { }

                            try { txtBillName.Text = Convert.ToString(drPledge["strBillName"]); }
                            catch { }

                            try { txtBillPhone.Text = Convert.ToString(drPledge["strBillPhone"]); }
                            catch { }

                            try { txtBillZip.Text = Convert.ToString(drPledge["strBillZip"]); }
                            catch { }

                            try { txtExpDate.Text = Convert.ToString(drPledge["strCCExpDate"]); }
                            catch { }

                            try { txtCardNumber.Text = Convert.ToString(drPledge["strCCNumber"]); }
                            catch { }

                            txtCardNumber.Text = clsEncryption.fcnDecrypt(txtCardNumber.Text);

                            try { txtCVV2.Text = Convert.ToString(drPledge["strCCValCode"]); }
                            catch { }

                            try { txtRoutingNumber.Text = Convert.ToString(drPledge["strRoutingNum"]); }
                            catch { }

                            try { txtXCAlias.Text = Convert.ToString(drPledge["strXCAlias"]); }
                            catch { }

                            try { txtPNRef.Text = Convert.ToString(drPledge["strPNRef"]); }
                            catch { txtPNRef.Text = ""; }

                            try { txtXCEFTAuthCode.Text = Convert.ToString(drPledge["strXCEFTAuthCode"]); }
                            catch { txtXCEFTAuthCode.Text = ""; }

                            try { txtXCEFTRefID.Text = Convert.ToString(drPledge["strXCEFTRefID"]); }
                            catch { txtXCEFTRefID.Text = ""; }

                            try { txtEPSPmtAcctID.Text = Convert.ToString(drPledge["strEPSPmtAcctID"]); }
                            catch { txtEPSPmtAcctID.Text = ""; }

                            subLiveChargeVisibility(lngPaymentTypeID);
                        }

                        drPledge.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subLiveChargeVisibility(long _lngPmtTypeID)
        {
            txtRoutingNumber.Visible = false;
            txtAcctNumber.Visible = false;
            txtBankName.Visible = false;
            txtExpDate.Visible = false;
            txtCVV2.Visible = false;
            txtCardNumber.Visible = false;
            txtXCAlias.Visible = false;
            txtPNRef.Visible = false;
            txtXCEFTAuthCode.Visible = false;
            txtXCEFTRefID.Visible = false;
            txtEPSPmtAcctID.Visible = false;

            lblRoutingNumber.Visible = false;
            lblAcctNumber.Visible = false;
            lblBankName.Visible = false;
            lblExpDate.Visible = false;
            lblCVV2.Visible = false;
            lblCardNumber.Visible = false;
            lblXCAlias.Visible = false;
            lblPNRef.Visible = false;
            lblEPSPmtAcctID.Visible = false;
            lblXCEFTAuthCode.Visible = false;
            lblXCEFTRefID.Visible = false;


            clsGlobalEnum.conLIVECHARGE intLiveCharge = clsLiveCharge.fcnLiveChargeMethod();

            if (_lngPmtTypeID == 2)
            {
                //credit card
                txtExpDate.Visible = true;
                txtCVV2.Visible = true;
                txtCardNumber.Visible = true;

                lblExpDate.Visible = true;
                lblCVV2.Visible = true;
                lblCardNumber.Visible = true;
            }
            else if (_lngPmtTypeID == 11)
            {
                //eft
                txtRoutingNumber.Visible = true;
                txtAcctNumber.Visible = true;
                txtBankName.Visible = true;

                lblRoutingNumber.Visible = true;
                lblAcctNumber.Visible = true;
                lblBankName.Visible = true;

                if (intLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
                {
                    txtXCEFTAuthCode.Visible = true;
                    txtXCEFTRefID.Visible = true;

                    lblXCEFTAuthCode.Visible = true;
                    lblXCEFTRefID.Visible = true;
                }
            }

            btnCopyFromBillingInfo.Visible = false;

            if (intLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
            {
                txtPNRef.Visible = true;
                lblPNRef.Visible = true;
                btnCopyFromBillingInfo.Visible = true;
            }
            else if (intLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
            {
                txtXCAlias.Visible = true;
                lblXCAlias.Visible = true;
                btnCopyFromBillingInfo.Visible = true;
            }
            else if (intLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
            {
                txtEPSPmtAcctID.Visible = true;
                lblEPSPmtAcctID.Visible = true;
                btnCopyFromBillingInfo.Visible = true;
            }            
        }

        private void subFillCbos()
        {
            cboPaymentType.Items.Add(new clsCboItem(2, "Credit Card"));
            cboPaymentType.Items.Add(new clsCboItem(11, "EFT"));

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngStateID, " +
                            "strState " +
                        "FROM tlkpStates " +
                        "ORDER BY strState";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drStates = cmdDB.ExecuteReader())
                    {
                        while (drStates.Read())
                        {
                            try
                            {
                                clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drStates["lngStateID"]), Convert.ToString(drStates["strState"]));

                                cboBillState.Items.Add(cboNew);
                            }
                            catch { }
                        }

                        drStates.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try { lngPaymentTypeID = ((clsCboItem)cboPaymentType.SelectedItem).ID; }
            catch { lngPaymentTypeID = 0; }

            try { lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
            catch { lngBillStateID = 0; }

            strAcctNum = txtAcctNumber.Text;
            strBankName = txtBankName.Text;
            strBillAddress = txtBillAddress.Text;
            strBillCity = txtBillCity.Text;
            strBillName = txtBillName.Text;
            strBillPhone = txtBillPhone.Text;
            strBillZip = txtBillZip.Text;
            strCCExpDate = txtExpDate.Text;
            strCCNumber = txtCardNumber.Text;
            strCCValCode = txtCVV2.Text;
            strRoutingNum = txtRoutingNumber.Text;
            strXCAlias = txtXCAlias.Text;
            strPNRef = txtPNRef.Text;
            strEPSPmtAcctID = txtEPSPmtAcctID.Text;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void cboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            long lngPmtType = 0;

            try { lngPmtType = Convert.ToInt32(((clsCboItem)cboPaymentType.SelectedItem).ID); }
            catch { lngPmtType = 0; }

            subLiveChargeVisibility(lngPmtType);
        }

        private void btnCopyFromBillingInfo_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblBillingInfo.lngPaymentTypeID, tblBillingInfo.lngBillStateID, " +
                            "tblBillingInfo.strAcctNum, tblBillingInfo.strBankName, tblBillingInfo.strBillAddress, tblBillingInfo.strBillCity, tblBillingInfo.strBillName, tblBillingInfo.strBillPhone, tblBillingInfo.strBillZip, tblBillingInfo.strCCExpDate, tblBillingInfo.strCCNumber, tblBillingInfo.strRoutingNum, tblBillingInfo.strPNRef, tblBillingInfo.strPrevTransType, tblBillingInfo.strXCAlias, tblBillingInfo.strXCTransID, tblBillingInfo.strXCEFTAuthCode, tblBillingInfo.strXCEFTRefID, tblBillingInfo.strEPSPmtAcctID " +
                        "FROM tblBillingInfo " +
                        "WHERE tblBillingInfo.lngRecordID=" + lngRecordID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drBilling = cmdDB.ExecuteReader())
                    {
                        if (drBilling.Read())
                        {
                            long lngPaymentTypeID = 0;

                            try { lngPaymentTypeID = Convert.ToInt32(drBilling["lngPaymentTypeID"]); }
                            catch { lngPaymentTypeID = 0; }

                            for (int intI = 0; intI < cboPaymentType.Items.Count; intI++)
                            {
                                if (((clsCboItem)cboPaymentType.Items[intI]).ID == lngPaymentTypeID)
                                {
                                    cboPaymentType.SelectedIndex = intI;
                                    break;
                                }
                            }

                            for (int intI = 0; intI < cboBillState.Items.Count; intI++)
                            {
                                long lngBillStateID = 0;

                                try { lngBillStateID = Convert.ToInt32(drBilling["lngBillStateID"]); }
                                catch { lngBillStateID = 0; }

                                if (((clsCboItem)cboBillState.Items[intI]).ID == lngBillStateID)
                                {
                                    cboBillState.SelectedIndex = intI;
                                    break;
                                }
                            }

                            try { txtAcctNumber.Text = Convert.ToString(drBilling["strAcctNum"]); }
                            catch { txtAcctNumber.Text = ""; }

                            try { txtBankName.Text = Convert.ToString(drBilling["strBankName"]); }
                            catch { txtBankName.Text = ""; }

                            try { txtBillAddress.Text = Convert.ToString(drBilling["strBillAddress"]); }
                            catch { txtBillAddress.Text = ""; }

                            try { txtBillCity.Text = Convert.ToString(drBilling["strBillCity"]); }
                            catch { txtBillCity.Text = ""; }

                            try { txtBillName.Text = Convert.ToString(drBilling["strBillName"]); }
                            catch { txtBillName.Text = ""; }

                            try { txtBillPhone.Text = Convert.ToString(drBilling["strBillPhone"]); }
                            catch { txtBillPhone.Text = ""; }

                            try { txtBillZip.Text = Convert.ToString(drBilling["strBillZip"]); }
                            catch { txtBillZip.Text = ""; }

                            try { txtExpDate.Text = Convert.ToString(drBilling["strCCExpDate"]); }
                            catch { txtExpDate.Text = ""; }

                            try { txtCardNumber.Text = Convert.ToString(drBilling["strCCNumber"]); }
                            catch { txtCardNumber.Text = ""; }

                            try { txtRoutingNumber.Text = Convert.ToString(drBilling["strRoutingNum"]); }
                            catch { txtRoutingNumber.Text = ""; }

                            try { txtPNRef.Text = Convert.ToString(drBilling["strPNRef"]); }
                            catch { txtPNRef.Text = ""; }

                            try { txtEPSPmtAcctID.Text = Convert.ToString(drBilling["strEPSPmtAcctID"]); }
                            catch { txtEPSPmtAcctID.Text = ""; }

                            try { txtXCAlias.Text = Convert.ToString(drBilling["strXCAlias"]); }
                            catch { txtXCAlias.Text = ""; }

                            try { txtXCEFTAuthCode.Text = Convert.ToString(drBilling["strXCEFTAuthCode"]); }
                            catch { txtXCEFTAuthCode.Text = ""; }

                            try { txtXCEFTRefID.Text = Convert.ToString(drBilling["strXCEFTRefID"]); }
                            catch { txtXCEFTRefID.Text = ""; }

                        }

                        drBilling.Close();
                    }
                }

                conDB.Close();
            }
        }
    }
}