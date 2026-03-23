using System.Net.Sockets;
using System.Net;
using System.Text;
namespace SeneOdev
{
    public class SUNUCU
    {
        public static string Client(string ip, int port)
        {
            using TcpClient client = new TcpClient(ip, port);
            using NetworkStream stream = client.GetStream();

            string mesaj = "selam";
            byte[] data = Encoding.UTF8.GetBytes(mesaj);

            stream.Write(data, 0, data.Length);

            return "Mesaj gönderildi";
        }
    }
}
