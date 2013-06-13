using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;


namespace CTWebMgmt.Donor
{
    public partial class frmULGiftSettings : Form
    {
        public frmULGiftSettings()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseULGiftSettings();
        }

        private void btnULGiftSettings_Click(object sender, EventArgs e)
        {
            string strSQL;
            string strULRes = "";

            //upload gift categories
            strSQL = "SELECT tblGiftCategory.blnUseOnline AS blnActive, " +
                        "tblGiftCategory.lngGiftCategoryID, 0 AS lngGiftCategoryWebID, " + clsAppSettings.GetAppSettings().lngCTUserID+ " AS lngCTUserID, lngOLSortOrder, " +
                        "IIf(IsNull(tblGiftCategory.strOLDesc), \"\", tblGiftCategory.strOLDesc) AS strOLDesc " +
                    "FROM tblGiftCategory ";// +
                    //"WHERE tblGiftCategory.blnUseOnline<>0;";

            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Gift Categories..."));
            Application.DoEvents();

            strULRes += clsWebTalk.fcnUploadData(strSQL, "tblGiftCategory", "lngGiftCategoryID", "lngGiftCategoryWebID", "spAppendGiftCat", true);

            //upload campaigns
            strSQL = "SELECT tlkpCampaignCodes.blnUseOnline AS blnActive, " +
                        "tlkpCampaignCodes.lngCampaignID, 0 AS lngCampaignWebID, " + clsAppSettings.GetAppSettings().lngCTUserID + " AS lngCTUserID, " +
                        "IIf(IsNull(tlkpCampaignCodes.strCampaignCode), \"\", tlkpCampaignCodes.strCampaignCode) AS strCampaignCode, IIf(IsNull(tlkpCampaignCodes.strOLDesc), \"\", tlkpCampaignCodes.strOLDesc) AS strOLDesc " +
                    "FROM tlkpCampaignCodes ";// +
                    //"WHERE tlkpCampaignCodes.blnUseOnline<>0;";

            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Uploading Campaigns..."));
            Application.DoEvents();

            strULRes += clsWebTalk.fcnUploadData(strSQL, "tlkpCampaignCodes", "lngCampaignID", "lngCampaignWebID", "spAppendCampaign", true);

            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Updating camp defaults..."));
            Application.DoEvents();

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT tblCampDefaults.lngCampState, " +
                            "tblCampDefaults.lngTaxID, tblCampDefaults.strCampName, tblCampDefaults.strCampAddress, tblCampDefaults.strCampCity, tblCampDefaults.strCampZip, tblCampDefaults.strCampPhone " +
                        "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                    {
                        if (drDef.Read())
                        {
                            clsWebTalk.subUpdateDef("lngCampStateID", drDef["lngCampState"].ToString(), "long");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": State"));
                            Application.DoEvents();
                            clsWebTalk.subUpdateDef("strCampName", drDef["strCampName"].ToString(), "string");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Camp Name"));
                            Application.DoEvents();
                            clsWebTalk.subUpdateDef("strCampAddress", drDef["strCampAddress"].ToString(), "string");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Address"));
                            Application.DoEvents();
                            clsWebTalk.subUpdateDef("strCampCity", drDef["strCampCity"].ToString(), "string");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": City"));
                            Application.DoEvents();
                            clsWebTalk.subUpdateDef("strCampZip", drDef["strCampZip"].ToString(), "string");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Zip"));
                            Application.DoEvents();
                            clsWebTalk.subUpdateDef("strCampPhone", drDef["strCampPhone"].ToString(), "string");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Phone"));
                            Application.DoEvents();
                            clsWebTalk.subUpdateDef("strTaxID", drDef["lngTaxID"].ToString(), "string");
                            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Tax ID"));
                            Application.DoEvents();
                        }

                        drDef.Close();
                    }
                }

                conDB.Close();
            }

            lstStatus.Items.Insert(0, (DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + ": Finished uploading gift setup information."));
            Application.DoEvents();
        }
    }
}