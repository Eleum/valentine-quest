using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Valentine.Api.Contracts.Requests;
using Valentine.Application.Interfaces;

namespace Valentine.Api.Controllers
{
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreasRepository _areasRepository;

        public AreasController(IAreasRepository areasRepository)
        {
            _areasRepository = areasRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAreas([FromBody]AreasSaveRequest request)
        {
            await _areasRepository.SaveAreasAsync(Guid.Parse("851c2973-bc9f-4078-9919-d5f44b35df61"), request.Ids);
            return Ok();
        }
    }
}
