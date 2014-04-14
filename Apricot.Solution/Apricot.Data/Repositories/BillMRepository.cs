using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class BillMRepository
    {
        private readonly ApricotContext _context;

        /// <summary>
        /// Constructor Of Bill_M Repository
        /// </summary>
        /// <param name="context">Context</param>
        /// <exception cref="">ArgumentNullException</exception>
        public BillMRepository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException();
            _context = context;
        }

        /// <summary>
        /// Get All Bill ID And Corresponding Manager ID Identifies By Manager ID
        /// </summary>
        /// <param name="BillMID"></param>
        /// <returns>All Instances Of Bill ID And Related Manager ID From Database</returns>
        public IEnumerable<Bill_M> GetAllByBillMID(Int64 BillMID)
        {
            return _context.Bill_Ms.Where(c => c.Emp_ID == BillMID).ToList<Bill_M>();
        }

        /// <summary>
        /// Add Bill ID And Corresponding Manager ID
        /// </summary>
        /// <param name="BillM">Bill Manager ID</param>
        public void AddBillM(Bill_M BillM)
        {
            if (BillM == null)
                throw new ArgumentNullException();

            _context.Bill_Ms.Add(BillM);

            _context.SaveChanges();

        }

        /// <summary>
        /// Get All Bill ID And Corresponding Manager ID
        /// </summary>
        /// <returns>List Of Bill ID And Related Manager ID</returns>
        public IEnumerable<Bill_M> GetAll() 
        {
            return _context.Bill_Ms.ToList<Bill_M>();
        }
    }
}
