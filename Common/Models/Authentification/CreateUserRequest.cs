using AutoMapper;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Common.Models.Authentification
{
    public class CreateUserRequest : IMapFrom<User>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserRequest, User>().ReverseMap();
        }
    }
}