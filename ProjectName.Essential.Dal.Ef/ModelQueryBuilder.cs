using System.Linq;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Ef
{
    // make it generic
    public class ModelQueryBuilder : IModelQueryBuilder
    {
        private readonly DatabaseContext _context;

        public ModelQueryBuilder(DatabaseContext context)
        {
            _context = context;
        }

        public IModelQuery<T> Build<T>() where T : class
        {
            if (typeof(T).GetInterfaces().Contains(typeof(IView)))
            {
                return new ModelQuery<T>(_context.Query<T>());
            }

            return new ModelQuery<T>(_context.Set<T>());
        }
    }
}