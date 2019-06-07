using Microsoft.EntityFrameworkCore;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.Dal.Ef
{
    // TODO: revisit this after MS finishes views scaffolding (https://github.com/aspnet/EntityFrameworkCore/issues/1679)
    public partial class DatabaseContext
    {
        public virtual DbQuery<ContactView> ContactView { get; set; }
    }
}