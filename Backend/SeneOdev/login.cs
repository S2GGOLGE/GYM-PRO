using Microsoft.Data.SqlClient;
using System.Data;

namespace SeneOdev
{
    public class Login
    {
        public static string GirisYap(string username, string password)
        {
            string sonuc = BosAlanKontrolu(username, password);
            if (sonuc != "OK")
                return sonuc;

            string connstring = "Data Source=Emree;Initial Catalog=GYM-PRO;Integrated Security=True;Multiple Active Result Sets=True;Encrypt=False";

            using var baglanti = new SqlConnection(connstring);
            baglanti.Open();

            // Kullanıcının hash ve salt'ını DB'den al
            string komut = "SELECT PasswordHash, Salt FROM Users WHERE Username = @username";

            using var islem = new SqlCommand(komut, baglanti);
            islem.Parameters.AddWithValue("@username", username);

            using var reader = islem.ExecuteReader();
            if (!reader.Read())
                return "Kullanıcı yok";
            string dbPassword = reader["PasswordHash"].ToString();
            string dbSalt = reader["Salt"].ToString();

            // Girilen şifreyi DB'den gelen salt ile hashle
            string loginHash = hash.Hash(password, dbSalt);

            // Karşılaştır
            if (loginHash != dbPassword)
                return "Şifre yanlış";

            return "OK";
        }

        public static string BosAlanKontrolu(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "Lütfen kullanıcı adınızı girin.";

            if (string.IsNullOrWhiteSpace(password))
                return "Lütfen şifrenizi giriniz.";

            return "OK";
        }
    }
}