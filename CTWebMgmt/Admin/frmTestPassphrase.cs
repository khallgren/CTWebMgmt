using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace CTWebMgmt.Admin
{
    public partial class frmTestPassphrase : Form
    {
        public frmTestPassphrase()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmTestPassphrase_Load(object sender, EventArgs e)
        {
            string strDefPassphrase = "";

            string strSQL = "";

            using (OleDbConnection conDB = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn))
            {
                conDB.Open();

                strSQL = "SELECT strEncPassPhrase " +
                       "FROM tblCampDefaults";

                using (OleDbCommand cmdDB = new OleDbCommand(strSQL, conDB))
                {
                    try { strDefPassphrase = Convert.ToString(cmdDB.ExecuteScalar()); }
                    catch { strDefPassphrase = "ERR"; }
                }

                conDB.Close();
            }

            txtPassphrase.Text = strDefPassphrase;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            //string strIn = txtEncrypted.Text;
            //string strOut = clsEncryption.fcnDecrypt(strIn, txtPassphrase.Text);

            //txtDecrypted.Text = strOut;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            //string strIn = txtDecrypted.Text;
            //string strOut = clsEncryption.fcnEncrypt(strIn, txtPassphrase.Text);

            //txtEncrypted.Text = strOut;
        }
    }
}
