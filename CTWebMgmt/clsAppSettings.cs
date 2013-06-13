using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace CTWebMgmt
{
    class clsAppSettings
    {
        private static clsAppSettings CTAppSettings = null;

        public string strCTConn = "";
        public string strCashLinqUName = "";
        public string strXChargePath = "";
        public string strCashLinqPW = "";
        public string strCashLinqCQUser = "";
        public string strCashLinqCQPW = "";
        public string strCashLinqCQMerchantID = "";
        public bool blnDebugMode = false;

        public long lngCTUserID = 0;
        public clsGlobalEnum.conLIVECHARGE lngLiveCharge = clsGlobalEnum.conLIVECHARGE.None;
        
        public string strPOSTFileURI = "https://www.camptrak.com/XferEventInfo/GetFile.aspx";
        public string strWebDBConn = "Data Source=localhost;Initial Catalog=CTWeb;User Id=EventULDL;Password=652NBnUD;";

       // public string strPOSTFileURI = "http://localhost/XferEventInfo/GetFile.aspx";

        private clsAppSettings()
        {
        }

        public static clsAppSettings GetAppSettings()
        {
            if (CTAppSettings == null)
                CTAppSettings = new clsAppSettings();

            return CTAppSettings;
        }

        public void subInitSettings()
        {
            using (wsXferEventInfo.XferEventInfo wsDev = new global::CTWebMgmt.wsXferEventInfo.XferEventInfo())
            {
                try
                {
                    if (wsDev.Url.Contains("localhost") ||
                        clsWebTalk.strWebConn.Contains("KINGTUBBY") ||
                        clsWebTalk.strWebConn.Contains("BUNNYLEE") ||
                        clsWebTalk.strPOSTFileURI.Contains("localhost"))
                        System.Windows.Forms.MessageBox.Show("Localhost: Used for development only\n\nwsDev.Url: " + wsDev.Url + "\nclsWebTalk.strWebConn: " + clsWebTalk.strWebConn + "\nclsWebTalk.strPOSTFileURI: " + clsWebTalk.strPOSTFileURI);

                    if (clsLiveCharge.strXChargeURI.Contains("test"))
                        System.Windows.Forms.MessageBox.Show("XCharge URI Test: Used for development only");
                }
                catch { }
            }

            //using ( wsXferEventInfoV2.xfereventinfov2SoapClient svc = new global::CTWebMgmt.wsXferEventInfoV2.xfereventinfov2SoapClient("xfereventinfov2Soap"))
            using (wsXferEventInfoV2.xfereventinfov2 svc = new global::CTWebMgmt.wsXferEventInfoV2.xfereventinfov2())
            {
                try
                {
                    if (svc.Url.Contains("localhost")) System.Windows.Forms.MessageBox.Show("Development settings, wsXferEventInfoV2: " + svc.Url + "\n\n");
                }
                catch { }
            }

            string strSQL = "SELECT lngCTUserID, lngLiveCharge, " +
                                "strCashLinqUName, strXChargePath, strCashLinqPW, strCashLinqCQUser, strCashLinqCQPW, strCashLinqCQMerchantID " +
                            "FROM tblCampDefaults";

            using (OleDbConnection conDB = new OleDbConnection(strCTConn))
            {
                conDB.Open();

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    using (OleDbDataReader drDef = cmdDB.ExecuteReader())
                    {
                        if (drDef.Read())
                        {
                            lngCTUserID = Convert.ToInt32(drDef["lngCTUserID"]);
                            lngLiveCharge = (clsGlobalEnum.conLIVECHARGE)Convert.ToInt32(drDef["lngLiveCharge"]);
                            strCashLinqUName = Convert.ToString(drDef["strCashLinqUName"]);
                            strXChargePath = Convert.ToString(drDef["strXChargePath"]);
                            strCashLinqPW = Convert.ToString(drDef["strCashLinqPW"]);

                            strCashLinqCQUser = Convert.ToString(drDef["strCashLinqCQUser"]);
                            strCashLinqCQPW = Convert.ToString(drDef["strCashLinqCQPW"]);
                            strCashLinqCQMerchantID = Convert.ToString(drDef["strCashLinqCQMerchantID"]);
                        }

                        drDef.Close();
                    }
                }

                conDB.Close();
            }
        }
    }
}