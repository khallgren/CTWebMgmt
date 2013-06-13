using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTWebMgmt.Admin
{
    public partial class frmUpdateLog : Form
    {
        public frmUpdateLog()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUpdateLog_Load(object sender, EventArgs e)
        {
            subLoadLog();
        }

        private void subLoadLog()
        {
            StringBuilder stbLog = new StringBuilder();

            stbLog.AppendLine("2013-05-21: Fix for trans types when issuing refunds");
            stbLog.AppendLine("2013-04-04: Fix for inaccurate camper custom flag processing");
            stbLog.AppendLine("2013-04-04: Modified merchant gateway report to more accurately identify un-matched transactions");
            stbLog.AppendLine("");
            stbLog.AppendLine("2013-02-28: Corrected default campaign code for donations attached to registrations");
            stbLog.AppendLine("2013-02-27: User-specific CashLinq fixes");
            stbLog.AppendLine("2013-02-27: Google calendar fix for clearing events");
            stbLog.AppendLine("");
            stbLog.AppendLine("2013-02-22: Added buttons to update individual fields when reconciling record details");
            stbLog.AppendLine("2013-02-22: Overhauled Google Calendar integration to leverage OAuth2 and api version 3");
            stbLog.AppendLine("2013-02-06: Added option for all-caps processing under Administration-->System Setup-->Web Settings");
            stbLog.AppendLine("2013-02-06: Updated Melissa Data address standardization process to V2 (No more ticketing system!)");
            stbLog.AppendLine("");
            stbLog.AppendLine("2013-01-15: Improved gender tracking through registration process");
            stbLog.AppendLine("2013-01-09: Added checkbox to allow program to be excluded except for program-specific registration");
            stbLog.AppendLine("2013-01-07: Added custom fields to donor express details screen");
            stbLog.AppendLine("2012-12-31: Added 'Clear Processed Registrations' function to individual event registration processing screen");
            stbLog.AppendLine("2012-12-20: Added mobile phone to individual and group event registrations");
            stbLog.AppendLine("2012-12-20: Adjustments to multi-line custom field functionality");
            stbLog.AppendLine("2012-12-20: Added verification for 'Approval Needed' camper registrations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-12-12: Fixed custom registration flag value download");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-11-04: Added housing availability validation to group event registration");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-09-19: Fix for NCOA alerts in address standardization process");
            stbLog.AppendLine("2012-09-19: Added additional validation for online gift processing");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-07-13: Added file upload for camper messaging system:  Individual Events-->Camper Messaging-->Image Upload");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-06-18: Added support for Element Payment Services to Reconcile Merchant Gateway Transactions report");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-06-04: Added report for reconciling live charge transactions: Administration-->Reports-->Reconcile Merchant Gateway Transactions");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-05-15: Added XCharge Trans ID to web camper detail report");
            stbLog.AppendLine("2012-05-15: Recording deposit and spending transactions for campers added to waiting lists");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-04-24: Added rudimentary Record Information screen");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-04-10: Added error trapping for invalid camper birth dates and registration dates");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-04-04: Added persistence of organization code through registration, regardless of whether it matches a hold or not");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-03-30: Added option for registration holds to be a percentage of the block tuition charge");
            stbLog.AppendLine("2012-03-30: Added ability to have registration holds not connected to a specific block");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-03-28: Fixed Web Camper Detail report duplicate custom field/flag display error for program-specific registrations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-03-19: Added ability to make custom flags and fields required in online use");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-03-08: Fixed card number, custom field issues on Web Camper Detail report");
            stbLog.AppendLine("2012-03-08: Automatically setting confirmation method for online registrations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-02-29: Fixed DOB for group event registration attendees");
            stbLog.AppendLine("2012-02-29: Multiple fixes to primary contact management for group event registration processing");
            stbLog.AppendLine("2012-02-29: Modifications to Donor Express gift detail report");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-02-25: Added batch Web Camper Detail report");
            stbLog.AppendLine("2012-02-25: Modifications to Web Camper Detail report layout");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-02-16: Added birth date to reconcile contact info form");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-24: Ironed out some wrinkles in the MOR functionality, online registrations for individual events");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-20: Removed discounts from block upload, discounts must now be uploaded separately from the discount definition screen");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-18: Fixed discount upload (wasn't uploading values with dollar sign)");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-13: Modified current enrollment, reg hold, waiting list, and capacity calculations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-10: Completed preliminary processing of donor express gifts (Custom field/flag processing is forthcoming)");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-09: Added setting for trans type for custom registration flag charges (Administration-->System Setup-->Web Settings tab)");
            stbLog.AppendLine("2012-01-09: Shifted program-specific custom flag and field definition to CTWeb Manager");
            stbLog.AppendLine("2012-01-09: Shifted discount definition to CTWeb Manager");
            stbLog.AppendLine("");
            stbLog.AppendLine("2012-01-02: Added custom field/flag data to new record, reconcile record functionality");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-12-31: Fixed Special Needs management, registration for individual events");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-12-16: Fixed validation for camper age, registration for individual events");
            stbLog.AppendLine("2011-12-16: Fixed error adding scholarship donation gift through individual event registration");
            stbLog.AppendLine("2011-12-16: Added profile web id to web camper details screen, used for live charge transaction auditing");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-12-06: Refined validation for age, grade, gender, and block stats for individual event registration");
            stbLog.AppendLine("2011-12-06: Fixed error adding address for new donors");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-11-14: Added error trap and auto-correct for expiration dates being added in wrong format for online group event registrations");
            stbLog.AppendLine("2011-11-14: Fixed issue of online donations being re-downloaded after processing");
            stbLog.AppendLine("2011-11-14: Removed themes for compatibility with Windows 7 and 64-bit machines");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-11-10: Added ability to choose if donations should be applied to parent or camper when processing registrations");
            stbLog.AppendLine("2011-11-10: Fixed upload of custom registration flag charges");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-11-09: Modified process of individual event registrations to persist registration date on transactions and gifts");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-10-24: Updated custom flag/field interface: Online Utilities-->Custom Flag/Field Definitions");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-08-10: Updates for CashLinq and X-Charge live refunds and auto-generated pledge payments");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-07-30: Updates for CashLinq live refunds for online registrations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-07-29: Fixed error downloading incomplete group event registrations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-07-11: Fixed time zone conflicts on uploads and downloads");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-06-16: Added credit card auth codes to web camper detail report");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-06-15: Modified google calendar refresh");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-06-09: Fixed problems linking web and local registrations for online balance payment");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-06-08: Fixed error uploading donor giving history");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-05-31: Modified transaction sync to correct spending money transaction problems");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-05-16: Fixed errors with individual/group event definition upload");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-05-12: Extended user feedback for Transaction sync function");
            stbLog.AppendLine("2011-05-12: Fixed timeout errors in Transaction sync function");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-05-04: Added NCOA Summary Report to address standardization menu");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-05-03: Increased size of local 'Home Phone' field to match web definition, preventing download errors");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-04-19: Fixed 'Household Builder' functionality for new campers");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-04-18: Implemented automated refund for CashLinq users when cancelling a registration");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-04-15: Modified download to include X-Charge approval code (in addition to trans id) used for reconciliation reporting");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-04-14: Fixed upload to remove blocks and programs that have been deleted");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-04-08: Implemented charges associated with custom registration flages when processing online registrations");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-03-30: Fixed bug uploading housing areas that don't have a description defined");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-03-29: Added recalc of block stats (current enrollment, capacity, and waiting list) prior to block upload");
            stbLog.AppendLine("");
            stbLog.AppendLine("2011-03-17: Added card number, exp date, and x-charge details for balance payment transactions");
            stbLog.AppendLine("2011-03-17: Added batch detail report for balance payment transactions: Individual Events-->Reports-->Downloaded Trans Batch Report");
            stbLog.AppendLine("2011-03-17:  Corrected date/time modification for non-central time users");
            stbLog.AppendLine("2011-03-17:  Added this update log");

            txtUpdateLog.Text = stbLog.ToString();

            txtUpdateLog.Select(0, 0);
        }
    }
}