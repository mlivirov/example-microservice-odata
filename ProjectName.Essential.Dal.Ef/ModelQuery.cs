using System.Linq;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Ef
{
    public class ModelQuery<T> : IModelQuery<T>
    {
        internal readonly IQueryable<T> Queryable;

        public ModelQuery(IQueryable<T> queryable)
        {
            Queryable = queryable;
        }

        public IQueryable<T> AsQueryable()
        {
            return Queryable;
        }
    }
}