using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;

namespace SeneOdev
{
    public class KayitOl
    {
        [JsonPropertyName("name")] // Düzenleme: JS'deki 'name' ile eşleşmesi için eklendi
        public string Name { get; set; }

        [JsonPropertyName("surname")] // Düzenleme: JS'deki 'surname' ile eşleşmesi için eklendi
        public string Surname { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("sozlesme")]
        public string Sozlesme { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("passwordRepeat")]
        public string PasswordRepeat { get; set; }

        // ... Diğer metodun (Kayit()) içeriği aynı kalabilir, 
        // ancak parametrelerin dolduğundan emin olduk.

        private readonly string connstring = "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        public bool Kayit()
        {
            try
            {
                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                if (Password != PasswordRepeat) return false;

                // Frontend'den "on" veya "true" gelebilir, ikisini de kabul et
                if (string.IsNullOrEmpty(Sozlesme) || (Sozlesme.ToLower() != "on" && Sozlesme.ToLower() != "true"))
                    return false;

                // Username & Email Kontrolü (Hız için tek sorguda birleştirilebilir ama bu haliyle de çalışır)
                string kontrol = "SELECT COUNT(*) FROM Kullanici WHERE Username=@U OR Email=@E";
                using var cmdKontrol = new SqlCommand(kontrol, baglanti);
                cmdKontrol.Parameters.AddWithValue("@U", Username);
                cmdKontrol.Parameters.AddWithValue("@E", Email);
                if ((int)cmdKontrol.ExecuteScalar() > 0) return false;

                // HASH İŞLEMİ
                string salt = hash.GenereateSalt();
                string passwordHash = hash.Hash(Password, salt);

                string query = @"INSERT INTO Kullanici (Ad, Soyad, Username, Email, Phone, Gender, PasswordHash, Salt) 
                                VALUES (@Ad, @Soyad, @Username, @Email, @Phone, @Gender, @PasswordHash, @Salt)";

                using var cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar, 50).Value = Name;
                cmd.Parameters.Add("@Soyad", SqlDbType.NVarChar, 50).Value = Surname;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Username;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = Email;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)Phone ?? DBNull.Value;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = (object)Gender ?? DBNull.Value;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = passwordHash;
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