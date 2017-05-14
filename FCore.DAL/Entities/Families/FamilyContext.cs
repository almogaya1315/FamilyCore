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

namespace FCore.DAL.Entities.Families
{


    public class FamilyContext : DbContext
    {
        public FamilyContext()
            : base("name=FamilyContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FamilyMemberEntity>()
                .HasRequired(e => e.ContactInfo)
                .WithMany()
                .HasForeignKey(e => e.ContactInfoId);

            modelBuilder.Entity<FamilyMemberEntity>()
                .HasRequired(e => e.Permissions)
                .WithMany()
                .HasForeignKey(e => e.PermissionId);

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
                toUpdate.Family = GetFamily(toUpdate.FamilyId);
                toUpdate.ContactInfo = GetContactInfo(toUpdate.ContactInfoId);
                toUpdate.ContactInfo.ContactBook = GetContactBook(toUpdate.ContactInfo.ContactBookId);

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
        public FamilyMemberEntity CreateChild(int creatorId, FamilyMemberEntity postedEntity, string relationship)
        {
            var creator = GetFamilyMember(creatorId);
            if (creator.LastName == postedEntity.LastName)
            {
                postedEntity.Family = GetFamily(creator.FamilyId);
                postedEntity.FamilyId = creator.FamilyId;

                var contactBook = GetContactBook(creator.ContactInfoId);
                if (contactBook != null)
                {
                    postedEntity.ContactInfo.ContactBook = contactBook;
                    postedEntity.ContactInfo.ContactBookId = contactBook.Id;
                }

                postedEntity.Permissions = new MemberPermissions();
                if (postedEntity.Relatives == null)
                {
                    postedEntity.Relatives = new List<MemberRelative>();
                }

                var gender = Enum.Parse(typeof(GenderType), creator.Gender);
                var rel = Enum.Parse(typeof(RelationshipType), relationship);
                postedEntity.Relatives.Add(new MemberRelative()
                {
                    Member = postedEntity,
                    Relative = creator,
                    Relationship = TreeHelper.GetOppositeRelationship((RelationshipType)rel, (GenderType)gender)
                });
                creator.Relatives.Add(new MemberRelative()
                {
                    Member = creator,
                    Relative = postedEntity,
                    Relationship = relationship
                });

                SaveChanges();
            }
            return postedEntity;
        }
    }
}