using AutoMapper;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Common.Models.Authentification
{
    public class AuthenticateResponse : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Img { get; set; }
        public string Token { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthenticateResponse, User>().ReverseMap();
        }
    }
}