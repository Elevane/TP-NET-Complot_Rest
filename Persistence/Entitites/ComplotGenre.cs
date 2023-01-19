namespace TP_Complot_Rest.Persistence.Entitites
{
    public class ComplotGenre
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public Complot Complot { get; set; }
        public int ComplotId { get; set; }
    }
}