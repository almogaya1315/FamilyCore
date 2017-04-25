namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAbout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbf.FamilyMembers", "About", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbf.FamilyMembers", "About");
        }
    }
}
