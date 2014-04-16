using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apricot.Data.Models;
using Apricot.Data.Repositories;
using Apricot.Data;
using Apricot.Web.Models;
using Apricot.Web.App_Code;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Apricot.Web.Controllers
{
    public class ManageEmployeeController : Controller
    {
        //
        // GET: /ManageEmployee/
        public ActionResult Index()
        {
            var Db = new ApricotContext();
            var full_employee = new FullEmployee();
            return View();
        }
	}
}