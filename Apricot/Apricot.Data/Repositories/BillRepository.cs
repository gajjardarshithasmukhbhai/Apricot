using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Apricot.Data;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class BillRepository
    {
        private readonly ApricotContext _context;

        /// <summary>
        /// Constructor Of Bill Repository
        /// </summary>
        /// <param name="context">Context</param>
        /// <exception cref="">ArgumentNullException</exception>
        public BillRepository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }

        /// <summary>
        /// Get Bills Identifies By Bill ID Asynchronously
        /// </summary>
        /// <param name="BillID">Bill ID</param>
        /// <returns>Bills</returns>
        public async Task<Bill> GetByBillID(Int64 BillID)
        {
            return await _context.Bills.FindAsync(BillID);
        }

        /// <summary>
        /// Get Bills Identifies By Employee ID
        /// </summary>
        /// <param name="EmpID">Employee ID</param>
        /// <returns>All Instances Of Bill whose Employee ID is EmpID</returns>
        public IEnumerable<Bill> GetByEmpID(Int64 EmpID)
        {
            return _context.Bills.Where(c => c.Emp_ID == EmpID).ToList<Bill>();
        }

        /// <summary>
        /// Add Bill 
        /// </summary>
        /// <param name="bill">Bill</param>
        /// <exception cref="">ArgumentNullException If Bill is Null</exception>
        public void AddBill(Bill bill)
        {
            if (bill == null)
                throw new ArgumentNullException();

            _context.Bills.Add(bill);
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Update Bill 
        /// </summary>
        /// <param name="bill">Bill</param>
        /// <exception cref="">ArgumentNullException If Bill is Null</exception>
        public void UpdateBill(Bill bill)
        {
            if (bill == null)
                throw new ArgumentNullException("context");

            _context.Entry<Bill>(bill).State = System.Data.Entity.EntityState.Modified;
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Get All Bills 
        /// </summary>
        /// <returns>All Instances Of Bill</returns>
        public IEnumerable<Bill> GetAll() 
        {
            return _context.Bills.ToList<Bill>();
        }
    }
}
