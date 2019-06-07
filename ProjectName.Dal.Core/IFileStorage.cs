using System;
using System.Threading.Tasks;

namespace ProjectName.Dal.Core
{
    public interface IFileStorage
    {
        Task<Guid> UploadAsync(IFileProxy file);

        Task<IFileProxy> DownloadAsync(Guid uuid);

        bool CheckExists(Guid uuid);
    }
}