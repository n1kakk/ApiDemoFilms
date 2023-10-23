using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDemoFilms.Model
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string filmName { get; set; }
        public float? Rating { get; set; }
        public int ReleaseYear { get; set; }
        //[ForeignKey("DirectorId")]
        public Director Director { get; set; }
        //[ForeignKey("GenreId")]
        public Genre Genre{ get; set; }
        //public FilmActors FilmActors { get; set; }

    }

}
