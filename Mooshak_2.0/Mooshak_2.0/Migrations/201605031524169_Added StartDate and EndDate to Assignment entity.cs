namespace Mooshak_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStartDateandEndDatetoAssignmententity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Assignments", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "EndTime");
            DropColumn("dbo.Assignments", "StartTime");
        }
    }
}
