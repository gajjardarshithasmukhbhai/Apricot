using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apricot.Data.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Emp_ID { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name="Empolyee Number")]
        public String Emp_No { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Employee Name")]
        public String Emp_Name { get; set; }

        [Required]
        public Boolean Is_Active { get; set; }

        public Int64 Dept_ID { get; set; }
        
        [ForeignKey("Dept_ID")]
        public virtual Department Department { get; set; }
    }
}
