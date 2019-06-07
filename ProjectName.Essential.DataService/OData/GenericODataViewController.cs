using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Dal.Core;
using ProjectName.Essential.Dal.Ef.Extensions;

namespace ProjectName.Essential.DataService.OData
{
    [GenericODataControllerNameConvention]
    public class GenericODataViewController<T> : ODataController
        where T : class, IView
    {
        private readonly IModelQuery<T> _query;

        private readonly string _keyPropertyName;

        public GenericODataViewController(IModelQueryBuilder modelQueryBuilder)
        {
            _query = modelQueryBuilder.Build<T>();
            _keyPropertyName = DatabaseHelper.GetKeyProperty<T>().Name;
        }
        
        [EnableQuery]
        public IQueryable<T> Get()
        {
            return _query.AsQueryable();
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var filterExpression = CreateGetByKeyLambdaExpression(key);
            var result = await _query.FirstOrDefaultAsync(filterExpression);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        private Expression<Func<T, bool>> CreateGetByKeyLambdaExpression(int key)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(T), "p");
            var nameProperty = Expression.Property(argParam, _keyPropertyName);
            var val = Expression.Constant(key);
            var equalExpression = Expression.Equal(nameProperty, val);

            return Expression.Lambda<Func<T, bool>>(equalExpression, argParam);
        }
    }
}