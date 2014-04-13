using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class Bill_SCopyRepository
    {
        private readonly ApricotContext _context;

        public Bill_SCopyRepository(ApricotContext context) 
        {
            if (context == null)
                throw new ArgumentNullException();
            _context = context;
        }
        public Bill_SCopy GetByBillID(Int64 BillID) 
        {
            return _context.Bill_SCopies.Find(BillID);
        }
        public void AddBillSCopy(Bill_SCopy BillSCopy) 
        {
            if (BillSCopy == null)
                throw new ArgumentNullException();

            _context.Bill_SCopies.Add(BillSCopy);
            
            _context.SaveChanges();
        }
        public void UpdateBillSCopy(Bill_SCopy BillSCopy) 
        {
            if (BillSCopy == null)
                throw new ArgumentNullException();

            _context.Entry<Bill_SCopy>(BillSCopy).State = System.Data.Entity.EntityState.Modified;
            
            _context.SaveChanges();

        }
    }
}
