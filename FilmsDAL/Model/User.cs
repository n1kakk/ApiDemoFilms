using System.ComponentModel.DataAnnotations;

namespace ApiDemoFilms.Model
{
    public class User
    {
        [Key]
        public  int Id { get; set; }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Birthday { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
