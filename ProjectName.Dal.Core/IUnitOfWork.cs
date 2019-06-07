using System.Threading.Tasks;

namespace ProjectName.Dal.Core
{
    public interface IUnitOfWork
    {
        void Add(IEntity entity);

        void Remove(IEntity entity);

        void Update(IEntity entity);

        Task SaveAsync();
    }
}