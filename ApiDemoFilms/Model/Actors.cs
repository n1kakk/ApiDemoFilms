namespace ApiDemoFilms.Model
{
    public class Actors
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Birthday { get; set; }
        public ICollection<FilmActors> FilmActors { get; set; }
    }
}
