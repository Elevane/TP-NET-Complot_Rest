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

        public async Task<Result> Create(ComplotDto toCreate, int UserId)
        {
            toCreate.UserId = UserId;
            Complot exist = _mapper.Map<Complot>(toCreate);
            await _context.AddAsync(exist);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<List<ComplotResponseDto>>> FindAll()
        {
            List<Complot> c = await _context.Complots.ToListAsync();
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