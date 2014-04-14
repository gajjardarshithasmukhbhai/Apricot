using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Bank_ID { get; set; }

        [StringLength(75)]
        [Required]
        [Display(Name="Bank Name")]
        public String Bank_Name { get; set; }

        [StringLength(75)]
        [Required]
        [Display(Name="Branch Name")]
        public String Branch_Name { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name="Branch City")]
        public String Branch_City { get; set; }
    }
}
