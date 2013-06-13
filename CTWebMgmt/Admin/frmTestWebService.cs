using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CTWebMgmt.Admin
{
    public partial class frmTestWebService : Form
    {
        public frmTestWebService()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            clsNav.subCloseTestWebService();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int intRes = 0;
            int intA = 0;
            int intB = 0;

            wsXferEventInfo.XferEventInfo wsTest;

            try
            {
                intA = int.Parse(txtIntA.Text);
                intB = int.Parse(txtIntB.Text);

                wsTest = new wsXferEventInfo.XferEventInfo();

                intRes = wsTest.fcnAddInt(intA, intB);

                wsTest.Dispose();

                MessageBox.Show("Returned from web service: " + intRes.ToString());
            }
            catch (Exception ex)
            {
                clsErr.subLogErr("frmTestWebService.btnSubmit_Click", ex);
            }

            wsTest = null;
        }
    }
}
