using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;
using Apricot.Data;
using Apricot.Data.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Apricot.BusinessLogic
{
    public class FinanceManagerBL
    {
        ApricotContext _context;
        public FinanceManagerBL(ApricotContext con)
        {
            _context = con;
        }
        public Boolean AlloteFinanceManager(Int64 bill_Id, String dept_name)
        {
            Department dept;
            DepartmentRepository _dept = new DepartmentRepository(_context);
            List<Employee> list;
            Bill_FM bill_fm = new Bill_FM();
            dept = _dept.GetByDepartName(dept_name);
            list = _context.Employees.Where(a => a.Dept_ID == dept.Dept_ID).ToList();
            
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApricotContext()));
            var im = new IdentityManager();
            Employee emp_min_bill = new Employee();
            foreach (var employee in list)
	        {
                var userId = um.Users.First(u => u.UserName == employee.Emp_No).Id;
                var userroles = um.GetRoles(userId);
                int n=1000000,k;
                bool inrole = false;
                foreach (var role in userroles)
                {
                    if (role == "Finance Manager")
                    {
                        inrole = true;
                        break;
                    }
                }
                if (inrole == true)
                {
                    if (n > (k = _context.Bill_FMs.Where(a => a.Bill_FM_ID == employee.Emp_ID).Count()))
                    {
                        n = k;
                        emp_min_bill = employee;
                    }
                }
            }
            bill_fm.Bill_FM_ID = emp_min_bill.Emp_ID;
            bill_fm.Bill_ID = bill_Id;
            BillFMRepository B = new BillFMRepository(_context);
            B.AddBillFM(bill_fm);
            return false;
        }
    }
}
