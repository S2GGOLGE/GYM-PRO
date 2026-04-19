namespace SeneOdev
{
    public class WeatherForecast
    {
        //Kullanıcının Kayıt Olduğu Vakti Yıl Ay Gün olarak Crated At Sutununa Kaydeder
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
