namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_req_ci_cbId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbf.ContactInfoes", new[] { "ContactBookId" });
            AlterColumn("dbf.ContactInfoes", "ContactBookId", c => c.Int());
            CreateIndex("dbf.ContactInfoes", "ContactBookId");
        }
        
        public override void Down()
        {
            DropIndex("dbf.ContactInfoes", new[] { "ContactBookId" });
            AlterColumn("dbf.ContactInfoes", "ContactBookId", c => c.Int(nullable: false));
            CreateIndex("dbf.ContactInfoes", "ContactBookId");
        }
    }
}
