using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Bill_SCopy
    {
        [Key]
        public Int64 Bill_ID { get; set; }

        [ForeignKey("Bill_ID")]
        public virtual Bill Bill { get; set; }

        [Required]
        [Display(Name="Scanned Copy of Bill")]
        public Byte[] SCopy { get; set; }
    }
}
