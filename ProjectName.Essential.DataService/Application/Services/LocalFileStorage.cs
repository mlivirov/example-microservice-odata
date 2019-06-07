using System;
using System.IO;
using System.Threading.Tasks;
using ProjectName.Dal.Core;
using ProjectName.Dal.Core.Utils;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.DataService.Application.Services
{
    public class LocalFileStorage : IFileStorage
    {
        public async Task<Guid> UploadAsync(IFileProxy file)
        {
            var uuid = Guid.NewGuid();

            using (var inputStream = file.OpenRead())
            {
                var storedFilePath = GetFilePath(uuid);
                using (var outputStream = File.Create(storedFilePath))
                {
                    inputStream.CopyTo(outputStream);
                }
            }

            return uuid;
        }

        public Task<IFileProxy> DownloadAsync(Guid uuid)
        {
            var storedFilePath = GetFilePath(uuid);
            return Task.FromResult<IFileProxy>(new SystemFileProxy(new FileInfo(storedFilePath)));
        }

        public bool CheckExists(Guid uuid)
        {
            var storedFilePath = GetFilePath(uuid);
            return File.Exists(storedFilePath);
        }

        private string GetFilePath(Guid uuid)
        {
            return Path.Combine(Path.GetTempPath(), uuid.ToString());
        }
    }
}