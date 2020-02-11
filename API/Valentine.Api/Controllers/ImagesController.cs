using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;

namespace Valentine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(42);
        }


        [HttpPost]
        public async Task<IActionResult> SaveImages([FromForm]ImagesUploadRequest request)
        {
            return NoContent();
        }
    }
}
