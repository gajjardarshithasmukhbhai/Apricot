namespace Apricot.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpIdOnComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Emp_ID", c => c.Long(nullable: false));
            CreateIndex("dbo.Comments", "Emp_ID");
            AddForeignKey("dbo.Comments", "Emp_ID", "dbo.Employees", "Emp_ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Emp_ID", "dbo.Employees");
            DropIndex("dbo.Comments", new[] { "Emp_ID" });
            DropColumn("dbo.Comments", "Emp_ID");
        }
    }
}
