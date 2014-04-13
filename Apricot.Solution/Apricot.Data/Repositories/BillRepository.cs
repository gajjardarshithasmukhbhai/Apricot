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
        public BillRepository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            _context = context;
        }
        public async Task<Bill> GetByBillID(Int64 BillID)
        {
            return await _context.Bills.FindAsync(BillID);
        }
        public IEnumerable<Bill> GetByEmpID(Int64 EmpID)
        {
            return _context.Bills.Where(c => c.Emp_ID == EmpID).ToList();
        }
        public void AddBill(Bill bill)
        {
            if (bill == null)
                throw new ArgumentNullException();

            _context.Bills.Add(bill);
            
            _context.SaveChanges();
        }
        public void UpdateBill(Bill bill)
        {
            if (bill == null)
                throw new ArgumentNullException("context");

            _context.Entry<Bill>(bill).State = System.Data.Entity.EntityState.Modified;
            
            _context.SaveChanges();
        }
    }
}
