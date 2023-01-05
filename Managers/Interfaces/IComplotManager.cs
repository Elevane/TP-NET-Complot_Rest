using CSharpFunctionalExtensions;
using TP_Complot_Rest.Common.Models;

namespace TP_Complot_Rest.Managers.Interfaces
{
    public interface IComplotManager
    {
        public Task<Result<PageDto>> Get(string title);

        public Task<Result<List<string>>> Search(string search, int limit = 20);
    }
}