namespace FCore.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FamilyContext : DbContext
    {
        public FamilyContext()
            : base("name=FamilyContext") { }

        public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}