namespace ApiDemoFilms.Model
{
    public class FilmActors
    {
        //public int Id { get; set; }
        public int ActorId { get; set; }
        public int FilmId { get; set; }
        public Films Film { get; set; }
        public Actors Actor { get; set; }
    }
}
