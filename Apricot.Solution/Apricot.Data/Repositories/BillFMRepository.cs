using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class Bill_FMRepository
    {
        private readonly ApricotContext _context;

        public Bill_FMRepository(ApricotContext context) 
        {
            if (context == null)
                throw new ArgumentNullException();

            _context = context;
        }
        public IEnumerable<Bill_FM> GetAllByBillFMID(Int64 BillFMID) 
        {
            return _context.Bill_FMs.Where(c => c.Bill_FM_ID == BillFMID).ToList();
        }
        public void AddBillFM(Bill_FM BillFM) 
        {
            if (BillFM == null)
                throw new ArgumentNullException();

            _context.Bill_FMs.Add(BillFM);

            _context.SaveChanges();

        }
    }
}
