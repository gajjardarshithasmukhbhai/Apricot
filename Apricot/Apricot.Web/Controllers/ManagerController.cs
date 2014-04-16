using Apricot.Data;
using Apricot.Web.App_Code;
using Apricot.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Apricot.BusinessLogic;
using Apricot.Data.Repositories;
using Apricot.Data.Models;

namespace Apricot.Web.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        public ActionResult Index()
        {
            var Db = new ApricotContext();
            FillBillsViewModel gbvm = new FillBillsViewModel(Db);
            var Managerbills = gbvm.getManagerBills(User.Identity.Name);

            return View(Managerbills);
        }

        public ActionResult Details(Int64 id)
        {
            ApricotContext Db = new ApricotContext();
            FillBillsViewModel fbvm = new FillBillsViewModel(Db);
            BillViewModel bvm = fbvm.getBillDetail(id);

            var departments = Db.Departments.Select(d => d.Dept_Name).ToList();
            ViewBag.departmentslist = departments;

            return View(bvm);
        }

        [HttpPost]
        public ActionResult ConfirmDecision(String decision, String Department, Int64 id)
        {
            ApricotContext Db = new ApricotContext();
            
            BillRepository billrepo = new BillRepository(Db);
            Bill bill = billrepo.GetByBillID(id);
            if (decision == "yes")
            {
                bill.Bill_Status = Data.Models.ApricotEnums.BillSatusEnum.APPROVED;
                billrepo.UpdateBill(bill);

                FinanceManagerBL fmbl = new FinanceManagerBL(Db);
                fmbl.AlloteFinanceManager(id, Department);

                NotificationBL notibl = new NotificationBL(Db);
                notibl.NotificationOfApproval(id);
            }
            
            else if(decision == "no")
            {
                bill.Bill_Status = Data.Models.ApricotEnums.BillSatusEnum.REJECTED;
                billrepo.UpdateBill(bill);

                NotificationBL notibl = new NotificationBL(Db);
                notibl.NotificationOfRejection(bill.Bill_ID);
            }

            return RedirectToAction("Index", "Manager");
        }
	}
}