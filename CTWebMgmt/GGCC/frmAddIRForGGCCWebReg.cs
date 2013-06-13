using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CTWebMgmt.GGCC
{
    public partial class frmAddIRForGGCCWebReg : Form
    {
        public long lngGGCCRegWebID = 0;
        public long lngRecordID = 0;

        public frmAddIRForGGCCWebReg(long _lngGGCCRegWebID)
        {
            InitializeComponent();

            lngGGCCRegWebID = _lngGGCCRegWebID;
        }

        private void frmAddIRForGGCCWebReg_Load(object sender, EventArgs e)
        {
            //fill fields with values from web
            OleDbConnection objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
            OleDbCommand objCommand;
            OleDbDataReader drWebRecord;

            string strSQL;

            try
            {
                objConn.Open();

                strSQL = "SELECT lngStateID, " +
                            "strFirstName, strLastCoName, strCompanyName, strAddress, strCity, strZip, strEmail, strHomePhone, strWorkPhone, strCellPhone " +
                        "FROM tblWebRecordsGGCCReg " +
                            "INNER JOIN tblWebGGCCRegistrations ON tblWebRecordsGGCCReg.lngRecordWebID = tblWebGGCCRegistrations.lngRecordWebID " +
                        "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCRegWebID + ";";

                objCommand = new OleDbCommand(strSQL, objConn);

                drWebRecord = objCommand.ExecuteReader();

                if (drWebRecord.Read())
                {
                    clsCboSources.subFillStateCbo(ref cboState, long.Parse(drWebRecord["lngStateID"].ToString()));
                    txtFName.Text = drWebRecord["strFirstName"].ToString();
                    txtLName.Text = drWebRecord["strLastCoName"].ToString();
                    txtCompany.Text = drWebRecord["strCompanyName"].ToString();
                    txtAddress.Text = drWebRecord["strAddress"].ToString();
                    txtCity.Text = drWebRecord["strCity"].ToString();
                    txtZip.Text = drWebRecord["strZip"].ToString();
                    txtEMail.Text = drWebRecord["strEmail"].ToString();
                    txtHomePhone.Text = drWebRecord["strHomePhone"].ToString();
                    txtWorkPhone.Text = drWebRecord["strWorkPhone"].ToString();
                    txtCellPhone.Text = drWebRecord["strCellPhone"].ToString();
                }

                drWebRecord.Close();

                objConn.Close();

                drWebRecord.Dispose();
                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmAddIRForGGCCWebReg.Load", ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lngRecordID = 0;
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //add record, get id, close form
            OleDbConnection objConn;
            OleDbCommand objCommand;

            string strSQL;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                strSQL = "INSERT INTO tblRecords " +
                        "( blnCamper, " +
                            "lngStateID, " +
                            "strFirstName, strLastCoName, strCompanyName, strAddress, strCity, strZip, strHomePhone, strWorkPhone, strCellPhone, strEmail ) " +
                        "SELECT 1 AS blnCamper, " +
                            ((clsCboItem)cboState.SelectedItem).ID + ", " +
                            "\"" + txtFName.Text + "\", \"" + txtLName.Text + "\", \"" + txtCompany.Text + "\", \"" + txtAddress.Text + "\", \"" + txtCity.Text + "\", \"" + txtZip.Text + "\", \"" + txtHomePhone.Text + "\", \"" + txtWorkPhone.Text + "\", @strCellPhone, \"" + txtEMail.Text + "\";";

                objCommand = new OleDbCommand(strSQL, objConn);

                objCommand.Parameters.AddWithValue("@strCellPhone", txtCellPhone.Text);

                if (objCommand.ExecuteNonQuery() > 0)
                {
                    objCommand.CommandText = "SELECT @@IDENTITY;";
                    objCommand.Parameters.Clear();

                    lngRecordID = int.Parse(objCommand.ExecuteScalar().ToString());
                }
                else
                {
                    lngRecordID = 0;
                }

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmAddIRForGGCCWebReg.btnContinue", ex);
                lngRecordID = 0;
            }

            this.Close();
        }
    }
}