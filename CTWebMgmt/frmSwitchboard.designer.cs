namespace CTWebMgmt
{
    partial class frmSwitchboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.smartExplorerTaskPane1 = new Xceed.SmartUI.Controls.ExplorerTaskPane.SmartExplorerTaskPane(this.components);
            this.mnuIndEvents = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Individual Events");
            this.tskDLIndReg = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Download Registrations");
            this.tskProcessIndReg = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Process Registrations");
            this.tskULBlocks = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Upload Block Settings");
            this.tskSyncTrans = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Synchronize Transactions");
            this.group5 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Settings");
            this.mnuDiscounts = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Discounts");
            this.tskMinDep = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Minimum Deposit");
            this.group6 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Reports");
            this.tskTransBatchRpt = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Downloaded Trans Batch Report");
            this.tskBatchWebCamperDetailRpt = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Batch Web Camper Detail Report");
            this.grpWebMail = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Camper Messaging");
            this.tskUploadImages = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Upload Images");
            this.group1 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Group Events");
            this.tskUploadGGCC = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Upload Event Definitions");
            this.tskDLGGCCReg = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Download Registrations");
            this.tskProcessGGCCReg = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Process Registrations");
            this.group2 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Donations");
            this.tskULGivingHistory = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Upload Giving History");
            this.tskULGiftSettings = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Upload Gift Settings");
            this.tskDLGifts = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Download Gifts");
            this.tskProcessGifts = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Process Gifts");
            this.group3 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Online Utilities");
            this.tskReferredByOptions = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Referred By Options");
            this.tskTestWebService = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Test Web Service");
            this.tskGoogleCal = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Google Calendar");
            this.tskCTAnywhere = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("CampTrak Anywhere");
            this.tskCustomFlagFieldDefs = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Custom Flag/Field Definitions");
            this.group4 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Help");
            this.task9 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("About CampTrak Web Manager");
            this.mnuContactInfo = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Contact Information");
            this.tskRecordInfo = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Record Information");
            this.grpAddressStd = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Address Standardization");
            this.tskStdAddV2 = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Standardize Addresses");
            this.tskNCOASummary = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("NCOA Summary Report");
            this.tskMORNCOAConflicts = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("MOR/NCOA Conflicts");
            this.mnuAdministration = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Administration");
            this.tskSystemSetup = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("System Setup");
            this.tskTextMessage = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Broadcast Text Message");
            this.tksUpdateLog = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Update Log");
            this.tskDebugMode = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Turn Debug Mode On");
            this.grpAdminReports = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Reports");
            this.tskUnfinishedTransactions = new Xceed.SmartUI.Controls.ExplorerTaskPane.Task("Reconcile Merchant Gateway Transactions");
            this.grpLogout = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Logout");
            this.mnuUpdateVersion = new Xceed.SmartUI.Controls.ExplorerTaskPane.Group("Update Version");
            this.mnuSwitchboard = new System.Windows.Forms.MenuStrip();
            this.mnuDonorMgmt = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoGeneratePledgePmts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSendBatchInvitations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDev = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.smartExplorerTaskPane1)).BeginInit();
            this.mnuSwitchboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // smartExplorerTaskPane1
            // 
            this.smartExplorerTaskPane1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smartExplorerTaskPane1.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.mnuIndEvents,
            this.group1,
            this.group2,
            this.group3,
            this.group4,
            this.mnuContactInfo,
            this.mnuAdministration,
            this.grpLogout,
            this.mnuUpdateVersion});
            this.smartExplorerTaskPane1.Location = new System.Drawing.Point(0, 24);
            this.smartExplorerTaskPane1.Name = "smartExplorerTaskPane1";
            this.smartExplorerTaskPane1.Size = new System.Drawing.Size(246, 516);
            this.smartExplorerTaskPane1.TabIndex = 30;
            this.smartExplorerTaskPane1.Text = "smartExplorerTaskPane1";
            this.smartExplorerTaskPane1.UIStyle = Xceed.SmartUI.UIStyle.UIStyle.WindowsClassic;
            // 
            // mnuIndEvents
            // 
            this.mnuIndEvents.Expanded = false;
            this.mnuIndEvents.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskDLIndReg,
            this.tskProcessIndReg,
            this.tskULBlocks,
            this.tskSyncTrans,
            this.group5,
            this.group6,
            this.grpWebMail});
            this.mnuIndEvents.Name = "mnuIndEvents";
            this.mnuIndEvents.Text = "Individual Events";
            // 
            // tskDLIndReg
            // 
            this.tskDLIndReg.Name = "tskDLIndReg";
            this.tskDLIndReg.Text = "Download Registrations";
            this.tskDLIndReg.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskDLIndReg_Click);
            // 
            // tskProcessIndReg
            // 
            this.tskProcessIndReg.Name = "tskProcessIndReg";
            this.tskProcessIndReg.Text = "Process Registrations";
            this.tskProcessIndReg.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskProcessIndReg_Click);
            // 
            // tskULBlocks
            // 
            this.tskULBlocks.Name = "tskULBlocks";
            this.tskULBlocks.Text = "Upload Block Settings";
            this.tskULBlocks.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskULBlocks_Click);
            // 
            // tskSyncTrans
            // 
            this.tskSyncTrans.Name = "tskULTransactions";
            this.tskSyncTrans.Text = "Synchronize Transactions";
            this.tskSyncTrans.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskSyncTrans_Click);
            // 
            // group5
            // 
            this.group5.Expanded = false;
            this.group5.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.mnuDiscounts,
            this.tskMinDep});
            this.group5.Name = "group5";
            this.group5.Text = "Settings";
            // 
            // mnuDiscounts
            // 
            this.mnuDiscounts.Name = "mnuDiscounts";
            this.mnuDiscounts.Text = "Discounts";
            this.mnuDiscounts.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.mnuDiscounts_Click);
            // 
            // tskMinDep
            // 
            this.tskMinDep.Name = "task1";
            this.tskMinDep.Text = "Minimum Deposit";
            this.tskMinDep.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskMinDep_Click);
            // 
            // group6
            // 
            this.group6.Expanded = false;
            this.group6.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskTransBatchRpt,
            this.tskBatchWebCamperDetailRpt});
            this.group6.Name = "group6";
            this.group6.Text = "Reports";
            // 
            // tskTransBatchRpt
            // 
            this.tskTransBatchRpt.Name = "tskTransBatchRpt";
            this.tskTransBatchRpt.Text = "Downloaded Trans Batch Report";
            this.tskTransBatchRpt.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskTransBatchRpt_Click);
            // 
            // tskBatchWebCamperDetailRpt
            // 
            this.tskBatchWebCamperDetailRpt.Name = "tskBatchWebCamperDetailRpt";
            this.tskBatchWebCamperDetailRpt.Text = "Batch Web Camper Detail Report";
            this.tskBatchWebCamperDetailRpt.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskBatchWebCamperDetailRpt_Click);
            // 
            // grpWebMail
            // 
            this.grpWebMail.Expanded = false;
            this.grpWebMail.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskUploadImages});
            this.grpWebMail.Name = "grpWebMail";
            this.grpWebMail.Text = "Camper Messaging";
            // 
            // tskUploadImages
            // 
            this.tskUploadImages.Name = "tskUploadImages";
            this.tskUploadImages.Text = "Upload Images";
            this.tskUploadImages.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskUploadImages_Click);
            // 
            // group1
            // 
            this.group1.Expanded = false;
            this.group1.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskUploadGGCC,
            this.tskDLGGCCReg,
            this.tskProcessGGCCReg});
            this.group1.Name = "group1";
            this.group1.Text = "Group Events";
            // 
            // tskUploadGGCC
            // 
            this.tskUploadGGCC.Name = "task1";
            this.tskUploadGGCC.Text = "Upload Event Definitions";
            this.tskUploadGGCC.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskUploadGGCC_Click);
            // 
            // tskDLGGCCReg
            // 
            this.tskDLGGCCReg.Name = "task2";
            this.tskDLGGCCReg.Text = "Download Registrations";
            this.tskDLGGCCReg.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskDLGGCCReg_Click);
            // 
            // tskProcessGGCCReg
            // 
            this.tskProcessGGCCReg.Name = "task3";
            this.tskProcessGGCCReg.Text = "Process Registrations";
            this.tskProcessGGCCReg.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskProcessGGCCReg_Click);
            // 
            // group2
            // 
            this.group2.Expanded = false;
            this.group2.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskULGivingHistory,
            this.tskULGiftSettings,
            this.tskDLGifts,
            this.tskProcessGifts});
            this.group2.Name = "group2";
            this.group2.Text = "Donations";
            // 
            // tskULGivingHistory
            // 
            this.tskULGivingHistory.Name = "tskULGivingHistory";
            this.tskULGivingHistory.Text = "Upload Giving History";
            this.tskULGivingHistory.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskULGivingHistory_Click);
            // 
            // tskULGiftSettings
            // 
            this.tskULGiftSettings.Name = "task4";
            this.tskULGiftSettings.Text = "Upload Gift Settings";
            this.tskULGiftSettings.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskULGiftSettings_Click);
            // 
            // tskDLGifts
            // 
            this.tskDLGifts.Name = "task5";
            this.tskDLGifts.Text = "Download Gifts";
            this.tskDLGifts.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskDLGifts_Click);
            // 
            // tskProcessGifts
            // 
            this.tskProcessGifts.Name = "task6";
            this.tskProcessGifts.Text = "Process Gifts";
            this.tskProcessGifts.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskProcessGifts_Click);
            // 
            // group3
            // 
            this.group3.Expanded = false;
            this.group3.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskReferredByOptions,
            this.tskTestWebService,
            this.tskGoogleCal,
            this.tskCTAnywhere,
            this.tskCustomFlagFieldDefs});
            this.group3.Name = "group3";
            this.group3.Text = "Online Utilities";
            // 
            // tskReferredByOptions
            // 
            this.tskReferredByOptions.Name = "task7";
            this.tskReferredByOptions.Text = "Referred By Options";
            this.tskReferredByOptions.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskReferredByOptions_Click);
            // 
            // tskTestWebService
            // 
            this.tskTestWebService.Name = "task8";
            this.tskTestWebService.Text = "Test Web Service";
            this.tskTestWebService.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskTestWebService_Click);
            // 
            // tskGoogleCal
            // 
            this.tskGoogleCal.Name = "tskGoogleCal";
            this.tskGoogleCal.Text = "Google Calendar";
            this.tskGoogleCal.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskGoogleCal_Click);
            // 
            // tskCTAnywhere
            // 
            this.tskCTAnywhere.Name = "tskCTAnywhere";
            this.tskCTAnywhere.Text = "CampTrak Anywhere";
            this.tskCTAnywhere.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskCTAnywhere_Click);
            // 
            // tskCustomFlagFieldDefs
            // 
            this.tskCustomFlagFieldDefs.Name = "tskCustomFlagFieldDefs";
            this.tskCustomFlagFieldDefs.Text = "Custom Flag/Field Definitions";
            this.tskCustomFlagFieldDefs.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskCustomFlagFieldDefs_Click);
            // 
            // group4
            // 
            this.group4.Expanded = false;
            this.group4.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.task9});
            this.group4.Name = "group4";
            this.group4.Text = "Help";
            this.group4.Visible = false;
            // 
            // task9
            // 
            this.task9.Name = "task9";
            this.task9.Text = "About CampTrak Web Manager";
            // 
            // mnuContactInfo
            // 
            this.mnuContactInfo.Expanded = false;
            this.mnuContactInfo.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskRecordInfo,
            this.grpAddressStd});
            this.mnuContactInfo.Name = "mnuContactInfo";
            this.mnuContactInfo.Text = "Contact Information";
            // 
            // tskRecordInfo
            // 
            this.tskRecordInfo.Name = "tskRecordInfo";
            this.tskRecordInfo.Text = "Record Information";
            this.tskRecordInfo.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskRecordInfo_Click);
            // 
            // grpAddressStd
            // 
            this.grpAddressStd.Expanded = false;
            this.grpAddressStd.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskStdAddV2,
            this.tskNCOASummary,
            this.tskMORNCOAConflicts});
            this.grpAddressStd.Name = "grpAddressStd";
            this.grpAddressStd.Text = "Address Standardization";
            // 
            // tskStdAddV2
            // 
            this.tskStdAddV2.Name = "tskStdAddV2";
            this.tskStdAddV2.Text = "Standardize Addresses";
            this.tskStdAddV2.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskStdAddV2_Click);
            // 
            // tskNCOASummary
            // 
            this.tskNCOASummary.Name = "tskNCOASummary";
            this.tskNCOASummary.Text = "NCOA Summary Report";
            this.tskNCOASummary.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskNCOASummary_Click);
            // 
            // tskMORNCOAConflicts
            // 
            this.tskMORNCOAConflicts.Name = "tskMORNCOAConflicts";
            this.tskMORNCOAConflicts.Text = "MOR/NCOA Conflicts";
            this.tskMORNCOAConflicts.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskMORNCOAConflicts_Click);
            // 
            // mnuAdministration
            // 
            this.mnuAdministration.Expanded = false;
            this.mnuAdministration.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskSystemSetup,
            this.tskTextMessage,
            this.tksUpdateLog,
            this.tskDebugMode,
            this.grpAdminReports});
            this.mnuAdministration.Name = "mnuAdministration";
            this.mnuAdministration.Text = "Administration";
            // 
            // tskSystemSetup
            // 
            this.tskSystemSetup.Name = "tskSystemSetup";
            this.tskSystemSetup.Text = "System Setup";
            this.tskSystemSetup.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskSystemSetup_Click);
            // 
            // tskTextMessage
            // 
            this.tskTextMessage.Name = "tskTextMessage";
            this.tskTextMessage.Text = "Broadcast Text Message";
            this.tskTextMessage.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskTextMessage_Click);
            // 
            // tksUpdateLog
            // 
            this.tksUpdateLog.Name = "task1";
            this.tksUpdateLog.Text = "Update Log";
            this.tksUpdateLog.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tksUpdateLog_Click);
            // 
            // tskDebugMode
            // 
            this.tskDebugMode.Name = "tskDebugMode";
            this.tskDebugMode.Text = "Turn Debug Mode On";
            this.tskDebugMode.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskDebugMode_Click);
            // 
            // grpAdminReports
            // 
            this.grpAdminReports.Expanded = false;
            this.grpAdminReports.Items.AddRange(new Xceed.SmartUI.SmartItem[] {
            this.tskUnfinishedTransactions});
            this.grpAdminReports.Name = "grpAdminReports";
            this.grpAdminReports.Text = "Reports";
            // 
            // tskUnfinishedTransactions
            // 
            this.tskUnfinishedTransactions.Name = "tskUnfinishedTransactions";
            this.tskUnfinishedTransactions.Text = "Reconcile Merchant Gateway Transactions";
            this.tskUnfinishedTransactions.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.tskUnfinishedTransactions_Click);
            // 
            // grpLogout
            // 
            this.grpLogout.Expanded = false;
            this.grpLogout.Name = "group5";
            this.grpLogout.Text = "Logout";
            this.grpLogout.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.grpLogout_Click);
            // 
            // mnuUpdateVersion
            // 
            this.mnuUpdateVersion.Expanded = false;
            this.mnuUpdateVersion.Name = "group6";
            this.mnuUpdateVersion.Text = "Update Version";
            this.mnuUpdateVersion.Visible = false;
            // 
            // mnuSwitchboard
            // 
            this.mnuSwitchboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDonorMgmt,
            this.mnuDev});
            this.mnuSwitchboard.Location = new System.Drawing.Point(0, 0);
            this.mnuSwitchboard.Name = "mnuSwitchboard";
            this.mnuSwitchboard.Size = new System.Drawing.Size(779, 24);
            this.mnuSwitchboard.TabIndex = 32;
            this.mnuSwitchboard.Text = "menuStrip1";
            // 
            // mnuDonorMgmt
            // 
            this.mnuDonorMgmt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutoGeneratePledgePmts,
            this.mnuSendBatchInvitations});
            this.mnuDonorMgmt.Name = "mnuDonorMgmt";
            this.mnuDonorMgmt.Size = new System.Drawing.Size(126, 20);
            this.mnuDonorMgmt.Text = "&Donor Management";
            // 
            // mnuAutoGeneratePledgePmts
            // 
            this.mnuAutoGeneratePledgePmts.Name = "mnuAutoGeneratePledgePmts";
            this.mnuAutoGeneratePledgePmts.Size = new System.Drawing.Size(245, 22);
            this.mnuAutoGeneratePledgePmts.Text = "Auto-generate &pledge payments";
            this.mnuAutoGeneratePledgePmts.Click += new System.EventHandler(this.mnuAutoGeneratePledgePmts_Click);
            // 
            // mnuSendBatchInvitations
            // 
            this.mnuSendBatchInvitations.Name = "mnuSendBatchInvitations";
            this.mnuSendBatchInvitations.Size = new System.Drawing.Size(245, 22);
            this.mnuSendBatchInvitations.Text = "Send Batch &Invitations";
            this.mnuSendBatchInvitations.Click += new System.EventHandler(this.mnuSendBatchInvitations_Click);
            // 
            // mnuDev
            // 
            this.mnuDev.Name = "mnuDev";
            this.mnuDev.Size = new System.Drawing.Size(91, 20);
            this.mnuDev.Text = "Test Function";
            this.mnuDev.Visible = false;
            this.mnuDev.Click += new System.EventHandler(this.mnuDev_Click);
            // 
            // frmSwitchboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 540);
            this.Controls.Add(this.smartExplorerTaskPane1);
            this.Controls.Add(this.mnuSwitchboard);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuSwitchboard;
            this.Name = "frmSwitchboard";
            this.Text = "CampTrak Web Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSwitchboard_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSwitchboard_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.smartExplorerTaskPane1)).EndInit();
            this.mnuSwitchboard.ResumeLayout(false);
            this.mnuSwitchboard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Xceed.SmartUI.Controls.ExplorerTaskPane.SmartExplorerTaskPane smartExplorerTaskPane1;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group group1;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskUploadGGCC;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskDLGGCCReg;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskProcessGGCCReg;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group group2;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskULGiftSettings;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskDLGifts;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskProcessGifts;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group group3;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskReferredByOptions;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskTestWebService;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group group4;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task task9;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group grpLogout;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group mnuUpdateVersion;
        private System.Windows.Forms.MenuStrip mnuSwitchboard;
        private System.Windows.Forms.ToolStripMenuItem mnuDonorMgmt;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoGeneratePledgePmts;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskULGivingHistory;
        private System.Windows.Forms.ToolStripMenuItem mnuSendBatchInvitations;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group mnuIndEvents;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskDLIndReg;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskProcessIndReg;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskULBlocks;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskGoogleCal;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskSyncTrans;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group group5;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task mnuDiscounts;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskMinDep;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group group6;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskTransBatchRpt;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group mnuContactInfo;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group grpAddressStd;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskNCOASummary;
        private System.Windows.Forms.ToolStripMenuItem mnuDev;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskCTAnywhere;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskCustomFlagFieldDefs;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskBatchWebCamperDetailRpt;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskRecordInfo;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group mnuAdministration;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskSystemSetup;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskTextMessage;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tksUpdateLog;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group grpAdminReports;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskUnfinishedTransactions;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Group grpWebMail;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskUploadImages;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskStdAddV2;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskMORNCOAConflicts;
        private Xceed.SmartUI.Controls.ExplorerTaskPane.Task tskDebugMode;
    }
}