using System.Security.Cryptography;
using System.Text;

namespace SeneOdev
{
    public class hash
    {
        public static string Hash(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Password ve Salt'ı birleştirip tek seferde güçlü hash alıyoruz
                string combined = password + salt;
                byte[] bytes = Encoding.UTF8.GetBytes(combined);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string GenereateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}