using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;
using Apricot.Data;
using System.Data.Entity;

namespace Apricot.Data.Repositories
{
    public class Employee_DetailRepository
    {
        private readonly ApricotContext _context;

        /// <summary>
        /// Constructor of Employee_Detail Repositry
        /// </summary>
        /// <param name="context">Context</param>
        /// <exception cref="">ArgumentNullException</exception>
        public Employee_DetailRepository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Get Employee Details Identifies By Employee ID Asynchronously
        /// </summary>
        /// <param name="EmpID">Employee ID</param>
        /// <returns>Employee Details</returns>
        public async Task<Employee_Detail> GetByEmpID(Int64 EmpID)
        {
            return await _context.Employee_Details.FindAsync(EmpID);
        }

        /// <summary>
        /// Add Employee Details
        /// </summary>
        /// <param name="employeeDetail">Employee Detail</param>
        /// <exception cref="">ArgumentNullException if Employee Detail is Null</exception>
        public void AddEmployeeDetail(Employee_Detail employeeDetail)
        {
            if (employeeDetail == null)
                throw new ArgumentException("employee_Detail");

            _context.Employee_Details.Add(employeeDetail);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update Employee Detail
        /// </summary>
        /// <param name="employeeDetail">Employee Detail</param>
        public async Task UpdateEmployeeDetail(Employee_Detail employeeDetail)
        {
            _context.Entry<Employee_Detail>(employeeDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
