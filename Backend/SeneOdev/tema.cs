using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SeneOdev
{
    public class tema
    {
        public string Theme { get; set; }
        public string Token { get; set; } // Token'ı buraya set edeceksin

        private readonly string connstring =
            "Data Source=Emree;Initial Catalog=GYM-PRO;Integrated Security=True;Encrypt=False";

        private string GetUsernameFromToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(Token);
            return jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }

        public bool Kaydet()
        {
            try
            {
                string username = GetUsernameFromToken();

                using var bağlantı = new SqlConnection(connstring);
                bağlantı.Open();

                string query = @"UPDATE Users SET Theme = @Theme WHERE Username = @Username";

                using var cmd = new SqlCommand(query, bağlantı);
                cmd.Parameters.Add("@Theme", SqlDbType.NVarChar, 20).Value = (object)Theme ?? DBNull.Value;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = (object)username ?? DBNull.Value;

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL HATASI: " + ex.Message);
                return false;
            }
        }
    }
}