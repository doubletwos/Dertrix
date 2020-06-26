namespace Zeus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1232 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredUsers", "SelectedOrg", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisteredUsers", "SelectedOrg");
        }
    }
}
