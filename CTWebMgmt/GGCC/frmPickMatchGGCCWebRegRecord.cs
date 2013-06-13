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
    public partial class frmPickMatchGGCCWebRegRecord : Form
    {
        public long lngMatchedRecordID = 0;
        public long lngGGCCWebRegID = 0;

        public frmPickMatchGGCCWebRegRecord(long _lngGGCCWebRegID)
        {
            InitializeComponent();
            lngGGCCWebRegID = _lngGGCCWebRegID;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //return matched record id

            try
            {
                if (grdMatches.SelectedRows.Count > 0)
                {
                    lngMatchedRecordID = (long)(int)grdMatches.SelectedRows[0].Cells["lngRecordID"].Value;
             
                    this.Close();
                }
                else
                    MessageBox.Show("Please click the record selector to the left of the matching record or click 'Cancel'.");
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmPickMatchGGCCWebRegRecord.btnContinue", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //return 0 on cancel
            lngMatchedRecordID = 0;
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //open form to add new record, return new id
            lngMatchedRecordID = clsNav.fcnShowAddIRForGGCCWebReg(lngGGCCWebRegID);
            this.Close();
        }

        private void frmPickMatchGGCCWebRegRecord_Load(object sender, EventArgs e)
        {
            //fill grid with potential matches on form load

            OleDbConnection objConn;
            OleDbDataAdapter daMatches;
            OleDbDataReader drRegInfo;
            OleDbCommand objCommand;

            string strSQL;
            string strWhere = "";

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);
                daMatches = new OleDbDataAdapter();
                objCommand = new OleDbCommand();

                objConn.Open();

                objCommand.Connection = objConn;

                //get contact info for reg
                strSQL = "SELECT strFirstName, strLastCoName, strCompanyName " +
                        "FROM tblWebRecordsGGCCReg " +
                            "INNER JOIN tblWebGGCCRegistrations ON tblWebRecordsGGCCReg.lngRecordWebID = tblWebGGCCRegistrations.lngRecordWebID " +
                        "WHERE tblWebGGCCRegistrations.lngGGCCRegistrationWebID=" + lngGGCCWebRegID + ";";

                objCommand.CommandText = strSQL;

                drRegInfo = objCommand.ExecuteReader(System.Data.CommandBehavior.SingleRow);

                if (drRegInfo.Read())
                {
                    string[] strFields ={ "strFirstName", "strLastCoName", "strCompanyName" };

                    for (int intI = 0; intI < 3; intI++)
                    {
                        if (drRegInfo[strFields[intI]].ToString() != "")
                        {
                            if (drRegInfo[strFields[intI]].ToString().Length >= 3)
                            {
/*                                if (strWhere == "")
                                    strWhere = "WHERE " + strFields[intI] + " LIKE \"" + drRegInfo[strFields[intI]].ToString().Substring(0, 3) + "%\" ";
                                else
                                    strWhere += "AND " + strFields[intI] + " LIKE \"" + drRegInfo[strFields[intI]].ToString().Substring(0, 3) + "%\" ";
                                */

                                if (strWhere == "")
                                    strWhere = "WHERE ((" + strFields[intI] + " LIKE \"" + drRegInfo[strFields[intI]].ToString().Substring(0, 3) + "%\") OR (" + strFields[intI] + "=\"\") OR (" + strFields[intI] + " IS NULL)) ";
                                else
                                    strWhere += "AND ((" + strFields[intI] + " LIKE \"" + drRegInfo[strFields[intI]].ToString().Substring(0, 3) + "%\") OR (" + strFields[intI] + "=\"\") OR (" + strFields[intI] + " IS NULL)) ";
                            }
                        }
                    }
                }
                drRegInfo.Close();
                
                //matches
                strSQL = "SELECT tblRecords.lngRecordID, " +
                           "tblRecords.strLastCoName & \", \" & tblRecords.strFirstName AS strName, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORAddress1,tblRecords.strAddress) AS strAddress, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORCity,tblRecords.strCity) AS strCity, IIf(tblRecords.blnUseMORAddress=True,tlkpStates_1.strState,tlkpStates.strState) AS strState, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORZip,tblRecords.strZip) AS strZip, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORPhone,tblRecords.strHomePhone) AS strHomePhone " +
                        "FROM ((tblRecords " +
                            "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                            "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                            "LEFT JOIN tlkpStates AS tlkpStates_1 ON tblMOR.lngMORStateID = tlkpStates_1.lngStateID " +
                        strWhere + " " +
                        "ORDER BY tblRecords.strLastCoName & \", \" & tblRecords.strFirstName;";
                //Console.WriteLine(strSQL);
                BindingSource srcMatches = new BindingSource();

                grdMatches.DataSource = srcMatches;

                objCommand.CommandText = strSQL;

                daMatches.SelectCommand = objCommand;

                // Populate a new data table and bind it to the BindingSource.
                DataTable tblMatches = new DataTable();

                tblMatches.Locale = System.Globalization.CultureInfo.InvariantCulture;

                //use data adapter to fill datatable
                daMatches.Fill(tblMatches);

                //set data source of binding source to data table
                srcMatches.DataSource = tblMatches;

                //resize columns
                grdMatches.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                objConn.Close();

                objCommand.Dispose();
                daMatches.Dispose();
                objConn.Dispose();

            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmPickMatchGGCCWebRegRecord.Load", ex);
            }
        }
    }
}