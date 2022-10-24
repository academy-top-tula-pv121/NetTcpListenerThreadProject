using System.Net.Sockets;
using System.Net;

namespace NetTcpListenerThreadProject
{
    internal class Program
    {
        const int port = 10001;
        static TcpListener listener;
        static void Main(string[] args)
        {
            try
            {
                listener = new(IPAddress.Loopback, port);
                listener.Start();
                Console.WriteLine("Server is Listener...");

                while(true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ChatClient clientObj = new(client);

                    Thread threadClient = new(clientObj.Chat);
                    threadClient.Start();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(listener != null)
                    listener.Stop();
            }
        }
    }
}