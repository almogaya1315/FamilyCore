namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_req : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbf.FamilyMembers", new[] { "FamilyId" });
            DropIndex("dbf.FamilyMembers", new[] { "PermissionId" });
            DropIndex("dbf.FamilyMembers", new[] { "ContactInfoId" });
            AlterColumn("dbf.FamilyMembers", "FamilyId", c => c.Int());
            AlterColumn("dbf.FamilyMembers", "PermissionId", c => c.Int());
            AlterColumn("dbf.FamilyMembers", "ContactInfoId", c => c.Int());
            CreateIndex("dbf.FamilyMembers", "FamilyId");
            CreateIndex("dbf.FamilyMembers", "PermissionId");
            CreateIndex("dbf.FamilyMembers", "ContactInfoId");
        }
        
        public override void Down()
        {
            DropIndex("dbf.FamilyMembers", new[] { "FamilyId" });
            DropIndex("dbf.FamilyMembers", new[] { "ContactInfoId" });
            DropIndex("dbf.FamilyMembers", new[] { "PermissionId" });
            AlterColumn("dbf.FamilyMembers", "FamilyId", c => c.Int(nullable: false));
            AlterColumn("dbf.FamilyMembers", "ContactInfoId", c => c.Int(nullable: false));
            AlterColumn("dbf.FamilyMembers", "PermissionId", c => c.Int(nullable: false));
            CreateIndex("dbf.FamilyMembers", "FamilyId");
            CreateIndex("dbf.FamilyMembers", "ContactInfoId");
            CreateIndex("dbf.FamilyMembers", "PermissionId");
        }
    }
}
