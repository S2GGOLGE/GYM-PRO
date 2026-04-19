namespace SeneOdev
{
    public class tema
    {
        public static string Tema { get; set; }
        public static string Açık_Tema { get; set; }
        public static string Koyu_Tema { get; set; }
        private readonly string connstring =
          "Data Source=EMREE\\SQLEXPRESS;Initial Catalog=Sene_Odevi;Integrated Security=True;Encrypt=False";
        public bool koyu()
        {
            try
            {
                if (Açık_Tema != Tema)
                {
                    Tema = Koyu_Tema;
                }
                else if (Koyu_Tema != Tema)
                {
                    Tema = Açık_Tema;
                }
            }
            catch(Exception ex)
            {

            }
            return false;
        }
    }
}
