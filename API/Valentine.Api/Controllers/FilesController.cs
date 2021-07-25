using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valentine.Shared.Contracts.Requests;
using Valentine.Api.Interfaces;
using Valentine.Application.Interfaces;
using Valentine.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileProcessor _processor;
        //private readonly IFileCloudUploader _uploader;
        private readonly IFilesRepository _repository;
        private readonly IConfiguration _configuration;

        public FilesController(IFileProcessor processor, IFilesRepository repository, IConfiguration configuration)
        {
            _processor = processor;
            //_uploader = uploader;
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"{_configuration["Settings:General:InitializationMessageApiSuccess"]}");
        }

        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm]FilesUploadRequest request)
        {
            if (request.Files.Length == 0)
                return NoContent();

            var images = new List<Image>();

            for (int i = 0; i < request.Files?.Length; i++)
            {
                var file = (IFormFile)request.Files[i];
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
