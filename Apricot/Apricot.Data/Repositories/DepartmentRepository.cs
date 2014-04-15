using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Repositories
{
    public class DepartmentRepository
    {
        Department department;
        private readonly ApricotContext _context;
        /// <summary>
        /// Constructor of DepartmentRepository
        /// </summary>
        /// <param name="context">Accepts Context of the Application</param>
        public DepartmentRepository(ApricotContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            _context = context;
        }
        /// <summary>
        /// Returns the Departmemt Identified by Specified DepartmentID
        /// </summary>
        /// <param name="Dept_ID">Accept DepartmentID</param>
        /// <returns>Department if found else null</returns>
        public Department GetByDepartmentID(Int64 Dept_ID)
        {
            department = _context.Departments.Find(Dept_ID);
            return department;
        }
        /// <summary>
        /// Adds Department in Database
        /// </summary>
        /// <param name="_dept">Accepts Instance of Department</param>
        public void AddDepartment(Department _dept)
        {
            if (_dept == null)
            {
                throw new ArgumentNullException();
            }
            _context.Departments.Add(_dept);
            _context.SaveChanges();
            return;
        }
        /// <summary>
        /// Updates existing Department
        /// </summary>
        /// <param name="_dept">Accept Instance of Department</param>
        public void UpdateDepartment(Department _dept)
        {
            if (_dept == null)
            {
                throw new ArgumentNullException();
            }
            department = _dept;
            _context.SaveChanges();
            return;
        }
        /// <summary>
        /// Returns Whole Department Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList<Department>();
        }
        /// <summary>
        /// Returns Department Identified By Specified Department Name
        /// </summary>
        /// <param name="_dept_name">Accepts string</param>
        /// <returns>Department if found else null</returns>
        public Department GetByDepartName(string _dept_name)
        {
            department = _context.Departments.Where(a => a.Dept_Name == _dept_name).Single();
            return department;
        }
    }
}
