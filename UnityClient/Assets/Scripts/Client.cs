using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ClientNamespace
{
    class Client
    {
        //Создадим сокет
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //public bool closing = false;
        private byte[] buffer = new byte[12000];

        public static ManualResetEvent handleSignal = new ManualResetEvent(true);


        public void StartClient()
        {
            //Клиент асинхронно пытается подключиться
            socket.BeginConnect("127.0.0.1", 4000, new AsyncCallback(ConnectCallback), socket);
            
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            //Если подключился - начать слушать
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            //Получаем сокет сервера
            Socket socket = (Socket)ar.AsyncState;

            try
            {
                int received = socket.EndReceive(ar);

                if (received <= 0)
                {
                    CloseClient(); //Закрываем клиент
                }
                else
                {

                    //Ждем окончания прошлой операции
                    handleSignal.Reset();

                    byte[] databuffer = new byte[received];     //Создаем массив для сообщения
                    Array.Copy(buffer, databuffer, received);   //копируем полученное сообщение в массив (received длина)

                    HandleNetworkInformation handle = new HandleNetworkInformation();
                    handle.Handle(databuffer);
                    buffer = new byte[12000];

                    //Ждем окончания прошлой операции
                    handleSignal.WaitOne();


                    //Снова слушаем сервер:
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                }
            }
                catch 
            {
                    CloseClient();
            }
        }

        private void CloseClient()
        {
            //closing = true;
            socket.Close();
        }

        public static void SendDataTo(byte[] data)
        {
            if ((data == null) == true)
            {
                return;
            }

            socket.Send(data);
            data = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }



}
