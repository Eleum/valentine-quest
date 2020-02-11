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
    public class ImagesController : ControllerBase
    {
        private readonly IFileProcessor _fileProcessor;
        private readonly IFileCloudUploader _uploader;

        public ImagesController(IFileProcessor fileProcessor, IFileCloudUploader uploader)
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
        public async Task<IActionResult> SaveImages([FromForm]ImagesUploadRequest request)
        {
            foreach (var image in request.Images)
            {
                var bytes = _fileProcessor.ConvertImageToByteArray(image);
                var url = await _uploader.Upload(bytes, image.FileName, image.ContentType);
            }

            return NoContent();
        }
    }
}
