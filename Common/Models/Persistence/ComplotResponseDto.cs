using AutoMapper;
using System.Runtime.Serialization;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Common.Models.Persistence
{
    public class ComplotResponseDto : IMapFrom<Complot>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ComplotGenreResponseDto>? Genres { get; set; }

        public string Description { get; set; }

        public bool Public { get; set; }

        public int? Rate { get; set; }

        public double Lattitude { get; set; }
        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComplotResponseDto, Complot>().ReverseMap();
        }
    }
}