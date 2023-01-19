using TP_Complot_Rest.Persistence.Entities;

namespace TP_Complot_Rest.Persistence.Entitites
{
    public class Genre : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ComplotGenre> Complots { get; set; }
    }
}