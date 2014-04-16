using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apricot.Web.Models
{
    public class MyBillViewModel
    {
        [Display(Name = "Bill ID")]
        public Int64 BillID { get; set; }

        [Display(Name = "Bill Status")]
        public ApricotEnums.BillSatusEnum BillStatus { get; set; }

        [Display(Name = "Bill Amount")]
        public Double BillAmount { get; set; }

        [Display(Name = "Bill Date")]
        public DateTime BillDate { get; set; }

        [Display(Name = "Bill Type")]
        public String BillType { get; set; }

        [Display(Name = "Mode of Payment")]
        public ApricotEnums.BillModeOfPaymentEnum ModeOfPayment { get; set; }

        [Display(Name = "Scanned Copy")]
        public byte[] BillSCopy { get; set; }

        [Display(Name = "Manager")]
        public String Manager { get; set; }

        [Display(Name = "Finance Manager")]
        public String FManager { get; set; }

    }
}