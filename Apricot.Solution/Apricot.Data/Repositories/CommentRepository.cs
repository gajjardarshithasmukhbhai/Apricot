using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apricot.Data.Repositories
{
    public class CommentRepository
    {
        Comment comment;
        ApricotContext _context;
        /// <summary>
        /// Contructor of Comment Repository
        /// </summary>
        /// <param name="context">Accepts context of the Application</param>
        public CommentRepository(ApricotContext context)
        {
            if ( context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
        }
        /// <summary>
        /// Returns Comment Identified By CommentID
        /// </summary>
        /// <param name="CmtID">CommentID</param>
        /// <returns>Comment if found else null</returns>
        public Comment GetByCommentID(Int64 CmtID) 
        {
            if (CmtID == null)
            {
                throw new ArgumentNullException("Comment");
            }
            comment = _context.Comments.Find(CmtID);
            return comment;
        }
        /// <summary>
        /// Returns a List of Comments having specified Bill_ID
        /// </summary>
        /// <param name="Bill_ID">Bill_ID</param>
        /// <returns>List of Comments if found else null</returns>
        public IEnumerable<Comment> GetAllByBillID(Int64 Bill_ID)
        {
            if (Bill_ID == null)
            {
                throw new ArgumentNullException("Comment");
            }
            return _context.Comments.Where(c => c.Bill_ID == Bill_ID).ToList();
        }
        /// <summary>
        /// Adds a new instance of Comment in Database
        /// </summary>
        /// <param name="_comment">Accepts Comment as a parameter</param>
        public void AddComment(Comment _comment)
        {
            _context.Comments.Add(_comment);
            return;
        }
        /// <summary>
        /// Returns whole Comment Table
        /// </summary>
        /// <returns></returns>
        public List<Comment> GetAll()
        {
            return _context.Comments.ToList<Comment>();
        }
    }
}
