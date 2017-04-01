namespace FCore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbf.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        FamilyId = c.Int(nullable: false),
                        FamilyName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.Families", t => t.FamilyId)
                .Index(t => t.FamilyId);
            
            CreateTable(
                "dbf.Families",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbf.ChatGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        FamilyId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.Families", t => t.FamilyId)
                .ForeignKey("dbf.FamilyMembers", t => t.ManagerId)
                .Index(t => t.FamilyId)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbf.FamilyMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        ContactInfoId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 40),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(nullable: false, maxLength: 100),
                        ProfileImagePath = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.ContactInfoes", t => t.ContactInfoId)
                .ForeignKey("dbf.Families", t => t.FamilyId)
                .ForeignKey("dbf.Permissions", t => t.PermissionId)
                .Index(t => t.FamilyId)
                .Index(t => t.PermissionId)
                .Index(t => t.ContactInfoId);
            
            CreateTable(
                "dbf.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactBookId = c.Int(nullable: false),
                        MemberName = c.String(nullable: false, maxLength: 30),
                        Country = c.String(nullable: false, maxLength: 40),
                        City = c.String(nullable: false, maxLength: 40),
                        Street = c.String(nullable: false, maxLength: 40),
                        HouseNo = c.Int(),
                        PhoneNo = c.String(nullable: false, maxLength: 11),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.ContactBooks", t => t.ContactBookId)
                .Index(t => t.ContactBookId);
            
            CreateTable(
                "dbf.ContactBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FamilyId = c.Int(nullable: false),
                        FamilyName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.Families", t => t.FamilyId)
                .Index(t => t.FamilyId);
            
            CreateTable(
                "dbf.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Create = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        ManageChat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbf.Relatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        RelativeId = c.Int(nullable: false),
                        Relationship = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.FamilyMembers", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbf.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        RecieverId = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.ChatGroups", t => t.GroupId)
                .ForeignKey("dbf.FamilyMembers", t => t.RecieverId)
                .ForeignKey("dbf.FamilyMembers", t => t.SenderId)
                .Index(t => t.GroupId)
                .Index(t => t.SenderId)
                .Index(t => t.RecieverId);
            
            CreateTable(
                "dbf.VideoLibraries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        FamilyId = c.Int(nullable: false),
                        FamilyName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.Families", t => t.FamilyId)
                .Index(t => t.FamilyId);
            
            CreateTable(
                "dbf.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libraryid = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                        Path = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.VideoLibraries", t => t.Libraryid)
                .Index(t => t.Libraryid);
            
            CreateTable(
                "dbf.images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                        Path = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbf.Albums", t => t.AlbumId)
                .Index(t => t.AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbf.images", "AlbumId", "dbf.Albums");
            DropForeignKey("dbf.Videos", "Libraryid", "dbf.VideoLibraries");
            DropForeignKey("dbf.VideoLibraries", "FamilyId", "dbf.Families");
            DropForeignKey("dbf.Messages", "SenderId", "dbf.FamilyMembers");
            DropForeignKey("dbf.Messages", "RecieverId", "dbf.FamilyMembers");
            DropForeignKey("dbf.Messages", "GroupId", "dbf.ChatGroups");
            DropForeignKey("dbf.ChatGroups", "ManagerId", "dbf.FamilyMembers");
            DropForeignKey("dbf.Relatives", "MemberId", "dbf.FamilyMembers");
            DropForeignKey("dbf.FamilyMembers", "PermissionId", "dbf.Permissions");
            DropForeignKey("dbf.FamilyMembers", "FamilyId", "dbf.Families");
            DropForeignKey("dbf.FamilyMembers", "ContactInfoId", "dbf.ContactInfoes");
            DropForeignKey("dbf.ContactBooks", "FamilyId", "dbf.Families");
            DropForeignKey("dbf.ContactInfoes", "ContactBookId", "dbf.ContactBooks");
            DropForeignKey("dbf.ChatGroups", "FamilyId", "dbf.Families");
            DropForeignKey("dbf.Albums", "FamilyId", "dbf.Families");
            DropIndex("dbf.images", new[] { "AlbumId" });
            DropIndex("dbf.Videos", new[] { "Libraryid" });
            DropIndex("dbf.VideoLibraries", new[] { "FamilyId" });
            DropIndex("dbf.Messages", new[] { "RecieverId" });
            DropIndex("dbf.Messages", new[] { "SenderId" });
            DropIndex("dbf.Messages", new[] { "GroupId" });
            DropIndex("dbf.Relatives", new[] { "MemberId" });
            DropIndex("dbf.ContactBooks", new[] { "FamilyId" });
            DropIndex("dbf.ContactInfoes", new[] { "ContactBookId" });
            DropIndex("dbf.FamilyMembers", new[] { "ContactInfoId" });
            DropIndex("dbf.FamilyMembers", new[] { "PermissionId" });
            DropIndex("dbf.FamilyMembers", new[] { "FamilyId" });
            DropIndex("dbf.ChatGroups", new[] { "ManagerId" });
            DropIndex("dbf.ChatGroups", new[] { "FamilyId" });
            DropIndex("dbf.Albums", new[] { "FamilyId" });
            DropTable("dbf.images");
            DropTable("dbf.Videos");
            DropTable("dbf.VideoLibraries");
            DropTable("dbf.Messages");
            DropTable("dbf.Relatives");
            DropTable("dbf.Permissions");
            DropTable("dbf.ContactBooks");
            DropTable("dbf.ContactInfoes");
            DropTable("dbf.FamilyMembers");
            DropTable("dbf.ChatGroups");
            DropTable("dbf.Families");
            DropTable("dbf.Albums");
        }
    }
}
