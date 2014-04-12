using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Bill_FM
    {
        [Key]
        public Int64 Bill_FM_ID { get; set; }

        [ForeignKey("Bill_FM_ID")]
        public virtual Employee FinanceManager { get; set; }

        [Key]
        public Int64 Bill_ID { get; set; }

        [ForeignKey("Bill_ID")]
        public virtual Bill Bill { get; set; }
    }
}
