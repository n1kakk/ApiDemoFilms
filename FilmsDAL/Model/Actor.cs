namespace ApiDemoFilms.Model
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Birthday { get; set; }
        public ICollection<FilmActors> FilmActors { get; set; }
    }
}
