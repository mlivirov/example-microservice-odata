using System.IO;
using Microsoft.AspNetCore.Http;

namespace ProjectName.Dal.Core.Utils
{
    public class FormFileProxy : IFileProxy
    {
        private readonly IFormFile _file;

        public string FileName => _file.FileName;
        
        public long Size => _file.Length;

        public FormFileProxy(IFormFile file)
        {
            _file = file;
        }

        public Stream OpenRead()
        {
            return _file.OpenReadStream();
        }
    }
}