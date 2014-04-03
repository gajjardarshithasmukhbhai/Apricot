namespace Apricot.Web.Migrations
{
    using Apricot.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Apricot.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Apricot.Web.Models.ApplicationDbContext context)
        {
            //Always use AddorUpdate to prevent seeding of Duplicate Data
            this.AddRoles();
            this.AddAdmins();
        }

        bool AddRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success) return success;

            success = idManager.CreateRole("Employee");
            if (!success) return success;

            success = idManager.CreateRole("Finance Manager");
            if (!success) return success;

            success = idManager.CreateRole("Manager");
            if (!success) return success;

            return success;
        }

        bool AddAdmins()
        {
            bool success = false;

            var user = new ApplicationUser() {
                UserName = "admin",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@apricot.com"
            };
            
            var idManager = new IdentityManager();

            success = idManager.CreateUser(user, "admin123");
            if (!success) return success;

            success = idManager.AddUserToRole(user.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(user.Id, "Employee");
            if (!success) return success;

            success = idManager.AddUserToRole(user.Id, "Finance Manager");
            if (!success) return success;

            success = idManager.AddUserToRole(user.Id, "Manager");
            if (!success) return success;

            return success;
        }

    }
}
