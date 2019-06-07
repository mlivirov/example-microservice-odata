using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectName.Dal.Core;

namespace ProjectName.Essential.Dal.Ef.Extensions
{
    public static class ModelQueryFindExtension
    {
        public static async Task<T> FirstOrDefaultAsync<T>(this IModelQuery<T> query, Expression<Func<T, bool>> expression)
        {
            return await ((ModelQuery<T>) query).Queryable.FirstOrDefaultAsync(expression);
        }
    }
}