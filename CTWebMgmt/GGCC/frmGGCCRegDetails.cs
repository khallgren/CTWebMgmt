using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt.GGCC
{
    public partial class frmGGCCRegDetails : Form
    {
        public long lngGGCCRegWebID;
        public long lngRecordID = 0;
        public long lngRecordWebID = 0;

        public frmGGCCRegDetails()
        {
            lngGGCCRegWebID = 0;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseGGCCRegDetails();
        }

        private void frmGGCCRegDetails_Load(object sender, EventArgs e)
        {
            string strSQL;

            try
            {
                //fill contact/reg text boxes
                //fill attendee grid
                //fill activity grid
                //fill housing request grid

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblWebGGCCRegistrations.lngGGCCRegistrationWebID, tblWebGGCCRegistrations.lngRecordWebID, tblWebRecordsGGCCReg.lngRecordID, tblWebRecordsGGCCReg.lngStateID, " +
                                "tblGGCC.dteStartDate, tblWebGGCCRegistrations.dteDateRegistered, " +
                                "tblWebGGCCRegistrations.curDeposit, " +
                                "tblWebRecordsGGCCReg.strCompanyName, tblWebRecordsGGCCReg.strFirstName, tblWebRecordsGGCCReg.strLastCoName, tblWebRecordsGGCCReg.strHomePhone, tblWebRecordsGGCCReg.strWorkPhone, tblWebRecordsGGCCReg.strCellPhone, tblWebRecordsGGCCReg.strAddress, tblWebRecordsGGCCReg.strCity, tblWebRecordsGGCCReg.strZip, tblWebRecordsGGCCReg.strEmail, tblWebGGCCRegistrations.strPaymentType, tblWebGGCCRegistrations.strBankName, tblWebGGCCRegistrations.strAcctNum, tblWebGGCCRegistrations.strCardNum, tblWebGGCCRegistrations.strCVV2, tblWebGGCCRegistrations.strRoutingNum, tblWebGGCCRegistrations.strCardType, tblWebGGCCRegistrations.strCCExp, tblGGCC.strGGCCName, tblWebGGCCRegistrations.strPNRef, tblWebGGCCRegistrations.strXCTransID, tblWebGGCCRegistrations.strXCAlias, tblWebGGCCRegistrations.strEPSTransID, tblWebGGCCRegistrations.strEPSApprovalNumber, tblWebGGCCRegistrations.strEPSValidationCode, tblWebGGCCRegistrations.strEPSPmtAcctID " +
                            "FROM (tblWebGGCCRegistrations " +
                                "INNER JOIN tblWebRecordsGGCCReg ON tblWebGGCCRegistrations.lngRecordWebID = tblWebRecordsGGCCReg.lngRecordWebID) " +
                                "INNER JOIN tblGGCC ON tblWebGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID " +
                            "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + ";";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drReg = cmdDB.ExecuteReader())
                        {
                            if (drReg.Read())
                            {
                                txtGGCCRegistrationWebID.Text = drReg["lngGGCCRegistrationWebID"].ToString();
                                txtStartDate.Text = drReg["dteStartDate"].ToString();
                                txtRegDate.Text = drReg["dteDateRegistered"].ToString();
                                txtCompanyName.Text = drReg["strCompanyName"].ToString();
                                txtFName.Text = drReg["strFirstName"].ToString();
                                txtLName.Text = drReg["strLastCoName"].ToString();
                                txtHomePhone.Text = drReg["strHomePhone"].ToString();
                                txtWorkPhone.Text = drReg["strWorkPhone"].ToString();
                                txtCellPhone.Text = drReg["strCellPhone"].ToString();
                                txtAddress.Text = drReg["strAddress"].ToString();
                                txtCity.Text = drReg["strCity"].ToString();
                                txtZip.Text = drReg["strZip"].ToString();
                                txtEMail.Text = drReg["strEmail"].ToString();
                                txtBankName.Text = drReg["strBankName"].ToString();
                                txtAcctNum.Text = drReg["strAcctNum"].ToString();
                                txtCardNum.Text = drReg["strCardNum"].ToString();
                                txtCVV2.Text = drReg["strCVV2"].ToString();
                                txtCCExp.Text = drReg["strCCExp"].ToString();
                                txtRoutingNum.Text = drReg["strRoutingNum"].ToString();
                                txtCardType.Text = drReg["strCardType"].ToString();
                                txtGGCCName.Text = drReg["strGGCCName"].ToString();

                                clsCboSources.subFillStateCbo(ref cboState, long.Parse(drReg["lngStateID"].ToString()));

                                txtAcctNum.Visible = false;
                                txtRoutingNum.Visible = false;
                                txtBankName.Visible = false;
                                txtCardNum.Visible = false;
                                txtCardType.Visible = false;
                                txtCVV2.Visible = false;
                                txtCCExp.Visible = false;
                                lblAcctNum.Visible = false;
                                lblRoutingNum.Visible = false;
                                lblBankName.Visible = false;
                                lblCardNum.Visible = false;
                                lblCardType.Visible = false;
                                lblCVV2.Visible = false;
                                lblCCExp.Visible = false;

                                if (drReg["strPaymentType"].ToString() == "EFT")
                                {
                                    clsCboSources.subFillPmtTypeCbo(ref cboPmtType, 11);
                                    txtAcctNum.Visible = true;
                                    txtRoutingNum.Visible = true;
                                    txtBankName.Visible = true;
                                    lblAcctNum.Visible = true;
                                    lblRoutingNum.Visible = true;
                                    lblBankName.Visible = true;
                                }
                                else
                                {
                                    clsCboSources.subFillPmtTypeCbo(ref cboPmtType, 2);
                                    txtCardNum.Visible = true;
                                    txtCardType.Visible = true;
                                    txtCVV2.Visible = true;
                                    txtCCExp.Visible = true;
                                    lblCardNum.Visible = true;
                                    lblCardType.Visible = true;
                                    lblCVV2.Visible = true;
                                    lblCCExp.Visible = true;
                                }

                                lblXCTransID.Visible = false;
                                txtXCTransID.Visible = false;
                                lblXCAlias.Visible = false;
                                txtXCAlias.Visible = false;
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

                                if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
                                {
                                    lblXCTransID.Visible = true;
                                    txtXCTransID.Visible = true;
                                    lblXCAlias.Visible = true;
                                    txtXCAlias.Visible = true;

                                    try { txtXCAlias.Text = Convert.ToString(drReg["strXCAlias"]); }
                                    catch { txtXCAlias.Text = "ERR"; }

                                    try { txtXCTransID.Text = Convert.ToString(drReg["strXCTransID"]); }
                                    catch { txtXCTransID.Text = "ERR"; }
                                }
                                else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
                                {
                                    lblPNRef.Visible = true;
                                    txtPNRef.Visible = true;

                                    try { txtPNRef.Text = Convert.ToString(drReg["strPNRef"]); }
                                    catch { txtPNRef.Text = ""; }
                                }
                                else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
                                {
                                    lblEPSApprovalNumber.Visible = true;
                                    lblEPSPmtAcctID.Visible = true;
                                    lblEPSTransID.Visible = true;
                                    lblEPSValidationCode.Visible = true;
                                    txtEPSApprovalNumber.Visible = true;
                                    txtEPSPmtAcctID.Visible = true;
                                    txtEPSTransID.Visible = true;
                                    txtEPSValidationCode.Visible = true;

                                    try { txtEPSApprovalNumber.Text = Convert.ToString(drReg["strEPSApprovalNumber"]); }
                                    catch { txtEPSApprovalNumber.Text = ""; }

                                    try { txtEPSPmtAcctID.Text = Convert.ToString(drReg["strEPSPmtAcctID"]); }
                                    catch { txtEPSPmtAcctID.Text = ""; }

                                    try { txtEPSTransID.Text = Convert.ToString(drReg["strEPSTransID"]); }
                                    catch { txtEPSTransID.Text = ""; }

                                    try { txtEPSValidationCode.Text = Convert.ToString(drReg["strEPSValidationCode"]); }
                                    catch { txtEPSValidationCode.Text = ""; }
                                }

                                try { lngRecordID = Convert.ToInt32(drReg["lngRecordID"]); }
                                catch { lngRecordID = 0; }

                                try { lngRecordWebID = Convert.ToInt32(drReg["lngRecordWebID"]); }
                                catch { lngRecordWebID = 0; }

                                try { txtPmtAmt.Text = Convert.ToDecimal(drReg["curDeposit"]).ToString("C"); }
                                catch { txtPmtAmt.Text = ""; }
                            }

                            drReg.Close();
                        }

                        /*//////////////////////////////////////////////////////////////////////////////*/
                        //attendee grid
                        /*//////////////////////////////////////////////////////////////////////////////*/
                        strSQL = "SELECT " +
                                    "(SELECT Count(tblWebGGCCRegAttendees.lngGGCCRegAttendeeWebID) AS intMCount " +
                                    "FROM tblWebGGCCRegAttendees " +
                                    "WHERE tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                        "tblWebGGCCRegAttendees.intGender=-1 AND " +
                                        "tblWebGGCCRegAttendees.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                                    "GROUP BY tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID, tblWebGGCCRegAttendees.lngGGCCRegistrationWebID " +
                                    ") AS intMCount, " +
                                    "(SELECT Count(tblWebGGCCRegAttendees.lngGGCCRegAttendeeWebID) AS intFCount " +
                                    "FROM tblWebGGCCRegAttendees " +
                                    "WHERE tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                        "tblWebGGCCRegAttendees.intGender=0 AND " +
                                        "tblWebGGCCRegAttendees.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                                    "GROUP BY tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID, tblWebGGCCRegAttendees.lngGGCCRegistrationWebID " +
                                    ") AS intFCount, " +
                                    "(SELECT Count(tblWebGGCCRegAttendees.lngGGCCRegAttendeeWebID) AS intNACount " +
                                    "FROM tblWebGGCCRegAttendees " +
                                    "WHERE tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID=tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID AND " +
                                        "tblWebGGCCRegAttendees.intGender=3 AND " +
                                        "tblWebGGCCRegAttendees.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                                    "GROUP BY tblWebGGCCRegAttendees.lngGGCCAttendeeStatsID, tblWebGGCCRegAttendees.lngGGCCRegistrationWebID " +
                                    ") AS intNACount, " +
                                    "tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID, " +
                                    "[tlkpGuestTypes].[strGuestType] & \", \" & [tblCabinCategories].[strCabinCategory] AS strGuestType " +
                                "FROM ((((tblWebGGCCRegistrations " +
                                    "INNER JOIN tblGGCC ON tblWebGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID) " +
                                    "INNER JOIN tblnkGGCCAttendeeStats ON tblGGCC.lngGGCCID = tblnkGGCCAttendeeStats.lngGGCCID) " +
                                    "INNER JOIN tblAdvRateTypes ON tblnkGGCCAttendeeStats.lngAdvRateTypeID = tblAdvRateTypes.lngAdvRateTypeID) " +
                                    "INNER JOIN tblCabinCategories ON tblAdvRateTypes.lngCabinCategoryID = tblCabinCategories.lngCabinCategoryID) " +
                                    "INNER JOIN tlkpGuestTypes ON tblAdvRateTypes.lngGuestTypeID = tlkpGuestTypes.lngGuestTypeID " +
                                "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                                "ORDER BY tblCabinCategories.strCabinCategory, tlkpGuestTypes.strGuestType;";

                        BindingSource srcAttendees = new BindingSource();

                        grdAttendees.DataSource = srcAttendees;

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        using (OleDbDataAdapter daAtt = new OleDbDataAdapter(cmdDB))
                        {
                            // Populate a new data table and bind it to the BindingSource.
                            DataTable tblAtt = new DataTable();

                            tblAtt.Locale = System.Globalization.CultureInfo.InvariantCulture;

                            daAtt.Fill(tblAtt);

                            srcAttendees.DataSource = tblAtt;

                            grdAttendees.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                            grdAttendees.Columns["colGGCCAttendeeStatsID"].Visible = false;
                        }

                        /*//////////////////////////////////////////////////////////////////////////////*/
                        //activity grid
                        /*//////////////////////////////////////////////////////////////////////////////*/
                        strSQL = "SELECT tblWebGGCCRegActivities.lngGGCCActivityID, tlkpGGCCActivities.strActivityName, Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\") AS dteActivityDateTime, Sum(tblWebGGCCRegActivities.intParticipants) AS intParticipants " +
                                "FROM (tblWebGGCCRegActivities " +
                                    "INNER JOIN tblGGCCActivities ON tblWebGGCCRegActivities.lngGGCCActivityID = tblGGCCActivities.lngGGCCActivityID) " +
                                    "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID " +
                                "WHERE tblWebGGCCRegActivities.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + " " +
                                "GROUP BY tblWebGGCCRegActivities.lngGGCCActivityID, " +
                                    "tlkpGGCCActivities.strActivityName, Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\") " +
                                "ORDER BY Format([tblGGCCActivities].[dteActivityDate],\"m/d/yyyy\") & \" \" & Format([tblGGCCActivities].[dteActivityTime],\"h:nn ampm\")";

                        BindingSource srcActivities = new BindingSource();

                        grdActivities.DataSource = srcActivities;

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        using (OleDbDataAdapter daAct = new OleDbDataAdapter(cmdDB))
                        {
                            // Populate a new data table and bind it to the BindingSource.
                            DataTable tblAct = new DataTable();

                            tblAct.Locale = System.Globalization.CultureInfo.InvariantCulture;

                            daAct.Fill(tblAct);

                            srcActivities.DataSource = tblAct;

                            grdActivities.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                            grdActivities.Columns["colGGCCActivityID"].Visible = false;
                        }

                        /*//////////////////////////////////////////////////////////////////////////////*/
                        //housing request grid
                        /*//////////////////////////////////////////////////////////////////////////////*/
                        strSQL = "SELECT tblWebGGCCRegHousingRequests.lngGGCCRegHousingRequestID, tblWebGGCCRegHousingRequests.lngCount, " +
                                    "tblWebGGCCRegHousingRequests.curCharge, " +
                                    "tlkpHousingName.strHousingName " +
                                "FROM tblWebGGCCRegHousingRequests " +
                                    "LEFT JOIN tlkpHousingName ON tblWebGGCCRegHousingRequests.lngHousingID = tlkpHousingName.lngHousingID " +
                                "WHERE tblWebGGCCRegHousingRequests.lngGGCCRegistrationWebID=" + lngGGCCRegWebID;

                        BindingSource srcHousing = new BindingSource();

                        grdHousing.DataSource = srcHousing;

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        using (OleDbDataAdapter daHousing = new OleDbDataAdapter(cmdDB))
                        {
                            // Populate a new data table and bind it to the BindingSource.
                            DataTable tblHousing = new DataTable();

                            tblHousing.Locale = System.Globalization.CultureInfo.InvariantCulture;

                            daHousing.Fill(tblHousing);

                            srcHousing.DataSource = tblHousing;

                            grdHousing.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                            grdHousing.Columns["colGGCCRegHousingRequestID"].Visible = false;
                            grdHousing.Columns["colCharge"].DefaultCellStyle.Format = "c";
                        }
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmGGCCRegDetails.Load", ex);
            }

            subValidateCCExp(lngGGCCRegWebID);
        }

        private void subValidateCCExp(long _lngGGCCRegWebID)
        {
            try
            {
                if (txtCCExp.Text.Length > 4)
                {
                    string strCCExp = "";

                    strCCExp = txtCCExp.Text.Substring(0, 2) + txtCCExp.Text.Substring(txtCCExp.Text.Length - 2, 2);

                    int intCCExp = 0;

                    try { intCCExp = Convert.ToInt32(strCCExp); }
                    catch { intCCExp = 0; }

                    if (intCCExp <= 0)
                        strCCExp = "ERR";

                    //update reg
                    txtCCExp.Text = strCCExp;

                    string strSQL = "";

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        strSQL = "UPDATE tblWebGGCCRegistrations " +
                                "SET strCCExp=@strCCExp " +
                                "WHERE lngGGCCRegistrationWebID=@lngGGCCRegistrationWebID";

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCExp", strCCExp));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngGGCCRegistrationWebID", _lngGGCCRegWebID));

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }
                        }

                        conDB.Close();
                    }
                }
            }
            catch { }
        }

        private void btnAddRegistration_Click(object sender, EventArgs e)
        {
            //reconcile registration if it already exists
            //otherwise add new

            long lngGGCCRegID;

            bool blnPmtAdded = false;

            try
            {
                clsGlobalEnum.conCheckExistingRegRes lngRes = clsGGCCCRUD.fcnGGCCRegWebExists(lngGGCCRegWebID);

                if (lngRes == clsGlobalEnum.conCheckExistingRegRes.DoesntExist)
                {
                    lngRecordID = clsGGCCCRUD.fcnGetWebRegIR(this);
                    clsIRCRUD.subSetToSync(lngRecordID);

                    if (lngRecordID > 0)
                    {
                        //add reg
                        lngGGCCRegID = clsGGCCCRUD.fcnAddWebReg(lngGGCCRegWebID, lngRecordID);

                        //add attendees
                        clsGGCCCRUD.subAddWebRegAtt(lngGGCCRegWebID, lngGGCCRegID);

                        //add activities
                        clsGGCCCRUD.subAddWebRegAct(lngGGCCRegWebID, lngGGCCRegID);

                        //add housing requests
                        clsGGCCCRUD.subAddWebRegHousingRequests(lngGGCCRegWebID, lngGGCCRegID);

                        //add payment
                        blnPmtAdded = clsGGCCCRUD.fcnAddWebRegPmt((int)this.Handle, lngGGCCRegWebID, lngGGCCRegID, lngRecordID);

                        //add discount
                        clsGGCCCRUD.subAddWebRegDiscount(lngGGCCRegWebID, lngGGCCRegID);

                        //delete from queue when processed
                        if (blnPmtAdded) clsGGCCCRUD.subDeleteProcessedReg(lngGGCCRegWebID);

                        //refresh list
                        clsNav.subCloseGGCCRegDetails();
                        clsNav.subCloseProcessGGCCReg();
                    }
                }
                else if (lngRes == clsGlobalEnum.conCheckExistingRegRes.Cancel)
                {
                    MessageBox.Show("Reconcile records cancelled.  No action taken.");
                }
                else if (lngRes == clsGlobalEnum.conCheckExistingRegRes.Exists)
                {
                    //registrations reconciled--delete web queue
                    MessageBox.Show("Registrations reconciled.");
                    clsGGCCCRUD.subDeleteProcessedReg(lngGGCCRegWebID);
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmGGCCRegDetails", ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to remove this registration without processing it?", "Continue?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //prompt for refund
                if (MessageBox.Show("Would you like to issue a refund for the deposit?", "Refund", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    decimal decRefundAmt = 0;

                    using (frmCollectRefundAmt objCollectRefundAmt = new frmCollectRefundAmt(lngGGCCRegWebID))
                    {
                        if (objCollectRefundAmt.ShowDialog() == DialogResult.OK)
                        {
                            decRefundAmt = objCollectRefundAmt.decAmt;

                            if (decRefundAmt > 0)
                            {
                                if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
                                {
                                    if (cboPmtType.SelectedItem.ToString() == "EFT")
                                        clsLiveCharge.subProcessRefundXCEFT(decRefundAmt, lngGGCCRegWebID, txtAcctNum.Text, txtRoutingNum.Text, txtFName.Text + " " + txtLName.Text, txtAddress.Text, txtZip.Text, (int)this.Handle, clsLiveCharge.fcnGetXChargePath());
                                    else
                                        clsLiveCharge.subProcessRefundXCCC(decRefundAmt, txtXCTransID.Text, txtXCAlias.Text);
                                }
                                else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
                                {
                                    if (cboPmtType.SelectedItem.ToString() == "EFT")
                                        clsLiveCharge.subProcessRefundCashLinqEFT(decRefundAmt, lngGGCCRegWebID, txtAcctNum.Text, txtRoutingNum.Text, txtFName.Text + " " + txtLName.Text, txtAddress.Text, txtZip.Text, (int)this.Handle);
                                    else
                                        clsLiveCharge.subProcessRefundCashLinqCC(decRefundAmt, txtPNRef.Text);
                                }
                                else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
                                {
                                    if (cboPmtType.SelectedItem.ToString() == "EFT")
                                        clsLiveCharge.subProcessRefundEPSEFT(decRefundAmt);
                                    else
                                        clsLiveCharge.subProcessRefundEPSCC(decRefundAmt, lngGGCCRegWebID, txtEPSTransID.Text);
                                }

                            }
                            else
                                return;
                        }
                        else
                            return;
                    }

                    string strMsg = "Would you like to record the deposit and refund in CampTrak?";

                    if (MessageBox.Show(strMsg, "Save in CampTrak?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        clsIR irToSearch = new clsIR(0, ((clsCboItem)cboState.SelectedItem).ID, txtFName.Text, txtLName.Text, "", txtAddress.Text, txtCity.Text, txtZip.Text, txtHomePhone.Text, "", txtCellPhone.Text, txtEMail.Text);

                        irToSearch.lngRecordWebID = lngRecordWebID;
                        irToSearch.lngRecordID = lngRecordID;

                        //find record
                        using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find registrant's record"))
                        {
                            if (objFindIR.ShowDialog() == DialogResult.OK)
                            {
                                if (objFindIR.irToSearch.lngRecordID == 0)
                                    return;
                                else
                                    lngRecordID = objFindIR.irToSearch.lngRecordID;
                            }
                        }

                        //add transactions
                        //////////////////////////////////////////////////////////////////////////
                        decimal decPmtAmt = 0;

                        try { decPmtAmt = decimal.Parse(txtPmtAmt.Text, System.Globalization.NumberStyles.Currency); }
                        catch { decPmtAmt = 0; }

                        using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                        {
                            conDB.Open();

                            string strSQL = "";

                            //add payment
                            strSQL = "INSERT INTO tblTransactions " +
                                    "(blnMarkedForCC, " +
                                        "lngPaymentTypeID, lngTransTypeID, lngRecordID, lngBillStateID, lngUserID, " +
                                        "curPayment, " +
                                        "dteDateAdded, " +
                                        "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strPNRef, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID ) " +
                                    "SELECT 1, " +
                                        "@lngPaymentTypeID, 8 AS lngTransTypeID, @lngRecordID, @lngBillStateID, @lngUserID, " +
                                        "@curPayment, " +
                                        "@dteDateAdded, " +
                                        "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strPNRef, @strEPSTransID, @strEPSApprovalNumber, @strEPSValidationCode, @strEPSPmtAcctID ";

                            using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                            {
                                long lngPmtType = 0;

                                //if (txtBankName.Text != "")
                                //    lngPmtType = 11;
                                //else
                                lngPmtType = 2;

                                long lngBillStateID = 0;

                                try { lngBillStateID = ((clsCboItem)cboState.SelectedItem).ID; }
                                catch { lngBillStateID = 0; }

                                cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                                cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decPmtAmt));

                                cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", OleDbType.Date));
                                cmdDB.Parameters["@dteDateAdded"].Value = DateTime.Now;

                                cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Tuition"));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtCCExp.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtFName.Text + " " + txtLName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtAddress.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtCity.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtZip.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtHomePhone.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));

                                cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));

                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                //add refund record
                                strSQL = "INSERT INTO tblTransactions " +
                                        "(blnMarkedForCC, " +
                                            "lngTransTypeID, lngTransSubTypeID, lngBillStateID, lngRecordID, lngUserID, " +
                                            "curCharge, " +
                                            "dteDateAdded, " +
                                            "strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strCCNumber, strCCExpDate, strTransactionDesc) " +
                                        "VALUES " +
                                        "(1, " +
                                            "4, 0, @lngBillStateID, @lngRecordID, @lngUserID, " +
                                            "@curCharge, " +
                                            "Now(), " +
                                            "@strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strCCNumber, @strCCExpDate, \"Refund for online registration\")";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngCTUserID));
                                cmdDB.Parameters.Add(new OleDbParameter("@curCharge", decRefundAmt));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtFName.Text + " " + txtLName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtAddress.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtCity.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtZip.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtHomePhone.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtCCExp.Text));

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                            }//</command>

                            conDB.Close();
                        }//</connection>
                    }//</save in ct>                    
                }

                clsGGCCCRUD.subDeleteProcessedReg(lngGGCCRegWebID);
                clsNav.subCloseGGCCRegDetails();
                clsNav.subCloseProcessGGCCReg();
            }
        }
    }
}