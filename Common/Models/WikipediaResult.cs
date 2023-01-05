namespace TP_Complot_Rest.Common.Models
{
    public class WikipediaResult
    {
        public string Search { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Values { get; set; }
        public List<string> Urls { get; set; }
    }
}