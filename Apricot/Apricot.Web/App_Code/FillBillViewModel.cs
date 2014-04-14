using Apricot.Data;
using Apricot.Data.Models;
using Apricot.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apricot.Web.App_Code
{
    public class FillBillViewModel
    {
        private ApricotContext _context;

        public FillBillViewModel(Data.ApricotContext _context)
        {
            // TODO: Complete member initialization
            this._context = _context;
        }

        public IEnumerable<BillViewModel> getMyBills(String EmployeeNumber)
        {
            List<BillViewModel> myBills = new List<BillViewModel>(0);
            List<Bill> bills = _context.Bills.Where(b => b.Employee.Emp_No == EmployeeNumber).ToList();

            foreach (var bill in bills)
            {
                Bill_Detail billDetail = _context.Bill_Details.Find(bill.Bill_ID);
                String manager = _context.Employees.Find(bill.Emp_ID).Emp_Name;
                String Fmanager = "Not Avialable";
                var billStatus = bill.Bill_Status;

                if (billStatus == ApricotEnums.BillSatusEnum.CLOSED || billStatus == ApricotEnums.BillSatusEnum.APPROVED)
                {
                    Fmanager = _context.Bill_FMs.Where(bf => bf.Bill_ID == bill.Bill_ID).Select(b => b.FinanceManager.Emp_Name).Single();
                }

                BillViewModel bvm = new BillViewModel();
                
                //There will always be a Entry for a Bill. Just to double Check in case of Incosistent Data
                if (billDetail != null)
                {
                    bvm.BillID = bill.Bill_ID;
                    bvm.BillAmount = billDetail.Bill_Amount;
                    bvm.BillDate = billDetail.Bill_Date;
                    bvm.BillStatus = bill.Bill_Status;
                    bvm.BillType = billDetail.Bill_Type;
                    bvm.ModeOfPayment = billDetail.Bill_ModeOfPayment;
                    bvm.FManager = Fmanager;
                    bvm.Manager = manager;
                    bvm.BillSCopy = (billDetail.Bill_have_SCopy) ? _context.Bill_SCopies.Find(bill.Bill_ID).SCopy : null;
                }

                myBills.Add(bvm);
            }

            return myBills;
        }
    }
}