using System;
using System.Linq.Expressions;
using ProjectName.Dal.Core;

namespace ProjectName.Dal.OData.Extensions
{
    public static class ModelQueryTakeSkipExtension
    {
        public static IModelQuery<T> Take<T>(this IModelQuery<T> query, int count) 
            where T : class
        {
            var newClient = ((ModelQuery<T>) query).Client.Top(count);

            return new ModelQuery<T>(newClient);
        }

        public static IModelQuery<T> Skip<T>(this IModelQuery<T> query, int count) 
            where T : class
        {
            var newClient = ((ModelQuery<T>) query).Client.Skip(count);

            return new ModelQuery<T>(newClient);
        }
    }
}