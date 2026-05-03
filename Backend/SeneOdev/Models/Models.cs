namespace SeneOdev.Models
{
   public class user
    {
       public int Id { get; set; }
        public string username { get; set; }
        public string Password { get; set; }

    }
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class Theame
    {
        public int Id { get; set; }
        public string ThemaName { get; set; }
    }
}
