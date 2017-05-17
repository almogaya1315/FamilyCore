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
        public FamilyMemberEntity CreateFamilyMember(FamilyMemberEntity postedEntity)
        {
            return new FamilyMemberEntity()
            {
                About = postedEntity.About,
                BirthDate = postedEntity.BirthDate,
                BirthPlace = postedEntity.BirthPlace,
                FirstName = postedEntity.FirstName,
                Gender = postedEntity.Gender,
                LastName = postedEntity.LastName,
                ProfileImagePath = postedEntity.ProfileImagePath
            };
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
        public void UpdateMemberRelatives(FamilyMemberEntity member)
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

            foreach (var rel in member.Relatives)
            {
                if (!toUpdate.Relatives.Contains(rel))
                {
                    toUpdate.Relatives.Add(rel);
                    Relationships.Add(rel);
                    Entry(rel).State = EntityState.Added;
                    SaveChanges();

                    var r = new MemberRelative()
                    {
                        Member = rel.Relative,
                        MemberId = rel.Relative.Id,

                        Relative = toUpdate,
                        RelativeId = toUpdate.Id,

                        Relationship = TreeHelper.GetOppositeRelationship((RelationshipType)Enum.Parse(typeof(RelationshipType), rel.Relationship), 
                                                                          (GenderType)Enum.Parse(typeof(GenderType), toUpdate.Gender))
                    };
                    rel.Relative.Relatives.Add(r);
                    Relationships.Add(r);
                    Entry(r).State = EntityState.Added;
                    SaveChanges();
                }
            }
        }

        public FamilyMemberEntity CreateChild(int creatorId, FamilyMemberEntity postedEntity, string relationship)
        {
            var newChild = SaveChild(postedEntity);

            newChild = SaveContactInfo(postedEntity.ContactInfo, newChild);

            var creator = GetFamilyMember(creatorId);
            if (creator.LastName == newChild.LastName)
            {
                newChild = SaveFamily(newChild, creator, false);
                newChild = SaveContactBook(newChild, creator, false);
            }
            else
            {
                newChild = SaveFamily(newChild, creator, true);
                newChild = SaveContactBook(newChild, creator, true);
            }

            newChild = SaveRelationship(newChild, creator, relationship);

            return newChild;
        }

        FamilyMemberEntity SaveChild(FamilyMemberEntity postedEntity)
        {
            var newChild = CreateFamilyMember(postedEntity);
            FamilyMembers.Add(newChild);
            Entry(newChild).State = EntityState.Added;
            SaveChanges();
            return newChild;
        }
        FamilyMemberEntity SaveContactInfo(ContactInfoEntity postedInfo, FamilyMemberEntity newChild)
        {
            newChild.ContactInfo = CreateContactInfo(postedInfo, newChild);
            ContactInfoes.Add(newChild.ContactInfo);
            Entry(newChild.ContactInfo).State = EntityState.Added;
            SaveChanges();
            return newChild;
        }
        FamilyMemberEntity SaveFamily(FamilyMemberEntity newChild, FamilyMemberEntity creator, bool newFamily)
        {
            if (newFamily)
            {
                newChild.Family = CreateFamily(newChild.LastName);
                Families.Add(newChild.Family);
                newChild.Family.FamilyMembers.Add(newChild);
                Entry(newChild.Family).State = EntityState.Added;
                SaveChanges();
                newChild.FamilyId = newChild.Family.Id;
                SaveChanges();
            }
            else
            {
                newChild.Family = GetFamily((int)creator.FamilyId);
                newChild.Family.FamilyMembers.Add(newChild);
                Entry(newChild.Family).State = EntityState.Modified;
                newChild.FamilyId = creator.FamilyId;
                SaveChanges();
            }
            return newChild;
        }
        FamilyMemberEntity SaveContactBook(FamilyMemberEntity newChild, FamilyMemberEntity creator, bool newContactBook)
        {
            if (newContactBook)
            {
                newChild.ContactInfo.ContactBook = CreateContactBook(newChild.Family);
                ContactBooks.Add(newChild.ContactInfo.ContactBook);
                Entry(newChild.ContactInfo.ContactBook).State = EntityState.Added;
                SaveChanges();
                newChild.ContactInfo.ContactBookId = newChild.ContactInfo.ContactBook.Id;
                SaveChanges();
            }
            else
            {
                var contactBook = GetContactBook((int)creator.ContactInfoId, null);
                if (contactBook != null)
                {
                    newChild.ContactInfo.ContactBook = contactBook;
                    Entry(newChild.ContactInfo.ContactBook).State = EntityState.Modified;
                    newChild.ContactInfo.ContactBookId = contactBook.Id;
                    SaveChanges();
                }
                else throw new NullReferenceException("Unable to find creator's contact book.");
            }
            return newChild;
        }
        FamilyMemberEntity SaveRelationship(FamilyMemberEntity newChild, FamilyMemberEntity creator, string relationship)
        {
            creator.Relatives.Add(new MemberRelative()
            {
                Member = creator,
                Relative = newChild,
                RelativeId = newChild.Id,
                Relationship = relationship
            });
            Entry(creator.Relatives.LastOrDefault()).State = EntityState.Added;

            var creatorGender = Enum.Parse(typeof(GenderType), creator.Gender);
            var rel = Enum.Parse(typeof(RelationshipType), relationship);
            newChild.Relatives.Add(new MemberRelative()
            {
                Member = newChild,
                Relative = creator,
                RelativeId = creator.Id,
                Relationship = TreeHelper.GetOppositeRelationship((RelationshipType)rel, (GenderType)creatorGender)
            });
            Entry(newChild.Relatives.LastOrDefault()).State = EntityState.Added;

            SaveChanges();
            return newChild;
        }

        // for debug. very specific. needs to be modified every run.
        public void DeletePreviousCreatedMember()
        {
            var relToRemove = new List<MemberRelative>();
            foreach (var rel in Relationships)
            {
                if (rel.Relationship == RelationshipType.Mother.ToString() || rel.Relationship == RelationshipType.Daughter.ToString())
                {
                    relToRemove.Add(rel);
                }
            }
            Relationships.RemoveRange(relToRemove);
            SaveChanges();

            FamilyMemberEntity memToRemove = null;
            foreach (var member in FamilyMembers)
            {
                if (member.FirstName == "Gaya")
                {
                    memToRemove = member;
                    break;
                }
            }
            FamilyMembers.Remove(memToRemove);
            SaveChanges();

            ContactInfoEntity ciToRemove = null;
            foreach (var ci in ContactInfoes)
            {
                if (ci.MemberName == "Gaya Matsliah")
                {
                    ciToRemove = ci;
                    break;
                }
            }
            ContactInfoes.Remove(ciToRemove);
            SaveChanges();

            var permsToRemove = new List<MemberPermissions>();
            foreach (var perm in Permissions)
            {
                if (perm.Id != 26 && perm.Id != 27) // check before every run 
                {
                    permsToRemove.Add(perm);
                }
            }
            Permissions.RemoveRange(permsToRemove);
            SaveChanges();
        }
    }
}