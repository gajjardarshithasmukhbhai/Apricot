using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Employee_Detail
    {
        [Key]
        public Int64 Emp_ID { get; set; }

        [ForeignKey("Emp_ID")]
        public virtual Employee Employee { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name="Address")]
        public String Emp_Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name="Contact Number")]
        public String Emp_Contact_No { get; set; }

        [Display(Name="Gender")]
        [Required]
        public Char Emp_Gender { get; set; }
    }
}
