using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Dept_ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Department Name")]
        public String Dept_Name { get; set; }
    }
}
