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
    public partial class frmIndRegDetails : Form
    {
        private long lngRegWebID;
        private long lngRecordWebID = 0;
        private long lngRecordID = 0;
        private int intCustIRTopL = 0;
        private int intCustIRTopR = 0;
        private int intCustRegTopL = 0;
        private int intCustRegTopR = 0;
        private bool blnCustIRRightCol = false;

        public frmIndRegDetails(long _lngRegWebID)
        {
            InitializeComponent();

            lngRegWebID = _lngRegWebID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void subLoadCbo(ComboBox _cboToLoad, long _lngDefaultID)
        {
            string strSQL = "";

            switch (_cboToLoad.Name)
            {
                case "cboCamperState":
                case "cboBillState":
                    strSQL = "SELECT tlkpStates.lngStateID AS lngID, " +
                                "tlkpStates.strState AS strValue " +
                            "FROM tlkpStates " +
                            "ORDER BY tlkpStates.strState;";

                    break;

                case "cboPmtMethod":
                    strSQL = "SELECT tlkpPaymentType.lngPaymentTypeID AS lngID, " +
                                "tlkpPaymentType.strPaymentType AS strValue " +
                            "FROM tlkpPaymentType " +
                            "WHERE tlkpPaymentType.lngPaymentTypeID=2 OR " +
                                "tlkpPaymentType.lngPaymentTypeID=11 " +
                            "ORDER BY tlkpPaymentType.strPaymentType;";

                    break;

                case "cboBlockChoice1":
                case "cboBlockChoice2":
                case "cboBlockChoice3":
                case "cboBlockChoice4":
                case "cboBlockChoice5":
                case "cboBlockChoice6":
                case "cboBlockChoice7":
                case "cboBlockChoice8":
                case "cboBlockChoice9":
                case "cboBlockChoice10":
                case "cboBlockChoice11":
                    strSQL = "SELECT tblBlock.lngBlockID AS lngID, " +
                                "tblBlock.strBlockCode AS strValue " +
                            "FROM tblBlock " +
                            "ORDER BY tblBlock.strBlockCode;";

                    break;
            }

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCbo = cmdDB.ExecuteReader())
                    {
                        while (drCbo.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drCbo["lngID"]), Convert.ToString(drCbo["strValue"]));

                            _cboToLoad.Items.Add(cboNew);

                            if (Convert.ToInt32(drCbo["lngID"]) == _lngDefaultID) _cboToLoad.SelectedItem = cboNew;
                        }

                        drCbo.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subLoadCbo(ComboBox _cboToLoad, string _strDefault)
        {
            string strSQL = "";

            switch (_cboToLoad.Name)
            {
                case "cboReferredBy":
                    strSQL = "SELECT tblReferredBy.strReferredBy AS strValue " +
                            "FROM tblReferredBy " +
                            "ORDER BY tblReferredBy.strReferredBy";

                    break;
            }

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCbo = cmdDB.ExecuteReader())
                    {
                        while (drCbo.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToString(drCbo["strValue"]), Convert.ToString(drCbo["strValue"]));

                            _cboToLoad.Items.Add(cboNew);

                            if (Convert.ToString(drCbo["strValue"]) == _strDefault) _cboToLoad.SelectedItem = cboNew;
                        }

                        drCbo.Close();
                    }
                }

                conDB.Close();
            }

            _cboToLoad.Items.Insert(0, new clsCboItem("", ""));
        }

        private int fcnUpdateCurrentEnrollment(long _lngBlockID)
        {
            string strSQL = "";

            int intRes = 0;
            int intCurrentRegNotHeld = 0;
            int intHeldSpots = 0;
            int intUsedHeldSpots = 0;
            int intUnusedHeldSpots = 0;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand())
                {
                    cmdDB.Connection = conDB;

                    //current enrollment
                    strSQL = "SELECT COUNT(tblRegistrations.lngRegistrationID) AS intRegCount " +
                            "FROM tblRegistrations " +
                            "WHERE tblRegistrations.lngBlockID=" + _lngBlockID.ToString();

                    cmdDB.CommandText = strSQL;

                    try { intCurrentRegNotHeld = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intCurrentRegNotHeld = 0; }

                    //spots reserved by active holds
                    strSQL = "SELECT SUM(tblRegHold.intHoldQty) AS intTotHeld " +
                            "FROM tblRegHold " +
                            "WHERE tblRegHold.lngBlockID=" + _lngBlockID.ToString() + " AND " +
                                "DateDiff(\"d\", Now(), tblRegHold.dteDeadline) > 0";

                    cmdDB.CommandText = strSQL;

                    try { intHeldSpots = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch (Exception ex) { intHeldSpots = 0; }

                    //spots used by active holds
                    strSQL = "SELECT COUNT(tblRegistrations.lngRegistrationID) AS intUsedHeldCount " +
                            "FROM tblRegHold " +
                                "INNER JOIN tblRegistrations ON tblRegHold.lngRegHoldID = tblRegistrations.lngRegHoldID " +
                            "WHERE tblRegistrations.lngBlockID=" + _lngBlockID.ToString() + " AND " +
                                "DateDiff(\"d\", Now(), tblRegHold.dteDeadline) > 0";

                    cmdDB.CommandText = strSQL;

                    try { intUsedHeldSpots = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch (Exception ex) { intUsedHeldSpots = 0; }

                    //remaining held spots
                    intUnusedHeldSpots = intHeldSpots - intUsedHeldSpots;

                    //total current:  reg count + unused held spots
                    intRes = intCurrentRegNotHeld + intUnusedHeldSpots;

                    //update block
                    strSQL = "UPDATE tblBlock " +
                            "SET tblBlock.intCurrEnrollment = " + intRes.ToString() + " " +
                            "WHERE tblBlock.lngBlockID=" + _lngBlockID.ToString();

                    cmdDB.CommandText = strSQL;

                    try { cmdDB.ExecuteNonQuery(); }
                    catch { }
                }

                conDB.Close();
            }

            //return val
            return intRes;
        }

        private void subSetBlockFull(int _intBlockChoice, long _lngBlockID, int _intBlockCap, int _intWaitingList)
        {
            //update current enrollments for all blocks
            int intCurrentEnrollment = 0;

            try { intCurrentEnrollment = fcnUpdateCurrentEnrollment(_lngBlockID); }
            catch { intCurrentEnrollment = 0; }

            if (_lngBlockID > 0 && ((intCurrentEnrollment >= _intBlockCap) || _intWaitingList > 0))
                ((Label)(pagRegInfo.Controls["lblFull" + _intBlockChoice.ToString()])).Visible = true;
            else
                ((Label)(pagRegInfo.Controls.Find("lblFull" + _intBlockChoice.ToString(), true)[0])).Visible = false;
        }

        private void frmIndRegDetails_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand())
                {
                    cmdDB.Connection = conDB;

                    string strDiscounts = "";

                    for (int intI = 1; intI <= 10; intI++)
                        strDiscounts += "tblWebIndRegistrations.blnDiscount" + intI.ToString() + ", ";

                    strSQL = "SELECT tblWebRecords_Camper.blnGender, tblWebIndRegistrations.blnSpecialNeeds, " + strDiscounts +
                                "tblWebRecords_Parent.lngRecordWebID AS lngRecordWebID_Parent, tblWebRecords_Camper.intGradeCompleted, tblWebRecords_Camper.lngRecordWebID AS lngRecordWebID_Camper, tblWebRecords_Camper.lngRecordID AS lngRecordID_Camper, tblWebRecords_Camper.lngStateID, tblWebRecords_Camper.lngCountryID, tblWebIndRegistrations.lngConfMethodID, " +
                                "tblWebIndRegistrations.curDeposit, tblWebIndRegistrations.curDonation, tblWebIndRegistrations.curSpendingMoney, " +
                                "tblWebRecords_Camper.dteBirthDate, tblWebIndRegistrations.dteRegistrationDate, " +
                                "tblWebRecords_Parent.strLastCoName AS strLName_Parent, tblWebRecords_Parent.strFirstName AS strFName_Parent, tblWebRecords_Camper.strLastCoName AS strLName_Camper, tblWebRecords_Camper.strFirstName AS strFName_Camper, tblWebRecords_Camper.strCompanyName, tblWebRecords_Camper.strEmail, tblWebRecords_Camper.strAddress, tblWebRecords_Camper.strZip, tblWebRecords_Camper.strWorkExt, tblWebRecords_Camper.strWorkPhone, tblWebRecords_Camper.strCellPhone, tblWebRecords_Camper.strCity, tblWebRecords_Parent.strEmail AS strConfEmail, tblWebRecords_Camper.strMI, tblWebRecords_Camper.strHomePhone, tblWebIndRegistrations.strBuddy1, tblWebIndRegistrations.strBuddy2, tblWebIndRegistrations.strReleaseTo, tblWebIndRegistrations.strXCTransID, tblWebIndRegistrations.strXCAlias, tblWebIndRegistrations.strXCAuthCode, tblWebIndRegistrations.strPNRef, tblWebIndRegistrations.strEPSApprovalNumber, tblWebIndRegistrations.strEPSPmtAcctID, tblWebIndRegistrations.strEPSTransID, tblWebIndRegistrations.strEPSValidationCode, tblWebIndRegistrations.strPmtType, tblWebIndRegistrations.strRoutingNumber, tblWebIndRegistrations.strAcctNumber, tblWebRecords_Parent.strReferredBy, tblWebIndRegistrations.strCardNumber, tblWebRecords_Camper.strMotherName, tblWebRecords_Camper.strFatherName, tblWebIndRegistrations.strOrgCode, " +
                                "tblWebRecords_Camper.mmoSpecialNeeds, tblWebRecords_Camper.mmoNotes, tblWebIndRegistrations.strDependNotes, tblWebIndRegistrations.mmoRegNotes " +
                            "FROM (tblWebIndRegistrations " +
                                "INNER JOIN tblWebRecords AS tblWebRecords_Camper ON tblWebIndRegistrations.lngRecordWebID = tblWebRecords_Camper.lngRecordWebID) " +
                                "INNER JOIN tblWebRecords AS tblWebRecords_Parent ON tblWebRecords_Camper.lngProfileWebID = tblWebRecords_Parent.lngRecordWebID " +
                            "WHERE tblWebIndRegistrations.lngRegistrationWebID=" + lngRegWebID.ToString();

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drReg = cmdDB.ExecuteReader())
                    {
                        if (drReg.Read())
                        {
                            bool blnGender;

                            try { blnGender = Convert.ToBoolean(drReg["blnGender"]); }
                            catch { blnGender = true; }

                            if (blnGender) { radM.Checked = true; }
                            else { radF.Checked = true; }

                            bool blnSpecNeeds = false;

                            try { blnSpecNeeds = Convert.ToBoolean(drReg["blnSpecialNeeds"]); }
                            catch { blnSpecNeeds = false; }

                            btnSpecialNeeds.Visible = blnSpecNeeds;

                            try { lngRecordWebID = Convert.ToInt32(drReg["lngRecordWebID_Camper"]); }
                            catch { lngRecordWebID = 0; }

                            try { lngRecordID = Convert.ToInt32(drReg["lngRecordID_Camper"]); }
                            catch { lngRecordID = 0; }

                            txtCamperID.Text = lngRecordWebID.ToString();

                            long lngStateID = 0;

                            try { lngStateID = Convert.ToInt32(drReg["lngStateID"]); }
                            catch { lngStateID = 0; }

                            subLoadCbo(cboCamperState, lngStateID);

                            txtCamperGrade.Text = Convert.ToString(drReg["intGradeCompleted"]);

                            txtProfileName.Text = Convert.ToString(drReg["strFName_Parent"]) + " " + Convert.ToString(drReg["strLName_Parent"]);
                            btnProfileDetails.Tag = Convert.ToInt32(drReg["lngRecordWebID_Parent"]);
                            txtProfileWebID.Text = Convert.ToString(drReg["lngRecordWebID_Parent"]);

                            try { txtPmtAmt.Text = Convert.ToDecimal(drReg["curDeposit"]).ToString("C"); }
                            catch { txtPmtAmt.Text = ""; }

                            decimal curDonation = 0;

                            try { curDonation = Convert.ToDecimal(drReg["curDonation"]); }
                            catch { curDonation = 0; }

                            txtDonation.Text = curDonation.ToString("C");

                            if (curDonation > 0)
                            {
                                fraApplyDonationTo.Visible = true;
                                radDonationToParent.Checked = true;
                                radDonationToCamper.Checked = false;
                            }
                            else
                                fraApplyDonationTo.Visible = false;

                            decimal curSpending = 0;

                            try { curSpending = Convert.ToDecimal(drReg["curSpendingMoney"]); }
                            catch { curSpending = 0; }

                            txtSpending.Text = curSpending.ToString("C");

                            try { txtCamperBDate.Text = ((DateTime)drReg["dteBirthDate"]).ToString("MM/dd/yyyy"); }
                            catch { txtCamperBDate.Text = ""; }

                            try { txtRegDate.Text = Convert.ToDateTime(drReg["dteRegistrationDate"]).ToString("MM/dd/yyyy h:mm tt"); }
                            catch { txtRegDate.Text = ""; }

                            txtCamperFName.Text = Convert.ToString(drReg["strFName_Camper"]);
                            txtCamperLName.Text = Convert.ToString(drReg["strLName_Camper"]);
                            txtCamperMI.Text = Convert.ToString(drReg["strMI"]);
                            txtCamperAddress.Text = Convert.ToString(drReg["strAddress"]);
                            txtCamperCity.Text = Convert.ToString(drReg["strCity"]);
                            txtCamperZip.Text = Convert.ToString(drReg["strZip"]);
                            txtCamperPhone.Text = Convert.ToString(drReg["strHomePhone"]);
                            txtCamperCellPhone.Text = Convert.ToString(drReg["strCellPhone"]);
                            txtCamperEMail.Text = Convert.ToString(drReg["strEmail"]);

                            try { txtProfileEmail.Text = Convert.ToString(drReg["strConfEmail"]); }
                            catch { txtProfileEmail.Text = ""; }

                            txtBuddy1.Text = Convert.ToString(drReg["strBuddy1"]);
                            txtBuddy2.Text = Convert.ToString(drReg["strBuddy2"]);
                            txtReleaseTo.Text = Convert.ToString(drReg["strReleaseTo"]);

                            txtMother.Text = Convert.ToString(drReg["strMotherName"]);
                            txtFather.Text = Convert.ToString(drReg["strFatherName"]);

                            //configure payment display details
                            lblXCAlias.Visible = false;
                            lblXCTransID.Visible = false;
                            txtXCAlias.Visible = false;
                            txtXCTransID.Visible = false;
                            txtXCAuthCode.Visible = false;
                            lblXCAuthCode.Visible = false;
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
                                txtXCTransID.Text = Convert.ToString(drReg["strXCTransID"]);
                                txtXCAlias.Text = Convert.ToString(drReg["strXCAlias"]);

                                try { txtXCAuthCode.Text = Convert.ToString(drReg["strXCAuthCode"]); }
                                catch { }

                                lblXCAlias.Visible = true;
                                lblXCTransID.Visible = true;
                                txtXCAlias.Visible = true;
                                txtXCTransID.Visible = true;
                                lblXCAuthCode.Visible = true;
                                txtXCAuthCode.Visible = true;
                            }
                            else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
                            {
                                txtPNRef.Text = Convert.ToString(drReg["strPNRef"]);

                                lblPNRef.Visible = true;
                                txtPNRef.Visible = true;
                            }
                            else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
                            {
                                lblEPSApprovalNumber.Visible = true;
                                lblEPSPmtAcctID.Visible = true;
                                lblEPSTransID.Visible = true;
                                lblEPSValidationCode.Visible = true;

                                //set vals
                                txtEPSApprovalNumber.Text = Convert.ToString(drReg["strEPSApprovalNumber"]);
                                txtEPSPmtAcctID.Text = Convert.ToString(drReg["strEPSPmtAcctID"]);
                                txtEPSTransID.Text = Convert.ToString(drReg["strEPSTransID"]);
                                txtEPSValidationCode.Text = Convert.ToString(drReg["strEPSValidationCode"]);

                                txtEPSApprovalNumber.Visible = true;
                                txtEPSPmtAcctID.Visible = true;
                                txtEPSTransID.Visible = true;
                                txtEPSValidationCode.Visible = true;
                            }

                            txtPmtType.Text = Convert.ToString(drReg["strPmtType"]);
                            txtRoutingNum.Text = Convert.ToString(drReg["strRoutingNumber"]);
                            txtAcctNum.Text = Convert.ToString(drReg["strAcctNumber"]);

                            string strCardNumber = "";

                            try { strCardNumber = Convert.ToString(drReg["strCardNumber"]); }// clsEncryption.fcnDecrypt(Convert.ToString(drReg["strCardNumber"])); }
                            catch { strCardNumber = ""; }

                            txtCardNumber.Text = strCardNumber;

                            try { subLoadCbo(cboReferredBy, Convert.ToString(drReg["strReferredBy"])); }
                            catch { }

                            txtAcctNum.Visible = false;
                            txtRoutingNum.Visible = false;
                            lblAcctNum.Visible = false;
                            lblRoutingNum.Visible = false;
                            txtCardNumber.Visible = false;
                            txtCVV2.Visible = false;
                            txtExpDate.Visible = false;
                            lblCardNumber.Visible = false;
                            lblCVV2.Visible = false;
                            lblExpDate.Visible = false;

                            if (txtPmtType.Text == "EFT")
                            {
                                txtAcctNum.Visible = true;
                                txtRoutingNum.Visible = true;

                                lblAcctNum.Visible = true;
                                lblRoutingNum.Visible = true;
                            }
                            else
                            {
                                txtCardNumber.Visible = true;
                                txtCVV2.Visible = true;
                                txtExpDate.Visible = true;

                                lblCardNumber.Visible = true;
                                lblCVV2.Visible = true;
                                lblExpDate.Visible = true;
                            }

                            txtNotes.Text = Convert.ToString(drReg["mmoRegNotes"]);

                            if (Convert.ToString(drReg["mmoSpecialNeeds"]) == "")
                                btnSpecialNeeds.Visible = false;
                            else
                                btnSpecialNeeds.Visible = true;

                            try { txtOrgCode.Text = Convert.ToString(drReg["strOrgCode"]); }
                            catch { txtOrgCode.Text = ""; }

                            //discounts
                            for (int intI = 1; intI <= 10; intI++)
                            {
                                try { ((CheckBox)fraDiscounts.Controls["chkDiscount" + intI.ToString()]).Checked = Convert.ToBoolean(drReg["blnDiscount" + intI.ToString()]); }
                                catch { }
                            }
                        }

                        drReg.Close();
                    }

                    strSQL = "SELECT tblWebIndRegBlockChoices.lngBlockID, tblWebIndRegBlockChoices.lngChoice, tblBlock.intCurrEnrollment, tblBlock.intCapacity, tblBlock.intWaitingList " +
                            "FROM tblWebIndRegBlockChoices " +
                                "INNER JOIN tblBlock ON tblWebIndRegBlockChoices.lngBlockID = tblBlock.lngBlockID " +
                            "WHERE tblWebIndRegBlockChoices.lngRegistrationWebID=" + lngRegWebID + " " +
                            "ORDER BY tblWebIndRegBlockChoices.lngChoice;";

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drRegChoices = cmdDB.ExecuteReader())
                    {
                        int intMaxChoice = 0;

                        while (drRegChoices.Read())
                        {
                            ComboBox cboChoice = (ComboBox)(pagRegInfo.Controls.Find("cboBlockChoice" + Convert.ToString(drRegChoices["lngChoice"]), true)[0]);

                            subLoadCbo(cboChoice, Convert.ToInt32(drRegChoices["lngBlockID"]));

                            long lngBlockID = 0;

                            try { lngBlockID = Convert.ToInt32(drRegChoices["lngBlockID"]); }
                            catch { lngBlockID = 0; }

                            int intChoice = 0;

                            try { intChoice = Convert.ToInt32(drRegChoices["lngChoice"]); }
                            catch { intChoice = 0; }

                            intMaxChoice = Convert.ToInt32(drRegChoices["lngChoice"]);
                        }

                        drRegChoices.Close();

                        if (intMaxChoice > 0)
                        {
                            for (int intI = intMaxChoice + 1; intI <= 11; intI++)
                            {
                                ((Label)(pagRegInfo.Controls.Find("lblFull" + intI, true)[0])).Visible = false;
                            }
                        }

                        lblFull11.Visible = false;
                    }

                    subLoadCbo(cboBlockChoice11, 0);

                    strSQL = "SELECT tblCampDefaults.curDiscAmt1, tblCampDefaults.curDiscAmt2, tblCampDefaults.curDiscAmt3, tblCampDefaults.curDiscAmt4, tblCampDefaults.curDiscAmt5, tblCampDefaults.curDiscAmt6, tblCampDefaults.curDiscAmt7, tblCampDefaults.curDiscAmt8, tblCampDefaults.curDiscAmt9, tblCampDefaults.curDiscAmt10, " +
                                "tblCampDefaults.strDiscDesc1, tblCampDefaults.strDiscDesc2, tblCampDefaults.strDiscDesc3, tblCampDefaults.strDiscDesc4, tblCampDefaults.strDiscDesc5, tblCampDefaults.strDiscDesc6, tblCampDefaults.strDiscDesc7, tblCampDefaults.strDiscDesc8, tblCampDefaults.strDiscDesc9, tblCampDefaults.strDiscDesc10 " +
                            "FROM tblCampDefaults;";

                    cmdDB.CommandText = strSQL;

                    using (OleDbDataReader drDisc = cmdDB.ExecuteReader())
                    {
                        if (drDisc.Read())
                        {
                            for (int intI = 1; intI <= 10; intI++)
                            {
                                CheckBox chkDisc = (CheckBox)(pagRegInfo.Controls.Find("chkDiscount" + intI.ToString(), true)[0]);

                                decimal decDisc = 0;

                                try { decDisc = Convert.ToDecimal(drDisc["curDiscAmt" + intI.ToString()]); }
                                catch { decDisc = 0; }

                                if (decDisc > 0)
                                {
                                    chkDisc.Text = Convert.ToString(drDisc["strDiscDesc" + intI.ToString()]) + " (" + decDisc.ToString("C") + ")";
                                    chkDisc.Visible = true;
                                }
                                else
                                    chkDisc.Visible = false;
                            }
                        }

                        drDisc.Close();
                    }

                    //create definitions
                    strSQL = "SELECT tblCustomFieldDefIR.lngCustomFieldDefIRID, " +
                                "tblCustomFieldDefIR.strFieldType, tblCustomFieldDefIR.strLocalCaption " +
                            "FROM tblCustomFieldDefIR " +
                            "WHERE tblCustomFieldDefIR.blnUseLocal=true " +
                            "ORDER BY tblCustomFieldDefIR.lngSortOrder, " +
                                "tblCustomFieldDefIR.strLocalCaption";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    using (OleDbDataReader drCustDef = cmdDB.ExecuteReader())
                    {
                        intCustIRTopL = 6;
                        intCustIRTopR = 6;

                        while (drCustDef.Read())
                        {
                            long lngCustomFieldDefIRID = 0;
                            string strFieldType = "";
                            string strLocalCaption = "";

                            try { lngCustomFieldDefIRID = Convert.ToInt32(drCustDef["lngCustomFieldDefIRID"]); }
                            catch { lngCustomFieldDefIRID = 0; }
                            try { strFieldType = Convert.ToString(drCustDef["strFieldType"]); }
                            catch { strFieldType = ""; }
                            try { strLocalCaption = Convert.ToString(drCustDef["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }

                            //add field def
                            Panel panCustIR = new Panel();
                            panCustIR.Name = "panCustomIR_" + lngCustomFieldDefIRID.ToString();
                            panCustIR.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustIR.Width = 355;

                            if (blnCustIRRightCol)
                                panCustIR.Top = intCustIRTopR;
                            else
                                panCustIR.Top = intCustIRTopL;

                            switch (strFieldType)
                            {
                                case "FIELD":
                                    Label lblCustFieldIR = new Label();
                                    lblCustFieldIR.Name = "lblCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    lblCustFieldIR.Text = strLocalCaption;
                                    lblCustFieldIR.Top = 6;
                                    lblCustFieldIR.Left = 6;
                                    lblCustFieldIR.Width = 175;

                                    TextBox txtCustFieldIR = new TextBox();
                                    txtCustFieldIR.Name = "txtCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    txtCustFieldIR.Left = 187;
                                    txtCustFieldIR.Top = 6;
                                    txtCustFieldIR.Width = 156;
                                    txtCustFieldIR.Height = 20;

                                    panCustIR.Height = 32;
                                    panCustIR.Controls.Add(lblCustFieldIR);
                                    panCustIR.Controls.Add(txtCustFieldIR);

                                    break;

                                case "MULTI-LINE TEXT":
                                    Label lblCustMLIR = new Label();
                                    lblCustMLIR.Name = "lblCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    lblCustMLIR.Text = strLocalCaption;
                                    lblCustMLIR.Top = 6;
                                    lblCustMLIR.Left = 6;
                                    lblCustMLIR.Width = 175;

                                    TextBox txtCustMLIR = new TextBox();
                                    txtCustMLIR.Name = "txtCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    txtCustMLIR.Left = 187;
                                    txtCustMLIR.Top = 6;
                                    txtCustMLIR.Width = 156;
                                    txtCustMLIR.Height = 40;
                                    txtCustMLIR.Multiline = true;

                                    panCustIR.Height = 52;
                                    panCustIR.Controls.Add(lblCustMLIR);
                                    panCustIR.Controls.Add(txtCustMLIR);

                                    break;

                                case "FLAG":
                                    CheckBox chkCustIR = new CheckBox();
                                    chkCustIR.Name = "chkCustIR_" + lngCustomFieldDefIRID.ToString();
                                    chkCustIR.Text = strLocalCaption;
                                    chkCustIR.Top = 6;
                                    chkCustIR.Left = 6;
                                    chkCustIR.Width = 226;

                                    panCustIR.Height = 32;
                                    panCustIR.Controls.Add(chkCustIR);

                                    break;

                                case "DROPDOWN":
                                    Label lblCustCboIR = new Label();
                                    lblCustCboIR.Name = "lblCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    lblCustCboIR.Text = strLocalCaption;
                                    lblCustCboIR.Top = 6;
                                    lblCustCboIR.Left = 6;
                                    lblCustCboIR.Width = 175;

                                    ComboBox cboCustFieldIR = new ComboBox();
                                    cboCustFieldIR.Name = "cboCustFieldIR_" + lngCustomFieldDefIRID.ToString();
                                    cboCustFieldIR.Left = 187;
                                    cboCustFieldIR.Top = 6;
                                    cboCustFieldIR.Width = 156;
                                    cboCustFieldIR.Height = 20;
                                    //add options
                                    subSetCboItemsIR(cboCustFieldIR, strLocalCaption);

                                    panCustIR.Height = 32;
                                    panCustIR.Controls.Add(lblCustCboIR);
                                    panCustIR.Controls.Add(cboCustFieldIR);

                                    break;
                            }

                            if (blnCustIRRightCol)
                            {
                                panCustIR.Left = 387;
                                intCustIRTopR += panCustIR.Height + 6;
                            }
                            else
                            {
                                panCustIR.Left = 6;
                                intCustIRTopL += panCustIR.Height + 6;
                            }

                            pagCustomIR.Controls.Add(panCustIR);

                            blnCustIRRightCol = !blnCustIRRightCol;
                        }

                        drCustDef.Close();
                    }

                    //custom ir field values
                    strSQL = "SELECT tblCustomFieldDefIR.lngCustomFieldDefIRID, " +
                            "tblCustomFieldValWebIR.strValue, tblCustomFieldDefIR.strFieldType " +
                        "FROM tblCustomFieldValWebIR " +
                            "INNER JOIN tblCustomFieldDefIR ON tblCustomFieldValWebIR.strLocalCaption = tblCustomFieldDefIR.strLocalCaption " +
                        "WHERE tblCustomFieldValWebIR.lngRecordWebID=@lngRecordWebID";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    cmdDB.Parameters.AddWithValue("@lngRecordWebID", lngRecordWebID);

                    using (OleDbDataReader drCustVal = cmdDB.ExecuteReader())
                    {
                        while (drCustVal.Read())
                        {
                            long lngCustomFieldDefIRID = 0;
                            string strValue = "";
                            string strFieldType = "";

                            try { lngCustomFieldDefIRID = Convert.ToInt32(drCustVal["lngCustomFieldDefIRID"]); }
                            catch { lngCustomFieldDefIRID = 0; }
                            try { strValue = Convert.ToString(drCustVal["strValue"]); }
                            catch { strValue = ""; }
                            try { strFieldType = Convert.ToString(drCustVal["strFieldType"]); }
                            catch { strFieldType = ""; }

                            switch (strFieldType)
                            {
                                case "FIELD":
                                case "MULTI-LINE TEXT":
                                    try { ((TextBox)pagCustomIR.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()].Controls["txtCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).Text = strValue; }
                                    catch { }
                                    break;

                                case "FLAG":
                                    try { ((CheckBox)pagCustomIR.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()].Controls["chkCustIR_" + lngCustomFieldDefIRID.ToString()]).Checked = Convert.ToBoolean(Convert.ToInt32(strValue)); }
                                    catch (Exception ex) 
                                    {
                                        try { ((CheckBox)pagCustomIR.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()].Controls["chkCustIR_" + lngCustomFieldDefIRID.ToString()]).Checked = Convert.ToBoolean(strValue); }
                                        catch (Exception ex2) { }
                                    }
                                    break;

                                case "DROPDOWN":
                                    ComboBox cboCustFieldIR;

                                    try
                                    {
                                        try
                                        {
                                            cboCustFieldIR = ((ComboBox)pagCustomIR.Controls["panCustomIR_" + lngCustomFieldDefIRID.ToString()].Controls["cboCustFieldIR_" + lngCustomFieldDefIRID.ToString()]);

                                            for (int intI = 0; intI < cboCustFieldIR.Items.Count; intI++)
                                                if (cboCustFieldIR.Items[intI].ToString() == strValue) cboCustFieldIR.SelectedItem = strValue;
                                        }
                                        catch { }

                                    }
                                    catch { }

                                    break;
                            }
                        }

                        drCustVal.Close();
                    }

                    //custom registration fields
                    strSQL = "SELECT tblCustomFieldDefReg.lngCustomFieldDefRegID, " +
                                "tblCustomFieldDefReg.strFieldType, tblCustomFieldDefReg.strLocalCaption " +
                            "FROM tblCustomFieldDefReg " +
                            "WHERE tblCustomFieldDefReg.blnUseLocal = true " +
                            "ORDER BY tblCustomFieldDefReg.lngSortOrder, " +
                                "tblCustomFieldDefReg.strLocalCaption";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    using (OleDbDataReader drCustDef = cmdDB.ExecuteReader())
                    {
                        intCustRegTopL = 6;
                        intCustRegTopR = 6;

                        bool blnCustRegRightCol = false;

                        while (drCustDef.Read())
                        {
                            long lngCustomFieldDefRegID = 0;
                            string strFieldType = "";
                            string strLocalCaption = "";

                            try { lngCustomFieldDefRegID = Convert.ToInt32(drCustDef["lngCustomFieldDefRegID"]); }
                            catch { lngCustomFieldDefRegID = 0; }
                            try { strFieldType = Convert.ToString(drCustDef["strFieldType"]); }
                            catch { strFieldType = ""; }
                            try { strLocalCaption = Convert.ToString(drCustDef["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }

                            //add field def
                            Panel panCustReg = new Panel();
                            panCustReg.Name = "panCustomReg_" + lngCustomFieldDefRegID.ToString();
                            panCustReg.BackColor = ColorTranslator.FromHtml("#E0E0E0");
                            panCustReg.Width = 355;
                            if (blnCustRegRightCol)
                                panCustReg.Top = intCustRegTopR;
                            else
                                panCustReg.Top = intCustRegTopL;

                            switch (strFieldType)
                            {
                                case "FIELD":
                                    Label lblCustFieldReg = new Label();
                                    lblCustFieldReg.Name = "lblCustFieldReg_" + lngCustomFieldDefRegID.ToString();
                                    lblCustFieldReg.Text = strLocalCaption;
                                    lblCustFieldReg.Top = 6;
                                    lblCustFieldReg.Left = 6;
                                    lblCustFieldReg.Width = 175;

                                    TextBox txtCustFieldReg = new TextBox();
                                    txtCustFieldReg.Name = "txtCustFieldReg_" + lngCustomFieldDefRegID.ToString();
                                    txtCustFieldReg.Left = 187;
                                    txtCustFieldReg.Top = 6;
                                    txtCustFieldReg.Width = 156;
                                    txtCustFieldReg.Height = 20;

                                    panCustReg.Height = 32;
                                    panCustReg.Controls.Add(lblCustFieldReg);
                                    panCustReg.Controls.Add(txtCustFieldReg);

                                    break;

                                case "MULTI-LINE TEXT":
                                    Label lblCustMLReg = new Label();
                                    lblCustMLReg.Name = "lblCustFieldReg_" + lngCustomFieldDefRegID.ToString();
                                    lblCustMLReg.Text = strLocalCaption;
                                    lblCustMLReg.Top = 6;
                                    lblCustMLReg.Left = 6;
                                    lblCustMLReg.Width = 175;

                                    TextBox txtCustMLReg = new TextBox();
                                    txtCustMLReg.Name = "txtCustFieldReg_" + lngCustomFieldDefRegID.ToString();
                                    txtCustMLReg.Left = 187;
                                    txtCustMLReg.Top = 6;
                                    txtCustMLReg.Width = 156;
                                    txtCustMLReg.Height = 40;
                                    txtCustMLReg.Multiline = true;

                                    panCustReg.Height = 52;
                                    panCustReg.Controls.Add(lblCustMLReg);
                                    panCustReg.Controls.Add(txtCustMLReg);

                                    break;

                                case "FLAG":
                                    CheckBox chkCustReg = new CheckBox();
                                    chkCustReg.Name = "chkCustReg_" + lngCustomFieldDefRegID.ToString();
                                    chkCustReg.Text = strLocalCaption;
                                    chkCustReg.Top = 6;
                                    chkCustReg.Left = 6;
                                    chkCustReg.Width = 226;

                                    panCustReg.Height = 32;
                                    panCustReg.Controls.Add(chkCustReg);

                                    break;

                                case "DROPDOWN":
                                    Label lblCustCboReg = new Label();
                                    lblCustCboReg.Name = "lblCustFieldReg_" + lngCustomFieldDefRegID.ToString();
                                    lblCustCboReg.Text = strLocalCaption;
                                    lblCustCboReg.Top = 6;
                                    lblCustCboReg.Left = 6;
                                    lblCustCboReg.Width = 175;

                                    ComboBox cboCustFieldReg = new ComboBox();
                                    cboCustFieldReg.Name = "cboCustFieldReg_" + lngCustomFieldDefRegID.ToString();
                                    cboCustFieldReg.Left = 187;
                                    cboCustFieldReg.Top = 6;
                                    cboCustFieldReg.Width = 156;
                                    cboCustFieldReg.Height = 20;
                                    //add options
                                    subSetCboItemsReg(cboCustFieldReg, strLocalCaption);

                                    panCustReg.Height = 32;
                                    panCustReg.Controls.Add(lblCustCboReg);
                                    panCustReg.Controls.Add(cboCustFieldReg);

                                    break;
                            }

                            if (blnCustRegRightCol)
                            {
                                panCustReg.Left = 387;
                                intCustRegTopR += panCustReg.Height + 6;
                            }
                            else
                            {
                                panCustReg.Left = 6;
                                intCustRegTopL += panCustReg.Height + 6;
                            }

                            pagCustomReg.Controls.Add(panCustReg);

                            blnCustRegRightCol = !blnCustRegRightCol;
                        }

                        drCustDef.Close();
                    }

                    //custom reg field values
                    strSQL = "SELECT tblCustomFieldDefReg.lngCustomFieldDefRegID, " +
                            "tblCustomFieldValWebReg.strValue, tblCustomFieldDefReg.strFieldType " +
                        "FROM tblCustomFieldValWebReg " +
                            "INNER JOIN tblCustomFieldDefReg ON tblCustomFieldValWebReg.strLocalCaption = tblCustomFieldDefReg.strLocalCaption " +
                        "WHERE tblCustomFieldValWebReg.lngRegistrationWebID=@lngRegistrationWebID";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    cmdDB.Parameters.AddWithValue("@lngRegistrationWebID", lngRegWebID);

                    using (OleDbDataReader drCustVal = cmdDB.ExecuteReader())
                    {
                        while (drCustVal.Read())
                        {
                            long lngCustomFieldDefRegID = 0;
                            string strValue = "";
                            string strFieldType = "";

                            try { lngCustomFieldDefRegID = Convert.ToInt32(drCustVal["lngCustomFieldDefRegID"]); }
                            catch { lngCustomFieldDefRegID = 0; }
                            try { strValue = Convert.ToString(drCustVal["strValue"]); }
                            catch { strValue = ""; }
                            try { strFieldType = Convert.ToString(drCustVal["strFieldType"]); }
                            catch { strFieldType = ""; }

                            switch (strFieldType)
                            {
                                case "FIELD":
                                case "MULTI-LINE TEXT":
                                    ((TextBox)pagCustomReg.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["txtCustFieldReg_" + lngCustomFieldDefRegID.ToString()]).Text = strValue;
                                    break;

                                case "FLAG":
                                    try { ((CheckBox)pagCustomReg.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["chkCustReg_" + lngCustomFieldDefRegID.ToString()]).Checked = Convert.ToBoolean(Convert.ToInt32(strValue)); }
                                    catch
                                    {
                                        try { ((CheckBox)pagCustomReg.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["chkCustReg_" + lngCustomFieldDefRegID.ToString()]).Checked = Convert.ToBoolean(strValue); }
                                        catch { }
                                    }
                                    break;

                                case "DROPDOWN":
                                    ComboBox cboCustFieldReg;

                                    try
                                    {
                                        cboCustFieldReg = ((ComboBox)pagCustomReg.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["cboCustFieldReg_" + lngCustomFieldDefRegID.ToString()]);

                                        for (int intI = 0; intI < cboCustFieldReg.Items.Count; intI++)
                                            if (cboCustFieldReg.Items[intI].ToString() == strValue) cboCustFieldReg.SelectedItem = strValue;

                                    }
                                    catch { }

                                    break;
                            }
                        }

                        drCustVal.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subSetCboItemsIR(ComboBox _cboCustFieldIR, string _strLocalCaption)
        {
            string strSQL = "";

            _cboCustFieldIR.Items.Add("");

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldDefIROptions.strValue " +
                        "FROM tblCustomFieldDefIROptions " +
                        "WHERE tblCustomFieldDefIROptions.strLocalCaption=@strLocalCaption " +
                        "ORDER BY tblCustomFieldDefIROptions.intSortOrder";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@strLocalCaption", _strLocalCaption);

                    using (OleDbDataReader drOptions = cmdDB.ExecuteReader())
                    {
                        while (drOptions.Read())
                        {
                            string strValue = "";

                            try { strValue = Convert.ToString(drOptions["strValue"]); }
                            catch { strValue = ""; }

                            if (strValue != "") _cboCustFieldIR.Items.Add(strValue);
                        }

                        drOptions.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subSetCboItemsReg(ComboBox _cboCustFieldReg, string _strLocalCaption)
        {
            string strSQL = "";

            _cboCustFieldReg.Items.Add("");

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldDefRegOptions.strValue " +
                        "FROM tblCustomFieldDefRegOptions " +
                        "WHERE tblCustomFieldDefRegOptions.strLocalCaption=@strLocalCaption " +
                        "ORDER BY tblCustomFieldDefRegOptions.intSortOrder";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@strLocalCaption", _strLocalCaption);

                    using (OleDbDataReader drOptions = cmdDB.ExecuteReader())
                    {
                        while (drOptions.Read())
                        {
                            string strValue = "";

                            try { strValue = Convert.ToString(drOptions["strValue"]); }
                            catch { strValue = ""; }

                            if (strValue != "") _cboCustFieldReg.Items.Add(strValue);
                        }

                        drOptions.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnSpecialNeeds_Click(object sender, EventArgs e)
        {
            clsNav.subShowSpecNeeds(lngRegWebID);
        }

        private void btnUpdateToProcessed_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            string strMsg = "";

            bool blnIssueRefund = false;

            DialogResult resRefund;

            strMsg = "Would you like to issue a refund for the payment?";

            resRefund = MessageBox.Show(strMsg, "Issue Refund?", MessageBoxButtons.YesNoCancel);

            if (resRefund == DialogResult.Yes)
                blnIssueRefund = true;
            else if (resRefund == DialogResult.Cancel)
                return;
            else
                blnIssueRefund = false;

            DateTime dteRegDate;

            try { dteRegDate = Convert.ToDateTime(txtRegDate.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with the registration date: " + ex.Message + "\nThe current date will be used.");
                dteRegDate = DateTime.Now;
            }

            if (blnIssueRefund)
            {
                decimal decRefundAmt = 0;

                using (frmCollectRefundAmt objCollectRefundAmt = new frmCollectRefundAmt(lngRegWebID))
                {
                    if (objCollectRefundAmt.ShowDialog() == DialogResult.OK)
                    {
                        decRefundAmt = objCollectRefundAmt.decAmt;

                        if (decRefundAmt > 0)
                        {
                            if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.XCharge)
                            {
                                if (txtPmtType.Text == "EFT")
                                    clsLiveCharge.subProcessRefundXCEFT(decRefundAmt, lngRegWebID, txtAcctNum.Text, txtRoutingNum.Text, txtProfileName.Text, txtCamperAddress.Text, txtCamperZip.Text, (int)this.Handle, clsLiveCharge.fcnGetXChargePath());
                                else
                                    clsLiveCharge.subProcessRefundXCCC(decRefundAmt, txtXCTransID.Text, txtXCAlias.Text);
                            }
                            else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.CashLinq)
                            {
                                if (txtPmtType.Text == "EFT")
                                    clsLiveCharge.subProcessRefundCashLinqEFT(decRefundAmt, lngRegWebID, txtAcctNum.Text, txtRoutingNum.Text, txtProfileName.Text, txtCamperAddress.Text, txtCamperZip.Text, (int)this.Handle);
                                else
                                    clsLiveCharge.subProcessRefundCashLinqCC(decRefundAmt, txtPNRef.Text);
                            }
                            else if (clsAppSettings.GetAppSettings().lngLiveCharge == clsGlobalEnum.conLIVECHARGE.EPS)
                            {
                                if (txtPmtType.Text == "EFT")
                                    clsLiveCharge.subProcessRefundEPSEFT(decRefundAmt);
                                else
                                    clsLiveCharge.subProcessRefundEPSCC(decRefundAmt, lngRegWebID, txtEPSTransID.Text);
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }

                strMsg = "Would you like to record the deposit and refund in CampTrak?";

                if (MessageBox.Show(strMsg, "Save in CampTrak?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    long lngStateID = 0;

                    try { lngStateID = ((clsCboItem)cboCamperState.SelectedItem).ID; }
                    catch { lngStateID = 0; }

                    clsIR irToSearch = new clsIR(0, lngStateID, txtCamperFName.Text, txtCamperLName.Text, "", txtCamperAddress.Text, txtCamperCity.Text, txtCamperZip.Text, txtCamperPhone.Text, "", txtCamperCellPhone.Text, txtCamperEMail.Text);

                    irToSearch.lngRecordWebID = lngRecordWebID;
                    irToSearch.lngRecordID = lngRecordID;

                    irToSearch.strConfEmail = txtProfileEmail.Text;

                    irToSearch.blnGender = radM.Checked;

                    //find record
                    using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find camper"))
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
                    decimal decSpending = 0;

                    try { decSpending = decimal.Parse(txtSpending.Text, System.Globalization.NumberStyles.Currency); }
                    catch { decSpending = 0; }

                    try { decPmtAmt = decimal.Parse(txtPmtAmt.Text, System.Globalization.NumberStyles.Currency); }
                    catch { decPmtAmt = 0; }

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        //add payment
                        strSQL = "INSERT INTO tblTransactions " +
                                "(blnMarkedForCC, " +
                                    "lngPaymentTypeID, lngTransTypeID, lngRecordID, lngBillStateID, lngUserID, " +
                                    "curPayment, " +
                                    "dteDateAdded, " +
                                    "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode) " +
                                "SELECT 1, " +
                                    "@lngPaymentTypeID, 8 AS lngTransTypeID, @lngRecordID, @lngBillStateID, @lngUserID, " +
                                    "@curPayment, " +
                                    "@dteDateAdded, " +
                                    "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode";

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            long lngPmtType = 0;

                            if (txtCardNumber.Text == "" && txtAcctNum.Text != "")
                                lngPmtType = 11;
                            else
                                lngPmtType = 2;

                            long lngBillStateID = 0;

                            try { lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
                            catch { lngBillStateID = 0; }

                            cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                            cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decPmtAmt));

                            cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                            cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Tuition"));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));

                            cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));

                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }

                            //add spending money if amt > 0
                            if (decSpending > 0)
                            {
                                strSQL = "INSERT INTO tblTransactions " +
                                      "(blnMarkedForCC, " +
                                          "lngRecordID, lngTransTypeID, lngPaymentTypeID, lngBillStateID, lngUserID, " +
                                          "curPayment, " +
                                          "dteDateAdded, " +
                                          "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode)" +
                                    "SELECT 1, " +
                                        "@lngRecordID, @lngTransTypeID, @lngPaymentTypeID, @lngBillStateID, @lngUserID, " +
                                        "@curPayment, " +
                                        "@dteDateAdded, " +
                                        "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngTransTypeID", 25));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                                cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decSpending));

                                cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Spending Money"));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));

                                cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));

                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransactionID", txtEPSTransID.Text));

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }
                            }

                            //add donation if required
                            decimal decDonation = 0;

                            try { decDonation = decimal.Parse(txtDonation.Text, System.Globalization.NumberStyles.Currency); }
                            catch { decDonation = 0; }

                            if (decDonation > 0)
                            {
                                long lngGiftCategory = 0;
                                long lngCampaign = 0;
                                long lngTrigger = 0;

                                strSQL = "SELECT lngOLGiftCategoryID, lngOLGiftCampaign, lngOLTriggerID " +
                                        "FROM tblCampDefaults";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                                {
                                    if (drDef.Read())
                                    {
                                        try { lngGiftCategory = Convert.ToInt32(drDef["lngOLGiftCategoryID"]); }
                                        catch { lngGiftCategory = 0; }

                                        try { lngCampaign = Convert.ToInt32(drDef["lngOLGiftCampaign"]); }
                                        catch { lngCampaign = 0; }

                                        try { lngTrigger = Convert.ToInt32(drDef["lngOLTriggerID"]); }
                                        catch { lngTrigger = 0; }
                                    }

                                    drDef.Close();
                                }

                                strSQL = "INSERT INTO tblGift " +
                                        "(lngGiftCategoryID, lngRecordID, lngCampaignID, lngGiftTypeID, lngTriggerID, lngPaymentTypeID, lngBillStateID, " +
                                            "dteGiftDate, dteDateEntered, " +
                                            "curAmount, " +
                                            "strAcctNum, strBankName, strBillAddress, strBillCity, strBillName, strBillPhone, strBillZip, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum, " +
                                            "mmoNotes) " +
                                        "SELECT @lngGiftCategoryID, @lngRecordID, @lngCampaignID, @lngGiftTypeID, @lngTriggerID, @lngPaymentTypeID, @lngBillStateID, " +
                                            "@dteGiftDate, @dteDateEntered, " +
                                            "@curAmount, " +
                                            "@strAcctNum, @strBankName, @strBillAddress, @strBillCity, @strBillName, @strBillPhone, @strBillZip, @strCCExpDate, @strCCNumber, @strCCValCode, @strRoutingNum, " +
                                            "@mmoNotes";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
                                catch { lngBillStateID = 0; }

                                cmdDB.Parameters.Add(new OleDbParameter("@lngGiftCategoryID", lngGiftCategory));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngCampaignID", lngCampaign));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngGiftTypeID", 1));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngTriggerID", lngTrigger));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));

                                cmdDB.Parameters.Add(new OleDbParameter("@dteGiftDate", dteRegDate));
                                cmdDB.Parameters.Add(new OleDbParameter("@dteDateEntered", OleDbType.Date));

                                try { cmdDB.Parameters["@dteDateEntered"].Value = DateTime.Now; }
                                catch (Exception ex) { MessageBox.Show("There was an error setting the 'DateEntered' field: " + ex.Message); }

                                cmdDB.Parameters.Add(new OleDbParameter("@curAmount", decDonation));
                                cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumberUnmasked.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@mmoNotes", "Online Donation"));

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }

                                //mark record as donor
                                strSQL = "UPDATE tblRecords " +
                                        "SET blnDonor = True " +
                                        "WHERE lngRecordID=" + lngRecordID;

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }
                            }//</donation

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
                                        "@dteDateAdded, " +
                                        "@strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strCCNumber, @strCCExpDate, \"Refund for online registration\")";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngCTUserID));
                            cmdDB.Parameters.Add(new OleDbParameter("@curCharge", decRefundAmt));

                            cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                            cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }

                        }//</command>

                        conDB.Close();
                    }//</connection>
                }//</save in ct>
            }//</issue refund>

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "UPDATE tblWebIndRegistrations " +
                        "SET blnProcessed=True " +
                        "WHERE lngRegistrationWebID=" + lngRegWebID;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    cmdDB.ExecuteNonQuery();

                conDB.Close();
            }

            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            decimal decPmtAmt = 0;
            decimal decSpending = 0;

            long lngRegistrationID = 0;
            long lngPmtType = 0;

            int intTotChoices = 0;
            int[] intWaitList = new int[11];

            //'first check availability
            //'try to match camper to an existing record
            //'if a possible match is found, display a form to pick exact record
            //'if no match is found, try to find a mor that the record might fit into.  add record and assign to mor
            //'if no mor is found, add record
            //'return id, register camper
            DateTime dteRegDate;

            try { dteRegDate = Convert.ToDateTime(txtRegDate.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with the registration date: " + ex.Message + "\nThe current date will be used.");
                dteRegDate = DateTime.Now;
            }

            DateTime dteCamperBDate;

            try { dteCamperBDate = Convert.ToDateTime(txtCamperBDate.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with the birth date: " + ex.Message + "\nThe current date will be used.");
                dteCamperBDate = DateTime.Now;
            }

            for (int intI = 1; intI <= 11; intI++)
            {
                if (((CheckBox)pagRegInfo.Controls["chkReg" + intI.ToString()]).Checked)
                    intTotChoices++;
                else if (((CheckBox)pagRegInfo.Controls["chkWait" + intI.ToString()]).Checked)
                    intTotChoices++;
            }

            if (intTotChoices == 0)
            {
                MessageBox.Show("Please select a block to register or wait list this camper for.");
                pagRegInfo.Focus();
                return;
            }

            try { decSpending = decimal.Parse(txtSpending.Text, System.Globalization.NumberStyles.Currency); }
            catch { decSpending = 0; }

            try { decPmtAmt = decimal.Parse(txtPmtAmt.Text, System.Globalization.NumberStyles.Currency); }
            catch { decPmtAmt = 0; }

            //availability check--un-mark any full blocks, mark wait list
            for (int intI = 1; intI <= 11; intI++)
            {
                if (((CheckBox)pagRegInfo.Controls["chkReg" + intI]).Checked)
                {
                    if (((Label)pagRegInfo.Controls["lblFull" + intI]).Visible)
                    {
                        if (MessageBox.Show("This block (" + ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intI]).SelectedItem).Item + ") is full.\n\nWould you like to continue with this registration\nand thus overbook the block?\n\n(Selecting 'No' will place this camper on the waiting list.)", "Block Full", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            ((CheckBox)pagRegInfo.Controls["chkReg" + intI.ToString()]).Checked = false;
                            ((CheckBox)pagRegInfo.Controls["chkWait" + intI.ToString()]).Checked = true;
                        }
                    }
                }
            }

            if (intTotChoices == 0)
            {
                decSpending = 0;
                decPmtAmt = 0;
            }
            else
            {
                decSpending = decSpending / intTotChoices;
                decPmtAmt = decPmtAmt / intTotChoices;
            }

            // 'populate waiting list array
            for (int intI = 0; intI <= 10; intI++)
            {
                if (((CheckBox)pagRegInfo.Controls["chkWait" + (intI + 1)]).Checked) { intWaitList[intI] = Convert.ToInt32(((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + (intI + 1)]).SelectedItem).ID); }
                else
                    intWaitList[intI] = 0;
            }

            long lngParentIRID = 0;
            bool blnParentRequired = false;

            if (fraApplyDonationTo.Visible && radDonationToParent.Checked) blnParentRequired = true;

            try { lngParentIRID = fcnProcessProfileRecord(blnParentRequired); }
            catch (Exception ex) { lngParentIRID = 0; }

            if (blnParentRequired && lngParentIRID <= 0)
            {
                MessageBox.Show("You've chosen to assign the donation to the parent, so a parent record must be selected or created to continue.");
                txtDonation.Focus();
                return;
            }

            //'get record to add reg/wait list for
            //'if there are no eligible matches, go directly to add new.
            //'show list of possible matches
            long lngStateID = 0;

            try { lngStateID = ((clsCboItem)cboCamperState.SelectedItem).ID; }
            catch { lngStateID = 0; }

            clsIR irToSearch = new clsIR(0, lngStateID, txtCamperFName.Text, txtCamperLName.Text, "", txtCamperAddress.Text, txtCamperCity.Text, txtCamperZip.Text, txtCamperPhone.Text, "", txtCamperCellPhone.Text, txtCamperEMail.Text);

            //fill in ir details to pass to search screen
            irToSearch.blnGender = radM.Checked;

            irToSearch.blnCamper = true;
            irToSearch.blnParent = false;

            try { irToSearch.intGrade = Convert.ToInt32(txtCamperGrade.Text); }
            catch { irToSearch.intGrade = 0; }

            irToSearch.lngRecordWebID = lngRecordWebID;
            irToSearch.lngRecordID = lngRecordID;

            try { irToSearch.lngStateID = ((clsCboItem)cboCamperState.SelectedItem).ID; }
            catch { irToSearch.lngStateID = 0; }

            try { irToSearch.lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
            catch { irToSearch.lngBillStateID = 0; }

            irToSearch.dteBDate = dteCamperBDate;

            irToSearch.strMI = txtCamperMI.Text;
            irToSearch.strEmail = txtCamperEMail.Text;
            irToSearch.strConfEmail = txtProfileEmail.Text;
            irToSearch.strHomePhone = txtCamperPhone.Text;
            irToSearch.strZip = txtCamperZip.Text;
            irToSearch.strCity = txtCamperCity.Text;
            irToSearch.strAddress = txtCamperAddress.Text;
            irToSearch.strLName = txtCamperLName.Text;
            irToSearch.strFName = txtCamperFName.Text;
            irToSearch.strPmtType = txtPmtType.Text;
            irToSearch.strSpecialNeeds = "";
            irToSearch.strNotes = txtNotes.Text;
            irToSearch.strBankName = txtBankName.Text;
            irToSearch.strFather = txtFather.Text;
            irToSearch.strMother = txtMother.Text;
            irToSearch.strBillName = txtBillName.Text;
            irToSearch.strBillAddress = txtBillAddress.Text;
            irToSearch.strBillCity = txtBillCity.Text;
            irToSearch.strBillZip = txtBillZip.Text;
            irToSearch.strBillPhone = txtBillPhone.Text;

            //add custom vals to ir to search for
            for (int intI = 0; intI <pagCustomIR.Controls.Count; intI++)
            {
                if (pagCustomIR.Controls[intI].HasChildren)
                {
                    if (pagCustomIR.Controls[intI].Name.StartsWith("panCustomIR_"))
                    {
                        long lngCustomFieldDefIRID = 0;
                        string strID = pagCustomIR.Controls[intI].Name;

                        lngCustomFieldDefIRID = Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                        Panel panCustom = (Panel)pagCustomIR.Controls[intI];

                        string strLocalCaption = "";
                        string strValue = "";

                        //assume textbox or cbo for caption
                        try { strLocalCaption = ((Label)panCustom.Controls["lblCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                        catch { strLocalCaption = ""; }

                        //either a flag or err
                        if (strLocalCaption == "")
                        {
                            try { strLocalCaption = ((CheckBox)panCustom.Controls["chkCustIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strLocalCaption = ""; }
                        }

                        if (strLocalCaption != "")
                        {
                            try { strValue = ((TextBox)panCustom.Controls["txtCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).Text; }
                            catch { strValue = ""; }

                            if (strValue == "")
                            {
                                try { strValue = ((CheckBox)panCustom.Controls["chkCustIR_" + lngCustomFieldDefIRID.ToString()]).Checked.ToString(); }
                                catch { strValue = ""; }
                            }

                            if (strValue == "")
                            {
                                try { strValue = ((ComboBox)panCustom.Controls["cboCustFieldIR_" + lngCustomFieldDefIRID.ToString()]).SelectedItem.ToString(); }
                                catch { strValue = ""; }
                            }
                        }

                        string[] strCustom = new string[2];

                        strCustom[0] = strLocalCaption;
                        strCustom[1] = strValue;

                        irToSearch.strCustom.Add(strCustom);
                    }
                }
            }

            using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find camper record"))
            {
                if (objFindIR.ShowDialog() == DialogResult.OK)
                {
                    if (objFindIR.irToSearch.lngRecordID == 0)
                        return;
                    else
                    {
                        lngRecordID = objFindIR.irToSearch.lngRecordID;
                        clsIRCRUD.subSetToSync(lngRecordID);

                        if (!objFindIR.blnAddNew)
                        {
                            using (IRUtils.frmReconcileIR objReconcileIR = new global::CTWebMgmt.IRUtils.frmReconcileIR("tblWebRecords", lngRecordID, lngRecordWebID))
                            {
                                if (objReconcileIR.ShowDialog() == DialogResult.Cancel)
                                    return;
                                else
                                    lngRecordID = objReconcileIR.lngDBID;
                            }
                        }
                    }
                }
                else
                    return;
            }

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                //'put web camper id in tblRecords
                strSQL = "UPDATE tblRecords " +
                        "SET blnCamper=True, " +
                            "lngWebCamperID=" + lngRecordWebID.ToString() + ", lngRecordWebID=" + lngRecordWebID.ToString() + " " +
                        "WHERE lngRecordID=" + lngRecordID.ToString();

                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.ExecuteNonQuery();
                    
                    //update local id on server
                    using (wsXferEventInfoV2.xfereventinfov2 svc = new global::CTWebMgmt.wsXferEventInfoV2.xfereventinfov2())
                    {
                        string strWebRes = "";

                        strWebRes = svc.fcnUpdateLocalID(lngRecordWebID, lngRecordID, clsAppSettings.GetAppSettings().lngCTUserID, clsAppSettings.GetAppSettings().strWebDBConn);

                        if (strWebRes != "") MessageBox.Show("There was an error updating the camper's web id: " + strWebRes);
                    }

                    subManageSpecNeeds(cmdDB, lngRegWebID, lngRecordID);

                    //at this point, user has either selected a matching record, created a new record, or cancelled
                    //attempt to add mor for record (if one doesn't already exist)
                    long lngMORID = 0;

                    strSQL = "SELECT lngPrimaryMORID " +
                            "FROM tblRecords " +
                            "WHERE lngRecordID=" + lngRecordID;

                    cmdDB.CommandText = strSQL;

                    cmdDB.Parameters.Clear();

                    try { lngMORID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { lngMORID = 0; }

                    if (lngMORID == 0)
                    {
                        if (MessageBox.Show("Add MOR for this record?", "Add MOR?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            using (frmFindMOR objFindMOR = new frmFindMOR("%" + txtCamperLName.Text + "%", 1))
                            {
                                if (objFindMOR.ShowDialog() == DialogResult.OK)
                                {
                                    if (objFindMOR.lngMORID > 0)
                                        lngMORID = objFindMOR.lngMORID;
                                    else
                                    {//give option to create new MOR--forthcoming
                                        using (MORUtils.frmAddMOR objAddMOR = new global::CTWebMgmt.MORUtils.frmAddMOR(lngRecordWebID, lngRecordID, lngParentIRID))
                                        {
                                            if (objAddMOR.ShowDialog() == DialogResult.OK)
                                                lngMORID = objAddMOR.lngMORID;
                                            else
                                                return;
                                        }
                                    }

                                    if (lngMORID > 0)
                                    {
                                        //update camper's primary mor
                                        strSQL = "UPDATE tblRecords " +
                                                "SET blnUseMORAddress=-1, " +
                                                    "lngPrimaryMORID=" + lngMORID.ToString() + " " +
                                                "WHERE lngRecordID=" + lngRecordID.ToString();

                                        cmdDB.Parameters.Clear();
                                        cmdDB.CommandText = strSQL;

                                        try { cmdDB.ExecuteNonQuery(); }
                                        catch { }
                                    }
                                }
                                else
                                    return;
                            }

                            if (lngMORID > 0)
                            {
                                long lngMORIRLinkID = 0;

                                strSQL = "SELECT lngMORIRLinkID " +
                                        "FROM tblnkMORIR " +
                                        "WHERE lngMORID=" + lngMORID.ToString() + " AND " +
                                            "lngRecordID=" + lngRecordID.ToString();

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                try { lngMORIRLinkID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                catch { lngMORIRLinkID = 0; }

                                if (lngMORIRLinkID == 0)
                                {
                                    strSQL = "INSERT INTO tblnkMORIR " +
                                            "( lngMORID, lngRecordID, lngLastModifiedUser, " +
                                                "dteLastModified ) " +
                                            "SELECT " + lngMORID + ", " + lngRecordID + ", " + CTWebMgmt.lngCTUserID + ", " +
                                                "Now()";

                                    cmdDB.CommandText = strSQL;

                                    cmdDB.Parameters.Clear();

                                    cmdDB.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    //'make sure they are not already registered for one of the blocks

                    for (int intI = 1; intI <= 11; intI++)
                    {
                        if (((CheckBox)pagRegInfo.Controls["chkReg" + intI.ToString()]).Checked)
                        {
                            strSQL = "SELECT lngRegistrationID " +
                                    "FROM tblRegistrations " +
                                    "WHERE lngRecordID=" + lngRecordID + " AND " +
                                        "lngBlockID=" + ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intI]).SelectedItem).ID;

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            lngRegistrationID = 0;

                            try { lngRegistrationID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                            catch { lngRegistrationID = 0; }

                            if (lngRegistrationID > 0)
                            {
                                MessageBox.Show("This record is already registered for this block. (" + ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intI]).SelectedItem).Item + ")");
                                return;
                            }
                        }
                    }

                    //check to see if approval is needed for the camper
                    try
                    {
                        strSQL = "SELECT tblRecords.blnApprovalNeeded " +
                                "FROM tblRecords " +
                                "WHERE tblRecords.lngRecordID=@lngRecordID";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.Parameters.AddWithValue("@lngRecordID", lngRecordID);

                        bool blnApprovalNeeded = false;

                        try { blnApprovalNeeded = Convert.ToBoolean(cmdDB.ExecuteScalar()); }
                        catch { blnApprovalNeeded = false; }

                        if (blnApprovalNeeded)
                        {
                            if (MessageBox.Show("This record needs administrative approval to register for an individual event.\nAre you sure you wish to continue?", "Approval Needed", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                                return;
                        }
                    }
                    catch (Exception ex) { }


                    //'add waiting list(s)
                    for (int intI = 0; intI <= 10; intI++)
                    {
                        if (intWaitList[intI] != 0)
                        {
                            //'make sure they aren't already on the list
                            strSQL = "SELECT lngWaitID " +
                                   "FROM tblWaitingList " +
                                   "WHERE lngRecordID=" + lngRecordID + " AND " +
                                       "lngBlockID=" + intWaitList[intI];

                            cmdDB.CommandText = strSQL;

                            cmdDB.Parameters.Clear();

                            using (OleDbDataReader drWait = cmdDB.ExecuteReader())
                            {
                                if (!drWait.Read())
                                {
                                    drWait.Close();

                                    int intGender = 0;

                                    //'make sure they are the right gender
                                    strSQL = "SELECT lngGenderID " +
                                           "FROM tblBlock " +
                                           "WHERE lngBlockID=" + intWaitList[intI];

                                    cmdDB.CommandText = strSQL;
                                    cmdDB.Parameters.Clear();

                                    try { intGender = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                    catch { intGender = 0; }

                                    if (!(intGender == 1 && radF.Checked) && !(intGender == 2 && radM.Checked))
                                    {
                                        //'add waiting list entry
                                        strSQL = "INSERT INTO tblWaitingList " +
                                               "( lngBlockID, lngRecordID, " +
                                                   "dteDateAdded ) " +
                                               "SELECT " + intWaitList[intI] + ", " + lngRecordID + ", " +
                                                   "@dteDateAdded";

                                        cmdDB.CommandText = strSQL;

                                        cmdDB.Parameters.Clear();
                                        cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                        try { cmdDB.ExecuteNonQuery(); }
                                        catch { }

                                        //'update block stats
                                        strSQL = "SELECT Count(tblWaitingList.lngWaitID) AS CountOflngWaitID " +
                                                "FROM tblWaitingList " +
                                                "WHERE tblWaitingList.lngBlockID=" + intWaitList[intI];

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        int intWaitListCount = 0;

                                        try { intWaitListCount = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                        catch { intWaitListCount = 0; }

                                        strSQL = "UPDATE tblBlock " +
                                                "SET tblBlock.intWaitingList = " + intWaitListCount + " " +
                                                "WHERE tblBlock.lngBlockID=" + intWaitList[intI];

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        try { cmdDB.ExecuteNonQuery(); }
                                        catch { }
                                    }
                                    else
                                    {
                                        MessageBox.Show("The camper appears to be the wrong gender for the block.\n\nThe waiting list entry was not added.");
                                    }
                                }
                                else
                                    drWait.Close();
                            }

                            //add deposit and spending money for wait list selections
                            strSQL = "INSERT INTO tblTransactions " +
                                    "(blnMarkedForCC, " +
                                        "lngPaymentTypeID, lngRecordID, lngBillStateID, lngUserID, lngTransTypeID, " +
                                        "curPayment, " +
                                        "dteDateAdded, " +
                                        "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strXCAlias, strXCTransID, strXCAuthCode, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode ) " +
                                    "SELECT @blnMarkedForCC, " +
                                        "@lngPaymentTypeID, @lngRecordID, @lngBillStateID, @lngUserID, @lngTransTypeID, " +
                                        "@curPayment, " +
                                        "@dteDateAdded, " +
                                        "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strXCAlias, @strXCTransID, @strXCAuthCode, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode ";

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            if (txtCardNumber.Text == "" && txtAcctNum.Text != "")
                                lngPmtType = 11;
                            else
                                lngPmtType = 2;

                            long lngBillStateID = 0;

                            try { lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
                            catch { lngBillStateID = 0; }

                            cmdDB.Parameters.Add(new OleDbParameter("@blnMarkedForCC", true));

                            cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                            cmdDB.Parameters.Add(new OleDbParameter("@lngTransTypeID", 8));

                            cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decPmtAmt));

                            cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                            cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Tuition, Wait List"));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strXCAlias", txtXCAlias.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strXCTransID", txtXCTransID.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strXCAuthCode", txtXCAuthCode.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));

                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                            cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                            try { cmdDB.ExecuteNonQuery(); }
                            catch (Exception ex) { MessageBox.Show("There was an error recording the deposit: " + ex.Message); }

                            //add spending money credit for waiting list entry
                            if (decSpending > 0)
                            {
                                strSQL = "INSERT INTO tblTransactions " +
                                      "(blnMarkedForCC, " +
                                            "lngRecordID, lngTransTypeID, lngPaymentTypeID, lngBillStateID, lngUserID, " +
                                          "curPayment, " +
                                          "dteDateAdded, " +
                                          "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strXCAlias, strXCTransID, strXCAuthCode, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode )" +
                                  "SELECT @blnMarkedForCC, " +
                                            "@lngRecordID, @lngTransTypeID, @lngPaymentTypeID, @lngBillStateID, @lngUserID, " +
                                          "@curPayment, " +
                                          "@dteDateAdded, " +
                                          "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strXCAlias, @strXCTransID, @strXCAuthCode, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode ";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.Add(new OleDbParameter("@blnMarkedForCC", true));

                                cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngTransTypeID", 25));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                                cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                                cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decSpending));

                                cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Spending Money, Wait List"));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strXCAlias", txtXCAlias.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strXCTransID", txtXCTransID.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strXCAuthCode", txtXCAuthCode.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                                cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                                try { cmdDB.ExecuteNonQuery(); }
                                catch { }
                            }
                        }
                    }

                    //choice level variables that need to be checked for each reg box checked
                    string strBlockCode = "";

                    long lngBlockID = 0;
                    decimal decTuitionCharge = 0;

                    //get pref for how to apply reg flag charges for multiple block choices
                    bool blnMultRegFlagCharge = true;

                    if (intTotChoices > 1)
                    {
                        using (wsXferEventInfo.XferEventInfo svcCT = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                            blnMultRegFlagCharge = svcCT.fcnGetMultRegFlagCharge(clsAppSettings.GetAppSettings().lngCTUserID, clsWebTalk.strWebConn);
                    }
                    else
                        blnMultRegFlagCharge = false;

                    for (int intBlockChoice = 1; intBlockChoice <= 11; intBlockChoice++)
                    {
                        //if block is selected and reg box is checked
                        if (((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intBlockChoice.ToString()]).SelectedIndex >= 0 && ((CheckBox)pagRegInfo.Controls["chkReg" + intBlockChoice.ToString()]).Checked)
                        {
                            try { lngBlockID = ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intBlockChoice.ToString()]).SelectedItem).ID; }
                            catch { lngBlockID = 0; }

                            try { strBlockCode = ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intBlockChoice.ToString()]).SelectedItem).Item; }
                            catch { strBlockCode = ""; }

                            //validate gender, grade, age group, availability of block choice
                            if (!fcnValBlockChoice(lngRecordID, lngBlockID))
                                return;
                            else
                            {
                                //camper selected is correct gender and grade, verify registration.
                                if (MessageBox.Show("Register " + txtCamperFName.Text + " " + txtCamperLName.Text + " for program block " + strBlockCode + "?", "Register?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                                    return;
                                else
                                {
                                    //add registration record

                                    strSQL = "INSERT INTO tblRegistrations " +
                                            "( lngBlockID, lngRecordID, lngRegSourceID, lngUserID, lngConfMethodID, " +
                                                "dteRegistrationDate, " +
                                                "strBuddy1, strBuddy2, strReleaseTo, strReferredBy, strOrgCode, " +
                                                "mmoRegNotes ) " +
                                            "SELECT @lngBlockID, @lngRecordID, @lngRegSourceID, @lngUserID, 2, " +
                                                "@dteRegistrationDate, " +
                                                "@strBuddy1, @strBuddy2, @strReleaseTo, @strReferredBy, @strOrgCode, " +
                                                "@mmoRegNotes";

                                    cmdDB.Parameters.Clear();
                                    cmdDB.CommandText = strSQL;

                                    cmdDB.Parameters.Add(new OleDbParameter("@lngBlockID", lngBlockID));
                                    cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                    cmdDB.Parameters.Add(new OleDbParameter("@lngRegSourceID", 3));
                                    cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                                    cmdDB.Parameters.Add(new OleDbParameter("@dteRegistrationDate", dteRegDate));
                                    cmdDB.Parameters.Add(new OleDbParameter("@strBuddy1", txtBuddy1.Text));
                                    cmdDB.Parameters.Add(new OleDbParameter("@strBuddy2", txtBuddy2.Text));
                                    cmdDB.Parameters.Add(new OleDbParameter("@strReleaseTo", txtReleaseTo.Text));

                                    string strReferredBy = "";

                                    try { strReferredBy = ((clsCboItem)cboReferredBy.SelectedItem).Item; }
                                    catch { strReferredBy = ""; }

                                    cmdDB.Parameters.Add(new OleDbParameter("@strReferredBy", strReferredBy));

                                    cmdDB.Parameters.AddWithValue("@strOrgCode", txtOrgCode.Text);

                                    string mmoRegNotes = "";

                                    try { mmoRegNotes = txtNotes.Text; }
                                    catch { mmoRegNotes = ""; }

                                    cmdDB.Parameters.AddWithValue("@mmoRegNotes", mmoRegNotes);

                                    try { cmdDB.ExecuteNonQuery(); }
                                    catch { }

                                    strSQL = "SELECT @@IDENTITY;";

                                    cmdDB.Parameters.Clear();
                                    cmdDB.CommandText = strSQL;

                                    try { lngRegistrationID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                    catch { lngRegistrationID = 0; }

                                    //add reg custom data vals
                                    strSQL = "DELETE tblCustomFieldValReg.* " +
                                            "FROM tblCustomFieldValReg " +
                                            "WHERE lngRegistrationID=@lngRegistrationID";

                                    cmdDB.Parameters.Clear();
                                    cmdDB.CommandText = strSQL;

                                    cmdDB.Parameters.AddWithValue("@lngRegistrationID", lngRegistrationID);

                                    try { cmdDB.ExecuteNonQuery(); }
                                    catch { }

                                    //loop through reg data and add to registration
                                    ////////////////////////////////////////////////////////////////////
                                    for (int intI = 0; intI < pagCustomReg.Controls.Count; intI++)
                                    {
                                        if (pagCustomReg.Controls[intI].HasChildren)
                                        {
                                            if (pagCustomReg.Controls[intI].Name.StartsWith("panCustomReg_"))
                                            {
                                                long lngCustomFieldDefRegID = 0;
                                                string strID = pagCustomReg.Controls[intI].Name;

                                                lngCustomFieldDefRegID= Convert.ToInt32(strID.Substring(strID.IndexOf("_") + 1, strID.Length - (strID.IndexOf("_") + 1)));

                                                Panel panCustom = (Panel)pagCustomReg.Controls[intI];

                                                string strLocalCaption = "";
                                                string strValue = "";

                                                //assume textbox or cbo for caption
                                                try { strLocalCaption = ((Label)panCustom.Controls["lblCustFieldReg_" +lngCustomFieldDefRegID.ToString()]).Text; }
                                                catch { strLocalCaption = ""; }

                                                //either a flag or err
                                                if (strLocalCaption == "")
                                                {
                                                    try { strLocalCaption = ((CheckBox)panCustom.Controls["chkCustReg_" +lngCustomFieldDefRegID.ToString()]).Text; }
                                                    catch { strLocalCaption = ""; }
                                                }

                                                if (strLocalCaption != "")
                                                {
                                                    try { strValue = ((TextBox)panCustom.Controls["txtCustFieldReg_" +lngCustomFieldDefRegID.ToString()]).Text; }
                                                    catch { strValue = ""; }

                                                    if (strValue == "")
                                                    {
                                                        try { strValue = ((CheckBox)panCustom.Controls["chkCustReg_" +lngCustomFieldDefRegID.ToString()]).Checked.ToString(); }
                                                        catch { strValue = ""; }
                                                    }

                                                    if (strValue == "")
                                                    {
                                                        try { strValue = ((ComboBox)panCustom.Controls["cboCustFieldReg_" +lngCustomFieldDefRegID.ToString()]).SelectedItem.ToString(); }
                                                        catch { strValue = ""; }
                                                    }
                                                }

                                                //strLocalCaption;
                                                //strValue;
                                                strSQL = "INSERT INTO tblCustomFieldValReg " +
                                                        "( lngRegistrationID, " +
                                                            "strLocalCaption, strValue ) " +
                                                        "VALUES " +
                                                        "( @lngRegistrationID, " +
                                                            "@strLocalCaption, @strValue )";

                                                cmdDB.CommandText = strSQL;
                                                cmdDB.Parameters.Clear();

                                                cmdDB.Parameters.AddWithValue("@lngRegistrationID", lngRegistrationID);
                                                cmdDB.Parameters.AddWithValue("@strLocalCaption", strLocalCaption);
                                                cmdDB.Parameters.AddWithValue("@strValue", strValue);

                                                try { cmdDB.ExecuteNonQuery(); }
                                                catch { }
                                            }
                                        }
                                    }

                                    //get reg id to add transaction
                                    long lngTuitionChargeID = fcnAddTuitionCharge(cmdDB, lngRecordID, lngBlockID, lngRegistrationID, ref decTuitionCharge);

                                    //update reg w/ transaction id
                                    strSQL = "UPDATE tblRegistrations " +
                                            "SET lngTransactionID=" + lngTuitionChargeID.ToString() + " " +
                                            "WHERE lngRegistrationID=" + lngRegistrationID.ToString();

                                    cmdDB.CommandText = strSQL;
                                    cmdDB.Parameters.Clear();

                                    cmdDB.ExecuteNonQuery();

                                    //adjust block stats
                                    subOneBlockRegCount(cmdDB, lngBlockID);

                                    long lngBillStateID = 0;

                                    try { lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
                                    catch { lngBillStateID = 0; }

                                    if (decPmtAmt > 0)
                                    {
                                        //add payment
                                        strSQL = "INSERT INTO tblTransactions " +
                                                "(blnMarkedForCC, " +
                                                    "lngPaymentTypeID, lngRecordID, lngRegistrationID, lngBillStateID, lngUserID, lngTransTypeID, " +
                                                    "curPayment, " +
                                                    "dteDateAdded, " +
                                                    "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strXCAlias, strXCTransID, strXCAuthCode, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode ) " +
                                                "SELECT @blnMarkedForCC, " +
                                                    "@lngPaymentTypeID, @lngRecordID, @lngRegistrationID, @lngBillStateID, @lngUserID, @lngTransTypeID, " +
                                                    "@curPayment, " +
                                                    "@dteDateAdded, " +
                                                    "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strXCAlias, @strXCTransID, @strXCAuthCode, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode ";

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        if (txtCardNumber.Text == "" && txtAcctNum.Text != "")
                                            lngPmtType = 11;
                                        else
                                            lngPmtType = 2;

                                        cmdDB.Parameters.Add(new OleDbParameter("@blnMarkedForCC", true));

                                        cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngRegistrationID", lngRegistrationID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngTransTypeID", 8));

                                        cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decPmtAmt));

                                        cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                        cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Tuition"));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strXCAlias", txtXCAlias.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strXCTransID", txtXCTransID.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strXCAuthCode", txtXCAuthCode.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));

                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                                        try { cmdDB.ExecuteNonQuery(); }
                                        catch { }

                                        strSQL = "SELECT @@IDENTITY";

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        long lngPmtTransID = 0;

                                        try { lngPmtTransID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                        catch { lngPmtTransID = 0; }

                                        //update pn ref in billing info
                                        clsIRCRUD.subUpdateBillingInfoCC(lngRecordID, txtPNRef.Text, txtCardNumber.Text, txtXCAlias.Text, txtEPSPmtAcctID.Text);
                                    }

                                    //add spending money if amt > 0
                                    if (decSpending > 0)
                                    {
                                        strSQL = "INSERT INTO tblTransactions " +
                                              "(blnMarkedForCC, " +
                                                    "lngRecordID, lngTransTypeID, lngPaymentTypeID, lngRegistrationID, lngBillStateID, lngUserID, " +
                                                  "curPayment, " +
                                                  "dteDateAdded, " +
                                                  "strTransactionDesc, strCCNumber, strCCExpDate, strCCValCode, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strBankName, strAcctNum, strRoutingNum, strXCAlias, strXCTransID, strXCAuthCode, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode )" +
                                          "SELECT @blnMarkedForCC, " +
                                                    "@lngRecordID, @lngTransTypeID, @lngPaymentTypeID, @lngRegistrationID, @lngBillStateID, @lngUserID, " +
                                                  "@curPayment, " +
                                                  "@dteDateAdded, " +
                                                  "@strTransactionDesc, @strCCNumber, @strCCExpDate, @strCCValCode, @strBillName, @strBillAddress, @strBillCity, @strBillZip, @strBillPhone, @strBankName, @strAcctNum, @strRoutingNum, @strXCAlias, @strXCTransID, @strXCAuthCode, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode ";

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        cmdDB.Parameters.Add(new OleDbParameter("@blnMarkedForCC", true));

                                        cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngRecordID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngTransTypeID", 25));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngRegistrationID", lngRegistrationID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                                        cmdDB.Parameters.Add(new OleDbParameter("@curPayment", decSpending));

                                        cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                        cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Web Reg Spending Money"));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumber.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strXCAlias", txtXCAlias.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strXCTransID", txtXCTransID.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strXCAuthCode", txtXCAuthCode.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                                        cmdDB.ExecuteNonQuery();

                                        strSQL = "SELECT @@IDENTITY";

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        long lngSpendingTransID = 0;

                                        try { lngSpendingTransID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                        catch { lngSpendingTransID = 0; }
                                    }

                                    //check for discounts
                                    strSQL = "SELECT curDiscAmt1, curDiscAmt10, curDiscAmt2, curDiscAmt3, curDiscAmt4, curDiscAmt5, curDiscAmt6, curDiscAmt7, curDiscAmt8, curDiscAmt9, " +
                                                "strDiscDesc1, strDiscDesc10, strDiscDesc2, strDiscDesc3, strDiscDesc4, strDiscDesc5, strDiscDesc6, strDiscDesc7, strDiscDesc8, strDiscDesc9 " +
                                            "FROM tblCampDefaults;";

                                    cmdDB.CommandText = strSQL;
                                    cmdDB.Parameters.Clear();

                                    using (OleDbDataReader drDiscount = cmdDB.ExecuteReader())
                                    {
                                        if (drDiscount.Read())
                                        {
                                            string strDiscDesc = "";

                                            decimal decDiscAmt = 0;

                                            for (int intJ = 1; intJ <= 10; intJ++)
                                            {
                                                try
                                                {
                                                    decDiscAmt = Convert.ToDecimal(drDiscount["curDiscAmt" + intJ.ToString()]);
                                                    strDiscDesc = Convert.ToString(drDiscount["strDiscDesc" + intJ.ToString()]);
                                                }
                                                catch
                                                {
                                                    strDiscDesc = "";
                                                    decDiscAmt = 0;
                                                }

                                                //if discount is being used by camp, check to see if it is applied to this camper
                                                if (decDiscAmt > 0)
                                                {
                                                    if (((CheckBox)fraDiscounts.Controls["chkDiscount" + intJ.ToString()]).Checked)
                                                    {
                                                        strSQL = "INSERT INTO tblTransactions " +
                                                                "( lngRecordID, lngTransTypeID, lngPaymentTypeID, lngRegistrationID, lngUserID, " +
                                                                    "curPayment, " +
                                                                    "dteDateAdded, " +
                                                                    "strTransactionDesc ) " +
                                                                "SELECT " + lngRecordID + ", 69, 6, " + lngRegistrationID + ", " + CTWebMgmt.lngUserID + ", " +
                                                                    decDiscAmt + ", " +
                                                                    "@dteDateAdded, " +
                                                                    "\"" + strDiscDesc + "\"";

                                                        using (OleDbCommand cmdDisc = new OleDbCommand(strSQL, conDB))
                                                        {
                                                            cmdDisc.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));
                                                            cmdDisc.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        drDiscount.Close();
                                    }

                                    //add any charges associated w/ custom reg flags                                                                        
                                    //get trans type to use for charges
                                    if (blnMultRegFlagCharge || intBlockChoice==1)
                                    {
                                        long lngDefCustRegFlagTransType = 0;

                                        strSQL = "SELECT lngDefCustRegFlagTransType " +
                                                "FROM tblCampDefaults";

                                        cmdDB.CommandText = strSQL;
                                        cmdDB.Parameters.Clear();

                                        try { lngDefCustRegFlagTransType = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                                        catch { lngDefCustRegFlagTransType = 0; }

                                        //next get rs of reg flags w/ a charge
                                        strSQL = "SELECT tblCustomFieldDefReg.lngCustomFieldDefRegID, " +
                                                    "tblCustomFieldDefReg.decCharge, " +
                                                    "tblCustomFieldDefReg.strLocalCaption " +
                                                "FROM tblCustomFieldDefReg " +
                                                "WHERE tblCustomFieldDefReg.blnUseLocal=True AND " +
                                                    "tblCustomFieldDefReg.decCharge>0 AND " +
                                                    "tblCustomFieldDefReg.strFieldType='FLAG'";

                                        cmdDB.Parameters.Clear();
                                        cmdDB.CommandText = strSQL;

                                        using (OleDbDataReader drFlagCharge = cmdDB.ExecuteReader())
                                        {
                                            while (drFlagCharge.Read())
                                            {
                                                long lngCustomFieldDefRegID = 0;

                                                try { lngCustomFieldDefRegID = Convert.ToInt32(drFlagCharge["lngCustomFieldDefRegID"]); }
                                                catch { lngCustomFieldDefRegID = 0; }

                                                if (((CheckBox)pagCustomReg.Controls["panCustomReg_" + lngCustomFieldDefRegID.ToString()].Controls["chkCustReg_" + lngCustomFieldDefRegID.ToString()]).Checked)
                                                {
                                                    decimal decCharge = 0;

                                                    try { decCharge = Convert.ToDecimal(drFlagCharge["decCharge"]); }
                                                    catch { decCharge = 0; }

                                                    string strDesc = "";

                                                    try { strDesc = "Charge for " + Convert.ToString(drFlagCharge["strLocalCaption"]); }
                                                    catch { strDesc = "Charge for custom reg flag"; }

                                                    //add transaction
                                                    if (decCharge > 0)
                                                        subAddRegFlagCharge(decCharge, lngRegistrationID, lngRecordID, CTWebMgmt.lngUserID, lngDefCustRegFlagTransType, strDesc);
                                                }
                                            }

                                            drFlagCharge.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //add donation if required
                    decimal decDonation = 0;

                    try { decDonation = decimal.Parse(txtDonation.Text, System.Globalization.NumberStyles.Currency); }
                    catch { decDonation = 0; }

                    if (decDonation > 0)
                    {
                        long lngGiftCategory = 0;
                        long lngCampaign = 0;
                        long lngTrigger = 0;

                        strSQL = "SELECT lngOLGiftCategoryID, lngOLGiftCampaign, lngOLTriggerID " +
                                "FROM tblCampDefaults";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                        {
                            if (drDef.Read())
                            {
                                try { lngGiftCategory = Convert.ToInt32(drDef["lngOLGiftCategoryID"]); }
                                catch { lngGiftCategory = 0; }

                                try { lngCampaign = Convert.ToInt32(drDef["lngOLGiftCampaign"]); }
                                catch { lngCampaign = 0; }

                                try { lngTrigger = Convert.ToInt32(drDef["lngOLTriggerID"]); }
                                catch { lngTrigger = 0; }
                            }

                            drDef.Close();
                        }

                        long lngGiftID = 0;

                        strSQL = "INSERT INTO tblGift " +
                                "(lngGiftCategoryID, lngRecordID, lngCampaignID, lngGiftTypeID, lngTriggerID, lngPaymentTypeID, lngBillStateID, " +
                                    "dteGiftDate, dteDateEntered, " +
                                    "curAmount, " +
                                    "strAcctNum, strBankName, strBillAddress, strBillCity, strBillName, strBillPhone, strBillZip, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum, strXCAlias, strXCTransID, strXCAuthCode, strPNRef, strEPSApprovalNumber, strEPSPmtAcctID, strEPSTransID, strEPSValidationCode, " +
                                    "mmoNotes) " +
                                "SELECT @lngGiftCategoryID, @lngRecordID, @lngCampaignID, @lngGiftTypeID, @lngTriggerID, @lngPaymentTypeID, @lngBillStateID, " +
                                    "@dteGiftDate, @dteDateEntered, " +
                                    "@curAmount, " +
                                    "@strAcctNum, @strBankName, @strBillAddress, @strBillCity, @strBillName, @strBillPhone, @strBillZip, @strCCExpDate, @strCCNumber, @strCCValCode, @strRoutingNum, @strXCAlias, @strXCTransID, @strXCAuthCode, @strPNRef, @strEPSApprovalNumber, @strEPSPmtAcctID, @strEPSTransID, @strEPSValidationCode, " +
                                    "@mmoNotes";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        long lngBillStateID = 0;

                        try { lngBillStateID = ((clsCboItem)cboBillState.SelectedItem).ID; }
                        catch { lngBillStateID = 0; }

                        cmdDB.Parameters.Add(new OleDbParameter("@lngGiftCategoryID", lngGiftCategory));

                        long lngDonorID = 0;

                        if (radDonationToParent.Checked)
                            lngDonorID = lngParentIRID;
                        else
                            lngDonorID = lngRecordID;

                        cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", lngDonorID));

                        cmdDB.Parameters.Add(new OleDbParameter("@lngCampaignID", lngCampaign));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngGiftTypeID", 1));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngTriggerID", lngTrigger));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngPaymentTypeID", lngPmtType));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngBillStateID", lngBillStateID));

                        cmdDB.Parameters.Add(new OleDbParameter("@dteGiftDate", dteRegDate));
                        cmdDB.Parameters.Add(new OleDbParameter("@dteDateEntered", OleDbType.Date));
                        cmdDB.Parameters["@dteDateEntered"].Value = DateTime.Now;

                        cmdDB.Parameters.Add(new OleDbParameter("@curAmount", decDonation));
                        cmdDB.Parameters.Add(new OleDbParameter("@strAcctNum", txtAcctNum.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strBankName", txtBankName.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strBillAddress", txtBillAddress.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strBillCity", txtBillCity.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strBillName", txtBillName.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strBillPhone", txtBillPhone.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strBillZip", txtBillZip.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCCExpDate", txtExpDate.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCCNumber", txtCardNumberUnmasked.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strCCValCode", txtCVV2.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strRoutingNum", txtRoutingNum.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strXCAlias", txtXCAlias.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strXCTransID", txtXCTransID.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strXCAuthCode", txtXCAuthCode.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strPNRef", txtPNRef.Text));

                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSApprovalNumber", txtEPSApprovalNumber.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSPmtAcctID", txtEPSPmtAcctID.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSTransID", txtEPSTransID.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strEPSValidationCode", txtEPSValidationCode.Text));

                        cmdDB.Parameters.Add(new OleDbParameter("@mmoNotes", "Online Donation"));

                        cmdDB.ExecuteNonQuery();

                        strSQL = "SELECT @@IDENTITY";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { lngGiftID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { lngGiftID = 0; }

                        //mark record as donor
                        strSQL = "UPDATE tblRecords " +
                                "SET blnDonor = True " +
                                "WHERE lngRecordID=" + lngDonorID.ToString();

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        cmdDB.ExecuteNonQuery();
                    }

                    //add choices for camper
                    strSQL = "SELECT lngChoiceID " +
                            "FROM tblChoices " +
                            "WHERE lngRecordID=" + lngRecordID.ToString();

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    if (Convert.ToInt32(cmdDB.ExecuteScalar()) > 0)
                    {
                        //create choices record
                        strSQL = "INSERT INTO tblChoices ( lngRecordID, lngBlockID1, lngBlockID2, lngBlockID3, lngBlockID4, lngBlockID5, lngBlockID6, lngBlockID7, lngBlockID8, lngBlockID9, lngBlockID10 ) " +
                                "SELECT " + lngRecordID.ToString();

                        for (int intI = 1; intI <= 10; intI++)
                        {
                            try { strSQL += ", " + ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intI.ToString()]).SelectedItem).ID.ToString(); }
                            catch { strSQL += ", 0"; }
                        }
                    }
                    else
                    {
                        //update existing choices record
                        strSQL = "UPDATE tblChoices " +
                                "SET ";

                        for (int intI = 1; intI <= 10; intI++)
                        {
                            try
                            {
                                long lngChoiceBlockID = 0;

                                try { lngChoiceBlockID = ((clsCboItem)((ComboBox)pagRegInfo.Controls["cboBlockChoice" + intI.ToString()]).SelectedItem).ID; }
                                catch { lngChoiceBlockID = 0; }

                                strSQL += "lngBlockID" + intI.ToString() + "=" + lngChoiceBlockID.ToString() + ", ";
                            }
                            catch { strSQL += "lngBlockID" + intI.ToString() + "=0, "; }
                        }

                        strSQL = strSQL.Substring(0, strSQL.Length - 2);

                    }

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    cmdDB.ExecuteNonQuery();

                    //apply hold registration, shared cost
                    //////////////////////////////
                    if (cboRegHold.SelectedIndex > 0)
                    {
                        long lngRegHoldID = 0;

                        try { lngRegHoldID = Convert.ToInt32(((clsCboItem)cboRegHold.SelectedItem).ID); }
                        catch { lngRegHoldID = 0; }

                        if (lngRegHoldID > 0)
                        {
                            //add transaction (if applicable) to registration holder, camper
                            decimal decSplitAmt = 0;
                            long lngRegHolderID = 0;

                            bool blnSharedCostPercent = false;

                            strSQL = "SELECT blnSharedCostPercent, " +
                                        "curCostShare, " +
                                        "lngRecordID " +
                                    "FROM tblRegHold " +
                                    "WHERE lngRegHoldID=" + lngRegHoldID.ToString();

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            using (OleDbDataReader drHold = cmdDB.ExecuteReader())
                            {
                                if (drHold.Read())
                                {
                                    try { blnSharedCostPercent = Convert.ToBoolean(drHold["blnSharedCostPercent"]); }
                                    catch { blnSharedCostPercent = false; }

                                    try { decSplitAmt = Convert.ToDecimal(drHold["curCostShare"]); }
                                    catch { decSplitAmt = 0; }

                                    try { lngRegHolderID = Convert.ToInt32(drHold["lngRecordID"]); }
                                    catch { lngRegHolderID = 0; }
                                }

                                drHold.Close();
                            }

                            if (decSplitAmt > 0)
                            {
                                if (blnSharedCostPercent)
                                    decSplitAmt = decSplitAmt * decTuitionCharge;

                                strSQL = "INSERT INTO tblTransactions ( lngRecordID, lngTransTypeID, lngRegistrationID, lngRegHoldID, lngUserID, " +
                                            "curCharge, " +
                                            "dteDateAdded ) " +
                                        "VALUES " +
                                            "(" + lngRegHolderID + ", 38, " + lngRegistrationID + ", " + lngRegHoldID + ", " + CTWebMgmt.lngCTUserID.ToString() + ", " +
                                            decSplitAmt + ", " +
                                            "@dteDateAdded)";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                cmdDB.ExecuteNonQuery();

                                //add credit to registering camper
                                strSQL = "INSERT INTO tblTransactions ( lngRecordID, lngTransTypeID, lngRegistrationID, lngRegHoldID, lngUserID, " +
                                            "curPayment, " +
                                            "dteDateAdded ) " +
                                        "VALUES " +
                                        "(" + lngRecordID + ", 39 , " + lngRegistrationID + ", " + lngRegHoldID + ", " + CTWebMgmt.lngCTUserID.ToString() + ", " +
                                            decSplitAmt + ", " +
                                            "@dteDateAdded)";

                                cmdDB.CommandText = strSQL;
                                cmdDB.Parameters.Clear();

                                cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                                cmdDB.ExecuteNonQuery();
                            }

                            strSQL = "UPDATE tblRegistrations " +
                                    "SET tblRegistrations.lngRegHoldID = " + lngRegHoldID.ToString() + " " +
                                    "WHERE tblRegistrations.lngRegistrationID=" + lngRegistrationID.ToString();

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.ExecuteNonQuery();
                        }
                    }
                    ////////////////////////////////

                    //mark camper as processed
                    strSQL = "UPDATE tblWebIndRegistrations " +
                        "SET blnProcessed=True " +
                        "WHERE lngRegistrationWebID=" + lngRegWebID.ToString();

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    cmdDB.ExecuteNonQuery();
                }

                conDB.Close();
            }

            Close();
        }

        private void subAddRegFlagCharge(decimal _decCharge, long _lngRegistrationID, long _lngRecordID, long _lngUserID, long _lngTransType, string _strTransDesc)
        {
            string strSQL = "";

            DateTime dteRegDate;

            try { dteRegDate = Convert.ToDateTime(txtRegDate.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with the registration date: " + ex.Message + "\nThe current date will be used.");
                dteRegDate = DateTime.Now;
            }

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblBlock.lngCampID " +
                        "FROM tblBlock " +
                            "INNER JOIN tblRegistrations ON tblBlock.lngBlockID = tblRegistrations.lngBlockID " +
                        "WHERE tblRegistrations.lngRegistrationID=" + _lngRegistrationID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    long lngProgramTypeID = 0;

                    try { lngProgramTypeID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { lngProgramTypeID = 0; }

                    strSQL = "INSERT INTO tblTransactions " +
                            "(lngTransTypeID, lngRegistrationID, lngProgramTypeID, lngRecordID, lngUserID, " +
                                "curCharge, " +
                                "dteDateAdded, " +
                                "strTransactionDesc) " +
                            "SELECT @lngTransTypeID, @lngRegistrationID, @lngProgramTypeID, @lngRecordID, @lngUserID, " +
                                "@curCharge, " +
                                "@dteDateAdded, " +
                                "@strTransactionDesc";

                    cmdDB.Parameters.Clear();
                    cmdDB.CommandText = strSQL;

                    if (_lngTransType <= 0) _lngTransType = 15;

                    cmdDB.Parameters.Add(new OleDbParameter("@lngTransTypeID", _lngTransType));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngRegistrationID", _lngRegistrationID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngProgramTypeID", lngProgramTypeID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngRecordID", _lngRecordID));
                    cmdDB.Parameters.Add(new OleDbParameter("@lngUserID", _lngUserID));
                    cmdDB.Parameters.Add(new OleDbParameter("@curCharge", _decCharge));

                    cmdDB.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                    cmdDB.Parameters.Add(new OleDbParameter("@strTransactionDesc", _strTransDesc));

                    try { cmdDB.ExecuteNonQuery(); }
                    catch (Exception ex) { }
                }

                conDB.Close();
            }
        }

        private void btnProfileDetails_Click(object sender, EventArgs e)
        {
            long lngProfileID = 0;

            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblWebRecords.lngProfileWebID " +
                            "FROM tblWebIndRegistrations " +
                                "INNER JOIN tblWebRecords ON tblWebIndRegistrations.lngRecordWebID = tblWebRecords.lngRecordWebID " +
                            "WHERE tblWebIndRegistrations.lngRegistrationWebID=" + lngRegWebID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { lngProfileID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { lngProfileID = 0; }
                    }

                    conDB.Close();
                }

                using (frmProfileDetails objProfileDetails = new frmProfileDetails(lngProfileID))
                {
                    objProfileDetails.ShowDialog();
                }
            }
            catch { }
        }

        private bool fcnIsCamperRegistered(long _lngRecordID, long _lngBlockID)
        {
            string strSQL = "";

            bool blnRes = true;

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngRegistrationID " +
                        "FROM tblRegistrations " +
                        "WHERE lngBlockID=" + _lngBlockID.ToString() + " " +
                            "AND lngRecordID=" + _lngRecordID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    long lngRegID = 0;

                    try { lngRegID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { lngRegID = 0; }

                    if (lngRegID > 0) blnRes = true;
                }

                conDB.Close();
            }

            return blnRes;
        }

        private bool fcnValBlockChoice(long _lngRecordID, long _lngBlockID)
        {
            string strSQL = "";

            bool blnRes = true;

            bool blnCamperGender = false;

            int intCamperGrade = 0;
            int intCamperAge = 0;
            int intBlockGender = 0;
            int intBlockMinGrade = 0;
            int intBlockMaxGrade = 0;
            int intBlockMinAge = 0;
            int intBlockMaxAge = 0;

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    //get attributes of camper
                    strSQL = "SELECT blnGender, " +
                                "intGradeCompleted, " +
                                "dteBirthDate " +
                            "FROM tblRecords " +
                            "WHERE lngRecordID=" + _lngRecordID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drIR = cmdDB.ExecuteReader())
                        {
                            if (drIR.Read())
                            {
                                try { blnCamperGender = Convert.ToBoolean(drIR["blnGender"]); }
                                catch { blnCamperGender = false; }

                                try { intCamperGrade = Convert.ToInt32(drIR["intGradeCompleted"]); }
                                catch { intCamperGrade = 0; }

                                DateTime dteDOB = DateTime.MinValue;

                                try { dteDOB = Convert.ToDateTime(drIR["dteBirthDate"]); }
                                catch { dteDOB = DateTime.MinValue; }

                                if (dteDOB > DateTime.MinValue)
                                {
                                    intCamperAge = DateTime.Today.Year - dteDOB.Year;
                                    if (dteDOB > DateTime.Today.AddYears(-intCamperAge)) intCamperAge--;
                                }
                            }

                            drIR.Close();
                        }

                        strSQL = "SELECT tlkpGradeGroup.intMinGrade, tlkpGradeGroup.intMaxGrade, tblBlock.lngGenderID, tlkpAgeGroup.intMinAge, tlkpAgeGroup.intMaxAge " +
                                "FROM (tblBlock " +
                                    "LEFT JOIN tlkpGradeGroup ON tblBlock.lngGradeGroupID = tlkpGradeGroup.lngGradeGroupID) " +
                                    "LEFT JOIN tlkpAgeGroup ON tblBlock.lngAgeGroupID = tlkpAgeGroup.lngAgeGroupID " +
                                "WHERE tblBlock.lngBlockID=" + _lngBlockID.ToString();

                        cmdDB.CommandText = strSQL;

                        using (OleDbDataReader drBlock = cmdDB.ExecuteReader())
                        {
                            if (drBlock.Read())
                            {
                                try { intBlockGender = Convert.ToInt32(drBlock["lngGenderID"]); }
                                catch { intBlockGender = 0; }

                                try
                                {
                                    intBlockMinGrade = Convert.ToInt32(drBlock["intMinGrade"]);
                                    intBlockMaxGrade = Convert.ToInt32(drBlock["intMaxGrade"]);
                                }
                                catch
                                {
                                    intBlockMinGrade = 0;
                                    intBlockMaxGrade = 0;
                                }

                                try
                                {
                                    intBlockMinAge = Convert.ToInt32(drBlock["intMinAge"]);
                                    intBlockMaxAge = Convert.ToInt32(drBlock["intMaxAge"]);
                                }
                                catch
                                {
                                    intBlockMinAge = 0;
                                    intBlockMaxAge = 0;
                                }
                            }

                            drBlock.Close();
                        }
                    }

                    conDB.Close();
                }

                //gender
                //1m 2f
                if ((intBlockGender == 1 && !blnCamperGender) || (intBlockGender == 2 && blnCamperGender))
                {
                    if (MessageBox.Show("This camper appears to be the wrong gender for this block.\n\nDo you wish to continue anyway?", "CampTrak Software", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        blnRes = false;
                }

                //age
                if (blnRes && (intBlockMinAge > intCamperAge || intBlockMaxAge < intCamperAge))
                {
                    if (MessageBox.Show("This camper appears to be the wrong age for this block.\n\nDo you wish to continue anyway?", "CampTrak Software", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        blnRes = false;
                }

                //grade
                if (blnRes && (intBlockMinGrade > intCamperGrade || intBlockMaxGrade < intCamperGrade))
                {
                    if (MessageBox.Show("This camper appears to be the wrong grade for this block.\n\nDo you wish to continue anyway?", "CampTrak Software", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        blnRes = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("There was an error validation a registration choice: " + ex.Message); }

            return blnRes;
        }

        public long fcnAddTuitionCharge(OleDbCommand _cmdDB, long _lngRecordID, long _lngBlockID, long _lngRegistrationID, ref decimal _decTuitionCharge)
        {
            //add a transaction for block tuition, return new transactionid
            string strSQL = "";

            decimal curBlockCharge = 0;
            decimal curConstDisc = 0;

            long lngMORID = 0;
            long lngRes = 0;
            long lngProgramTypeID = 0;

            DateTime dteRegDate;

            try { dteRegDate = Convert.ToDateTime(txtRegDate.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with the registration date: " + ex.Message + "\nThe current date will be used.");
                dteRegDate = DateTime.Now;
            }

            strSQL = "SELECT lngCampID, " +
                        "curCharge " +
                    "FROM tblBlock " +
                    "WHERE lngBlockID=" + _lngBlockID;

            _cmdDB.CommandText = strSQL;
            _cmdDB.Parameters.Clear();

            using (OleDbDataReader drBlock = _cmdDB.ExecuteReader())
            {
                if (drBlock.Read())
                {
                    try { curBlockCharge = Convert.ToDecimal(drBlock["curCharge"]); }
                    catch { curBlockCharge = 0; }

                    try { lngProgramTypeID = Convert.ToInt32(drBlock["lngCampID"]); }
                    catch { lngProgramTypeID = 0; }

                    //Find out if IR belongs to a constituent church
                    //loop through all the MORs associated with the IR and grab the MORID assoiciated with constituent church.

                    strSQL = "SELECT tblMOR.blnConstChurch, " +
                                "tblMOR.lngMORID, tblnkMORIR.lngRecordID, " +
                                "tblMOR.curConstDiscAmt " +
                            "FROM tblnkMORIR " +
                                "INNER JOIN tblMOR ON tblnkMORIR.lngMORID = tblMOR.lngMORID " +
                            "WHERE tblnkMORIR.lngRecordID=" + _lngRecordID.ToString();

                    using (OleDbCommand cmdMOR = new OleDbCommand(strSQL, _cmdDB.Connection))
                    {
                        using (OleDbDataReader drMORs = cmdMOR.ExecuteReader())
                        {
                            while (drMORs.Read())
                            {
                                bool blnConstChurch = false;

                                try { blnConstChurch = Convert.ToBoolean(drBlock["blnConstChurch"]); }
                                catch { blnConstChurch = false; }

                                if (blnConstChurch)
                                {
                                    try { lngMORID = Convert.ToInt32(drMORs["lngMORID"]); }
                                    catch { lngMORID = 0; }

                                    try { curConstDisc = Convert.ToDecimal(drMORs["curConstDiscAmt"]); }
                                    catch { curConstDisc = 0; }
                                }
                            }

                            drMORs.Close();
                        }
                    }

                    //add charge to transactions for tuition
                    //set constituent discount amt based on church association...
                    strSQL = "INSERT INTO tblTransactions " +
                            "(lngTransTypeID, lngRecordID, lngRegistrationID, lngUserID, lngProgramTypeID, " +
                                "curCharge, " +
                                "dteDateAdded, " +
                                "strTransactionDesc) " +
                            "SELECT @lngTransTypeID, @lngRecordID, @lngRegistrationID, @lngUserID, @lngProgramTypeID, " +
                                "@curCharge, " +
                                "@dteDateAdded, " +
                                "@strTransactionDesc;";

                    using (OleDbCommand cmdTrans = new OleDbCommand(strSQL, _cmdDB.Connection))
                    {
                        cmdTrans.CommandText = strSQL;
                        cmdTrans.Parameters.Clear();

                        cmdTrans.Parameters.Add(new OleDbParameter("@lngTransTypeID", 2));
                        cmdTrans.Parameters.Add(new OleDbParameter("@lngRecordID", _lngRecordID));
                        cmdTrans.Parameters.Add(new OleDbParameter("@lngRegistrationID", _lngRegistrationID));
                        cmdTrans.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                        cmdTrans.Parameters.Add(new OleDbParameter("@lngProgramTypeID", lngProgramTypeID));

                        cmdTrans.Parameters.Add(new OleDbParameter("@curCharge", OleDbType.Currency));
                        cmdTrans.Parameters["@curCharge"].Value = curBlockCharge;
                        _decTuitionCharge = curBlockCharge;

                        cmdTrans.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                        cmdTrans.Parameters.Add(new OleDbParameter("@strTransactionDesc", "Tuition Charge"));

                        cmdTrans.ExecuteNonQuery();

                        strSQL = "SELECT @@IDENTITY;";

                        cmdTrans.CommandText = strSQL;
                        cmdTrans.Parameters.Clear();

                        try { lngRes = Convert.ToInt32(cmdTrans.ExecuteScalar()); }
                        catch { lngRes = 0; }

                        //if there is a discount, add it as a payment
                        if (curConstDisc > 0)
                        {
                            strSQL = "INSERT INTO tblTransactions " +
                                    "(lngTransTypeID, lngRecordID, lngRegistrationID, lngUserID, lngProgramTypeID, " +
                                        "curPayment, " +
                                        "dteDateAdded) " +
                                    "SELECT @lngTransTypeID, @lngRecordID, @lngRegistrationID, @lngUserID, @lngProgramTypeID, " +
                                        "@curPayment, " +
                                        "@dteDateAdded";

                            cmdTrans.Parameters.Clear();
                            cmdTrans.CommandText = strSQL;

                            cmdTrans.Parameters.Add(new OleDbParameter("@lngTransTypeID", 54));
                            cmdTrans.Parameters.Add(new OleDbParameter("@lngRecordID", _lngRecordID));
                            cmdTrans.Parameters.Add(new OleDbParameter("@lngRegistrationID", _lngRegistrationID));
                            cmdTrans.Parameters.Add(new OleDbParameter("@lngUserID", CTWebMgmt.lngUserID));
                            cmdTrans.Parameters.Add(new OleDbParameter("@lngProgramTypeID", lngProgramTypeID));
                            cmdTrans.Parameters.Add(new OleDbParameter("@curPayment", curConstDisc));
                            cmdTrans.Parameters.Add(new OleDbParameter("@dteDateAdded", dteRegDate));

                            cmdTrans.ExecuteNonQuery();
                        }
                    }
                }

                drBlock.Close();
            }

            return lngRes;
        }

        private void subOneBlockRegCount(OleDbCommand _cmdDB, long _lngBlockID)
        {
            string strSQL = "";

            int intNonHoldReg = 0;
            int intHolds = 0;

            strSQL = "SELECT Count(tblRegistrations.lngRegistrationID) AS intNonHoldReg " +
                    "FROM tblRegistrations " +
                        "LEFT JOIN tblRegHold ON tblRegistrations.lngRegHoldID = tblRegHold.lngRegHoldID " +
                    "WHERE tblRegHold.lngRegHoldID Is Null AND " +
                        "tblRegistrations.lngBlockID=" + _lngBlockID.ToString();

            _cmdDB.CommandText = strSQL;
            _cmdDB.Parameters.Clear();

            try { intNonHoldReg = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
            catch { intNonHoldReg = 0; }

            strSQL = "SELECT Sum(tblRegHold.intHoldQty) AS intHolds " +
                    "FROM tblRegHold " +
                    "WHERE tblRegHold.lngBlockID=" + _lngBlockID.ToString();

            _cmdDB.CommandText = strSQL;

            try { intHolds = Convert.ToInt32(_cmdDB.ExecuteScalar()); }
            catch { intHolds = 0; }

            strSQL = "UPDATE tblBlock " +
                    "SET intCurrEnrollment = " + (intHolds + intNonHoldReg).ToString() + " " +
                    "WHERE lngBlockID=" + _lngBlockID.ToString();

            _cmdDB.CommandText = strSQL;
            _cmdDB.Parameters.Clear();

            try { _cmdDB.ExecuteNonQuery(); }
            catch { }

            return;
        }

        private void subManageSpecNeeds(OleDbCommand _cmdDB, long _lngRegWebID, long _lngRecordID)
        {
            string strSQL = "";
            string strLocalSpecNeeds = "";
            string strWebSpecNeeds = "";

            strSQL = "SELECT tblWebRecords.mmoSpecialNeeds " +
                    "FROM tblWebIndRegistrations " +
                        "INNER JOIN tblWebRecords ON tblWebIndRegistrations.lngRecordWebID = tblWebRecords.lngRecordWebID " +
                    "WHERE tblWebIndRegistrations.[lngRegistrationWebID]=" + _lngRegWebID.ToString();

            _cmdDB.CommandText = strSQL;
            _cmdDB.Parameters.Clear();

            try { strWebSpecNeeds = Convert.ToString(_cmdDB.ExecuteScalar()); }
            catch { strWebSpecNeeds = ""; }

            if (strWebSpecNeeds != "")
            {
                strSQL = "SELECT mmoSpecialNeeds " +
                        "FROM tblRecords " +
                        "WHERE lngRecordID=" + _lngRecordID;

                _cmdDB.CommandText = strSQL;
                _cmdDB.Parameters.Clear();

                try { strLocalSpecNeeds = Convert.ToString(_cmdDB.ExecuteScalar()); }
                catch { strLocalSpecNeeds = ""; }

                //'spec needs differ--figure out which to use
                if (strLocalSpecNeeds != strWebSpecNeeds)
                {
                    //'no local spec needs defined--update to web
                    if (strLocalSpecNeeds == "")
                    {
                        strSQL = "UPDATE tblRecords " +
                                "SET tblRecords.blnSpecialNeeds = True, " +
                                    "tblRecords.mmoSpecialNeeds = @strWebSpecNeeds " +
                                "WHERE tblRecords.lngRecordID=" + _lngRecordID;

                        _cmdDB.CommandText = strSQL;

                        _cmdDB.Parameters.Clear();
                        _cmdDB.Parameters.Add(new OleDbParameter("@strWebSpecNeeds", strWebSpecNeeds));

                        _cmdDB.ExecuteNonQuery();
                    }
                    else
                    {
                        //special needs differ--ask user if they want to update
                        if (MessageBox.Show("Special needs entered online differ from local special needs.\n\nDo you wish to over-write the local definition with the web definition?\n\nLocal definition: " + strLocalSpecNeeds + "\n\nWeb definition: " + strWebSpecNeeds, "Update Special Needs?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            strSQL = "UPDATE tblRecords " +
                                    "SET blnSpecialNeeds = True, " +
                                        "mmoSpecialNeeds = @strWebSpecNeeds " +
                                    "WHERE lngRecordID=" + _lngRecordID;

                            _cmdDB.CommandText = strSQL;

                            _cmdDB.Parameters.Clear();
                            _cmdDB.Parameters.Add(new OleDbParameter("@strWebSpecNeeds", strWebSpecNeeds));

                            _cmdDB.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void chkReg_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //refresh items in reg hold options based on blocks camper is registering for
                cboRegHold.Items.Clear();

                for (int intI = 1; intI <= 11; intI++)
                {

                    if (((CheckBox)((CheckBox)sender).Parent.Controls["chkReg" + intI.ToString()]).Checked)
                    {
                        long lngBlockID = 0;
                        lngBlockID = ((clsCboItem)((ComboBox)((CheckBox)sender).Parent.Controls["cboBlockChoice" + intI.ToString()]).SelectedItem).ID;

                        subAddRegHoldCboItems(lngBlockID);
                    }
                }

                subAddRegHoldCboItems(0);

                if (cboRegHold.Items.Count > 0)
                {
                    cboRegHold.Items.Insert(0, "Select reg hold if applicable...");
                    cboRegHold.Enabled = true;
                    cboRegHold.SelectedIndex = 0;

                    subSetRegHold();
                }
                else
                {
                    cboRegHold.Items.Insert(0, "No reg holds for selected block");
                    cboRegHold.Enabled = false;
                    cboRegHold.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error selecting the hold for this registration: " + ex.Message);
            }
        }

        private void subSetRegHold()
        {
            try
            {
                //get id
                //see if id matches a list item

                long lngRegHoldID = 0;

                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblWebIndRegistrations.lngRegHoldID " +
                            "FROM tblWebIndRegistrations " +
                            "WHERE tblWebIndRegistrations.lngRegistrationWebID=" + lngRegWebID.ToString();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        try { lngRegHoldID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { lngRegHoldID = 0; }
                    }

                    conDB.Close();
                }

                if (lngRegHoldID > 0)
                {
                    for (int intI = 1; intI < cboRegHold.Items.Count; intI++)
                    {
                        if (((clsCboItem)cboRegHold.Items[intI]).ID == lngRegHoldID)
                            cboRegHold.SelectedIndex = intI;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error looking up the hold for this registration: " + ex.Message);
            }
        }

        private void subAddRegHoldCboItems(long _lngBlockID)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblRegHold.blnSharedCostPercent, " +
                            "tblRegHold.lngRegHoldID, " +
                            "tblRegHold.curCostShare, " +
                            "tblRegHold.dteDeadline, " +
                            "tblRecords.strCompanyName, tblRecords.strLastCoName, tblRecords.strFirstName " +
                        "FROM tblRegHold " +
                            "INNER JOIN tblRecords ON tblRegHold.lngRecordID = tblRecords.lngRecordID " +
                        "WHERE tblRegHold.lngBlockID=" + _lngBlockID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drRegHold = cmdDB.ExecuteReader())
                    {
                        while (drRegHold.Read())
                        {
                            string strVal = "";
                            string strFName = "";
                            string strLName = "";
                            string strCompany = "";

                            decimal decCostShare = 0;

                            DateTime dteNow = DateTime.Now;
                            DateTime dteDeadline = dteNow;

                            long lngID = 0;

                            bool blnSharedCostPercent = false;

                            try { blnSharedCostPercent = Convert.ToBoolean(drRegHold["blnSharedCostPercent"]); }
                            catch { blnSharedCostPercent = false; }

                            try { lngID = Convert.ToInt32(drRegHold["lngRegHoldID"]); }
                            catch { lngID = 0; }

                            try { strFName = Convert.ToString(drRegHold["strFirstName"]); }
                            catch { strFName = ""; }

                            try { strLName = Convert.ToString(drRegHold["strLastCoName"]); }
                            catch { strLName = ""; }

                            try { strCompany = Convert.ToString(drRegHold["strCompanyName"]); }
                            catch { strCompany = ""; }

                            try { decCostShare = Convert.ToDecimal(drRegHold["curCostShare"]); }
                            catch { decCostShare = 0; }

                            try { dteDeadline = Convert.ToDateTime(drRegHold["dteDeadline"]); }
                            catch { dteDeadline = dteNow; }

                            if (strCompany == "")
                            {
                                if (strLName == "")
                                    strVal = strFName;
                                else
                                {
                                    if (strFName == "")
                                        strVal = strLName;
                                    else
                                        strVal = strLName + ", " + strFName;
                                }
                            }
                            else
                            {
                                strVal = strCompany;

                                if (strLName == "")
                                {
                                    if (strFName != "")
                                        strVal += " (" + strFName + ")";
                                }
                                else
                                {
                                    if (strFName == "")
                                        strVal += " (" + strLName + ")";
                                    else
                                        strVal += " (" + strLName + ", " + strFName + ")";
                                }
                            }

                            if (blnSharedCostPercent)
                                strVal += " - " + decCostShare * 100 + "%";
                            else
                                strVal += " - " + decCostShare.ToString("C");

                            if (dteDeadline != dteNow)
                                strVal += " - " + dteDeadline.ToString("MM/d/yyyy");

                            cboRegHold.Items.Add(new clsCboItem(lngID, strVal));
                        }

                        drRegHold.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnDetailRpt_Click(object sender, EventArgs e)
        {
            List<long> lngRegWebIDs = new List<long>();

            lngRegWebIDs.Add(lngRegWebID);

            using (Reports.frmWebCamperDetails objWebCamperDetails = new global::CTWebMgmt.Ind.Reports.frmWebCamperDetails(lngRegWebIDs))
            {
                objWebCamperDetails.WindowState = FormWindowState.Maximized;
                objWebCamperDetails.ShowDialog();
            }
        }

        private long fcnProcessProfileRecord(bool _blnRequired)
        {
            //match profile info to existing record in camptrak.
            //give option to create a record if it doesn't exist
            //update local record's web id, update web record's local id
            //return local record id of profile

            long lngProfileWebID = 0;
            long lngProfileID = 0;
            long lngStateID = 0;
            string strFName = "";
            string strLName = "";
            string strAddress = "";
            string strCity = "";
            string strZip = "";
            string strPhone = "";
            string strCellPhone = "";
            string strEmail = "";
            string strPassword="";
            string strConfEmail = "";

            string strSQL = "";

            try { lngProfileWebID = Convert.ToInt32(btnProfileDetails.Tag); }
            catch { lngProfileWebID = 0; }

            //collect details of profile
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT lngStateID, lngRecordID, " +
                            "strFirstName, strLastCoName, strAddress, strCity, strZip, strHomePhone, strCellPhone, strEmail, strPassword " +
                        "FROM tblWebRecords " +
                        "WHERE lngRecordWebID=" + lngProfileWebID.ToString();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drWeb = cmdDB.ExecuteReader())
                    {
                        if (drWeb.Read())
                        {
                            try { lngStateID = Convert.ToInt32(drWeb["lngStateID"]); }
                            catch { lngStateID = 0; }

                            try { lngProfileID = Convert.ToInt32(drWeb["lngRecordID"]); }
                            catch { lngProfileID = 0; }

                            try { strFName = Convert.ToString(drWeb["strFirstName"]); }
                            catch { strFName = ""; }

                            try { strLName = Convert.ToString(drWeb["strLastCoName"]); }
                            catch { strLName = ""; }

                            try { strAddress = Convert.ToString(drWeb["strAddress"]); }
                            catch { strAddress = ""; }

                            try { strCity = Convert.ToString(drWeb["strCity"]); }
                            catch { strCity = ""; }

                            try { strZip = Convert.ToString(drWeb["strZip"]); }
                            catch { strZip = ""; }

                            try { strPhone = Convert.ToString(drWeb["strHomePhone"]); }
                            catch { strPhone = ""; }

                            try { strCellPhone = Convert.ToString(drWeb["strCellPhone"]); }
                            catch { strCellPhone = ""; }

                            try { strEmail = Convert.ToString(drWeb["strEmail"]); }
                            catch { strEmail = ""; }  
                            try{strPassword=Convert.ToString(drWeb["strPassword"]);}
                            catch{strPassword="";}
                        }

                        drWeb.Close();
                    }

                    clsIR irToSearch = new clsIR(0, lngStateID, strFName, strLName, "", strAddress, strCity, strZip, strPhone, "", strCellPhone, strEmail);

                    irToSearch.blnParent = true;
                    irToSearch.blnCamper = false;

                    irToSearch.lngRecordWebID = lngProfileWebID;
                    irToSearch.lngRecordID = lngProfileID;

                    irToSearch.lngStateID = lngStateID;

                    irToSearch.strEmail = strEmail;
                    irToSearch.strConfEmail = strEmail;
                    irToSearch.strHomePhone = strPhone;
                    irToSearch.strZip = strZip;
                    irToSearch.strCity = strCity;
                    irToSearch.strAddress = strAddress;
                    irToSearch.strLName = strLName;
                    irToSearch.strFName = strFName;

                    irToSearch.strCustom = fcnGetCustomValsWebIR(lngProfileWebID);

                    if (lngProfileID <= 0)
                    {
                        bool blnContinueToSearch = false;

                        if (_blnRequired)
                            blnContinueToSearch = true;
                        else
                        {
                            if (MessageBox.Show("The profile for this registration appears to be new.\nWould you like to match the profile to a record in CampTrak?\nIf an existing record is not found you will be prompted to create a new one.\n\nClick 'No' to register the camper but not link a record to their profile.", "Match Profile Record?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                blnContinueToSearch = true;
                            else
                                blnContinueToSearch = false;
                        }

                        if (blnContinueToSearch)
                        {
                            using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find parent record"))
                            {
                                if (objFindIR.ShowDialog() == DialogResult.OK)
                                {
                                    if (objFindIR.irToSearch.lngRecordID == 0)
                                        return lngProfileID;
                                    else
                                    {
                                        lngProfileID = objFindIR.irToSearch.lngRecordID;

                                        if (!objFindIR.blnAddNew)
                                        {
                                            using (IRUtils.frmReconcileIR objReconcileIR = new global::CTWebMgmt.IRUtils.frmReconcileIR("tblWebRecords", lngProfileID, lngProfileWebID))
                                            {
                                                if (objReconcileIR.ShowDialog() == DialogResult.Cancel)
                                                    return lngProfileID;
                                                else
                                                    lngProfileID = objReconcileIR.lngDBID;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (lngProfileID > 0) clsIRCRUD.subSetToSync(lngProfileID);
                                    return lngProfileID;
                                }
                            }

                            //now we've got local id (lngProfileID) and web id (lngProfileWebID)
                            //update web record w/ local id, and local record w/ web id

                            //'put web record id in tblRecords
                            strSQL = "UPDATE tblRecords " +
                                    "SET blnToSync=@blnToSync, "+
                                        "lngRecordWebID=" + lngProfileWebID.ToString() + ", "+
                                        "strPassword=@strPassword " +
                                    "WHERE lngRecordID=" + lngProfileID.ToString();

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            cmdDB.Parameters.AddWithValue("@blnToSync", true);
                            cmdDB.Parameters.AddWithValue("@strPassword", strPassword);

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }

                            //put local ir id in tblWebRecords
                            strSQL = "UPDATE tblWebRecords " +
                                    "SET lngRecordID = " + lngProfileID.ToString() + " " +
                                    "WHERE lngRecordWebID=" + lngProfileWebID.ToString();

                            cmdDB.CommandText = strSQL;
                            cmdDB.Parameters.Clear();

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }
                        }
                    }
                }

                conDB.Close();
            }

            if (lngProfileID > 0)
            {
                clsIRCRUD.subSetToSync(lngProfileID);

                //post connected record id to web server
                //using (wsXferEventInfoV2.xfereventinfov2SoapClient svc = new global::CTWebMgmt.wsXferEventInfoV2.xfereventinfov2SoapClient("xfereventinfov2Soap"))
                using (wsXferEventInfoV2.xfereventinfov2 svc = new global::CTWebMgmt.wsXferEventInfoV2.xfereventinfov2())
                {
                    string strWebRes = "";

                    strWebRes = svc.fcnUpdateLocalID(lngProfileWebID, lngProfileID, clsAppSettings.GetAppSettings().lngCTUserID, clsAppSettings.GetAppSettings().strWebDBConn);

                    if (strWebRes != "") MessageBox.Show("There was an error updating the parent's web id: " + strWebRes);
                }
            }

            return lngProfileID;
        }

        private List<string[]> fcnGetCustomValsWebIR(long _lngRecordWebID)
        {
            string strSQL = "";
            List<string[]> lstRes = new List<string[]>();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCustomFieldValWebIR.strLocalCaption, tblCustomFieldValWebIR.strValue " +
                        "FROM tblCustomFieldValWebIR " +
                        "WHERE tblCustomFieldValWebIR.lngRecordWebID=@lngRecordWebID";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.Parameters.AddWithValue("@lngRecordWebID", _lngRecordWebID);

                    using (OleDbDataReader drCustomFieldVal = cmdDB.ExecuteReader())
                    {
                        while (drCustomFieldVal.Read())
                        {
                            string[] strCustom = new string[2];

                            string strLocalCaption = "";
                            string strValue = "";

                            try { strLocalCaption = Convert.ToString(drCustomFieldVal["strLocalCaption"]); }
                            catch { strLocalCaption = ""; }

                            try { strValue = Convert.ToString(drCustomFieldVal["strValue"]); }
                            catch { strValue = ""; }

                            strCustom[0] = strLocalCaption;
                            strCustom[1] = strValue;

                            lstRes.Add(strCustom);
                        }

                        drCustomFieldVal.Close();
                    }
                }

                conDB.Close();
            }

            return lstRes;
        }

        private void txtDonation_TextChanged(object sender, EventArgs e)
        {
            decimal decDonation = 0;

            try { decDonation = decimal.Parse(txtDonation.Text, System.Globalization.NumberStyles.Currency); }
            catch { decDonation = 0; }

            if (decDonation > 0)
            {
                if (!fraApplyDonationTo.Visible)
                {
                    radDonationToParent.Checked = true;
                    radDonationToCamper.Checked = false;
                }

                fraApplyDonationTo.Visible = true;
            }
            else
                fraApplyDonationTo.Visible = false;

        }

        private void subBlockChoiceChanged(object sender, EventArgs e)
        {
            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strSQL = "";

                using (OleDbCommand cmdDB = new OleDbCommand())
                {
                    cmdDB.Connection = conDB;

                    int intI = 0;

                    try { intI = Convert.ToInt32(((ComboBox)sender).Name.Substring(14, ((ComboBox)sender).Name.Length - 14)); }
                    catch { intI = 1; }

                    //for (int intI = 1; intI <= 10; intI++)
                    //{
                    long lngBlockID = 0;
                    int intCapacity = 0;
                    int intWaitingList = 0;

                    try { lngBlockID = Convert.ToInt32(((clsCboItem)((ComboBox)(pagRegInfo.Controls.Find("cboBlockChoice" + intI.ToString(), true)[0])).SelectedItem).ID); }
                    catch { lngBlockID = 0; }

                    strSQL = "SELECT intCapacity " +
                            "FROM tblBlock " +
                            "WHERE lngBlockID=" + lngBlockID.ToString();

                    cmdDB.CommandText = strSQL;

                    try { intCapacity = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intCapacity = 0; }

                    strSQL = "SELECT COUNT(tblWaitingList.lngWaitID) AS intWaiting " +
                            "FROM tblWaitingList " +
                                "RIGHT JOIN tblBlock ON tblWaitingList.lngBlockID = tblBlock.lngBlockID " +
                            "WHERE tblBlock.lngBlockID=" + lngBlockID.ToString();

                    cmdDB.CommandText = strSQL;

                    try { intWaitingList = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intWaitingList = 0; }

                    subSetBlockFull(intI, lngBlockID, intCapacity, intWaitingList);
                    //                   }
                }

                conDB.Close();
            }
        }
    }
}