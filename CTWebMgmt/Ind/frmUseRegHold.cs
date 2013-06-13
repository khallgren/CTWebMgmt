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
    public partial class frmUseRegHold : Form
    {
        private long lngBlockID = 0;
        public long lngRegHoldID = 0;

        public frmUseRegHold(long _lngBlockID)
        {
            InitializeComponent();

            lngBlockID = _lngBlockID;

            subPopRegHoldCbo();
        }

        private void subPopRegHoldCbo()
            {
            string strSQL="";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblRegHold.lngRegHoldID, " +
                            "[strLastCoName] & \", \" & [strFirstName] & \"--\" & [strCompanyName] AS Name, tblBlock.strBlockCode, tblRegHold.intHoldQty, tblRegHold.curCostShare, tblRegHold.dteDeadline AS Deadline " +
                        "FROM (tblBlock " +
                            "INNER JOIN tblRegHold ON tblBlock.lngBlockID = tblRegHold.lngBlockID) " +
                            "INNER JOIN tblRecords ON tblRecords.lngRecordID = tblRegHold.lngRecordID " +
                        "WHERE tblRegHold.intHoldQty > DCount(\"lngRegistrationID\", \"tblRegistrations\", \"lngRegHoldID=\" & [tblRegHold].[lngRegHoldID]) AND " +
                            "tblRegHold.dteDeadline >= Now() AND " +
                            "tblRegHold.lngBlockID = " + lngBlockID + " " +
                        "ORDER BY [strLastCoName] & \", \" & [strFirstName] & \"--\" & [strCompanyName];";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drHolds = cmdDB.ExecuteReader())
                    {
                        while (drHolds.Read())
                        {
                            string strCbo = "";
                            long lngCbo = 0;

                            try { lngCbo = Convert.ToInt32(drHolds["lngRegHoldID"]); }
                            catch { lngCbo = 0; }

                            try { strCbo = Convert.ToString(drHolds["Name"]) + ", " + Convert.ToString(drHolds["intHoldQty"]) + ", " + Convert.ToDecimal(drHolds["curCostShare"]).ToString("C"); }
                            catch { strCbo = ""; }

                            clsCboItem cboNew = new clsCboItem(lngCbo, strCbo);

                            cboRegHoldID.Items.Add(cboNew);
                        }

                        drHolds.Close();
                    }
                }

                conDB.Close();
            }                
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboRegHoldID.SelectedIndex >= 0)
                {
                    clsCboItem cboHold = (clsCboItem)cboRegHoldID.SelectedItem;

                    lngRegHoldID = cboHold.ID;
                }
                else
                    lngRegHoldID = 0;
            }
            catch { lngRegHoldID = 0; }

            Close();
        }
    }
}
