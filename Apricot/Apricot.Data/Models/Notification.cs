using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Ntf_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name="Notification Subject")]
        public String Ntf_Subject { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name="Notification Body")]
        public String Ntf_Body { get; set; }

        [Required]
        public Boolean Seen { get; set; }

        [Required]
        public Int64 Emp_ID { get; set; }

        [ForeignKey("Emp_ID")]
        public virtual Employee Employee { get; set; }
    }
}
