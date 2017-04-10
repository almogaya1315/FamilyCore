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
        }
        public FamilyEntity GetFamily(int id)
        {
            foreach (FamilyEntity family in Families)
            {
                if (family.Id == id) return family;
            }
            return null;
        }

        public FamilyMemberEntity GetFamilyMember(int id)
        {
            foreach (FamilyMemberEntity member in FamilyMembers)
            {
                if (member.Id == id) return member;
            }
            return null;
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


        public ContactInfoEntity GetContactInfo(int id)
        {
            foreach (ContactInfoEntity contact in ContactInfoes)
            {
                if (contact.Id == id) return contact;
            }
            return null;
        }

        public ContactBookEntity GetContactBook(int id)
        {
            return ContactBooks.FirstOrDefault(b => b.Id == id);

            //foreach (ContactBookEntity book in ContactBooks)
            //{
            //    if (book.Id == id) return book;
            //}
            //return null;
        }
    }
}