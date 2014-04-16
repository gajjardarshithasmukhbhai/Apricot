using Apricot.Data;
using Apricot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apricot.Web.Models;
using System.Web.Mvc;
using Apricot.Data.Repositories;

namespace Apricot.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class EmployeeController : Controller
    {
        public ActionResult index()
        {
            var full_employees = new FullEmployee();
            return View(full_employees);
        }
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        /*public ActionResult Create(FullEmployee model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApricotContext();
                model.
                return RedirectToAction("Index");
            }

            //Something Went wrong, redisplay form
            return View(model);
        }*/

        public ActionResult Edit(Int64 id)
        {
            var Db = new ApricotContext();
            var emp = Db.Employees.First(d => d.Emp_ID == id);
            return View(emp);
        }

        /*[HttpPost]
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
        }*/
    }
}