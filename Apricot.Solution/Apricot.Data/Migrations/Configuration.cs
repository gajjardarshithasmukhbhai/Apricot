namespace Apricot.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Apricot.Data.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Apricot.Data.ApricotContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Apricot.Data.ApricotContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddOrUpdateAccounts(context);
            AddOrUpdateBanks(context);
            AddOrUpdateBills(context);
            AddOrUpdateBillDetails(context);
            AddOrUpdateDepartments(context);
            AddOrUpdateEmployees(context);
            AddOrUpdateEmployeeDetails(context);
        }

        /// <summary>
        ///Seed Method For Adding Or Updating Accounts 
        /// </summary>
        /// <param name="_context">Context</param>
        private void AddOrUpdateAccounts(ApricotContext _context) 
        {
            _context.Accounts.AddOrUpdate(new Account() 
            {
                Account_No = 1234567890
            });
            _context.Accounts.AddOrUpdate(new Account()
            {
                Account_No = 2345678901
            });
            _context.Accounts.AddOrUpdate(new Account() 
            {
                Account_No = 3456789012
            });
        }

        /// <summary>
        /// Seed Method For Adding Or Updating Banks
        /// </summary>
        /// <param name="_context">Context</param>
        private void AddOrUpdateBanks(ApricotContext _context) 
        {
            _context.Banks.AddOrUpdate(new Bank() { 
                Bank_Name = "State Bank Of India",
                Branch_Name = "Arya Nagar",
                Branch_City = "Ghaziabad"
            });
            _context.Banks.AddOrUpdate(new Bank() 
            {
                Bank_Name = "Punjab National Bank",
                Branch_Name = "Meerut Road",
                Branch_City = "Ghaziabad"
            });
            _context.Banks.AddOrUpdate(new Bank() 
            {
                Bank_Name = "AXIS BANK",
                Branch_Name = "Anand Nagar",
                Branch_City = "Jabalpur"
            });
        }

        /// <summary>
        /// Seed Method For Adding Or Updating Bills
        /// </summary>
        /// <param name="_context">Context</param>
        private void AddOrUpdateBills(ApricotContext _context) 
        {
            _context.Bills.AddOrUpdate(new Bill() 
            {
                Bill_Status = ApricotEnums.BillSatusEnum.PENDING
            });
            _context.Bills.AddOrUpdate(new Bill() 
            {
                Bill_Status = ApricotEnums.BillSatusEnum.APPROVED
            });
            _context.Bills.AddOrUpdate(new Bill() 
            {
                Bill_Status = ApricotEnums.BillSatusEnum.CLOSED
            });
            _context.Bills.AddOrUpdate(new Bill() 
            {
                Bill_Status = ApricotEnums.BillSatusEnum.REJECTED
            });
        }

        /// <summary>
        /// Seed Method For Adding Or Updating Bill Details
        /// </summary>
        /// <param name="_context">Context</param>
        private void AddOrUpdateBillDetails(ApricotContext _context) 
        {
            _context.Bill_Details.AddOrUpdate(new Bill_Detail() 
            {
                Bill_Amount = 1000,
                Bill_Date = new DateTime(2014,04,01),
                Bill_Type = "Telephone",
                Bill_ModeOfPayment = ApricotEnums.BillModeOfPaymentEnum.ACCOUNT_TRANSFER,
                Bill_have_SCopy = true
            });
            _context.Bill_Details.AddOrUpdate(new Bill_Detail()
            {
                Bill_Amount = 5000,
                Bill_Date = new DateTime(2014, 02, 23),
                Bill_Type = "Medical",
                Bill_ModeOfPayment = ApricotEnums.BillModeOfPaymentEnum.ACCOUNT_TRANSFER,
                Bill_have_SCopy = true
            });
            _context.Bill_Details.AddOrUpdate(new Bill_Detail()
            {
                Bill_Amount = 1400,
                Bill_Date = new DateTime(2014, 03, 15),
                Bill_Type = "Acedmic",
                Bill_ModeOfPayment = ApricotEnums.BillModeOfPaymentEnum.CHEQUE,
                Bill_have_SCopy = false
            });
        }

        /// <summary>
        /// Seed Method For Adding Or Updating Departments
        /// </summary>
        /// <param name="_context"></param>
        private void AddOrUpdateDepartments(ApricotContext _context) 
        {
            _context.Departments.AddOrUpdate(new Department() 
            {
                Dept_Name = "Academic"
            });
            _context.Departments.AddOrUpdate(new Department()
            {
                Dept_Name = "Medical"
            });
            _context.Departments.AddOrUpdate(new Department()
            {
                Dept_Name = "Departmental Reimbursement"
            });
        }
        
        /// <summary>
        /// Seed Method For Adding Or Updating Employees
        /// </summary>
        /// <param name="_context">Context</param>
        private void AddOrUpdateEmployees(ApricotContext _context) 
        {
            _context.Employees.AddOrUpdate(new Employee() 
            {
                Emp_Name = "Ankur Tyagi",
                Is_Active = false
            });
            _context.Employees.AddOrUpdate(new Employee()
            {
                Emp_Name = "Dev Kumar",
                Is_Active = true
            });
            _context.Employees.AddOrUpdate(new Employee()
            {
                Emp_Name = "Prashant Kumar",
                Is_Active = true
            });
        }

        /// <summary>
        /// Seed Method For Adding Or Updating Employee Details
        /// </summary>
        /// <param name="_context">Context</param>
        private void AddOrUpdateEmployeeDetails(ApricotContext _context) 
        {
            _context.Employee_Details.AddOrUpdate(new Employee_Detail() 
            {
                Emp_Address = "Ghaziabad, Delhi",
                Emp_Contact_No = "+918786456743",
                Emp_Gender = 'M'
            });
            _context.Employee_Details.AddOrUpdate(new Employee_Detail()
            {
                Emp_Address = "Mahakoshal Nagar, Adhartal, Jabalpur, pin code = 482004",
                Emp_Contact_No = "+918756432453",
                Emp_Gender = 'M'
            });
            _context.Employee_Details.AddOrUpdate(new Employee_Detail()
            {
                Emp_Address = "Seva Nagar, Ghaziabad, Delhi",
                Emp_Contact_No = "+919645734562",
                Emp_Gender = 'M'
            });
        }
    }
}
