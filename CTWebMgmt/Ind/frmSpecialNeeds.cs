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
    public partial class frmSpecialNeeds : Form
    {
        long lngRegWebID = 0;

        public frmSpecialNeeds(long _lngRegWebID)
        {
            InitializeComponent();

            lngRegWebID = _lngRegWebID;
        }

        private void frmSpecialNeeds_Load(object sender, EventArgs e)
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblWebRecords.lngRecordWebID, " +
                            "tblWebRecords.strFirstName, tblWebRecords.strLastCoName, " +
                            "tblWebRecords.mmoSpecialNeeds " +
                        "FROM tblWebRecords " +
                            "INNER JOIN tblWebIndRegistrations ON tblWebRecords.lngRecordWebID = tblWebIndRegistrations.lngRecordWebID " +
                        "WHERE tblWebIndRegistrations.lngRegistrationWebID=" + lngRegWebID;

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drSpecNeeds = cmdDB.ExecuteReader())
                    {
                        if (drSpecNeeds.Read())
                        {
                            this.Text = "Special Needs for " + Convert.ToString(drSpecNeeds["strFirstName"]) + " " + Convert.ToString(drSpecNeeds["strLastCoName"]);
                            txtSpecNeeds.Text = Convert.ToString(drSpecNeeds["mmoSpecialNeeds"]);
                        }

                        drSpecNeeds.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseSpecNeeds();
        }
    }
}
