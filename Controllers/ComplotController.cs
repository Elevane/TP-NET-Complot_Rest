using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TP_Complot_Rest.Common.Models;
using TP_Complot_Rest.Common.Models.Persistence;
using TP_Complot_Rest.Managers.Interfaces;

namespace TP_Complot_Rest.Controllers
{
    public class ComplotController : BaseApiController
    {
        private readonly IComplotManager _manager;
        private readonly IPersistenceManager _persistenceManager;

        public ComplotController(IComplotManager manager, IPersistenceManager persistenceManager)
        {
            _persistenceManager= persistenceManager;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComplotDto dto)
        {
            Result res = await _persistenceManager.Create(dto);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            Result<List<ComplotDto>> res = await _persistenceManager.FindAll();
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }
    }
}