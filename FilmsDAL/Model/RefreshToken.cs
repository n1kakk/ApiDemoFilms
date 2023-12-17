using StackExchange.Redis;

namespace Films.DAL.Model
{
    public class RefreshToken
    {
        public string NickName { get; set; }
        public bool isUsed { get; set; }
        public DateTime Expiration { get; set; }
    }
}
