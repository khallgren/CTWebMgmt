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
    public partial class frmDonorExpressDetails : Form
    {
        private long lngDonorExpressID;

        public frmDonorExpressDetails(long _lngDonorExpressID)
        {
            InitializeComponent();

            lngDonorExpressID = _lngDonorExpressID;
        }

        private void frmDonorExpressDetails_Load(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    string strSQL;

                    clsGlobalEnum.conLIVECHARGE intLiveChargeMethod = clsGlobalEnum.conLIVECHARGE.None;

                    //fill category, campaign cbos
                    clsDonorCRUD.subFillGiftCatCbo(ref cboGiftCategory);
                    clsDonorCRUD.subFillCampaignCbo(ref cboCampaign);

                    strSQL = "SELECT lngLiveCharge, lngDXCampaignID, lngDXCategoryID " +
                                "FROM tblCampDefaults;";

                    long lngDXCampaignID = 0;
                    long lngDXCategoryID = 0;

                    using (OleDbCommand cmdDBNew = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drDef = cmdDBNew.ExecuteReader())
                        {
                            if (drDef.Read())
                            {
                                intLiveChargeMethod = (clsGlobalEnum.conLIVECHARGE)drDef["lngLiveCharge"];

                                try { lngDXCampaignID = Convert.ToInt32(drDef["lngDXCampaignID"]); }
                                catch { lngDXCampaignID = 0; }

                                try { lngDXCategoryID = Convert.ToInt32(drDef["lngDXCategoryID"]); }
                                catch { lngDXCategoryID = 0; }
                            }

                            drDef.Close();
                        }
                    }

                    //set default campaign, category
                    clsCboItem.subSetSelectedIndex(ref cboCampaign, lngDXCampaignID);
                    clsCboItem.subSetSelectedIndex(ref cboGiftCategory, lngDXCategoryID);

                    //load gift details
                    strSQL = "SELECT IIf(ISNULL([strIHO]) <> \"\", 1, 0) AS blnInHonorOf, IIf(ISNULL([strIMO]) <> \"\", 1, 0) AS blnMemorial, " +
                                "tblDonorExpress.lngPaymentTypeID, " +
                                "tblDonorExpress.curGiftAmt, " +
                                "tblDonorExpress.dteSubmitted, " +
                                "tblDonorExpress.strIHO, tblDonorExpress.strAcctNum, tblDonorExpress.strBankName, tblDonorExpress.strCCExpDate, tblDonorExpress.strCCNumber, tblDonorExpress.strCCValCode, tblDonorExpress.strRoutingNum, tblDonorExpress.strIMO, tblDonorExpress.strAddress, tblDonorExpress.strCity, tblDonorExpress.strState, tblDonorExpress.strEmail, tblDonorExpress.strFName, tblDonorExpress.strHomePhone, tblDonorExpress.strLName, tblDonorExpress.strReferredBy, tblDonorExpress.strZip, tblDonorExpress.strAuthNum, tblDonorExpress.strPNRef, tblDonorExpress.strXCAlias, tblDonorExpress.strXCTransID, tblDonorExpress.strXCEFTAuthCode, tblDonorExpress.strXCEFTRefID, tblDonorExpress.strEPSTransID, tblDonorExpress.strEPSApprovalNumber, tblDonorExpress.strEPSValidationCode, tblDonorExpress.strEPSPmtAcctID  " +
                            "FROM tblDonorExpress " +
                            "WHERE tblDonorExpress.lngDonorExpressID=" + lngDonorExpressID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drGift = cmdDB.ExecuteReader())
                        {
                            if (drGift.Read())
                            {
                                txtAmount.Text = drGift["curGiftAmt"].ToString();
                                txtGiftDate.Text = drGift["dteSubmitted"].ToString();
                                txtFName.Text = drGift["strFName"].ToString();
                                txtLName.Text = drGift["strLName"].ToString();

                                chkInHonorOf.Checked = Convert.ToBoolean(drGift["blnInHonorOf"]);
                                chkMemorial.Checked = Convert.ToBoolean(drGift["blnMemorial"]);

                                long lngPaymentTypeID = 0;

                                try { lngPaymentTypeID = Convert.ToInt32(drGift["lngPaymentTypeID"]); }
                                catch { lngPaymentTypeID = 0; }

                                txtInHonorOf.Text = drGift["strIHO"].ToString();
                                txtAcctNum.Text = Convert.ToString(drGift["strAcctNum"]); // clsEncryption.fcnDecrypt(drGift["strAcctNum"].ToString());
                                txtBankName.Text = drGift["strBankName"].ToString();
                                txtExpDate.Text = drGift["strCCExpDate"].ToString();
                                txtAuthNum.Text = drGift["strAuthNum"].ToString();
                                txtPNRef.Text = drGift["strPNRef"].ToString();

                                string strCC = Convert.ToString(drGift["strCCNumber"]); //clsEncryption.fcnDecrypt(drGift["strCCNumber"].ToString());

                                txtCardNumber.Text = "************" + strCC;

                                txtCCNumberUnMasked.Text = strCC;

                                txtCVV2.Text = drGift["strCCValCode"].ToString();
                                txtRoutingNum.Text = drGift["strRoutingNum"].ToString();
                                clsCboSources.subFillPmtTypeCbo(ref cboPaymentType, lngPaymentTypeID);
                                txtMemorial.Text = drGift["strIMO"].ToString();
                                txtAddress.Text = drGift["strAddress"].ToString();
                                txtCity.Text = drGift["strCity"].ToString();
                                txtEMail.Text = drGift["strEMail"].ToString();
                                txtHomePhone.Text = drGift["strHomePhone"].ToString();
                                clsCboSources.subFillReferredByCbo(ref cboReferredBy, drGift["strReferredBy"].ToString());
                                txtZip.Text = drGift["strZip"].ToString();

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

                                lblXCAlias.Visible = false;
                                lblXCEFTAuthCode.Visible = false;
                                lblXCEFTRefID.Visible = false;
                                lblXCTransID.Visible = false;
                                lblEPSApprovalNumber.Visible = false;
                                lblEPSPmtAcctID.Visible = false;
                                lblEPSTransID.Visible = false;
                                lblEPSValidationCode.Visible = false;

                                txtXCAlias.Visible = false;
                                txtXCEFTAuthCode.Visible = false;
                                txtXCEFTRefID.Visible = false;
                                txtXCTransID.Visible = false;
                                txtEPSApprovalNumber.Visible = false;
                                txtEPSPmtAcctID.Visible = false;
                                txtEPSTransID.Visible = false;
                                txtEPSValidationCode.Visible = false;

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

                                txtXCAlias.Text = Convert.ToString(drGift["strXCAlias"]);
                                txtXCTransID.Text = Convert.ToString(drGift["strXCTransID"]);
                                txtXCEFTAuthCode.Text = Convert.ToString(drGift["strXCEFTAuthCode"]);
                                txtXCEFTRefID.Text = Convert.ToString(drGift["strXCEFTRefID"]);

                                txtEPSApprovalNumber.Text = Convert.ToString(drGift["strEPSApprovalNumber"]);
                                txtEPSPmtAcctID.Text = Convert.ToString(drGift["strEPSPmtAcctID"]);
                                txtEPSTransID.Text = Convert.ToString(drGift["strEPSTransID"]);
                                txtEPSValidationCode.Text = Convert.ToString(drGift["strEPSValidationCode"]);
                            }

                            drGift.Close();
                        }

                        //load custom donor express fields

                        strSQL = "SELECT tblCustomFieldsGiftDef.strFieldName, tblDonorExpressCustomVals.strValue " +
                                "FROM tblCustomFieldsGiftDef " +
                                    "LEFT JOIN tblDonorExpressCustomVals ON tblCustomFieldsGiftDef.strFieldName = tblDonorExpressCustomVals.strFieldName " +
                                "WHERE tblDonorExpressCustomVals.lngDonorExpressID=" + lngDonorExpressID.ToString() + " " +
                                "ORDER BY tblCustomFieldsGiftDef.intSortOrder, tblCustomFieldsGiftDef.strFieldName";

                        using (OleDbDataAdapter daGiftFields= new OleDbDataAdapter(strSQL, conDB))
                        {
                            using (OleDbCommandBuilder cmdGiftFields= new OleDbCommandBuilder(daGiftFields))
                            {
                                // Populate a new data table and bind it to the BindingSource.
                                using (DataTable tblGiftFields = new DataTable())
                                {
                                    tblGiftFields.Locale = System.Globalization.CultureInfo.InvariantCulture;

                                    daGiftFields.Fill(tblGiftFields);

                                    grdGiftFields.DataSource = tblGiftFields;
                                }
                            }
                        }

                        grdGiftFields.AutoResizeColumns();
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAddGift_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            long lngGiftID = 0;

            //validate
            if (cboCampaign.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a campaign for the gift.");
                tabGiftDetails.SelectedTab = pagGiftInfo;
                cboCampaign.Focus();
                return;
            }

            if (cboGiftCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a category for the gift.");
                tabGiftDetails.SelectedTab = pagGiftInfo;
                cboGiftCategory.Focus();
                return;
            }

            long lngRecordID = 0;

            //'if there are no eligible matches, go directly to add new.
            //'show list of possible matches
            long lngStateID = 0;

            try { lngStateID = clsIRCRUD.fcnGetStateIDFromAbbr(txtState.Text); }
            catch { lngStateID = 0; }

            clsIR irToSearch = new clsIR(0, lngStateID, txtFName.Text, txtLName.Text, "", txtAddress.Text, txtCity.Text, txtZip.Text, txtHomePhone.Text, "", "", txtEMail.Text);

            try { irToSearch.lngStateID = lngStateID; }
            catch { irToSearch.lngStateID = 0; }

            try { irToSearch.lngBillStateID = lngStateID; }
            catch { irToSearch.lngBillStateID = 0; }

            irToSearch.strEmail = txtEMail.Text;
            irToSearch.strHomePhone =txtHomePhone.Text;
            irToSearch.strZip =txtZip.Text;
            irToSearch.strCity =txtCity.Text;
            irToSearch.strAddress =txtAddress.Text;
            irToSearch.strLName =txtLName.Text;
            irToSearch.strFName =txtFName.Text;

            try { irToSearch.strPmtType = ((clsCboItem)cboPaymentType.SelectedItem).Item; }
            catch { irToSearch.strPmtType = "CC"; }

            irToSearch.strSpecialNeeds = "";
            irToSearch.strBankName = txtBankName.Text;
            irToSearch.strBillName = txtFName.Text + " " + txtLName.Text;
            irToSearch.strBillAddress =txtAddress.Text;
            irToSearch.strBillCity =txtCity.Text;
            irToSearch.strBillZip =txtZip.Text;
            irToSearch.strBillPhone =txtHomePhone.Text;

            using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find donor record"))
            {
                if (objFindIR.ShowDialog() == DialogResult.OK)
                {
                    if (objFindIR.irToSearch.lngRecordID == 0)
                        return;
                    else
                    {
                        lngRecordID = objFindIR.irToSearch.lngRecordID;
                        clsIRCRUD.subSetToSync(lngRecordID);

                        using (IRUtils.frmReconcileIR objReconcileIR = new global::CTWebMgmt.IRUtils.frmReconcileIR("tblDonorExpress", lngRecordID, lngDonorExpressID))
                        {
                            if (objReconcileIR.ShowDialog() == DialogResult.Cancel)
                                return;
                            else
                                lngRecordID = objReconcileIR.lngDBID;
                        }
                    }
                }
                else
                    return;
            }
            /////////////////////////////////////////////////////////////////
            if (lngRecordID > 0)
            {
                subMarkIRAsDonor(lngRecordID, (txtFName.Text + " " + txtLName.Text).Replace("  ", " "), "");

                long lngCampaignID = 0;
                long lngPaymentTypeID = 0;
                long lngBillStateID = 0;

                try { lngCampaignID = ((clsCboItem)cboCampaign.SelectedItem).ID; }
                catch { lngCampaignID = 0; }

                try { lngPaymentTypeID = ((clsCboItem)cboPaymentType.SelectedItem).ID; }
                catch { lngPaymentTypeID = 2; }

                //add gift
                lngGiftID = clsDonorCRUD.fcnAddGift(chkMemorial.Checked, chkInHonorOf.Checked, ((clsCboItem)cboGiftCategory.SelectedItem).ID, lngRecordID, lngCampaignID, lngPaymentTypeID, lngBillStateID, 0, lngDonorExpressID, decimal.Parse(txtAmount.Text), DateTime.Parse(txtGiftDate.Text), txtMemorial.Text, txtInHonorOf.Text, txtAcctNum.Text, txtBankName.Text, txtAddress.Text, txtCity.Text, txtFName.Text + " " + txtLName.Text, txtHomePhone.Text, txtZip.Text, txtExpDate.Text, txtCCNumberUnMasked.Text, txtCVV2.Text, txtRoutingNum.Text, txtAuthNum.Text, txtPNRef.Text, txtXCAlias.Text, txtXCTransID.Text, txtEPSTransID.Text, txtEPSApprovalNumber.Text, txtEPSValidationCode.Text, txtEPSPmtAcctID.Text);

                //update billing info (this gives pnref for future transactions)
                clsIRCRUD.subUpdateBillingInfoCC(lngRecordID, txtPNRef.Text, txtCCNumberUnMasked.Text, txtXCAlias.Text, txtEPSPmtAcctID.Text);

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //mark gift processed
                    strSQL = "UPDATE tblDonorExpress " +
                            "SET blnProcessed=-1 " +
                            "WHERE lngDonorExpressID=" + lngDonorExpressID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }
            }

            DialogResult = DialogResult.OK;
            Close();
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
            using (Reports.frmDXDetailsViewer objDXDetailsViewer = new global::CTWebMgmt.Donor.Reports.frmDXDetailsViewer(lngDonorExpressID))
            {
                objDXDetailsViewer.WindowState = FormWindowState.Maximized;
                objDXDetailsViewer.ShowDialog();
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
                            clsLiveCharge.subProcessRefundXCEFT(decRefundAmt,lngDonorExpressID, txtAcctNum.Text, txtRoutingNum.Text, txtFName.Text + " " + txtLName.Text, txtAddress.Text, txtZip.Text, (int)this.Handle, clsLiveCharge.fcnGetXChargePath());
                        else
                            clsLiveCharge.subProcessRefundXCCC(decRefundAmt, txtXCTransID.Text, txtXCAlias.Text);
                    }
                    else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
                    {
                        if (cboPaymentType.SelectedItem.ToString() == "EFT")
                            clsLiveCharge.subProcessRefundCashLinqEFT(decRefundAmt, lngDonorExpressID, txtAcctNum.Text, txtRoutingNum.Text, txtFName.Text + " " + txtLName.Text, txtAddress.Text, txtZip.Text, (int)this.Handle);
                        else
                            clsLiveCharge.subProcessRefundCashLinqCC(decRefundAmt, txtPNRef.Text);
                    }
                    else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
                    {
                        if (cboPaymentType.SelectedItem.ToString() == "EFT")
                            clsLiveCharge.subProcessRefundEPSEFT(decRefundAmt);
                        else
                            clsLiveCharge.subProcessRefundEPSCC(decRefundAmt, lngDonorExpressID, txtEPSTransID.Text);
                    }
                }

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //mark gift processed
                    strSQL = "UPDATE tblDonorExpress " +
                            "SET blnProcessed=-1 " +
                            "WHERE lngDonorExpressID=" + lngDonorExpressID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.ExecuteNonQuery();
                    }

                    conDB.Close();
                }

                //refresh list
                //clsNav.subCloseProcessGifts();
                clsNav.objProcessGifts.subRefreshGrid();
                Close();
            }          
        }
    }
}