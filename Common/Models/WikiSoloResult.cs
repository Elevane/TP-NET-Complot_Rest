using AutoMapper;
using Newtonsoft.Json;
using TP_Complot_Rest.Common.Mapping;

namespace TP_Complot_Rest.Common.Models
{
    public class WikiSoloResult
    {
        [JsonProperty("batchcomplete")]
        public bool Batchcomplete { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
    }

    public class Query
    {
        [JsonProperty("pages")]
        public Page[] Pages { get; set; }
    }

    public class Page
    {
        [JsonProperty("pageid")]
        public long Pageid { get; set; }

        [JsonProperty("ns")]
        public long Ns { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("extract")]
        public string Extract { get; set; }
    }

    public class PageDto : IMapFrom<Page>
    {
        public long Pageid { get; set; }

        public string Title { get; set; }
        public string Extract { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PageDto, Page>().ReverseMap();
        }
    }
}