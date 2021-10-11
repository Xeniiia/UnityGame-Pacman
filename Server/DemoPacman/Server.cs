using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

namespace DemoPacman
{
    class Server
    {

        public static ManualResetEvent connectDone = new ManualResetEvent(false);
        public static ManualResetEvent sendDataDone = new ManualResetEvent(false);
        public static ManualResetEvent receiveDone = new ManualResetEvent(false);
        public static ManualResetEvent levelInfoSend = new ManualResetEvent(false);

        //Создадим сокет сервера
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Создадим буфер (обработка данных)
        private static byte[] buffer = new byte[12000];
        public static Client client = new Client();

        public static void SetupServer()
        {
            //Привяжем ip-порт
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 4000));
            Console.WriteLine("A socket was created.");
            serverSocket.Listen(1);
            Console.WriteLine("Server wait...");
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Console.WriteLine("Client try connected");
            //"Одобряем" подключение
            //Получаем данные о подключенном клиенте и записываем в "socket"
            Socket socket = serverSocket.EndAccept(ar);
            client.socket = socket;


            // Основной поток программы Program ждет этой команды
            // Т.е. продолжит, только когда клиент подключится и данные о нем будут записаны
            connectDone.Set();

            Console.WriteLine("Client was connected.");


            levelInfoSend.WaitOne();
            client.socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), client.socket);

            return;
        }

        public static void SendDataTo(byte[] data)
        {
            if ((data == null) == true)
            {
                Console.WriteLine("Send error: message for send is empty.");
                return;
            }
            client.socket.Send(data);
            data = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            Socket socketClient = (Socket)ar.AsyncState;
            Socket handler = socketClient;
            try
            {
                int received = socketClient.EndReceive(ar);

                if (received > 0)
                {
                    AllData message = ProtoSerialize.Deserialize<AllData>(buffer);
                    HandleInfoFromClient handleInfoFromClient = new HandleInfoFromClient();
                    SendDataTo(handleInfoFromClient.HandleInfo(message));

                    buffer = new byte[12000];
                    handler.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), handler);

                }
                else
                {
                    Console.WriteLine("Client disconnected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
