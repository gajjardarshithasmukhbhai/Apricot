using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class Bill_MRepository
    {
        private readonly ApricotContext _context;

        public Bill_MRepository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException();
            _context = context;
        }
        public IEnumerable<Bill_M> GetAllByBillMID(Int64 BillMID)
        {
            return _context.Bill_Ms.Where(c => c.Emp_ID == BillMID).ToList();
        }
        public void AddBillM(Bill_M BillM)
        {
            if (BillM == null)
                throw new ArgumentNullException();

            _context.Bill_Ms.Add(BillM);

            _context.SaveChanges();

        }
    }
}
