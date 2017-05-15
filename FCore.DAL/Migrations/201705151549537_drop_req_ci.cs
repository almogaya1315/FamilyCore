namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_req_ci : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbf.ContactInfoes", "Street", c => c.String(maxLength: 40));
            AlterColumn("dbf.ContactInfoes", "PhoneNo", c => c.String(maxLength: 11));
            AlterColumn("dbf.ContactInfoes", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbf.ContactInfoes", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbf.ContactInfoes", "PhoneNo", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbf.ContactInfoes", "Street", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
