using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TP_Complot_Rest.Common;
using TP_Complot_Rest.Common.Models;
using TP_Complot_Rest.Managers.Interfaces;

namespace TP_Complot_Rest.Managers
{
    public class WikipediaManager : IComplotManager
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly SourceSettings _settings;

        public WikipediaManager(IOptions<SourceSettings> settings, IMapper mapper)
        {
            _settings = settings.Value;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_settings.SourceUrl);
            _mapper = mapper;
        }

        public async Task<Result<PageDto>> Get(string title)
        {
            if (title == null || title == string.Empty)
                return Result.Failure<PageDto>("Search format is invalid");
            //string query = $"?action=parse&page={title}&format=json&prop=text&section=0";
            string query = $"?action=query&prop=extracts&exintro=1&explaintext=1&titles={title}&continue=&format=json&formatversion=2";
            HttpResponseMessage res = await _client.GetAsync(query);
            if (!res.IsSuccessStatusCode)
                return Result.Failure<PageDto>($"Error while calling Wikipedia with given message : {res.RequestMessage}");
            string readble = await res.Content.ReadAsStringAsync();
            WikiSoloResult mapped = JsonConvert.DeserializeObject<WikiSoloResult>(readble);
            PageDto dto = _mapper.Map<PageDto>(mapped.Query.Pages.First());
            return Result.Success(dto);
        }

        public async Task<Result<List<string>>> Search(string search, int limit = 20)
        {
            List<string> list = new List<string>();
            if (search == null || search == string.Empty)
                return Result.Failure<List<string>>("Search format is invalid");
            string query = $"?action=query&origin=*&format=json&generator=search&gsrnamespace=0&gsrlimit={limit}&gsrsearch={search}";
            HttpResponseMessage res = await _client.GetAsync(query);
            if (!res.IsSuccessStatusCode)
                return Result.Failure<List<string>>($"Error while calling Wikipedia with given message : {res.RequestMessage}");
            string readble = await res.Content.ReadAsStringAsync();
            WikipediaResult mapped = JsonConvert.DeserializeObject<WikipediaResult>(readble);
            bool isNullOrEmpty = mapped == null || mapped.Query == null || mapped.Query.Pages == null || mapped.Query.Pages.Count == 0 || mapped.Query.Pages.Values.Count == 0 || mapped.Query.Pages.Values == null;
            if (!isNullOrEmpty)
                list = mapped.Query.Pages.Values.Select(v => v.Title).ToList();
            return Result.Success(list);
        }

        public class WikipediaResult
        {
            [JsonProperty("batchcomplete")]
            public string Batchcomplete { get; set; }

            [JsonProperty("continue")]
            public Continue Continue { get; set; }

            [JsonProperty("query")]
            public Query Query { get; set; }
        }

        public class Continue
        {
            [JsonProperty("gsroffset")]
            public long Gsroffset { get; set; }

            [JsonProperty("continue")]
            public string ContinueContinue { get; set; }
        }

        public class Query
        {
            [JsonProperty("pages")]
            public System.Collections.Generic.Dictionary<string, Page> Pages { get; set; }
        }

        public class Page
        {
            [JsonProperty("pageid")]
            public long Pageid { get; set; }

            [JsonProperty("ns")]
            public long Ns { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("index")]
            public long Index { get; set; }
        }
    }
}