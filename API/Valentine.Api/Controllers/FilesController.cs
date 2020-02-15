using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;
using Valentine.Api.Interfaces;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileCloudUploader _uploader;

        public FilesController(IFileProcessor fileProcessor, IFileCloudUploader uploader)
        {
            _fileProcessor = fileProcessor;
            _uploader = uploader;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(42);
        }


        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm]FilesUploadRequest request)
        {
            for (int i = 0; i < request.Files?.Length; i++)
            {
                var file = request.Files[i];
                var fileId = request.Ids[i];
                var bytes = _fileProcessor.ConvertFileToByteArray(file);
                var url = await _uploader.Upload(bytes, file.FileName, file.ContentType);
            }

            return NoContent();
        }
    }
}
