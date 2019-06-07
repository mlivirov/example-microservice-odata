using System.Threading.Tasks;
using ProjectName.Dal.Core;
using Simple.OData.Client;

namespace ProjectName.Dal.OData
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IODataClient _dataClient;

        private ODataBatch _batch;

        public UnitOfWork(IODataClient dataClient)
        {
            _dataClient = dataClient;
            _batch = new ODataBatch(_dataClient);
        }
        
        public void Add(IEntity entity)
        {
            _batch += command => command.For(entity.GetType().Name).Set(entity).InsertEntryAsync();
        }

        public void Remove(IEntity entity)
        {
            _batch += command => command.For(entity.GetType().Name).Key(entity.Id).DeleteEntryAsync();
        }

        public void Update(IEntity entity)
        {
            _batch += command => command.For(entity.GetType().Name)
                .Key(entity.Id)
                .Set(entity)
                .UpdateEntryAsync();
        }

        public async Task SaveAsync()
        {
            await _batch.ExecuteAsync();
            _batch = new ODataBatch(_dataClient);
        }
    }
}