using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ProjectName.Dal.Core;
using ProjectName.Dal.Core.Utils;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.DataService.WebApi
{
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileStorage _fileStorage;

        private readonly IModelQuery<FileInfoRecord> _query;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IContentTypeProvider _contentTypeProvider;

        public FileController(
            IFileStorage fileStorage, 
            IModelQuery<FileInfoRecord> query,
            IUnitOfWork unitOfWork,
            IContentTypeProvider contentTypeProvider)
        {
            _fileStorage = fileStorage;
            _query = query;
            _unitOfWork = unitOfWork;
            _contentTypeProvider = contentTypeProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploadedFileUuid = await _fileStorage.UploadAsync(new FormFileProxy(file));

            var fileEntity = new FileInfoRecord
            {
                FileName = file.FileName,
                Size = file.Length,
                Uuid = uploadedFileUuid,
            };

            _unitOfWork.Add(fileEntity);
            await _unitOfWork.SaveAsync();

            return Ok(uploadedFileUuid);
        }

        [HttpGet("{uuid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(Guid uuid, bool shouldOpen)
        {
            var fileInfo = _query.AsQueryable().FirstOrDefault(p => p.Uuid == uuid);

            if (fileInfo == null)
            {
                return NotFound();
            }

            if (!(User.Identity.IsAuthenticated && fileInfo.IsShared))
            {
                return Unauthorized();
            }

            if(!_contentTypeProvider.TryGetContentType(fileInfo.FileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var file = await _fileStorage.DownloadAsync(uuid);
            return File(file.OpenRead(), contentType, fileInfo.FileName);
        }
    }
}