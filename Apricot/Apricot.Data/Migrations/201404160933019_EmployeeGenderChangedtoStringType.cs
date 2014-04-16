namespace Apricot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeGenderChangedtoStringType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee_Detail", "Emp_Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee_Detail", "Emp_Gender");
        }
    }
}
