using AutoMapper;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Common.Models.Persistence
{
    public class ComplotDto : IMapFrom<Complot>
    {
        public string Name { get; set; }

        public List<ComplotGenreDto>? Genres { get; set; }

        public string Description { get; set; }
        public int UserId { get; set; }
        public bool Public { get; set; }

        public int? Rate { get; set; }

        public double Lattitude { get; set; }
        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComplotDto, Complot>().ReverseMap();
        }
    }
}