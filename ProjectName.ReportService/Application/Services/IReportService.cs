using System.Threading.Tasks;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.ReportService.Application.Services
{
    public interface IReportService
    {
        Task<Report> GenerateReportAsync(string name);
    }
}