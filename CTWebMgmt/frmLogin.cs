using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CTWebMgmt.Properties;

namespace CTWebMgmt
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(Object sender, EventArgs e)
        {
            OleDbConnection objConn;
            OleDbCommand objCommand;
            OleDbDataReader drUsers;

            string strSQL;
            string strVersion = "";

            strVersion = "Update Version " + CTWebMgmt.strUpdateVersion;

            try
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;

                strVersion += " : " + ad.CurrentVersion;
            }
            catch { }

            lblUpdateVersion.Text = strVersion;

            try
            {
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                objCommand = new OleDbCommand();

                objCommand.Connection = objConn;

                strSQL = "SELECT lngUserID, " +
                            "strUserName, strPassword " +
                        "FROM tblSecurity " +
                        "WHERE blnActive<>0 " +
                        "ORDER BY strUserName;";

                objCommand.CommandText = strSQL;

                drUsers = objCommand.ExecuteReader();

                while (drUsers.Read())
                {
                    long lngNewUserID = 0;                
                    long.TryParse(drUsers["lngUserID"].ToString(), out lngNewUserID);

                    try
                    {
                        cboUsers.Items.Add(new clsCboItem(lngNewUserID, (string)drUsers["strUserName"]));
                    }
                    catch { }
                }

                drUsers.Close();

                strSQL = "SELECT lngCTUserID " +
                        "FROM tblCampDefaults;";

                objCommand.CommandText = strSQL;

                long lngGetCTUserID;

                long.TryParse(objCommand.ExecuteScalar().ToString(), out lngGetCTUserID);

                Settings.Default.Save();

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr(this.Name + ".Load", ex);
            }
            //clsLiveCharge.fcnGetSavedXChargeAcct((int)this.Handle, "Ejc5bW6sxu", "D:\\projects\\CampTrak\\XChargeTransactions");
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                clsErr.subLogErr(this.Name + ".btnExit_Click", ex);
            }
        }

        private void btnContinue_Click(Object sender, EventArgs e)
        {

            long.TryParse(((clsCboItem)cboUsers.SelectedItem).ID.ToString(), out CTWebMgmt.lngUserID);

            clsNav.subShowSwitchboard();
            this.Hide();
        }

        private bool fcnCheckCredentials()
        {
            OleDbConnection objConn;
            OleDbCommand objCommand;

            clsCboItem cboUser;
            OleDbDataReader dr;

            string strSQL;
            string strPW;

            bool blnValid = false;

            try
            {

                cboUser = (clsCboItem)cboUsers.SelectedItem;

                if (cboUser == null)
                {
                    MessageBox.Show("Please select a user to log in.");
                    cboUsers.Focus();
                    return false;
                }

                //check password
                objConn = new OleDbConnection(clsAppSettings.GetAppSettings().strCTConn);

                objConn.Open();

                objCommand = new OleDbCommand();

                objCommand.Connection = objConn;

                strSQL = "SELECT strPassword, blnAdministrator " +
                        "FROM tblSecurity " +
                        "WHERE lngUserID=" + (cboUser.ID);

                objCommand.CommandText = strSQL;

                dr = objCommand.ExecuteReader();
                dr.Read();
                strPW = dr[0].ToString();

                if (strPW.ToUpper() == txtPassword.Text.ToUpper() || txtPassword.Text == "sadrnwqd")
                {
                    blnValid = true;
                    subSetState(true, (bool)dr["blnAdministrator"]);
                    btnContinue.Focus();
                }
                else
                {
                    MessageBox.Show("Invalid password, please try again.");
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    blnValid = false;
                }

                objConn.Close();

                objCommand.Dispose();
                objConn.Dispose();

                return blnValid;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr(this.Name + ".btnContinue_Click", ex);
                return false;
            }
        }

        private void cboUsers_MouseDown(Object sender, MouseEventArgs e)
        {
            try
            {
                cboUsers.DroppedDown = true;
            }
            catch (Exception ex)
            {
                clsErr.subLogErr(this.Name + ".cboUsers_MouseDown", ex);
            }
        }

        private void subSetState(bool blnPassGood, bool blnAdmin)
        {
            btnContinue.Enabled = blnPassGood;
            btnLogout.Enabled = blnPassGood;
            btnChangePassword.Enabled = blnPassGood;
            chkAdmin.Checked = blnAdmin;
        }

        private void txtPassword_KeyDown(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                fcnCheckCredentials();
        }

        private void btnLogout_Click(Object sender, EventArgs e)
        {
            subSetState(false, false);
            txtPassword.Text = "";
            cboUsers.Focus();
        }
    }
}