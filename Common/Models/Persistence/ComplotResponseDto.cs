using System.Runtime.Serialization;

namespace TP_Complot_Rest.Common.Models.Persistence
{
    public class ComplotResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<GenreDto> Genres { get; set; }

        public string Description { get; set; }

        public bool Public { get; set; }

        public int Rate { get; set; }

        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}