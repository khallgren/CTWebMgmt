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
    public partial class frmAutoGeneratePledgePmts : Form
    {
        public frmAutoGeneratePledgePmts()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseAutoGeneratePledgePmts();
        }

        private void frmAutoGeneratePledgePmts_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            using (OleDbConnection conCTMain = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conCTMain.Open();

                strSQL = "SELECT tblPledge.lngPledgeID, tblRecords.lngRecordID, " +
                            "tblPledgePayments.curScheduledAmt, " +
                            "Min(tblPledgePayments.dteScheduledDate) AS dteNextPmtDate, " +
                            "tblRecords.strFirstName & \" \" & tblRecords.strLastCoName AS strName, tblRecords.strCompanyName " +
                        "FROM (tblRecords " +
                            "INNER JOIN tblPledge ON tblRecords.lngRecordID = tblPledge.lngRecordID) " +
                            "INNER JOIN tblPledgePayments ON tblPledge.lngPledgeID = tblPledgePayments.lngPledgeID " +
                        "WHERE tblPledgePayments.curPaidAmt=0 AND " +
                            "tblPledge.blnPledgeAutopay=True AND " +
                            "tblPledge.blnNoMoreGifts=False " +
                        "GROUP BY tblPledge.lngPledgeID, tblRecords.lngRecordID, " +
                            "tblPledgePayments.curScheduledAmt, " +
                            "tblRecords.strFirstName, tblRecords.strLastCoName, tblRecords.strCompanyName " +
                        "HAVING DateDiff(\"d\", Min([tblPledgePayments].[dteScheduledDate]), Now())>=0";

                using (OleDbCommand cmdCTMain = new OleDbCommand(strSQL, conCTMain))
                {
                    using (OleDbDataAdapter daPledgePmts = new OleDbDataAdapter(cmdCTMain))
                    {
                        DataTable dtPledgePmts = new DataTable();

                        daPledgePmts.Fill(dtPledgePmts);

                        grdPmtPreview.DataSource = dtPledgePmts;

                        grdPmtPreview.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                        grdPmtPreview.Columns["colPledgeID"].DisplayIndex = 0;
                        grdPmtPreview.Columns["colRecordID"].DisplayIndex = 1;
                        grdPmtPreview.Columns["colName"].DisplayIndex = 2;
                        grdPmtPreview.Columns["colCompanyName"].DisplayIndex = 3;
                        grdPmtPreview.Columns["colNextPmtDate"].DisplayIndex = 4;
                        grdPmtPreview.Columns["colScheduledAmt"].DisplayIndex = 5;
                        grdPmtPreview.Columns["colEditBilling"].DisplayIndex = 6;
                    }
                }

                conCTMain.Close();
            }
        }

        private void btnGeneratePmts_Click(object sender, EventArgs e)
        {
            lstStatus.Items.Insert(0, "Starting auto-generate process: " + DateTime.Now.ToString());
            Application.DoEvents();

            //add gifts, pledge pmts
            ////open set of pmts to be added
             string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblPledge.blnMemorial, tblPledge.blnInHonorOf, " +
                            "tblRecords.lngRecordID, tblPledge.lngCategoryID, tblPledge.lngCampaignID, tblPledge.lngPaymentTypeID, tblPledge.lngBillStateID, tblPledge.lngPledgeID, " +
                            "tblPledgePayments.curScheduledAmt, " +
                            "Min(tblPledgePayments.dteScheduledDate) AS dteNextPmtDate, " +
                            "tblPledge.strMemorialName, tblPledge.strInHonorOf, tblPledge.strAcctNum, tblPledge.strBankName, tblPledge.strBillAddress, tblPledge.strBillCity, tblPledge.strBillName, tblPledge.strBillPhone, tblPledge.strBillZip, tblPledge.strCCExpDate, tblPledge.strXCAlias, tblPledge.strCCNumber, \"\" AS strCCValCode, tblPledge.strRoutingNum, tblPledge.strPNRef, tblPledge.strEPSPmtAcctID " +
                        "FROM (tblRecords " +
                            "INNER JOIN tblPledge ON tblRecords.lngRecordID = tblPledge.lngRecordID) " +
                            "INNER JOIN tblPledgePayments ON tblPledge.lngPledgeID = tblPledgePayments.lngPledgeID " +
                        "WHERE tblPledgePayments.curPaidAmt=0 AND " +
                            "tblPledge.blnPledgeAutopay=True AND " +
                            "tblPledge.blnNoMoreGifts=False " +
                        "GROUP BY tblPledge.blnMemorial, tblPledge.blnInHonorOf, " +
                            "tblRecords.lngRecordID, tblPledge.lngCategoryID, tblPledge.lngCampaignID, tblPledge.lngPaymentTypeID, tblPledge.lngBillStateID, tblPledge.lngPledgeID, " +
                            "tblPledgePayments.curScheduledAmt, " +
                            "tblPledge.strMemorialName, tblPledge.strInHonorOf, tblPledge.strAcctNum, tblPledge.strBankName, tblPledge.strBillAddress, tblPledge.strBillCity, tblPledge.strBillName, tblPledge.strBillPhone, tblPledge.strBillZip, tblPledge.strCCExpDate, tblPledge.strXCAlias, tblPledge.strCCNumber, \"\", tblPledge.strRoutingNum, tblPledge.strPNRef, tblPledge.strEPSPmtAcctID " +
                        "HAVING DateDiff(\"d\", Min([tblPledgePayments].[dteScheduledDate]),Now())>=0";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drPledgePmts = cmdDB.ExecuteReader())
                    {
                        long lngGiftID = 0;
                        long lngPledgeID = 0;
                        long lngPledgePmtID = 0;
                        long lngRecordID = 0;

                        int intPmtCount = 0;

                        while (drPledgePmts.Read())
                        {
                            intPmtCount++;

                            lstStatus.Items.Insert(0, "Adding payment " + intPmtCount.ToString() + " of " + grdPmtPreview.Rows.Count);
                            Application.DoEvents();

                            lngPledgeID = long.Parse(drPledgePmts["lngPledgeID"].ToString());
                            lngRecordID = Convert.ToInt32(drPledgePmts["lngRecordID"]);

                            bool blnMemorial = false;
                            bool blnInHonorOf = false;
                            long lngCategoryID = 0;
                            long lngCampaignID = 0;
                            long lngPaymentTypeID = 0;
                            long lngBillStateID = 0;

                            decimal curScheduledAmt = 0;
                            DateTime dteNextPmtDate = DateTime.MinValue;

                            string strMemorialName = "";
                            string strInHonorOf = "";
                            string strAcctNum = "";
                            string strBankName = "";
                            string strBillAddress = "";
                            string strBillCity = "";
                            string strBillName = "";
                            string strBillPhone = "";
                            string strBillZip = "";
                            string strCCExpDate = "";
                            string strCCNumber = "";
                            string strCCValCode = "";
                            string strRoutingNum = "";

                            try { blnMemorial = Convert.ToBoolean(drPledgePmts["blnMemorial"]); }
                            catch { blnMemorial = false; }

                            try { blnInHonorOf = Convert.ToBoolean(drPledgePmts["blnInHonorOf"]); }
                            catch { }

                            try { lngCategoryID = Convert.ToInt32(drPledgePmts["lngCategoryID"]); }
                            catch { }

                            try { lngCampaignID = Convert.ToInt32(drPledgePmts["lngCampaignID"]); }
                            catch { }

                            try { lngPaymentTypeID = Convert.ToInt32(drPledgePmts["lngPaymentTypeID"]); }
                            catch { lngPaymentTypeID = 0; }

                            try { curScheduledAmt = Convert.ToDecimal(drPledgePmts["curScheduledAmt"]); }
                            catch { }

                            try { dteNextPmtDate = Convert.ToDateTime(drPledgePmts["dteNextPmtDate"]); }
                            catch { }

                            try { strMemorialName = Convert.ToString(drPledgePmts["strMemorialName"]); }
                            catch { }

                            try { strInHonorOf = Convert.ToString(drPledgePmts["strInHonorOf"]); }
                            catch { }

                            try { strAcctNum = Convert.ToString(drPledgePmts["strAcctNum"]); }// clsEncryption.fcnDecrypt(Convert.ToString(drPledgePmts["strAcctNum"])); }
                            catch { }

                            try { strBankName = Convert.ToString(drPledgePmts["strBankName"]); }
                            catch { }

                            try { strBillAddress = Convert.ToString(drPledgePmts["strBillAddress"]); }
                            catch { }

                            try { strBillCity = Convert.ToString(drPledgePmts["strBillCity"]); }
                            catch { }

                            try { strBillName = Convert.ToString(drPledgePmts["strBillName"]); }
                            catch { }

                            try { strBillPhone = Convert.ToString(drPledgePmts["strBillPhone"]); }
                            catch { }

                            try { strBillZip = Convert.ToString(drPledgePmts["strBillZip"]); }
                            catch { }

                            try { strCCExpDate = Convert.ToString(drPledgePmts["strCCExpDate"]); }
                            catch { }

                            try { strCCNumber = Convert.ToString(drPledgePmts["strCCNumber"]); }// clsEncryption.fcnDecrypt(Convert.ToString(drPledgePmts["strCCNumber"])); }
                            catch { }

                            try { strCCValCode = Convert.ToString(drPledgePmts["strCCValCode"]); }
                            catch { }

                            try { strRoutingNum = Convert.ToString(drPledgePmts["strRoutingNum"]); }
                            catch { }

                            //add gift
                            lngGiftID = clsDonorCRUD.fcnAddGift(blnMemorial, blnInHonorOf, lngCategoryID, lngRecordID, lngCampaignID, lngPaymentTypeID, lngBillStateID, lngPledgeID, 0, curScheduledAmt, dteNextPmtDate, strMemorialName, strInHonorOf, strAcctNum, strBankName, strBillAddress, strBillCity, strBillName, strBillPhone, strBillZip, strCCExpDate, strCCNumber, strCCValCode, strRoutingNum);

                            //run transaction through gateway
                            if (!clsLiveCharge.fcnSendGiftTrans((int)this.Handle, lngGiftID, lngRecordID, lstStatus)) MessageBox.Show("There was a problem performing the live charge for the gift.\nPlease process manually.");

                            if (lngPledgeID > 0)
                            {
                                if (lngGiftID > 0)
                                    lngPledgePmtID = clsDonorCRUD.fcnAddPledgePmt(lngPledgeID, lngGiftID, decimal.Parse(drPledgePmts["curScheduledAmt"].ToString()), DateTime.Parse(drPledgePmts["dteNextPmtDate"].ToString()));
                                else
                                    lstStatus.Items.Insert(0, "Error adding payment for pledge id " + lngPledgeID.ToString());
                                
                                Application.DoEvents();
                            }
                        }
                    }
                }

                conDB.Close();
            }

            lstStatus.Items.Insert(0, "Finished auto-generate process: " + DateTime.Now.ToString());
            Application.DoEvents();
        }

        private void grdPmtPreview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            long lngPledgeID = 0;

            try { lngPledgeID = Convert.ToInt32(grdPmtPreview.Rows[e.RowIndex].Cells["colPledgeID"].Value); }
            catch { lngPledgeID = 0; }

            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex>=0)
                {
                    if (grdPmtPreview.Columns[e.ColumnIndex].Name == "colEditBilling")
                    {
                        if (lngPledgeID > 0)
                        {
                            long lngRecordID = 0;

                            try { lngRecordID = Convert.ToInt32(grdPmtPreview.Rows[e.RowIndex].Cells["colRecordID"].Value); }
                            catch { lngRecordID = 0; }

                            using (frmEditPledgeBillingInfo objEditBilling = new frmEditPledgeBillingInfo(lngPledgeID, lngRecordID))
                            {
                                if (objEditBilling.ShowDialog() == DialogResult.OK)
                                {
                                    string strSQL = "";

                                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                                    {
                                        conDB.Open();

                                        long lngPaymentTypeID = 0;
                                        long lngBillStateID = 0;

                                        try { lngPaymentTypeID = objEditBilling.lngPaymentTypeID; }
                                        catch { lngPaymentTypeID = 0; }

                                        try { lngBillStateID = objEditBilling.lngBillStateID; }
                                        catch { lngBillStateID = 0; }

                                        strSQL = "UPDATE tblPledge " +
                                                "SET lngPaymentTypeID=" + lngPaymentTypeID.ToString() + ", lngBillStateID=" + lngBillStateID.ToString() + ", " +
                                                    "strAcctNum='" + objEditBilling.strAcctNum + "', strBankName='" + objEditBilling.strBankName + "', strBillAddress='" + objEditBilling.strBillAddress + "', strBillCity='" + objEditBilling.strBillCity + "', strBillName='" + objEditBilling.strBillName + "', strBillPhone='" + objEditBilling.strBillPhone + "', strBillZip='" + objEditBilling.strBillZip + "', strRoutingNum='" + objEditBilling.strRoutingNum + "', strXCAlias = '" + objEditBilling.strXCAlias + "', strPNRef='" + objEditBilling.strPNRef + "', strEPSPmtAcctID='"+objEditBilling.strEPSPmtAcctID+"' " +
                                                "WHERE lngPledgeID=" + lngPledgeID.ToString();
                                        
                                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                                        {
                                            try
                                            {
                                                cmdDB.ExecuteNonQuery();

                                                strSQL = "UPDATE tblBillingInfo " +
                                                        "SET strPNRef='" + objEditBilling.strPNRef + "', strXCAlias='" + objEditBilling.strXCAlias + "', strEPSPmtAcctID='" + objEditBilling.strEPSPmtAcctID + "' " +
                                                        "WHERE lngRecordID=" + lngRecordID.ToString();

                                                cmdDB.CommandText = strSQL;

                                                try { cmdDB.ExecuteNonQuery(); }
                                                catch { }

                                                MessageBox.Show("Pledge info updated successfuly.");
                                            }
                                            catch (Exception ex) { MessageBox.Show("Error updating billing info: " + ex.Message); }
                                        }

                                        conDB.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("grdPmtPreview_CellClick", ex);
            }
        }

        private void frmAutoGeneratePledgePmts_Activated(object sender, EventArgs e)
        {
            grdPmtPreview.Columns["colPledgeID"].DisplayIndex = 0;
            grdPmtPreview.Columns["colRecordID"].DisplayIndex = 1;
            grdPmtPreview.Columns["colName"].DisplayIndex = 2;
            grdPmtPreview.Columns["colCompanyName"].DisplayIndex = 3;
            grdPmtPreview.Columns["colNextPmtDate"].DisplayIndex = 4;
            grdPmtPreview.Columns["colScheduledAmt"].DisplayIndex = 5;
            grdPmtPreview.Columns["colEditBilling"].DisplayIndex = 6;
        }
    }
}
