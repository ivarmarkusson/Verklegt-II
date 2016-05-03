namespace Mooshak_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradeaddedtoMilestoneentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentMilestones", "Grade", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentMilestones", "Grade");
        }
    }
}
