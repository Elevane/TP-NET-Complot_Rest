using TP_Complot_Rest.Common.Models.Persistence;
using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Persistence.Entitites
{
    public class Complot : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime created { get; set; }
        public List<Genre>? Genres { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
        public string Description { get; set; }

        public bool Public { get; set; }

        public int? Rate { get; set; }

        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}