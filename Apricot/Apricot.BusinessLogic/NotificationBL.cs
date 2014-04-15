using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;
using Apricot.Data.Repositories;
using Apricot.Data;

namespace Apricot.BusinessLogic
{
    public class NotificationBL
    {
        ApricotContext _context = new ApricotContext();
        public NotificationBL(ApricotContext con)
        {
            _context = con;
        }
        public void NotifyManager(Int64 bill_ID)
        {
            BillMRepository billm = new BillMRepository(_context);
            Bill_M bill_m = new Bill_M();
            bill_m = billm.GetByBillID(bill_ID);
            Employee emp = new Employee();
            Bill bill = new Bill();
            bill = _context.Bills.Find(bill_ID);
            emp = _context.Employees.Find(bill.Emp_ID);
            NotificationRepository ntf = new NotificationRepository(_context);
            Notification _ntf = new Notification()
            {
               Emp_ID = bill_m.Emp_ID,
               Seen = false,
               Ntf_Body = "New bill for review has arrived from "+emp.Emp_No+" - "+emp.Emp_Name,
               Ntf_Subject = "New Bill",
               TimeStamp = DateTime.Now

            };
            ntf.AddNotification(_ntf);
            return;
        }
        public void NotificationOfRejection(Int64 bill_ID)
        {

            NotificationRepository ntf = new NotificationRepository(_context);
            Bill bill = new Bill();
            bill = _context.Bills.Find(bill_ID);
            Notification _ntf = new Notification()
            {
                Emp_ID = bill.Emp_ID,
                Seen = false,
                Ntf_Body = "Bill Number " + bill.Bill_ID.ToString() + " has been rejected",
                Ntf_Subject = "Rejection of Bill",
                TimeStamp = DateTime.Now
            };
            ntf.AddNotification(_ntf);
            return;
        }
        public void NotificationOfCriditing(Int64 bill_ID)
        {
            NotificationRepository ntf = new NotificationRepository(_context);
            Bill bill = new Bill();
            bill = _context.Bills.Find(bill_ID);
            Notification _ntf = new Notification()
            {
                Emp_ID = bill.Emp_ID,
                Seen = false,
                Ntf_Body = "Bill Number " + bill.Bill_ID.ToString() + " has been credited",
                Ntf_Subject = "Bill credited",
                TimeStamp = DateTime.Now
            };
            ntf.AddNotification(_ntf);
            return;
        }
        public void NotificationOfApproval(Int64 bill_ID)
        {
            NotificationRepository ntf = new NotificationRepository(_context);
            Bill bill = new Bill();
            bill = _context.Bills.Find(bill_ID);
            Notification _ntf = new Notification()
            {
                Emp_ID = bill.Emp_ID,
                Seen = false,
                Ntf_Body = "Bill Number " + bill.Bill_ID.ToString() + " has been approved",
                Ntf_Subject = "Approval of Bill",
                TimeStamp = DateTime.Now
            };
            ntf.AddNotification(_ntf);
            Bill_FM bill_fm = new Bill_FM();
            BillFMRepository bfm = new BillFMRepository(_context);
            bill_fm = bfm.GetByBillId(bill_ID);
            _ntf.Emp_ID = bill_fm.Bill_FM_ID;
            _ntf.Ntf_Body = "New bill for financing has arrived";
            _ntf.Ntf_Subject = "New Bill";
            _ntf.TimeStamp = DateTime.Now;
            ntf.AddNotification(_ntf);
            return;
        }
        public void NotificationofComment(Int64 billID, Int64 emp_ID)
        {
            Employee emp, man, fin_man;
            EmployeeRepository _emp = new EmployeeRepository(_context);
            Bill bill = new Bill();
            bill = _context.Bills.Find(billID);
            emp = new Employee();
            emp = _emp.GetByEmpID(bill.Emp_ID);
            man = new Employee();
            Bill_M billm = new Bill_M();
            billm = _context.Bill_Ms.Where(a=> a.Emp_ID == billID).Single();
            man = _emp.GetByEmpID(billm.Emp_ID);
            fin_man = new Employee();
            Bill_FM billfm = new Bill_FM();
            billfm = _context.Bill_FMs.Where(a => a.Bill_ID == billID).Single();
            fin_man = _emp.GetByEmpID(billfm.Bill_FM_ID);
            NotificationGenerationAfterComment(emp, man, fin_man, emp_ID, bill);
            return;
        }
        private void CommentNotifier(Employee sender, Employee reciever1, Employee reciever2,Bill bill)
        {
            NotificationRepository ntf = new NotificationRepository(_context);
            if (reciever1 != null)
            {
                Notification _ntf = new Notification()
                {
                    Emp_ID = reciever1.Emp_ID,
                    Seen = false,
                    Ntf_Body = sender.Emp_Name+" commented on Bill number " + bill.Bill_ID.ToString(),
                    Ntf_Subject = "Comment Added",
                    TimeStamp = DateTime.Now
                };
                ntf.AddNotification(_ntf);
            }
            if (reciever2 != null)
            {
                Notification _ntf = new Notification()
                {
                    Emp_ID = reciever2.Emp_ID,
                    Seen = false,
                    Ntf_Body = sender.Emp_Name + " commented on Bill number " + bill.Bill_ID.ToString(),
                    Ntf_Subject = "Comment Added",
                    TimeStamp = DateTime.Now
                };
                ntf.AddNotification(_ntf);
            }
        }
        private void NotificationGenerationAfterComment(Employee emp1, Employee emp2, Employee emp3, Int64 senderId,Bill bill)
        {
            if (emp1.Emp_ID == senderId)
            {
                CommentNotifier(emp1, emp2, emp3,bill);
            }
            else if (emp2.Emp_ID == senderId)
            {
                CommentNotifier(emp2, emp1, emp3, bill);
            }
            else
            {
                CommentNotifier(emp3, emp2, emp1, bill);
            }
        }
    }
}
