using Apricot.BusinessLogic;
using Apricot.Data;
using Apricot.Data.Models;
using Apricot.Data.Repositories;
using Apricot.Web.App_Code;
using Apricot.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apricot.Web.Controllers
{
    public class FManagerController : Controller
    {
        //
        // GET: /FManager/
        public ActionResult Index()
        {
            var Db = new ApricotContext();
            FillBillsViewModel gbvm = new FillBillsViewModel(Db);
            var Managerbills = gbvm.getFManagerBills(User.Identity.Name);

            return View(Managerbills);
        }

        public ActionResult Details(Int64 id)
        {
            ApricotContext Db = new ApricotContext();
            FillBillsViewModel fbvm = new FillBillsViewModel(Db);
            BillViewModel bvm = fbvm.getBillDetail(id);

            return View(bvm);
        }

        [HttpPost]
        public ActionResult ConfirmDecision(String decision, Int64 id)
        {
            ApricotContext Db = new ApricotContext();

            BillRepository billrepo = new BillRepository(Db);
            Bill bill = billrepo.GetByBillID(id);
            if (decision == "yes")
            {
                bill.Bill_Status = Data.Models.ApricotEnums.BillSatusEnum.CLOSED;
                billrepo.UpdateBill(bill);

                NotificationBL notibl = new NotificationBL(Db);
                notibl.NotificationOfCriditing(id);
            }

            //This means Bill is Still not Closed, Send Back the Index
            return RedirectToAction("Index", "FManager");
        }
	}
}