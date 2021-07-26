using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Valentine.Api.Interfaces;
using Valentine.Application.Interfaces;
using Valentine.Domain;
using Valentine.Shared.Contracts.Requests;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileCloudUploader _uploader;
        private readonly IFilesRepository _filesRepository;
        private readonly IConfiguration _configuration;

        public FilesController(IFileCloudUploader uploader, IFilesRepository filesRepository, IConfiguration configuration)
        {
            _uploader = uploader;
            _filesRepository = filesRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"{_configuration["Settings:General:InitializationMessageApiSuccess"]}");
        }

        [HttpPost]
        public async Task<IActionResult> SaveFiles([FromForm]StoryUploadRequest request)
        {
            if (!request.Files.Any()) 
                return NoContent();

            var images = new List<Image>();
            
            foreach (var file in request.Files)
            {
                var url = await _uploader.Upload(file);
                images.Add(new Image(Guid.NewGuid(), url, Guid.Parse(request.AreaId)));
            }

            await _filesRepository.AddFiles(images);
            return Ok();
        }
    }
}
