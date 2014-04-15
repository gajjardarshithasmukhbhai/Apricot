using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;
using Apricot.Data;
using Apricot.Data.Repositories;

namespace Apricot.BusinessLogic
{
    public class DepartmentBL
    {
        ApricotContext _context = new ApricotContext();
        public DepartmentBL(ApricotContext context)
        {
            _context = context;
        }
        public bool AddNewDepartment(string dept_name)
        {
            if(_context.Departments.Where(a => a.Dept_Name == dept_name).Single() != null )
            {
                return false;
            }
            Department dept = new Department()
            {
                Dept_Name = dept_name
            };
            DepartmentRepository _dept = new DepartmentRepository(_context);
            _dept.AddDepartment(dept);
            return true;
        }
        public bool EditDepartment( Department dept )
        {
            DepartmentRepository _dept = new DepartmentRepository(_context);
            _dept.UpdateDepartment(dept);
            return true;
        }
    }
}
