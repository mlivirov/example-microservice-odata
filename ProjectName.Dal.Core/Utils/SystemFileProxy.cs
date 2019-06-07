using System.IO;

namespace ProjectName.Dal.Core.Utils
{
    public class SystemFileProxy : IFileProxy
    {
        private readonly FileInfo _fileInfo;
        
        public string FileName { get; }

        public long Size => _fileInfo.Length;

        public SystemFileProxy(FileInfo fileInfo, string fileName = null)
        {
            FileName = string.IsNullOrWhiteSpace(fileName) ? _fileInfo.Name : fileName;
            _fileInfo = fileInfo;
        }
        
        public Stream OpenRead()
        {
            return File.Open(_fileInfo.FullName, FileMode.Open, FileAccess.Read);
        }
    }
}