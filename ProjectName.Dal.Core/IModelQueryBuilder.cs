using System.Threading.Tasks;

namespace ProjectName.Dal.Core
{
    public interface IModelQueryBuilder
    {
        IModelQuery<T> Build<T>() where T : class;
    }
}