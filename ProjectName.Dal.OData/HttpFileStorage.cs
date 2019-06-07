using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProjectName.Dal.Core;
using ProjectName.Dal.Core.Utils;

namespace ProjectName.Dal.OData
{
    public class HttpFileStorage : IFileStorage
    {
        private readonly HttpClient _httpClient;
        private readonly HttpFileStorageOptions _options;

        public HttpFileStorage(HttpClient httpClient, HttpFileStorageOptions options)
        {
            _httpClient = httpClient;
            _options = options;
        }
        
        public async Task<Guid> UploadAsync(IFileProxy file)
        {
            using (var fileStream = file.OpenRead())
            {
                using (fileStream)
                {
                    var result = await _httpClient.PostAsync(_options.EndpointUrl, new StreamContent(fileStream)
                    {
                        Headers =
                        {
                            ContentDisposition =
                            {
                                FileName = file.FileName,
                                Name = nameof(file),
                                Size = file.Size,
                            }
                        }
                    });

                    result.EnsureSuccessStatusCode();

                    var resultContent = await result.Content.ReadAsStringAsync();
                    return Guid.Parse(resultContent);
                }
            }
        }

        public async Task<IFileProxy> DownloadAsync(Guid uuid)
        {
            var resourceUri = new Uri(_options.EndpointUrl, uuid.ToString());
            using (var response = await _httpClient.GetAsync(resourceUri))
            {
                response.EnsureSuccessStatusCode();

                var fileName = Path.GetTempFileName();
                using (var tempFile = File.Create(fileName))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        stream.CopyTo(tempFile);
                    }
                }
                
                return new SystemFileProxy(
                    new FileInfo(fileName), 
                    response.Content.Headers.ContentDisposition.FileName);
            }
        }

        public bool CheckExists(Guid uuid)
        {
            throw new NotImplementedException();
        }
    }
}