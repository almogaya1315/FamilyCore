namespace FCore.DAL.Migrations
{
    using Common.Enums;
    using Entities;
    using Entities.Albums;
    using Entities.ChatGroups;
    using Entities.Contacts;
    using Entities.Families;
    using Entities.Members;
    using Entities.Videos;
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
            //FamilyMemberEntity m1 = context.FamilyMembers.FirstOrDefault(m => m.Id == 1);
            //m1.Permissions.Admin = true;
            //context.Entry(m1).State = EntityState.Modified;
            //context.SaveChanges();

            //ContactInfoEntity info1 = context.ContactInfoes.FirstOrDefault(ci => ci.Id == 1);
            //info1.MemberId = 1;
            //context.SaveChanges();
            //ContactInfoEntity info2 = context.ContactInfoes.FirstOrDefault(ci => ci.Id == 2);
            //info2.MemberId = 2;
            //context.SaveChanges();

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
            //    Name = "First album",
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
            //    Email = "kreshqueen@gmail.com",
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
            //    BirthPlace = "Ha-Emek hospital, Afula",
            //    ContactInfo = context.ContactInfoes.FirstOrDefault(c => c.MemberName.Contains("Keren")),
            //    Family = context.Families.FirstOrDefault(),
            //    FirstName = "Keren",
            //    LastName = context.Families.FirstOrDefault().Name,
            //    Permissions = new MemberPermissions(),
            //    ProfileImagePath = string.Empty
            //};
            //context.FamilyMembers.Add(member2);
            //context.SaveChanges();

            //var member = context.FamilyMembers.FirstOrDefault(m => m.FirstName == "Lior");
            //var relative = context.FamilyMembers.FirstOrDefault(m => m.FirstName == "keren");
            //member.Relatives.Add(new MemberRelative(member, relative, RelationshipType.אישה));
            //context.SaveChanges();

            //relative.Relatives.Add(new MemberRelative(relative, member, RelationshipType.בעל));
            //context.SaveChanges();

            //ChatGroupEntity group = new ChatGroupEntity()
            //{
            //    Family = context.Families.FirstOrDefault(),
            //    Manager = context.FamilyMembers.FirstOrDefault(),
            //    Name = "First group"
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

            //VideoLibraryEntity library = new VideoLibraryEntity()
            //{
            //    Family = context.Families.FirstOrDefault(),
            //    Name = "First library",
            //    FamilyName = context.Families.FirstOrDefault().Name
            //};
            //context.VideoLibraries.Add(library);
            //context.SaveChanges();

            //VideoEntity video = new VideoEntity()
            //{
            //    Description = "Test",
            //    Library = context.VideoLibraries.FirstOrDefault(),
            //    Path = string.Empty
            //};
            //context.Videos.Add(video);
            //context.SaveChanges();
        }
    }
}
