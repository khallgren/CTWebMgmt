using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.ContactInfo.AddStdV2
{
    public partial class frmAddStdV2 : Form
    {
        BindingSource srcRecords = new BindingSource();
        OleDbDataAdapter daRecords = new OleDbDataAdapter();

        string strListName = "";
        wsMDSmartMoverV2B.SmartMover mdSmartMover = null;

        public frmAddStdV2(string _strListName)
        {
            InitializeComponent();
            strListName = _strListName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAddStd_1_Load(object sender, EventArgs e)
        {
            subPopTickedRecords();

            grdRecords.DataSource = srcRecords;
            subFillGrid();
        }

        private void subPopTickedRecords()
        {
            //load table to expand ticked record set
            //this lets us send other mor members to be validated
            //needed to catch individual moves out of households

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                //first step is to clear current ids
                strSQL = "DELETE tblNCOAIDsToProcess.lngRecordID " +
                        "FROM tblNCOAIDsToProcess";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    cmdDB.ExecuteNonQuery();

                    //insert ticked records
                    strSQL = "INSERT INTO tblNCOAIDsToProcess ( lngRecordID ) " +
                            "SELECT tblRecords.lngRecordID " +
                            "FROM tblRecords " +
                            "WHERE tblRecords.blnTick=True";

                    cmdDB.CommandText = strSQL;

                    cmdDB.ExecuteNonQuery();

                    //insert members of ticked record's primary mor if:
                    //ticked record is using mor address AND 
                    //member is using mor address
                    if (!chkTickedOnly.Checked)
                    {
                        strSQL = "INSERT INTO tblNCOAIDsToProcess ( lngRecordID ) " +
                                "SELECT tblRecords_MORMembers.lngRecordID " +
                                "FROM ((tblRecords " +
                                    "INNER JOIN tblnkMORIR ON tblRecords.lngPrimaryMORID = tblnkMORIR.lngMORID) " +
                                    "INNER JOIN tblRecords AS tblRecords_MORMembers ON (tblRecords_MORMembers.lngPrimaryMORID = tblnkMORIR.lngMORID) AND " +
                                        "(tblnkMORIR.lngRecordID = tblRecords_MORMembers.lngRecordID)) " +
                                    "LEFT JOIN tblNCOAIDsToProcess ON tblRecords_MORMembers.lngRecordID = tblNCOAIDsToProcess.lngRecordID " +
                                "WHERE tblRecords.blnUseMORAddress=True AND " +
                                    "tblRecords_MORMembers.blnUseMORAddress=True AND " +
                                    "tblRecords.blnTick=True AND " +
                                    "tblNCOAIDsToProcess.lngRecordID Is Null " +
                                "GROUP BY tblRecords_MORMembers.lngRecordID;";

                        cmdDB.CommandText = strSQL;

                        cmdDB.ExecuteNonQuery();
                    }

                    if (chkOnlyNonCertified.Checked)
                    {
                        //remove records processed previously
                        strSQL = "DELETE lngRecordID " +
                                "FROM tblNCOAIDsToProcess " +
                                "WHERE lngRecordID IN " +
                                    "(SELECT lngRecordID " +
                                    "FROM tblRecordCert " +
                                    "WHERE DateDiff(\"d\", tblRecordCert.dteProcessed, Now)<=95 OR " +
                                        "tblRecordCert.strStatus=\"Error\" OR " +
                                        "IsNull(tblRecordCert.dteProcessed))";

                        cmdDB.CommandText = strSQL;
                        cmdDB.Parameters.Clear();

                        try { cmdDB.ExecuteNonQuery(); }
                        catch { }
                    }
                }

                conDB.Close();
            }
        }

        private void subFillGrid()
        {
            subPopTickedRecords();

            try
            {
                string strSQL = "";

                strSQL = "SELECT tblRecords.lngRecordID, " +
                            "IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strCompanyName, IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) AS strFName, IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) AS strLName, IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])<>\"\" And IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName])<>\"\",\" - \",\"\") & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & \" \" & IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) AS strName, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORAddress1,tblRecords.strAddress) AS strAddress, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORCity,tblRecords.strCity) AS strCity, IIf(tblRecords.blnUseMORAddress=True,tlkpStates_1.strState,tlkpStates.strState) AS strState, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORZip,tblRecords.strZip) AS strZip " +
                        "FROM (((tblRecords " +
                            "INNER JOIN tblNCOAIDsToProcess ON tblRecords.lngRecordID = tblNCOAIDsToProcess.lngRecordID) " +
                            "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                            "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                            "LEFT JOIN tlkpStates AS tlkpStates_1 ON tblMOR.lngMORStateID = tlkpStates_1.lngStateID " +
                        "ORDER BY tblRecords.strLastCoName, tblRecords.strFirstName, tblRecords.strCompanyName;";

                // Create a new data adapter based on the specified query.
                daRecords = new OleDbDataAdapter(strSQL, clsAppSettings.GetAppSettings().strCTConn);
                // Populate a new data table and bind it to the BindingSource.
                DataTable tblRecords = new DataTable();

                daRecords.Fill(tblRecords);
                srcRecords.DataSource = tblRecords;

                grdRecords.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                lblRecordCount.Text = "Record Count: " + grdRecords.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmAddStd_1.subFillGrid", ex);
            }
        }

        private void chkOnlyNonCertified_CheckedChanged(object sender, EventArgs e)
        {
            subFillGrid();
        }

        private void chkTickedOnly_CheckedChanged(object sender, EventArgs e)
        {
            subFillGrid();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            int intRecordsToProcess = 0;

            intRecordsToProcess = fcnGetProcessRecordCount();

            //verify process
            DialogResult dlgConf = MessageBox.Show("Process " + intRecordsToProcess.ToString() + " records?", "Confirm Process", MessageBoxButtons.OKCancel);

            if (dlgConf == System.Windows.Forms.DialogResult.Cancel) return;

            string strMDFileName = "";

            try
            {
                string strFileLoc = System.IO.Path.GetDirectoryName(Properties.Settings.Default.MainPath);
                strMDFileName = strFileLoc + "\\" + strListName + "_MDCertRes.txt";
            }
            catch { strMDFileName = null; }

            lstStatus.Items.Insert(0, "Started address standardization");
            lstStatus.Items.Insert(0, "Melissa Data results will be written to file " + strMDFileName);
            Application.DoEvents();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //process, show results
                //new SmartMover
                mdSmartMover = new global::CTWebMgmt.wsMDSmartMoverV2B.SmartMover();

                //Construct RequestArray and fill in Request details
                wsMDSmartMoverV2B.RequestArray mdRequestArray = fcnNewRequestArray();

                //first initialize array to 100, max you can send in at 1 time
                mdRequestArray.Record = new global::CTWebMgmt.wsMDSmartMoverV2B.RequestRecord[100];

                if (mdRequestArray == null)
                {
                    MessageBox.Show("Error. Please Check ErrorLog");
                    return;
                }

                //Read records
                string strSQL = "";

                int count = 0;

                int intCycleCount = 0;

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    strSQL = "SELECT tblRecords.lngRecordID, " +
                                    "IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) AS strCompanyName, IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) AS strFName, IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) AS strLName, IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName]) & IIf(IIf(IsNull([tblRecords].[strCompanyName]),\"\",[tblRecords].[strCompanyName])<>\"\" And IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName])<>\"\",\" - \",\"\") & IIf(IsNull([tblRecords].[strFirstName]),\"\",[tblRecords].[strFirstName]) & \" \" & IIf(IsNull([tblRecords].[strLastCoName]),\"\",[tblRecords].[strLastCoName]) AS strName, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORAddress1,tblRecords.strAddress) AS strAddress, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORCity,tblRecords.strCity) AS strCity, IIf(tblRecords.blnUseMORAddress=True,tlkpStates_1.strState,tlkpStates.strState) AS strState, IIf(tblRecords.blnUseMORAddress=True,tblMOR.strMORZip,tblRecords.strZip) AS strZip " +
                                "FROM (((tblRecords " +
                                    "INNER JOIN tblNCOAIDsToProcess ON tblRecords.lngRecordID = tblNCOAIDsToProcess.lngRecordID) " +
                                    "LEFT JOIN tblMOR ON tblRecords.lngPrimaryMORID = tblMOR.lngMORID) " +
                                    "LEFT JOIN tlkpStates ON tblRecords.lngStateID = tlkpStates.lngStateID) " +
                                    "LEFT JOIN tlkpStates AS tlkpStates_1 ON tblMOR.lngMORStateID = tlkpStates_1.lngStateID " +
                                "ORDER BY tblRecords.strLastCoName, tblRecords.strFirstName, tblRecords.strCompanyName;";

                    using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                    {
                        using (OleDbDataReader drIR = cmdDB.ExecuteReader())
                        {
                            while (drIR.Read())
                            {
                                //if we reach 100 records, process batch through Smartmover
                                if (count == 100)
                                {
                                    lstStatus.Items.Insert(0, "Sending records " + ((100 * intCycleCount) + 1).ToString() + "-" + ((intCycleCount + 1) * 100).ToString() + " of " + intRecordsToProcess.ToString());
                                    Application.DoEvents();

                                    intCycleCount++;

                                    mdRequestArray.TotalRecords = 100;

                                    wsMDSmartMoverV2B.ResponseArray respArray = fcnSubmitRequest(mdRequestArray, strMDFileName == "" ? null : strMDFileName);

                                    if (respArray == null)
                                    {
                                        MessageBox.Show("Error Processing request. Consider increasing the timeout");
                                        return;
                                    }
                                    if (!fcnProcessResponseArray(respArray))
                                    {
                                        return;
                                    }

                                    //start new RequestArray
                                    mdRequestArray = fcnNewRequestArray();
                                    if (mdRequestArray == null)
                                    {
                                        MessageBox.Show("Error. Please Check ErrorLog");
                                        return;
                                    }

                                    //initialize array to 100, max you can send in at 1 time
                                    mdRequestArray.Record = new global::CTWebMgmt.wsMDSmartMoverV2B.RequestRecord[100];

                                    count = 0;
                                }

                                //fill record request
                                wsMDSmartMoverV2B.RequestRecord reqRecord = new global::CTWebMgmt.wsMDSmartMoverV2B.RequestRecord();

                                reqRecord.RecordID = Convert.ToString(drIR["lngRecordID"]);
                                reqRecord.Company = Convert.ToString(drIR["strCompanyName"]);
                                reqRecord.FullName = Convert.ToString(drIR["strFName"]) + " " + Convert.ToString(drIR["strLName"]);
                                reqRecord.Address1 = Convert.ToString(drIR["strAddress"]);
                                reqRecord.City = Convert.ToString(drIR["strCity"]);
                                reqRecord.State = Convert.ToString(drIR["strState"]);
                                reqRecord.Zip = Convert.ToString(drIR["strZip"]);

                                //add to RequestArray
                                mdRequestArray.Record[count] = reqRecord;
                                count++;
                            }

                            drIR.Close();
                        }
                    }

                    conDB.Close();
                }

                //If there are remaining records, process them
                if (count > 0)
                {
                    lstStatus.Items.Insert(0, "Sending records " + ((100 * intCycleCount) + 1).ToString() + "-" + ((100 * intCycleCount) + count).ToString() + " of " + intRecordsToProcess.ToString());
                    Application.DoEvents();

                    mdRequestArray.TotalRecords = count;

                    wsMDSmartMoverV2B.ResponseArray respArray = fcnSubmitRequest(mdRequestArray, strMDFileName == "" ? null : strMDFileName);
                    if (respArray == null)
                    {
                        MessageBox.Show("Error Processing request. Consider increasing the timeout");
                        return;
                    }
                    if (!fcnProcessResponseArray(respArray))
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing addresses: " + ex.Message);
            }

            lstStatus.Items.Insert(0, "Processing complete!");
            Application.DoEvents();

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Processing complete!");

            using (frmAddStdV2Results objAddStdV2Results = new frmAddStdV2Results(strListName))
            {
                objAddStdV2Results.Location = new System.Drawing.Point(this.Location.X, this.Location.Y);
                objAddStdV2Results.ShowDialog();
            }

            Close();
        }

        private int fcnGetProcessRecordCount()
        {
            int intRes = 0;
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT COUNT(lngRecordID) " +
                        "FROM tblNCOAIDsToProcess";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { intRes = Convert.ToInt32(cmdDB.ExecuteScalar()); }
                    catch { intRes = 0; }
                }

                conDB.Close();
            }

            return intRes;
        }

        private wsMDSmartMoverV2B.RequestArray fcnNewRequestArray()
        {
            try
            {
                int intCustomerID = 0;

                using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
                {
                    conDB.Open();

                    string strSQL = "SELECT strMDCustomerID " +
                            "FROM tblCampDefaults";

                    using (OleDbCommand cmdDef = new OleDbCommand(strSQL, conDB))
                    {
                        try { intCustomerID = Convert.ToInt32(cmdDef.ExecuteScalar()); }
                        catch { intCustomerID = 0; }

                    }

                    conDB.Close();
                }

                wsMDSmartMoverV2B.RequestArray mdRequestArray = new global::CTWebMgmt.wsMDSmartMoverV2B.RequestArray();
                mdRequestArray.CustomerID = intCustomerID;
                mdRequestArray.OptAddressParsed = false;// chkParse.Checked;
                mdRequestArray.OptSmartMoverDetail = false;// chkDetailed.Checked;
                mdRequestArray.OptSmartMoverListName = strListName;// txtListName.Text;
                //     MessageBox.Show("Defaulting to monthly processing...");
                mdRequestArray.OptSmartMoverListOwnerFreqProcessing = 2;// Int32.Parse(numFrequency.Value.ToString());
                mdRequestArray.OptSmartMoverNumberOfMonthsRequested = 6;// Int32.Parse(numRequested.Value.ToString());
                //if (cboProcessingType.SelectedText == "Business Only")
                //                    mdRequestArray.OptSmartMoverProcessingType = SmartMover_sample.smartmover.ProcessingType.Business;
                //                else if (cboProcessingType.SelectedText == "Individuals Only")
                //                    mdRequestArray.OptSmartMoverProcessingType = SmartMover_sample.smartmover.ProcessingType.Individual;
                //                else if (cboProcessingType.SelectedText == "Business and Individuals")
                //                    mdRequestArray.OptSmartMoverProcessingType = SmartMover_sample.smartmover.ProcessingType.IndividualAndBusiness;
                //                else if (cboProcessingType.SelectedText == "Residential")
                //                    mdRequestArray.OptSmartMoverProcessingType = SmartMover_sample.smartmover.ProcessingType.Residential;
                //                else
                mdRequestArray.OptSmartMoverProcessingType = global::CTWebMgmt.wsMDSmartMoverV2B.ProcessingType.Standard;

                return mdRequestArray;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmAddStd_3.NewRequestArray", ex);
                return null;
            }
        }

        public wsMDSmartMoverV2B.ResponseArray fcnSubmitRequest(wsMDSmartMoverV2B.RequestArray reqArray, string serializeToFile)
        {
            //check parameters
            if (reqArray == null)
                throw new ArgumentNullException("reqArray cannot be Null");
            else if (reqArray.Record.Length <= 0)
                throw new ArgumentException("RequestArray does not have any records");
            else if (serializeToFile == string.Empty)
                throw new ArgumentException("serializeToFile parameter must be null or a non-empty string");

            //get timeout and retry settings
            int timeout = 30;
            int retries = 10;

            //submit request
            wsMDSmartMoverV2B.ResponseArray respArray = null;
            int retryCount = 0;
            mdSmartMover.Timeout = timeout * 1000;
            bool found = false;

            do
            {
                try
                {
                    respArray = mdSmartMover.DoSmartMover(reqArray);
                    if (respArray.Fault.Code.StartsWith("WSE00"))
                    {
                        //InternalError. Likely due to traffic spike so keep retrying.
                        //clsErr.subLogErr("frmAddStd_3.fcnSubmitRequest",null);
                        retryCount++;
                        found = false;
                    }
                    else
                    {
                        found = true;

                        //serialize if requested
                        if (serializeToFile != null)
                        {
                            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(respArray.GetType());
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(serializeToFile, true);
                            ser.Serialize(sw, respArray);
                            sw.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    retryCount++;
                    clsErr.subLogErr("frmAddStd_3.fcnSubmitRequest", ex);
                }
            } while (retryCount < retries && found == false);

            return respArray;
        }

        private bool fcnProcessResponseArray(wsMDSmartMoverV2B.ResponseArray respArray)
        {
            //check for Fault
            if (respArray.Fault.Code != "")
            {
                //handle the error
                MessageBox.Show("Error! \n" + respArray.Fault.Desc);
                return false;
            }

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand())
                {
                    cmdDB.Connection = conDB;

                    int count = Int32.Parse(respArray.TotalRecords);
                    for (int x = 0; x < count; x++)
                    {
                        string strAddress = respArray.Record[x].Address.Address1 + " " + respArray.Record[x].Address.Suite + " " + respArray.Record[x].Address.PrivateMailBox;
                        string strStatus = "";
                        if (respArray.Record[x].Address.Result.Status.Code == "0")
                            strStatus = "Error";
                        else if (respArray.Record[x].Address.Result.Status.Code == "1")
                            strStatus = "Moved";
                        else if (respArray.Record[x].Address.Result.Status.Code == "2")
                            strStatus = "Standardized";

                        string strCity = respArray.Record[x].Address.City.Name;
                        string strState = respArray.Record[x].Address.State.Abbreviation;
                        string strZip = respArray.Record[x].Address.Zip + "-" + respArray.Record[x].Address.Plus4;
                        string strErrorCode = respArray.Record[x].Address.Result.Error.Code;
                        string strErrorDesc = respArray.Record[x].Address.Result.Error.Description;
                        string strMoveDate = respArray.Record[x].Address.Move.EffectiveDate;
                        string strMoveReturnCode = respArray.Record[x].Address.Move.Return.Code;
                        string strMoveReturnDesc = respArray.Record[x].Address.Move.Return.Description;
                        string strMoveTypeCode = respArray.Record[x].Address.Move.Type.Code;
                        string strMoveTypeDesc = respArray.Record[x].Address.Move.Type.Description;

                        long lngRecordID = Convert.ToInt32(respArray.Record[x].RecordID);

                        //add/edit record in results table
                        strSQL = "SELECT tblRecordCert.blnReconciled, " +
                                    "tblRecordCert.lngRecordID, " +
                                    "tblRecordCert.dteProcessed, " +
                                    "tblRecordCert.strErrorCode, tblRecordCert.strErrorDesc, tblRecordCert.strStatus, tblRecordCert.strMoveDate, tblRecordCert.strMoveReturnCode, tblRecordCert.strMoveReturnDesc, tblRecordCert.strMoveTypeCode, tblRecordCert.strMoveTypeDesc, tblRecordCert.strListName, tblRecordCert.strAddress, tblRecordCert.strCity, tblRecordCert.strState, tblRecordCert.strZip, tblRecordCert.strTicketID " +
                                "FROM tblRecordCert " +
                                "WHERE tblRecordCert.lngRecordID=" + lngRecordID;

                        cmdDB.Parameters.Clear();
                        cmdDB.CommandText = strSQL;

                        using (OleDbDataAdapter daIRCert = new OleDbDataAdapter())
                        {
                            daIRCert.SelectCommand = cmdDB;
                            using (OleDbCommandBuilder cbdIRCert = new OleDbCommandBuilder(daIRCert))
                            {
                                using (DataSet dsIRCert = new DataSet())
                                {
                                    daIRCert.Fill(dsIRCert);

                                    using (DataTable dtIRCert = dsIRCert.Tables[0])
                                    {
                                        DataRow rowToUpdate;

                                        bool blnAdd = false;

                                        if (dtIRCert.Rows.Count > 0)
                                            rowToUpdate = dtIRCert.Rows[0];
                                        else
                                        {
                                            rowToUpdate = dtIRCert.NewRow();
                                            blnAdd = true;
                                        }

                                        rowToUpdate["blnReconciled"] = false;
                                        rowToUpdate["lngRecordID"] = lngRecordID;
                                        rowToUpdate["dteProcessed"] = DateTime.Now;
                                        rowToUpdate["strErrorCode"] = strErrorCode;
                                        rowToUpdate["strErrorDesc"] = strErrorDesc;
                                        rowToUpdate["strStatus"] = strStatus;
                                        rowToUpdate["strMoveDate"] = strMoveDate;
                                        rowToUpdate["strMoveReturnCode"] = strMoveReturnCode;
                                        rowToUpdate["strMoveReturnDesc"] = strMoveReturnDesc;
                                        rowToUpdate["strMoveTypeCode"] = strMoveTypeCode;
                                        rowToUpdate["strMoveTypeDesc"] = strMoveTypeDesc;
                                        rowToUpdate["strListName"] = strListName;
                                        rowToUpdate["strAddress"] = strAddress;
                                        rowToUpdate["strCity"] = strCity;
                                        rowToUpdate["strState"] = strState;
                                        rowToUpdate["strZip"] = strZip;
                                        //rowToUpdate["strTicketID"] = clsNav.objAddStd_2.txtTicketID.Text;

                                        if (blnAdd) dtIRCert.Rows.Add(rowToUpdate);

                                        daIRCert.Update(dtIRCert);
                                    }
                                }
                            }
                        }
                    }
                }

                conDB.Close();
            }

            return true;
        }
    }
}