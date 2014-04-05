using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Apricot.Data.Models;

namespace Apricot.Data
{
    public class ApricotContext : DbContext
    {
        public ApricotContext()
            : base("ApricotDevDb")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Bill_Detail> Bill_Details { get; set; }

        public DbSet<Bill_FM> Bill_FMs { get; set; }

        public DbSet<Bill_SCopy> Bill_SCopies { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Employee_Detail> Employee_Details { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set Up Constraints, Indexes etc here using Fluent API
            //ForeignKey and Primary Key are not required to be Set as they are done by DataAnnotation

            base.OnModelCreating(modelBuilder);
        }
    }
}
