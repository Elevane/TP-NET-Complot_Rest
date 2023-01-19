using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TP_Complot_Rest.Common.Helpers;
using TP_Complot_Rest.Common.Models;
using TP_Complot_Rest.Common.Models.Persistence;
using TP_Complot_Rest.Managers.Interfaces;
using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Controllers
{
    public class ComplotController : BaseApiController
    {
        private readonly IComplotManager _manager;
        private readonly IPersistenceManager _persistenceManager;
        private int _currentUser;
        private IHttpContextAccessor _contextAccessor;

        public ComplotController(IComplotManager manager, IPersistenceManager persistenceManager, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            var user = _contextAccessor.HttpContext.Items["User"];
            if (user == null) throw new Exception("Could not get current User when activatin controller");
            _currentUser = int.Parse(user.GetType().GetProperty("Id").GetValue(user, null).ToString());
            if (_currentUser == null) throw new Exception("Could not get current User id when activatin controller");
            _persistenceManager = persistenceManager;
            _manager = manager;
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int limit = 20)
        {
            Result<List<string>> res = await _manager.Search(query);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }

        [Authorize]
        [HttpGet("searchOne")]
        public async Task<IActionResult> SearchOne([FromQuery] string title)
        {
            Result<PageDto> res = await _manager.Get(title);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComplotDto dto)
        {
            Result res = await _persistenceManager.Create(dto, _currentUser);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            Result<List<ComplotResponseDto>> res = await _persistenceManager.FindAll();
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }

        [Authorize]
        [HttpGet("genres")]
        public async Task<IActionResult> getAllGenres()
        {
            Result<List<GenreDto>> res = await _persistenceManager.GetGenres();
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }
    }
}