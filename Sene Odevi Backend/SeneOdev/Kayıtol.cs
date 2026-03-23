using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SeneOdev
{
    public class KayitOl
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }

        private readonly string connstring =
            "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        public bool Kayit()
        {
            try
            {
                using var baglanti = new SqlConnection(connstring);
                baglanti.Open();

                // 🔴 Şifre kontrolü
                if (Password != PasswordRepeat)
                    return false;

                // 🔴 Username var mı kontrol
                string kontrol = "SELECT COUNT(*) FROM Kullanici WHERE Username=@Username";
                using var comnd = new SqlCommand(kontrol, baglanti);
                comnd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Username;
                int varmi = (int)comnd.ExecuteScalar();
                if (varmi > 0)
                    return false;

                // 🔴 INSERT
                string query = @"INSERT INTO Kullanici
        (Ad,Soyad,Username,Email,Phone,Gender,[PasswordHash])
        VALUES (@Ad,@Soyad,@Username,@Email,@Phone,@Gender,@PasswordHash)";

                using var cmd = new SqlCommand(query, baglanti);
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar, 50).Value = Name;
                cmd.Parameters.Add("@Soyad", SqlDbType.NVarChar, 50).Value = Surname;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Username;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = Email;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = Gender;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = Password; // dilersen burada hashle

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
        }
    }
}