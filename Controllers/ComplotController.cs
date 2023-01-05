using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TP_Complot_Rest.Common.Models;
using TP_Complot_Rest.Managers.Interfaces;

namespace TP_Complot_Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplotController : ControllerBase
    {
        private readonly IComplotManager _manager;

        public ComplotController(IComplotManager manager)
        {
            _manager = manager;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int limit = 20)
        {
            Result<List<string>> res = await _manager.Search(query);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }

        [HttpGet("searchOne")]
        public async Task<IActionResult> SearchOne([FromQuery] string title)
        {
            Result<PageDto> res = await _manager.Get(title);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }
    }
}