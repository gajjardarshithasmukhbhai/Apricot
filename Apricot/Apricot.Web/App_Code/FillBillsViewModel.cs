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
                bscopyrepo.AddBillSCopy(billscopy);
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

        public BillViewModel getBillDetail(Int64 id)
        {
            BillRepository billrepo = new BillRepository(_context);
            BillDetailRepository billdetailrepo = new BillDetailRepository(_context);
            BillMRepository billmrepo = new BillMRepository(_context);
            BillFMRepository billfmrepo = new BillFMRepository(_context);
            BillSCopyRepository billscopyrepo = new BillSCopyRepository(_context);
            BillViewModel bvm = new BillViewModel();

            Bill bill = billrepo.GetByBillID(id);
            Bill_Detail billdetail = billdetailrepo.GetByBillID(id);
            Bill_M billmanager = billmrepo.GetByBillID(id);
            Bill_FM billfmanager = null;
            Bill_SCopy billscopy= null;
            
            if (bill.Bill_Status == ApricotEnums.BillSatusEnum.APPROVED || bill.Bill_Status == ApricotEnums.BillSatusEnum.CLOSED)
            {
                billfmanager = billfmrepo.GetByBillId(id);
            }

            if (billdetail.Bill_have_SCopy)
                billscopy = billscopyrepo.GetByBillID(id);

            bvm.BillID = bill.Bill_ID;
            bvm.BillStatus = bill.Bill_Status;
            bvm.BillAmount = billdetail.Bill_Amount;
            bvm.BillDate = billdetail.Bill_Date;
            bvm.BillSCopy = null;
            bvm.BillType = billdetail.Bill_Type;
            bvm.FManager = (billfmanager != null) ? billfmanager.FinanceManager.Emp_No : "Not Aviablable";
            bvm.Manager = billmanager.Manager.Emp_Name;
            bvm.ModeOfPayment = billdetail.Bill_ModeOfPayment;

            return bvm;
        }

        public IEnumerable<BillViewModel> getManagerBills(string username)
        {
            EmployeeRepository employeerepo = new EmployeeRepository(_context);
            BillMRepository bmrepo = new BillMRepository(_context);
            List<BillViewModel> managerBillslist = new List<BillViewModel>(0);
            var managerbills = bmrepo.GetAllByBillMID(employeerepo.GetByEmpNo(username).Emp_ID);

            foreach (var billm in managerbills)
            {
                Bill_Detail billDetail = _context.Bill_Details.Find(billm.Bill_ID);
                BillViewModel bvm = new BillViewModel();
                Bill bill = _context.Bills.Find(billm.Bill_ID);
                String fmanager = "Not yet Alloted";
                if(bill.Bill_Status == ApricotEnums.BillSatusEnum.CLOSED || bill.Bill_Status == ApricotEnums.BillSatusEnum.APPROVED)
                {
                    fmanager = _context.Bill_FMs.Where(bfm => bfm.Bill_ID == billDetail.Bill_ID).Select(bfmn => bfmn.FinanceManager.Emp_Name).Single();
                }
                //There will always be a Entry for a Bill. Just to double Check in case of Incosistent Data
                if (billDetail != null)
                {
                    bvm.BillID = billm.Bill_ID;
                    bvm.BillAmount = billDetail.Bill_Amount;
                    bvm.BillDate = billDetail.Bill_Date;
                    bvm.BillStatus = billm.Bill.Bill_Status;
                    bvm.BillType = billDetail.Bill_Type;
                    bvm.ModeOfPayment = billDetail.Bill_ModeOfPayment;
                    bvm.FManager = fmanager;
                    bvm.Manager = billm.Manager.Emp_Name;
                    bvm.BillSCopy = (billDetail.Bill_have_SCopy) ? _context.Bill_SCopies.Find(bill.Bill_ID).SCopy : null;
                }

                managerBillslist.Add(bvm);
            }

            return managerBillslist;
           
        }

        public object getFManagerBills(string username)
        {
            EmployeeRepository employeerepo = new EmployeeRepository(_context);
            BillFMRepository bfmrepo = new BillFMRepository(_context);
            List<BillViewModel> fmanagerBillslist = new List<BillViewModel>(0);
            var fmanagerbills = bfmrepo.GetAllByBillFMID(employeerepo.GetByEmpNo(username).Emp_ID);

            foreach (var billfm in fmanagerbills)
            {
                Bill_Detail billDetail = _context.Bill_Details.Find(billfm.Bill_ID);
                BillViewModel bvm = new BillViewModel();
                Bill bill = _context.Bills.Find(billfm.Bill_ID);
                String manager = _context.Bill_Ms.Where(bm => bm.Bill_ID == billDetail.Bill_ID).Select(bmn => bmn.Manager.Emp_Name).Single();
                
                //There will always be a Entry for a Bill. Just to double Check in case of Incosistent Data
                if (billDetail != null)
                {
                    bvm.BillID = billfm.Bill_ID;
                    bvm.BillAmount = billDetail.Bill_Amount;
                    bvm.BillDate = billDetail.Bill_Date;
                    bvm.BillStatus = billfm.Bill.Bill_Status;
                    bvm.BillType = billDetail.Bill_Type;
                    bvm.ModeOfPayment = billDetail.Bill_ModeOfPayment;
                    bvm.FManager = billfm.FinanceManager.Emp_Name;
                    bvm.Manager = manager;
                    bvm.BillSCopy = (billDetail.Bill_have_SCopy) ? _context.Bill_SCopies.Find(bill.Bill_ID).SCopy : null;
                }

                fmanagerBillslist.Add(bvm);
            }

            return fmanagerBillslist;
        }
    }
}