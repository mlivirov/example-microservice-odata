using System.Linq;
using ProjectName.Dal.Core;
using Simple.OData.Client;

namespace ProjectName.Dal.OData
{
    public class ModelQueryBuilder : IModelQueryBuilder
    {
        private readonly IODataClient _dataClient;

        public ModelQueryBuilder(IODataClient dataClient)
        {
            _dataClient = dataClient;
        }

        public IModelQuery<T> Build<T>() where T : class
        {
            return new ModelQuery<T>(_dataClient.For<T>());
        }
    }
}