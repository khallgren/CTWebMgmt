namespace CTWebMgmt
{
    partial class frmSystemSetup
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
            this.tabWS = new System.Windows.Forms.TabControl();
            this.tabDBConnect = new System.Windows.Forms.TabPage();
            this.btnFindCTMain_B = new System.Windows.Forms.Button();
            this.btnTestCTMain = new System.Windows.Forms.Button();
            this.txtSQLPassword = new System.Windows.Forms.TextBox();
            this.lblSQLPassword = new System.Windows.Forms.Label();
            this.txtSQLUsername = new System.Windows.Forms.TextBox();
            this.lblSQLUsername = new System.Windows.Forms.Label();
            this.txtSQLDatabase = new System.Windows.Forms.TextBox();
            this.lblSQLDatabase = new System.Windows.Forms.Label();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.lblSQLServer = new System.Windows.Forms.Label();
            this.btnSetConnection = new System.Windows.Forms.Button();
            this.lblPOSConnection = new System.Windows.Forms.Label();
            this.txtCTConnection = new System.Windows.Forms.TextBox();
            this.fraCTDBType = new System.Windows.Forms.GroupBox();
            this.radSQLServer = new System.Windows.Forms.RadioButton();
            this.radMSAccess = new System.Windows.Forms.RadioButton();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.txtCashLinqCQMerchantID = new System.Windows.Forms.TextBox();
            this.lblCashLinqCQMerchantID = new System.Windows.Forms.Label();
            this.txtCashLinqCQPW = new System.Windows.Forms.TextBox();
            this.lblCashLinqCQPW = new System.Windows.Forms.Label();
            this.txtCashLinqCQUser = new System.Windows.Forms.TextBox();
            this.lblCashLinqCQUser = new System.Windows.Forms.Label();
            this.btnDev = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMDCustomerID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClientID = new System.Windows.Forms.TextBox();
            this.txtXChargeAuthKey = new System.Windows.Forms.TextBox();
            this.txtXChargeXWebID = new System.Windows.Forms.TextBox();
            this.lblXChargeAuthKey = new System.Windows.Forms.Label();
            this.lblXChargeXWebID = new System.Windows.Forms.Label();
            this.txtCashLinqPW = new System.Windows.Forms.TextBox();
            this.txtCashLinqUser = new System.Windows.Forms.TextBox();
            this.lblCashLinqPW = new System.Windows.Forms.Label();
            this.lblCashLinqUser = new System.Windows.Forms.Label();
            this.btnSetXChargePath = new System.Windows.Forms.Button();
            this.lblXChargePath = new System.Windows.Forms.Label();
            this.txtXChargePath = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.cboCCMethod = new System.Windows.Forms.ComboBox();
            this.cmdSaveSettings = new System.Windows.Forms.Button();
            this.txtStationID = new System.Windows.Forms.TextBox();
            this.lblStationID = new System.Windows.Forms.Label();
            this.pagGiftSettings = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboDefaultCampaign = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboIndRegCategory = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboDXCampaign = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDXCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSaveGiftTab = new System.Windows.Forms.Button();
            this.pagWebSettings = new System.Windows.Forms.TabPage();
            this.btnSaveWebSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTransTypeForCustomRegFlagCharge = new System.Windows.Forms.ComboBox();
            this.chkCapsWebProcessing = new System.Windows.Forms.CheckBox();
            this.tabWS.SuspendLayout();
            this.tabDBConnect.SuspendLayout();
            this.fraCTDBType.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pagGiftSettings.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pagWebSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabWS
            // 
            this.tabWS.Controls.Add(this.tabDBConnect);
            this.tabWS.Controls.Add(this.tabSettings);
            this.tabWS.Controls.Add(this.pagGiftSettings);
            this.tabWS.Controls.Add(this.pagWebSettings);
            this.tabWS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWS.Location = new System.Drawing.Point(0, 0);
            this.tabWS.Name = "tabWS";
            this.tabWS.SelectedIndex = 0;
            this.tabWS.Size = new System.Drawing.Size(651, 425);
            this.tabWS.TabIndex = 0;
            // 
            // tabDBConnect
            // 
            this.tabDBConnect.Controls.Add(this.btnFindCTMain_B);
            this.tabDBConnect.Controls.Add(this.btnTestCTMain);
            this.tabDBConnect.Controls.Add(this.txtSQLPassword);
            this.tabDBConnect.Controls.Add(this.lblSQLPassword);
            this.tabDBConnect.Controls.Add(this.txtSQLUsername);
            this.tabDBConnect.Controls.Add(this.lblSQLUsername);
            this.tabDBConnect.Controls.Add(this.txtSQLDatabase);
            this.tabDBConnect.Controls.Add(this.lblSQLDatabase);
            this.tabDBConnect.Controls.Add(this.txtSQLServer);
            this.tabDBConnect.Controls.Add(this.lblSQLServer);
            this.tabDBConnect.Controls.Add(this.btnSetConnection);
            this.tabDBConnect.Controls.Add(this.lblPOSConnection);
            this.tabDBConnect.Controls.Add(this.txtCTConnection);
            this.tabDBConnect.Controls.Add(this.fraCTDBType);
            this.tabDBConnect.Location = new System.Drawing.Point(4, 22);
            this.tabDBConnect.Name = "tabDBConnect";
            this.tabDBConnect.Padding = new System.Windows.Forms.Padding(3);
            this.tabDBConnect.Size = new System.Drawing.Size(643, 399);
            this.tabDBConnect.TabIndex = 0;
            this.tabDBConnect.Text = "DB Connect";
            this.tabDBConnect.UseVisualStyleBackColor = true;
            // 
            // btnFindCTMain_B
            // 
            this.btnFindCTMain_B.Location = new System.Drawing.Point(595, 233);
            this.btnFindCTMain_B.Name = "btnFindCTMain_B";
            this.btnFindCTMain_B.Size = new System.Drawing.Size(34, 24);
            this.btnFindCTMain_B.TabIndex = 31;
            this.btnFindCTMain_B.Text = "...";
            this.btnFindCTMain_B.UseVisualStyleBackColor = true;
            this.btnFindCTMain_B.Click += new System.EventHandler(this.btnFindCTMain_B_Click);
            // 
            // btnTestCTMain
            // 
            this.btnTestCTMain.Location = new System.Drawing.Point(478, 262);
            this.btnTestCTMain.Name = "btnTestCTMain";
            this.btnTestCTMain.Size = new System.Drawing.Size(111, 24);
            this.btnTestCTMain.TabIndex = 30;
            this.btnTestCTMain.Text = "Test Connection";
            this.btnTestCTMain.UseVisualStyleBackColor = true;
            this.btnTestCTMain.Click += new System.EventHandler(this.btnTestCTMain_Click);
            // 
            // txtSQLPassword
            // 
            this.txtSQLPassword.Location = new System.Drawing.Point(285, 207);
            this.txtSQLPassword.Name = "txtSQLPassword";
            this.txtSQLPassword.Size = new System.Drawing.Size(304, 20);
            this.txtSQLPassword.TabIndex = 29;
            // 
            // lblSQLPassword
            // 
            this.lblSQLPassword.AutoSize = true;
            this.lblSQLPassword.Location = new System.Drawing.Point(281, 207);
            this.lblSQLPassword.Name = "lblSQLPassword";
            this.lblSQLPassword.Size = new System.Drawing.Size(53, 13);
            this.lblSQLPassword.TabIndex = 28;
            this.lblSQLPassword.Text = "Password";
            // 
            // txtSQLUsername
            // 
            this.txtSQLUsername.Location = new System.Drawing.Point(284, 181);
            this.txtSQLUsername.Name = "txtSQLUsername";
            this.txtSQLUsername.Size = new System.Drawing.Size(304, 20);
            this.txtSQLUsername.TabIndex = 27;
            // 
            // lblSQLUsername
            // 
            this.lblSQLUsername.AutoSize = true;
            this.lblSQLUsername.Location = new System.Drawing.Point(281, 181);
            this.lblSQLUsername.Name = "lblSQLUsername";
            this.lblSQLUsername.Size = new System.Drawing.Size(55, 13);
            this.lblSQLUsername.TabIndex = 26;
            this.lblSQLUsername.Text = "Username";
            // 
            // txtSQLDatabase
            // 
            this.txtSQLDatabase.Location = new System.Drawing.Point(284, 153);
            this.txtSQLDatabase.Name = "txtSQLDatabase";
            this.txtSQLDatabase.Size = new System.Drawing.Size(304, 20);
            this.txtSQLDatabase.TabIndex = 25;
            // 
            // lblSQLDatabase
            // 
            this.lblSQLDatabase.AutoSize = true;
            this.lblSQLDatabase.Location = new System.Drawing.Point(281, 153);
            this.lblSQLDatabase.Name = "lblSQLDatabase";
            this.lblSQLDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblSQLDatabase.TabIndex = 24;
            this.lblSQLDatabase.Text = "Database";
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Location = new System.Drawing.Point(284, 125);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(304, 20);
            this.txtSQLServer.TabIndex = 23;
            // 
            // lblSQLServer
            // 
            this.lblSQLServer.AutoSize = true;
            this.lblSQLServer.Location = new System.Drawing.Point(281, 125);
            this.lblSQLServer.Name = "lblSQLServer";
            this.lblSQLServer.Size = new System.Drawing.Size(38, 13);
            this.lblSQLServer.TabIndex = 22;
            this.lblSQLServer.Text = "Server";
            // 
            // btnSetConnection
            // 
            this.btnSetConnection.Location = new System.Drawing.Point(563, 6);
            this.btnSetConnection.Name = "btnSetConnection";
            this.btnSetConnection.Size = new System.Drawing.Size(74, 30);
            this.btnSetConnection.TabIndex = 21;
            this.btnSetConnection.Text = "Save";
            this.btnSetConnection.UseVisualStyleBackColor = true;
            this.btnSetConnection.Click += new System.EventHandler(this.btnSetConnection_Click);
            // 
            // lblPOSConnection
            // 
            this.lblPOSConnection.AutoSize = true;
            this.lblPOSConnection.Location = new System.Drawing.Point(31, 239);
            this.lblPOSConnection.Name = "lblPOSConnection";
            this.lblPOSConnection.Size = new System.Drawing.Size(116, 13);
            this.lblPOSConnection.TabIndex = 20;
            this.lblPOSConnection.Text = "CampTrak Connection:";
            // 
            // txtCTConnection
            // 
            this.txtCTConnection.Location = new System.Drawing.Point(178, 236);
            this.txtCTConnection.Name = "txtCTConnection";
            this.txtCTConnection.Size = new System.Drawing.Size(411, 20);
            this.txtCTConnection.TabIndex = 19;
            // 
            // fraCTDBType
            // 
            this.fraCTDBType.Controls.Add(this.radSQLServer);
            this.fraCTDBType.Controls.Add(this.radMSAccess);
            this.fraCTDBType.Location = new System.Drawing.Point(14, 126);
            this.fraCTDBType.Name = "fraCTDBType";
            this.fraCTDBType.Size = new System.Drawing.Size(184, 71);
            this.fraCTDBType.TabIndex = 18;
            this.fraCTDBType.TabStop = false;
            this.fraCTDBType.Text = "CampTrak Connection Type";
            // 
            // radSQLServer
            // 
            this.radSQLServer.AutoSize = true;
            this.radSQLServer.Location = new System.Drawing.Point(23, 42);
            this.radSQLServer.Name = "radSQLServer";
            this.radSQLServer.Size = new System.Drawing.Size(80, 17);
            this.radSQLServer.TabIndex = 1;
            this.radSQLServer.TabStop = true;
            this.radSQLServer.Text = "SQL Server";
            this.radSQLServer.UseVisualStyleBackColor = true;
            this.radSQLServer.CheckedChanged += new System.EventHandler(this.radSQLServer_CheckedChanged);
            // 
            // radMSAccess
            // 
            this.radMSAccess.AutoSize = true;
            this.radMSAccess.Location = new System.Drawing.Point(23, 19);
            this.radMSAccess.Name = "radMSAccess";
            this.radMSAccess.Size = new System.Drawing.Size(79, 17);
            this.radMSAccess.TabIndex = 0;
            this.radMSAccess.TabStop = true;
            this.radMSAccess.Text = "MS Access";
            this.radMSAccess.UseVisualStyleBackColor = true;
            this.radMSAccess.CheckedChanged += new System.EventHandler(this.radMSAccess_CheckedChanged);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.txtCashLinqCQMerchantID);
            this.tabSettings.Controls.Add(this.lblCashLinqCQMerchantID);
            this.tabSettings.Controls.Add(this.txtCashLinqCQPW);
            this.tabSettings.Controls.Add(this.lblCashLinqCQPW);
            this.tabSettings.Controls.Add(this.txtCashLinqCQUser);
            this.tabSettings.Controls.Add(this.lblCashLinqCQUser);
            this.tabSettings.Controls.Add(this.btnDev);
            this.tabSettings.Controls.Add(this.groupBox1);
            this.tabSettings.Controls.Add(this.label2);
            this.tabSettings.Controls.Add(this.txtClientID);
            this.tabSettings.Controls.Add(this.txtXChargeAuthKey);
            this.tabSettings.Controls.Add(this.txtXChargeXWebID);
            this.tabSettings.Controls.Add(this.lblXChargeAuthKey);
            this.tabSettings.Controls.Add(this.lblXChargeXWebID);
            this.tabSettings.Controls.Add(this.txtCashLinqPW);
            this.tabSettings.Controls.Add(this.txtCashLinqUser);
            this.tabSettings.Controls.Add(this.lblCashLinqPW);
            this.tabSettings.Controls.Add(this.lblCashLinqUser);
            this.tabSettings.Controls.Add(this.btnSetXChargePath);
            this.tabSettings.Controls.Add(this.lblXChargePath);
            this.tabSettings.Controls.Add(this.txtXChargePath);
            this.tabSettings.Controls.Add(this.Label6);
            this.tabSettings.Controls.Add(this.cboCCMethod);
            this.tabSettings.Controls.Add(this.cmdSaveSettings);
            this.tabSettings.Controls.Add(this.txtStationID);
            this.tabSettings.Controls.Add(this.lblStationID);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(643, 399);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "System Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // txtCashLinqCQMerchantID
            // 
            this.txtCashLinqCQMerchantID.Location = new System.Drawing.Point(132, 178);
            this.txtCashLinqCQMerchantID.Name = "txtCashLinqCQMerchantID";
            this.txtCashLinqCQMerchantID.Size = new System.Drawing.Size(100, 20);
            this.txtCashLinqCQMerchantID.TabIndex = 45;
            // 
            // lblCashLinqCQMerchantID
            // 
            this.lblCashLinqCQMerchantID.AutoSize = true;
            this.lblCashLinqCQMerchantID.Location = new System.Drawing.Point(8, 181);
            this.lblCashLinqCQMerchantID.Name = "lblCashLinqCQMerchantID";
            this.lblCashLinqCQMerchantID.Size = new System.Drawing.Size(115, 13);
            this.lblCashLinqCQMerchantID.TabIndex = 44;
            this.lblCashLinqCQMerchantID.Text = "CustomQ Merchant ID:";
            // 
            // txtCashLinqCQPW
            // 
            this.txtCashLinqCQPW.Location = new System.Drawing.Point(132, 152);
            this.txtCashLinqCQPW.Name = "txtCashLinqCQPW";
            this.txtCashLinqCQPW.Size = new System.Drawing.Size(100, 20);
            this.txtCashLinqCQPW.TabIndex = 43;
            // 
            // lblCashLinqCQPW
            // 
            this.lblCashLinqCQPW.AutoSize = true;
            this.lblCashLinqCQPW.Location = new System.Drawing.Point(8, 155);
            this.lblCashLinqCQPW.Name = "lblCashLinqCQPW";
            this.lblCashLinqCQPW.Size = new System.Drawing.Size(102, 13);
            this.lblCashLinqCQPW.TabIndex = 42;
            this.lblCashLinqCQPW.Text = "CustomQ Password:";
            // 
            // txtCashLinqCQUser
            // 
            this.txtCashLinqCQUser.Location = new System.Drawing.Point(132, 126);
            this.txtCashLinqCQUser.Name = "txtCashLinqCQUser";
            this.txtCashLinqCQUser.Size = new System.Drawing.Size(100, 20);
            this.txtCashLinqCQUser.TabIndex = 41;
            // 
            // lblCashLinqCQUser
            // 
            this.lblCashLinqCQUser.AutoSize = true;
            this.lblCashLinqCQUser.Location = new System.Drawing.Point(8, 129);
            this.lblCashLinqCQUser.Name = "lblCashLinqCQUser";
            this.lblCashLinqCQUser.Size = new System.Drawing.Size(109, 13);
            this.lblCashLinqCQUser.TabIndex = 40;
            this.lblCashLinqCQUser.Text = "CustomQ User Name:";
            // 
            // btnDev
            // 
            this.btnDev.Location = new System.Drawing.Point(520, 368);
            this.btnDev.Name = "btnDev";
            this.btnDev.Size = new System.Drawing.Size(115, 23);
            this.btnDev.TabIndex = 39;
            this.btnDev.Text = "CampTrak Dev";
            this.btnDev.UseVisualStyleBackColor = true;
            this.btnDev.Visible = false;
            this.btnDev.Click += new System.EventHandler(this.btnDev_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMDCustomerID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(11, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 68);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Melissa Data NCOA";
            // 
            // txtMDCustomerID
            // 
            this.txtMDCustomerID.Location = new System.Drawing.Point(115, 28);
            this.txtMDCustomerID.Name = "txtMDCustomerID";
            this.txtMDCustomerID.Size = new System.Drawing.Size(100, 20);
            this.txtMDCustomerID.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Customer ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Text Ripple Client ID:";
            // 
            // txtClientID
            // 
            this.txtClientID.Location = new System.Drawing.Point(132, 234);
            this.txtClientID.Name = "txtClientID";
            this.txtClientID.Size = new System.Drawing.Size(100, 20);
            this.txtClientID.TabIndex = 35;
            // 
            // txtXChargeAuthKey
            // 
            this.txtXChargeAuthKey.Location = new System.Drawing.Point(132, 100);
            this.txtXChargeAuthKey.Name = "txtXChargeAuthKey";
            this.txtXChargeAuthKey.Size = new System.Drawing.Size(100, 20);
            this.txtXChargeAuthKey.TabIndex = 34;
            // 
            // txtXChargeXWebID
            // 
            this.txtXChargeXWebID.Location = new System.Drawing.Point(132, 74);
            this.txtXChargeXWebID.Name = "txtXChargeXWebID";
            this.txtXChargeXWebID.Size = new System.Drawing.Size(235, 20);
            this.txtXChargeXWebID.TabIndex = 33;
            // 
            // lblXChargeAuthKey
            // 
            this.lblXChargeAuthKey.AutoSize = true;
            this.lblXChargeAuthKey.Location = new System.Drawing.Point(8, 103);
            this.lblXChargeAuthKey.Name = "lblXChargeAuthKey";
            this.lblXChargeAuthKey.Size = new System.Drawing.Size(53, 13);
            this.lblXChargeAuthKey.TabIndex = 32;
            this.lblXChargeAuthKey.Text = "Auth Key:";
            // 
            // lblXChargeXWebID
            // 
            this.lblXChargeXWebID.AutoSize = true;
            this.lblXChargeXWebID.Location = new System.Drawing.Point(8, 77);
            this.lblXChargeXWebID.Name = "lblXChargeXWebID";
            this.lblXChargeXWebID.Size = new System.Drawing.Size(57, 13);
            this.lblXChargeXWebID.TabIndex = 31;
            this.lblXChargeXWebID.Text = "X-Web ID:";
            // 
            // txtCashLinqPW
            // 
            this.txtCashLinqPW.Location = new System.Drawing.Point(132, 100);
            this.txtCashLinqPW.Name = "txtCashLinqPW";
            this.txtCashLinqPW.Size = new System.Drawing.Size(100, 20);
            this.txtCashLinqPW.TabIndex = 28;
            // 
            // txtCashLinqUser
            // 
            this.txtCashLinqUser.Location = new System.Drawing.Point(132, 74);
            this.txtCashLinqUser.Name = "txtCashLinqUser";
            this.txtCashLinqUser.Size = new System.Drawing.Size(100, 20);
            this.txtCashLinqUser.TabIndex = 27;
            // 
            // lblCashLinqPW
            // 
            this.lblCashLinqPW.AutoSize = true;
            this.lblCashLinqPW.Location = new System.Drawing.Point(8, 103);
            this.lblCashLinqPW.Name = "lblCashLinqPW";
            this.lblCashLinqPW.Size = new System.Drawing.Size(103, 13);
            this.lblCashLinqPW.TabIndex = 26;
            this.lblCashLinqPW.Text = "CashLinq Password:";
            // 
            // lblCashLinqUser
            // 
            this.lblCashLinqUser.AutoSize = true;
            this.lblCashLinqUser.Location = new System.Drawing.Point(8, 77);
            this.lblCashLinqUser.Name = "lblCashLinqUser";
            this.lblCashLinqUser.Size = new System.Drawing.Size(79, 13);
            this.lblCashLinqUser.TabIndex = 25;
            this.lblCashLinqUser.Text = "CashLinq User:";
            // 
            // btnSetXChargePath
            // 
            this.btnSetXChargePath.Location = new System.Drawing.Point(413, 71);
            this.btnSetXChargePath.Name = "btnSetXChargePath";
            this.btnSetXChargePath.Size = new System.Drawing.Size(28, 23);
            this.btnSetXChargePath.TabIndex = 24;
            this.btnSetXChargePath.Text = "...";
            this.btnSetXChargePath.UseVisualStyleBackColor = true;
            this.btnSetXChargePath.Click += new System.EventHandler(this.btnSetXChargePath_Click);
            // 
            // lblXChargePath
            // 
            this.lblXChargePath.AutoSize = true;
            this.lblXChargePath.Location = new System.Drawing.Point(8, 77);
            this.lblXChargePath.Name = "lblXChargePath";
            this.lblXChargePath.Size = new System.Drawing.Size(79, 13);
            this.lblXChargePath.TabIndex = 23;
            this.lblXChargePath.Text = "X-Charge Path:";
            // 
            // txtXChargePath
            // 
            this.txtXChargePath.Location = new System.Drawing.Point(132, 74);
            this.txtXChargePath.Name = "txtXChargePath";
            this.txtXChargePath.Size = new System.Drawing.Size(275, 20);
            this.txtXChargePath.TabIndex = 22;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(8, 49);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(67, 13);
            this.Label6.TabIndex = 21;
            this.Label6.Text = "Live Charge:";
            // 
            // cboCCMethod
            // 
            this.cboCCMethod.FormattingEnabled = true;
            this.cboCCMethod.Location = new System.Drawing.Point(132, 46);
            this.cboCCMethod.Name = "cboCCMethod";
            this.cboCCMethod.Size = new System.Drawing.Size(121, 21);
            this.cboCCMethod.TabIndex = 20;
            this.cboCCMethod.SelectedIndexChanged += new System.EventHandler(this.cboCCMethod_SelectedIndexChanged);
            // 
            // cmdSaveSettings
            // 
            this.cmdSaveSettings.Location = new System.Drawing.Point(563, 6);
            this.cmdSaveSettings.Name = "cmdSaveSettings";
            this.cmdSaveSettings.Size = new System.Drawing.Size(74, 30);
            this.cmdSaveSettings.TabIndex = 19;
            this.cmdSaveSettings.Text = "Save";
            this.cmdSaveSettings.UseVisualStyleBackColor = true;
            this.cmdSaveSettings.Click += new System.EventHandler(this.cmdSaveSettings_Click);
            // 
            // txtStationID
            // 
            this.txtStationID.Location = new System.Drawing.Point(132, 20);
            this.txtStationID.Name = "txtStationID";
            this.txtStationID.Size = new System.Drawing.Size(100, 20);
            this.txtStationID.TabIndex = 18;
            // 
            // lblStationID
            // 
            this.lblStationID.AutoSize = true;
            this.lblStationID.Location = new System.Drawing.Point(8, 23);
            this.lblStationID.Name = "lblStationID";
            this.lblStationID.Size = new System.Drawing.Size(54, 13);
            this.lblStationID.TabIndex = 17;
            this.lblStationID.Text = "Station ID";
            // 
            // pagGiftSettings
            // 
            this.pagGiftSettings.Controls.Add(this.label9);
            this.pagGiftSettings.Controls.Add(this.panel2);
            this.pagGiftSettings.Controls.Add(this.label8);
            this.pagGiftSettings.Controls.Add(this.panel1);
            this.pagGiftSettings.Controls.Add(this.btnSaveGiftTab);
            this.pagGiftSettings.Location = new System.Drawing.Point(4, 22);
            this.pagGiftSettings.Name = "pagGiftSettings";
            this.pagGiftSettings.Padding = new System.Windows.Forms.Padding(3);
            this.pagGiftSettings.Size = new System.Drawing.Size(643, 399);
            this.pagGiftSettings.TabIndex = 2;
            this.pagGiftSettings.Text = "Gift Settings";
            this.pagGiftSettings.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Block Registration Donations";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.cboDefaultCampaign);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cboIndRegCategory);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(30, 182);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(408, 83);
            this.panel2.TabIndex = 25;
            // 
            // cboDefaultCampaign
            // 
            this.cboDefaultCampaign.FormattingEnabled = true;
            this.cboDefaultCampaign.Location = new System.Drawing.Point(114, 12);
            this.cboDefaultCampaign.Name = "cboDefaultCampaign";
            this.cboDefaultCampaign.Size = new System.Drawing.Size(150, 21);
            this.cboDefaultCampaign.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Default Category:";
            // 
            // cboIndRegCategory
            // 
            this.cboIndRegCategory.FormattingEnabled = true;
            this.cboIndRegCategory.Location = new System.Drawing.Point(114, 39);
            this.cboIndRegCategory.Name = "cboIndRegCategory";
            this.cboIndRegCategory.Size = new System.Drawing.Size(287, 21);
            this.cboIndRegCategory.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Default Campaign:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Donor Express";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cboDXCampaign);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cboDXCategory);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(30, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 83);
            this.panel1.TabIndex = 23;
            // 
            // cboDXCampaign
            // 
            this.cboDXCampaign.FormattingEnabled = true;
            this.cboDXCampaign.Location = new System.Drawing.Point(114, 12);
            this.cboDXCampaign.Name = "cboDXCampaign";
            this.cboDXCampaign.Size = new System.Drawing.Size(150, 21);
            this.cboDXCampaign.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Default Category:";
            // 
            // cboDXCategory
            // 
            this.cboDXCategory.FormattingEnabled = true;
            this.cboDXCategory.Location = new System.Drawing.Point(114, 39);
            this.cboDXCategory.Name = "cboDXCategory";
            this.cboDXCategory.Size = new System.Drawing.Size(287, 21);
            this.cboDXCategory.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Default Campaign:";
            // 
            // btnSaveGiftTab
            // 
            this.btnSaveGiftTab.Location = new System.Drawing.Point(563, 6);
            this.btnSaveGiftTab.Name = "btnSaveGiftTab";
            this.btnSaveGiftTab.Size = new System.Drawing.Size(74, 30);
            this.btnSaveGiftTab.TabIndex = 20;
            this.btnSaveGiftTab.Text = "Save";
            this.btnSaveGiftTab.UseVisualStyleBackColor = true;
            this.btnSaveGiftTab.Click += new System.EventHandler(this.btnSaveGiftTab_Click);
            // 
            // pagWebSettings
            // 
            this.pagWebSettings.Controls.Add(this.chkCapsWebProcessing);
            this.pagWebSettings.Controls.Add(this.btnSaveWebSettings);
            this.pagWebSettings.Controls.Add(this.label1);
            this.pagWebSettings.Controls.Add(this.cboTransTypeForCustomRegFlagCharge);
            this.pagWebSettings.Location = new System.Drawing.Point(4, 22);
            this.pagWebSettings.Name = "pagWebSettings";
            this.pagWebSettings.Padding = new System.Windows.Forms.Padding(3);
            this.pagWebSettings.Size = new System.Drawing.Size(643, 399);
            this.pagWebSettings.TabIndex = 3;
            this.pagWebSettings.Text = "Web Settings";
            this.pagWebSettings.UseVisualStyleBackColor = true;
            // 
            // btnSaveWebSettings
            // 
            this.btnSaveWebSettings.Location = new System.Drawing.Point(563, 6);
            this.btnSaveWebSettings.Name = "btnSaveWebSettings";
            this.btnSaveWebSettings.Size = new System.Drawing.Size(74, 30);
            this.btnSaveWebSettings.TabIndex = 22;
            this.btnSaveWebSettings.Text = "Save";
            this.btnSaveWebSettings.UseVisualStyleBackColor = true;
            this.btnSaveWebSettings.Click += new System.EventHandler(this.btnSaveWebSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trans Type for Custom Reg Flag Charges:";
            // 
            // cboTransTypeForCustomRegFlagCharge
            // 
            this.cboTransTypeForCustomRegFlagCharge.FormattingEnabled = true;
            this.cboTransTypeForCustomRegFlagCharge.Location = new System.Drawing.Point(244, 22);
            this.cboTransTypeForCustomRegFlagCharge.Name = "cboTransTypeForCustomRegFlagCharge";
            this.cboTransTypeForCustomRegFlagCharge.Size = new System.Drawing.Size(166, 21);
            this.cboTransTypeForCustomRegFlagCharge.TabIndex = 0;
            // 
            // chkCapsWebProcessing
            // 
            this.chkCapsWebProcessing.AutoSize = true;
            this.chkCapsWebProcessing.Location = new System.Drawing.Point(22, 74);
            this.chkCapsWebProcessing.Name = "chkCapsWebProcessing";
            this.chkCapsWebProcessing.Size = new System.Drawing.Size(188, 17);
            this.chkCapsWebProcessing.TabIndex = 23;
            this.chkCapsWebProcessing.Text = "Convert Web Data to Uppercase?";
            this.chkCapsWebProcessing.UseVisualStyleBackColor = true;
            // 
            // frmSystemSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 425);
            this.Controls.Add(this.tabWS);
            this.Name = "frmSystemSetup";
            this.Text = "System Setup";
            this.Load += new System.EventHandler(this.frmSystemSetup_Load);
            this.tabWS.ResumeLayout(false);
            this.tabDBConnect.ResumeLayout(false);
            this.tabDBConnect.PerformLayout();
            this.fraCTDBType.ResumeLayout(false);
            this.fraCTDBType.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pagGiftSettings.ResumeLayout(false);
            this.pagGiftSettings.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pagWebSettings.ResumeLayout(false);
            this.pagWebSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabWS;
        private System.Windows.Forms.TabPage tabDBConnect;
        private System.Windows.Forms.TabPage tabSettings;
        internal System.Windows.Forms.Button btnFindCTMain_B;
        internal System.Windows.Forms.Button btnTestCTMain;
        internal System.Windows.Forms.TextBox txtSQLPassword;
        internal System.Windows.Forms.Label lblSQLPassword;
        internal System.Windows.Forms.TextBox txtSQLUsername;
        internal System.Windows.Forms.Label lblSQLUsername;
        internal System.Windows.Forms.TextBox txtSQLDatabase;
        internal System.Windows.Forms.Label lblSQLDatabase;
        internal System.Windows.Forms.TextBox txtSQLServer;
        internal System.Windows.Forms.Label lblSQLServer;
        internal System.Windows.Forms.Button btnSetConnection;
        internal System.Windows.Forms.Label lblPOSConnection;
        internal System.Windows.Forms.TextBox txtCTConnection;
        internal System.Windows.Forms.GroupBox fraCTDBType;
        internal System.Windows.Forms.RadioButton radSQLServer;
        internal System.Windows.Forms.RadioButton radMSAccess;
        internal System.Windows.Forms.TextBox txtCashLinqPW;
        internal System.Windows.Forms.TextBox txtCashLinqUser;
        internal System.Windows.Forms.Label lblCashLinqPW;
        internal System.Windows.Forms.Label lblCashLinqUser;
        internal System.Windows.Forms.Button btnSetXChargePath;
        internal System.Windows.Forms.Label lblXChargePath;
        internal System.Windows.Forms.TextBox txtXChargePath;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.ComboBox cboCCMethod;
        internal System.Windows.Forms.Button cmdSaveSettings;
        internal System.Windows.Forms.TextBox txtStationID;
        internal System.Windows.Forms.Label lblStationID;
        internal System.Windows.Forms.TextBox txtXChargeAuthKey;
        internal System.Windows.Forms.TextBox txtXChargeXWebID;
        internal System.Windows.Forms.Label lblXChargeAuthKey;
        internal System.Windows.Forms.Label lblXChargeXWebID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClientID;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox txtMDCustomerID;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage pagGiftSettings;
        internal System.Windows.Forms.Button btnSaveGiftTab;
        private System.Windows.Forms.Button btnDev;
        internal System.Windows.Forms.TextBox txtCashLinqCQMerchantID;
        internal System.Windows.Forms.Label lblCashLinqCQMerchantID;
        internal System.Windows.Forms.TextBox txtCashLinqCQPW;
        internal System.Windows.Forms.Label lblCashLinqCQPW;
        internal System.Windows.Forms.TextBox txtCashLinqCQUser;
        internal System.Windows.Forms.Label lblCashLinqCQUser;
        private System.Windows.Forms.TabPage pagWebSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTransTypeForCustomRegFlagCharge;
        internal System.Windows.Forms.Button btnSaveWebSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboDXCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboIndRegCategory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboDefaultCampaign;
        private System.Windows.Forms.ComboBox cboDXCampaign;
        private System.Windows.Forms.CheckBox chkCapsWebProcessing;
    }
}