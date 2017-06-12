using FCore.DAL.Entities.Albums;
using FCore.DAL.Entities.ChatGroups;
using FCore.DAL.Entities.Contacts;
using FCore.DAL.Entities.Members;
using FCore.DAL.Entities.Videos;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System;
using System.Collections;
using System.Linq.Expressions;
using FCore.Common.Utils;
using FCore.Common.Enums;
using FCore.Common.Models.Users;

namespace FCore.DAL.Entities.Families
{


    public class FamilyContext : DbContext
    {
        public FamilyContext()
            : base("name=FamilyContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<FamilyMemberEntity>()
            //    .HasRequired(e => e.ContactInfo)
            //    .WithMany()
            //    .HasForeignKey(e => e.ContactInfoId);

            //modelBuilder.Entity<FamilyMemberEntity>()
            //    .HasRequired(e => e.Permissions)
            //    .WithMany()
            //    .HasForeignKey(e => e.PermissionId);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public virtual DbSet<FamilyEntity> Families { get; set; }
        public virtual DbSet<FamilyMemberEntity> FamilyMembers { get; set; }
        public virtual DbSet<ContactBookEntity> ContactBooks { get; set; }
        public virtual DbSet<ContactInfoEntity> ContactInfoes { get; set; }
        public virtual DbSet<AlbumEntity> Albums { get; set; }
        public virtual DbSet<ImageEntity> Images { get; set; }
        public virtual DbSet<VideoLibraryEntity> VideoLibraries { get; set; }
        public virtual DbSet<VideoEntity> Videos { get; set; }
        public virtual DbSet<ChatGroupEntity> ChatGroups { get; set; }
        public virtual DbSet<MessageEntity> Messages { get; set; }
        public virtual DbSet<MemberPermissions> Permissions { get; set; }
        public virtual DbSet<MemberRelative> Relationships { get; set; }

        public ICollection<FamilyEntity> GetFamilies()
        {
            ICollection<FamilyEntity> families = new List<FamilyEntity>();
            foreach (FamilyEntity family in Families)
                families.Add(family);
            return families;
        }
        public FamilyEntity GetFamily(string name)
        {
            foreach (FamilyEntity family in Families)
            {
                if (family.Name == name) return family;
            }
            return null;

            //return Families.FirstOrDefault(f => f.Name == name);
        }
        public FamilyEntity GetFamily(int id)
        {
            foreach (FamilyEntity family in Families)
            {
                if (family.Id == id) return family;
            }
            return null;

            //return Families.FirstOrDefault(f => f.Id == id);
        }
        public FamilyEntity CreateFamily(string familyName)
        {
            var family = new FamilyEntity() { Name = familyName, };
            Families.Add(family);
            SaveChanges();
            return family;
        }

        public FamilyMemberEntity GetFamilyMember(int id)
        {
            foreach (FamilyMemberEntity member in FamilyMembers)
            {
                if (member.Id == id) return member;
            }
            return null;

            //return FamilyMembers.FirstOrDefault(m => m.Id == id);
        }
        public FamilyMemberEntity GetLastMemberJoined()
        {
            int highestId = int.MinValue;
            foreach (FamilyMemberEntity member in FamilyMembers)
            {
                if (member.Id > highestId) highestId = member.Id;
            }
            return GetFamilyMember(highestId);

            //return FamilyMembers.Last();
        }
        //public FamilyMemberEntity CreateFamilyMember(FamilyMemberEntity postedEntity)
        //{
        //    return new FamilyMemberEntity()
        //    {
        //        About = postedEntity.About,
        //        BirthDate = postedEntity.BirthDate,
        //        BirthPlace = postedEntity.BirthPlace,
        //        FirstName = postedEntity.FirstName,
        //        Gender = postedEntity.Gender,
        //        LastName = postedEntity.LastName,
        //        ProfileImagePath = postedEntity.ProfileImagePath
        //    };
        //}

        public MemberPermissions GetPermissionsEntity(int id)
        {
            foreach (MemberPermissions perms in Permissions)
            {
                if (perms.Id == id) return perms;
            }
            return null;
        }

        public ICollection<AlbumEntity> GetAlbums()
        {
            return Albums.ToList();
        }
        public AlbumEntity GetAlbum(int id)
        {
            foreach (AlbumEntity album in Albums)
            {
                if (album.Id == id) return album;
            }
            return null;

            //return Albums.FirstOrDefault(a => a.Id == id);
        }
        public ImageEntity GetLastImageUploaded()
        {
            int highestId = int.MinValue;
            foreach (ImageEntity image in Images)
            {
                if (image.Id > highestId) highestId = image.Id;
            }
            return GetImage(highestId);

            //return Images.Last();
        }
        public ImageEntity GetImage(int id)
        {
            foreach (ImageEntity image in Images)
            {
                if (image.Id == id) return image;
            }
            return null;

            //return Images.FirstOrDefault(i => i.Id == id);
        }

        public ContactInfoEntity GetContactInfo(int id)
        {
            foreach (ContactInfoEntity contact in ContactInfoes)
            {
                if (contact.Id == id) return contact;
            }
            return null;

            //return ContactInfoes.FirstOrDefault(i => i.Id == id);
        }
        public ContactInfoEntity CreateContactInfo(ContactInfoEntity postedInfo, FamilyMemberEntity newChild)
        {
            return new ContactInfoEntity()
            {
                City = postedInfo.City,
                Country = postedInfo.Country,
                Email = postedInfo.Email,
                HouseNo = postedInfo.HouseNo,
                MemberId = newChild.Id,
                MemberName = $"{newChild.FirstName} {newChild.LastName}",
                PhoneNo = postedInfo.PhoneNo,
                Street = postedInfo.Street
            };
        }
        public ContactBookEntity GetContactBook(int id)
        {
            foreach (ContactBookEntity book in ContactBooks)
            {
                if (book.Id == id) return book;
            }
            return null;

            //return ContactBooks.FirstOrDefault(b => b.Id == id);
        }
        public ContactBookEntity GetContactBook(int contactInfoId = -1, ContactInfoEntity entity = null)
        {
            foreach (ContactBookEntity book in ContactBooks)
            {
                if (entity == null && contactInfoId != -1)
                {
                    return FindBook(book, contactInfoId);
                }
                else if ((contactInfoId == -1 && entity != null) || (contactInfoId != -1 && entity != null))
                {
                    return FindBook(book, entity.Id);
                }
            }
            return null;
        }
        ContactBookEntity FindBook(ContactBookEntity book, int id)
        {
            var contactInfo = book.ContactInfoes.FirstOrDefault(ci => ci.Id == id);
            if (contactInfo != null) return book;
            else return null;
        }
        public ContactBookEntity CreateContactBook(FamilyEntity family)
        {
            var book = new ContactBookEntity()
            {
                Family = family,
                FamilyId = family.Id,
                FamilyName = family.Name
            };
            ContactBooks.Add(book);
            SaveChanges();
            return book;
        }

        public VideoEntity GetMostViewedVideo()
        {
            foreach (VideoEntity video in Videos)
            {
                return video; // needs to have an 'entries' value in entity
            }
            return null;
        }
        public VideoLibraryEntity GetVideoLibrary(int id)
        {
            foreach (VideoLibraryEntity library in VideoLibraries)
            {
                if (library.Id == id) return library;
            }
            return null;

            //return VideoLibraries.FirstOrDefault(l => l.Id == id);
        }
        public ICollection<VideoLibraryEntity> GetVideoLibraries()
        {
            ICollection<VideoLibraryEntity> libraries = new List<VideoLibraryEntity>();
            foreach (VideoLibraryEntity library in VideoLibraries)
                libraries.Add(library);
            return libraries;
        }

        public ICollection<ChatGroupEntity> GetChatGroups()
        {
            return ChatGroups.ToList();
        }
        public ChatGroupEntity GetChatGroup(int id)
        {
            foreach (ChatGroupEntity group in ChatGroups)
            {
                if (group.Id == id) return group;
            }
            return null;

            //return ChatGroups.FirstOrDefault(g => g.Id == id);
        }
        public MessageEntity GetMessage(int id)
        {
            foreach (MessageEntity msg in Messages)
            {
                if (msg.Id == id) return msg;
            }
            return null;

            //return Messages.FirstOrDefault(m => m.Id == id);
        }

        public void UpdateUserAbout(FamilyMemberEntity entity, string about)
        {
            FamilyMemberEntity toUpdate = null;
            foreach (FamilyMemberEntity member in FamilyMembers)
            {
                if (member.Id == entity.Id)
                {
                    toUpdate = member;
                    break;
                }
            }
            if (toUpdate != null)
            {
                toUpdate.About = about;
                SaveChanges();
            }
        }
        public void UpdateMemberProfileImage(FamilyMemberEntity entity, string path)
        {
            FamilyMemberEntity toUpdate = null;
            foreach (FamilyMemberEntity member in FamilyMembers)
            {
                if (member.Id == entity.Id)
                {
                    toUpdate = member;
                    break;
                }
            }
            if (toUpdate != null)
            {
                toUpdate.ProfileImagePath = path;
                SaveChanges();
            }
        }
        public void UpdateUserDetails(ContactInfoEntity postedInfoEntity)
        {
            FamilyMemberEntity toUpdate = null;
            foreach (FamilyMemberEntity member in FamilyMembers)
            {
                if (member.Id == postedInfoEntity.MemberId)
                {
                    toUpdate = member;
                    break;
                }
            }
            if (toUpdate != null)
            {
                toUpdate.Family = GetFamily((int)toUpdate.FamilyId);
                toUpdate.ContactInfo = GetContactInfo((int)toUpdate.ContactInfoId);
                toUpdate.ContactInfo.ContactBook = GetContactBook((int)toUpdate.ContactInfo.ContactBookId);

                toUpdate.ContactInfo.Country = postedInfoEntity.Country;
                toUpdate.ContactInfo.City = postedInfoEntity.City;
                toUpdate.ContactInfo.Street = postedInfoEntity.Street;
                toUpdate.ContactInfo.HouseNo = postedInfoEntity.HouseNo;
                toUpdate.ContactInfo.PhoneNo = postedInfoEntity.PhoneNo;
                toUpdate.ContactInfo.Email = postedInfoEntity.Email;

                try
                {
                    SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to update database. {e.Message}");
                }
            }
            else
            {
                throw new NullReferenceException($"Member was not found by posted member id #{postedInfoEntity.MemberId}");
            }
        }
        public void UpdateUserPermissions(int memberId, MemberPermissions postedPermsEntity)
        {
            MemberPermissions toUpdate = null;
            foreach (MemberPermissions perms in Permissions)
            {
                if (perms.Id == postedPermsEntity.Id)
                {
                    toUpdate = perms;
                    break;
                }
            }

            if (toUpdate != null)
            {
                postedPermsEntity = PermissionHandler<MemberPermissions>.VerifyHierarchy(toUpdate, postedPermsEntity);

                toUpdate.Admin = postedPermsEntity.Admin;
                toUpdate.Create = postedPermsEntity.Create;
                toUpdate.Edit = postedPermsEntity.Edit;
                toUpdate.ManageChat = postedPermsEntity.ManageChat;

                try
                {
                    SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception($"Unable to update database. {e.Message}");
                }
            }
            else
            {
                throw new NullReferenceException($"Member permissions were not found by posted permissions id #{postedPermsEntity.Id}");
            }
        }
        public void UpdateMemberRelatives(FamilyMemberEntity member, MemberRelative createdRelativeRel)
        {
            FamilyMemberEntity toUpdate = null;
            foreach (var entity in FamilyMembers)
            {
                if (entity.Id == member.Id)
                {
                    toUpdate = entity;
                    break;
                }
            }

            toUpdate.Relatives.Add(createdRelativeRel);
            Relationships.Add(createdRelativeRel);
            Entry(createdRelativeRel).State = EntityState.Added;
            SaveChanges();

            var relative = GetFamilyMember(createdRelativeRel.RelativeId);
            var relativeCreatedRel = new MemberRelative()
            {
                Member = relative,
                MemberId = relative.Id,

                Relative = toUpdate,
                RelativeId = toUpdate.Id,

                Relationship = TreeHelper.GetOppositeRelationship((RelationshipType)Enum.Parse(typeof(RelationshipType), createdRelativeRel.Relationship),
                                                                  (GenderType)Enum.Parse(typeof(GenderType), toUpdate.Gender))
            };
            relative.Relatives.Add(relativeCreatedRel);
            Relationships.Add(relativeCreatedRel);
            Entry(relativeCreatedRel).State = EntityState.Added;
            SaveChanges();
        }

        public FamilyMemberEntity CreateMember(int relativeId, FamilyMemberEntity postedMember, string relationship)
        {
            var newMember = SaveMember(postedMember);

            newMember = SaveContactInfo(postedMember.ContactInfo, newMember);

            var relative = GetFamilyMember(relativeId);
            if (relative.LastName == newMember.LastName)
            {
                newMember = SaveFamily(newMember, relative, false);
                newMember = SaveContactBook(newMember, relative, false);
            }
            else
            {
                newMember = SaveFamily(newMember, relative, true);
                newMember = SaveContactBook(newMember, relative, true);
            }

            newMember = SaveRelationship(newMember, relative, relationship);

            return newMember;
        }

        FamilyMemberEntity SaveMember(FamilyMemberEntity postedEntity)
        {
            //var newMember = CreateFamilyMember(postedEntity);
            FamilyMembers.Add(postedEntity);
            Entry(postedEntity).State = EntityState.Added;
            try
            {
                SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return postedEntity;
        }
        FamilyMemberEntity SaveContactInfo(ContactInfoEntity postedInfo, FamilyMemberEntity newMember)
        {
            newMember.ContactInfo = CreateContactInfo(postedInfo, newMember);
            ContactInfoes.Add(newMember.ContactInfo);
            Entry(newMember.ContactInfo).State = EntityState.Added;
            SaveChanges();
            return newMember;
        }
        FamilyMemberEntity SaveFamily(FamilyMemberEntity newMember, FamilyMemberEntity creator, bool newFamily)
        {
            if (newFamily)
            {
                newMember.Family = CreateFamily(newMember.LastName);
                Families.Add(newMember.Family);
                newMember.Family.FamilyMembers.Add(newMember);
                Entry(newMember.Family).State = EntityState.Added;
                SaveChanges();
                newMember.FamilyId = newMember.Family.Id;
                SaveChanges();
            }
            else
            {
                newMember.Family = GetFamily((int)creator.FamilyId);
                newMember.Family.FamilyMembers.Add(newMember);
                Entry(newMember.Family).State = EntityState.Modified;
                newMember.FamilyId = creator.FamilyId;
                SaveChanges();
            }
            return newMember;
        }
        FamilyMemberEntity SaveContactBook(FamilyMemberEntity newMember, FamilyMemberEntity creator, bool newContactBook)
        {
            if (newContactBook)
            {
                newMember.ContactInfo.ContactBook = CreateContactBook(newMember.Family);
                ContactBooks.Add(newMember.ContactInfo.ContactBook);
                Entry(newMember.ContactInfo.ContactBook).State = EntityState.Added;
                SaveChanges();
                newMember.ContactInfo.ContactBookId = newMember.ContactInfo.ContactBook.Id;
                SaveChanges();
            }
            else
            {
                var contactBook = GetContactBook((int)creator.ContactInfoId, null);
                if (contactBook != null)
                {
                    newMember.ContactInfo.ContactBook = contactBook;
                    Entry(newMember.ContactInfo.ContactBook).State = EntityState.Modified;
                    newMember.ContactInfo.ContactBookId = contactBook.Id;
                    SaveChanges();
                }
                else throw new NullReferenceException("Unable to find creator's contact book.");
            }
            return newMember;
        }
        FamilyMemberEntity SaveRelationship(FamilyMemberEntity newMember, FamilyMemberEntity relative, string relationship)
        {
            relative.Relatives.Add(new MemberRelative()
            {
                Member = relative,
                Relative = newMember,
                RelativeId = newMember.Id,
                Relationship = relationship
            });
            Entry(relative.Relatives.LastOrDefault()).State = EntityState.Added;

            var relativeGender = Enum.Parse(typeof(GenderType), relative.Gender);
            var rel = Enum.Parse(typeof(RelationshipType), relationship);
            newMember.Relatives.Add(new MemberRelative()
            {
                Member = newMember,
                Relative = relative,
                RelativeId = relative.Id,
                Relationship = TreeHelper.GetOppositeRelationship((RelationshipType)rel, (GenderType)relativeGender)
            });
            Entry(newMember.Relatives.LastOrDefault()).State = EntityState.Added;

            SaveChanges();
            return newMember;
        }
    }
}