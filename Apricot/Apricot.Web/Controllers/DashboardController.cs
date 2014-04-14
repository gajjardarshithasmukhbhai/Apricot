using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apricot.Web.App_Code;
using Apricot.Data;
using Apricot.Web.Models;

namespace Apricot.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        ApricotContext _context = new ApricotContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        public ActionResult MyBills()
        {
            FillBillViewModel FB = new FillBillViewModel(_context);
            var allbills = FB.getMyBills(User.Identity.Name);

            return View(allbills);
        }

        [Authorize(Roles="Employee")]
        public ActionResult NewBill()
        {
            return View();
        }

        [Authorize(Roles="Employee")]
        public ActionResult Notifications()
        {
            return View();
        }

        [Authorize(Roles="Manager")]
        public ActionResult ManagerBills()
        {
            return View();
        }

        [Authorize(Roles="Finance Manager")]
        public ActionResult FManagerBills()
        {
            return View();
        }
    }
}