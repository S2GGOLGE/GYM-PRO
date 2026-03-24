using Microsoft.Data.SqlClient;
using System.Data;

namespace SeneOdev
{
    public class Pass_Update
    {
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }

        public string Username { get; set; }  // opsiyonel
        public string Email { get; set; }     // opsiyonel
        public string Phone { get; set; }     // opsiyonel

        private readonly string connstring =
           "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        public bool Update()
        {
            try
            {
                if (Password != PasswordRepeat)
                    return false; // Şifreler uyuşmuyor

                using var connection = new SqlConnection(connstring);
                connection.Open();

                string query = @"UPDATE Kullanici 
                                 SET [PasswordHash] = @PasswordHash 
                                 WHERE (Username = @Username OR Email = @Email OR Phone = @Phone)";

                using var cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = Password;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = (object)Username ?? DBNull.Value;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = (object)Email ?? DBNull.Value;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)Phone ?? DBNull.Value;

                return cmd.ExecuteNonQuery() > 0; // Güncelleme başarılı mı kontrol
            }
            catch (Exception ex)
            {
                Console.WriteLine("HATA: " + ex.Message);
                return false;
            }
        }
        public static string BoşKontrol(string username, string email, string Phone, string Pass, string Passtekrar)
        {

            if(string.IsNullOrWhiteSpace(Pass))
                return "Lütfen Şİfrenizi girin.";
            if (string.IsNullOrWhiteSpace(Passtekrar))
                return "Lutfen Şifrenizi Tekrar Giriniz giriniz";
            return "OK";
        }
    }
}