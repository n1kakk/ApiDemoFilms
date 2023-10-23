using System.ComponentModel.DataAnnotations;

namespace ApiDemoFilms.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}
