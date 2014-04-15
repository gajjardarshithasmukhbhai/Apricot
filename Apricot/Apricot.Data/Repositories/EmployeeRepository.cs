using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data;
using Apricot.Data.Models;
using System.Data.Entity;

namespace Apricot.Data.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApricotContext _context;
        
        /// <summary>
        /// Constructor of Employee Repository
        /// </summary>
        /// <param name="context">Context of Application</param>
        /// <exception cref="">ArgumentNullException if context is NULL</exception>
        public EmployeeRepository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Get Employee By Employee_ID
        /// </summary>
        /// <param name="EmpID">Employee ID</param>
        /// <returns>Employee whose ID is EmpID</returns>
        public async Task<Employee> GetByEmpID(Int64 EmpID)
        {
            return await _context.Employees.FindAsync(EmpID);
        }

        /// <summary>
        /// Get Employee Identified by Empolyee Number.
        /// </summary>
        /// <param name="EmpNo">Employee Number</param>
        /// <returns>Employee</returns>
        /// <exception cref="">ArgumentNullException id EmpNo is Null</exception>
        public Employee GetByEmpNo(String EmpNo)
        {
            if (EmpNo == null)
                throw new ArgumentNullException();

            return _context.Employees.Where(e => e.Emp_No == EmpNo).Single<Employee>();
        }

        /// <summary>
        /// Adds New Employee to Store and Database
        /// </summary>
        /// <param name="employee">Employee to be Added</param>
        /// <returns>True if Employee is successfully Added, False Otherwise (i.e Duplicate Employee Number)</returns>
        /// <exception cref="">ArgumentNullException if employee to add is null</exception>
        public bool AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException("employee");

            if (!UniqueEmployee(employee))
                return false;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Update Employee Asynchronously
        /// </summary>
        /// <param name="employee">Employee to Update</param>
        /// <returns>True is Employee is Update Successfully, False Otherwise (i.e. Duplicate Employee Number)</returns>
        /// <exception cref="">ArgumentNullException if employee is NULL</exception>
        public async Task<bool> UpdateEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException("employee");

            if (!UniqueEmployee(employee))
                return false;

            _context.Entry<Employee>(employee).State = EntityState.Modified;
 
            await _context.SaveChangesAsync();

            return true;
        }

        #region Helpers

        /* Employee to be added here should have Unique Employee Number
           Since Entity Framework donot Support Unique Constraints yet.
           So, Unique Constraint Functionality is Implemented Here
         */
        private bool UniqueEmployee(Employee employee)
        {
            int cnt = _context.Employees.Where(e => e.Emp_No == employee.Emp_No).Count();
            if(cnt == 0)
                return true;

            return false;
        }

        #endregion
        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns>List Of Employee</returns>
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList<Employee>();
        }
    }
}
