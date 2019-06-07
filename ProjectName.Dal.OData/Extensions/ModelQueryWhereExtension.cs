using System;
using System.Linq.Expressions;
using ProjectName.Dal.Core;

namespace ProjectName.Dal.OData.Extensions
{
    public static class ModelQueryWhereExtension
    {
        public static IModelQuery<T> Where<T>(this IModelQuery<T> query, Expression<Func<T, bool>> expression) 
            where T : class
        {
            var newClient = ((ModelQuery<T>) query).Client.Filter(expression);

            return new ModelQuery<T>(newClient);
        }
    }
}