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
    [Authorize(Roles="Admin")]
    public class ManageEmployeeController : Controller
    {
        //
        // GET: /ManageEmployee/
        public ActionResult Index()
        {
            var Db = new ApricotContext();
            FillFullEmployee gfemp = new FillFullEmployee(Db);
            var fullemployees = gfemp.GetAllFullEmployee();
            return View(fullemployees);
        }

        public ActionResult Create() 
        {
            var Db = new ApricotContext();
            FullEmployee femp = new FullEmployee();
            List<Department> departments = new List<Department>(); 
            
            //Get All Departments
            DepartmentRepository departmentRepo = new DepartmentRepository(Db);
            departments = departmentRepo.GetAll().ToList();
            var departmentList = new List<String>(0); 
            
            foreach (var department in departments) 
            {
                departmentList.Add(department.Dept_ID.ToString() + " - " + department.Dept_Name);
            }
            
            ViewBag.DepartmentList = departmentList;
            
            return View(femp);
        }

        [HttpPost]
        public ActionResult Create(FullEmployee model) 
        {
            if (ModelState.IsValid) 
            {
                var Db = new ApricotContext();
                FillFullEmployee Fillfemp = new FillFullEmployee(Db);
                Fillfemp.CreateFullEmployee(model);
                
                return RedirectToAction("index");
            }
            return View(model);
        }

        public ActionResult Edit(Int64 id) 
        {
            var Db = new ApricotContext();
            FillFullEmployee fillFullEmployee = new FillFullEmployee(Db);
            FullEmployee fullEmployee = new FullEmployee();
            fullEmployee = fillFullEmployee.GetFullEmployeeByEmpNumber(id);
            List<Department> departments = new List<Department>(); 
            
            //Get All Departments
            DepartmentRepository departmentRepo = new DepartmentRepository(Db);
            departments = departmentRepo.GetAll().ToList();
            var departmentList = new List<String>(0); 
            
            foreach (var department in departments) 
            {
                departmentList.Add(department.Dept_ID.ToString() + " - " + department.Dept_Name);
            }
            
            ViewBag.DepartmentList = departmentList;
            
            return View(fullEmployee);
        }

        [HttpPost]
        public ActionResult Edit(FullEmployee model)
        {
            if (ModelState.IsValid) 
            {
                var Db = new ApricotContext();
                FillFullEmployee Fillfemp = new FillFullEmployee(Db);
                Fillfemp.EditFullEmployee(model);

                return RedirectToAction("Index", "ManageEmployee");
            }

            return View(model);
        }
	}
}