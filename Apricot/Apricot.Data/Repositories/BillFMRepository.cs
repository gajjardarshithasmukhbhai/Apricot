using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;

namespace Apricot.Data.Repositories
{
    public class BillFMRepository
    {
        private readonly ApricotContext _context;

        /// <summary>
        /// Constructor Of Bill_FM Repository
        /// </summary>
        /// <param name="context">Context</param>
        /// <exception cref="">ArgumentNullException</exception>
        public BillFMRepository(ApricotContext context) 
        {
            if (context == null)
                throw new ArgumentNullException();

            _context = context;
        }

        /// <summary>
        /// Get All Bill ID And Bill Finance Manager ID Identifies By Bill Finance Manager Id 
        /// </summary>
        /// <param name="BillFMID">Bill Finance Manager ID</param>
        /// <returns>All Instaces Of Bill_FM From Database</returns>
        public IEnumerable<Bill_FM> GetAllByBillFMID(Int64 BillFMID) 
        {
            return _context.Bill_FMs.Where(c => c.Bill_FM_ID == BillFMID).ToList<Bill_FM>();
        }

        /// <summary>
        /// Add Bill ID And Corresponding Bill Finance Manager ID
        /// </summary>
        /// <param name="BillFM">Bill_FM</param>
        public void AddBillFM(Bill_FM BillFM) 
        {
            if (BillFM == null)
                throw new ArgumentNullException();

            _context.Bill_FMs.Add(BillFM);

            _context.SaveChanges();

        }


        /// <summary>
        /// Return Bill Finance Manager Bill_FM Identified by billId
        /// </summary>
        /// <param name="billId">Bill Id</param>
        /// <returns>Bill_FM</returns>
        public Bill_FM GetByBillId(Int64 billId)
        {
            return _context.Bill_FMs.First(bfm => bfm.Bill_ID == billId);
        }

        /// <summary>
        /// Get All Bill ID And Corresopnding Bill Finance Manager ID
        /// </summary>
        /// <returns>All Instances Of Bill ID And Corresopnding Bill Finance Manager ID from Database</returns>
        public IEnumerable<Bill_FM> GetAll() 
        {
            return _context.Bill_FMs.ToList<Bill_FM>();
        }
    }
}
