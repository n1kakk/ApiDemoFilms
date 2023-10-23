using System.ComponentModel.DataAnnotations;

namespace ApiDemoFilms.Model
{
    public class FilmActors
    {
        [Key]
        public int Id { get; set; }
        public int ActorId { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public Actor Actor { get; set; }
    }
}
