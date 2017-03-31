namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbf.Permissions", "MemberId", "dbf.FamilyMembers");
            DropIndex("dbf.Permissions", new[] { "MemberId" });
            DropColumn("dbf.Permissions", "MemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbf.Permissions", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbf.Permissions", "MemberId");
            AddForeignKey("dbf.Permissions", "MemberId", "dbf.FamilyMembers", "Id");
        }
    }
}
