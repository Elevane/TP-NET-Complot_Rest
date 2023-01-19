using AutoMapper;
using System.Numerics;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Common.Models.Persistence
{
    public class ComplotGenreDto : IMapFrom<ComplotGenre>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ComplotGenre, ComplotGenreDto>().ForMember(dest => dest.Name,
                    opt => opt.MapFrom(from => from.Complot.Name)
                );
        }
    }
}