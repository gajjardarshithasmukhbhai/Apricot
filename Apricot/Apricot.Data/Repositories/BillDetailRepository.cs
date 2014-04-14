using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class BillDetailRepository
    {
        private readonly ApricotContext _context;

        /// <summary>
        /// Constructor Of Bill_Detail Repository
        /// </summary>
        /// <param name="context">Context</param>
        /// <exception cref="">ArgumentNullException</exception>
        public BillDetailRepository(ApricotContext context) 
        {
            if (context == null)
                throw new ArgumentNullException();

            _context = context;
        }

        /// <summary>
        /// Get Bill Details Identifies By Bill ID
        /// </summary>
        /// <param name="BillID">Bill ID</param>
        /// <returns>Bill Details</returns>
        public Bill_Detail GetByBillID(Int64 BillID) 
        {
            return _context.Bill_Details.Find(BillID);
        }

        /// <summary>
        /// Add Bill Details
        /// </summary>
        /// <param name="BillDetail">Bill Detail</param>
        /// <exception cref="">ArgumentNullException if Bill Detail is NULL </exception>
        public void AddBillDetail(Bill_Detail BillDetail)
        {
            if (BillDetail == null)
                throw new ArgumentNullException();

            _context.Bill_Details.Add(BillDetail);

            _context.SaveChanges();
        }
        
        /// <summary>
        /// Update Bill Detail
        /// </summary>
        /// <param name="BillDetail">Bill Detail</param>
        /// <exception cref="">ArgumentNullException if Bill Detail is NULL</exception>
        public void UpdateBillDetails(Bill_Detail BillDetail) 
        {
            if (BillDetail == null)
                throw new ArgumentNullException();

            _context.Entry<Bill_Detail>(BillDetail).State = System.Data.Entity.EntityState.Modified;

            _context.SaveChanges();
        }

        /// <summary>
        /// Get Bill Details for All Bills 
        /// </summary>
        /// <returns>List Of Bill Detail</returns>
        public IEnumerable<Bill_Detail> GetAll() 
        {
            return _context.Bill_Details.ToList<Bill_Detail>();
        }
    }
}
