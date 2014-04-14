using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class BillSCopyRepository
    {
        private readonly ApricotContext _context;

        /// <summary>
        /// Constructor Of Bill_SCopy Repository
        /// </summary>
        /// <param name="context">Context</param>
        /// <exception cref="">ArgumentNullException</exception>
        public BillSCopyRepository(ApricotContext context) 
        {
            if (context == null)
                throw new ArgumentNullException();
            _context = context;
        }

        /// <summary>
        /// Get Bill Scan Copy And Related Bill ID Identifies By Bill ID
        /// </summary>
        /// <param name="BillID">Bill ID</param>
        /// <returns>Bill Scan Copy Details</returns>
        /// <exception cref="">If Bill Scan Copy And Related Bill ID ,whose Bill ID is BillID ,is not present</exception>
        public Bill_SCopy GetByBillID(Int64 BillID) 
        {
             Bill_SCopy BillSCopy =  _context.Bill_SCopies.Find(BillID);
             if (BillSCopy == null)
                 throw new ArgumentNullException();
             return BillSCopy;
        }

        /// <summary>
        /// Add Bill Scan Copy and Its Bill ID
        /// </summary>
        /// <param name="BillSCopy">Bill Scan Copy With Its Bill ID</param>
        public void AddBillSCopy(Bill_SCopy BillSCopy) 
        {
            if (BillSCopy == null)
                throw new ArgumentNullException();

            _context.Bill_SCopies.Add(BillSCopy);
            
            _context.SaveChanges();
        }

        /// <summary>
        /// Update Bill Scan Copy
        /// </summary>
        /// <param name="BillSCopy">Bill Scan Copy</param>
        /// <exception cref="">ArgumentNullException If Bill Scan Copy is Null</exception>
        public void UpdateBillSCopy(Bill_SCopy BillSCopy) 
        {
            if (BillSCopy == null)
                throw new ArgumentNullException();

            _context.Entry<Bill_SCopy>(BillSCopy).State = System.Data.Entity.EntityState.Modified;
            
            _context.SaveChanges();

        }

        /// <summary>
        /// Get All Bill Scan Copies
        /// </summary>
        /// <returns>All Instances Of Bill Scan Copy</returns>
        public IEnumerable<Bill_SCopy> GetAll() 
        {
            return _context.Bill_SCopies.ToList<Bill_SCopy>();
        }
    }
}
