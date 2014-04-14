namespace Apricot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Emp_ID = c.Long(nullable: false),
                        Account_No = c.Long(nullable: false),
                        Bank_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Emp_ID)
                .ForeignKey("dbo.Banks", t => t.Bank_ID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Emp_ID)
                .Index(t => t.Emp_ID)
                .Index(t => t.Bank_ID);
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Bank_ID = c.Long(nullable: false, identity: true),
                        Bank_Name = c.String(nullable: false, maxLength: 75),
                        Branch_Name = c.String(nullable: false, maxLength: 75),
                        Branch_City = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Bank_ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Emp_ID = c.Long(nullable: false, identity: true),
                        Emp_No = c.String(nullable: false, maxLength: 15),
                        Emp_Name = c.String(nullable: false, maxLength: 50),
                        Is_Active = c.Boolean(nullable: false),
                        Dept_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Emp_ID)
                .ForeignKey("dbo.Departments", t => t.Dept_ID, cascadeDelete: true)
                .Index(t => t.Dept_ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Dept_ID = c.Long(nullable: false, identity: true),
                        Dept_Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Dept_ID);
            
            CreateTable(
                "dbo.Bill_Detail",
                c => new
                    {
                        Bill_ID = c.Long(nullable: false),
                        Bill_Amount = c.Double(nullable: false),
                        Bill_Date = c.DateTime(nullable: false),
                        Bill_Type = c.String(nullable: false, maxLength: 50),
                        Bill_ModeOfPayment = c.Int(nullable: false),
                        Bill_have_SCopy = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Bill_ID)
                .ForeignKey("dbo.Bills", t => t.Bill_ID)
                .Index(t => t.Bill_ID);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Bill_ID = c.Long(nullable: false, identity: true),
                        Bill_Status = c.Int(nullable: false),
                        Emp_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Bill_ID)
                .ForeignKey("dbo.Employees", t => t.Emp_ID, cascadeDelete: true)
                .Index(t => t.Emp_ID);
            
            CreateTable(
                "dbo.Bill_FM",
                c => new
                    {
                        Bill_FM_ID = c.Long(nullable: false),
                        Bill_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bill_FM_ID, t.Bill_ID })
                .ForeignKey("dbo.Bills", t => t.Bill_ID, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.Bill_FM_ID, cascadeDelete: false)
                .Index(t => t.Bill_FM_ID)
                .Index(t => t.Bill_ID);
            
            CreateTable(
                "dbo.Bill_M",
                c => new
                    {
                        Emp_ID = c.Long(nullable: false),
                        Bill_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Emp_ID, t.Bill_ID })
                .ForeignKey("dbo.Bills", t => t.Bill_ID, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.Emp_ID, cascadeDelete: false)
                .Index(t => t.Emp_ID)
                .Index(t => t.Bill_ID);
            
            CreateTable(
                "dbo.Bill_SCopy",
                c => new
                    {
                        Bill_ID = c.Long(nullable: false),
                        SCopy = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Bill_ID)
                .ForeignKey("dbo.Bills", t => t.Bill_ID)
                .Index(t => t.Bill_ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CmtID = c.Long(nullable: false, identity: true),
                        Cmt_Subject = c.String(nullable: false, maxLength: 100),
                        Cmt_Body = c.String(nullable: false, maxLength: 1000),
                        Bill_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CmtID)
                .ForeignKey("dbo.Bills", t => t.Bill_ID, cascadeDelete: true)
                .Index(t => t.Bill_ID);
            
            CreateTable(
                "dbo.Employee_Detail",
                c => new
                    {
                        Emp_ID = c.Long(nullable: false),
                        Emp_Address = c.String(nullable: false, maxLength: 100),
                        Emp_Contact_No = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Emp_ID)
                .ForeignKey("dbo.Employees", t => t.Emp_ID)
                .Index(t => t.Emp_ID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Ntf_ID = c.Long(nullable: false, identity: true),
                        Ntf_Subject = c.String(nullable: false, maxLength: 100),
                        Ntf_Body = c.String(nullable: false, maxLength: 1000),
                        Seen = c.Boolean(nullable: false),
                        Emp_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Ntf_ID)
                .ForeignKey("dbo.Employees", t => t.Emp_ID, cascadeDelete: true)
                .Index(t => t.Emp_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Notifications", "Emp_ID", "dbo.Employees");
            DropForeignKey("dbo.Employee_Detail", "Emp_ID", "dbo.Employees");
            DropForeignKey("dbo.Comments", "Bill_ID", "dbo.Bills");
            DropForeignKey("dbo.Bill_SCopy", "Bill_ID", "dbo.Bills");
            DropForeignKey("dbo.Bill_M", "Emp_ID", "dbo.Employees");
            DropForeignKey("dbo.Bill_M", "Bill_ID", "dbo.Bills");
            DropForeignKey("dbo.Bill_FM", "Bill_FM_ID", "dbo.Employees");
            DropForeignKey("dbo.Bill_FM", "Bill_ID", "dbo.Bills");
            DropForeignKey("dbo.Bill_Detail", "Bill_ID", "dbo.Bills");
            DropForeignKey("dbo.Bills", "Emp_ID", "dbo.Employees");
            DropForeignKey("dbo.Accounts", "Emp_ID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Dept_ID", "dbo.Departments");
            DropForeignKey("dbo.Accounts", "Bank_ID", "dbo.Banks");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Notifications", new[] { "Emp_ID" });
            DropIndex("dbo.Employee_Detail", new[] { "Emp_ID" });
            DropIndex("dbo.Comments", new[] { "Bill_ID" });
            DropIndex("dbo.Bill_SCopy", new[] { "Bill_ID" });
            DropIndex("dbo.Bill_M", new[] { "Bill_ID" });
            DropIndex("dbo.Bill_M", new[] { "Emp_ID" });
            DropIndex("dbo.Bill_FM", new[] { "Bill_ID" });
            DropIndex("dbo.Bill_FM", new[] { "Bill_FM_ID" });
            DropIndex("dbo.Bills", new[] { "Emp_ID" });
            DropIndex("dbo.Bill_Detail", new[] { "Bill_ID" });
            DropIndex("dbo.Employees", new[] { "Dept_ID" });
            DropIndex("dbo.Accounts", new[] { "Bank_ID" });
            DropIndex("dbo.Accounts", new[] { "Emp_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.Employee_Detail");
            DropTable("dbo.Comments");
            DropTable("dbo.Bill_SCopy");
            DropTable("dbo.Bill_M");
            DropTable("dbo.Bill_FM");
            DropTable("dbo.Bills");
            DropTable("dbo.Bill_Detail");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Banks");
            DropTable("dbo.Accounts");
        }
    }
}
