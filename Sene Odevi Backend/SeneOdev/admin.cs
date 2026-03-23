namespace SeneOdev
{
    public class admin
    {
        public static string Login(string username, string pass, string role)
        {
            //Geçici
            if (username == "admin" && pass == "1234" && role == "admin")
            {
                return "Giriş başarılı";
            }
            else
            {
                return "Kullanıcı adı veya şifre hatalı";
            }
        }
    }
}