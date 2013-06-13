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
    public partial class frmWebGiftDetails : Form
    {
        private long lngGiftWebID;
        private long lngRecordWebID;

        public frmWebGiftDetails(long _lngGiftWebID)
        {
            lngGiftWebID = _lngGiftWebID;
            InitializeComponent();
        }

        private void frmWebGiftDetails_Load(object sender, EventArgs e)
        {
            int intMarker = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    string strSQL;

                    long lngCampaignID = 0;
                    clsGlobalEnum.conLIVECHARGE intLiveChargeMethod = clsGlobalEnum.conLIVECHARGE.None;

                    //fill category, campaign cbos
                    clsDonorCRUD.subFillGiftCatCbo(ref cboGiftCategory);
                    clsDonorCRUD.subFillCampaignCbo(ref cboCampaign);

                    clsCboSources.subFillStateCbo(ref cboState);
                    clsCboSources.subFillStateCbo(ref cboBillState);
                    clsCboSources.subFillTitleCbo(ref cboTitle);

                    strSQL = "SELECT lngLiveCharge " +
                                "FROM tblCampDefaults;";

                    using (OleDbCommand cmdDBNew = new OleDbCommand(strSQL, conDB))
                    {
                        intLiveChargeMethod = (clsGlobalEnum.conLIVECHARGE)cmdDBNew.ExecuteScalar();
                    }

                    //load gift details
                    strSQL = "SELECT tblWebGift.blnAnonymous, tblWebGift.blnInHonorOf, tblWebGift.blnMemorial, tblWebGift.blnPledgeReminders, tblWebGift.blnPledgeAutopay, " +
                                    "tblWebGift.intPledgeFreq, tblWebGift.intPledgeTerm, tblWebGift.lngRecordWebID, tblWebGift.lngGiftWebID, tblWebGift.lngCampaignID, tblWebGift.lngGiftCategoryID, tblWebGift.lngPaymentTypeID, tblWebGift.lngBillStateID, tblWebRecords.lngRecordID, tblWebRecords.lngStateID, " +
                                "tblWebGift.curAmount, " +
                                "tblWebGift.dteGiftDate, " +
                                "tblWebGift.strInHonorOf, tblWebGift.strAcctNum, tblWebGift.strBankName, tblWebGift.strBillAddress, tblWebGift.strBillCity, tblWebGift.strBillName, tblWebGift.strBillPhone, tblWebGift.strBillZip, tblWebGift.strCCExpDate, tblWebGift.strCCNumber, tblWebGift.strCCValCode, tblWebGift.strRoutingNum, tblWebGift.strMemorialName, tblWebRecords.strAddress, tblWebRecords.strCity, tblWebRecords.strEMail, tblWebRecords.strWorkExt AS strExt, tblWebRecords.strFirstName AS strFName, tblWebRecords.strHomePhone, tblWebRecords.strCellPhone, tblWebRecords.strLastCoName AS strLName, tblWebRecords.strReferredBy, tblWebRecords.strSpouseFName, tblWebRecords.strSpouseLName, tblWebRecords.strSpousePhone, tblWebRecords.strWorkPhone, tblWebRecords.strZip, tblWebRecords.strTitle, tblWebRecords.strInformalSal, tblWebRecords.strMI, tblWebGift.strAuthNum, tblWebGift.strPNRef, strXCAlias, strXCTransID, strXCEFTAuthCode, strXCEFTRefID, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID " +
                            "FROM tblWebGift " +
                                "INNER JOIN tblWebRecords ON tblWebGift.lngRecordWebID = tblWebRecords.lngRecordWebID " +
                            "WHERE tblWebGift.lngGiftWebID=" + lngGiftWebID + ";";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drGift = cmdDB.ExecuteReader())
                        {
                            if (drGift.Read())
                            {
                                clsCboItem.subSetSelectedIndex(ref cboTitle, drGift["strTitle"].ToString());

                                lngCampaignID = long.Parse(drGift["lngCampaignID"].ToString());

                                if (lngCampaignID <= 0) lngCampaignID = clsDonorCRUD.fcnGetDefaultCampaignID();

                                clsCboItem.subSetSelectedIndex(ref cboCampaign, lngCampaignID);

                                clsCboItem.subSetSelectedIndex(ref cboGiftCategory, long.Parse(drGift["lngGiftCategoryID"].ToString()));

                                long lngRecordID = 0;

                                try { lngRecordID = Convert.ToInt32(drGift["lngRecordID"]); }
                                catch { lngRecordID = 0; }

                                txtRecordID.Text = lngRecordID.ToString();

                                txtAmount.Text = drGift["curAmount"].ToString();
                                txtGiftDate.Text = drGift["dteGiftDate"].ToString();
                                txtFName.Text = drGift["strFName"].ToString();
                                txtLName.Text = drGift["strLName"].ToString();
                                txtInformalSal.Text = drGift["strInformalSal"].ToString();

                                chkInHonorOf.Checked = bool.Parse(drGift["blnInHonorOf"].ToString());
                                chkMemorial.Checked = bool.Parse(drGift["blnMemorial"].ToString());

                                long lngPaymentTypeID = 0;

                                try { lngPaymentTypeID = Convert.ToInt32(drGift["lngPaymentTypeID"]); }
                                catch { lngPaymentTypeID = 0; }

                                long lngBillStateID = 0;

                                try { lngBillStateID = Convert.ToInt32(drGift["lngBillStateID"]); }
                                catch { lngBillStateID = 0; }

                                clsCboItem.subSetSelectedIndex(ref  cboPaymentType, lngPaymentTypeID);
                                clsCboItem.subSetSelectedIndex(ref cboBillState, lngBillStateID);
                                clsCboItem.subSetSelectedIndex(ref cboState, long.Parse(drGift["lngStateID"].ToString()));

                                txtInHonorOf.Text = drGift["strInHonorOf"].ToString();
                                txtAcctNum.Text = Convert.ToString(drGift["strAcctNum"]); // clsEncryption.fcnDecrypt(drGift["strAcctNum"].ToString());
                                txtBankName.Text = drGift["strBankName"].ToString();
                                txtBillAddress.Text = drGift["strBillAddress"].ToString();
                                txtBillCity.Text = drGift["strBillCity"].ToString();
                                txtBillName.Text = drGift["strBillName"].ToString();
                                txtBillPhone.Text = drGift["strBillPhone"].ToString();
                                txtBillZip.Text = drGift["strBillZip"].ToString();
                                txtExpDate.Text = drGift["strCCExpDate"].ToString();
                                txtAuthNum.Text = drGift["strAuthNum"].ToString();
                                
                                try { txtPNRef.Text = drGift["strPNRef"].ToString(); }
                                catch { txtPNRef.Text = ""; }

                                try { txtEPSTransID.Text = Convert.ToString(drGift["strEPSTransID"]); }
                                catch { txtEPSTransID.Text = ""; }

                                try { txtEPSApprovalNumber.Text = Convert.ToString(drGift["strEPSApprovalNumber"]); }
                                catch { txtEPSApprovalNumber.Text = ""; }

                                try { txtEPSValidationCode.Text = Convert.ToString(drGift["strEPSValidationCode"]); }
                                catch { txtEPSValidationCode.Text = ""; }

                                try { txtEPSPmtAcctID.Text = Convert.ToString(drGift["strEPSPmtAcctID"]); }
                                catch { txtEPSPmtAcctID.Text = ""; }
                                
                                string strCC = Convert.ToString(drGift["strCCNumber"]); //clsEncryption.fcnDecrypt(drGift["strCCNumber"].ToString());

                                try
                                {
                                    if (intLiveChargeMethod == clsGlobalEnum.conLIVECHARGE.None)
                                    {
                                        txtCardNumber.Text = strCC;
                                        txtCCNumberUnMasked.Text = strCC;
                                    }
                                    else
                                    {
                                        if (strCC.Length > 4)
                                            txtCardNumber.Text = "************" + strCC.Substring(strCC.Length - 4, 4);
                                        else
                                            txtCardNumber.Text = "************" + strCC;

                                        txtCCNumberUnMasked.Text = strCC;
                                    }
                                }
                                catch { }

                                txtCVV2.Text = drGift["strCCValCode"].ToString();
                                txtRoutingNum.Text = drGift["strRoutingNum"].ToString();
                                clsCboSources.subFillPmtTypeCbo(ref cboPaymentType, lngPaymentTypeID);
                                txtMemorial.Text = drGift["strMemorialName"].ToString();
                                txtAddress.Text = drGift["strAddress"].ToString();
                                txtCity.Text = drGift["strCity"].ToString();
                                txtEMail.Text = drGift["strEMail"].ToString();
                                txtExt.Text = drGift["strExt"].ToString();
                                txtHomePhone.Text = drGift["strHomePhone"].ToString();
                                txtCellPhone.Text = drGift["strCellPhone"].ToString();
                                clsCboSources.subFillReferredByCbo(ref cboReferredBy, drGift["strReferredBy"].ToString());
                                txtSpouseFName.Text = drGift["strSpouseFName"].ToString();
                                txtSpouseLName.Text = drGift["strSpouseLName"].ToString();
                                txtSpouseWorkPhone.Text = drGift["strSpousePhone"].ToString();
                                txtWorkPhone.Text = drGift["strWorkPhone"].ToString();
                                txtZip.Text = drGift["strZip"].ToString();
                                txtMI.Text = drGift["strMI"].ToString();

                                long.TryParse(drGift["lngRecordWebID"].ToString(), out lngRecordWebID);

                                txtCardNumber.Visible = false;
                                txtCVV2.Visible = false;
                                txtExpDate.Visible = false;
                                txtBankName.Visible = false;
                                txtAcctNum.Visible = false;
                                txtRoutingNum.Visible = false;
                                lblCardNum.Visible = false;
                                lblCVV2.Visible = false;
                                lblExpDate.Visible = false;
                                lblBankName.Visible = false;
                                lblAcctNum.Visible = false;
                                lblRoutingNum.Visible = false;

                                lblPNRef.Visible = false;
                                txtPNRef.Visible = false;
                                
                                lblEPSApprovalNumber.Visible = false;
                                lblEPSPmtAcctID.Visible = false;
                                lblEPSTransID.Visible = false;
                                lblEPSValidationCode.Visible = false;
                                txtEPSApprovalNumber.Visible = false;
                                txtEPSPmtAcctID.Visible = false;
                                txtEPSTransID.Visible = false;
                                txtEPSValidationCode.Visible = false;

                                lblXCAlias.Visible = false;
                                lblXCEFTAuthCode.Visible = false;
                                lblXCEFTRefID.Visible = false;
                                lblXCTransID.Visible = false;

                                txtXCAlias.Visible = false;
                                txtXCEFTAuthCode.Visible = false;
                                txtXCEFTRefID.Visible = false;
                                txtXCTransID.Visible = false;

                                if (intLiveChargeMethod == clsGlobalEnum.conLIVECHARGE.CashLinq)
                                {
                                    lblPNRef.Visible = true;
                                    txtPNRef.Visible = true;
                                }
                                else if (intLiveChargeMethod == clsGlobalEnum.conLIVECHARGE.EPS)
                                {
                                    lblEPSApprovalNumber.Visible = true;
                                    lblEPSPmtAcctID.Visible = true;
                                    lblEPSTransID.Visible = true;
                                    lblEPSValidationCode.Visible = true;
                                    txtEPSApprovalNumber.Visible = true;
                                    txtEPSPmtAcctID.Visible = true;
                                    txtEPSTransID.Visible = true;
                                    txtEPSValidationCode.Visible = true;
                                }

                                if (lngPaymentTypeID == 2)
                                {
                                    //cc
                                    txtCardNumber.Visible = true;
                                    txtCVV2.Visible = true;
                                    txtExpDate.Visible = true;
                                    lblCardNum.Visible = true;
                                    lblCVV2.Visible = true;
                                    lblExpDate.Visible = true;

                                    if (intLiveChargeMethod == clsGlobalEnum.conLIVECHARGE.XCharge)
                                    {
                                        lblXCAlias.Visible = true;
                                        lblXCTransID.Visible = true;
                                        txtXCAlias.Visible = true;
                                        txtXCTransID.Visible = true;
                                    }
                                }
                                else if (lngPaymentTypeID == 11)
                                {
                                    //eft
                                    txtBankName.Visible = true;
                                    txtAcctNum.Visible = true;
                                    txtRoutingNum.Visible = true;
                                    lblBankName.Visible = true;
                                    lblAcctNum.Visible = true;
                                    lblRoutingNum.Visible = true;

                                    if (intLiveChargeMethod == clsGlobalEnum.conLIVECHARGE.XCharge)
                                    {
                                        lblXCEFTAuthCode.Visible = true;
                                        lblXCEFTRefID.Visible = true;
                                        txtXCEFTAuthCode.Visible = true;
                                        txtXCEFTRefID.Visible = true;
                                    }
                                }

                                intMarker++;
                                txtFormalSal.Text = ((string)(cboTitle.SelectedItem.ToString() + " " + txtFName.Text + " " + txtMI.Text + " " + txtLName.Text)).Replace("  ", " ");
                                intMarker++;
                                //load pledge (if applicable)
                                bool blnAddPledge = false;
                                bool blnPledgeReminders = false;
                                bool blnPledgeAutopay = false;
                                int intPledgeFreq = 0;
                                int intPledgeTerm = 0;

                                intMarker++;

                                try
                                {
                                    bool.TryParse(drGift["blnPledgeReminders"].ToString(), out blnPledgeReminders);
                                    bool.TryParse(drGift["blnPledgeAutopay"].ToString(), out blnPledgeAutopay);
                                    int.TryParse(drGift["intPledgeFreq"].ToString(), out intPledgeFreq);
                                    int.TryParse(drGift["intPledgeTerm"].ToString(), out intPledgeTerm);
                                    if (intPledgeTerm > 0) blnAddPledge = true;
                                }
                                catch { }
                                intMarker++;
                                if (blnAddPledge)
                                {
                                    chkPledgeReminders.Checked = blnPledgeReminders;
                                    chkPledgeAutopay.Checked = blnPledgeAutopay;

                                    clsCboSources.subFillPledgeFreqCbo(ref cboPledgeFreq, intPledgeFreq);
                                    txtPledgeTerm.Text = intPledgeTerm.ToString();
                                    txtTotalPledgeAmt.Text = ((decimal)(intPledgeTerm * decimal.Parse(txtAmount.Text))).ToString("C");
                                }
                                else
                                    tabGiftDetails.TabPages.Remove(pagPledgeInfo);

                                try
                                {
                                    txtXCAlias.Text = Convert.ToString(drGift["strXCAlias"]);
                                    txtXCTransID.Text = Convert.ToString(drGift["strXCTransID"]);
                                    txtXCEFTAuthCode.Text = Convert.ToString(drGift["strXCEFTAuthCode"]);
                                    txtXCEFTRefID.Text = Convert.ToString(drGift["strXCEFTRefID"]);
                                }
                                catch { }

                                intMarker++;
                            }
                            else
                                lngRecordWebID = 0;

                            drGift.Close();
                        }
                    }
                    intMarker++;
                    conDB.Close();
                }

                intMarker++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("(" + intMarker + ") Error: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseWebGiftDetails();
        }

        private void btnAddGift_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            long lngPledgeID = 0;
            long lngPledgePmtID = 0;
            long lngGiftID = 0;

            //validate
            if (cboCampaign.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a campaign for the gift.");
                tabGiftDetails.SelectedTab = pagGiftInfo;
                cboCampaign.Focus();
                return;
            }

            long lngRecordID = 0;

            try { lngRecordID = Convert.ToInt32(txtRecordID.Text); }
            catch { lngRecordID = 0; }

            long lngStateID=0;

            try { lngStateID = ((clsCboItem)cboState.SelectedItem).ID; }
            catch { lngStateID = 0; }

            lngRecordID = clsNav.fcnFindIR("", txtFName.Text, txtLName.Text, lngRecordID, txtAddress.Text, txtCity.Text, lngStateID, txtZip.Text, txtHomePhone.Text, txtWorkPhone.Text, txtCellPhone.Text, txtEMail.Text);

            if (lngRecordID == 0)
            {
                DialogResult dlgRes = MessageBox.Show("Create new record?", "Create new record?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dlgRes == DialogResult.Yes)
                    lngRecordID = clsIRCRUD.fcnAddIRFromWebRecord(lngRecordWebID);

            }
            else if (lngRecordID > 0)
                lngRecordID = clsNav.fcnShowReconcileIR("tblWebRecords", lngRecordID, lngRecordWebID);

            if (lngRecordID > 0)
            {
                clsIRCRUD.subUpdateWebID(lngRecordID, lngRecordWebID);
                clsIRCRUD.subSetToSync(lngRecordID);

                subMarkIRAsDonor(lngRecordID, txtFormalSal.Text, txtInformalSal.Text);

                int intPledgeTerm = 0;

                try { int.TryParse(txtPledgeTerm.Text, out intPledgeTerm); }
                catch { }

                //add pledge (and schedule pmts) if term is > 0
                //if (intPledgeTerm > 0) lngPledgeID = clsDonorCRUD.fcnAddPledge(chkPledgeReminders.Checked, chkPledgeAutopay.Checked, chkMemorial.Checked, chkInHonorOf.Checked, lngRecordID, ((clsCboItem)cboCampaign.SelectedItem).ID, ((clsCboItem)cboGiftCategory.SelectedItem).ID, ((clsCboItem)cboPledgeFreq.SelectedItem).ID, long.Parse(txtPledgeTerm.Text), ((clsCboItem)cboPaymentType.SelectedItem).ID, ((clsCboItem)cboBillState.SelectedItem).ID, decimal.Parse(txtAmount.Text), txtMemorial.Text, txtInHonorOf.Text, clsEncryption.fcnEncrypt(txtAcctNum.Text), txtBankName.Text, txtBillAddress.Text, txtBillCity.Text, txtFName.Text + " " + txtLName.Text, txtBillPhone.Text, txtBillZip.Text, txtExpDate.Text, txtCCNumberUnMasked.Text, txtCVV2.Text, txtRoutingNum.Text);
                if (intPledgeTerm > 0) lngPledgeID = clsDonorCRUD.fcnAddPledge(chkPledgeReminders.Checked, chkPledgeAutopay.Checked, chkMemorial.Checked, chkInHonorOf.Checked, lngRecordID, ((clsCboItem)cboCampaign.SelectedItem).ID, ((clsCboItem)cboGiftCategory.SelectedItem).ID, ((clsCboItem)cboPledgeFreq.SelectedItem).ID, long.Parse(txtPledgeTerm.Text), ((clsCboItem)cboPaymentType.SelectedItem).ID, ((clsCboItem)cboBillState.SelectedItem).ID, decimal.Parse(txtAmount.Text), txtMemorial.Text, txtInHonorOf.Text, txtAcctNum.Text, txtBankName.Text, txtBillAddress.Text, txtBillCity.Text, txtFName.Text + " " + txtLName.Text, txtBillPhone.Text, txtBillZip.Text, txtExpDate.Text, txtCCNumberUnMasked.Text, txtCVV2.Text, txtRoutingNum.Text, txtPNRef.Text, txtXCAlias.Text, txtEPSPmtAcctID.Text);

                long lngCampaignID = 0;
                long lngPaymentTypeID = 0;

                try { lngCampaignID = ((clsCboItem)cboCampaign.SelectedItem).ID; }
                catch { lngCampaignID = 0; }

                try { lngPaymentTypeID = ((clsCboItem)cboPaymentType.SelectedItem).ID; }
                catch { lngPaymentTypeID = 2; }

                //add gift
                lngGiftID = clsDonorCRUD.fcnAddGift(chkMemorial.Checked, chkInHonorOf.Checked, ((clsCboItem)cboGiftCategory.SelectedItem).ID, lngRecordID, lngCampaignID, lngPaymentTypeID, ((clsCboItem)cboBillState.SelectedItem).ID, lngPledgeID, lngGiftWebID, decimal.Parse(txtAmount.Text), Convert.ToDateTime(txtGiftDate.Text), txtMemorial.Text, txtInHonorOf.Text, txtAcctNum.Text, txtBankName.Text, txtBillAddress.Text, txtBillCity.Text, txtFName.Text + " " + txtLName.Text, txtBillPhone.Text, txtBillZip.Text, txtExpDate.Text, txtCCNumberUnMasked.Text, txtCVV2.Text, txtRoutingNum.Text, txtAuthNum.Text, txtPNRef.Text, txtXCAlias.Text, txtXCTransID.Text, txtEPSTransID.Text, txtEPSApprovalNumber.Text, txtEPSValidationCode.Text, txtEPSPmtAcctID.Text);

                //update billing info (this gives pnref for future transactions)
                clsIRCRUD.subUpdateBillingInfoCC(lngRecordID, txtPNRef.Text, txtCCNumberUnMasked.Text, txtXCAlias.Text, txtEPSPmtAcctID.Text);

                //add first pledge pmt if applicable
                if (lngPledgeID > 0)
                {
                    if (lngGiftID > 0)
                        lngPledgePmtID = clsDonorCRUD.fcnAddPledgePmt(lngPledgeID, lngGiftID, decimal.Parse(txtAmount.Text), DateTime.Parse(txtGiftDate.Text));
                }

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //mark gift processed
                    strSQL = "UPDATE tblWebGift " +
                            "SET blnProcessed=1 " +
                            "WHERE lngGiftWebID=" + lngGiftWebID + ";";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }

                //refresh list
                clsNav.subCloseProcessGifts();
            }

            clsNav.subCloseWebGiftDetails();
        }
         
        private void subMarkIRAsDonor(long _lngRecordID, string _strFormalSal, string _strInformalSal)
        {
            string strSQL = "";

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //mark ir as donor
                    /*strSQL = "UPDATE tblRecords " +
                            "SET blnDonor=True, " +
                                "strFormalGiftSal='" + _strFormalSal + "', strInformalGiftSal='" + _strInformalSal + "' " +
                            "WHERE lngRecordID=" + _lngRecordID;*/

                    strSQL = "UPDATE tblRecords " +
                            "SET blnDonor=True " +
                            "WHERE lngRecordID=" + _lngRecordID;

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmWebGiftDetails.subMarkIRAsDonor", ex);
            }
        }

        private void btnGiftSummary_Click(object sender, EventArgs e)
        {
            //try { clsNav.subShowGiftSummaryPreview(this.MdiParent, lngGiftWebID); }
            //catch (Exception ex) { MessageBox.Show("There was an error opening the gift detail report: " + ex.Message); }
            using(Reports.frmWebGiftDetailsViewer objWebGiftDetailsViewer=new global::CTWebMgmt.Donor.Reports.frmWebGiftDetailsViewer(lngGiftWebID))
            {
                objWebGiftDetailsViewer.WindowState = FormWindowState.Maximized;
                objWebGiftDetailsViewer.ShowDialog();
            }
        }

        private void btnMarkProcessed_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            if (MessageBox.Show("This will mark the gift as processed without actually adding a gift record in CampTrak.\nDo you wish to continue?", "Mark as Processed", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (MessageBox.Show("Would you like to refund the payment for this gift?", "Refund", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    decimal decRefundAmt = 0;

                    try { decRefundAmt = Convert.ToDecimal(txtAmount.Text); }
                    catch { decRefundAmt = 0; }

                    if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
                    {
                        if (cboPaymentType.SelectedItem.ToString() == "EFT")
                            clsLiveCharge.subProcessRefundXCEFT(decRefundAmt,lngGiftWebID, txtAcctNum.Text, txtRoutingNum.Text, txtFName.Text + " " + txtLName.Text, txtAddress.Text, txtZip.Text, (int)this.Handle, clsLiveCharge.fcnGetXChargePath());
                        else
                            clsLiveCharge.subProcessRefundXCCC(decRefundAmt, txtXCTransID.Text, txtXCAlias.Text);
                    }
                    else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
                    {
                        if (cboPaymentType.SelectedItem.ToString() == "EFT")
                            clsLiveCharge.subProcessRefundCashLinqEFT(decRefundAmt, lngGiftWebID, txtAcctNum.Text, txtRoutingNum.Text, txtFName.Text + " " + txtLName.Text, txtAddress.Text, txtZip.Text, (int)this.Handle);
                        else
                            clsLiveCharge.subProcessRefundCashLinqCC(decRefundAmt, txtPNRef.Text);
                    }
                    else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
                    {
                        if (cboPaymentType.SelectedItem.ToString() == "EFT")
                            clsLiveCharge.subProcessRefundEPSEFT(decRefundAmt);
                        else
                            clsLiveCharge.subProcessRefundEPSCC(decRefundAmt, lngGiftWebID, txtEPSTransID.Text);
                    }
                }

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //mark gift processed
                    strSQL = "UPDATE tblWebGift " +
                            "SET blnProcessed=1 " +
                            "WHERE lngGiftWebID=" + lngGiftWebID + ";";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }

                //refresh list
                //clsNav.subCloseProcessGifts();
                clsNav.objProcessGifts.subRefreshGrid();
                clsNav.subCloseWebGiftDetails();
            }          
        }
    }
}