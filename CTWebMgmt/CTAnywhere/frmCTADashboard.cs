using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt.CTAnywhere
{
    public partial class frmCTADashboard : Form
    {
        public frmCTADashboard()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnULUsers_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //upload users from tblSecurity
            string strSQL = "";
            string strULRes = "";

            strSQL = "SELECT IIf(IsNull(tblSecurity.lngUserWebID), 0, tblSecurity.lngUserWebID) AS lngUserWebID, tblSecurity.lngUserID, " + clsAppSettings.GetAppSettings().lngCTUserID.ToString() + " AS lngCTUserID, " +
                        "tblSecurity.blnEditTransactions, tblSecurity.blnEditGifts, tblSecurity.blnEditRegistrations, tblSecurity.blnAdministrator, tblSecurity.blnActive, tblSecurity.blnEditRecords, tblSecurity.blnGuestGroup, " +
                        "tblSecurity.lngDepartmentID, " +
                        "tblSecurity.strUserName, tblSecurity.strPassword " +
                    "FROM tblSecurity " +
                    "WHERE tblSecurity.blnActive=True";

            lstStatus.Items.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Begin Uploading Users");
            Application.DoEvents();

            strULRes += clsWebTalk.fcnULWithUpdate(strSQL, "tblSecurity", "lngUserID", "lngUserWebID", true, "users", lstStatus);

            lstStatus.Items.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Finished Uploading Users");
            Application.DoEvents();

            this.Cursor = Cursors.Default;
        }
    }
}
