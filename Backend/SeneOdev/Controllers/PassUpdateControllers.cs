using Microsoft.Data.SqlClient;
using System.Data;
using SeneOdev.Sql;

namespace SeneOdev
{
    public class PassUpdateControllers
    {
        public string NewPass { get; set; }
        public string NewPassRepeat { get; set; }
        public string Username { get; set; }

        // Connection string artık DefaultConnection üzerinden alınacak
        private readonly string connstring = new DefaultConnection(
            "Server=Emree;Database=GYM-PRO;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;"
        ).ConnectionString;

        public (bool success, string message) Update()
        {
            try
            {
                if (NewPass != NewPassRepeat)
                    return (false, "Şifreler eşleşmiyor.");

                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                string islem = "UPDATE Users SET [PasswordHash] = @Password, Salt = @Salt WHERE Username = @Username";
                string salt = hash.GenereateSalt();
                string hashedPassword = hash.Hash(NewPass, salt);

                using var cmd = new SqlCommand(islem, baglanti);
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 256).Value = hashedPassword;
                cmd.Parameters.Add("@Salt", SqlDbType.NVarChar, 128).Value = salt;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = Username;

                bool updated = cmd.ExecuteNonQuery() > 0;
                return updated
                    ? (true, "Şifre başarıyla güncellendi.")
                    : (false, "Kullanıcı bulunamadı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("KRİTİK SQL HATASI: " + ex.Message);
                return (false, "Sunucu hatası oluştu.");
            }
        }
    }
}
