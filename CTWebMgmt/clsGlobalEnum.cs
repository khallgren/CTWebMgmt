using System;
using System.Collections.Generic;
using System.Text;

namespace CTWebMgmt
{
    public class clsGlobalEnum
    {
        public enum conLIVECHARGE
        {

            None = 0,
            XCharge = 1,
            CashLinq = 2,
            XChargeXML = 3,
            Vanco = 4,
            EPS = 5
        };

        public enum conPROGRAMTYPE
        {

            IndEvent = 1,
            GroupEvent = 2,
            GroupRental = 3
        };

        public enum conCheckExistingRegRes
        {
            DoesntExist,
            Exists,
            Cancel
        };

        public enum conPMTTYPE
        {
            CHECK = 1,
            CC = 2,
            CASH = 3,
            StaffChg = 5,
            SPENDMNY = 7,
            DEPTTRNSFR = 9
        };
    }
}
