using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NetTcpListenerThreadProject
{
    internal class ChatClient
    {
        TcpClient client;
        public ChatClient(TcpClient client)
        {
            this.client = client;
        }
        public void Chat()
        {
            NetworkStream stream = null!;
            try
            {
                stream = client.GetStream();
                byte[] buffer = new byte[1024];

                while (true)
                {
                    StringBuilder strClient = new StringBuilder();
                    int bufferSize = 0;
                    do
                    {
                        bufferSize = stream.Read(buffer, 0, buffer.Length);
                        strClient.Append(Encoding.Default.GetString(buffer, 0, bufferSize));
                    } while (stream.DataAvailable);
                    Console.WriteLine($"Client message {DateTime.Now.ToShortDateString()}: {strClient.ToString()}");

                    string strListener = "Your message: " + strClient.ToString();
                    buffer = Encoding.Default.GetBytes(strListener);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if(stream != null)
                    stream.Close();
                if(client != null)
                    client.Close();
            }

        }
    }
}
