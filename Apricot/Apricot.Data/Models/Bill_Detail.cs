using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Models
{
    public class Bill_Detail
    {
        [Key]
        public Int64 Bill_ID { get; set; }

        [ForeignKey("Bill_ID")]
        public virtual Bill Bill { get; set; }

        [Required]
        [Display(Name="Bill Amount")]
        public Double Bill_Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name="Bill Date")]
        public DateTime Bill_Date { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Bill Type")]
        public String Bill_Type { get; set; }

        [Required]
        [Display(Name="Mode of Payment")]
        public ApricotEnums.BillModeOfPaymentEnum Bill_ModeOfPayment { get; set; }

        [Required]
        [Display(Name="Supported by Bill Scanned Copy")]
        public Boolean Bill_have_SCopy { get; set; }
    }
}
