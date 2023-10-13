namespace ApiDemoFilms.Model
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public int ReleaseYear { get; set; }
        public ICollection<Director> DirectorId { get; set; }
        public ICollection<Genre> GenreId{ get; set; }
        public ICollection<FilmActors> FilmActors { get; set; }

    }

}
