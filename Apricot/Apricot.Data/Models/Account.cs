using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Account
    {
        [Key]
        public Int64 Emp_ID { get; set; }

        [ForeignKey("Emp_ID")]
        public virtual Employee Employee { get; set; }

        [Required]
        [Display(Name="Account Number")]
        public Int64 Account_No { get; set; }

        public Int64 Bank_ID { get; set; }

        [ForeignKey("Bank_ID")]
        public virtual Bank Bank { get; set; }
    }
}
