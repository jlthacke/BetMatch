namespace BetMatch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAdminApproved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsAdminApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsAdminApproved");
        }
    }
}
