using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectName.Dal.Core
{
    public interface IModelQuery<out T>
    {
        IQueryable<T> AsQueryable();
    }
}