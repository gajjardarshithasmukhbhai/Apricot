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
               Ntf_Subject = "New Bill"

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
                Ntf_Subject = "Rejection of Bill"
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
                Ntf_Subject = "Approval of Bill"
            };
            ntf.AddNotification(_ntf);
            Bill_FM bill_fm = new Bill_FM();
            BillFMRepository bfm = new BillFMRepository(_context);
            bill_fm = bfm.GetByBillId(bill_ID);
            _ntf.Emp_ID = bill_fm.Bill_FM_ID;
            _ntf.Ntf_Body = "New bill for financing has arrived";
            _ntf.Ntf_Subject = "New Bill";
            ntf.AddNotification(_ntf);
            return;
        }

    }
}
