using CSharpFunctionalExtensions;
using TP_Complot_Rest.Common.Models;
using TP_Complot_Rest.Common.Models.Persistence;

namespace TP_Complot_Rest.Managers.Interfaces
{
    public interface IPersistenceManager
    {
        public Task<Result> Create(ComplotDto toCreate);

        public Task<Result> Udpate(ComplotDto toUpdate);

        public Task<Result<List<ComplotDto>>> FindAll();
    }
}