using AutoMapper;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Common.Models.Persistence
{
    public class ComplotGenreResponseDto : IMapFrom<ComplotGenre>
    {
        public string Name { get; set; }
        public int GenreId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComplotGenre, ComplotGenreResponseDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Genre.Id)).ReverseMap();
        }
    }
}