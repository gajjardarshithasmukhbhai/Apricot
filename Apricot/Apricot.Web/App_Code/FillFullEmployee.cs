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

        public List<FullEmployee> GetAllFullEmployee()
        {
            var Db = new ApricotContext();
            List<FullEmployee> myfullemployees = new List<FullEmployee>(0);
            EmployeeRepository employeeRepo = new EmployeeRepository(Db);
            EmployeeDetailRepository employeeDetailRepo = new EmployeeDetailRepository(Db);
            List<Employee> employees = employeeRepo.GetAll().ToList();
            foreach (var employee in employees) 
            {
                Employee_Detail employeeDetail = employeeDetailRepo.GetByEmpID(employee.Emp_ID);
                FullEmployee fullemp = new FullEmployee();
                if (employeeDetail != null) 
                {
                    fullemp.Emp_ID = employee.Emp_ID;
                    fullemp.Emp_Name = employee.Emp_Name;
                    fullemp.Emp_No = employee.Emp_No;
                    fullemp.Department = _context.Departments.Find(employee.Dept_ID);
                    fullemp.Is_Active = employee.Is_Active;
                    fullemp.Emp_Address = employeeDetail.Emp_Address;
                    fullemp.Emp_Contact_No = employeeDetail.Emp_Contact_No;
                    fullemp.Emp_Gender = employeeDetail.Emp_Gender;
                }
                myfullemployees.Add(fullemp);
            }
            return myfullemployees;
        }
        public FullEmployee GetFullEmployeeByEmpNumber(Int64 id) 
        {
            var Db = new ApricotContext();
            FullEmployee fullEmployee = new FullEmployee();

            EmployeeRepository employeeRepo = new EmployeeRepository(Db);
            EmployeeDetailRepository employeeDetailRepo = new EmployeeDetailRepository(Db);
            DepartmentRepository departmentRepo = new DepartmentRepository(Db);

            Employee employee = employeeRepo.GetByEmpID(id);
            Employee_Detail employeeDetail = employeeDetailRepo.GetByEmpID(id);
            Department department = departmentRepo.GetByDepartmentID(employee.Dept_ID);

            if (employee != null && employeeDetail != null) 
            {
                fullEmployee.Emp_ID = employee.Emp_ID;
                fullEmployee.Emp_No = employee.Emp_No;
                fullEmployee.Emp_Name = employee.Emp_Name;
                fullEmployee.Is_Active = employee.Is_Active;
                fullEmployee.Department = _context.Departments.Find(employee.Dept_ID);
                fullEmployee.Emp_Address = employeeDetail.Emp_Address;
                fullEmployee.Emp_Contact_No = employeeDetail.Emp_Contact_No;
                fullEmployee.Emp_Gender = employeeDetail.Emp_Gender;
            }
            return fullEmployee;
        }

        public void CreateFullEmployee(FullEmployee model) 
        {
            Employee employee = new Employee();
            EmployeeRepository employeeRepo = new EmployeeRepository(_context);
            employee.Emp_ID = model.Emp_ID;
            employee.Emp_Name = model.Emp_Name;
            employee.Emp_No = model.Emp_No;
            employee.Dept_ID = Convert.ToInt64(model.Department.Dept_Name.Split('-')[0]);
            employee.Is_Active = model.Is_Active;
            employeeRepo.AddEmployee(employee);

            EmployeeDetailRepository employeeDetailRepo = new EmployeeDetailRepository(_context);
            Employee_Detail employeeDetail = new Employee_Detail() 
            {
                Emp_ID = employee.Emp_ID,
                Emp_Address = model.Emp_Address,
                Emp_Contact_No = model.Emp_Contact_No,
                Emp_Gender = model.Emp_Gender
            };
            employeeDetailRepo.AddEmployeeDetail(employeeDetail);
        }

        public void EditFullEmployee(FullEmployee model) 
        {
            EmployeeRepository employeeRepo = new EmployeeRepository(_context);
            Employee employee = employeeRepo.GetByEmpID(model.Emp_ID);
            if(employee != null)
            {
                employee.Emp_ID = model.Emp_ID;
                employee.Emp_Name = model.Emp_Name;
                employee.Emp_No = model.Emp_No;
                employee.Dept_ID = Convert.ToInt64(model.Department.Dept_Name.Split('-')[0]);
                employee.Is_Active = model.Is_Active;

                employeeRepo.UpdateEmployee(employee);


                EmployeeDetailRepository employeeDetailRepo = new EmployeeDetailRepository(_context);
                Employee_Detail employeeDetail = employeeDetailRepo.GetByEmpID(employee.Emp_ID);
                employeeDetail.Emp_Address = model.Emp_Address;
                employeeDetail.Emp_Contact_No = model.Emp_Contact_No;
                employeeDetail.Emp_Gender = model.Emp_Gender;

                employeeDetailRepo.UpdateEmployeeDetail(employeeDetail);
            }
        }
    }
}