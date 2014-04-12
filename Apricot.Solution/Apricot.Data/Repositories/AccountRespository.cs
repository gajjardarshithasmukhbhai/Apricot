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
    public class AccountRespository
    {
        private readonly ApricotContext _context;
       
        /// <summary>
        /// Constructor or Account Repository
        /// </summary>
        /// <param name="context">Accepts context of the Application</param>
        /// <exception cref="">ArgumentNullException if context is NULL</exception>
        public AccountRespository(ApricotContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Returns Account Async Identified by EmpId (Employee ID) 
        /// </summary>
        /// <param name="EmpID">Employee ID whose Account is to be Found</param>
        /// <returns>Account is found, else NULL </returns>
        public async Task<Account> GetByEmpID(Int64 EmpID)
        {
            return await _context.Accounts.FindAsync(EmpID);
        }

        /// <summary>
        /// Returns Account Identified by the AccountNumber
        /// </summary>
        /// <param name="AccountNumber">Account Number of Account</param>
        /// <returns>Account if Found, else NULL</returns>
        public Account GetByAccountNumber(Int64 AccountNumber)
        {
            Account account = null;
            try
            {
                account = _context.Accounts.Where(a => a.Account_No == AccountNumber).Single<Account>();
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
                return null;
            }

            return account;
        }

        /// <summary>
        /// Adds new Unique Account to Store and Save Changes to Database
        /// </summary>
        /// <param name="account">Account to be added</param>
        /// <returns>True if Account adds Successfully, False is Account is not Unique</returns>
        /// <exception cref="">ArgumentNullException if Account is NULL</exception>
        public bool AddAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            //Check Whether Account to be Unique or not in terms or 'Account Number'
            if (!UniqueAccount(account))
                return false;

            //Account is Unique, Add Account and Save Changes to Database
            _context.Accounts.Add(account);
            _context.SaveChanges();
            
            return true;
        }

        /// <summary>
        /// Updates Account
        /// </summary>
        /// <param name="account">Account to be Updated</param>
        /// <returns>True if Account is Updated Successfully, False otherwise i.e (When Duplicate Account Number)</returns>
        /// <exception cref="">ArgumentNUllException if account is NULL</exception>
        public async Task<bool> UpdateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            if (!UniqueAccount(account))
                return false;

            _context.Entry<Account>(account).State = System.Data.Entity.EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Delete the Account passed from store and from Database
        /// </summary>
        /// <param name="account">Account to be deleted</param>
        /// <returns>True is Account is Successfully Delete, False otherwise</returns>
        /// <exception cref="">ArgumentNullException if account is NULL</exception>
        public bool DeleteAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            Account accnt = _context.Accounts.Remove(account);
            if (accnt != null)
                return true;

            return false;
        }

        #region Helpers
        /* Account to be added here should have Unique Account Number
           Since Entity Framework donot Support Unique Constraints yet.
           So, Unique Constraint Functionality is Implemented Here
         */
        private bool UniqueAccount(Account account)
        {
            int cnt = 0;
            cnt = _context.Accounts.Where(a => a.Account_No == account.Account_No).Count();

            if (cnt == 0)
                return true;

            return false;
        }

        #endregion
    }
}
