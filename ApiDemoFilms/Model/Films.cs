namespace ApiDemoFilms.Model
{
    public class Films
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public int ReleaseYear { get; set; }
        public ICollection<Directors> DirectorId { get; set; }
        public ICollection<Genres> GenreId{ get; set; }
        public ICollection<FilmActors> FilmActors { get; set; }

    }

}
