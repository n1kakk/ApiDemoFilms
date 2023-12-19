using ApiDemoFilms.Model;

namespace Films.DAL.InterfacesServices
{
    public interface IDirectorService
    {
        public Task<Director> GetIdDirectorsAsync(int id);
        public Task<ICollection<Director>> GetDirectorsAsync();
    }
}
