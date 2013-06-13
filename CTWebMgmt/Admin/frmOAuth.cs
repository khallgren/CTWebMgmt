using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt.Admin
{
    public partial class frmOAuth : Form
    {
        string strAuthURI = "";
        public string strAuthCode = "";

        public frmOAuth(string _strAuthURI)
        {
            InitializeComponent();
            strAuthURI = _strAuthURI;
        }

        private void frmOAuth_Load(object sender, EventArgs e)
        {
            brsOAuth.Navigate(strAuthURI);
        }

        private void brsOAuth_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string strTitle = "";

            try
            {
                strTitle = brsOAuth.Document.Title;

                if (strTitle.IndexOf("code=") > 0)
                    strAuthCode = strTitle.Substring(strTitle.IndexOf("code=") + 5, strTitle.Length - (strTitle.IndexOf("code=") + 5));
                else
                    strAuthCode = "";
            }
            catch { strAuthCode = ""; }

            if (strAuthCode != "")
                Close();
        }
    }
}
