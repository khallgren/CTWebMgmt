using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.ContactInfo
{
    public partial class frmRecordInformation : Form
    {
        private long lngRECORDID = 0;
        private long lngPRIMARYMORID = 0;

        public frmRecordInformation()
        {
            InitializeComponent();
        }

        public frmRecordInformation(long _lngRecordID)
        {
            lngRECORDID = _lngRecordID;
        }

        private void frmRecordInformation_Load(object sender, EventArgs e)
        {
            //load state cbos
            subLoadCbos();

            if (lngRECORDID > 0)
                subLoadRecord(lngRECORDID);
        }

        private void subLoadCbos()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //load state cbos
                strSQL = "SELECT tlkpStates.lngStateID, " +
                            "tlkpStates.strState " +
                        "FROM tlkpStates " +
                        "ORDER BY tlkpStates.strState";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drStates = cmdDB.ExecuteReader())
                    {
                        while (drStates.Read())
                        {
                            long lngStateID = 0;
                            string strState = "";

                            try { lngStateID = Convert.ToInt32(drStates["lngStateID"]); }
                            catch { lngStateID = 0; }

                            try { strState = Convert.ToString(drStates["strState"]); }
                            catch { strState = ""; }

                            cboState.Items.Add(new clsCboItem(lngStateID, strState));
                        }

                        drStates.Close();
                    }
                }

                conDB.Close();
            }
        }

        private void txtFindByID_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool blnEnter = false;

            try { blnEnter = (e.KeyChar == '\r'); }
            catch { blnEnter = false; }

            if (blnEnter)
            {
                long lngFindID = 0;

                try { lngFindID = Convert.ToInt32(txtFindByID.Text); }
                catch { lngFindID = 0; }

                subLoadRecord(lngFindID);
            }
        }

        private void subLoadRecord(long _lngRecordID)
        {
            txtFindByID.Text = "";

            string strSQL = "";

            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblRecords.blnUseMORAddress, " +
                                "tblRecords.lngStateID, tblMOR.lngMORStateID, tblRecords.lngPrimaryMORID, " +
                                "tblRecords.strFirstName, tblRecords.strLastCoName, tblRecords.strMI, tblRecords.strCompanyName, tblRecords.strAddress, tblRecords.strCity, tblRecords.strZip, tblRecords.txtParentSalutation, tblRecords.strInformalParentSal, tblRecords.strFormalGiftSal, tblRecords.strInformalGiftSal, tblMOR.strMORAddress1, tblMOR.strMORCity, tblMOR.strMORZip " +
                            "FROM tblRecords " +
                                "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID " +
                            "WHERE tblRecords.lngRecordID=@lngRecordID";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        cmdDB.Parameters.AddWithValue("@lngRecordID", _lngRecordID);

                        using (OleDbDataReader drIR = cmdDB.ExecuteReader())
                        {
                            if (drIR.Read())
                            {
                                //load record
                                bool blnUseMORAddress = false;

                                long lngStateID = 0;
                                long lngMORStateID = 0;

                                try { blnUseMORAddress = Convert.ToBoolean(drIR["blnUseMORAddress"]); }
                                catch { blnUseMORAddress = false; }

                                try { lngStateID = Convert.ToInt32(drIR["lngStateID"]); }
                                catch { lngStateID = 0; }

                                try { lngMORStateID = Convert.ToInt32(drIR["lngMORStateID"]); }
                                catch { lngMORStateID = 0; }

                                try { lngPRIMARYMORID = Convert.ToInt32(drIR["lngPrimaryMORID"]); }
                                catch { lngPRIMARYMORID = 0; }

                                lngRECORDID = _lngRecordID;

                                txtRecordID.Text = lngRECORDID.ToString();

                                chkUseMORAddress.Checked = blnUseMORAddress;

                                txtFName.Text = Convert.ToString(drIR["strFirstName"]);
                                txtLName.Text = Convert.ToString(drIR["strLastCoName"]);
                                txtMI.Text = Convert.ToString(drIR["strMI"]);
                                txtCompanyName.Text = Convert.ToString(drIR["strCompanyName"]);

                                if (blnUseMORAddress)
                                {
                                    txtAddress.Text = Convert.ToString(drIR["strMORAddress1"]);
                                    txtCity.Text = Convert.ToString(drIR["strMORCity"]);
                                    txtZip.Text = Convert.ToString(drIR["strMORZip"]);

                                    for (int intI = 0; intI < cboState.Items.Count; intI++)
                                        if (((clsCboItem)cboState.Items[intI]).ID == lngMORStateID) cboState.SelectedIndex = intI;
                                }
                                else
                                {
                                    txtAddress.Text = Convert.ToString(drIR["strAddress"]);
                                    txtCity.Text = Convert.ToString(drIR["strCity"]);
                                    txtZip.Text = Convert.ToString(drIR["strZip"]);

                                    for (int intI = 0; intI < cboState.Items.Count; intI++)
                                        if (((clsCboItem)cboState.Items[intI]).ID == lngStateID) cboState.SelectedIndex = intI;
                                }

                                txtParentSalutation.Text = Convert.ToString(drIR["txtParentSalutation"]);
                                txtInformalParentSal.Text = Convert.ToString(drIR["strInformalParentSal"]);
                                txtFormalGiftSal.Text = Convert.ToString(drIR["strFormalGiftSal"]);
                                txtInformalGiftSal.Text = Convert.ToString(drIR["strInformalGiftSal"]);
                            }
                            else
                            {
                                //clear data
                                lngRECORDID = 0;
                            }

                            drIR.Close();
                        }
                    }

                    conDB.Close();
                }

                subFillGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error loading the record: " + ex.Message);
            }
        }

        private void subFillGrids()
        {
            //fill registration and transaction grids for current record id (form.lngRECORDID)

            try
            {
                string strSQL = "";

                strSQL = "SELECT tblRegistrations.lngRegistrationID, " +
                            "tblRegistrations.dteRegistrationDate, " +
                            "tblBlock.strBlockCode, IIf(IsNull([tlkpWeekDesc].[dteStartDate]) Or IsNull([tlkpWeekDesc].[dteEndDate]), '', [tlkpWeekDesc].[dteStartDate] & ' - ' & [tlkpWeekDesc].[dteEndDate]) AS strStartEnd " +
                        "FROM (tblRegistrations " +
                            "INNER JOIN tblBlock ON tblRegistrations.lngBlockID = tblBlock.lngBlockID) " +
                            "LEFT JOIN tlkpWeekDesc ON tblBlock.lngWeekID = tlkpWeekDesc.lngWeekID " +
                        "WHERE tblRegistrations.lngRecordID=" + lngRECORDID.ToString() + " " +
                        "ORDER BY tblRegistrations.dteRegistrationDate";

                using (OleDbDataAdapter daReg = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn))
                {
                    using (OleDbCommandBuilder cmdReg = new OleDbCommandBuilder(daReg))
                    {
                        // Populate a new data table and bind it to the BindingSource.
                        using (DataTable tblReg = new DataTable())
                        {
                            tblReg.Locale = System.Globalization.CultureInfo.InvariantCulture;

                            daReg.Fill(tblReg);

                            grdRegistrations.DataSource = tblReg;
                        }
                    }
                }

                grdRegistrations.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error loading grids: " + ex.Message);
            }
        }

        private void btnAdvFind_Click(object sender, EventArgs e)
        {
            clsIR irToSearch = new clsIR(0, 0, "", "", "", "", "", "", "", "", "", "");

            long lngFindIR = 0;

            using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find record", false))
            {
                if (objFindIR.ShowDialog() == DialogResult.OK)
                {
                    lngFindIR = objFindIR.irToSearch.lngRecordID;

                    if (lngFindIR > 0)
                        subLoadRecord(lngFindIR);
                }
                else
                    return;
            }

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (fcnSave()) this.Close();
        }

        private bool fcnSave()
        {
            bool blnRes = false;

            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "";

                    //collect values from form
                    long lngStateID = 0;

                    try { lngStateID = ((clsCboItem)cboState.SelectedItem).ID; }
                    catch { lngStateID = 0; }

                    if (chkUseMORAddress.Checked)
                    {
                        //update MOR address, set sql for updating other fields
                        strSQL = "UPDATE tblMOR " +
                                "SET tblMOR.lngMORStateID = @lngMORStateID, " +
                                    "tblMOR.strMORAddress1 = @strMORAddress1, tblMOR.strMORCity = @strMORCity, tblMOR.strMORZip = @strMORZip " +
                                "WHERE tblMOR.lngMORID=" + lngPRIMARYMORID.ToString();

                        using (OleDbCommand cmdUpdate = new OleDbCommand(strSQL, conDB))
                        {

                            cmdUpdate.Parameters.AddWithValue("@lngMORStateID", lngStateID);
                            cmdUpdate.Parameters.AddWithValue("@strMORAddress1", txtAddress.Text);
                            cmdUpdate.Parameters.AddWithValue("@strMORCity", txtCity.Text);
                            cmdUpdate.Parameters.AddWithValue("@strMORZip", txtZip.Text);

                            cmdUpdate.ExecuteNonQuery();

                            //update additional record info
                            strSQL = "UPDATE tblRecords " +
                                    "SET tblRecords.blnUseMORAddress = @blnUseMORAddress, " +
                                        "tblRecords.strFirstName = @strFirstName, tblRecords.strLastCoName = @strLastCoName, tblRecords.strMI = @strMI, tblRecords.strCompanyName = @strCompanyName, tblRecords.txtParentSalutation = @txtParentSalutation, tblRecords.strInformalParentSal = @strInformalParentSal, tblRecords.strFormalGiftSal = @strFormalGiftSal, tblRecords.strInformalGiftSal = @strInformalGiftSal " +
                                    "WHERE tblRecords.lngRecordID=" + lngRECORDID.ToString();

                            cmdUpdate.CommandText = strSQL;
                            cmdUpdate.Parameters.Clear();

                            cmdUpdate.Parameters.AddWithValue("@blnUseMORAddress", chkUseMORAddress.Checked);
                            cmdUpdate.Parameters.AddWithValue("@strFirstName", txtFName.Text);
                            cmdUpdate.Parameters.AddWithValue("@strLastCoName", txtLName.Text);
                            cmdUpdate.Parameters.AddWithValue("@strMI", txtMI.Text);
                            cmdUpdate.Parameters.AddWithValue("@strCompanyName", txtCompanyName.Text);
                            cmdUpdate.Parameters.AddWithValue("@txtParentSalutation", txtParentSalutation.Text);
                            cmdUpdate.Parameters.AddWithValue("@strInformalParentSal", txtInformalParentSal.Text);
                            cmdUpdate.Parameters.AddWithValue("@strFormalGiftSal", txtFormalGiftSal.Text);
                            cmdUpdate.Parameters.AddWithValue("@strInformalGiftSal", txtInformalGiftSal.Text);

                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        strSQL = "UPDATE tblRecords " +
                                "SET tblRecords.blnUseMORAddress = @blnUseMORAddress, " +
                                    "tblRecords.lngStateID = @lngStateID, " +
                                    "tblRecords.strFirstName = @strFirstName, tblRecords.strLastCoName = @strLastCoName, tblRecords.strMI = @strMI, tblRecords.strCompanyName = @strCompanyName, tblRecords.strAddress = @strAddress, tblRecords.strCity = @strCity, tblRecords.strZip = @strZip, tblRecords.txtParentSalutation = @txtParentSalutation, tblRecords.strInformalParentSal = @strInformalParentSal, tblRecords.strFormalGiftSal = @strFormalGiftSal, tblRecords.strInformalGiftSal = @strInformalGiftSal " +
                                "WHERE tblRecords.lngRecordID=" + lngRECORDID.ToString();

                        using (OleDbCommand cmdUpdate = new OleDbCommand(strSQL, conDB))
                        {
                            cmdUpdate.Parameters.AddWithValue("@blnUseMORAddress", chkUseMORAddress.Checked);
                            cmdUpdate.Parameters.AddWithValue("@lngStateID", lngStateID);
                            cmdUpdate.Parameters.AddWithValue("@strFirstName", txtFName.Text);
                            cmdUpdate.Parameters.AddWithValue("@strLastCoName", txtLName.Text);
                            cmdUpdate.Parameters.AddWithValue("@strMI", txtMI.Text);
                            cmdUpdate.Parameters.AddWithValue("@strCompanyName", txtCompanyName.Text);
                            cmdUpdate.Parameters.AddWithValue("@strAddress", txtAddress.Text);
                            cmdUpdate.Parameters.AddWithValue("@strCity", txtCity.Text);
                            cmdUpdate.Parameters.AddWithValue("@strZip", txtZip.Text);
                            cmdUpdate.Parameters.AddWithValue("@txtParentSalutation", txtParentSalutation.Text);
                            cmdUpdate.Parameters.AddWithValue("@strInformalParentSal", txtInformalParentSal.Text);
                            cmdUpdate.Parameters.AddWithValue("@strFormalGiftSal", txtFormalGiftSal.Text);
                            cmdUpdate.Parameters.AddWithValue("@strInformalGiftSal", txtInformalGiftSal.Text);

                            cmdUpdate.ExecuteNonQuery();
                        }
                    }

                    conDB.Close();
                }

                blnRes = true;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("There was an error saving the record: " + ex.Message + "\n\nWould you like to continue closing this screen?", "CampTrak Software", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    blnRes = true;
            }

            return blnRes;
        }

        private void txtFindByName_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool blnEnter = false;

            try { blnEnter = (e.KeyChar == '\r'); }
            catch { blnEnter = false; }

            if (blnEnter)
            {
                clsIR irToSearch = new clsIR(0, 0, "", "", "", "", "", "", "", "", "", "");

                long lngFindIR = 0;

                using (frmFindIR objFindIR = new frmFindIR(irToSearch, "Find record", true, txtFindByName.Text))
                {
                    if (objFindIR.ShowDialog() == DialogResult.OK)
                    {
                        lngFindIR = objFindIR.irToSearch.lngRecordID;

                        if (lngFindIR > 0)
                            subLoadRecord(lngFindIR);
                    }
                    else
                        return;
                }

                txtFindByName.Text = "";
            }
        }
    }
}