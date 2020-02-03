using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.ReportService.Application.Services;
using Simple.OData.Client;

namespace ProjectName.ReportService.Controllers
{
    [Route("/api/[controller]")]
    public class UsageReportController : Controller
    {
        private readonly IReportService _reportService;

        public UsageReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var result = await _reportService.GenerateReportAsync("UsageReport");
            return Ok(result);
        }
    }
}