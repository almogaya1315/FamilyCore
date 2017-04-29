namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ci_memberId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbf.ContactInfoes", "MemberId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbf.ContactInfoes", "MemberId");
        }
    }
}
