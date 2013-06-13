using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Xml;


namespace CTWebMgmt.Admin
{
    public partial class frmTextMessage : Form
    {
        public frmTextMessage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseTextMessage();
        }

        private void frmTextMessage_Load(object sender, EventArgs e)
        {
            radTickedRecords.Checked = false;
            radIndReg.Checked = false;
            radCCReg.Checked = false;

            lstRecipients.Items.Clear();

            subVisibility();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                string strSQL = "SELECT strTextClientID " +
                                "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { txtClientID.Text = cmdDB.ExecuteScalar().ToString(); }
                    catch { }
                }

                conDB.Close();
            }
        }

        private void subVisibility()
        {
            try
            {
                fraBlockFilter.Visible = false;
                fraEventFilter.Visible = false;
                cboStart.Visible = false;
                cboEnd.Visible = false;
                cboSpecific.Visible = false;
                lblStart.Visible = false;
                lblEnd.Visible = false;
                chkTicked.Visible = false;

                if (radIndReg.Checked)
                {
                    fraBlockFilter.Visible = true;
                    chkTicked.Visible = true;

                    if (radBlockDateRange.Checked)
                    {
                        lblStart.Visible = true;
                        cboStart.Visible = true;
                        lblEnd.Visible = true;
                        cboEnd.Visible = true;
                    }
                    else if (radSpecificBlock.Checked)
                    {
                        cboSpecific.Visible = true;
                        subFillSpecificCboSource("Ind");
                    }
                }
                else if (radCCReg.Checked)
                {
                    fraEventFilter.Visible = true;
                    chkTicked.Visible = true;

                    //fraEventFilter.BringToFront();
                    //fraBlockFilter.SendToBack();

                    if (radEventDateRange.Checked)
                    {
                        lblStart.Visible = true;
                        cboStart.Visible = true;
                        lblEnd.Visible = true;
                        cboEnd.Visible = true;
                    }
                    else if (radSpecificEvent.Checked)
                    {
                        cboSpecific.Visible = true;
                        subFillSpecificCboSource("CC");
                    }
                }

                subFillRecipientLst();
            }
            catch (Exception ex) { clsErr.subLogErr("frmTextMessage.subVisibility", ex); }
        }

        private void subFillSpecificCboSource(string _strType)
        {
            string strSQL = "";

            cboSpecific.Items.Clear();

            if (_strType == "Ind")
                strSQL = "SELECT tblBlock.lngBlockID AS lngID, " +
                            "tblBlock.strBlockCode AS strName " +
                        "FROM tblBlock " +
                        "ORDER BY tblBlock.strBlockCode;";
            else if (_strType == "CC")
                strSQL = "SELECT tblGGCC.lngGGCCID AS lngID, " +
                            "tblGGCC.strGGCCName AS strName " +
                        "FROM tblGGCC " +
                        "WHERE tblGGCC.lngGroupTypeID=0 AND " +
                            "tblGGCC.lngGroupStatusID<>1 " +
                        "ORDER BY tblGGCC.strGGCCName;";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCbo = cmdDB.ExecuteReader())
                    {
                        while (drCbo.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drCbo["lngID"]), Convert.ToString(drCbo["strName"]));

                            cboSpecific.Items.Add(cboNew);
                        }

                        drCbo.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void subFillRecipientLst()
        {
            try
            {
                string strSQL = "";
                string strErr = "";

                lstRecipients.Items.Clear();

                strSQL = fcnGetSQLString(ref strErr);

                if (strErr == "")
                {
                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            using (OleDbDataReader drRecipients = cmdDB.ExecuteReader())
                            {
                                while (drRecipients.Read())
                                {
                                    if (drRecipients["strCellPhone"].ToString() == "")
                                        lstRecipients.Items.Add(drRecipients["strName"].ToString() + " - No Cell Phone");
                                    else
                                        lstRecipients.Items.Add(drRecipients["strName"].ToString() + " - " + drRecipients["strCellPhone"].ToString());
                                }

                                drRecipients.Close();
                            }
                        }

                        conDB.Close();
                    }
                }
                else
                    lstRecipients.Items.Add(strErr);
            }
            catch (Exception ex) { clsErr.subLogErr("frmTextMessage.subFillRecipientList", ex); }
        }

        #region Controls and Events

        private void radTickedRecords_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radIndReg_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radCCReg_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radAllEvents_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radEventDateRange_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radSpecificEvent_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void chkTicked_CheckedChanged(object sender, EventArgs e)
        {
            subFillRecipientLst();
        }

        private void cboStart_ValueChanged(object sender, EventArgs e)
        {
            subFillRecipientLst();
        }

        private void cboSpecific_SelectedIndexChanged(object sender, EventArgs e)
        {
            subFillRecipientLst();
        }

        private void cboEnd_ValueChanged(object sender, EventArgs e)
        {
            subFillRecipientLst();
        }

        private void radAllBlocks_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radBlockDateRange_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        private void radSpecificBlock_CheckedChanged(object sender, EventArgs e)
        {
            subVisibility();
        }

        #endregion

        private string fcnGetSQLString(ref string _strErr)
        {
            string strRes = "";

            try
            {
                string strSQL = "";

                if (radTickedRecords.Checked)
                    strSQL = "SELECT tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]), \"\", [tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]), \"\", [tblRecords].[strCompanyName])=\"\", \"\", \" - \") & IIf(IsNull([tblRecords].[strCompanyName]), \"\", [tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                    "FROM tblRecords " +
                                    "WHERE tblRecords.blnTick=True;";

                else if (radIndReg.Checked)
                {
                    if (radAllBlocks.Checked)
                    {
                        if (chkTicked.Checked)
                            strSQL = "SELECT tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                    "FROM tblRecords " +
                                        "INNER JOIN tblRegistrations ON tblRecords.lngRecordID = tblRegistrations.lngRecordID " +
                                    "WHERE tblRecords.blnTick=True " +
                                    "GROUP BY tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                    "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                        else
                            strSQL = "SELECT tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                    "FROM tblRecords " +
                                        "INNER JOIN tblRegistrations ON tblRecords.lngRecordID = tblRegistrations.lngRecordID " +
                                    "GROUP BY tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                    "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                    }
                    else if (radBlockDateRange.Checked)
                    {
                        try
                        {
                            DateTime dteStart = Convert.ToDateTime(cboStart.Text);
                            DateTime dteEnd = Convert.ToDateTime(cboEnd.Text);

                            if (chkTicked.Checked)
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM ((tblRecords " +
                                            "INNER JOIN tblRegistrations ON tblRecords.lngRecordID = tblRegistrations.lngRecordID) " +
                                            "INNER JOIN tblBlock ON tblRegistrations.lngBlockID = tblBlock.lngBlockID) " +
                                            "INNER JOIN tlkpWeekDesc ON tblBlock.lngWeekID = tlkpWeekDesc.lngWeekID " +
                                        "WHERE tblRecords.blnTick=True AND " +
                                            "DateDiff(\"d\",#" + Convert.ToString(dteStart) + "#,[tlkpWeekDesc].[dteStartDate])>=0 AND " +
                                            "DateDiff(\"d\",[tlkpWeekDesc].[dteStartDate],#" + Convert.ToString(dteEnd) + "#)>=0 " +
                                        "GROUP BY tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                            else
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM ((tblRecords " +
                                            "INNER JOIN tblRegistrations ON tblRecords.lngRecordID = tblRegistrations.lngRecordID) " +
                                            "INNER JOIN tblBlock ON tblRegistrations.lngBlockID = tblBlock.lngBlockID) " +
                                            "INNER JOIN tlkpWeekDesc ON tblBlock.lngWeekID = tlkpWeekDesc.lngWeekID " +
                                        "WHERE DateDiff(\"d\",#" + Convert.ToString(dteStart) + "#,[tlkpWeekDesc].[dteStartDate])>=0 AND " +
                                            "DateDiff(\"d\",[tlkpWeekDesc].[dteStartDate],#" + Convert.ToString(dteEnd) + "#)>=0 " +
                                        "GROUP BY tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";
                        }
                        catch { _strErr = "Please select a valid start and end date."; }

                    }
                    else if (radSpecificBlock.Checked)
                    {
                        try
                        {
                            long lngBlockID = ((clsCboItem)cboSpecific.SelectedItem).ID;

                            if (chkTicked.Checked)
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM tblRecords " +
                                            "INNER JOIN tblRegistrations ON tblRecords.lngRecordID = tblRegistrations.lngRecordID " +
                                        "WHERE tblRecords.blnTick=True AND " +
                                            "tblRegistrations.lngBlockID=" + lngBlockID.ToString() + " " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                            else
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM tblRecords " +
                                            "INNER JOIN tblRegistrations ON tblRecords.lngRecordID = tblRegistrations.lngRecordID " +
                                        "WHERE tblRegistrations.lngBlockID=" + lngBlockID.ToString() + " " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                        }
                        catch { _strErr = "Please select a block."; }
                    }
                    else
                        _strErr = "Please select records to send a message to.";
                }
                else if (radCCReg.Checked)
                {
                    if (radAllEvents.Checked)
                    {
                        if (chkTicked.Checked)
                            strSQL = "SELECT tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                    "FROM tblRecords " +
                                        "INNER JOIN tblGGCCRegistrations ON tblRecords.lngRecordID = tblGGCCRegistrations.lngRecordID " +
                                    "WHERE tblRecords.blnTick=True " +
                                    "GROUP BY tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                    "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                        else
                            strSQL = "SELECT tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                    "FROM tblRecords " +
                                        "INNER JOIN tblGGCCRegistrations ON tblRecords.lngRecordID = tblGGCCRegistrations.lngRecordID " +
                                    "GROUP BY tblRecords.lngRecordID, " +
                                        "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                    "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";
                    }
                    else if (radEventDateRange.Checked)
                    {
                        try
                        {
                            DateTime dteStart = Convert.ToDateTime(cboStart.Text);
                            DateTime dteEnd = Convert.ToDateTime(cboEnd.Text);

                            if (chkTicked.Checked)
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM (tblRecords " +
                                            "INNER JOIN tblGGCCRegistrations ON tblRecords.lngRecordID = tblGGCCRegistrations.lngRecordID) " +
                                            "INNER JOIN tblGGCC ON tblGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID " +
                                        "WHERE tblRecords.blnTick=True AND " +
                                            "DateDiff(\"d\",#" + dteStart + "#,[tblGGCC].[dteStartDate])>=0 AND " +
                                            "DateDiff(\"d\",[tblGGCC].[dteStartDate],#" + dteEnd + "#)>=0 " +
                                        "GROUP BY tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                            else
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM (tblRecords " +
                                            "INNER JOIN tblGGCCRegistrations ON tblRecords.lngRecordID = tblGGCCRegistrations.lngRecordID) " +
                                            "INNER JOIN tblGGCC ON tblGGCCRegistrations.lngGGCCID = tblGGCC.lngGGCCID " +
                                        "WHERE DateDiff(\"d\",#" + dteStart + "#,[tblGGCC].[dteStartDate])>=0 AND " +
                                            "DateDiff(\"d\",[tblGGCC].[dteStartDate],#" + dteEnd + "#)>=0 " +
                                        "GROUP BY tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]), tblRecords.strCellPhone " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";
                        }
                        catch { _strErr = "Please select a valid start and end date."; }
                    }
                    else if (radSpecificEvent.Checked)
                    {
                        try
                        {
                            long lngGGCCID = ((clsCboItem)cboSpecific.SelectedItem).ID;

                            if (chkTicked.Checked)
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM tblRecords " +
                                            "INNER JOIN tblGGCCRegistrations ON tblRecords.lngRecordID = tblGGCCRegistrations.lngRecordID " +
                                        "WHERE tblRecords.blnTick=True AND " +
                                            "tblGGCCRegistrations.lngGGCCID=" + lngGGCCID + " " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";

                            else
                                strSQL = "SELECT tblRecords.lngRecordID, " +
                                            "IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strName, tblRecords.strCellPhone " +
                                        "FROM tblRecords " +
                                            "INNER JOIN tblGGCCRegistrations ON tblRecords.lngRecordID = tblGGCCRegistrations.lngRecordID " +
                                        "WHERE tblGGCCRegistrations.lngGGCCID=" + lngGGCCID + " " +
                                        "ORDER BY IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) & \", \" & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])=\"\",\"\",\" - \") & IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]);";
                        }
                        catch { _strErr = "Please select an event."; }
                    }
                    else
                        _strErr = "Please select records to send a message to.";
                }
                else
                    _strErr = "Please select records to send a message to.";

                strRes = strSQL;
            }
            catch (Exception ex) { clsErr.subLogErr("frmTextMessage.fcnGetSQLString", ex); }

            return strRes;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            if (txtMessage.Text.Length > 0)
            {
                string strLastChar = txtMessage.Text.Substring(txtMessage.Text.Length - 1, 1);

                if (strLastChar == "&")
                {
                    txtMessage.Text = txtMessage.Text.Substring(0, txtMessage.Text.Length - 1);
                    txtMessage.Text += "and";
                    txtMessage.Select(txtMessage.Text.Length, 0);
                }
                else if (strLastChar == "'" || strLastChar == "/" || strLastChar == "\\" || strLastChar == "~" || strLastChar == ";")
                {
                    txtMessage.Text = txtMessage.Text.Substring(0, txtMessage.Text.Length - 1);
                    txtMessage.Select(txtMessage.Text.Length, 0);
                }
            }

            txtCharRemaining.Text = (160 - txtMessage.Text.Length).ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strErr = "";
            string strMsg = txtMessage.Text;

            DateTime dteStart;
            DateTime dteEnd;

            bool blnSend = false;

            if (radTickedRecords.Checked)
                blnSend = true;
            else if (radIndReg.Checked)
            {
                if (radAllBlocks.Checked)
                    blnSend = true;
                else if (radSpecificBlock.Checked)
                {
                    if (cboSpecific.SelectedIndex >= 0)
                        blnSend = true;
                    else
                    {
                        strErr = "Please select a block.";
                        blnSend = false;
                        cboSpecific.Focus();
                    }
                }
                else if (radBlockDateRange.Checked)
                {
                    try
                    {
                        dteStart = DateTime.Parse(cboStart.Text);
                        dteEnd = DateTime.Parse(cboEnd.Text);
                        blnSend = true;
                    }
                    catch
                    {
                        blnSend = false;
                        strErr = "Please enter valid start and end dates.";
                        cboStart.Focus();
                    }
                }
                else
                {
                    blnSend = false;
                    strErr = "Please select records to send to.";
                    fraBlockFilter.Focus();
                }
            }
            else if (radCCReg.Checked)
            {
                if (radAllEvents.Checked)
                    blnSend = true;
                else if (radSpecificEvent.Checked)
                {
                    if (cboSpecific.SelectedIndex >= 0)
                        blnSend = true;
                    else
                    {
                        strErr = "Please select an event.";
                        blnSend = false;
                        cboSpecific.Focus();
                    }
                }
                else if (radEventDateRange.Checked)
                {
                    try
                    {
                        dteStart = DateTime.Parse(cboStart.Text);
                        dteEnd = DateTime.Parse(cboEnd.Text);
                        blnSend = true;
                    }
                    catch
                    {
                        blnSend = false;
                        strErr = "Please enter valid start and end dates.";
                        cboStart.Focus();
                    }
                }
                else
                {
                    blnSend = false;
                    strErr = "Please select records to send to.";
                    fraEventFilter.Focus();
                }
            }
            else
            {
                blnSend = false;
                strErr = "Please select records to send to.";
                fraSendTo.Focus();
            }

            if (lstRecipients.Items.Count <= 0)
            {
                blnSend = false;
                strErr = "There are no records to send a message to.";
                return;
            }

            if (blnSend)
            {
                if (fcnSendTextMessages(ref strErr))
                    MessageBox.Show("Messages send successfully.");
                else
                    MessageBox.Show("There was an error sending the text messages.");
            }
            else
                MessageBox.Show(strErr);
        }

        private bool fcnSendTextMessages(ref string _strErr)
        {
            bool blnRes = false;
            try
            {
                HttpWebRequest reqTextRipple;

                //create web request, set request parameters
                reqTextRipple = (HttpWebRequest)WebRequest.Create("http://www.textripple.com/aggregator.php");

                reqTextRipple.KeepAlive = false;
                reqTextRipple.Method = "POST";
                reqTextRipple.ContentType = "application/x-www-form-urlencoded";

                MemoryStream msmToPost = new MemoryStream();

                XmlDocument xmlDoc = new XmlDocument();

                //create top element of xml -- other transaction elements will be appended under this parent.
                XmlElement xmlRequest = xmlDoc.CreateElement("request");

                xmlDoc.AppendChild(xmlRequest);

                XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", null, null);

                //put xml declaration (version spec) before top level parent
                xmlDoc.InsertBefore(xmlDec, xmlRequest);

                //append transaction details as elements of xml document
                XmlElement xmlAccount = xmlDoc.CreateElement("account");
                xmlAccount.SetAttribute("id", txtClientID.Text);

                XmlElement xmlMsg = xmlDoc.CreateElement("message");
                //xmlMsg.SetAttribute("data", "48656c6c6f");
                xmlMsg.SetAttribute("data", fcnEncodeMsg());

                XmlElement xmlNotification = xmlDoc.CreateElement("notification");
                XmlElement xmlNotEmail = xmlDoc.CreateElement("email");
                xmlNotEmail.SetAttribute("data", "kraig@camptrak.com");
                xmlNotification.AppendChild(xmlNotEmail);

                XmlElement xmlDest = xmlDoc.CreateElement("destination");
                
                //set numbers
                string strErr="";
                string strSQL = fcnGetSQLString(ref strErr);

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drMsg = cmdDB.ExecuteReader())
                        {
                            Regex regNumeric = new Regex("[0-9]");

                            while (drMsg.Read())
                            {
                                if (drMsg["strCellPhone"].ToString() != "")
                                {
                                    string strDBCell = drMsg["strCellPhone"].ToString();
                                    string strNumber = "";

                                    for (int intI = 0; intI < strDBCell.Length; intI++)
                                    {
                                        if (regNumeric.IsMatch(strDBCell.Substring(intI, 1))) strNumber += strDBCell.Substring(intI, 1);
                                    }

                                    try
                                    {
                                        if (strNumber.Substring(0, 1) != "1")
                                            strNumber = "1" + strNumber;
                                    }
                                    catch { }

                                    XmlElement xmlNumber = xmlDoc.CreateElement("number");

                                    xmlNumber.SetAttribute("data", strNumber);
                                    xmlDest.AppendChild(xmlNumber);
                                }
                            }

                            drMsg.Close();
                        }
                    }

                    conDB.Close();
                }
                                               
                xmlRequest.AppendChild(xmlAccount);
                xmlRequest.AppendChild(xmlMsg);
                xmlRequest.AppendChild(xmlNotification);
                xmlRequest.AppendChild(xmlDest);

                //write xml document to string variable
                //xmlDoc.Save(msmToPost);
                string strToPost = "xml=" + xmlDoc.InnerXml;

                //convert xml string to byte array
                byte[] bytToWrite = Encoding.ASCII.GetBytes(strToPost);

                //write string variable to memory stream
                msmToPost.Write(bytToWrite, 0, bytToWrite.Length);

                reqTextRipple.ContentLength = msmToPost.Length;

                //get request stream for web request, write to it
                Stream stmRequest = reqTextRipple.GetRequestStream();

                msmToPost.WriteTo(stmRequest);

                stmRequest.Close();
                msmToPost.Close();
                msmToPost = null;

                // get the response
                WebResponse webResponse = reqTextRipple.GetResponse();
                if (webResponse == null)
                    _strErr = "There was an error sending the text message.";
                else
                    blnRes = true;
            }
            catch (Exception ex) { clsErr.subLogErr("frmTextMessage.btnSend_Click", ex); }
            return blnRes;
        }

        private string fcnEncodeMsg()
        {
            string strRes = "";
            
            foreach (char c in txtMessage.Text)
            {
                int tmp = c;
                strRes += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            
            return strRes;
        }
    }    
}