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
    public partial class frmProfileDetails : Form
    {
        private long lngProfileID = 0;

        public frmProfileDetails(long _lngProfileID)
        {
            InitializeComponent();

            lngProfileID = _lngProfileID;

            subConfigCustomFields();
            subLoadProfileDetails();
        }

        private void subLoadProfileDetails()
        {
            try
            {
                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    string strSQL = "";

                    strSQL = "SELECT tblWebRecords.blnFlag1, tblWebRecords.blnFlag2, tblWebRecords.blnFlag3, tblWebRecords.blnFlag4, tblWebRecords.blnFlag5, tblWebRecords.blnFlag6, tblWebRecords.blnFlag7, tblWebRecords.blnFlag8, tblWebRecords.blnFlag9, tblWebRecords.blnFlag10, " +
                                "tblWebRecords.strAddress, tblWebRecords.strLastCoName, tblWebRecords.strFirstName, tblWebRecords.strCity, tlkpStates.strState, tblWebRecords.strZip, tblWebRecords.strHomePhone, tblWebRecords.strWorkPhone, tblWebRecords.strCellPhone, tblWebRecords.strWorkExt, tblWebRecords.strEmail, tblWebRecords.strSpouseFName, tblWebRecords.strSpouseLName, tblWebRecords.strSpousePhone, tblWebRecords.strCustom1, tblWebRecords.strCustom2, tblWebRecords.strCustom3, tblWebRecords.strCustom4, tblWebRecords.strCustom5, tblWebRecords.strCustom6, tblWebRecords.strCustom7, tblWebRecords.strCustom8, tblWebRecords.strCustom9, tblWebRecords.strCustom10 " +
                            "FROM tblWebRecords " +
                                "LEFT JOIN tlkpStates ON tblWebRecords.lngStateID = tlkpStates.lngStateID " +
                            "WHERE tblWebRecords.lngRecordWebID=" + lngProfileID.ToString();
                    
                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drIR = cmdDB.ExecuteReader())
                        {
                            if (drIR.Read())
                            {

                                for (int intI = 1; intI <= 10; intI++)
                                {
                                    try { ((CheckBox)Controls["chkFlag" + intI.ToString()]).Checked = Convert.ToBoolean(drIR["blnFlag" + intI.ToString()]); }
                                    catch { }

                                    try { ((TextBox)Controls["txtCustom" + intI.ToString()]).Text = Convert.ToString(drIR["strCustom" + intI.ToString()]); }
                                    catch { }
                                }

                                txtName.Text = Convert.ToString(drIR["strFirstName"]) + " " + Convert.ToString(drIR["strLastCoName"]);
                                txtAddress.Text = Convert.ToString(drIR["strAddress"]);
                                txtCityStateZip.Text = Convert.ToString(drIR["strCity"]) + ", " + Convert.ToString(drIR["strState"]) + " " + Convert.ToString(drIR["strZip"]);
                                txtHomePhone.Text = Convert.ToString(drIR["strHomePhone"]);
                                txtWorkPhone.Text = Convert.ToString(drIR["strWorkPhone"]) + " " + Convert.ToString(drIR["strWorkExt"]);
                                txtCellPhone.Text = Convert.ToString(drIR["strCellPhone"]);
                                txtEMail.Text = Convert.ToString(drIR["strEmail"]);
                                txtSpouseName.Text = Convert.ToString(drIR["strSpouseFName"]) + " " + Convert.ToString(drIR["strSpouseLName"]);
                                txtSpousePhone.Text = Convert.ToString(drIR["strSpousePhone"]);

                            }

                            drIR.Close();
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }
        }

        void subConfigCustomFields()
        {
            try
            {
                string strSQL = "";

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT blnField1, blnField10, blnField2, blnField3, blnField4, blnField5, blnField6, blnField7, blnField8, blnField9, blnFlag1, blnFlag10, blnFlag2, blnFlag3, blnFlag4, blnFlag5, blnFlag6, blnFlag7, blnFlag8, blnFlag9, " +
                                "strField1, strField10, strField2, strField3, strField4, strField5, strField6, strField7, strField8, strField9, strFlag1, strFlag10, strFlag2, strFlag3, strFlag4, strFlag5, strFlag6, strFlag7, strFlag8, strFlag9 " +
                            "FROM tblCustomWebProfileDesc;";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drCustom = cmdDB.ExecuteReader())
                        {
                            if (drCustom.Read())
                            {
                                string strCaption = "";
                                bool blnUsing = false;

                                for (int intI = 1; intI <= 10; intI++)
                                {
                                    try { blnUsing = Convert.ToBoolean(drCustom["blnField" + intI.ToString()]); }
                                    catch { blnUsing = false; }

                                    try { strCaption = Convert.ToString(drCustom["strField" + intI.ToString()]); }
                                    catch { strCaption = ""; }

                                    if (blnUsing && strCaption != "")
                                    {
                                        try
                                        {
                                            ((TextBox)Controls["txtCustom" + intI.ToString()]).Visible = true;
                                            ((Label)Controls["lblCustom" + intI.ToString()]).Visible = true;
                                            ((Label)Controls["lblCustom" + intI.ToString()]).Text = strCaption;
                                        }
                                        catch { }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            ((TextBox)Controls["txtCustom" + intI.ToString()]).Visible = false;
                                            ((Label)Controls["lblCustom" + intI.ToString()]).Visible = false;
                                        }
                                        catch { }
                                    }

                                    try { blnUsing = Convert.ToBoolean(drCustom["blnFlag" + intI.ToString()]); }
                                    catch { blnUsing = false; }

                                    try { strCaption = Convert.ToString(drCustom["strFlag" + intI.ToString()]); }
                                    catch { strCaption = ""; }

                                    if (blnUsing && strCaption != "")
                                    {
                                        try
                                        {
                                            ((CheckBox)Controls["chkFlag" + intI.ToString()]).Visible = true;
                                            ((CheckBox)Controls["chkFlag" + intI.ToString()]).Text = strCaption;
                                        }
                                        catch { }
                                    }
                                    else
                                    {
                                        try { ((CheckBox)Controls["chkFlag" + intI.ToString()]).Visible = false; }
                                        catch { }
                                    }
                                }

                                drCustom.Close();
                            }
                        }
                    }

                    conDB.Close();
                }
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
