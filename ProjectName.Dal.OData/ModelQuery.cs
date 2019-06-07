using System;
using System.Linq;
using ProjectName.Dal.Core;
using Simple.OData.Client;

namespace ProjectName.Dal.OData
{
    public class ModelQuery<T> : IModelQuery<T> where T : class
    {
        internal readonly IBoundClient<T> Client;

        public ModelQuery(IBoundClient<T> client)
        {
            Client = client;
        }

        public IQueryable<T> AsQueryable()
        {
            throw new NotImplementedException();
        }
    }
}