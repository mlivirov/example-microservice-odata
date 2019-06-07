using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjectName.Dal.Core;

namespace ProjectName.Dal.OData.Extensions
{
    public static class ModelQueryFirstExtension
    {
        public static Task<T> FirstOrDefaultAsync<T>(this IModelQuery<T> query, Expression<Func<T, bool>> expression) 
            where T : class
        {
            return ((ModelQuery<T>) query).Client.Filter(expression).FindEntryAsync();
        }
    }
}