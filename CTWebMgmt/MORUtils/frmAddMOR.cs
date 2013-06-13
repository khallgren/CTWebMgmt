using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace CTWebMgmt.MORUtils
{
    public partial class frmAddMOR : Form
    {
        public long lngMORID = 0;
        private long lngRecordWebID = 0;
        private long lngRecordIDCamper = 0;
        private long lngRecordIDParent = 0;

        public frmAddMOR(long _lngRecordWebID, long _lngRecordIDCamper, long _lngRecordIDParent)
        {
            InitializeComponent();

            lngRecordWebID = _lngRecordWebID;
            lngRecordIDCamper = _lngRecordIDCamper;
            lngRecordIDParent = _lngRecordIDParent;

            subAddIRToMemberList(lngRecordIDCamper);
            subAddIRToMemberList(lngRecordIDParent);

            subLoadCbos();
        }

        private void subAddIRToMemberList(long _lngRecordID)
        {
            string strSQL = "";

            strSQL = "SELECT strLastCoName & \", \" & strFirstName AS strName " +
                    "FROM tblRecords " +
                    "WHERE lngRecordID=" + _lngRecordID.ToString();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try
                    {
                        clsCboItem cboNew = new clsCboItem(_lngRecordID, cmdDB.ExecuteScalar().ToString());

                        lstMembers.Items.Add(cboNew);
                    }
                    catch { }
                }

                conDB.Close();
            }
        }

        private void subLoadCbos()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tlkpStates.lngStateID, " +
                            "tlkpStates.strState " +
                        "FROM tlkpStates " +
                        "ORDER BY tlkpStates.strState";

                using (OleDbCommand cmdCbo = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drStates = cmdCbo.ExecuteReader())
                    {
                        while (drStates.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drStates["lngStateID"]), drStates["strState"].ToString());

                            cboMORState.Items.Add(cboNew);
                        }

                        drStates.Close();
                    }

                    strSQL = "SELECT tlkpCountry.lngCountryID, " +
                                "tlkpCountry.strCountry " +
                            "FROM tlkpCountry " +
                            "ORDER BY tlkpCountry.lngCountryID";

                    cmdCbo.CommandText = strSQL;

                    using (OleDbDataReader drCountries = cmdCbo.ExecuteReader())
                    {
                        while (drCountries.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drCountries["lngCountryID"]), drCountries["strCountry"].ToString());

                            cboMORCountry.Items.Add(cboNew);
                        }

                        drCountries.Close();
                    }

                    strSQL = "SELECT tlkpMORTypes.lngMORTypeID, " +
                                "tlkpMORTypes.strMORType " +
                            "FROM tlkpMORTypes " +
                            "ORDER BY tlkpMORTypes.strMORType";

                    cmdCbo.CommandText = strSQL;

                    using (OleDbDataReader drMORTypes = cmdCbo.ExecuteReader())
                    {
                        while (drMORTypes.Read())
                        {
                            clsCboItem cboNew = new clsCboItem(Convert.ToInt32(drMORTypes["lngMORTypeID"]), drMORTypes["strMORType"].ToString());

                            cboMORType.Items.Add(cboNew);
                        }

                        drMORTypes.Close();
                    }
                }

                conDB.Close();
            }

            subLoadPrimaryContact();
        }

        private void subLoadPrimaryContact()
        {
            cboPrimaryContact.Items.Clear();

            //load primary contact cbo from members list
            for (int intI = 0; intI < lstMembers.Items.Count; intI++)
            {
                clsCboItem cboNew = new clsCboItem(((clsCboItem)lstMembers.Items[intI]).ID, ((clsCboItem)lstMembers.Items[intI]).Item);

                cboPrimaryContact.Items.Add(cboNew);
            }
        }

        private void frmAddMOR_Load(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "";

                //load details from record web id
                if (lngRecordWebID > 0)
                {
                    //check caps and convert if necessary
                    bool blnCapsWebProcessing = false;

                    strSQL = "SELECT blnCapsWebProcessing " +
                            "FROM tblCampDefaults";

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            try { blnCapsWebProcessing = Convert.ToBoolean(cmdDB.ExecuteScalar()); }
                            catch { blnCapsWebProcessing = false; }
                        }

                        conDB.Close();
                    }

                    using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                    {
                        conDB.Open();

                        strSQL = "SELECT tblWebRecords.lngStateID, " +
                                    "tblWebRecords.strFatherName, tblWebRecords.strMotherName, tblWebRecords.strLastCoName, tblWebRecords.strCity, tblWebRecords.strAddress, tblWebRecords.strZip, tblWebRecords.strHomePhone, tblWebRecords.strEmail " +
                                "FROM tblWebRecords " +
                                "WHERE tblWebRecords.lngRecordWebID=" + lngRecordWebID.ToString();

                        using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                        {
                            using (OleDbDataReader drMOR = cmdDB.ExecuteReader())
                            {
                                if (drMOR.Read())
                                {
                                    string strMother = "";
                                    string strFather = "";

                                    try { strMother = drMOR["strMotherName"].ToString().Trim(); }
                                    catch { strMother = ""; }

                                    try { strFather = drMOR["strFatherName"].ToString().Trim(); }
                                    catch { strFather = ""; }

                                    if (strMother.Contains(" "))
                                    {
                                        txtMotherFName.Text = strMother.Substring(0, strMother.IndexOf(" ")).Trim();
                                        txtMotherLName.Text = strMother.Substring(strMother.IndexOf(" "), strMother.Length - strMother.IndexOf(" ")).Trim();
                                    }
                                    else
                                        txtMotherFName.Text = strMother.Trim();

                                    if (strFather.Contains(" "))
                                    {
                                        txtFatherFName.Text = strFather.Substring(0, strFather.IndexOf(" ")).Trim();
                                        txtFatherLName.Text = strFather.Substring(strFather.IndexOf(" "), strFather.Length - strFather.IndexOf(" ")).Trim();
                                    }
                                    else
                                        txtFatherFName.Text = strFather.Trim();

                                    txtMORName.Text = drMOR["strLastCoName"].ToString() + " Household, " + drMOR["strCity"].ToString();

                                    //Me.cboPrimaryContact = lngRecordID
                                    txtMORAddress.Text = drMOR["strAddress"].ToString();
                                    txtMORCity.Text = drMOR["strCity"].ToString();
                                    //                    Me.cboMORStateID = Forms!frmWebCamperDetails!cboStateID
                                    txtMORZip.Text = drMOR["strZip"].ToString();
                                    txtMORPhone.Text = drMOR["strHomePhone"].ToString();

                                    txtMOREmail.Text = drMOR["strEmail"].ToString();
                                    txtFatherEmail.Text = drMOR["strEmail"].ToString();
                                    txtMotherEmail.Text = drMOR["strEmail"].ToString();
                                    //Me.cboCountry = 1

                                    //Me.cboMORType = 1                    
                                    long lngStateID = 0;

                                    try { lngStateID = Convert.ToInt32(drMOR["lngStateID"]); }
                                    catch { lngStateID = 0; }

                                    for (int intI = 0; intI < cboMORState.Items.Count; intI++)
                                        if (((clsCboItem)cboMORState.Items[intI]).ID == lngStateID) cboMORState.SelectedIndex = intI;

                                    //cboMORCountry.SelectedIndex = 0;

                                    for (int intI = 0; intI < cboMORType.Items.Count; intI++)
                                        if (((clsCboItem)cboMORType.Items[intI]).Item == "Household") cboMORType.SelectedIndex = intI;

                                    //cboMORState.SelectedValue = lngStateID;                                    
                                }

                                drMOR.Close();
                            }
                        }

                        conDB.Close();
                    }

                    if (blnCapsWebProcessing)
                    {
                        txtMotherFName.Text=txtMotherFName.Text.ToUpper();
                        txtMotherLName.Text = txtMotherLName.Text.ToUpper();
                        txtFatherFName.Text = txtFatherFName.Text.ToUpper();
                        txtFatherLName.Text = txtFatherLName.Text.ToUpper();
                        txtMORName.Text = txtMORName.Text.ToUpper();
                        txtMORAddress.Text = txtMORAddress.Text.ToUpper();
                        txtMORCity.Text = txtMORCity.Text.ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error loading the 'Add MOR' form: " + ex.Message);
            }
        }

        private void btnAddMother_Click(object sender, EventArgs e)
        {
            long lngMotherID = 0;

            clsIR irMother = new clsIR(0, ((clsCboItem)cboMORState.SelectedItem).ID, txtMotherFName.Text, txtMotherLName.Text, "", txtMORAddress.Text, txtMORCity.Text, txtMORZip.Text, txtMotherCellPhone.Text, "", txtMotherCellPhone.Text, txtMotherEmail.Text);
            irMother.blnGender = false;

            using (frmFindIR objFindIR = new frmFindIR(irMother, "Find camper's mother"))
            {
                if (objFindIR.ShowDialog() == DialogResult.OK)
                {
                    if (objFindIR.irToSearch.lngRecordID == 0)
                        return;
                    else
                        lngMotherID = objFindIR.irToSearch.lngRecordID;

                    subAddIRToMemberList(lngMotherID);

                    subLoadPrimaryContact();
                }
                else
                    return;
            }
        }

        private void btnAddFather_Click(object sender, EventArgs e)
        {
            long lngFatherID = 0;

            clsIR irFather = new clsIR(0, ((clsCboItem)cboMORState.SelectedItem).ID, txtFatherFName.Text, txtFatherLName.Text, "", txtMORAddress.Text, txtMORCity.Text, txtMORZip.Text, txtFatherCellPhone.Text, "", txtFatherCellPhone.Text, txtFatherEmail.Text);
            irFather.blnGender = true;

            using (frmFindIR objFindIR = new frmFindIR(irFather, "Find camper's father"))
            {
                if (objFindIR.ShowDialog() == DialogResult.OK)
                {
                    if (objFindIR.irToSearch.lngRecordID == 0)
                        return;
                    else
                        lngFatherID = objFindIR.irToSearch.lngRecordID;

                    subAddIRToMemberList(lngFatherID);

                    subLoadPrimaryContact();
                }
                else
                    return;
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            long lngIRID = 0;

            clsIR irMember = new clsIR(0, ((clsCboItem)cboMORState.SelectedItem).ID, "", "", "", txtMORAddress.Text, txtMORCity.Text, txtMORZip.Text, txtMORPhone.Text, "", "", "");

            using (frmFindIR objFindIR = new frmFindIR(irMember))
            {
                if (objFindIR.ShowDialog() == DialogResult.OK)
                {
                    if (objFindIR.irToSearch.lngRecordID == 0)
                        return;
                    else
                        lngIRID = objFindIR.irToSearch.lngRecordID;

                    subAddIRToMemberList(lngIRID);

                    subLoadPrimaryContact();
                }
                else
                    return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "INSERT INTO tblMOR " +
                            "(lngMORStateID, lngMORCountryID, lngMORTypeID, lngPrimaryContactID, lngContactLastModifiedUser, " +
                                "dteContactLastModified, " +
                                "strMORName, strMORAddress1, strMORCity, strMORZip, strMORPhone, strMOREMail ) " +
                            "VALUES " +
                            "(@lngMORStateID, @lngMORCountryID, @lngMORTypeID, @lngPrimaryContactID, @lngContactLastModifiedUser, " +
                                "Now(), " +
                                "@strMORName, @strMORAddress1, @strMORCity, @strMORZip, @strMORPhone, @strMOREMail ) ";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        long lngMORStateID = 0;
                        long lngMORCountryID = 0;
                        long lngMORTypeID = 0;
                        long lngPrimaryContactID = 0;

                        try { lngMORStateID = ((clsCboItem)cboMORState.SelectedItem).ID; }
                        catch { lngMORStateID = 0; }

                        try { lngMORCountryID = ((clsCboItem)cboMORCountry.SelectedItem).ID; }
                        catch { lngMORCountryID = 0; }

                        try { lngMORTypeID = ((clsCboItem)cboMORType.SelectedItem).ID; }
                        catch { lngMORTypeID = 0; }

                        try { lngPrimaryContactID = ((clsCboItem)cboPrimaryContact.SelectedItem).ID; }
                        catch { lngPrimaryContactID = 0; }

                        cmdDB.Parameters.Add(new OleDbParameter("@lngMORStateID", lngMORStateID));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngMORCountryID", lngMORCountryID));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngMORTypeID", lngMORTypeID));
                        cmdDB.Parameters.Add(new OleDbParameter("@lngPrimaryContactID", lngPrimaryContactID));

                        cmdDB.Parameters.Add(new OleDbParameter("@lngContactLastModifiedUser", CTWebMgmt.lngUserID));

                        cmdDB.Parameters.Add(new OleDbParameter("@strMORName", txtMORName.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strMORAddress1", txtMORAddress.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strMORCity", txtMORCity.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strMORZip", txtMORZip.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strMORPhone", txtMORPhone.Text));
                        cmdDB.Parameters.Add(new OleDbParameter("@strMOREMail", txtMOREmail.Text));

                        try { cmdDB.ExecuteNonQuery(); }
                        catch (Exception ex) { }

                        strSQL = "SELECT @@IDENTITY;";

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        try { lngMORID = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                        catch { lngMORID = 0; }

                        long lngRecordID = 0;

                        //add members
                        for (int intI = 0; intI < lstMembers.Items.Count; intI++)
                        {
                            lngRecordID = ((clsCboItem)lstMembers.Items[intI]).ID;

                            strSQL = "INSERT INTO tblnkMORIR " +
                                    "(lngMORID, lngRecordID) " +
                                    "VALUES " +
                                    "(" + lngMORID.ToString() + ", " + lngRecordID.ToString() + ")";

                            cmdDB.CommandText = strSQL;

                            cmdDB.Parameters.Clear();

                            try { cmdDB.ExecuteNonQuery(); }
                            catch  { }

                            //check to see if ir has a primary mor;  if it doesn't update to current mor
                            strSQL = "UPDATE tblRecords " +
                                    "SET tblRecords.lngPrimaryMORID = " + lngMORID.ToString() + " " +
                                    "WHERE tblRecords.lngRecordID=" + lngRecordID.ToString() + " AND " +
                                        "IIf(IsNull([tblRecords].[lngPrimaryMORID]), 0, [tblRecords].[lngPrimaryMORID])=0";

                            cmdDB.Parameters.Clear();
                            cmdDB.CommandText = strSQL;

                            try { cmdDB.ExecuteNonQuery(); }
                            catch { }
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}