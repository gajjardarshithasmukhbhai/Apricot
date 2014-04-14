using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Repositories
{
    public class BankRepository
    {
        Bank bank;
        private readonly ApricotContext _context;
        /// <summary>
        /// Constructor of Banks Repository
        /// </summary>
        /// <param name="context">Accepts Context of the Application</param>
        public BankRepository(ApricotContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            _context = context;
        }
        /// <summary>
        /// Returns Bank identified by specified BankID
        /// </summary>
        /// <param name="BankID">BankID</param>
        /// <returns>Bank if found else null</returns>
        public Bank GetByBankID (Int64 Bank_ID)
        {
            bank = _context.Banks.Find(Bank_ID);
            return bank;
        }
        /// <summary>
        /// Adds new instance of Bank in Database
        /// </summary>
        /// <param name="_bank">Accepts Bank as parameter</param>
        public void AddBank(Bank _bank)
        {
            _context.Banks.Add(_bank);
            return;
        }
        /// <summary>
        /// Updates the instance where Bank_ID=_bank.Bank_ID
        /// </summary>
        /// <param name="_bank">Accepts Bank as parameter</param>
        public void UpdateBank(Bank _bank)
        {
            if (_bank == null)
            {
                throw new ArgumentNullException();
            }
            bank = _bank;
            _context.SaveChanges();
            return;
        }
        /// <summary>
        /// Returns whole Bank Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bank> GetAll()
        {
            return _context.Banks.ToList<Bank>();
        }
    }
}
