namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perm_admin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbf.Permissions", "Admin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbf.Permissions", "Admin");
        }
    }
}
