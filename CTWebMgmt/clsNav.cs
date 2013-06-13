using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt
{
    class clsNav
    {
        public static frmLogin objLogin;
        public static frmSwitchboard objSwitchboard;
        public static frmReferredBy objReferredBy;
        public static frmSystemSetup objSystemSetup;

        public static Donor.frmULGiftSettings objULGiftSettings;
        public static Donor.frmDLGifts objDLGifts;
        public static Donor.frmProcessGifts objProcessGifts;
        public static Donor.frmWebGiftDetails objWebGiftDetails;
        public static Donor.frmAutoGeneratePledgePmts objAutoGeneratePledgePmts;
        public static Donor.frmULGivingHistory objULGivingHistory;
        public static Donor.frmBatchDonorInvite objBatchDonorInvite;

        public static GGCC.frmUploadEvents objUploadEvents;
        public static GGCC.frmDLEvents objDLEvents;
        public static GGCC.frmProcessGGCCReg objProcessGGCCReg;
        public static GGCC.frmGGCCRegDetails objGGCCRegDetails;
        public static GGCC.frmAddIRForGGCCWebReg objAddIRForGGCCWebReg;
        public static GGCC.frmPickMatchGGCCWebRegRecord objPickMatchGGCCWebRegRecord;

        public static Ind.frmDLIndReg objDLIndReg;
        public static Ind.frmProcessIndReg objProcessIndReg;
        public static Ind.frmSpecialNeeds objSpecialNeeds;
        public static Ind.frmIndRegUL objIndRegUL;

        public static Admin.frmTestWebService objTestWebService;
        public static Admin.frmTextMessage objTextMessage;
        
        private static int intAddStdX = 25;
        private static int intAddStdY = 25;

        public static void subShowSwitchboard()
        {
            try
            {
                if (objSwitchboard == null) objSwitchboard = new frmSwitchboard();

                if (objSwitchboard.IsDisposed)
                {
                    objSwitchboard = null;
                    objSwitchboard = new frmSwitchboard();
                }

                objSwitchboard.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowSwitchboard", ex);
            }
        }

        public static void subShowUploadEvents(Form mdiParent)
        {
            try
            {
                if (objUploadEvents == null) objUploadEvents = new GGCC.frmUploadEvents();

                objUploadEvents.MdiParent = mdiParent;

                objUploadEvents.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowUploadEvents", ex);
            }
        }

        public static void subCloseSwitchboard()
        {
            try
            {
                objSwitchboard.Close();
                objSwitchboard.Dispose();
                objSwitchboard = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseSwitchboard", ex);
            }
        }

        public static void subShowLogin()
        {
            try
            {
                if (objLogin == null) objLogin = new frmLogin();

                objLogin.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowLogin", ex);
            }
        }

        public static void subCloseUploadEvents()
        {
            try
            {
                objUploadEvents.Close();
                objUploadEvents.Dispose();
                objUploadEvents = null;
            }
            catch (Exception ex)
            {

                clsErr.subLogErr("subCloseUploadEvents", ex);
            }
        }

        public static void subShowDLEvents(Form _mdiParent)
        {
            try
            {
                if (objDLEvents == null) objDLEvents = new GGCC.frmDLEvents();

                objDLEvents.MdiParent = _mdiParent;

                objDLEvents.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowDLEvents", ex);
            }
        }

        public static void subCloseDLEvents()
        {
            try
            {
                objDLEvents.Close();
                objDLEvents.Dispose();
                objDLEvents = null;
            }
            catch (Exception ex)
            {

                clsErr.subLogErr("subCloseDLEvents", ex);
            }
        }

        public static void subShowProcessGGCCReg(Form _mdiParent)
        {
            try
            {
                if (objProcessGGCCReg == null || objProcessGGCCReg.IsDisposed) objProcessGGCCReg = new GGCC.frmProcessGGCCReg();

                objProcessGGCCReg.MdiParent = _mdiParent;

                objProcessGGCCReg.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowProcessGGCCReg", ex);
            }
        }

        public static void subCloseProcessGGCCReg()
        {
            try
            {
                objProcessGGCCReg.Close();
                objProcessGGCCReg.Dispose();
                objProcessGGCCReg = null;
            }
            catch (Exception ex)
            {

                clsErr.subLogErr("subCloseProcessGGCCReg", ex);
            }
        }

        public static void subShowGGCCRegDetails(Form _mdiParent, long _lngGGCCRegWebID)
        {
            try
            {
                if (objGGCCRegDetails == null) objGGCCRegDetails = new GGCC.frmGGCCRegDetails();

                objGGCCRegDetails.lngGGCCRegWebID = _lngGGCCRegWebID;

                objGGCCRegDetails.MdiParent = _mdiParent;

                objGGCCRegDetails.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowGGCCRegDetails", ex);
            }
        }

        public static void subCloseGGCCRegDetails()
        {
            try
            {
                objGGCCRegDetails.Close();
                objGGCCRegDetails.Dispose();
                objGGCCRegDetails = null;
            }
            catch (Exception ex)
            {

                clsErr.subLogErr("subCloseGGCCRegDetails", ex);
            }
        }

        public static long fcnShowAddIRForGGCCWebReg(long _lngGGCCRegWebID)
        {
            long lngRes = 0;

            try
            {
                if (objAddIRForGGCCWebReg == null) objAddIRForGGCCWebReg = new GGCC.frmAddIRForGGCCWebReg(_lngGGCCRegWebID);

                //objAddIRForGGCCWebReg.MdiParent = _mdiParent;

                objAddIRForGGCCWebReg.ShowDialog();

                lngRes = objAddIRForGGCCWebReg.lngRecordID;

                objAddIRForGGCCWebReg.Dispose();
                objAddIRForGGCCWebReg = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowAddIRForGGCCWebReg", ex);
            }

            return lngRes;
        }

        public static long fcnShowPickMatchGGCCWebRegRecord(long _lngGGCCRegWebID)
        {
            long lngRes = 0;

            try
            {
                if (objPickMatchGGCCWebRegRecord == null) objPickMatchGGCCWebRegRecord = new GGCC.frmPickMatchGGCCWebRegRecord(_lngGGCCRegWebID);

                objPickMatchGGCCWebRegRecord.ShowDialog();

                lngRes = objPickMatchGGCCWebRegRecord.lngMatchedRecordID;

                objPickMatchGGCCWebRegRecord.Dispose();
                objPickMatchGGCCWebRegRecord = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowPickMatchGGCCWebRegRecord", ex);
            }

            return lngRes;
        }

        public static long fcnShowReconcileIR(string _strWebTbl, long _lngDBID, long _lngWebID)
        {
            long lngRes = 0;

            try
            {
                using (IRUtils.frmReconcileIR objReconcileIR = new global::CTWebMgmt.IRUtils.frmReconcileIR(_strWebTbl, _lngDBID, _lngWebID))
                {
                    objReconcileIR.ShowDialog();

                    lngRes = objReconcileIR.lngDBID;
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowPickMatchGGCCWebRegRecord", ex);
            }

            return lngRes;
        }

        public static void subShowULGiftSettings(Form _mdiParent)
        {
            try
            {
                if (objULGiftSettings == null) objULGiftSettings = new Donor.frmULGiftSettings();

                objULGiftSettings.MdiParent = _mdiParent;

                objULGiftSettings.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowULGiftSettings", ex);
            }
        }

        public static void subCloseULGiftSettings()
        {
            try
            {
                objULGiftSettings.Close();
                objULGiftSettings.Dispose();
                objULGiftSettings = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseULGiftSettings", ex);
            }
        }

        public static void subShowDLGifts(Form _mdiParent)
        {
            try
            {
                if (objDLGifts == null) objDLGifts = new Donor.frmDLGifts();

                objDLGifts.MdiParent = _mdiParent;

                objDLGifts.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowDLGifts", ex);
            }
        }

        public static void subCloseDLGifts()
        {
            try
            {
                objDLGifts.Close();
                objDLGifts.Dispose();
                objDLGifts = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseDLGifts", ex);
            }
        }

        public static void subShowProcessGifts(Form _mdiParent)
        {
            try
            {
                if (objProcessGifts == null) objProcessGifts = new Donor.frmProcessGifts();

                objProcessGifts.MdiParent = _mdiParent;

                objProcessGifts.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowProcessGifts", ex);
            }
        }

        public static void subCloseProcessGifts()
        {
            try
            {
                objProcessGifts.Close();
                objProcessGifts.Dispose();
                objProcessGifts = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseProcessGifts", ex);
            }
        }

        public static void subShowWebGiftDetails(Form _mdiParent, long _lngGiftWebID)
        {
            try
            {
                if (objWebGiftDetails == null) objWebGiftDetails = new Donor.frmWebGiftDetails(_lngGiftWebID);

                objWebGiftDetails.MdiParent = _mdiParent;
                objWebGiftDetails.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowWebGiftDetails", ex);
            }
        }

        public static void subCloseWebGiftDetails()
        {
            try
            {
                objWebGiftDetails.Close();
                objWebGiftDetails.Dispose();
                objWebGiftDetails = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseWebGiftDetails", ex);
            }
        }

        public static long fcnFindIR(string _strCompany, string _strFName, string _strLName, long _lngRecordID)
        {
            long lngRecordID = 0;

            lngRecordID = fcnFindIR(_strCompany, _strFName, _strLName, _lngRecordID, "", "", 0, "", "", "", "", "");

            return lngRecordID;
        }

        public static long fcnFindIR(string _strCompany, string _strFName, string _strLName, long _lngRecordID, string _strAddress, string _strCity, long _lngStateID, string _strZip, string _strHomePhone, string _strWorkPhone,string _strCellPhone, string _strEmail)
        {
            clsIR irToSearch = new clsIR(_lngRecordID, _lngStateID, _strFName, _strLName, _strCompany, _strAddress, _strCity, _strZip, _strHomePhone, _strWorkPhone, _strCellPhone, _strEmail);

            long lngRecordID = 0;

            using (frmFindIR objFindIR = new frmFindIR(irToSearch))
            {
                objFindIR.ShowDialog();

                lngRecordID = objFindIR.irToSearch.lngRecordID;
            }

            return lngRecordID;
        }

        public static long fcnFindIR(string _strCompany, string _strFName, string _strLName)
        {
            long lngRecordID = 0;

            lngRecordID = fcnFindIR(_strCompany, _strFName, _strLName, 0);

            return lngRecordID;
        }

        public static void subShowReferredBy(Form _mdiParent)
        {
            try
            {
                if (objReferredBy == null) objReferredBy = new frmReferredBy();

                objReferredBy.MdiParent = _mdiParent;

                objReferredBy.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowReferredBy", ex);
            }
        }

        public static void subCloseReferredBy()
        {
            try
            {
                objReferredBy.Close();
                objReferredBy.Dispose();
                objReferredBy = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseReferredBy", ex);
            }
        }

        public static void subShowTestWebService(Form _mdiParent)
        {
            try
            {
                if (objTestWebService == null || objTestWebService.IsDisposed) objTestWebService = new Admin.frmTestWebService();

                objTestWebService.MdiParent = _mdiParent;

                objTestWebService.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowTestWebService", ex);
            }
        }

        public static void subCloseTestWebService()
        {
            try
            {
                objTestWebService.Close();
                objTestWebService.Dispose();
                objTestWebService = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseTestWebService", ex);
            }
        }

        public static void subShowAutoGeneratePledgePmts(Form _mdiParent)
        {
            try
            {
                if (objAutoGeneratePledgePmts == null || objAutoGeneratePledgePmts.IsDisposed) objAutoGeneratePledgePmts = new Donor.frmAutoGeneratePledgePmts();

                objAutoGeneratePledgePmts.MdiParent = _mdiParent;

                objAutoGeneratePledgePmts.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowAutoGeneratePledgePmts", ex);
            }
        }

        public static void subCloseAutoGeneratePledgePmts()
        {
            try
            {
                objAutoGeneratePledgePmts.Close();
                objAutoGeneratePledgePmts.Dispose();
                objAutoGeneratePledgePmts = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseAutoGeneratePledgePmts", ex);
            }
        }

        public static void subShowULGivingHistory(Form _mdiParent)
        {
            try
            {
                if (objULGivingHistory == null || objULGivingHistory.IsDisposed) objULGivingHistory = new Donor.frmULGivingHistory();

                objULGivingHistory.MdiParent = _mdiParent;

                objULGivingHistory.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowULGivingHistory", ex);
            }
        }

        public static void subCloseULGivingHistory()
        {
            try
            {
                objULGivingHistory.Close();
                objULGivingHistory.Dispose();
                objULGivingHistory = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseULGivingHistory", ex);
            }
        }

        public static void subShowBatchDonorInvite(Form _mdiParent)
        {
            try
            {
                if (objBatchDonorInvite == null || objBatchDonorInvite.IsDisposed) objBatchDonorInvite = new Donor.frmBatchDonorInvite();

                objBatchDonorInvite.MdiParent = _mdiParent;

                objBatchDonorInvite.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowBatchDonorInvite", ex);
            }
        }

        public static void subCloseBatchDonorInvite()
        {
            try
            {
                objBatchDonorInvite.Close();
                objBatchDonorInvite.Dispose();
                objBatchDonorInvite = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseBatchDonorInvite", ex);
            }
        }

        public static void subShowSystemSetup(Form _mdiParent)
        {
            try
            {
                if (objSystemSetup == null || objSystemSetup.IsDisposed) objSystemSetup = new frmSystemSetup();

                objSystemSetup.MdiParent = _mdiParent;

                objSystemSetup.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowSystemSetup", ex);
            }
        }

        public static void subCloseSystemSetup()
        {
            try
            {
                objSystemSetup.Close();
                objSystemSetup.Dispose();
                objSystemSetup = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseSystemSetup", ex);
            }
        }

        public static void subShowDLIndReg(Form _mdiParent)
        {
            try
            {
                if (objDLIndReg == null || objDLIndReg.IsDisposed) objDLIndReg = new global::CTWebMgmt.Ind.frmDLIndReg();

                objDLIndReg.MdiParent = _mdiParent;

                objDLIndReg.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowDLIndReg", ex);
            }
        }

        public static void subCloseDLIndReg()
        {
            try
            {
                objDLIndReg.Close();
                objDLIndReg.Dispose();
                objDLIndReg = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseDLIndReg", ex);
            }
        }

        public static void subShowProcessIndReg(Form _mdiParent)
        {
            try
            {
                if (objProcessIndReg == null || objProcessIndReg.IsDisposed) objProcessIndReg = new global::CTWebMgmt.Ind.frmProcessIndReg();

                objProcessIndReg.MdiParent = _mdiParent;

                objProcessIndReg.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowProcessIndReg", ex);
            }
        }

        public static void subCloseProcessIndReg()
        {
            try
            {
                objProcessIndReg.Close();
                objProcessIndReg.Dispose();
                objProcessIndReg = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseProcessIndReg", ex);
            }
        }

        public static void subShowSpecNeeds(long _lngRegWebID)
        {
            try
            {
                if (objSpecialNeeds == null || objSpecialNeeds.IsDisposed) objSpecialNeeds = new global::CTWebMgmt.Ind.frmSpecialNeeds(_lngRegWebID);

                objSpecialNeeds.ShowDialog();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowSpecNeeds", ex);
            }
        }

        public static void subCloseSpecNeeds()
        {
            try
            {
                objSpecialNeeds.Close();
                objSpecialNeeds.Dispose();
                objSpecialNeeds = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseSpecNeeds", ex);
            }
        }

        public static void subShowTextMessage()
        {
            try
            {
                if (objTextMessage == null || objTextMessage.IsDisposed) objTextMessage = new global::CTWebMgmt.Admin.frmTextMessage();

                objTextMessage.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowTextMessage", ex);
            }
        }

        public static void subCloseTextMessage()
        {
            try
            {
                objTextMessage.Close();
                objTextMessage.Dispose();
                objTextMessage = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseTextMessage", ex);
            }
        }      

        public static void subShowIndRegUL(Form _mdiParent)
        {
            try
            {
                if (objIndRegUL == null || objIndRegUL.IsDisposed) objIndRegUL = new global::CTWebMgmt.Ind.frmIndRegUL();

                objIndRegUL.MdiParent = _mdiParent;

                objIndRegUL.Show();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subShowIndRegUL", ex);
            }
        }

        public static void subCloseIndRegUL()
        {
            try
            {
                objIndRegUL.Close();
                objIndRegUL.Dispose();
                objIndRegUL = null;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("subCloseIndRegUL", ex);
            }
        }
    }
}