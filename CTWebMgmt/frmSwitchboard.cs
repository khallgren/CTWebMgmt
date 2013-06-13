using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CTWebMgmt
{
    public partial class frmSwitchboard : Form
    {
        public frmSwitchboard()
        {
            InitializeComponent();
        }

        private void frmSwitchboard_Load(object sender, EventArgs e)
        {
            mnuUpdateVersion.Text = "Update Version " + CTWebMgmt.strUpdateVersion;
        }

        private void tskUploadGGCC_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowUploadEvents(this);
        }

        private void tskDLGGCCReg_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowDLEvents(clsNav.objSwitchboard);
        }

        private void tskProcessGGCCReg_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowProcessGGCCReg(clsNav.objSwitchboard);
        }

        private void tskULGiftSettings_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowULGiftSettings(clsNav.objSwitchboard);
        }

        private void tskDLGifts_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowDLGifts(clsNav.objSwitchboard);
        }

        private void tskProcessGifts_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowProcessGifts(clsNav.objSwitchboard);
        }

        private void tskReferredByOptions_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowReferredBy(clsNav.objSwitchboard);
        }

        private void tskTestWebService_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowTestWebService(clsNav.objSwitchboard);
        }

        private void grpLogout_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subCloseSwitchboard();
        }

        private void mnuAutoGeneratePledgePmts_Click(object sender, EventArgs e)
        {
            clsNav.subShowAutoGeneratePledgePmts(clsNav.objSwitchboard);
        }

        private void tskULGivingHistory_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowULGivingHistory(clsNav.objSwitchboard);
        }

        private void mnuSendBatchInvitations_Click(object sender, EventArgs e)
        {
            clsNav.subShowBatchDonorInvite(clsNav.objSwitchboard);
        }

        private void tskDLIndReg_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowDLIndReg(clsNav.objSwitchboard);
        }

        private void tskProcessIndReg_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowProcessIndReg(clsNav.objSwitchboard);
        }

        private void tskULBlocks_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowIndRegUL(clsNav.objSwitchboard);
        }

        private void tskGoogleCal_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            frmGoogleCal objGoogleCal = new frmGoogleCal();

            objGoogleCal.MdiParent = this;

            objGoogleCal.Show();
        }

        private void frmSwitchboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsNav.subShowLogin();
        }

        private void tskSyncTrans_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            Ind.frmSyncTrans objSyncTrans = new global::CTWebMgmt.Ind.frmSyncTrans();

            objSyncTrans.MdiParent = this;

            objSyncTrans.Show();
        }

        private void mnuDiscounts_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (Ind.Setup.frmDiscounts objDiscounts = new global::CTWebMgmt.Ind.Setup.frmDiscounts())
            {
                objDiscounts.ShowDialog();
            }
        }

        private void tskMinDep_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (Ind.Setup.frmMinDep objMinDep = new global::CTWebMgmt.Ind.Setup.frmMinDep())
                objMinDep.ShowDialog();
        }

        private void tskTransBatchRpt_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            Ind.Reports.frmTransDownloadsSetup objTransDownloadSetup = new global::CTWebMgmt.Ind.Reports.frmTransDownloadsSetup();

            objTransDownloadSetup.MdiParent = this;

            objTransDownloadSetup.Show();
        }

        private void tskNCOASummary_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            try
            {
                using (ContactInfo.AddressStandardization.Reports.frmNCOASummaryReport objNCOASummaryReport = new global::CTWebMgmt.ContactInfo.AddressStandardization.Reports.frmNCOASummaryReport())
                {
                    objNCOASummaryReport.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void mnuDev_Click(object sender, EventArgs e)
        {
            clsLiveCharge.subProcessRefundCashLinqCC(1, "1431000");
        }

        private void tskCTAnywhere_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (CTAnywhere.frmCTADashboard objCTADashboard = new global::CTWebMgmt.CTAnywhere.frmCTADashboard())
            {
                objCTADashboard.ShowDialog();
            }
        }

        private void tskCustomFlagFieldDefs_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            Admin.frmSyncCustomFlags objSyncCustomFlags = new global::CTWebMgmt.Admin.frmSyncCustomFlags();

            objSyncCustomFlags.MdiParent = this;

            objSyncCustomFlags.Show();
        }

        private void tskBatchWebCamperDetailRpt_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (Ind.Reports.frmBatchWebCamperDetailSetup objBatchWebCamperDetailSetup = new global::CTWebMgmt.Ind.Reports.frmBatchWebCamperDetailSetup())
            {
                objBatchWebCamperDetailSetup.ShowDialog();
            }
        }

        private void tskRecordInfo_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            ContactInfo.frmRecordInformation objRecordInformation = new global::CTWebMgmt.ContactInfo.frmRecordInformation();

            objRecordInformation.Show();
        }

        private void tskSystemSetup_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowSystemSetup(clsNav.objSwitchboard);
        }

        private void tskTextMessage_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            clsNav.subShowTextMessage();
        }

        private void tksUpdateLog_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (Admin.frmUpdateLog objUpdateLog = new global::CTWebMgmt.Admin.frmUpdateLog())
                objUpdateLog.ShowDialog();
        }

        private void tskUnfinishedTransactions_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (Admin.Reports.frmReconcileMerchantGatewaySetup objReconcileMerchantGatewaySetup = new global::CTWebMgmt.Admin.Reports.frmReconcileMerchantGatewaySetup())
                objReconcileMerchantGatewaySetup.ShowDialog();
        }

        private void tskUploadImages_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (Ind.Messaging.frmImgUL objImgUL = new global::CTWebMgmt.Ind.Messaging.frmImgUL())
            {
                objImgUL.ShowDialog();
            }
        }

        private void tskStdAddV2_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            string strListName = "";

            strListName = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            using (ContactInfo.AddStdV2.frmAddStdV2 objAddStdV2_1 = new global::CTWebMgmt.ContactInfo.AddStdV2.frmAddStdV2(strListName))
            {
                objAddStdV2_1.ShowDialog();
            }
        }

        private void tskMORNCOAConflicts_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            using (ContactInfo.AddStdV2.frmMORNCOAAlerts objMORNCOAAlerts = new global::CTWebMgmt.ContactInfo.AddStdV2.frmMORNCOAAlerts())
            {
                objMORNCOAAlerts.ShowDialog();
            }
        }

        private void tskDebugMode_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
        {
            if (clsAppSettings.GetAppSettings().blnDebugMode)
            {
                clsAppSettings.GetAppSettings().blnDebugMode = false;
                tskDebugMode.Text = "Turn Debug Mode On";
            }
            else
            {
                clsAppSettings.GetAppSettings().blnDebugMode = true;
                tskDebugMode.Text = "Turn Debug Mode Off";
            }

        }
    }
}