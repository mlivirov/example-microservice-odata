using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectName.Sso.Application.Database.Configuration.Database
{
    public class AccessRoleTypeBuilder : IEntityTypeBuilder<AccessRole>
    {
        public void Build(EntityTypeBuilder<AccessRole> modelBuilder)
        {
            modelBuilder.HasIndex(t => t.Name).IsUnique();
        }
    }
}