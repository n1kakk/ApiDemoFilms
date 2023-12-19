using System.Security.Cryptography;
using System.Text;

namespace Films.DAL.Helpers
{
    public class PasswordHasher

    {
        public static string Salt()
        {
            byte[] saltBytes = new byte[16];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);
            return salt;
        }
       
        public static string  HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] passwordbytes = Encoding.UTF8.GetBytes(password);
                byte[] hashbytes = sha256Hash.ComputeHash(passwordbytes);

                // преобразуем байты в строку hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashbytes.Length; i++)
                {
                    builder.Append(hashbytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string salt)
        {
            string passwordHash = HashPassword(password + salt);
            return passwordHash == storedHash;
        }
    }
}
