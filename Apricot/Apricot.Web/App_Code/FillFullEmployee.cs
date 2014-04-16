using Apricot.Data;
using Apricot.Data.Models;
using Apricot.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apricot.Data.Repositories;

namespace Apricot.Web.App_Code
{
    public class FillFullEmployee
    {
        private ApricotContext _context;
        public FillFullEmployee(Data.ApricotContext _context) 
        {
            this._context = _context;
        }

        /*public IEnumerable<FullEmployee> GetAllFullEmployee(String EmployeeNumber)
        {
            List<FullEmployee> fullemployee = new List<FullEmployee>(0);
        }*/
    }
}