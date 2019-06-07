using System.Threading.Tasks;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Ef
{
    // make it generic
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public void Add(IEntity entity)
        {
            _databaseContext.Add(entity);
        }

        public void Remove(IEntity entity)
        {
            _databaseContext.Remove(entity);
        }

        public void Update(IEntity entity)
        {
            // EF tracks changes automatically
        }

        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}