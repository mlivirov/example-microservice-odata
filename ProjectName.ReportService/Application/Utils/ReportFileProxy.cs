using System.IO;
using ProjectName.Dal.Core;

namespace ProjectName.ReportService.Application.Utils
{
    public class ReportFileProxy : IFileProxy
    {
        public string FileName { get; }
        public long Size { get; }
        public Stream OpenRead()
        {
            throw new System.NotImplementedException();
        }
    }
}