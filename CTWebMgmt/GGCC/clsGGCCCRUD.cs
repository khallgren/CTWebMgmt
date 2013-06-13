using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CTWebMgmt.GGCC
{
    class clsGGCCCRUD
    {
        public static long fcnGetWebRegIR(frmGGCCRegDetails _objGGCCRegDetails)
        {
            //match existing or add new ir for received ggccregweb id.
            //return matched or new ir id.
            long lngRes = 0;

            try
            {
                long lngStateID = 0;

                try { lngStateID = ((clsCboItem)_objGGCCRegDetails.cboState.SelectedItem).ID; }
                catch { lngStateID = 0; }

                clsIR irToSearch = new clsIR(0, lngStateID, _objGGCCRegDetails.txtFName.Text, _objGGCCRegDetails.txtLName.Text, _objGGCCRegDetails.txtCompanyName.Text, _objGGCCRegDetails.txtAddress.Text, _objGGCCRegDetails.txtCity.Text, _objGGCCRegDetails.txtZip.Text, _objGGCCRegDetails.txtHomePhone.Text, _objGGCCRegDetails.txtWorkPhone.Text, _objGGCCRegDetails.txtCellPhone.Text, _objGGCCRegDetails.txtEMail.Text);

                //fill in ir details to pass to search screen
                irToSearch.blnGender = true;

                irToSearch.lngRecordWebID = _objGGCCRegDetails.lngRecordWebID;
                irToSearch.lngRecordID = _objGGCCRegDetails.lngRecordID;

                irToSearch.strEmail = _objGGCCRegDetails.txtEMail.Text;
                irToSearch.strHomePhone = _objGGCCRegDetails.txtHomePhone.Text;
                irToSearch.strCellPhone = _objGGCCRegDetails.txtCellPhone.Text;
                irToSearch.strZip = _objGGCCRegDetails.txtZip.Text;
                irToSearch.strCity = _objGGCCRegDetails.txtCity.Text;
                irToSearch.strAddress = _objGGCCRegDetails.txtAddress.Text;
                irToSearch.strLName = _objGGCCRegDetails.txtLName.Text;
                irToSearch.strFName = _objGGCCRegDetails.txtFName.Text;

                using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find camper record"))
                {
                    if (objFindIR.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (objFindIR.irToSearch.lngRecordID == 0)
                            return 0;
                        else
                        {
                            lngRes = objFindIR.irToSearch.lngRecordID;

                            if (!objFindIR.blnAddNew)
                            {
                                using (IRUtils.frmReconcileIR objReconcileIR = new global::CTWebMgmt.IRUtils.frmReconcileIR("tblWebRecordsGGCCReg", _objGGCCRegDetails.lngRecordID, _objGGCCRegDetails.lngRecordWebID))
                                {
                                    if (objReconcileIR.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                                        return 0;
                                    else
                                        _objGGCCRegDetails.lngRecordID = objReconcileIR.lngDBID;
                                }
                            }
                        }
                    }
                    else
                        return 0;
                }

                if (lngRes > 0)
                {
                    string strSQL = "";

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        strSQL = "UPDATE tblRecords " +
                                "SET blnCamper=-1 " +
                                "WHERE lngRecordID=" + lngRes.ToString();

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }
                        }

                        conDB.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("fcnGetWebRegIR", ex);
            }

            return lngRes;
        }

        public static long fcnAddWebReg(long _lngGGCCRegWebID, long _lngRecordID)
        {
            //add event reg based on web record--adding based on passed recordid, event id, and date registered
            //return new event reg id

            OleDbConnection objConn;
            OleDbCommand objCommand;

            string strSQL;

            long lngRes = 0;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                strSQL = "INSERT INTO tblGGCCRegistrations " +
                        "( blnCustomGGCCRegFlag1, blnCustomGGCCRegFlag2, blnCustomGGCCRegFlag3, blnCustomGGCCRegFlag4, blnCustomGGCCRegFlag5, blnCustomGGCCRegFlag6, blnCustomGGCCRegFlag7, blnCustomGGCCRegFlag8, blnCustomGGCCRegFlag9, blnCustomGGCCRegFlag10, blnCustomGGCCRegFlag11, blnCustomGGCCRegFlag12, blnCustomGGCCRegFlag13, blnCustomGGCCRegFlag14, blnCustomGGCCRegFlag15, " +
                            "lngGGCCID, lngRecordID, lngGGCCRegistrationWebID, " +
                            "dblDiscount, " +
                            "dteDateRegistered ) " +
                        "SELECT tblWebGGCCRegistrations.blnCustomGGCCRegFlag1, tblWebGGCCRegistrations.blnCustomGGCCRegFlag2, tblWebGGCCRegistrations.blnCustomGGCCRegFlag3, tblWebGGCCRegistrations.blnCustomGGCCRegFlag4, tblWebGGCCRegistrations.blnCustomGGCCRegFlag5, tblWebGGCCRegistrations.blnCustomGGCCRegFlag6, tblWebGGCCRegistrations.blnCustomGGCCRegFlag7, tblWebGGCCRegistrations.blnCustomGGCCRegFlag8, tblWebGGCCRegistrations.blnCustomGGCCRegFlag9, tblWebGGCCRegistrations.blnCustomGGCCRegFlag10, tblWebGGCCRegistrations.blnCustomGGCCRegFlag11, tblWebGGCCRegistrations.blnCustomGGCCRegFlag12, tblWebGGCCRegistrations.blnCustomGGCCRegFlag13, tblWebGGCCRegistrations.blnCustomGGCCRegFlag14, tblWebGGCCRegistrations.blnCustomGGCCRegFlag15, " +
                            "lngGGCCID, " + _lngRecordID + ", " + _lngGGCCRegWebID + ", " +
                            "dblDiscount, " +
                            "dteDateRegistered " +
                        "FROM tblWebGGCCRegistrations " +
                        "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID + ";";

                objCommand = new OleDbCommand(strSQL, objConn);

                if (objCommand.ExecuteNonQuery() > 0)
                {
                    objCommand.CommandText = "SELECT @@IDENTITY;";

                    lngRes = (long)(int)objCommand.ExecuteScalar();
                }
                else
                    lngRes = 0;

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("cslGGCCCRUD.fcnAddWebReg()", ex);
            }

            return lngRes;
        }

        public static void subAddWebRegAtt(long _lngGGCCRegWebID, long _lngGGCCRegID)
        {
            //add attendees
            //add tuition transaction
            //update transaction id in reg record

            OleDbConnection objConn;
            OleDbCommand objCommand;

            string strSQL;

            long lngTransID = 0;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                strSQL = "INSERT INTO tblGGCCRegAttendees " +
                        "( intGender, " +
                            "lngGGCCRegistrationID, lngGGCCAttendeeStatsID, lngGGCCID, lngGGCCRegHousingID, lngGuestTypeID, lngRateUsageID, lngGGCCHousingID, lngRecordID, " +
                            "curRate, " +
                            "dteDOB, " +
                            "strFName, strLName, strNotes, strBuddyRequest ) " +
                        "SELECT intGender, " +
                            _lngGGCCRegID + ", lngGGCCAttendeeStatsID, lngGGCCID, lngGGCCRegHousingID, lngGuestTypeID, lngRateUsageID, lngGGCCHousingID, lngRecordID, " +
                            "curRate, " +
                            "dteDOB, " +
                            "strFName, strLName, strNotes, strBuddyRequest " +
                        "FROM tblWebGGCCRegAttendees " +
                        "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID + ";";

                objCommand = new OleDbCommand(strSQL, objConn);

                //add tuition transaction, update id if attendees are being added
                if (objCommand.ExecuteNonQuery() > 0)
                {
                    objConn.Close();

                    objCommand.Dispose();
                    objConn.Dispose();

                    lngTransID = fcnRecalcTuition(_lngGGCCRegID);
                }
                else
                {
                    objConn.Close();

                    objCommand.Dispose();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.subAddWebRegAtt", ex);
            }
        }

        public static bool fcnAddWebRegPmt(int _intHandle, long _lngGGCCRegWebID, long _lngGGCCRegID, long _lngRecordID)
        {
            //add payment for tuition
            string strSQL;
            string strCCNum = "";
            string strExpDate = "";
            string strZip = "";
            string strCVV2 = "";
            string strBillName = "";
            string strAuth = "";
            string strPmtType = "";
            string strPNRef = "";
            string strEPSTransID="";
           string strEPSApprovalNumber="";
           string strEPSValidationCode="";
           string strEPSPmtAcctID = "";

            string strXCAlias = "";
            string strXCTransID = "";
            string strVancoCustRef = "";
            string strVancoPmtMethID = "";
            string strErr = "";
            string strXChargePath = "";

            double dblAmt = 0;

            clsGlobalEnum.conLIVECHARGE intLiveChargeMethod = clsGlobalEnum.conLIVECHARGE.None;

            bool blnRes = false;
            bool blnChargeRes = false;

            try
            {
                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {

                    objConn.Open();

                    //get live charge setting from db
                    strSQL = "SELECT lngLiveCharge, strXChargePath " +
                            "FROM tblCampDefaults;";

                    using (OleDbCommand objCommand = new OleDbCommand(strSQL, objConn))
                    {
                        using (OleDbDataReader drDef = objCommand.ExecuteReader())
                        {
                            if (drDef.Read())
                            {
                                intLiveChargeMethod = (clsGlobalEnum.conLIVECHARGE)drDef["lngLiveCharge"];
                                strXChargePath = Convert.ToString(drDef["strXChargePath"]);
                            }

                            drDef.Close();
                        }

                        //collect trans details                            
                        strSQL = "SELECT tblWebGGCCRegistrations.curDeposit, " +
                                    "[tblWebRecordsGGCCReg].[strFirstName] & \" \" & [tblWebRecordsGGCCReg].[strLastCoName] AS strBillName, tblWebRecordsGGCCReg.strZip, tblWebGGCCRegistrations.strCardNum, tblWebGGCCRegistrations.strCVV2, tblWebGGCCRegistrations.strCCExp, tblWebGGCCRegistrations.strPaymentType, tblWebGGCCRegistrations.strPNRef, tblWebGGCCRegistrations.strVancoCustRef, tblWebGGCCRegistrations.strVancoPmtMethID, tblWebGGCCRegistrations.strXCAlias, tblWebGGCCRegistrations.strXCTransID, tblWebGGCCRegistrations.strEPSTransID, tblWebGGCCRegistrations.strEPSApprovalNumber, tblWebGGCCRegistrations.strEPSValidationCode, tblWebGGCCRegistrations.strEPSPmtAcctID " +
                                "FROM tblWebGGCCRegistrations " +
                                    "INNER JOIN tblWebRecordsGGCCReg ON tblWebGGCCRegistrations.lngRecordWebID = tblWebRecordsGGCCReg.lngRecordWebID " +
                                "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + _lngGGCCRegWebID + ";";

                        objCommand.CommandText = strSQL;

                        using (OleDbDataReader drTrans = objCommand.ExecuteReader())
                        {
                            if (drTrans.Read())
                            {
                                strPmtType = drTrans["strPaymentType"].ToString();
                                strBillName = drTrans["strBillName"].ToString();
                                strCCNum = drTrans["strCardNum"].ToString();
                                strExpDate = drTrans["strCCExp"].ToString();
                                strZip = drTrans["strZip"].ToString();
                                strCVV2 = drTrans["strCVV2"].ToString();
                                dblAmt = double.Parse(drTrans["curDeposit"].ToString());

                                try { strPNRef = Convert.ToString(drTrans["strPNRef"]); }
                                catch { strPNRef = ""; }

                                try { strEPSTransID = Convert.ToString(drTrans["strEPSTransID"]); }
                                catch { strEPSTransID = ""; }

                                try { strEPSApprovalNumber = Convert.ToString(drTrans["strEPSApprovalNumber"]); }
                                catch { strEPSApprovalNumber = ""; }

                                try { strEPSValidationCode = Convert.ToString(drTrans["strEPSValidationCode"]); }
                                catch { strEPSValidationCode = ""; }

                                try { strEPSPmtAcctID = Convert.ToString(drTrans["strEPSPmtAcctID"]); }
                                catch { strEPSPmtAcctID = ""; }

                                strVancoCustRef = Convert.ToString(drTrans["strVancoCustRef"]);
                                strVancoPmtMethID = Convert.ToString(drTrans["strVancoPmtMethID"]);

                                try { strXCAlias = Convert.ToString(drTrans["strXCAlias"]); }
                                catch { strXCAlias = ""; }

                                try { strXCTransID = Convert.ToString(drTrans["strXCTransID"]); }
                                catch { strXCTransID = ""; }
                            }

                            drTrans.Close();
                        }
                        if (dblAmt > 0)
                        {
                            if (strPmtType != "EFT")
                            {
                                switch (intLiveChargeMethod)
                                {
                                    case clsGlobalEnum.conLIVECHARGE.None:
                                        blnChargeRes = false;
                                        break;

                                    //skipping XCharge CC processing...this was done at registration time
                                    case clsGlobalEnum.conLIVECHARGE.XCharge:
                                        //    blnChargeRes = clsLiveCharge.fcnLiveXCharge(_intHandle, strCCNum, strExpDate, strZip, "", strCVV2, strXChargePath, dblAmt, _lngGGCCRegID, ref strAuth);
                                        blnChargeRes = true;
                                        break;

                                    //skipping CashLinq CC processing...this was done at registration time
                                    case clsGlobalEnum.conLIVECHARGE.CashLinq:
                                        //  blnChargeRes = clsLiveCharge.fcnLiveCashLinqFromAuth(strBillName, strCCNum, dblAmt, _lngGGCCRegID, _lngRecordID, ref strAuth, ref strPNRef);
                                        blnChargeRes = true;
                                        break;

                                    case clsGlobalEnum.conLIVECHARGE.EPS:
                                        blnChargeRes = true;
                                        break;

                                    case clsGlobalEnum.conLIVECHARGE.Vanco:
                                        blnChargeRes = false;
                                        break;
                                }

                                if (intLiveChargeMethod != clsGlobalEnum.conLIVECHARGE.None)
                                {
                                    if (blnChargeRes)
                                    {
                                        if (strCCNum.Length > 4)
                                            strCCNum = strCCNum.Substring(strCCNum.Length - 4, 4);
                                    }
                                    else
                                        System.Windows.Forms.MessageBox.Show("There was an error processing the credit card.\n" + strErr + "\nThe registration will be added but the payment must be processed manually.");
                                }
                            }
                            
                            strSQL = "INSERT INTO tblTransactions " +
                                    "( blnMarkedForCC, " +
                                        "lngTransTypeID, lngPaymentTypeID, lngBillStateID, lngGGCCRegistrationID, lngProgramTypeID, lngRecordID, lngUserID, " +
                                        "curPayment, " +
                                        "dteDateAdded, " +
                                        "strBankName, strAcctNum, strRoutingNum, strBillName, strBillAddress, strBillCity, strBillZip, strBillPhone, strCCNumber, strCCValCode, strCCExpDate, strTransactionDesc, strAuthNumber, strPNRef, strXCAlias, strXCTransID, strEPSTransID, strEPSApprovalNumber, strEPSValidationCode, strEPSPmtAcctID ) " +
                                    "SELECT " + blnChargeRes + ", " +
                                        "8 AS lngTransTypeID, IIf([tblWebGGCCRegistrations].[strPaymentType]=\"EFT\", 11, 2) AS lngPaymentTypeID, tblWebRecordsGGCCReg.lngStateID, " + _lngGGCCRegID + " AS lngGGCCRegistrationID, tblGGCC.lngProgramTypeID, tblGGCCRegistrations.lngRecordID, " + CTWebMgmt.lngUserID + " AS lngUserID, " +
                                        "tblWebGGCCRegistrations.curDeposit, " +
                                        "Now() AS dteDateAdded, " +
                                        "tblWebGGCCRegistrations.strBankName, tblWebGGCCRegistrations.strAcctNum, tblWebGGCCRegistrations.strRoutingNum, [tblWebRecordsGGCCReg].[strFirstName] & \" \" & [tblWebRecordsGGCCReg].[strLastCoName] AS strBillName, tblWebRecordsGGCCReg.strAddress, tblWebRecordsGGCCReg.strCity, tblWebRecordsGGCCReg.strZip, tblWebRecordsGGCCReg.strHomePhone, \"" + strCCNum + "\" AS strCardNum, tblWebGGCCRegistrations.strCVV2, tblWebGGCCRegistrations.strCCExp AS strCCExpDate, \"Payment for Online Group Event Registration\" AS strTransactionDesc, \"" + strAuth + "\" AS strAuthNumber, \"" + strPNRef + "\" AS  strPNRef, \"" + strXCAlias + "\" AS strXCAlias, \"" + strXCTransID + "\" AS strXCTransID, \"" + strEPSTransID + "\" AS strEPSTransID, \"" + strEPSApprovalNumber + "\" AS strEPSApprovalNumber, \"" + strEPSValidationCode + "\" AS strEPSValidationCode, \"" + strEPSPmtAcctID + "\" AS strEPSPmtAcctID " +
                                    "FROM ((tblWebGGCCRegistrations " +
                                        "INNER JOIN tblGGCCRegistrations ON tblWebGGCCRegistrations.lngGGCCRegistrationWebID = tblGGCCRegistrations.lngGGCCRegistrationWebID) " +
                                        "INNER JOIN tblWebRecordsGGCCReg ON tblWebGGCCRegistrations.lngRecordWebID = tblWebRecordsGGCCReg.lngRecordWebID) " +
                                        "INNER JOIN tblGGCC ON tblWebGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID " +
                                    "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + _lngGGCCRegWebID.ToString();

                            objCommand.CommandText = strSQL;

                            try { objCommand.ExecuteNonQuery(); }
                            catch { }

                            clsIRCRUD.subUpdateBillingInfoCC(_lngRecordID, strPNRef, strCCNum, strXCAlias, strEPSPmtAcctID);
                        }
                    }

                    objConn.Close();
                }

                blnRes = true;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.subAddWebRegPmt", ex);
            }

            return blnRes;
        }

        public static void subAddWebRegDiscount(long _lngGGCCRegWebID, long _lngGGCCRegID)
        {
            //add discount if applicable
            OleDbConnection objConn;
            OleDbCommand objCommand;

            string strSQL;

            try
            {
                decimal curDiscountAmt = fcnGetDiscountAmt(_lngGGCCRegID);

                if (curDiscountAmt > 0)
                {
                    objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                    objConn.Open();

                    strSQL = "INSERT INTO tblTransactions " +
                            "( lngRecordID, lngTransTypeID, lngPaymentTypeID, lngGGCCRegistrationID, lngUserID, " +
                                "curPayment, " +
                                "dteDateAdded, " +
                                "strTransactionDesc ) " +
                            "SELECT tblGGCCRegistrations.lngRecordID, 69 AS lngTransTypeID, 6 AS lngPaymentTypeID, " + _lngGGCCRegID + " AS lngRegistrationID, " + CTWebMgmt.lngUserID + " AS lngUserID, " +
                                curDiscountAmt + " AS curPayment, " +
                                "Now() AS dteDateAdded, " +
                                "\"Event Registration Discount\" AS strTransactionDesc " +
                            "FROM tblGGCCRegistrations " +
                                "INNER JOIN tblWebGGCCRegistrations ON tblGGCCRegistrations.lngGGCCRegistrationWebID = tblWebGGCCRegistrations.lngGGCCRegistrationWebID;";

                    objCommand = new OleDbCommand(strSQL, objConn);

                    objCommand.ExecuteNonQuery();

                    objConn.Close();

                    objCommand.Dispose();
                    objConn.Dispose();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.subAddWebRegPmt", ex);
            }
        }

        private static decimal fcnGetDiscountAmt(long _lngGGCCRegID)
        {
            //return discount amount for a specific web registration
            OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand cmdDB;
            OleDbDataReader drCharges;

            string strSQL;

            decimal curRes = 0;

            try
            {
                conDB.Open();

                strSQL = "SELECT Sum(tblTransactions.curCharge) AS curTotCharge, " +
                            "tblGGCCRegistrations.dblDiscount " +
                        "FROM tblTransactions " +
                            "INNER JOIN tblGGCCRegistrations ON tblTransactions.lngGGCCRegistrationID = tblGGCCRegistrations.lngGGCCRegistrationID " +
                        "WHERE tblTransactions.lngGGCCRegistrationID=" + _lngGGCCRegID + " " +
                        "GROUP BY tblGGCCRegistrations.dblDiscount;";

                cmdDB = new OleDbCommand(strSQL, conDB);

                drCharges = cmdDB.ExecuteReader();

                if (drCharges.Read())
                    curRes = (decimal.Parse(drCharges["dblDiscount"].ToString()) / 100) * decimal.Parse(drCharges["curTotCharge"].ToString());

                drCharges.Close();

                conDB.Close();

                drCharges.Dispose();
                cmdDB.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("fcnGetDiscountAmt", ex);
            }

            conDB.Dispose();

            return curRes;
        }

        private static long fcnRecalcTuition(long _lngGGCCRegID)
        {
            //calculate and update tuition charge for passed event reg id
            //update trans id field in reg record
            //return trans id of new/updated tuition charge

            OleDbConnection objConn;
            OleDbCommand objCommand;
            OleDbDataReader drAtt;

            string strSQL;

            double dblTuition = 0;

            long lngTransID = 0;
            long lngRes = 0;

            int intNights = 0;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
                objConn.Open();

                //open rs of charges, sum daily vs non-daily rates
                strSQL = "SELECT tblAdvRateTypes.blnDaily, " +
                            "tblGGCC.dteStartDate, tblGGCC.dteEndDate, " +
                            "Sum(tblGGCCRegAttendees.curRate) AS curSumRate " +
                        "FROM (((tblGGCC " +
                            "INNER JOIN tblGGCCRegistrations ON tblGGCC.lngGGCCID = tblGGCCRegistrations.lngGGCCID) " +
                            "INNER JOIN tblGGCCRegAttendees ON tblGGCCRegistrations.lngGGCCRegistrationID = tblGGCCRegAttendees.lngGGCCRegistrationID) " +
                            "INNER JOIN tblnkGGCCAttendeeStats ON tblGGCCRegAttendees.lngGGCCAttendeeStatsID = tblnkGGCCAttendeeStats.lngGGCCAttendeeStatsID) " +
                            "INNER JOIN tblAdvRateTypes ON tblnkGGCCAttendeeStats.lngAdvRateTypeID = tblAdvRateTypes.lngAdvRateTypeID " +
                        "WHERE tblGGCCRegistrations.lngGGCCRegistrationID=" + _lngGGCCRegID + " " +
                        "GROUP BY tblAdvRateTypes.blnDaily, tblGGCC.dteStartDate, tblGGCC.dteEndDate;";

                objCommand = new OleDbCommand(strSQL, objConn);

                drAtt = objCommand.ExecuteReader();

                while (drAtt.Read())
                {
                    intNights = ((TimeSpan)(DateTime.Parse(drAtt["dteEndDate"].ToString()) - DateTime.Parse(drAtt["dteStartDate"].ToString()))).Days;

                    if (bool.Parse(drAtt["blnDaily"].ToString()))
                        //add tuition for daily adv rate types--multiply rate by number of days
                        dblTuition += double.Parse(drAtt["curSumRate"].ToString()) * intNights;
                    else
                        //add tuition for regular rate typs
                        dblTuition += double.Parse(drAtt["curSumRate"].ToString());
                }

                drAtt.Close();

                if (dblTuition > 0)
                {
                    //check if trans already exists for event reg
                    strSQL = "SELECT lngTransactionID " +
                            "FROM tblGGCCRegistrations " +
                            "WHERE lngGGCCRegistrationID=" + _lngGGCCRegID + ";";

                    objCommand.CommandText = strSQL;

                    lngTransID = (long)(int)objCommand.ExecuteScalar();

                    if (lngTransID > 0)
                    {
                        strSQL = "UPDATE tblTransactions " +
                                "SET curCharge=" + dblTuition + " " +
                                "WHERE lngTransactionID=" + lngTransID + ";";

                        objCommand.CommandText = strSQL;

                        objCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        strSQL = "INSERT INTO tblTransactions " +
                                "( lngRecordID, lngTransTypeID, lngProgramTypeID, lngGGCCRegistrationID, lngUserID, " +
                                    "curCharge, " +
                                    "dteDateAdded, " +
                                    "strTransactionDesc ) " +
                                "SELECT tblGGCCRegistrations.lngRecordID, 49, tblGGCC.lngProgramTypeID, tblGGCCRegistrations.lngGGCCRegistrationID, " + CTWebMgmt.lngUserID + ", " +
                                    dblTuition + ", " +
                                    "Now(), " +
                                    "\"Event Registration Tuition\" " +
                                "FROM tblGGCCRegistrations " +
                                    "INNER JOIN tblGGCC ON tblGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID " +
                                "WHERE tblGGCCRegistrations.lngGGCCRegistrationID=" + _lngGGCCRegID + ";";

                        objCommand.CommandText = strSQL;

                        objCommand.ExecuteNonQuery();

                        objCommand.CommandText = "SELECT @@IDENTITY;";

                        lngTransID = (long)(int)objCommand.ExecuteScalar();

                        //adjust registration to hold trans id
                        strSQL = "UPDATE tblGGCCRegistrations " +
                                "SET lngTransactionID=" + lngTransID + " " +
                                "WHERE lngGGCCRegistrationID=" + _lngGGCCRegID + ";";

                        objCommand.CommandText = strSQL;

                        objCommand.ExecuteNonQuery();
                    }
                    lngRes = lngTransID;
                }
                objConn.Close();

                drAtt.Dispose();
                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.fcnRecalcTuition", ex);
            }

            return lngRes;
        }

        public static void subAddWebRegAct(long _lngGGCCRegWebID, long _lngGGCCRegID)
        {
            //add actvities for passed web reg id to local event reg id

            OleDbConnection objConn;
            OleDbCommand objCommand;
            OleDbDataReader drAct;

            string strTransDesc;
            string strSQL;
            string strWHERE;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                //add activities 
                strSQL = "INSERT INTO tblGGCCRegActivities " +
                        "( intParticipants, " +
                            "lngGGCCActivityID, lngGGCCPackageID, lngGGCCRegistrationID ) " +
                        "SELECT intParticipants, " +
                            "lngGGCCActivityID, lngGGCCPackageID, " + _lngGGCCRegID + " " +
                        "FROM tblWebGGCCRegActivities " +
                        "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID + ";";

                objCommand = new OleDbCommand(strSQL, objConn);

                if (objCommand.ExecuteNonQuery() > 0)
                {
                    //add transactions
                    //get charges to add
                    strSQL = "SELECT Count(tblGGCCRegActivities.lngGGCCActivityID) AS lngGGCCActivityID, tblGGCCRegActivities.lngGGCCPackageID, tblGGCCRegistrations.lngRecordID, " +
                                "[tblGGCCPackages].[curPackageCost]*[tblGGCCRegActivities].[intParticipants] AS curCharge, " +
                                "tblGGCCPackages.strPackageName AS strActPkgName " +
                            "FROM (tblGGCCRegActivities " +
                                "INNER JOIN tblGGCCPackages ON tblGGCCRegActivities.lngGGCCPackageID = tblGGCCPackages.lngGGCCPackageID) " +
                                "INNER JOIN tblGGCCRegistrations ON tblGGCCRegActivities.lngGGCCRegistrationID = tblGGCCRegistrations.lngGGCCRegistrationID " +
                            "WHERE tblGGCCRegActivities.lngGGCCRegistrationID=" + _lngGGCCRegID + " " +
                            "GROUP BY tblGGCCRegActivities.lngGGCCPackageID, tblGGCCRegistrations.lngRecordID, " +
                                "[tblGGCCPackages].[curPackageCost]*[tblGGCCRegActivities].[intParticipants], " +
                                "tblGGCCPackages.strPackageName " +
                            "HAVING (([tblGGCCPackages].[curPackageCost]*[tblGGCCRegActivities].[intParticipants])>0) AND " +
                                "tblGGCCRegActivities.lngGGCCPackageID>0 " +
                            "UNION " +
                            "SELECT tblGGCCRegActivities.lngGGCCActivityID, tblGGCCRegActivities.lngGGCCPackageID, tblGGCCRegistrations.lngRecordID, " +
                                "[tblGGCCActivities].[curChargePerPerson]*[tblGGCCRegActivities].[intParticipants] AS curCharge, " +
                                "tlkpGGCCActivities.strActivityName AS strActPkgName " +
                            "FROM ((tblGGCCRegActivities " +
                                "INNER JOIN tblGGCCActivities ON tblGGCCActivities.lngGGCCActivityID = tblGGCCRegActivities.lngGGCCActivityID) " +
                                "INNER JOIN tblGGCCRegistrations ON tblGGCCRegActivities.lngGGCCRegistrationID = tblGGCCRegistrations.lngGGCCRegistrationID) " +
                                "INNER JOIN tlkpGGCCActivities ON tblGGCCActivities.lngActivityID = tlkpGGCCActivities.lngActivityID " +
                            "WHERE tblGGCCRegActivities.lngGGCCPackageID=0 AND " +
                                "tblGGCCRegActivities.lngGGCCRegistrationID=" + _lngGGCCRegID + " AND " +
                                "(([tblGGCCActivities].[curChargePerPerson]*[tblGGCCRegActivities].[intParticipants])>0);";

                    objCommand.CommandText = strSQL;

                    drAct = objCommand.ExecuteReader();

                    while (drAct.Read())
                    {
                        //add transaction

                        if ((long)(int)drAct["lngGGCCPackageID"] > 0)
                        {
                            strTransDesc = "Activity package charge - " + drAct["strActPkgName"].ToString();
                            strWHERE = "WHERE tblGGCCRegActivities.lngGGCCPackageID=" + drAct["lngGGCCPackageID"];
                        }
                        else
                        {
                            strTransDesc = "Activity Charge - " + drAct["strActPkgName"].ToString();
                            strWHERE = "WHERE lngGGCCActivityID=" + drAct["lngGGCCActivityID"];
                        }

                        strSQL = "INSERT INTO tblTransactions " +
                                 "( lngRecordID, lngTransTypeID, lngGGCCRegistrationID, lngUserID, " +
                                     "curCharge, " +
                                     "dteDateAdded, " +
                                     "strTransactionDesc ) " +
                                 "SELECT " + drAct["lngRecordID"] + ", 37, " + _lngGGCCRegID + ", " + CTWebMgmt.lngUserID + ", " +
                                     drAct["curCharge"] + ", " +
                                     "Now(), " +
                                     "\"" + strTransDesc + "\";";

                        OleDbCommand cmdTrans = new OleDbCommand(strSQL, objConn);

                        cmdTrans.ExecuteNonQuery();

                        cmdTrans.CommandText = "SELECT @@IDENTITY;";

                        //update id in reg act record(s)
                        strSQL = "UPDATE tblGGCCRegActivities " +
                                "SET lngTransactionID=" + cmdTrans.ExecuteScalar() + " " +
                                strWHERE + ";";

                        cmdTrans.CommandText = strSQL;

                        cmdTrans.ExecuteNonQuery();

                        cmdTrans.Dispose();
                    }
                    drAct.Close();
                }
                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.subAddWebRegAct", ex);
            }
        }

        public static void subAddWebRegHousingRequests(long _lngGGCCRegWebID, long _lngGGCCRegID)
        {
            //add housing requests for passed web reg id to local event reg id
            OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand objCommand;

            string strSQL;

            try
            {
                objConn.Open();

                //add housing requests
                strSQL = "INSERT INTO tblGGCCRegHousingRequests " +
                        "( lngHousingID, lngGGCCRegistrationID, lngCount, " +
                            "curCharge ) " +
                        "SELECT tlkpHousingName.lngHousingID, " + _lngGGCCRegID + ", tblWebGGCCRegHousingRequests.lngCount, " +
                            "tblWebGGCCRegHousingRequests.curCharge " +
                        "FROM tblWebGGCCRegHousingRequests " +
                            "INNER JOIN tlkpHousingName ON tblWebGGCCRegHousingRequests.lngHousingID = tlkpHousingName.lngHousingID " +
                        "WHERE tblWebGGCCRegHousingRequests.lngGGCCRegistrationWebID=" + _lngGGCCRegWebID;

                objCommand = new OleDbCommand(strSQL, objConn);

                objCommand.ExecuteNonQuery();

                objCommand.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.subAddWebRegHousingRequests", ex);
            }
            finally
            {
                objConn.Close();
                objConn.Dispose();
            }
        }

        public static void subDeleteProcessedReg(long _lngGGCCRegWebID)
        {
            //remove processed registration from queue
            string strSQL;

            try
            {
                using (OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {

                    objConn.Open();

                    //get contact id
                    strSQL = "SELECT lngRecordWebID " +
                            "FROM tblWebGGCCRegistrations " +
                            "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID.ToString();

                    using (OleDbCommand objCommand = new OleDbCommand(strSQL, objConn))
                    {

                        long lngRecordWebID = 0;

                        try
                        {
                            long.TryParse(objCommand.ExecuteScalar().ToString(), out lngRecordWebID);
                        }
                        catch { }

                        //delete activities
                        strSQL = "DELETE * " +
                                "FROM tblWebGGCCRegActivities " +
                                "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID;

                        objCommand.CommandText = strSQL;

                        try { objCommand.ExecuteNonQuery(); }
                        catch { }

                        //delete attendees
                        strSQL = "DELETE * " +
                                "FROM tblWebGGCCRegAttendees " +
                                "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID;

                        objCommand.CommandText = strSQL;

                        try { objCommand.ExecuteNonQuery(); }
                        catch { }

                        strSQL = "SELECT COUNT(lngGGCCRegistrationWebID) AS intRegCount " +
                                "FROM tblWebGGCCRegistrations " +
                                "WHERE lngRecordWebID=" + lngRecordWebID + " AND " +
                                    "lngGGCCRegistrationWebID<>" + _lngGGCCRegWebID.ToString();

                        objCommand.CommandText = strSQL;

                        try
                        {
                            if (long.Parse(objCommand.ExecuteScalar().ToString()) == 0)
                            {
                                //delete contact
                                strSQL = "DELETE * " +
                                        "FROM tblWebRecordsGGCCReg " +
                                        "WHERE lngRecordWebID=" + lngRecordWebID.ToString();

                                objCommand.CommandText = strSQL;
                                objCommand.ExecuteNonQuery();
                            }
                        }
                        catch { }

                        //delete registration
                        strSQL = "DELETE * " +
                                "FROM tblWebGGCCRegistrations " +
                                "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID.ToString();

                        objCommand.CommandText = strSQL;
                        try { objCommand.ExecuteNonQuery(); }
                        catch { }
                    }

                    objConn.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("clsGGCCCRUD.subMarkWebRegProcessed", ex);
            }
        }

        public static int fcnGGCCRegWebCount()
        {
            //get current count of web registrations

            OleDbConnection objConn;
            OleDbCommand objCommand;

            string strSQL;

            int intRes = 0;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                strSQL = "SELECT Count(lngGGCCRegistrationWebID) AS intRegCount " +
                        "FROM tblWebGGCCRegistrations;";

                objCommand = new OleDbCommand(strSQL, objConn);

                int.TryParse(objCommand.ExecuteScalar().ToString(), out intRes);

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("cslGGCCCRUD.fcnGGCCRegWebCount()", ex);
            }

            return intRes;
        }

        public static clsGlobalEnum.conCheckExistingRegRes fcnGGCCRegWebExists(long _lngGGCCRegWebID)
        {
            //check to see if passed ggccregwebid exists in processed event registrations
            //return false if it doesn't exist
            //show reconcile form if it exists
            try
            {
                OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
                OleDbCommand objCommand;

                string strSQL;

                long lngGGCCRegID = 0;

                objConn.Open();

                strSQL = "SELECT lngGGCCRegistrationID " +
                        "FROM tblGGCCRegistrations " +
                        "WHERE lngGGCCRegistrationWebID=" + _lngGGCCRegWebID;

                objCommand = new OleDbCommand(strSQL, objConn);

                if (objCommand.ExecuteScalar() != null) long.TryParse(objCommand.ExecuteScalar().ToString(), out lngGGCCRegID);

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();

                if (lngGGCCRegID > 0)
                {
                    frmReconcileWebGGCCReg objReconcileWebGGCCReg = new frmReconcileWebGGCCReg(_lngGGCCRegWebID);

                    clsGlobalEnum.conCheckExistingRegRes lngRes;

                    objReconcileWebGGCCReg.ShowDialog();

                    lngRes = objReconcileWebGGCCReg.lngRes;

                    objReconcileWebGGCCReg.Dispose();

                    return lngRes;
                }
                else
                    return clsGlobalEnum.conCheckExistingRegRes.DoesntExist;

            }
            catch (Exception ex)
            {
                clsErr.subLogErr("fcnGGCCRegWebExists", ex);
            }
            return clsGlobalEnum.conCheckExistingRegRes.Cancel;
        }
    }
}