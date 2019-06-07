using System;
using System.Threading.Tasks;
using ProjectName.Dal.Core;
using ProjectName.Dal.OData.Extensions;
using ProjectName.Essential.Dal.Core.Models;
using ProjectName.ReportService.Application.Utils;

namespace ProjectName.ReportService.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IFileStorage _fileStorage;
        private readonly IModelQueryBuilder _queryBuilder;
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(
            IFileStorage fileStorage, 
            IModelQueryBuilder queryBuilder, 
            IUnitOfWork unitOfWork)
        {
            _fileStorage = fileStorage;
            _queryBuilder = queryBuilder;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Report> GenerateReportAsync(string name)
        {
            var query = _queryBuilder.Build<ReportTemplate>();

            var template = await query.FirstOrDefaultAsync(p => p.Name == name);
            var templateFile = await _fileStorage.DownloadAsync(template.FileInfoRecord.Uuid);
            
            var reportFile = await GenerateReportFileAsync(templateFile);
            var reportFileUuid = await _fileStorage.UploadAsync(reportFile);
            
            var file = _queryBuilder.Build<FileInfoRecord>().FirstOrDefaultAsync(p => p.Uuid == reportFileUuid);
            var report = new Report
            {
                CreatedAt = DateTime.Now,
                ReportTemplateId = template.Id,
                FileInfoRecordId = file.Id,
            };

            _unitOfWork.Add(report);
            await _unitOfWork.SaveAsync();

            return report;
        }

        private Task<IFileProxy> GenerateReportFileAsync(IFileProxy templateFile)
        {
            return Task.FromResult<IFileProxy>(new ReportFileProxy());
        }
    }
}