using Apricot.Data;
using Apricot.Data.Models;
using Apricot.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apricot.Data.Repositories;

namespace Apricot.Web.App_Code
{
    public class FillBillsViewModel
    {
        private ApricotContext _context;

        public FillBillsViewModel(Data.ApricotContext _context)
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
                var manager = _context.Bill_Ms.Where(bm => bm.Bill_ID == bill.Bill_ID).First().Manager.Emp_Name;
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

        public void CreateNewBill(string username, BillViewModel model)
        {
            //get Employee
            EmployeeRepository employeerepo = new EmployeeRepository(_context);
            var employee = employeerepo.GetByEmpNo(username);
            var empId = employee.Emp_ID;

            //Create Bill
            BillRepository billrepo = new BillRepository(_context);
            Bill bill = new Bill();
            bill.Bill_Status = model.BillStatus;
            bill.Emp_ID = empId;
            
            billrepo.AddBill(bill);

            //Create Bill Details
            BillDetailRepository billdetailrepo = new BillDetailRepository(_context);
            Bill_Detail billdetail = new Bill_Detail()
            {
                Bill_Amount = model.BillAmount,
                Bill_Date = model.BillDate,
                Bill_ModeOfPayment = model.ModeOfPayment,
                Bill_Type = model.BillType,
                Bill_ID = bill.Bill_ID,
                Bill_have_SCopy = (model.BillSCopy != null) ? true : false,
            };
            billdetailrepo.AddBillDetail(billdetail);

            //Add Scanned Copy
            if (billdetail.Bill_have_SCopy)
            {
                BillSCopyRepository bscopyrepo = new BillSCopyRepository(_context);
                Bill_SCopy billscopy = new Bill_SCopy()
                {
                    Bill_ID = bill.Bill_ID,
                    SCopy = model.BillSCopy
                };
            }

            //Add Bill Manager
            //get Manager's EmpID
            
            String[] emp_no_name = model.Manager.Split('-');
            var ManagerId = employeerepo.GetByEmpNo(emp_no_name[0]).Emp_ID;

            BillMRepository bmrepo = new BillMRepository(_context);
            
            Bill_M billm = new Bill_M();
            billm.Bill_ID = bill.Bill_ID;
            billm.Emp_ID = ManagerId;

            bmrepo.AddBillM(billm);
        }
    }
}