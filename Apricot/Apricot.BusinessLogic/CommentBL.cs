using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apricot.Data.Models;
using Apricot.Data.Repositories;
using Apricot.Data;

namespace Apricot.BusinessLogic
{
    public class CommentBL
    {
        ApricotContext _context = new ApricotContext();
        public CommentBL(ApricotContext con)
        {
            _context = con;
        }
        public void CreateComment(Int64 billId, string cmtbody, string subject, Int64 empId)
        {

            Comment _cmt = new Comment()
            {
                Emp_ID = empId,
                TimeStamp = DateTime.Now,
                Bill_ID = billId,
                Cmt_Body = cmtbody,
                Cmt_Subject = subject
            };
            CommentRepository cmt = new CommentRepository(_context);
            cmt.AddComment(_cmt);
            return;
        }
    }
}
