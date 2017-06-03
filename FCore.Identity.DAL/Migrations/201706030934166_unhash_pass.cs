namespace FCore.Identity.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unhash_pass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Password");
        }
    }
}
