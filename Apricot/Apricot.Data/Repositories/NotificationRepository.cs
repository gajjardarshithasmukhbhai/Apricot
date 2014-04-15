using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Repositories
{
    public class NotificationRepository
    {
        private readonly ApricotContext _context;
        /// <summary>
        /// Constructor of Notification Repository
        /// </summary>
        /// <param name="context">Accepts Context of the Applicayion</param>
        public NotificationRepository(ApricotContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
            _context = context;
        }
        /// <summary>
        /// Returns Notification with specified Notification Id
        /// </summary>
        /// <param name="Ntf_ID">Notification Id</param>
        /// <returns>Notification if found else null</returns>
        public Notification GetByNtfID(Int64 Ntf_ID)
        {
            return _context.Notifications.Find(Ntf_ID);
        }
        /// <summary>
        /// Adds Instance of Notification to the Table 
        /// </summary>
        /// <param name="ntf">Accepts Instance of Notification</param>
        public void AddNotification(Notification ntf)
        {
            _context.Notifications.Add(ntf);
            _context.SaveChanges();
            return;
        }
        /// <summary>
        /// Returns All Notification For Employee with specified Employee ID
        /// </summary>
        /// <param name="Emp_ID">EmployeeID</param>
        /// <returns>List of Notification if found else null</returns>
        public IEnumerable<Notification> GetAllByEmpID(Int64 Emp_ID)
        {
            return _context.Notifications.Where(a => a.Emp_ID == Emp_ID).ToList<Notification>();
        }
        /// <summary>
        /// Returns whole Notification Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notification> GetAll()
        {
            return _context.Notifications.ToList<Notification>();
        }
    }
}
