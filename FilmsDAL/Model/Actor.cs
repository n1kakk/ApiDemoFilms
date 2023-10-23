using System.ComponentModel.DataAnnotations;

namespace ApiDemoFilms.Model

{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Birthday { get; set; }
        //public ICollection<FilmActors> FilmActors { get; set; }
    }
}
