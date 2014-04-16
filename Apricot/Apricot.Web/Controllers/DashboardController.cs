using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apricot.Web.App_Code;
using Apricot.Data;
using Apricot.Web.Models;
using System.Threading.Tasks;
using Apricot.Data.Models;

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
            FillMyBillsViewModel FB = new FillMyBillsViewModel(_context);
            var allbills = FB.getMyBills(User.Identity.Name);

            return View(allbills);
        }

        [Authorize(Roles="Employee")]
        public ActionResult NewBill()
        {
            return View();
        }

        [Authorize(Roles="Employee")]
        [HttpPost]
        public Action NewBill(NewBillViewModel billModel)
        {

            //reached here, Everything is OK 
            return View(ViewBag.Message = "Successfully Filled Bill");
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