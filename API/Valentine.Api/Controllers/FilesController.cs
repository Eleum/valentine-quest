using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;
using Valentine.Api.Interfaces;
using Valentine.Application.Interfaces;
using Valentine.Domain;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileProcessor _processor;
        //private readonly IFileCloudUploader _uploader;
        private readonly IFilesRepository _repository;

        public FilesController(IFileProcessor processor, IFilesRepository repository)
        {
            _processor = processor;
            //_uploader = uploader;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(42);
        }

        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm]FilesUploadRequest request)
        {
            if (request.Files.Length == 0)
                return NoContent();

            var images = new List<Image>();

            for (int i = 0; i < request.Files?.Length; i++)
            {
                var file = request.Files[i];
                var fileId = Guid.Parse(request.Ids[i]);
                var areaId = Guid.Parse(request.AreaId);

                var bytes = _processor.ConvertFileToByteArray(file);
                //var url = await _uploader.Upload(bytes, file.FileName, file.ContentType);

                //images.Add(new Image(fileId, url, areaId));
            }

            try
            {
                await _repository.AddFiles(images);
            }
            catch (Exception)
            {

            }
            

            return Ok();
        }
    }
}
