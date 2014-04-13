using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class Bill_DetailRepository
    {
        private readonly ApricotContext _context;

        public Bill_DetailRepository(ApricotContext context) 
        {
            if (context == null)
                throw new ArgumentNullException();

            _context = context;
        }
        public Bill_Detail GetByBillID(Int64 BillID) 
        {
            return _context.Bill_Details.Find(BillID);
        }
        public void AddBillDetail(Bill_Detail BillDetail)
        {
            if (BillDetail == null)
                throw new ArgumentNullException();

            _context.Bill_Details.Add(BillDetail);

            _context.SaveChanges();
        }
        public void UpdateBillDetails(Bill_Detail BillDetail) 
        {
            if (BillDetail == null)
                throw new ArgumentNullException();

            _context.Entry<Bill_Detail>(BillDetail).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
