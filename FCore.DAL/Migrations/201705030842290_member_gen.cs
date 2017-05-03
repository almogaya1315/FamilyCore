namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class member_gen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbf.FamilyMembers", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbf.FamilyMembers", "Gender");
        }
    }
}
