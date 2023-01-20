using AutoMapper;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Common.Models.Persistence
{
    public class GenreDto : IMapFrom<Genre>
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}