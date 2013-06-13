using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Ind.Messaging
{
    public partial class frmImgUL : Form
    {
        public frmImgUL()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlgFile = new OpenFileDialog())
            {
                dlgFile.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
                dlgFile.RestoreDirectory = true;

                if (dlgFile.ShowDialog() == DialogResult.OK)
                {
                    string strFile = "";

                    try { strFile = dlgFile.FileName; }
                    catch { strFile = ""; }

                    if (strFile != "")
                    {
                        bool blnAlreadyInList = false;

                        for (int intI = 0; intI < lstFiles.Items.Count; intI++)
                            if (lstFiles.Items[intI].ToString() == strFile) blnAlreadyInList = true;

                        if (!blnAlreadyInList)
                            lstFiles.Items.Insert(0, strFile);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlgDir = new FolderBrowserDialog())
            {
                dlgDir.ShowNewFolderButton = false;

                if (dlgDir.ShowDialog() == DialogResult.OK)
                {
                    string strDir = "";

                    try { strDir = dlgDir.SelectedPath; }
                    catch { strDir = ""; }

                    if (strDir != "")
                    {
                        string[] strFiles;

                        strFiles = System.IO.Directory.GetFiles(strDir);

                        foreach (string strFile in strFiles)
                        {
                            if (strFile != "")
                            {
                                bool blnIsImg = false;

                                string strExt = "";

                                try { strExt = strFile.Substring(strFile.LastIndexOf("."), strFile.Length - strFile.LastIndexOf(".")).ToLower(); }
                                catch { strExt = ""; }

                                if (strExt == ".jpg" || strExt == ".bmp" || strExt == ".jpeg" || strExt == ".gif") blnIsImg = true;

                                if (blnIsImg)
                                {
                                    bool blnAlreadyInList = false;

                                    for (int intI = 0; intI < lstFiles.Items.Count; intI++)
                                        if (lstFiles.Items[intI].ToString() == strFile) blnAlreadyInList = true;

                                    if (!blnAlreadyInList)
                                        lstFiles.Items.Add(strFile);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            try
            {
                lstFiles.Items.RemoveAt(lstFiles.SelectedIndex);
            }
            catch { }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            lstFiles.Items.Clear();
        }

        private void frmImgUL_Load(object sender, EventArgs e)
        {
            subLoadCbos();
        }

        private void subLoadCbos()
        {
            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tlkpWeekDesc.lngWeekID, " +
                            "tlkpWeekDesc.strWeekDesc " +
                        "FROM tlkpWeekDesc " +
                        "ORDER BY tlkpWeekDesc.intSortOrder, " +
                            "tlkpWeekDesc.dteStartDate";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drCbo = cmdDB.ExecuteReader())
                    {
                        while (drCbo.Read())
                        {
                            clsCboItem cboNewWeek;
                            long lngWeekID = 0;
                            string strWeek = "";

                            try { lngWeekID = Convert.ToInt32(drCbo["lngWeekID"]); }
                            catch { lngWeekID = 0; }

                            try { strWeek = Convert.ToString(drCbo["strWeekDesc"]); }
                            catch { strWeek = ""; }

                            cboNewWeek = new clsCboItem(lngWeekID, strWeek);

                            cboWeek.Items.Add(cboNewWeek);
                        }

                        drCbo.Close();
                    }

                    //program cbo
                    strSQL = "SELECT tlkpCampName.lngCampID, " +
                                "tlkpCampName.strCampName " +
                            "FROM tlkpCampName " +
                            "WHERE tlkpCampName.lngProgramTypeID=1 " +
                            "ORDER BY tlkpCampName.strCampName";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    using (OleDbDataReader drCbo = cmdDB.ExecuteReader())
                    {
                        while (drCbo.Read())
                        {
                            clsCboItem cboNewCamp;
                            long lngCampID = 0;
                            string strCampName = "";

                            try { lngCampID = Convert.ToInt32(drCbo["lngCampID"]); }
                            catch { lngCampID = 0; }

                            try { strCampName = Convert.ToString(drCbo["strCampName"]); }
                            catch { strCampName = ""; }

                            cboNewCamp = new clsCboItem(lngCampID, strCampName);

                            cboProgram.Items.Add(cboNewCamp);
                        }

                        drCbo.Close();
                    }

                    //housing cbo
                    strSQL = "SELECT tlkpHousingName.lngHousingID, " +
                                "tlkpHousingName.strHousingName " +
                            "FROM tlkpHousingName " +
                            "ORDER BY tlkpHousingName.strHousingName";

                    cmdDB.CommandText = strSQL;
                    cmdDB.Parameters.Clear();

                    using (OleDbDataReader drCbo = cmdDB.ExecuteReader())
                    {
                        while (drCbo.Read())
                        {
                            clsCboItem cboNewHousing;
                            long lngHousingID = 0;
                            string strHousingName = "";

                            try { lngHousingID = Convert.ToInt32(drCbo["lngHousingID"]); }
                            catch { lngHousingID = 0; }

                            try { strHousingName = Convert.ToString(drCbo["strHousingName"]); }
                            catch { strHousingName = ""; }

                            cboNewHousing = new clsCboItem(lngHousingID, strHousingName);

                            cboHousing.Items.Add(cboNewHousing);
                        }

                        drCbo.Close();
                    }
                }

                conDB.Close();
            }

        }

        private void btnUL_Click(object sender, EventArgs e)
        {
            bool blnSuccess = true;

            for (int intI = 0; intI < lstFiles.Items.Count; intI++)
            {
                lblStatus.Text = "Uploading file " + (intI + 1).ToString() + " of " + lstFiles.Items.Count.ToString() + " (" + lstFiles.Items[intI].ToString() + ")";
                Application.DoEvents();

                if (!fcnULImg(lstFiles.Items[intI].ToString()))
                {
                    intI = lstFiles.Items.Count + 1;
                    blnSuccess = false;
                }
                else
                {
                    lblStatus.Text += " SUCCESS!";
                    Application.DoEvents();
                }
            }

            if (blnSuccess)
            {
                if (lstFiles.Items.Count == 1)
                    lblStatus.Text = "Successfully uploaded 1 image!";
                else
                    lblStatus.Text = "Successfully uploaded " + lstFiles.Items.Count.ToString() + " images!";

                Application.DoEvents();
            }
        }

        private bool fcnULImg(string _strLocFileToUL)
        {
            bool blnRes = false;

            string strULRes = "";

            try
            {
                System.Net.WebClient cliUL = new System.Net.WebClient();

                byte[] bytULRes = cliUL.UploadFile(clsWebTalk.strPOSTFileURI, _strLocFileToUL);

                // Decode and display the response.
                try { strULRes = System.Text.Encoding.ASCII.GetString(bytULRes); }
                catch { strULRes = "ERR"; }

                using (wsXferEventInfo.XferEventInfo wsXfer = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
                {
                    string strFileName = "";

                    long lngWeekID = 0;
                    long lngCampID = 0;
                    long lngHousingID = 0;

                    try { strFileName = _strLocFileToUL.Substring(_strLocFileToUL.LastIndexOf("\\") + 1, _strLocFileToUL.Length - (_strLocFileToUL.LastIndexOf("\\") + 1)); }
                    catch { strFileName = _strLocFileToUL; }

                    try { lngWeekID = ((clsCboItem)cboWeek.SelectedItem).ID; }
                    catch { lngWeekID = 0; }

                    try { lngCampID = ((clsCboItem)cboProgram.SelectedItem).ID; }
                    catch { lngCampID = 0; }

                    try { lngHousingID = ((clsCboItem)cboHousing.SelectedItem).ID; }
                    catch { lngHousingID = 0; }

                    try 
                    {
                        string strWebDBConn = "Server=localhost;Database=dbCTOnline;UID=WebMailAdmin;PWD=tS3UnhYa;";

                        string strRes = wsXfer.fcnCommitMsgImgUL(strFileName, clsAppSettings.GetAppSettings().lngCTUserID, lngWeekID, lngCampID, lngHousingID, strWebDBConn);

                        if (strRes == "")
                            blnRes = true;
                        else
                        {
                            blnRes = false;
                            lblStatus.Text = "Error committing image " + strRes;
                        }
                    }
                    catch(Exception ex) 
                    {
                        blnRes = false;
                        lblStatus.Text = "ERROR Commiting Image: " + ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ": Error uploading XML file: " + ex.Message;

                Application.DoEvents();
                blnRes = false;
            }

            return blnRes;
        }
    }
}