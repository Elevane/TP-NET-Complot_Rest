using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TP_Complot_Rest.Common;
using TP_Complot_Rest.Common.Models.Persistence;
using TP_Complot_Rest.Managers.Interfaces;
using TP_Complot_Rest.Persistence;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Managers
{
    public class SqlManager : IPersistenceManager
    {
        private readonly IMapper _mapper;
        private readonly UserContext _context;

        public SqlManager(IMapper mapper, UserContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Create(ComplotDto toCreate, int userId)
        {
            int JOUR = 24;
            var last3days = DateTime.Now.AddHours(-(24 * 7));
            List<Complot> last = await _context.Complots.Where(c => c.created > last3days && c.Public && c.UserId == userId).ToListAsync();
            if (last.Count >= 3 && toCreate.Public) return Result.Failure("Max public complor created since 3 days");
            toCreate.UserId = userId;

            Complot exist = _mapper.Map<Complot>(toCreate);
            exist.created = DateTime.Now;
            await _context.AddAsync(exist);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<List<ComplotResponseDto>>> FindAllComplot(int userId)
        {
            List<Complot> mines = await _context.Complots.Where(c => c.UserId == userId && !c.Public).Include(c => c.Genres).ToListAsync();
            List<Complot> c = await _context.Complots.Where(c => c.Public).Include(c => c.Genres).ToListAsync();
            List<Complot> merge = mines.Concat(c).ToList();
            List<ComplotResponseDto> dto = _mapper.Map<List<ComplotResponseDto>>(merge);
            return Result.Success(dto);
        }

        public async Task<Result<List<ComplotResponseDto>>> FindAll(int userId)
        {
            List<Complot> c = await _context.Complots.Where(c => c.UserId == userId).Include(c => c.Genres).ToListAsync();
            List<ComplotResponseDto> dto = _mapper.Map<List<ComplotResponseDto>>(c);
            return Result.Success(dto);
        }

        public async Task<Result<List<GenreDto>>> GetGenres()
        {
            List<Genre> entitites = await _context.Genres.ToListAsync();
            List<GenreDto> dtos = _mapper.Map<List<GenreDto>>(entitites);
            return Result.Success(dtos);
        }

        public Task<Result> Udpate(ComplotDto toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}