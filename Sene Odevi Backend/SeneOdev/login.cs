namespace SeneOdev;

using Microsoft.Data.SqlClient;
using System.Data;

public class Login
{
    public static string GirisYap(string username, string password)
    {
        string sonuc = BosAlanKontrolu(username, password);
        if (sonuc != "OK")
            return sonuc;

        string connstring = "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";

        using var baglanti = new SqlConnection(connstring);
        baglanti.Open();

        // 🔴 Tablo ve sütun adı güncellendi
        string komut = "SELECT PasswordHash FROM Kullanici WHERE Username = @username";

        using var islem = new SqlCommand(komut, baglanti);
        islem.Parameters.AddWithValue("@username", username);

        var result = islem.ExecuteScalar();

        if (result == null)
            return "Kullanıcı yok";

        string dbPassword = result.ToString();

        if (dbPassword != password) // dilersen burayı hash kontrolü ile güvenli yapabiliriz
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