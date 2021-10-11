using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ClientNamespace
{
    class Client
    {
        //�������� �����
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //public bool closing = false;
        private byte[] buffer = new byte[12000];

        public static ManualResetEvent handleSignal = new ManualResetEvent(true);


        public void StartClient()
        {
            //������ ���������� �������� ������������
            socket.BeginConnect("127.0.0.1", 4000, new AsyncCallback(ConnectCallback), socket);
            
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            //���� ����������� - ������ �������
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            //�������� ����� �������
            Socket socket = (Socket)ar.AsyncState;

            try
            {
                int received = socket.EndReceive(ar);

                if (received <= 0)
                {
                    CloseClient(); //��������� ������
                }
                else
                {

                    //���� ��������� ������� ��������
                    handleSignal.Reset();

                    byte[] databuffer = new byte[received];     //������� ������ ��� ���������
                    Array.Copy(buffer, databuffer, received);   //�������� ���������� ��������� � ������ (received �����)

                    HandleNetworkInformation handle = new HandleNetworkInformation();
                    handle.Handle(databuffer);
                    buffer = new byte[12000];

                    //���� ��������� ������� ��������
                    handleSignal.WaitOne();


                    //����� ������� ������:
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
