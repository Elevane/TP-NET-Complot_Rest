using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using TP_Complot_Rest.Common;
using TP_Complot_Rest.Common.Models.Persistence;
using TP_Complot_Rest.Managers.Interfaces;

namespace TP_Complot_Rest.Managers
{
    public class SoapApiManager : IPersistenceManager
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly SoapSettings _settings;

        public SoapApiManager(IOptions<SoapSettings> settings, IMapper mapper)
        {
            _settings = settings.Value;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_settings.SoapUrl);
            _mapper = mapper;
        }

        public Task<Result> Create(ComplotDto toCreate)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<ComplotDto>>> FindAll()
        {
            var res = new List<ComplotDto>();
            return Result.Success(res);
        }

        public Task<Result> Udpate(ComplotDto toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}