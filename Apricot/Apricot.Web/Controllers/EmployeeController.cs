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
using Apricot.BusinessLogic;

namespace Apricot.Web.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            var Db = new ApricotContext();
            FillBillsViewModel gbvm = new FillBillsViewModel(Db);
            var Mybills = gbvm.getMyBills(User.Identity.Name).OrderByDescending(bil => bil.BillDate);

            return View(Mybills);
        }

        public ActionResult Create()
        {
            BillViewModel bvm = new BillViewModel();
            var managerlist = new List<String>(0);

            //Get All Managers
            var um = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(new ApricotContext()));
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApricotContext()));

            var users = um.Users.ToList();
            foreach (var user in users)
            {
                var rolesId = user.Roles.Select(r => r.RoleId);
                foreach (var roleid in rolesId)
                {
                    var rolename = rm.FindById(roleid).Name;
                    if (rolename == "Manager")
                    {
                        if (user.UserName != "admin")
                        {
                            managerlist.Add(user.UserName + " - " + user.FirstName + " " + user.LastName);
                        }
                        break;
                    }
                }
            }

            ViewBag.Managerslist = managerlist;
            return View(bvm);
        }

        [HttpPost]
        public ActionResult Create(BillViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApricotContext();
                FillBillsViewModel fbvm = new FillBillsViewModel(Db);
                fbvm.CreateNewBill(User.Identity.Name, model);

                //Get BillID
                BillDetailRepository billdetailrepo = new BillDetailRepository(Db);
                var billdetail= billdetailrepo.GetAll().Where(bd => (bd.Bill_Date == model.BillDate && 
                                                            bd.Bill_Type == model.BillType && 
                                                            bd.Bill_ModeOfPayment == model.ModeOfPayment));
                model.BillID = billdetail.Select(bd => bd.Bill_ID).Single();

                //Create Notification for New Bill
                NotificationBL notibl = new NotificationBL(Db);
                notibl.NotifyManager(model.BillID);

                return RedirectToAction("Index");
            }

            //Something Went wrong, redisplay form
            return View(model);
        }

        public ActionResult Details(Int64 id)
        {
            ApricotContext Db = new ApricotContext();
            FillBillsViewModel fbvm = new FillBillsViewModel(Db);
            BillViewModel bvm = fbvm.getBillDetail(id);

            return View(bvm);
        }

        public ActionResult Notifications()
        {
            ApricotContext Db = new ApricotContext();
            NotificationRepository notirepo = new NotificationRepository(Db);
            var emp_Id = Db.Employees.Where(e => e.Emp_No == User.Identity.Name).Select(e => e.Emp_ID).Single();

            var notifications = notirepo.GetAllByEmpID(emp_Id).OrderByDescending(ntf => ntf.TimeStamp);

            return View(notifications);
        }
    }
}