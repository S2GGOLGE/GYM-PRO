using System.Net.Sockets;
using System.Net;
using System.Text;
namespace SeneOdev
{
    public class SUNUCU
    {
        //Sunucuya bağlanmak için bir client hata alıyorum 
        public static string Client(string ip, int port)
        {
            try
            {
                // Zaman aşımı süresi eklemek bağlantı kopmalarını yönetmeni sağlar
                using TcpClient client = new TcpClient();

                // Bağlanmaya çalışırken bir süre sınırı koymak iyidir
                var result = client.BeginConnect(ip, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5)); // 5 saniye bekle

                if (!success) return "Hata: Sunucuya bağlanma zaman aşımına uğradı.";

                client.EndConnect(result);

                using NetworkStream stream = client.GetStream();
                string mesaj = "selam";
                byte[] data = Encoding.UTF8.GetBytes(mesaj);
                stream.Write(data, 0, data.Length);

                return "Mesaj başarıyla gönderildi";
            }
            catch (SocketException ex)
            {
                // 10061: Bağlantı reddedildi (Sunucu kapalı veya port yanlış)
                // 10060: Zaman aşımı (Firewall engeli)
                return $"Soket Hatası: {ex.ErrorCode} - {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Genel Hata: {ex.Message}";
            }
        }
    }
}
