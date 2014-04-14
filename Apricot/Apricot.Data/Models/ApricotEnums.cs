using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class ApricotEnums
    {
        public enum BillSatusEnum
        {
            PENDING, APPROVED, REJECTED, CLOSED
        }

        public enum BillModeOfPaymentEnum
        {
            ACCOUNT_TRANSFER, CHEQUE
        }
    }
}
