namespace FCore.DAL.Migrations
{
    using Common.Enums;
    using Entities;
    using Entities.Albums;
    using Entities.ChatGroups;
    using Entities.Contacts;
    using Entities.Families;
    using Entities.Members;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FCore.DAL.Entities.Families.FamilyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FamilyContext context)
        {
            //FamilyEntity family = new FamilyEntity()
            //{
            //    Name = "Matsliah"
            //};
            //context.Families.Add(family);
            //context.SaveChanges();

            //ContactBookEntity book = new ContactBookEntity()
            //{
            //    Family = context.Families.FirstOrDefault(),
            //    FamilyName = context.Families.FirstOrDefault().Name
            //};
            //context.ContactBooks.Add(book);
            //context.SaveChanges();

            //ContactInfoEntity info = new ContactInfoEntity()
            //{
            //    City = "Petah-Tikva",
            //    ContactBook = context.ContactBooks.FirstOrDefault(),
            //    Country = "Israel",
            //    Email = "liormatsliah1985@gmail.com",
            //    HouseNo = 16,
            //    MemberName = "Lior Matsliah",
            //    PhoneNo = "052-3751421",
            //    Street = "Haportsim"
            //};
            //context.ContactInfoes.Add(info);
            //context.SaveChanges();

            //FamilyMemberEntity member1 = new FamilyMemberEntity()
            //{
            //    BirthDate = new DateTime(1985, 5, 23),
            //    BirthPlace = "Beilinson hospital, Petah-Tikva",
            //    ContactInfo = context.ContactInfoes.FirstOrDefault(),
            //    Family = context.Families.FirstOrDefault(),
            //    FirstName = "Lior",
            //    LastName = context.Families.FirstOrDefault().Name,
            //    Permissions = new MemberPermissions(),
            //    ProfileImagePath = string.Empty
            //};
            //context.FamilyMembers.Add(member1);
            //context.SaveChanges();

            //AlbumEntity album = new AlbumEntity()
            //{
            //    Family = context.Families.FirstOrDefault(),
            //    FamilyName = context.Families.FirstOrDefault().Name
            //};
            //context.Albums.Add(album);
            //context.SaveChanges();

            //ImageEntity img = new ImageEntity()
            //{
            //    Album = context.Albums.FirstOrDefault(),
            //    Description = "Test",
            //    Path = string.Empty,
            //};
            //context.Images.Add(img);
            //context.SaveChanges();

            //ContactInfoEntity info2 = new ContactInfoEntity()
            //{
            //    City = "Petah-Tikva",
            //    ContactBook = context.ContactBooks.FirstOrDefault(),
            //    Country = "Israel",
            //    Email = "KreshQueen@gmail.com",
            //    HouseNo = 16,
            //    MemberName = "Keren Matsliah",
            //    PhoneNo = "0526758751",
            //    Street = "Haportsim"
            //};
            //context.ContactInfoes.Add(info2);
            //context.SaveChanges();

            //FamilyMemberEntity member2 = new FamilyMemberEntity()
            //{
            //    BirthDate = new DateTime(1984, 2, 5),
            //    BirthPlace = "Afula",
            //    ContactInfo = context.ContactInfoes.FirstOrDefault(c => c.MemberName.Contains("Keren")),
            //    Family = context.Families.FirstOrDefault(),
            //    FirstName = "Keren",
            //    LastName = context.Families.FirstOrDefault().Name,
            //    Permissions = new MemberPermissions(),
            //    ProfileImagePath = string.Empty
            //};
            //context.FamilyMembers.Add(member2);
            //context.SaveChanges();

            var member = context.FamilyMembers.FirstOrDefault(m => m.FirstName == "Lior");
            var relative = context.FamilyMembers.FirstOrDefault(m => m.FirstName == "keren");
            member.Relatives.Add(new MemberRelative(member, relative, RelationshipType.אישה));
            context.SaveChanges();

            //ChatGroupEntity group = new ChatGroupEntity()
            //{
            //    Manager = context.FamilyMembers.FirstOrDefault(),
            //    Name = "Test"
            //};
            //context.ChatGroups.Add(group);
            //context.SaveChanges();

            //MessageEntity msg = new MessageEntity()
            //{
            //    Content = "Test Message",
            //    Group = context.ChatGroups.FirstOrDefault(),
            //    Sender = context.FamilyMembers.FirstOrDefault(),
            //    Reciever = context.FamilyMembers.FirstOrDefault(m => m.FirstName == "Keren")
            //};
            //context.Messages.Add(msg);
            //context.SaveChanges();
        }
    }
}
