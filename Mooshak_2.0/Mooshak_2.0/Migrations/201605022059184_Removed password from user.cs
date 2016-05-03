namespace Mooshak_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedpasswordfromuser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String(nullable: false));
        }
    }
}
