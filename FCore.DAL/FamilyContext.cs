namespace FCore.DAL
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FamilyContext : DbContext
    {
        public FamilyContext()
            : base("name=FamilyContext") { }

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
    }
}