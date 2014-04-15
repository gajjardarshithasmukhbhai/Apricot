using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apricot.Web.Models
{
    public class FullEmployee
    {
        public Int64 Emp_ID { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Empolyee Number")]
        public String Emp_No { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Employee Name")]
        public String Emp_Name { get; set; }

        [Required]
        public Boolean Is_Active { get; set; }

        public Department Department { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Address")]
        public String Emp_Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        public String Emp_Contact_No { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public Char Emp_Gender { get; set; }
    }
}