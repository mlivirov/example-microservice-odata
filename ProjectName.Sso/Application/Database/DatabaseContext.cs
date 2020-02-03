using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;
using ProjectName.Sso.Application.Database.Configuration;
using ProjectName.Sso.Application.Database.Configuration.Database;

namespace ProjectName.Sso.Application.Database
{
    public class DatabaseContext : DbContext  
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserExternalProvider> UserExternalProviders { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<AccessRole> AccessRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: use reflection or DI
            new UserTypeBuilder().Build(modelBuilder.Entity<User>());
            new AccessRoleTypeBuilder().Build(modelBuilder.Entity<AccessRole>());
        }
    }
}