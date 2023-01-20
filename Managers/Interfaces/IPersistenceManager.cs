using CSharpFunctionalExtensions;
using TP_Complot_Rest.Common.Models;
using TP_Complot_Rest.Common.Models.Persistence;

namespace TP_Complot_Rest.Managers.Interfaces
{
    public interface IPersistenceManager
    {
        public Task<Result> Create(ComplotDto toCreate, int UserId);

        public Task<Result> Udpate(ComplotDto toUpdate);

        public Task<Result<List<ComplotResponseDto>>> FindAll(int userId);
        public Task<Result<List<ComplotResponseDto>>> FindAllComplot(int userId);
        public Task<Result<List<GenreDto>>> GetGenres();
    }
}