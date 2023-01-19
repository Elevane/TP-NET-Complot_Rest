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
            var res = new List<ComplotResponseDto>();
            //res.Add(new ComplotResponseDto() { Name = "nantre", Description = " nantes nord", Lattitude = 47.30, Longitude = -1.59, Public = true });
            //res.Add(new ComplotResponseDto() { Name = "nantre", Description = " nantes nord est qzdqçiazrdqzd847q9z648fdqz896 q4zd86 4q qz4dq6z 46q4z q48zd6qz4d6qz84 46qz48dqz", Lattitude = 47.34, Longitude = -1.42, Public = false, Genres = new List<GenreDto>() { new GenreDto() { Name = "Alien" } } });
            return Result.Success(res);
        }

        public Task<Result> Udpate(ComplotDto toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}