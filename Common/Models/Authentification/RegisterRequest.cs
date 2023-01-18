using AutoMapper;
using TP_Complot_Rest.Common.Mapping;
using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Common.Models.Authentification
{
    public class RegisterRequest : IMapFrom<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, RegisterRequest>().ReverseMap();
        }
    }
}