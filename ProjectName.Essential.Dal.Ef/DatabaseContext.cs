using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.Dal.Ef
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<FileInfoRecord> FileInfoRecord { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonAddress> PersonAddress { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserAccountGroup> UserAccountGroup { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserGroupRole> UserGroupRole { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        // Unable to generate entity type for table 'dbo.DATABASECHANGELOG'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ProjectName;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<FileInfoRecord>(entity =>
            {
                entity.Property(e => e.Uuid).HasDefaultValueSql("('NEWID()')");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__UserAcco__C9F284564226602B")
                    .IsUnique();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.UserAccount)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_UserAccount_Person");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.HasIndex(e => e.GroupName)
                    .HasName("UQ__UserGrou__6EFCD434F0269851")
                    .IsUnique();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasIndex(e => e.RoleName)
                    .HasName("UQ__UserRole__8A2B61603D9F40C8")
                    .IsUnique();
            });
        }
    }
}
