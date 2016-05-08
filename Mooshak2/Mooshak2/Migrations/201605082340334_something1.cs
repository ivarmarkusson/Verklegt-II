namespace Mooshak2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class something1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Course_ID", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "Course_ID" });
            DropColumn("dbo.AspNetUsers", "Course_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Course_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Course_ID");
            AddForeignKey("dbo.AspNetUsers", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
