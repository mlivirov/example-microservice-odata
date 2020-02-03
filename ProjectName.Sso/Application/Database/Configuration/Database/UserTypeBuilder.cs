using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Sso.Application.Database.Configuration.Database
{
    public class UserTypeBuilder : IEntityTypeBuilder<User>
    {
        public void Build(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasIndex(t => t.Guid).IsUnique();
            modelBuilder.HasIndex(t => t.Username).IsUnique();
        }
    }
}