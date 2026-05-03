using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using SeneOdev.Sql;

namespace SeneOdev
{
    public class KayitOl
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("passwordRepeat")]
        public string PasswordRepeat { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("sozlesme")]
        public bool Sozlesme { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        // Connection string artık DefaultConnection üzerinden alınacak
        private readonly string connstring = new DefaultConnection(
            "Server=Emree;Database=GYM-PRO;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;"
        ).ConnectionString;

        public bool Kayit()
        {
            try
            {
                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                if (Password != PasswordRepeat) return false;
                if (!Sozlesme) return false;

                // Kullanıcı adı veya email daha önce var mı kontrol et
                string kontrol = "SELECT COUNT(*) FROM Users WHERE Username=@U OR Email=@E";
                using var cmdKontrol = new SqlCommand(kontrol, baglanti);
                cmdKontrol.Parameters.AddWithValue("@U", Username);
                cmdKontrol.Parameters.AddWithValue("@E", Email);
                if ((int)cmdKontrol.ExecuteScalar() > 0) return false;

                // Salt ve hash üret
                string salt = hash.GenereateSalt();
                string passwordHash = hash.Hash(Password, salt);

                // Yeni kullanıcı ekle
                string query = @"INSERT INTO Users 
                    (Name, Surname, Username, Email, Phone, Gender, PasswordHash, Salt) 
                    VALUES (@Name, @Surname, @Username, @Email, @Phone, @Gender, @Hash, @Salt)";

                using var cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = Name;
                cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 50).Value = Surname;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Username;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = Email;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)Phone ?? DBNull.Value;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = (object)Gender ?? DBNull.Value;
                cmd.Parameters.Add("@Hash", SqlDbType.NVarChar, 256).Value = passwordHash;
                cmd.Parameters.Add("@Salt", SqlDbType.NVarChar, 128).Value = salt;

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("KRİTİK SQL HATASI: " + ex.Message);
                return false;
            }
        }
    }
}
