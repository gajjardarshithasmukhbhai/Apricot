using Apricot.Data;
using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apricot.Data.Repositories;

namespace Apricot.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class DepartmentController : Controller
    {
        public ActionResult Index()
        {
            var Db = new ApricotContext();
            var departments = Db.Departments;
            return View(departments);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department model)
        {
            if(ModelState.IsValid)
            {
                var Db = new ApricotContext();
                DepartmentRepository repo = new DepartmentRepository(Db);
                repo.AddDepartment(model);
                return RedirectToAction("Index");
            }

            //Something Went wrong, redisplay form
            return View(model);
        }

        public ActionResult Edit(Int64 id)
        {
            var Db = new ApricotContext();
            var dept = Db.Departments.First(d => d.Dept_ID == id);
            return View(dept);
        }

        [HttpPost]
        public ActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApricotContext();
                var department = Db.Departments.First(dept => dept.Dept_ID == model.Dept_ID);

                // Update the dept data:
                department.Dept_Name = model.Dept_Name;
                Db.Entry(department).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}