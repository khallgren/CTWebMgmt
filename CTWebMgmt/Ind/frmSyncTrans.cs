using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

using System.Xml;

namespace CTWebMgmt.Ind
{
    public partial class frmSyncTrans : Form
    {
        public frmSyncTrans()
        {
            InitializeComponent();

            string strCTSyncURL = "http://www.camptrak.com/ctsyncinstall/publish.htm";

            lnkCTSync.Links.Add(0, strCTSyncURL.Length - 1, strCTSyncURL);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lnkCTSync_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }
    }
}