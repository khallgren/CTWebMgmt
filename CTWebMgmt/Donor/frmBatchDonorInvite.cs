using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net.Mail;
using CTWebMgmt.Properties;


namespace CTWebMgmt.Donor
{
    public partial class frmBatchDonorInvite : Form
    {
        string strMailServer = "";
        string strUName = "";
        string strPW = "";
        string[,] strFlds;

        public frmBatchDonorInvite()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseBatchDonorInvite();
        }

        private void frmBatchDonorInvite_Load(object sender, EventArgs e)
        {
            subVisibility();
            subLoadTemplate();
            subFillFldList();
            subLoadCbos();
        }

        private void subLoadCbos()
        {
            string strSQL="";

            try
            {
                cboDefaultCampaign.Items.Add(new clsCboItem(0, ""));
                cboDefaultCategory.Items.Add(new clsCboItem(0, ""));

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblGiftCategory.lngGiftCategoryID, " +
                                "tblGiftCategory.strOLDesc " +
                            "FROM tblGiftCategory " +
                            "WHERE tblGiftCategory.blnInActive=0 AND " +
                                "tblGiftCategory.blnUseOnline=-1 " +
                            "ORDER BY tblGiftCategory.strOLDesc";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drCat = cmdDB.ExecuteReader())
                        {
                            while (drCat.Read())
                            {
                                cboDefaultCategory.Items.Add(new clsCboItem(Convert.ToInt32(drCat["lngGiftCategoryID"]), Convert.ToString(drCat["strOLDesc"])));
                            }

                            drCat.Close();
                        }

                        strSQL = "SELECT tlkpCampaignCodes.lngCampaignID, " +
                                    "tlkpCampaignCodes.strOLDesc " +
                                "FROM tlkpCampaignCodes " +
                                "WHERE tlkpCampaignCodes.blnActive=-1 AND " +
                                    "tlkpCampaignCodes.blnUseOnline=-1 " +
                                "ORDER BY tlkpCampaignCodes.strOLDesc";

                        cmdDB.CommandText = strSQL;

                        using (OleDbDataReader drCampaigns = cmdDB.ExecuteReader())
                        {
                            while (drCampaigns.Read())
                            {
                                cboDefaultCampaign.Items.Add(new clsCboItem(Convert.ToInt32(drCampaigns["lngCampaignID"]), Convert.ToString(drCampaigns["strOLDesc"])));
                            }

                            drCampaigns.Close();
                        }
                    }

                    conDB.Close();
                }
            }
            catch (Exception ex) { clsErr.subLogErr("frmBatchDonorInvite.subLoadCbos", ex); }        

        }

        private void subFillFldList()
        {
            lstFields.Items.Add("<LinkToSite>");
            lstFields.Items.Add("FirstName");
            lstFields.Items.Add("LastName");
            lstFields.Items.Add("CompanyName");
            lstFields.Items.Add("Address");
            lstFields.Items.Add("City");
            lstFields.Items.Add("State");
            lstFields.Items.Add("Zip");
            lstFields.Items.Add("EMail");

            strFlds = new string[8, 2];

            strFlds[0, 0] = "FirstName";
            strFlds[0, 1] = "strFirstName";

            strFlds[1, 0] = "LastName";
            strFlds[1, 1] = "strLastCoName";

            strFlds[2, 0] = "CompanyName";
            strFlds[2, 1] = "strCompanyName";

            strFlds[3, 0] = "Address";
            strFlds[3, 1] = "strAddress";

            strFlds[4, 0] = "City";
            strFlds[4, 1] = "strCity";

            strFlds[5, 0] = "State";
            strFlds[5, 1] = "strState";

            strFlds[6, 0] = "Zip";
            strFlds[6, 1] = "strZip";

            strFlds[7, 0] = "EMail";
            strFlds[7, 1] = "strEMail";
        }

        private void subVisibility()
        {
            lblDate1.Visible = false;
            lblDate2.Visible = false;
            dpkDate1.Visible = false;
            dpkDate2.Visible = false;

            if (radOneDate.Checked)
            {
                lblDate1.Visible = true;
                dpkDate1.Visible = true;
            }
            else if (radDateRange.Checked)
            {
                lblDate1.Visible = true;
                lblDate2.Visible = true;
                dpkDate1.Visible = true;
                dpkDate2.Visible = true;
            }
        }

        private void radAllDates_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radOneDate_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radDateRange_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void subLoadTemplate()
        {
            string strSQL = "";

            using (OleDbConnection conCT = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conCT.Open();

                strSQL = "SELECT blnUseSSL, "+
                            "tblCampDefaults.strReplyEmail, tblCampDefaults.strEmailName, tblCampDefaults.strDonorInviteSubject, tblCampDefaults.strSMTP, tblCampDefaults.strEMailUName, tblCampDefaults.strEMailPW, " +
                            "tblCampDefaults.mmoDonorInviteBody " +
                        "FROM tblCampDefaults;";

                using (OleDbCommand cmdCT = new OleDbCommand(strSQL, conCT))
                {
                    using (OleDbDataReader drDefaults = cmdCT.ExecuteReader())
                    {
                        if (drDefaults.Read())
                        {
                            txtFromEMail.Text = drDefaults["strReplyEMail"].ToString();
                            txtFromAlias.Text = drDefaults["strEmailName"].ToString();
                            txtSubject.Text = drDefaults["strDonorInviteSubject"].ToString();
                            txtMsgBody.Text = drDefaults["mmoDonorInviteBody"].ToString();
                            strMailServer = drDefaults["strSMTP"].ToString();
                            strUName = drDefaults["strEMailUName"].ToString();
                            strPW = drDefaults["strEMailPW"].ToString();

                            try
                            {
                                if (Convert.ToBoolean(drDefaults["blnUseSSL"]))
                                    chkUseSSL.Checked = true;
                                else
                                    chkUseSSL.Checked = false;
                            }
                            catch { }
                        }

                        drDefaults.Close();
                    }
                }

                conCT.Close();
            }
        }

        private void btnUpdateDef_Click(object sender, EventArgs e)
        {
            string strSQL = "";

            try
            {
                using (OleDbConnection conCT = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conCT.Open();

                    strSQL = "UPDATE tblCampDefaults " +
                            "SET blnUseSSL=" + chkUseSSL.Checked + ", " +
                                "strReplyEmail='" + txtFromEMail.Text + "', strEmailName='" + txtFromAlias.Text + "', strDonorInviteSubject='" + txtSubject.Text + "', " +
                                "mmoDonorInviteBody='" + txtMsgBody.Text + "';";

                    using (OleDbCommand cmdCT = new OleDbCommand(strSQL, conCT))
                    {
                        cmdCT.ExecuteNonQuery();
                    }

                    conCT.Close();
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmBatchDonorInvite.btnUpdateDef_Click", ex);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            string strWhere = "";

            strSQL = "SELECT tblRecords.lngRecordID, " +
                        "tblRecords.strFirstName, tblRecords.strLastCoName, tblRecords.strEmail, tblRecords.strCompanyName, IIf([tblRecords].[blnUseMORAddress],[tblMOR].[strMORAddress1],[tblRecords].[strAddress]) AS strAddress, IIf([tblRecords].[blnUseMORAddress],[tblMOR].[strMORCity],[tblRecords].[strCity]) AS strCity, IIf([tblRecords].[blnUseMORAddress],[tlkpStates_1].[strState],[tlkpStates].[strState]) AS strState, IIf([tblRecords].[blnUseMORAddress],[tblMOR].[strMORZip],[tblRecords].[strZip]) AS strZip, tblRecords.strEmail " +
                    "FROM ((tblRecords " +
                        "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                        "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                        "LEFT JOIN tlkpStates AS tlkpStates_1 ON tblMOR.lngMORStateID = tlkpStates_1.lngStateID ";

            if (radOneDate.Checked)
            {
                if (strWhere == "")
                    strWhere = "WHERE DateDiff(\"d\",#" + dpkDate1.Text + "#, [tblRecords].[dteCreationDate])>=0 ";
                else
                    strWhere += "AND DateDiff(\"d\",#" + dpkDate1.Text + "#, [tblRecords].[dteCreationDate])>=0 ";
            }
            else if (radDateRange.Checked)
            {
                if (strWhere == "")
                    strWhere = "WHERE DateDiff(\"d\",#" + dpkDate1.Text + "#, [tblRecords].[dteCreationDate])>=0 AND " +
                                "DateDiff(\"d\",[tblRecords].[dteCreationDate], #" + dpkDate2.Text + "#)>=0 ";
                else
                    strWhere += "AND DateDiff(\"d\",#" + dpkDate1.Text + "#, [tblRecords].[dteCreationDate])>=0 AND " +
                            "DateDiff(\"d\",[tblRecords].[dteCreationDate], #" + dpkDate2.Text + "#)>=0 ";
            }

            if (chkTicked.Checked)
            {
                if (strWhere == "")
                    strWhere = "WHERE tblRecords.blnTick=True ";
                else
                    strWhere += "AND tblRecords.blnTick=True ";
            }

            if (strWhere == "")
                strWhere = "WHERE tblRecords.blnDonor=True AND " +
                        "tblRecords.strEmail<>\"\";";
            else
                strWhere += "AND tblRecords.blnDonor=True AND " +
                        "tblRecords.strEmail<>\"\";";

            strSQL += strWhere;


            using (OleDbConnection conCT = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conCT.Open();

                using (OleDbCommand cmdCT = new OleDbCommand(strSQL, conCT))
                {
                    using (OleDbDataReader drDonors = cmdCT.ExecuteReader())
                    {
                        while (drDonors.Read())
                        {
                            lstStatus.Items.Insert(0, "Sending to " + drDonors["strEMail"].ToString() + " (" + drDonors["strFirstName"].ToString() + " " + drDonors["strLastCoName"].ToString() + ")");
                            Application.DoEvents();

                            subSendInvite(drDonors);
                        }

                        drDonors.Close();
                    }
                }

                conCT.Close();
            }

            lstStatus.Items.Insert(0, "Batch e-mail complete.");
            Application.DoEvents();
        }

        private void subSendInvite(OleDbDataReader _drDonor)
        {
            MailAddress madFrom;
            MailAddress madTo;

            string strBody;

            string strToEMail;
            string strFName;
            string strLName;

            strToEMail = _drDonor["strEMail"].ToString();
            strFName = _drDonor["strFirstName"].ToString();
            strLName = _drDonor["strLastCoName"].ToString();

            madFrom = new MailAddress(txtFromEMail.Text, txtFromAlias.Text);
            madTo = new MailAddress(strToEMail, strFName + " " + strLName);

            strBody = fcnParseBody(_drDonor);

            using (MailMessage msgConf = new MailMessage(madFrom, madTo))
            {
                msgConf.Body = strBody;

                if (chkHTML.Checked) msgConf.IsBodyHtml = true;
                msgConf.Subject = txtSubject.Text;

                SmtpClient cliLat = new SmtpClient(strMailServer);

                cliLat.EnableSsl = chkUseSSL.Checked;

                if (chkUseSSL.Checked) cliLat.Port = 587;

                cliLat.Credentials = new System.Net.NetworkCredential(strUName, strPW);

                try
                {
                    cliLat.Send(msgConf);
                }
                catch (Exception ex)
                {
                    clsErr.subLogErr("subSendInvite", ex);
                }
            }
        }

        private void lstFields_DoubleClick(object sender, EventArgs e)
        {
            string strFld = lstFields.SelectedItem.ToString();
            int intPos = txtMsgBody.SelectionStart;

            if (strFld == "<LinkToSite>")
                txtMsgBody.Text = txtMsgBody.Text.Insert(intPos, strFld + "EnterLinkText</LinkToSite>");
            else
                txtMsgBody.Text = txtMsgBody.Text.Insert(intPos, "[" + strFld + "]");
        }

        private string fcnParseBody(OleDbDataReader _drDonor)
        {
            string strRes = "";
            string strBody = txtMsgBody.Text;

            long lngRecordID;
            long lngCategoryID = 0;
            long lngCampaignID = 0;

            //get id for link url
            lngRecordID = long.Parse(_drDonor["lngRecordID"].ToString());

            //replace each field with the value in the dr
            for (int intI = 0; intI <= strFlds.GetUpperBound(0); intI++)
            {
                strBody = strBody.Replace("[" + strFlds[intI, 0] + "]", _drDonor[strFlds[intI, 1]].ToString());
            }

            if (cboDefaultCategory.SelectedIndex > 0)
                lngCategoryID = ((clsCboItem)cboDefaultCategory.SelectedItem).ID;
            
            if (cboDefaultCampaign.SelectedIndex > 0)
                lngCampaignID = ((clsCboItem)cboDefaultCampaign.SelectedItem).ID;

            strBody = strBody.Replace("<LinkToSite>", "<a href=\"https://www.camptrak.com/CTWeb/Signin.aspx?lngCTUserID=" + clsAppSettings.GetAppSettings().lngCTUserID + "&strTarget=Donor/DonorCore.aspx&strSrc=BatchInvite&lngCategoryID=" + lngCategoryID + "&lngCampaignID=" + lngCampaignID + "&lngRecordID=" + lngRecordID + "\">");
            strBody = strBody.Replace("</LinkToSite>", "</a>");

            strRes = strBody;

            return strRes;
        }
    }
}