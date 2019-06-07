using System.IO;

namespace ProjectName.Dal.Core
{
    public interface IFileProxy
    {
        string FileName { get; }
        
        long Size { get; }

        Stream OpenRead();
    }
}