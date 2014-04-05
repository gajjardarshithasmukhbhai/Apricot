using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Bill_ID { get; set; }

        [Required]
        [Display(Name="Bill Status")]
        public ApricotEnums.BillSatusEnum Bill_Status { get; set; }

        public Int64 Emp_ID { get; set; }

        [ForeignKey("Emp_ID")]
        public virtual Employee Employee { get; set; }
    }
}
